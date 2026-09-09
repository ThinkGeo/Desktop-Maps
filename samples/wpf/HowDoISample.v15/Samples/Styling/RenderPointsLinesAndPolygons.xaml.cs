using System;
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
    /// Style points, lines and polygons, and label each of them, from the style
    /// document beside the map - edit it and the map follows.
    /// </summary>
    public partial class RenderPointsLinesAndPolygons
    {
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public RenderPointsLinesAndPolygons()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The shapefiles are in Texas North Central feet (EPSG:2276); reproject to the map's EPSG:3857.
            var tileSource = new FeatureSourceVectorTileSource();
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Streets.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Hotels.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });

            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text, tileSource));

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9000;

            _ = Map.RefreshAsync();
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(new MapStyle(Editor.Text));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }
    }
}
