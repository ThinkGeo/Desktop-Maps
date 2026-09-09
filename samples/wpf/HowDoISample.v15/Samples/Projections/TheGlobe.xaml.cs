using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The map as a ball.
    /// </summary>
    public partial class TheGlobe : UserControl, IDisposable
    {
        private const double WorldHalfSize = 20037508.342789244;

        private static readonly TimeSpan MorphDuration = TimeSpan.FromMilliseconds(900);

        private static readonly Dictionary<string, string> Choices = new Dictionary<string, string>
        {
            ["RdoGlobe"] = "globe",
            ["RdoMercator"] = "3857",
            ["RdoRobinson"] = "+proj=robin +datum=WGS84 +units=m +no_defs",
        };

        private ThinkGeoRasterTileSource _source;
        private string _choice = "globe";
        private bool _ready;

        public TheGlobe()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready)
            {
                return;
            }

            Map.MapUnit = GeographyUnit.Meter;
            Map.CurrentExtentChanged += (_, _) => ShowStatus();

            try
            {
                _source = new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Aerial, scaleFactor: 2);

                Map.Basemap = new GpuBasemap(new MapStyle());
                Map.Basemap.AddRasterSource("thinkgeo", _source);
                Map.Basemap.DisplayProjection = ThinkGeo.Gpu.DisplayProjection.Globe;

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
            if (!_ready || sender is not RadioButton button || !Choices.TryGetValue(button.Name, out var definition))
            {
                return;
            }

            if (definition == _choice)
            {
                return;
            }

            _choice = definition;

            var target = definition switch
            {
                "globe" => ThinkGeo.Gpu.DisplayProjection.Globe,
                "3857" => ThinkGeo.Gpu.DisplayProjection.WebMercator,
                _ => ThinkGeo.Gpu.DisplayProjection.FromProjString(definition),
            };

            // The camera does not move: the screen center keeps the same ground
            // point and only the shape under it changes.
            var before = Map.CurrentExtent;
            Map.MaximumScale = definition == "3857" ? double.MaxValue : WorldFillScale();
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

        /// <summary>Zoom-out stops where the world fills the window: a projected canvas is not cyclic.</summary>
        private double WorldFillScale()
        {
            var world = new RectangleShape(-WorldHalfSize, WorldHalfSize, WorldHalfSize, -WorldHalfSize);
            return MapUtil.GetScale(world, Math.Max(1, Map.ActualWidth), GeographyUnit.Meter) * 1.05;
        }

        private async void World_Click(object sender, RoutedEventArgs e) => await FrameAsync(2.0 * WorldHalfSize);

        /// <summary>Into the handover band, where the ball is already half flat.</summary>
        private async void Band_Click(object sender, RoutedEventArgs e) => await FrameAsync(1500000);

        /// <summary>Past it, where the globe has switched itself off entirely.</summary>
        private async void Street_Click(object sender, RoutedEventArgs e) => await FrameAsync(400000);

        private async Task FrameAsync(double widthInMeters)
        {
            if (!_ready)
            {
                return;
            }

            var height = widthInMeters * Map.ActualHeight / Math.Max(1, Map.ActualWidth);
            Map.CurrentExtent = new RectangleShape(
                -widthInMeters * 0.5, height * 0.5, widthInMeters * 0.5, -height * 0.5);
            await Map.RefreshAsync();
            ShowStatus();
        }

        /// <summary>
        /// What the map is showing right now, in the terms this sample is about.
        /// </summary>
        private void ShowStatus()
        {
            if (StatusText == null || !_ready)
            {
                return;
            }

            var extent = Map.CurrentExtent;
            var basemap = Map.Basemap;
            var factor = basemap != null && extent != null ? basemap.GlobeHandoverFactorAt(extent) : 0;
            var zoom = extent == null || extent.Width <= 0
                ? 0
                : Math.Log2(2.0 * WorldHalfSize / extent.Width) + Math.Log2(Math.Max(1, Map.ActualWidth) / 512.0);

            var shape = _choice != "globe"
                ? "not a globe"
                : factor <= 0.001
                    ? "a ball"
                    : factor >= 0.999
                        ? "flat mercator - the globe has handed over"
                        : string.Create(CultureInfo.InvariantCulture, $"{1 - factor:P0} ball, {factor:P0} flat");

            StatusText.Text = string.Create(CultureInfo.InvariantCulture,
                $"projection  {_choice}\nzoom        {zoom:F2}\nscale       1:{Map.CurrentScale:N0}\nshowing     {shape}");
        }

        public void Dispose()
        {
            _source = null;
        }
    }
}
