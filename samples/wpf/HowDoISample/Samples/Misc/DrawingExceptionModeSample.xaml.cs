using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class DrawingExceptionModeSample : UserControl, IDisposable
    {
        public DrawingExceptionModeSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // This code sets up the sample to use the overlay versus the layer.
            UseLayer(DrawingExceptionMode.Default, false);
            
            // Set the current extent to a local area.
            mapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private async void DrawingExceptionMode_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (button.Content != null)
            {
                switch (button.Content.ToString())
                {
                    case "Default":
                    default:
                        UseLayer(DrawingExceptionMode.Default, false);
                        break;
                    case "Throw Exception":
                        UseLayer(DrawingExceptionMode.ThrowException, false);
                        break;
                    case "Draw Exception":
                        UseLayer(DrawingExceptionMode.DrawException, false);
                        break;
                    case "Draw Custom Exception":
                        UseLayer(DrawingExceptionMode.DrawException, true);
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private void UseLayer(DrawingExceptionMode drawingExceptionMode, bool drawCustomException)
        {
            // Clear out the overlays so we start fresh
            mapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.DrawingExceptionMode = drawingExceptionMode;
            if (drawingExceptionMode == DrawingExceptionMode.ThrowException)
            {
                staticOverlay.ThrowingException += (object sender, ThrowingExceptionOverlayEventArgs e) => {
                    txtException.Text = e.Exception.Message;
                };
            }

            mapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            CustomWmsRasterLayer wmsImageLayer = new CustomWmsRasterLayer(new Uri("http://ows.mundialis.de/services/service"), drawCustomException);
            wmsImageLayer.DrawingExceptionMode = drawingExceptionMode;
            wmsImageLayer.UpperThreshold = double.MaxValue;
            wmsImageLayer.LowerThreshold = 0;
            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");            
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);          
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public class CustomWmsRasterLayer: Core.Async.WmsRasterLayer
    {
        private bool drawCustomException;
        public CustomWmsRasterLayer(Uri uri, bool drawCustomException)
            :base(uri)
        {
            this.drawCustomException = drawCustomException;
        }

        protected override Task DrawAsyncCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            throw new Exception("mock exception");
            return base.DrawAsyncCore(canvas, labelsInAllLayers);
        }

        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (!drawCustomException)
            {
                base.DrawExceptionCore(canvas, e);
            }
            else
            {
                // customize the drawing exception. Here below we draw the the error in red on orange canvas.
                canvas.DrawArea(canvas.CurrentWorldExtent, GeoBrushes.LightOrange, DrawingLevel.LevelOne);
                canvas.DrawText("Something went wrong", new GeoFont("Arial", 10), GeoBrushes.Red, new ScreenPointF[] { new ScreenPointF(canvas.Width / 2, canvas.Height / 2) }, DrawingLevel.LabelLevel);
            }
        }
    }
}
