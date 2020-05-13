# Print Map With Multi Pages Sample for Wpf

### Description
In this sample we show you how to add robust printing support to your Map Suite applications for the WPF. Using the code in this sample, you'll be able to build a Print Preview interface that lets your users interactively arrange items on a virtual page before printing the result to a printer, exporting to a PDF or to a bitmap image. The printing system also print maps with multi pages or print multi maps to one page. 

Please refer to [Wiki](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf) for the details.

![Screenshot](https://github.com/ThinkGeo/PrintMapWithMultiPagesSample-ForWpf/blob/master/Screenshot.gif)

### Requirements
This sample makes use of the following NuGet Packages

[MapSuite 10.0.0](https://www.nuget.org/packages?q=ThinkGeo)

### About the Code
```csharp
private void PrintClick(object sender, RoutedEventArgs e)
{
    PagePrinterLayer pagePrinterLayer = selectPageOverlay.PrinterLayers["PageLayer"] as PagePrinterLayer;
    PrintDocument printDocument = new PrintDocument();
    printDocument.DefaultPageSettings.Landscape = true;
    if (pagePrinterLayer.Orientation == PrinterOrientation.Portrait)
        printDocument.DefaultPageSettings.Landscape = false;

    printDocument.DefaultPageSettings.PaperSize = GetPrintPreviewPaperSize(pagePrinterLayer);

    PrinterGeoCanvas printerGeoCanvas = new PrinterGeoCanvas();
    printerGeoCanvas.DrawingArea = printDocument.DefaultPageSettings.Bounds;
    printerGeoCanvas.BeginDrawing(printDocument, pagePrinterLayer.GetBoundingBox(), GeographyUnit.Meter);

    Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
    foreach (PrinterLayer printerLayer in selectPageOverlay.PrinterLayers)
    {
        printerLayer.IsDrawing = true;
        if (!(printerLayer is PagePrinterLayer))
            printerLayer.Draw(printerGeoCanvas, labelsInAllLayers);
        printerLayer.IsDrawing = false;
    }
    printerGeoCanvas.Flush();

    PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
    printPreviewDialog.Document = printDocument;
    DialogResult dialogResult = printPreviewDialog.ShowDialog();
}
```
### Getting Help

[Map Suite Desktop for Wpf Wiki Resources](http://wiki.thinkgeo.com/wiki/map_suite_desktop_for_wpf)

[Map Suite Desktop for Wpf Product Description](https://thinkgeo.com/ui-controls#desktop-platforms)

[ThinkGeo Community Site](http://community.thinkgeo.com/)

[ThinkGeo Web Site](http://www.thinkgeo.com)

### Key APIs
This example makes use of the following APIs:

- [ThinkGeo.MapSuite.Layers.PrinterZoomLevelSet](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerzoomlevelset)
- [ThinkGeo.MapSuite.Layers.PrinterHelper](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerhelper)
- [ThinkGeo.MapSuite.Wpf.PrinterInteractiveOverlay](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.wpf.printerinteractiveoverlay)
- [ThinkGeo.MapSuite.Layers.PagePrinterLayer](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.pageprinterlayer)
- [ThinkGeo.MapSuite.Layers.PrinterPageSize](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerpagesize)
- [ThinkGeo.MapSuite.Layers.PrinterOrientation](http://wiki.thinkgeo.com/wiki/api/thinkgeo.mapsuite.layers.printerorientation)

### About Map Suite
Map Suite is a set of powerful development components and services for the .Net Framework.

### About ThinkGeo
ThinkGeo is a GIS (Geographic Information Systems) company founded in 2004 and located in Frisco, TX. Our clients are in more than 40 industries including agriculture, energy, transportation, government, engineering, software development, and defense.
