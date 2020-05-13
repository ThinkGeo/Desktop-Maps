using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace RasterLayerPrintQuality
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MapPrinterLayer mapPrinterLayer;
        private PrinterInteractiveOverlay printerOverlay;
        private PagePrinterLayer pageLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

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
            mapPrinterLayer.SetPosition(pageLayer.GetPosition());
            mapPrinterLayer.MapUnit = GeographyUnit.Meter;
            mapPrinterLayer.PreviewDrawingMode = MapPrinterPreviewDrawingMode.Redraw;
            mapPrinterLayer.Open();

            RasterLayer rasterLayer = new NativeImageRasterLayer(@"..\..\MapData\poland_jnct_contour.gif", new RectangleShape(0, 1, 1, 0));
            rasterLayer.Name = "RasterLayer";
            rasterLayer.ScaleFactor = 1;
            rasterLayer.Open();

            mapPrinterLayer.Layers.Add(rasterLayer);
            mapPrinterLayer.MapExtent = rasterLayer.GetBoundingBox();

            printerOverlay.PrinterLayers.Add(pageLayer);
            printerOverlay.PrinterLayers.Add(mapPrinterLayer);

            wpfMap.InteractiveOverlays.Add(printerOverlay);
            wpfMap.MapUnit = GeographyUnit.Meter;
            wpfMap.CurrentExtent = pageLayer.GetBoundingBox();
            wpfMap.Refresh();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrinterGeoCanvas printerCanvas = new PrinterGeoCanvas();
            printerCanvas.Dpi = float.Parse(txtDpi.Text);

            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();

            PrintDocument printDocument = new PrintDocument();

            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.DefaultPageSettings.PaperSize = new PaperSize("AnsiA", 850, 1100);
            printDocument.DefaultPageSettings.PrinterSettings.PrinterName = cmbPrinterName.SelectedItem as string;

            printerCanvas.DrawingArea = printDocument.DefaultPageSettings.Bounds;
            printerCanvas.BeginDrawing(printDocument, pageLayer.GetBoundingBox(), wpfMap.MapUnit);

            foreach (PrinterLayer printerLayer in printerOverlay.PrinterLayers)
            {
                printerLayer.IsDrawing = true;
                if (!(printerLayer is PagePrinterLayer))
                {
                    printerLayer.Transparency = 256;
                    printerLayer.Draw(printerCanvas, labelsInAllLayers);
                }
                printerLayer.IsDrawing = false;
                printerCanvas.Flush();
            }

            printerCanvas.EndDrawing();
        }
    }
}
