# Desktop Maps Quick Start: Display a Simple Map using WPF


This section will introduce you to getting a nice looking map up and running with some external data and styling.  After reviewing this we strongly recommend that you open the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/samples/wpf/HowDoISample).  It's packed with dozens of examples covering nearly everything you can do with the control.

We will begin by creating a .NET Core WPF project in your favorite editor.  Next we will walk you through adding the required packages and getting a map on the default form.  Next we will add some code to show a nice looking background map and finally add some custom which will be styled and labeled.  After reading this you will be in a good position to look over the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/samples/wpf/HowDoISample) and explore our other features.

![alt text](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/master/assets/quickstart_shapefile_pointstyle_screenshot.png "Simple Map")

### Step 1: Setup a New Project

  In your editor of choice you need to create a **.NET Core WPF** project.  Please see your editor's instructions on how to create the project.  Here is a [sample video](https://channel9.msdn.com/Series/Desktop-and-NET-Core-101/Create-your-first-WPF-app-on-NET-Core) using Visual Studio for reference.  

### Step 2: Add NuGet Packages

You will need to install the **ThinkGeo.UI.Wpf** NuGet package.  We highly suggest you use your editors [built in NuGet package manager](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio) if possible.  If you're not using an IDE you can [install it via the the dotnet CLI](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli) from inside your project folder where your project file exists.

```shell
dotnet add package ThinkGeo.UI.Wpf
```

### Step 3: Add the Map Control to the `MainWindow.xaml`

Add the new namespace to the `MainWindow.xaml` existing XAML namespaces.

```xml
xmlns:uc1="clr-namespace:ThinkGeo.UI.Wpf;assembly=ThinkGeo.UI.Wpf"
```

Add the control to `MainWindow.xaml` file inside the `Grid` element.

```xml
<uc1:MapView x:Name="mapView" Loaded="mapView_Loaded" MapUnit="Meter"></uc1:MapView>
```

### Step 4: Add Namespaces to `MainWindow.xaml.cs`

Add the required usings.

```csharp
using ThinkGeo.Core;
```

### Step 5: Add the Background World Overlay

Add the code below to the `mapView_Loaded` event of the `MainWindow.xaml.cs`.

```csharp
     // Add a base map overlay.
            var cloudVectorBaseMapOverlay = new ThinkGeoCloudVectorMapsOverlay("USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~", "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(cloudVectorBaseMapOverlay);
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;
```

### Step 6: Run the Sample & Register for Your Free Evaluation

The first time you run your application, you will be presented with ThinkGeo's Product Center which will create and manage your licenses for all of ThinkGeo's products.
If not it will show licenses not installed Exception.

![alt text](./assets/LicenseNotInstalledException.png "Registration Exception")

Create a new account to begin a 60-day free evaluation.

1. Run the application in Debug mode.
1. Click the "Create a new Account?" link.
1. Fill out your name, email address, password and company name and click register.
1. Check your email and click the "Active Your Account" link.
1. Return to Product Center and login using the credentials your just created and hit "Continue Debugging" button.

You should now see your map with our Cloud Maps layer!

### Step 7: Add a Point Data Layer in the map

Now that you have a basic setup, you can add custom data to the map. Depending on the data, this can be complex or quite simple. We'll be going over the simple basics of adding custom data, with a pitfall or two to help you better understand how our framework can help you get around these issues.

Download the [WorldCapitals.zip](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/assets/WorldCapitals.zip) shapefile data and unzip it in your project under a new folder called `AppData`. Make sure that the files are set to copy to the build output directory. From there, we can add the shapefile to the map.

```csharp
            // Add a shapefile layer with point style.
            var capitalLayer = new ShapeFileFeatureLayer(@"./AppData/WorldCapitals.shp");
            var capitalStyle = new PointStyle()
            {
                SymbolType = PointSymbolType.Circle,
                SymbolSize = 8,
                FillBrush = new GeoSolidBrush(GeoColors.White),
                OutlinePen = new GeoPen(GeoColors.Black, 2)
            };
            capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = capitalStyle;
            capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            // Set the projection of the capitalLayer to Spherical Mercator
            capitalLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Create an overlay to add the layer to and add that overlay to the map.
            var customDataOverlay = new LayerOverlay();
            customDataOverlay.Layers.Add(capitalLayer);
            mapView.Overlays.Add(customDataOverlay);
            
            mapView.Refresh(); 
```
Now, the data shows up properly on the map!

## Summary

You now know the basics of using the ThinkGeo Map controls and are able to get started adding functionality into your own applications. Let's recap what we have learned about the object relationships and how the pieces of ThinkGeo UI work together:

1. It is of the utmost importance that the units (feet, meters, decimal degrees, etc.) be set properly for the Map control based on the data.
1. FeatureLayers provide the data used by a Map control to render a map.
1. A Map is the basic control that contains all of the other objects that are used to tell how the map is to be rendered.
1. A Map has many layers. A Layer correlates one-to-one with a single data source and typically of one type (point, polygon, line etc).
1. A FeatureLayer can have several ZoomLevels. ZoomLevels help to define ranges (upper and lower) of when a Layer should be shown or hidden.

You are now in a great position to look over the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/samples/wpf/HowDoISample) and explore our other features.
