using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Satellite imagery in any projection.
    /// </summary>
    public partial class AnyProjectionRaster : UserControl, IDisposable
    {
        private static readonly Dictionary<string, string> Choices = new Dictionary<string, string>
        {
            ["RdoMercator"] = "3857",
            ["RdoEquirectangular"] = "4326",
            ["RdoRobinson"] = "+proj=robin +datum=WGS84 +units=m +no_defs",
            ["RdoMollweide"] = "+proj=moll +datum=WGS84 +units=m +no_defs",
            ["RdoEckert"] = "+proj=eck4 +datum=WGS84 +units=m +no_defs",
            ["RdoCylindrical"] = "+proj=cea +datum=WGS84 +units=m +no_defs",
        };

        private const double WorldHalfSize = 20037508.342789244;

        /// <summary>The same 900ms the vector projection samples use, so a change of
        /// projection reads as one gesture across the gallery.</summary>
        private static readonly TimeSpan MorphDuration = TimeSpan.FromMilliseconds(900);

        private ThinkGeoRasterTileSource _source;
        private string _choice = "3857";
        private bool _ready;

        public AnyProjectionRaster()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_source != null)
                return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#DCE8EF"));
            Map.CurrentExtentChanged += (_, _) => ShowStatus();

            try
            {
                // Satellite imagery on purpose: a photograph has no vector twin to
                // fall back on, so what you are looking at can only be the warped
                // picture - and coastlines in a photo make the bend of a projection
                // easy to read.
                // The gallery's shared source: same credentials, one call.
                _source = new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Aerial, scaleFactor: 2);

                // One call puts the raster service on the GPU map. No style JSON, and
                // no reprojection setup: the projection is a property of the DISPLAY,
                // set below, not of the data.
                Map.Basemap = new GpuBasemap(new MapStyle());
                Map.Basemap.AddRasterSource("thinkgeo", _source);

                Map.CurrentExtent = new RectangleShape(-WorldHalfSize, WorldHalfSize, WorldHalfSize, -WorldHalfSize);
                await Map.RefreshAsync();
                _ready = true;
                ShowStatus();
            }
            catch (Exception ex)
            {
                StatusText.Text = "could not open the tile source:" + Environment.NewLine + ex.Message;
            }
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            // No busy guard. A change asked for while another is still walking used
            // to be swallowed here - the radio moved, the map did not, and the two
            // disagreed until you clicked again. The renderer cancels a walk in
            // flight and the next one resumes from wherever it stopped, so the
            // right answer to a second click is to honor it.
            if (!_ready || sender is not RadioButton button || !Choices.TryGetValue(button.Name, out var definition))
                return;

            if (definition == _choice)
                return;

            _choice = definition;
            try
            {
                // EPSG codes are not proj strings: the two the renderer knows by
                // number are named, the rest of the list is +proj=.
                var target = definition switch
                {
                    "3857" => ThinkGeo.Gpu.DisplayProjection.WebMercator,
                    "4326" => ThinkGeo.Gpu.DisplayProjection.FromName("equirectangular"),
                    _ => ThinkGeo.Gpu.DisplayProjection.FromProjString(definition),
                };

                // Zoom-out stops where the world fills the window: a projected canvas
                // is not cyclic, so past one world the renderer would tile a second
                // copy sideways. Mercator keeps its usual freedom.
                var before = Map.CurrentExtent;
                if (definition == "3857")
                {
                    Map.MaximumScale = double.MaxValue;
                }
                else
                {
                    var world = new RectangleShape(-WorldHalfSize, WorldHalfSize, WorldHalfSize, -WorldHalfSize);
                    Map.MaximumScale = MapUtil.GetScale(world, Math.Max(1, Map.ActualWidth), GeographyUnit.Meter) * 1.05;
                    if (Map.CurrentScale > Map.MaximumScale)
                        Map.CurrentScale = Map.MaximumScale;
                }

                // The camera is left exactly where it was. Under this renderer the
                // screen center always shows Map.CenterPoint whatever the display
                // projection is, so leaving it alone IS 'keep looking at the same
                // ground' rather than an approximation of it.
                Map.CurrentExtent = before;

                if (ChkMorph?.IsChecked == true)
                {
                    await Map.RefreshAsync();
                    await Map.AnimateDisplayProjectionAsync(target, MorphDuration);
                }
                else
                {
                    Map.DisplayProjection = target;
                    await Map.RefreshAsync();
                }

                ShowStatus();
            }
            catch (NotSupportedException ex)
            {
                StatusText.Text = "not a natively projected family:" + Environment.NewLine + ex.Message;
            }
            finally
            {
                ShowStatus();
            }
        }

        private void ShowStatus()
        {
            if (StatusText == null)
                return;

            var extent = Map.CurrentExtent;
            StatusText.Text = string.Create(CultureInfo.InvariantCulture,
                $"projection  {_choice}\nscale       1:{Map.CurrentScale:N0}\ncentre      {(extent.MinX + extent.MaxX) * 0.5:N0}, {(extent.MinY + extent.MaxY) * 0.5:N0}");
        }

        public void Dispose()
        {
            _source = null;
        }

    }
}
