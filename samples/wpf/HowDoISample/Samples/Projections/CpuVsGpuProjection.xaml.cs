using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The same shapefile drawn twice, projected by the CPU as it is read on the left and by the GPU as it is drawn on the right.
    /// </summary>
    public partial class CpuVsGpuProjection : UserControl
    {
        private const string CountriesFile = "Countries02.shp";
        private const int SourceSrid = 4326;
        private const double WorldHalfSize = 20037508.342789244;

        /// <summary>What each radio asks both sides for.</summary>
        private static readonly Dictionary<string, string> Choices =
            new Dictionary<string, string>
            {
                ["RdoRobinson"] = "+proj=robin +datum=WGS84 +units=m +no_defs",
                ["RdoMollweide"] = "+proj=moll +datum=WGS84 +units=m +no_defs",
                ["RdoEckert"] = "+proj=eck4 +datum=WGS84 +units=m +no_defs",
                ["RdoCylindrical"] = "+proj=cea +datum=WGS84 +units=m +no_defs",
                ["RdoEquirectangular"] = "+proj=eqc +datum=WGS84 +units=m +no_defs",
                ["RdoMercator"] = "+proj=merc +datum=WGS84 +units=m +no_defs",
            };

        private ShapeFileFeatureLayer _cpuLayer;
        private bool _ready;

        public CpuVsGpuProjection()
        {
            InitializeComponent();
        }

        private async void Maps_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || CpuMap.ActualWidth <= 0 || CpuMap.ActualHeight <= 0 || GpuMap.ActualWidth <= 0 || GpuMap.ActualHeight <= 0)
            {
                return;
            }

            CpuMap.MapUnit = GeographyUnit.Meter;
            GpuMap.MapUnit = GeographyUnit.Meter;

            // ---- left: projected on the way in ----
            _cpuLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile(CountriesFile));
            _cpuLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                new AreaStyle(new GeoPen(GeoColor.FromHtml("#8A8F96"), 1), new GeoSolidBrush(GeoColor.FromHtml("#4A4D52")));
            _cpuLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            var cpuOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            cpuOverlay.Layers.Add(_cpuLayer);
            CpuMap.Overlays.Add(cpuOverlay);
            CpuMap.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#1F2226"));

            // ---- right: projected on the way out ----
            // The one CPU transform this side does: 4326 to web mercator, once, so
            // the tiles are in the coordinates the renderer speaks. Everything the
            // projection radios ask for after that is done by the shader.
            var tiles = new FeatureSourceVectorTileSource();
            tiles.FeatureSources.Add("countries", new ShapeFileFeatureSource(SampleShared.Shapefile(CountriesFile))
            {
                ProjectionConverter = new ProjectionConverter(SourceSrid, 3857),
            });
            GpuMap.Basemap = new GpuBasemap(new MapStyle((string)Resources["StyleJson"], tiles));

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
            CpuMap.CurrentExtent = projectedExtent;
            await CpuMap.RefreshAsync();
            cpuClock.Stop();

            CpuReadout.Text = string.Create(CultureInfo.InvariantCulture,
                $"switch {cpuClock.ElapsedMilliseconds,4} ms   data read and reprojected again: yes");

            // ---- the GPU side: a different shader, and not one byte of data ----
            var gpuClock = Stopwatch.StartNew();
            var failed = string.Empty;
            try
            {
                var projection = DisplayProjection.FromProjString(projString);
                GpuMap.Basemap.DisplayProjection = projection;
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
                    $"switch {gpuClock.ElapsedMilliseconds,4} ms   data read and reprojected again: no");

            if (failed.Length > 0)
            {
                DatumReadout.Text = failed;
            }
        }

        /// <summary>
        /// The world's outline after this projection, so the GPU side frames what
        /// it draws the way the CPU side frames what it read.
        /// </summary>
        private static RectangleShape ProjectedWorldBounds(DisplayProjection projection)
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
        /// GPU side only owns the drawing, and a shader has nowhere to put a datum - so such a
        /// definition leaves the shader families and is drawn through the PROJ grid path, with
        /// a warning in the log.
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
                DisplayProjection.FromProjString(historic);
                gpu = "GPU: grid path - no shader family can apply a datum shift, so the definition is drawn through PROJ; the log carries a warning saying so";
            }
            catch (NotSupportedException ex)
            {
                gpu = "GPU: refused - " + ex.Message;
            }

            DatumReadout.Text = cpu + Environment.NewLine + Environment.NewLine + gpu;
        }
    }
}
