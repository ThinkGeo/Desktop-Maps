# Display File GeoDatabase Sample for WinForms

### Description
This sample demonstrates how you can read data from an ESRI FileGeodatabase, and you will find the code as straightforward as consuming any other data source in Map Suite.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/DisplayFileGeoDatabaseSample/Screenshot.png)

### About the Code
```csharp
FileGeoDatabaseFeatureLayer fileGeoDatabaseFeatureLayer = new FileGeoDatabaseFeatureLayer("../../AppData/Shapes.gdb", "states");
AreaStyle areaStyle = new AreaStyle();
areaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
areaStyle.OutlinePen.DashStyle = LineDashStyle.Solid;
fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
fileGeoDatabaseFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
