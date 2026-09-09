using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The same shapefile drawn twice, with the projection in a different place.
    /// </summary>
    public partial class CpuVsGpuProjection : UserControl, IDisposable
    {
        private const string CountriesFile = "Countries02.shp";
        private const int SourceSrid = 4326;
        private const double WorldHalfSize = 20037508.342789244;

        /// <summary>What each radio asks both sides for.</summary>
        private static readonly System.Collections.Generic.Dictionary<string, string> Choices =
            new System.Collections.Generic.Dictionary<string, string>
            {
                ["RdoRobinson"] = "+proj=robin +datum=WGS84 +units=m +no_defs",
                ["RdoMollweide"] = "+proj=moll +datum=WGS84 +units=m +no_defs",
                ["RdoEckert"] = "+proj=eck4 +datum=WGS84 +units=m +no_defs",
                ["RdoCylindrical"] = "+proj=cea +datum=WGS84 +units=m +no_defs",
                ["RdoEquirectangular"] = "+proj=eqc +datum=WGS84 +units=m +no_defs",
                ["RdoMercator"] = "+proj=merc +datum=WGS84 +units=m +no_defs",
            };

        private readonly DispatcherTimer _applyTimer;
        private ShapeFileFeatureLayer _cpuLayer;
        private LayerOverlay _cpuOverlay;
        private int _featureCount;
        private bool _ready;

        public CpuVsGpuProjection()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready)
            {
                return;
            }

            CpuMap.MapUnit = GeographyUnit.Meter;
            GpuMap.MapUnit = GeographyUnit.Meter;

            // How many features the CPU side has to push through proj4 every time
            // it is asked for the view - the number the readout reports.
            var counter = new ShapeFileFeatureSource(SampleShared.Shapefile(CountriesFile));
            counter.Open();
            _featureCount = counter.GetAllFeatures(ReturningColumnsType.NoColumns).Count;
            counter.Close();

            // ---- left: projected on the way in ----
            _cpuLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile(CountriesFile));
            _cpuLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                new AreaStyle(new GeoPen(GeoColor.FromHtml("#7A7568"), 1), new GeoSolidBrush(GeoColor.FromHtml("#D8D3C6")));
            _cpuLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _cpuOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _cpuOverlay.Layers.Add(_cpuLayer);
            CpuMap.Overlays.Add(_cpuOverlay);
            CpuMap.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#EAE8E2"));

            // ---- right: projected on the way out ----
            // The one CPU transform this side does: 4326 to web mercator, once, so
            // the tiles are in the coordinates the renderer speaks. Everything the
            // projection radios ask for after that is done by the shader.
            var tiles = new FeatureSourceVectorTileSource();
            tiles.FeatureSources.Add("countries", new ShapeFileFeatureSource(SampleShared.Shapefile(CountriesFile))
            {
                ProjectionConverter = new ProjectionConverter(SourceSrid, 3857),
            });

            var basemap = new GpuBasemap(new MapStyle().AddStyle(Editor.Text));
            basemap.TileSources.Add("features", tiles);
            GpuMap.Basemap = basemap;

            _ready = true;
            await ApplyAsync(Choices["RdoRobinson"]);
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready || sender is not RadioButton button || !Choices.TryGetValue(button.Name, out var projString))
            {
                return;
            }

            await ApplyAsync(projString);
        }

        /// <summary>Asks both sides for the same projection, and times what each one does about it.</summary>
        private async Task ApplyAsync(string projString)
        {
            // ---- the CPU side: a new converter, and every feature read again ----
            var cpuClock = Stopwatch.StartNew();
            _cpuLayer.Close();
            _cpuLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(SourceSrid, projString);
            _cpuLayer.Open();
            var projectedExtent = _cpuLayer.GetBoundingBox();
            _cpuLayer.Close();
            CpuMap.CurrentExtent = projectedExtent;
            await CpuMap.RefreshAsync();
            cpuClock.Stop();

            CpuReadout.Text = string.Create(CultureInfo.InvariantCulture,
                $"switch {cpuClock.ElapsedMilliseconds,4} ms   features through proj4: {_featureCount}   data re-read: yes");

            // ---- the GPU side: a different shader, and not one byte of data ----
            var gpuClock = Stopwatch.StartNew();
            var failed = string.Empty;
            try
            {
                var projection = ThinkGeo.Gpu.DisplayProjection.FromProjString(projString);
                GpuMap.DisplayProjection = projection;
                GpuMap.CurrentExtent = ProjectedWorldBounds(projection);
                await GpuMap.RefreshAsync();
            }
            catch (NotSupportedException ex)
            {
                failed = ex.Message;
            }

            gpuClock.Stop();
            GpuReadout.Text = failed.Length > 0
                ? "not a family this side implements - see the message on the right"
                : string.Create(CultureInfo.InvariantCulture,
                    $"switch {gpuClock.ElapsedMilliseconds,4} ms   features through proj4: 0     data re-read: no");

            if (failed.Length > 0)
            {
                DatumReadout.Text = failed;
            }
        }

        /// <summary>
        /// The world's outline after this projection, so the GPU side frames what
        /// it draws the way the CPU side frames what it read.
        /// </summary>
        private static RectangleShape ProjectedWorldBounds(ThinkGeo.Gpu.DisplayProjection projection)
        {
            var minX = double.MaxValue;
            var maxX = double.MinValue;
            var minY = double.MaxValue;
            var maxY = double.MinValue;

            void Sample(double x, double y)
            {
                var display = projection.ToDisplay(new PointShape(x, y));
                if (double.IsNaN(display.X) || double.IsNaN(display.Y)) return;
                if (display.X < minX) minX = display.X;
                if (display.X > maxX) maxX = display.X;
                if (display.Y < minY) minY = display.Y;
                if (display.Y > maxY) maxY = display.Y;
            }

            const int steps = 64;
            var edge = WorldHalfSize * 0.9999;
            for (var i = 0; i <= steps; i++)
            {
                var t = -edge + (2.0 * edge * i / steps);
                Sample(t, edge);
                Sample(t, -edge);
                Sample(-edge, t);
                Sample(edge, t);
            }

            if (minX > maxX || minY > maxY)
            {
                return new RectangleShape(-WorldHalfSize, WorldHalfSize, WorldHalfSize, -WorldHalfSize);
            }

            var marginX = (maxX - minX) * 0.03;
            var marginY = (maxY - minY) * 0.03;
            return new RectangleShape(minX - marginX, maxY + marginY, maxX + marginX, minY - marginY);
        }

        /// <summary>
        /// The same historic datum asked of both sides. The point is not that one
        /// throws - it is WHY: the CPU side owns the data, so it can shift it; the
        /// GPU side only owns the drawing, and a shader has nowhere to put a datum.
        /// </summary>
        private void Datum_Click(object sender, RoutedEventArgs e)
        {
            const string historic = "+proj=lcc +lat_1=33 +lat_2=45 +lon_0=-96 +datum=NAD27 +units=m +no_defs";

            string cpu;
            try
            {
                var converter = new ProjectionConverter(SourceSrid, historic);
                converter.Open();
                var moved = (PointShape)converter.ConvertToExternalProjection(new PointShape(-96, 39));
                converter.Close();
                cpu = string.Create(CultureInfo.InvariantCulture,
                    $"CPU: fine - (-96, 39) lands at ({moved.X:N0}, {moved.Y:N0}) on NAD27");
            }
            catch (Exception ex)
            {
                cpu = "CPU: " + ex.Message;
            }

            string gpu;
            try
            {
                ThinkGeo.Gpu.DisplayProjection.FromProjString(historic);
                gpu = "GPU: accepted (which it should not be - the datum would be ignored)";
            }
            catch (NotSupportedException ex)
            {
                gpu = "GPU: refused - " + ex.Message;
            }

            DatumReadout.Text = cpu + Environment.NewLine + Environment.NewLine + gpu;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ready) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await GpuMap.Basemap.SetStyleAsync(new MapStyle(Editor.Text));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        public void Dispose()
        {
            _cpuLayer?.Close();
        }
    }
}
