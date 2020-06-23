# Large Scale Map Printing Sample for Wpf

### Description

This WPF project is the second in our series of samples on printing. The

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/LargeScaleMapPrintingSample/ScreenShot.png)

### Requirements

This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();

Map1.InteractiveOverlays.Add("PrintPreviewOverlay", printerOverlay);
Map1.InteractiveOverlays.MoveToBottom("PrintPreviewOverlay");

PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
pagePrinterLayer.Open();
printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Wpf.PrinterInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.printerinteractiveoverlay)
- [ThinkGeo.MapSuite.Wpf.InteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.interactiveoverlay)
- [ThinkGeo.MapSuite.Layers.PagePrinterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.pageprinterlayer)
- [ThinkGeo.MapSuite.Layers.PrinterPageSize](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerpagesize)
- [ThinkGeo.MapSuite.Layers.PrinterOrientation](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerorientation)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
