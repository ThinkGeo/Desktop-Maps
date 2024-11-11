# Quick Start Guide on VS for WinForms

This guide is based on Visual Studio 2022. The project created in this guide is developed in .NET 8.

## Display a Simple Map using WinForms

In this section, we'll show you how to create a visually appealing map with external data and styling. We highly recommend that you also take a look at the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/samples/winforms/HowDoISample?ref_type=heads), which contains numerous examples that cover virtually everything you can do with the control.

First, to begin working on the map, you'll need to create a .NET WinForms project using Visual Studio 2022. Once that's done, we'll guide you through adding the required packages and setting up the map with a background on the default form. Next, we'll show you how to add a shapefile to the map and customize it with styles.

### Step 1: Create a WinForms Project
Create a C# WinForms project with .NET 8.0. 

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WinForms_Create_Project_ScreenShot.gif"  width="840" height="580">

### Step 2: Add Nuget Packages: 

Install **ThinkGeo.UI.WinForms** NuGet package through NuGet package manager.

You can switch between the Beta Branch and Release Branch by checking/unchecking the "Include prerelease" checkbox. The Beta Branch contains the latest features/bug fixes, while the Release Branch is more stable and better tested.

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WinForms_Add_Nuget_Packages_ScreenShot.gif"  width="840" height="580">

### Step 3: Enable WPF Support for the Project

When using `ThinkGeo.UI.WinForms` in a WinForms project, you may need to enable support for WPF assemblies, specifically `WindowsBase` and `WindowsFormsIntegration`, due to the following reasons:

- ThinkGeo Controls and WPF Integration: ThinkGeo’s WinForms controls often rely on Windows Presentation Foundation (WPF) functionality when running on .NET. The required `WindowsBase.dll` file, part of the .NET runtime, supports WPF elements within the application.

- Hosting WPF Content in WinForms: `WindowsFormsIntegration.dll` is essential for integrating Windows Forms and WPF content, allowing you to host WPF elements within a Windows Forms application if needed. Referencing this library enables smooth interaction between WPF and WinForms controls.

To enable these dependencies:

1. Open Project Properties: In Visual Studio, right-click your project and select `Properties`.

2. Enable WPF Support:

   - Go to the `Application` tab and then to the `General` section.
   - Check the box labeled `Enable WPF for this project`.

Enabling this option automatically includes the necessary WPF assemblies, such as `WindowsBase` and `WindowsFormsIntegration`, you no longer need to manually locate and add these DLLs, as Visual Studio will handle the references for you.

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WinForms_Enable_WPF_ScreenShot.gif"  width="840" height="610">

### Step 4: Add the Map Control to `Form1.cs`

Add the `ThinkGeo.Core` and `ThinkGeo.UI.WinForms` namespaces to the `Form1.cs` file using a `using` directive.

```csharp
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
```

Add the map control to the `Form1` class in the `Form1.cs` and initialize it in the constructor.

```csharp
 public partial class Form1 : Form
 {
     private MapView mapView;
     public Form1()
     {
         InitializeComponent();
         mapView = new ThinkGeo.UI.WinForms.MapView();
         mapView.Size = new System.Drawing.Size(800, 450);
         mapView.Dock = DockStyle.Fill;
         Controls.Add(this.mapView);
     }
 }
```

### Step 5: Add the ThinkGeo Background Map
Add the form's load event in the `Form1` constructor to display the map.

```csharp
 this.Load += new System.EventHandler(this.Form_Load);
```

