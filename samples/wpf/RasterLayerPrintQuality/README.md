# Raster Layer Print Quality Sample for Wpf

### Description
This WPF project is the 3rd sample on the printing series. It demonstrates how to print your maps in high quality. This new feature for Map Suite is available in version 9.0.483.0 or later. From the sample you will better understand how to use DPI for handling print quality. The sample is based on raster images, but could also be used with vector data.

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://gitlab.com/thinkgeo/public/thinkgeo-desktop-maps/-/raw/support/v10/samples/wpf/RasterLayerPrintQuality/Screenshot.png)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private MapPrinterLayer mapPrinterLayer;
private PrinterInteractiveOverlay printerOverlay;
private PagePrinterLayer pageLayer;

private void Window_Loaded(object sender, RoutedEventArgs e)
{
    foreach (var name in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
    {
        cmbPrinterName.Items.Add(name);
    }
    PrinterSettings s = new PrinterSettings();
    cmbPrinterName.SelectedItem = s.PrinterName;

    printerOverlay = new PrinterInteractiveOverlay();

    pageLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Landscape);
    pageLayer.Open();

    mapPrinterLayer = new MapPrinterLayer();
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.MapPrinterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.mapprinterlayer)
- [ThinkGeo.MapSuite.Wpf.PrinterInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.printerinteractiveoverlay)
- [ThinkGeo.MapSuite.Layers.PagePrinterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.pageprinterlayer)
- [ThinkGeo.MapSuite.Layers.PrinterPageSize](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerpagesize)
- [ThinkGeo.MapSuite.Layers.PrinterOrientation](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerorientation)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
