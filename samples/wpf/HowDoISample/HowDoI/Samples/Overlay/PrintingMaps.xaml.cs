using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for PrintingMaps.xaml
    /// </summary>
    [Serializable]
    public partial class PrintingMaps : UserControl
    {
        public PrintingMaps()
        {
            InitializeComponent();
        }

        public void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Setup the map unit, you want to always use feet or meters for the printer layout map.
            mapView.MapUnit = GeographyUnit.Meter;

            // Here we create the default zoom levels for the sample.  We have pre-created a PrinterZoomLevelSet
            // That pre-defines commonly used zoom levels based on percentages of zoom
            mapView.ZoomLevelSet = new PrinterZoomLevelSet(mapView.MapUnit, PrinterHelper.GetPointsPerGeographyUnit(mapView.MapUnit));

            // Here we set the background color to gray so there is contrast with the white page
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.LightGray);

            // Create the PrinterInteractiveOverlay to contain all of the PrinterLayers.
            // The interactive overlay allows the layers to be interacted with
            PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();

            mapView.InteractiveOverlays.Add("PrintPreviewOverlay", printerOverlay);
            mapView.InteractiveOverlays.MoveToBottom("PrintPreviewOverlay");

            // Create the PagePrinterLayer which shows the page boundary and is the area the user will
            // arrange all of the other layer on top of.
            PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
            pagePrinterLayer.Open();
            printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);

            // Get the pages extent, slightly scale it up, and then set that as the default current extent
            mapView.CurrentExtent = RectangleShape.ScaleUp(pagePrinterLayer.GetPosition(), 10).GetBoundingBox();

            // Set the minimum sscale of the map to the last zoom level
            mapView.MinimumScale = mapView.ZoomLevelSet.ZoomLevel20.Scale;

        }
    }
}
