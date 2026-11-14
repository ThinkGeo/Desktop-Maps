using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// A layer that fails: draw the exception on the map, draw it your own way, or throw it and catch it.
    /// </summary>
    public partial class HandleExceptions : IDisposable
    {
        private bool _initialized;
        private CustomWmsAsyncLayer _wmsLayer;
        private LayerOverlay _overlay;

        public HandleExceptions()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            _overlay = new LayerOverlay { DrawingExceptionMode = DrawingExceptionMode.DrawException };
            _overlay.ThrowingException += (_, args) =>
            {
                TxtException.Text = args.Exception?.Message;

                // Handled keeps the overlay from rethrowing the exception.
                args.Handled = true;
            };
            Map.Overlays.Add(_overlay);

            // The .invalid domain never resolves, so every map request fails. FastMode skips the
            // capabilities request, so the layer still opens and the failure comes when it draws.
            _wmsLayer = new CustomWmsAsyncLayer(new Uri("http://wms.invalid/ows")) { FastMode = true };
            _wmsLayer.Parameters.Add("LAYERS", "countries");
            _wmsLayer.OutputFormat = "image/png";
            _wmsLayer.Crs = "EPSG:3857";
            _overlay.Layers.Add(_wmsLayer);

            Map.CenterPoint = new PointShape(234655, 1247759);
            Map.CurrentScale = 295830000;

            _ = Map.RefreshAsync();
        }

        private async void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            TxtException.Text = string.Empty;
            _wmsLayer.DrawCustomException = DrawCustom.IsChecked == true;
            _overlay.DrawingExceptionMode = Throw.IsChecked == true ? DrawingExceptionMode.ThrowException : DrawingExceptionMode.DrawException;

            try
            {
                await _overlay.RefreshAsync();
            }
            catch (Exception ex)
            {
                TxtException.Text = ex.Message;
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// A WMS layer that can draw its failures its own way: an orange tile with a red message.
    /// </summary>
    public class CustomWmsAsyncLayer : WmsAsyncLayer
    {
        public CustomWmsAsyncLayer(Uri uri)
            : base(uri)
        { }

        public bool DrawCustomException { get; set; }

        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (!DrawCustomException)
            {
                base.DrawExceptionCore(canvas, e);
                return;
            }

            canvas.DrawArea(canvas.CurrentWorldExtent, GeoBrushes.LightOrange, DrawingLevel.LevelOne);
            canvas.DrawText("Customized Exception Message", new GeoFont("Noto Sans", 10), GeoBrushes.Red, new[] { new ScreenPointF(canvas.Width / 2, canvas.Height / 2) }, DrawingLevel.LabelLevel);
        }
    }
}