Add the following code to the `Form_Load` event, which is triggered when `Form1` is fully loaded and ready to use. (The key passed to ThinkGeoCloudVectorMapsOverlay is for test only; you can apply for your own key from [ThinkGeo Cloud](https://cloud.thinkgeo.com/clients.html))

We have set up a tile cache for the base overlay to improve performance. This cache retrieves tiles from the local disk instead of downloading them from the internet each time they are needed.

```csharp
private async void Form_Load(object? sender, EventArgs e)
{
    // Set the map's unit of measurement to meters(Spherical Mercator)
    mapView.MapUnit = GeographyUnit.Meter;

    // Add Cloud Maps as a background overlay
    var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
    {
        ClientId = "AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~",
        ClientSecret = "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~",
        MapType = ThinkGeoCloudVectorMapsMapType.Light,
        TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
    };
    mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

    // Set the map extent
    mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

    await mapView.RefreshAsync();
}
```
### Step 6: Run the Sample & Register for Your Free Evaluation

The first time you run your application, if you have not installed a license, you may encounter a 'licenses not installed' exception. 

![Registration Exception](https://docs.thinkgeo.com/products/desktop-maps/assets/WPF_LicenseNotInstalledException.png "Registration Exception")

You can open [ThinkGeo's Registration Website](https://helpdesk.thinkgeo.com/register) to create an account and start a 30-day free evaluation. From there, you can download and install the Product Center to manage licenses for ThinkGeo products.  

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WPF_Create_ThinkGeo_Account.png"  width="840" height="570">

Once you activate the 'ThinkGeo UI WinForms' license to start your evaluation, you should be able to see the map with our Cloud Maps layer! You can double-click to zoom in, use the mouse wheel to zoom in/out, and track zoom in by holding down the Shift key and tracking the map. Additionally, you can rotate the map by holding down the Alt key and dragging the map.

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WinForms_Cloud_Maps_Layer_ScreenShot.gif"  width="840" height="580">

### Step 7: Add a Point Data Layer to the map

Now that you have a basic setup, you can add custom data to the map. Depending on the data, this process can be complex or quite simple. Here, we will add a shapefile to the map, set its style, apply the style across a range of zoom levels, and project the shapefile from one projection (Decimal Degrees) to another (Spherical Mercator). This is essentially what you might need to do when displaying feature data on the map.

Download the [WorldCapitals](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/master/quick-start-guide/QuickStartGuide_WPF_VS/WpfSample/AppData) shapefile data and use it in your project under a new folder called _**AppData**_. From there, we can add the shapefile to the map.

```csharp
// Add a shapefile layer with point style.
var capitalLayer = new ShapeFileFeatureLayer(@"..\..\..\AppData\WorldCapitals.shp");
var capitalStyle = new PointStyle()
{
    SymbolType = PointSymbolType.Circle,
    SymbolSize = 8,
    FillBrush = new GeoSolidBrush(GeoColors.White),
    OutlinePen = new GeoPen(GeoColors.Black, 2)
};
// Each layer has 20 preset zoomlevels. Here we set the capitalStyle for ZoomLevel01 and apply the style to the other preset zoomlevels.
capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = capitalStyle;
capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

// The shapefile is in Decimal Degrees, while the base overlay is in Spherical Mercator.
// It's why the shapefile needs to be reprojected to match the coordinate system of the base overlay.
capitalLayer.FeatureSource.ProjectionConverter = 
    new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

// Add the layer to an overlay, add that overlay to the map.
var customDataOverlay = new LayerOverlay();
customDataOverlay.Layers.Add(capitalLayer);
mapView.Overlays.Add(customDataOverlay);           
```
Now, the data shows up properly on the map!

<img src="https://docs.thinkgeo.com/products/desktop-maps/assets/WinForms_QuickStart_ShapeFile_PointStyle_ScreenShot.gif"  width="840" height="580">

## Summary

You now have a basic understanding of how to use the ThinkGeo Map controls and can begin adding functionality to your own applications. Let's review what we've learned about the object relationships and how the pieces of ThinkGeo UI work together:

1. A `MapView` is the fundamental control that contains all the other objects used to determine how the map is rendered.
2. A `MapView` has multiple `Overlays`, and each `Overlay` corresponds to a tier of images displayed on the map control.
3. A `LayerOverlay` has multiple layers, and each `Layer` corresponds to a single data source. This can be a vector-based `FeatureLayer` (such as a shapefile or SQLite) or a raster-based `RasterLayer` (such as TIFF or MrSID).
4. A `FeatureLayer` can have multiple `ZoomLevels`, which define the upper and lower ranges of when a layer should be shown or hidden, and the styles for how the layer is supposed to be displayed.
5. The `MapUnit` (feet, meters, decimal degrees, etc.) and `CurrentExtent` need to be correctly set for the `Map` control.

Congratulations, you are now in an excellent position to review the [How Do I Sample](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/tree/develop/samples/winforms/HowDoISample?ref_type=heads) and explore other features.
