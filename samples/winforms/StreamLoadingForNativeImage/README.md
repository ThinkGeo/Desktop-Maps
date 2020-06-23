# Stream Loading For Native Image Sample for WinForms

### Description
As an alternative to loading an Image with the image file from the file system, you can choose to pass your own stream. This project shows you how to use the event StreamLoading of GdiPlusRasterSource for this purpose. In this project, we show how to do this using a Tiff image but you can also use that event for ShapeFileFeatureSource as we show in a previous project “Shapefile Encryption”. Keep in mind that this technique only works with images besides MrSid, ECW and Jpeg2000. These types of images do not work because the providers do not support streams in their decoding SDKs.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/winforms/StreamLoadingForNativeImage/ScreenShot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
NativeImageRasterLayer worldImageLayer = new NativeImageRasterLayer(@"world.tif");
((NativeImageRasterSource)(worldImageLayer.ImageSource)).StreamLoading += new EventHandler<StreamLoadingEventArgs>(MainForm_StreamLoading);
worldImageLayer.UpperThreshold = double.MaxValue;
worldImageLayer.LowerThreshold = 0;
worldImageLayer.IsGrayscale = false;
```
### Getting Help

[Map Suite Desktop for Winforms Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_winforms)

[Map Suite Desktop for Winforms Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.NativeImageRasterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.nativeimagerasterlayer)
- [ThinkGeo.MapSuite.Layers.NativeImageRasterSource](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.nativeimagerastersource)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
