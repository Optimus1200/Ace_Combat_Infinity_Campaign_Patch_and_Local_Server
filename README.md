# Requirements
- [ASP.NET Core Runtime 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (Hosting Bundle recommended for Windows)
- [RPCS3](https://rpcs3.net/)
- [PS3 Firmware](https://www.playstation.com/en-us/support/hardware/ps3/system-software/)
- [Ace Combat Infinity Patch](https://wiki.rpcs3.net/index.php?title=Ace_Combat_Infinity&oldid=58822)
- A legitimate copy of Ace Combat: Infinity v2.11 along with its license file

# Getting Started
0. Set up RPCS3: https://rpcs3.net/quickstart
1. In Releases, download the LocalServer_*.zip file for your OS.
2. Place `imported_patch.yml` in your RPCS3/patches folder.
3. If you haven't, create an RPCN account and sign in to RPCN.
4. Open RPCS3 and go to Manager -> Game Patches -> ACE COMBAT INFINITY and enable the patch.
5. In your RPCS3 game library, right-click the game and select `Create custom configuration`
6. Go to Settings -> Network -> paste `dev-wind.siliconstudio.co.jp=127.0.0.1` in IP/Hosts switches.
7. Make sure Network Status is set to `Connected` and PSN Status is `RPCN`.
8. Click `Apply` and `Save custom configuration`.
9. Extract the server .zip file you downloaded.
10.
    (Windows) Run `LocalServer.exe`. 
    (Linux) Run `sudo ./LocalServer`
12. Launch the game and keep pressing the `X` button past the title screen. The screen will go black for a second and then load to the main menu.

# Notes
- This server only listens and sends OK responses. It does not save game data nor send game data. It is not possible to save data with this server as it is.

# Legal Disclaimer

**OPERATION ETERNAL LIBERATION** is an independent, community-driven revival project and is **not** affiliated with, endorsed by, or otherwise connected to Bandai Namco Entertainment Inc. or any of the original rights holders of *Ace Combat Infinity*.

This project is developed strictly for **non-commercial, hobby, and preservation purposes**. It is, and will always remain, **freely available** via its official GitHub repository. Any distribution of this content in exchange for payment or other compensation is entirely unauthorized and is not endorsed by the -OPERATIONS- team. If you encounter such activity, we strongly encourage you to report it.

***

## Intellectual Property Notices

- **Ace Combat™** and **Ace Combat Infinity™** are intellectual properties of **Bandai Namco Entertainment Inc.** All rights reserved.
- All trademarks, copyrights, aircraft designations, manufacturer names, trade names, brand names, and visual imagery depicted in the original game remain the exclusive property of their respective owners and Bandai Namco Entertainment Inc.
- **PlayStation®** is a registered trademark of **Sony Interactive Entertainment Inc.**

> This project makes no claim of ownership over any of the above intellectual properties. All original assets, names, and likenesses are used solely for interoperability and non-commercial fan preservation purposes.
