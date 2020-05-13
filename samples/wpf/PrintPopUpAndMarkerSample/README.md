# Print Popup And Marker Sample for Wpf

### Description

Popup and marker need to use click events etc, so they inherit Control class. SimpleMarkerOverlay and PopupOverlay can't be printed directly, and They need to be converted to image. This sample demonstrates how to print popup and marker by PrinterGeoCanvas. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/PrintPopupAndMarkerSample-ForWpf/blob/master/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private ImagePrinterLayer CreatePrinterLayerByOverlay(Overlay overlay, PointShape centerPoint)
{
   Canvas canvas = overlay.OverlayCanvas;
   RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)map.ActualWidth, (int)map.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
   canvas.Measure(new System.Windows.Size((int)map.ActualWidth, (int)map.ActualHeight));
   canvas.Arrange(new Rect(new System.Windows.Size((int)map.ActualWidth, (int)map.ActualHeight)));
   renderBitmap.Render(canvas);

   PngBitmapEncoder encoder = new PngBitmapEncoder();
   encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
   MemoryStream stream = new MemoryStream();
   encoder.Save(stream);

   ImagePrinterLayer layer = new ImagePrinterLayer();
   var image = new GeoImage(stream);
   layer.Image = image;
   layer.SetPosition(image.Width, image.Height, centerPoint, PrintingUnit.Point);

   return layer;
}
```

### Getting Help

- [Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)
- [Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)
- [ThinkGeo Community Site](http://community.thinkgeo.com/)
- [ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.ImagePrinterLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.ImagePrinterLayer)
- [ThinkGeo.MapSuite.Layers.MapPrinterLayer](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.MapPrinterLayer)
- [ThinkGeo.MapSuite.Layers.PrinterGeoCanvas](http://wiki.thinkgeo.com/wiki/api/ThinkGeo.MapSuite.Layers.PrinterGeoCanvas)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
