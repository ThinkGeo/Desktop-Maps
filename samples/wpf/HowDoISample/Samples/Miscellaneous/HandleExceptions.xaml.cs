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
        private bool _initialized;

        public HandleExceptions()
        {
            InitializeComponent();
        }

        private CustomWmsAsyncLayer _wmsAsync;
        private LayerOverlay _overlay;

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

            var a = MapUtil.GetScaleFromResolution(0.5, GeographyUnit.Meter);

            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we will add the layer to.
            _overlay = new LayerOverlay();
            _overlay.TileCache = new FileRasterTileCache(@".\cache", "HandleExceptionSample"); // Tiles with exceptions will not be cached.
            _overlay.ThrowingException += (_, arg) =>
            {
                TxtException.Text = arg.Exception?.Message;
                arg.Handled = true;
            };
            MapView.Overlays.Add(_overlay);

            //_wms = new CustomWmsLayer(new Uri("http://not_exist.com/services/service"));
            _wmsAsync = new CustomWmsAsyncLayer(new Uri("http://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"));
            _wmsAsync.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");
            _wmsAsync.Parameters.Add("STYLES", "generic");
            _wmsAsync.OutputFormat = "image/png";
            _wmsAsync.Crs = "EPSG:3857";  // Coordinate system, typically EPSG:3857 for WMS with Spherical 
            _wmsAsync.TimeoutInSeconds = 1; // 1s basically for sure will Timeout 
            _overlay.Layers.Add("wmsImageLayer", _wmsAsync);

            _wmsAsync.DrawCustomException = false;
            _overlay.ThrowingExceptionMode = ThrowingExceptionMode.SuppressException;

            MapView.CenterPoint = new PointShape(234655, 1247759);
            MapView.CurrentScale = 295830000;

            _initialized = true;
            _ = MapView.RefreshAsync();
        }

        private void DrawingExceptionMode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            var button = (RadioButton)sender;
            if (button.Content == null) return;
            TxtException.Text = "";

            switch (button.Content.ToString())
            {
                case "Draw Custom Exception":
                    _wmsAsync.DrawCustomException = true;
                    _overlay.ThrowingExceptionMode = ThrowingExceptionMode.SuppressException;
                    break;

                case "Throw Exception":
                    _overlay.ThrowingExceptionMode = ThrowingExceptionMode.ThrowException;
                    break;

                default:
                    _wmsAsync.DrawCustomException = false;
                    _overlay.ThrowingExceptionMode = ThrowingExceptionMode.SuppressException;
                    break;
            }
            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public class CustomWmsAsyncLayer : Core.WmsAsyncLayer
    {
        public bool DrawCustomException { get; set; }
        public CustomWmsAsyncLayer(Uri uri)
            : base(uri)
        { }

        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (!DrawCustomException)
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