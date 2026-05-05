using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LocalServer
{
    class Program
    {
        static string LOG_FILEPATH = "ServerEntries.log";

        class AsyncListener
        {
            public string IpAddress { get; set; }
            public int Port { get; set; }
            public Task? Task { get; set; }

            public AsyncListener(string ipAddress, int port, Task? task = null)
            {
                IpAddress = ipAddress;
                Port = port;
                Task = task;
            }
        }

        static async Task Main(string[] args)
        {
            // SETTINGS

            const string IP_ADDRESS = "127.0.0.1";
            int[] HTTP_PORTS = { 80, 443 };

            // PROGRAM START

            File.WriteAllText(LOG_FILEPATH, string.Empty);

            var listeners = new List<AsyncListener>(HTTP_PORTS.Length);

            foreach (var PORT in HTTP_PORTS)
            {
                listeners.Add(new AsyncListener(IP_ADDRESS, PORT, null));
            }

            var tasks = new List<Task>(listeners.Count);

            foreach (var listener in listeners)
            {
                listener.Task = Task.Run(() => StartListenerAsync(listener));

                Log($"[HTTP {listener.IpAddress}:{listener.Port}] Started listening.");

                tasks.Add(listener.Task);
            }

            Log("All listeners started.\n");

            await Task.WhenAll(tasks);

            Console.WriteLine("Server offline. Press any key to exit...");
            Console.ReadKey();
        }

        static async Task StartListenerAsync(AsyncListener asyncListener)
        {
            var tcpListener = new TcpListener(IPAddress.Parse(asyncListener.IpAddress), asyncListener.Port);

            tcpListener.Start();

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();

                _ = Task.Run(() => HandleClient(client, asyncListener));
            }
        }

        static async Task HandleClient(TcpClient client, AsyncListener listener)
        {
            // read message

            using (client)
            using (var stream = client.GetStream())
            {
                var buffer = new byte[4096];

                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0) return;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                string messageHeader = message.Substring(0, message.IndexOf('{') - 1);

                string jsonData = message.Substring(
                    message.IndexOf('{'),
                    message.LastIndexOf('}') + 1 - message.IndexOf('{')
                );

                string formattedJsonData = JToken.Parse(jsonData).ToString(Newtonsoft.Json.Formatting.Indented);

                Log($"[{listener.IpAddress}:{listener.Port}] Received:\n\n" + messageHeader + "\n" + formattedJsonData + "\n");

                // respond to message
                string jsonResponse = "{\"status\": 0, \"data\": {\"aircraft\": []}}";

                // If the response contains an empty aircraft array, populate it using Newtonsoft.Json
                if (jsonResponse.Contains("\"aircraft\": []"))
                {
                    var jObject = JObject.Parse(jsonResponse);
                    jObject["data"]!["aircraft"] = new JArray("b01b", "adfx");
                    jsonResponse = jObject.ToString(Newtonsoft.Json.Formatting.None);
                }

                byte[] bodyBytes = Encoding.UTF8.GetBytes(jsonResponse);

                string responseHeader =
                    "HTTP/1.1 200 OK\r\n" +
                    "Content-Type: application/json;charset=utf-8\r\n" +
                    $"Content-Length: {bodyBytes.Length}\r\n" +
                    "Connection: close\r\n" +
                    "\r\n"; // end of headers

                byte[] headerBytes = Encoding.UTF8.GetBytes(responseHeader);

                await stream.WriteAsync(headerBytes, 0, headerBytes.Length);
                await stream.WriteAsync(bodyBytes, 0, bodyBytes.Length);

                await stream.FlushAsync();
            }
        }

        static void Log(string data)
        {
            Console.WriteLine(data);

            File.AppendAllText(LOG_FILEPATH, data + '\n');
        }
    }
}