# Graticule With Google Projection Sample for Wpf

### Description

In this Wpf project, we explore more features of the **GraticuleAdornmentLayer**, which shows meridians and parallels at various intervals based on the zoom level. Being natively in Geodetic, **GraticuleAdornmentLayer** can be set to any projection. In this project’s example, you have the graticule showing with World Map Kit in Spherical Mercator (Google Map projection). Also, note how easily you can change the appearance with properties such as **GraticuleLineStyle** and **GraticuleTextFont**.
              
Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/GraticuleWithGoogleProjectionSample-ForWpf/blob/master/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
Proj4Projection proj4 = new Proj4Projection();
proj4.InternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326);
proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
proj4.Open();

GraticuleFeatureLayer graticuleAdornmentLayer = new GraticuleFeatureLayer();
graticuleAdornmentLayer.Projection = proj4;
//Set the styles of the GraticuleAdornmentLayer.
LineStyle graticuleLineStyle = new LineStyle(new GeoPen(GeoColor.FromArgb(150, GeoColor.StandardColors.Navy), 1));
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Shapes.Proj4Projection](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.shapes.proj4projection)
- [ThinkGeo.MapSuite.Layers.GraticuleFeatureLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.graticulefeaturelayer)
- [ThinkGeo.MapSuite.Styles.LineStyle](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.styles.linestyle)
- [ThinkGeo.MapSuite.Drawing.GeoPen](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geopen)
- [ThinkGeo.MapSuite.Drawing.GeoColor](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.drawing.geocolor)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
