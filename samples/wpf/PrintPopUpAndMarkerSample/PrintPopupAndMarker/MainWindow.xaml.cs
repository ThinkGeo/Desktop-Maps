using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace PrintPopupAndMarker
{
    public partial class MainWindow : Window
    {
        private PopupOverlay popupOverlay;
        private SimpleMarkerOverlay markerOverlay;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.CurrentExtent = new RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);

            LayerOverlay layerOverlay = new LayerOverlay();
            OpenStreetMapLayer osmLayer = new OpenStreetMapLayer();
            layerOverlay.Layers.Add(osmLayer);
            map.Overlays.Add(layerOverlay);

            markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.Markers.Add(new Marker(new PointShape(-12245143.9872601, 4579425.8128701)));
            map.Overlays.Add(markerOverlay);

            popupOverlay = new PopupOverlay();
            PointShape popupPosition = new PointShape(-10575351.625361, 4579425.8128701);
            Popup popup = new Popup(popupPosition);
            popup.Width = 100;
            popup.Height = 40;
            popup.Content = "Mound City";
            popupOverlay.Popups.Add(popup);
            map.Overlays.Add(popupOverlay);

            map.Refresh();
        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {
            var printDocument = new PrintDocument
            {
                DefaultPageSettings =
                {
                    Landscape =true,
                    PaperSize = new PaperSize("AnsiA", 768, 1024)
                }
            };

            Collection<Layer> layers = new Collection<Layer>();
            RectangleShape boundingBox = new RectangleShape(0, map.ActualHeight, map.ActualWidth, 0);
            PointShape centerPoint = boundingBox.GetCenterPoint();
            var popupLayer = CreatePrinterLayerByOverlay(popupOverlay, centerPoint);
            layers.Add(popupLayer);
            var markerLayer = CreatePrinterLayerByOverlay(markerOverlay, centerPoint);
            layers.Add(markerLayer);

            MapPrinterLayer mapPrinterLayer = new MapPrinterLayer();
            mapPrinterLayer.MapUnit = GeographyUnit.Meter;
            mapPrinterLayer.MapExtent = map.CurrentExtent;
            mapPrinterLayer.SetPosition(boundingBox.Width, boundingBox.Height, boundingBox.GetCenterPoint(), PrintingUnit.Point);
            OpenStreetMapLayer osmLayer = new OpenStreetMapLayer();
            mapPrinterLayer.Layers.Insert(0, osmLayer);
            layers.Add(mapPrinterLayer);

            var printerGeoCanvas = new PrinterGeoCanvas { DrawingArea = printDocument.DefaultPageSettings.Bounds, Dpi = 100 };
            printerGeoCanvas.BeginDrawing(printDocument, boundingBox, map.MapUnit);
            var labelsInAllLayers = new Collection<SimpleCandidate>();
            foreach (var layer in layers)
            {
                layer.Open();
                layer.Draw(printerGeoCanvas, labelsInAllLayers);
            }
            printerGeoCanvas.Flush();

            var printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog { Document = printDocument };
            printPreviewDialog.ShowDialog();
        }

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
    }
}
