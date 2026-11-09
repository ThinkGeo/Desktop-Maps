In this article, we will demonstrate how to create an aesthetically pleasing map in a desktop project using VS Code. The project is designed for Windows and uses .Net 8. However, you can also create a similar .Net Framework/.Net Core project using Visual Studio. 

# Desktop Maps Quick Start: Display a Simple Map using VS Code

To begin working on the map, you will need to create a project in VS Code. Afterwards, we will guide you through the process of adding the required packages and setting up the map by adding a background.

### Step 1: Setup VS Code

First, install [.NET 8.0 SDK x64](https://dotnet.microsoft.com/en-us/download)

Second, ensure that the "C#" and "NuGet Package Manager" extensions are installed in VS Code. To do this, click on the Extensions button on the left-hand side of VS Code, search for the two extensions by name, and install them. Once the installation is complete, you will find them under the INSTALLED group.

<img src="./assets/Add_Extensions_ScreenShot.gif"  width="840" height="580">

### Step 2: Create a WPF Project

First, select **File → Open Folder** and choose a blank folder. If the TERMINAL window is not already open, select **Terminal → New Terminal** to open it. In this example, we will use the folder "D:\MapDemo", which will also be visible in the terminal. The name of the folder, "MAPDEMO," should show up in the explorer window on the left.

Second, create a WPF project by typing "**dotnet new wpf**" in the terminal and pressing enter. The project will be created, and the newly generated files can be seen in the explorer window on the left.

Next, select **View → Command Palette** and choose **.NET: Generate Assets for Build and Debug** to generate assets for building and debugging.

To begin debugging, press F5 or select **Run → Start Debugging**. If a blank window appears, congratulations! Your project is set up correctly.

<img src="./assets/Create_WPF_Project_ScreenShot.gif"  width="840" height="580">

### Step 3: Add Nuget Packages

First, select **View → Command Palette** and choose **NuGet Package Manager: Add Package**. This will open the NuGet Package Search Box. Type "ThinkGeo.UI" into the search box and press Enter. From the list that appears, select **ThinkGeo.UI.Wpf**.

Next, select the latest version of the package (in this guide, it's version 15.0.0). Click on the version to add it to the project. You will then be prompted to restore any unresolved dependencies. Click Restore to download the necessary NuGet packages, and you're all set!

<img src="./assets/Add_Nuget_Packages_ScreenShot.gif"  width="840" height="530">

### Step 4: Add the ThinkGeo Background

Add ThinkGeo.UI.Wpf namespace to `MainWindow.xaml`

```xml
xmlns:thinkgeo="clr-namespace:ThinkGeo.UI.Wpf;assembly=ThinkGeo.UI.Wpf"
```
Add a map control named 'mapView' to the Grid element in MainWindow.xaml, and include the mapView_Loaded event.

```xml
<thinkgeo:MapView x:Name="mapView" Loaded="mapView_Loaded"></thinkgeo:MapView>
```
Import the namespace at the top of 'MainWindow.xaml.cs` file.

```csharp
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
```
Add the following code to the mapView_Loaded event, which is triggered when the map view is fully loaded and ready to use. (The key passed to ThinkGeoVectorTileOverlay is a ThinkGeo Cloud key for the background map, for testing only; it is not your ThinkGeo license key. You can apply for your own Cloud key from [ThinkGeo Cloud](https://cloud.thinkgeo.com/clients.html))

Set up a tile cache for the base overlay to improve performance. The cache retrieves tiles from the local disk instead of downloading them from the internet each time they are needed.

```csharp
private async void mapView_Loaded(object sender, RoutedEventArgs e)
{
// Set the Map's Unit to Meter.
mapView.MapUnit = GeographyUnit.Meter;
// Set the Current Extent to the Max Extent of ThinkGeo Map.
mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;
                        
// Add a base map overlay.
var baseOverlay = new ThinkGeoVectorTileOverlay("SGjwpxF60knqZi3R0A9RRE3GqiuRA1LsnxHMot7rJ58~");
// Set up the tile cache for the base overlay, passing in the location and an ID to distinguish the cache.     
baseOverlay.TileCache = new FileRasterTileCache(@".\cache", "basemap");

// Add the newly created overlay to mapView.
mapView.Overlays.Add(baseOverlay);
                        
// Refresh the Map
await mapView.RefreshAsync();
}
```

### Step 5: Run the Sample

Press F5 to build and run the project. You don't need to register or install anything first: ThinkGeo 15 checks its license when your project **builds**, not when it runs.

- **Without a license key**, the first build on your machine starts a free 30-day trial. The build succeeds with warning `TG0001`, which tells you how many days of the trial are left, and the map shows a *Not Licensed for Runtime* watermark. After 30 days the build stops with error `TG0002` until a key is in place.
- **With a license key**, the watermark goes away. Sign in at [helpdesk.thinkgeo.com](https://helpdesk.thinkgeo.com), download your Desktop key (`desktop.thinkgeokey`) and save it, with its name unchanged, in `%LOCALAPPDATA%\ThinkGeo\`. Then build again. There is nothing to activate and nothing to restart.

The [License Guide for ThinkGeo 15](https://docs.thinkgeo.com/products/misc/license-guide-v15/) explains [how to get your key](https://docs.thinkgeo.com/products/misc/license-guide-v15/#get-your-license-key), [what each `TG` build message means](https://docs.thinkgeo.com/products/misc/license-guide-v15/#what-the-build-tells-you) and [how to build on a build server](https://docs.thinkgeo.com/products/misc/license-guide-v15/#build-servers-ci-and-containers). If you used ThinkGeo 14 before: Product Center and runtime license files are no longer used, see [What Changed from ThinkGeo 14](https://docs.thinkgeo.com/products/misc/license-guide-v15/#what-changed-from-thinkgeo-14).

Now you should see the map with our Cloud Maps layer! You can double-click to zoom in, use the mouse wheel to zoom in/out, and track zoom in by holding down the Shift key and tracking the map. Additionally, you can rotate the map by holding down the Alt key and dragging the map.

<img src="./assets/Cloud_Maps_Layer_ScreenShot.gif"  width="840" height="580">

## Summary

You now have a basic understanding of how to use the ThinkGeo Map controls in VS Code and can begin adding functionality to your own applications. Let's review what we've learned about the object relationships and how the pieces of ThinkGeo UI work together:

1. A `MapView` is the fundamental control that contains all the other objects used to determine how the map is rendered.
2. A `MapView` has multiple `Overlays`, and each `Overlay` corresponds to a tier of images displayed on the map control.
3. The `MapUnit` (feet, meters, decimal degrees, etc.) and `CurrentExtent` need to be correctly set for the `Map` control.

Congratulations, you are now in an excellent position to review the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/samples/wpf/HowDoISample) and explore other features.
