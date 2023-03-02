This guide is based on using VS Code in .Net 7 WPF. There are tons of map services we can use to set up a base map, however, only a few articles talk about how to do it for a Desktop project, and they are all over-complicated. In this article, I will cover how to simply create a desktop WPF application that shows a 3rd party background map using VS Code.  

# Desktop Maps Quick Start: Display a Simple Map using VS Code

In this section, we'll show you how to create a visually appealing map using VS Code. 

First, to begin working on the map, you'll need to create a .NET VS Code project using Visual Studio 2022. Once that's done, we'll guide you through the process of adding the required packages and getting the map set up on the default form. 

Next, we'll show you how to add a background to the map.

### Step 1: Setup VS Code

VS Code is a lightweight, open source development environment loved by millions of developers. I'm going to finish today's project using VS Code. This project will use .NET 7.0 on a Windows machine.

First, install [.NET 7.0 SDK x64](https://dotnet.microsoft.com/en-us/download)

Second, make sure "C#" and "NuGet Package Manager" extensions have been installed in VS Code. Click the Extensions button on the left in VS Code, search the 2 extensions by name and install them. You will see them under INSTALLED group once finished.

<img src="./assets/Add_Extensions_ScreenShot.gif"  width="840" height="580">
