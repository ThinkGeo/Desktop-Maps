# Desktop Maps

## Introduction

  Welcome, we're glad you're here!  If you're new to ThinkGeo's Desktop Maps we suggest that you start by taking a quick look below.  This will introcude you to getting a nice lookin map up and running with some external data and styling.  After reviewing this we strongly reccomend that you open the [How Do I Sample](samples/wpf/HowDoISample/HowDoI).  It's packed with dozens of examples covering nearly everything you can do with the control.

**Recap**

1. Take a quick look at the code below.
2. Open the [How Do I Sample](samples/wpf/HowDoISample/HowDoI) and explore all our features. 

## Samples

  We have a number of samples and the are 

[check out HowDoI samples](samples/wpf/HowDoISample/HowDoI)

## Display a Simple Map

We will begin by creating a .NET Core WPF project in your favorite editor.  Next we will walk you through addind the required packages and getting a map on the default form.  Next we will add some code to show a nice looking background map and finally add some custom which will be styled and labeled.  After reading this you will be in a good position to look over the [How Do I Sample](samples/wpf/HowDoISample/HowDoI) and explore our other features.

 Intro: what you'll be doing, and priming them on Product Center

### Step 1: Setup a New Project ###

  In your editor of choice you need to create a **.NET Core WPF** project.  Please see your editor's instructions on how to create the project.  We have included a sample video in Visual Studio below.  

[Visual Studio 2019 Example](https://channel9.msdn.com/Series/Desktop-and-NET-Core-101/Create-your-first-WPF-app-on-NET-Core)

### Step 2: Add NuGet Packages ###

You will need to install the **ThinkGeo.UI.Wpf** NuGet package.  We highly suggest you use your editors [built in NuGet package manager](https://docs.microsoft.com/en-us/nuget/quickstart/) if possible.  If you're not using an IDE you can [install it via the the dotnet CLI](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-dotnet-cli) from inside your project folder where where your project file exists.

```shell
dotnet add package ThinkGeo.UI.Wpf
```
### Step 3: Add the Map Control to the `MainWindow.xaml` ###

Add the new namespace to the `MainWindow.xaml` exisiting XAML namespaces.

```xml
xmlns:uc1="clr-namespace:ThinkGeo.UI.Wpf;assembly=ThinkGeo.UI.Wpf"
```

Add the control to `MainWindow.xaml` file inside the Grid element.

```xml
<uc1:MapView x:Name="mapView" Loaded="MapViewLoaded"></uc1:MapView>
```

### Step 4: Add Namespaces and the MapViewLoaded Event to `MainWindow.xaml.cs` ###

Add the required usings.

```csharp
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
```

Setup the `OnMapLoaded` event which will run when the map control is finsihed being loaded.

```csharp
        private void MapViewLoaded(object sender, RoutedEventArgs e)
        {
        }
```

### Step 5: Add the Background World Overlay ###

Add the code below to the `MapViewLoaded` event of the `MainWindow.xaml.cs`.

```csharp
            // Set the Map Unit.
            mapView.MapUnit = GeographyUnit.Meter;
 
            // Add a background layer
            var backgroundOverlay = new LayerOverlay();
            backgroundOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromHtml("#F0EEE8"))));
 
            // Add a base map overlay.
            var cloudRasterBaseMapOverlay = new ThinkGeoCloudRasterMapsOverlay("USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~", "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~", ThinkGeoCloudRasterMapsMapType.Light);
            mapView.Overlays.Add(cloudRasterBaseMapOverlay);
 
            mapView.CurrentExtent = new RectangleShape(-20000000, 20000000, 20000000, -20000000);
```

### Step 5: Run the Sample & Register for Your Free Evaluation ###

Run application

Starting Evaluation

[link](URL)

![image](imageURL)

### Step 6: Add a Point Data Layer ###

Add `FeatureSource` to map

```csharp
var capitolLayer ...
```

### Step 6: Add Styling and Labeling to the Points ###

Add styling to feature layer

```csharp
var capitolStyle ...
```
