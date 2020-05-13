# Use Wpf Map Control Sample for WinForms

### Description

The wpf map control supports multi-thread to render map, so it performs better than winforms map control. This sample demonstrates how you can use wpf map control in your winforms applications. 


Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/UseWpfMapControlSample-ForWinforms/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
/*===========================================
   Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
   a Client ID and Secret. These were sent to you via email when you signed up
   with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

public Form1()
{
    InitializeComponent();

    // Initialize the WpfMap object and add it to the ElementHost. 
    map = new WpfMap();
    elementHost1.Child = map;
}

private void Form1_Load(object sender, EventArgs e)
{
    map.MapUnit = GeographyUnit.Meter;
    map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

    ThinkGeoCloudRasterMapsOverlay thinkgeoBackgroundOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeoCloudClientId", "ThinkGeoCloudClientSecret");
    // Set up the TileCache for the background overlay. 
    thinkgeoBackgroundOverlay.TileCache = new XyzFileBitmapTileCache(".\\Cache");
    map.Overlays.Add(thinkgeoBackgroundOverlay);

    ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\Countries.shp");
    shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black);
    shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
    shapeFileFeatureLayer.FeatureSource.Projection = new Proj4Projection(Proj4Projection.GetDecimalDegreesParametersString(), Proj4Projection.GetSphericalMercatorParametersString());

    LayerOverlay layerOverlay = new LayerOverlay();
    layerOverlay.Layers.Add(shapeFileFeatureLayer);
    // Set up the TileCache for LayerOverlay.
    layerOverlay.TileCache = new FileBitmapTileCache(".\\Cache", "LayerOverlay");
    map.Overlays.Add(layerOverlay);

    map.CurrentExtent = new RectangleShape(-19489607.6157655, 12836528.7107853, 19293928.8244426, -8022830.44424083);
}
```
### Getting Help

[Map Suite Desktop for WinForms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for WinForms Product Description](https://thinkgeo.com/ui-controls#winforms-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

Working...


### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
