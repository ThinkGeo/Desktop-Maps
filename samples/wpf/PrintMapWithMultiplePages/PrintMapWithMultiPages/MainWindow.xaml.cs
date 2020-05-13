using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace PrintMapWithMultiPages
{
    public partial class MainWindow : Window
    {
        private readonly string cloudId = "USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~";
        private readonly string cloudSecret = "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~";
        private readonly RectangleShape thinkGeoMapsExtent = new RectangleShape(-20037508.2314698, 20037508.2314698, 20037508.2314698, -20037508.2314698);
        private PrinterInteractiveOverlay selectPageOverlay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new PrinterZoomLevelSet(mapView.MapUnit, PrinterHelper.GetPointsPerGeographyUnit(mapView.MapUnit));
            mapView.MinimumScale = mapView.ZoomLevelSet.ZoomLevel20.Scale;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightGray);

            var mapPageOverlay = CreateMapPage();
            mapPageOverlay.Name = "mapPage";
            mapView.InteractiveOverlays.Add("MapPageOverlay", mapPageOverlay);
            mapView.InteractiveOverlays.MoveToBottom("MapPageOverlay");
            selectPageOverlay = mapPageOverlay;

            var legendPageOverlay = CreateLegendPage();
            legendPageOverlay.Name = "legendPage";
            legendPageOverlay.IsVisible = false;
            mapView.InteractiveOverlays.Add("LegendPageOverlay", legendPageOverlay);
            mapView.InteractiveOverlays.MoveToBottom("LegendPageOverlay");

            mapView.CurrentExtent = RectangleShape.ScaleUp(new RectangleShape(-408, 528, 408, -528), 10).GetBoundingBox();
            mapView.Refresh();
        }

        private PrinterInteractiveOverlay CreateMapPage()
        {
            PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();
            PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
            pagePrinterLayer.Open();
            printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);


            MapPrinterLayer mapLayer = new MapPrinterLayer();
            mapLayer.MapUnit = GeographyUnit.Meter;
            mapLayer.MapExtent = thinkGeoMapsExtent;
            mapLayer.BackgroundMask = new AreaStyle(new GeoPen(GeoColor.StandardColors.Black, 1));
            mapLayer.Open();

            RectangleShape mapBBox = pagePrinterLayer.GetPosition(PrintingUnit.Inch);
            mapLayer.SetPosition(8, 7, mapBBox.GetCenterPoint().X, mapBBox.GetCenterPoint().Y + 1.5, PrintingUnit.Inch);

            ZoomLevelSet zoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapLayer.MapExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel04.Scale, thinkGeoMapsExtent, mapLayer.MapUnit, (float)mapLayer.GetBoundingBox().Width, (float)mapLayer.GetBoundingBox().Height);
            mapLayer.Layers.Add(new ThinkGeoCloudRasterMapsLayer(cloudId, cloudSecret));
            printerOverlay.PrinterLayers.Add(mapLayer);

            MapPrinterLayer miniMapLayer = new MapPrinterLayer();
            miniMapLayer.MapUnit = GeographyUnit.Meter;
            miniMapLayer.MapExtent = thinkGeoMapsExtent;
            miniMapLayer.BackgroundMask = new AreaStyle(new GeoPen(GeoColor.StandardColors.Black, 1));
            miniMapLayer.Open();

            RectangleShape miniMapBBox = miniMapLayer.GetPosition(PrintingUnit.Inch);
            miniMapLayer.SetPosition(4, 3, miniMapBBox.GetCenterPoint().X + 2, miniMapBBox.GetCenterPoint().Y - 3.7, PrintingUnit.Inch);
            miniMapLayer.MapExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel02.Scale, thinkGeoMapsExtent, miniMapLayer.MapUnit, (float)miniMapLayer.GetBoundingBox().Width, (float)miniMapLayer.GetBoundingBox().Height);
            miniMapLayer.Layers.Add(new ThinkGeoCloudRasterMapsLayer(cloudId, cloudSecret));
            printerOverlay.PrinterLayers.Add(miniMapLayer);

            return printerOverlay;
        }

        private PrinterInteractiveOverlay CreateLegendPage()
        {
            PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();
            PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
            pagePrinterLayer.Open();
            printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);

            LegendItem title = new LegendItem();
            title.TextStyle = new TextStyle("Map Legend", new GeoFont("Arial", 20, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendItem legendItem1 = new LegendItem();
            legendItem1.ImageStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(170, GeoColor.StandardColors.Green)));
            legendItem1.TextStyle = new TextStyle("Population > 70 million", new GeoFont("Arial", 16), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendItem legendItem2 = new LegendItem();
            legendItem2.ImageStyle = AreaStyles.Country1;
            legendItem2.TextStyle = new TextStyle("Population < 70 million", new GeoFont("Arial", 16), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Height = 400;
            legendAdornmentLayer.Title = title;
            legendAdornmentLayer.LegendItems.Add(legendItem1);
            legendAdornmentLayer.LegendItems.Add(legendItem2);

            LegendPrinterLayer legendPrinterLayer = new LegendPrinterLayer(legendAdornmentLayer);
            var centerPoint = pagePrinterLayer.GetPosition(PrintingUnit.Point).GetCenterPoint();
            legendPrinterLayer.SetPosition(6, 8, centerPoint.X - 1, centerPoint.Y, PrintingUnit.Inch);
            legendPrinterLayer.BackgroundMask = AreaStyles.CreateSimpleAreaStyle(new GeoColor(255, 230, 230, 230), GeoColor.SimpleColors.Black, 1);
            printerOverlay.PrinterLayers.Add(legendPrinterLayer);

            return printerOverlay;
        }

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

        private void SelectPage(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.RadioButton selectPageRadioButton = (System.Windows.Controls.RadioButton)sender;

            foreach (var overlay in mapView.InteractiveOverlays)
            {
                if (overlay is PrinterInteractiveOverlay)
                {
                    if (overlay.Name.Equals(selectPageRadioButton.Name))
                    {
                        overlay.IsVisible = true;
                        selectPageOverlay = (PrinterInteractiveOverlay)overlay;
                    }
                    else
                        overlay.IsVisible = false;
                }
            }

            mapView.Refresh();
        }

        private PaperSize GetPrintPreviewPaperSize(PagePrinterLayer pagePrinterLayer)
        {
            PaperSize printPreviewPaperSize = new PaperSize("AnsiA", 850, 1100);
            switch (pagePrinterLayer.PageSize)
            {
                case PrinterPageSize.AnsiA:
                    printPreviewPaperSize = new PaperSize("AnsiA", 850, 1100);
                    break;
                case PrinterPageSize.AnsiB:
                    printPreviewPaperSize = new PaperSize("AnsiB", 1100, 1700);
                    break;
                case PrinterPageSize.AnsiC:
                    printPreviewPaperSize = new PaperSize("AnsiC", 1700, 2200);
                    break;
                case PrinterPageSize.AnsiD:
                    printPreviewPaperSize = new PaperSize("AnsiD", 2200, 3400);
                    break;
                case PrinterPageSize.AnsiE:
                    printPreviewPaperSize = new PaperSize("AnsiE", 3400, 4400);
                    break;
                case PrinterPageSize.Custom:
                    printPreviewPaperSize = new PaperSize("Custom Size", (int)pagePrinterLayer.CustomWidth, (int)pagePrinterLayer.CustomHeight);
                    break;
                default:
                    break;
            }

            return printPreviewPaperSize;
        }
    }
}
