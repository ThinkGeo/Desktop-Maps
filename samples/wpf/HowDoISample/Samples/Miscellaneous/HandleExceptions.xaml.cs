using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class HandleExceptions : IDisposable
    {
        public HandleExceptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.DecimalDegree;

                // This code sets up the sample to use the overlay versus the layer.
                UseLayer(DrawingExceptionMode.DrawException, false);

                // Set the current extent to a local area.
                MapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

                // Refresh the map.
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void DrawingExceptionMode_Checked(object sender, RoutedEventArgs e)
        {
            try
            { 
                var button = (RadioButton)sender;
                if (button.Content == null) return;
                TxtException.Text = "";
                switch (button.Content.ToString())
                {
                    case "Throw Exception":
                        UseLayer(DrawingExceptionMode.ThrowException, false);
                        break;
                    case "Customize Drawing Exception":
                        UseLayer(DrawingExceptionMode.DrawException, true);
                        break;
                    case "Draw Exception":
                    default:
                        UseLayer(DrawingExceptionMode.DrawException, false);
                        break;
                }
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void UseLayer(DrawingExceptionMode drawingExceptionMode, bool drawCustomException)
        {
            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay
            {
                DrawingExceptionMode = drawingExceptionMode
            };
            if (drawingExceptionMode == DrawingExceptionMode.ThrowException)
            {
                staticOverlay.ThrowingException += (sender, e) =>
                {
                    TxtException.Text = e.Exception?.InnerException.Message;
                    e.Handled = true;
                };
            }

            MapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            var wmsImageLayer = new CustomWmsLayer(new Uri("http://not_exist.com/services/service"), drawCustomException)
                {
                    DrawingExceptionMode = drawingExceptionMode
                };

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public class CustomWmsLayer : Core.WmsAsyncLayer
    {
        private readonly bool _drawCustomException;
        public CustomWmsLayer(Uri uri, bool drawCustomException)
            : base(uri)
        {
            this._drawCustomException = drawCustomException;
        }

        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (!_drawCustomException)
            {
                base.DrawExceptionCore(canvas, e);
            }
            else
            {
                // customize the drawing exception. Here below we draw the error in red on orange canvas.
                canvas.DrawArea(canvas.CurrentWorldExtent, GeoBrushes.LightOrange, DrawingLevel.LevelOne);
                canvas.DrawText("Customized Exception Message", new GeoFont("Arial", 10), GeoBrushes.Red, new[] { new ScreenPointF(canvas.Width / 2, canvas.Height / 2) }, DrawingLevel.LabelLevel);
            }
        }
    }
}