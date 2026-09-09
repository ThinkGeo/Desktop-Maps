using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to automatically reproject a layer using the ProjectionConverter class
    /// </summary>
    public partial class SettingTheProjectionOfALayer
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private readonly FeatureSourceVectorTileSource _tiles = new FeatureSourceVectorTileSource();
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public SettingTheProjectionOfALayer()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            // Set the Map Unit to meters (Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Reproject a shapefile and set the extent
            _ = ReprojectFeaturesFromShapefileAsync();
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject features in a ShapeFileFeatureLayer
        /// </summary>
        private async Task ReprojectFeaturesFromShapefileAsync()
        {
            // The converter belongs to the source, so it applies wherever the source is
            // read - the tile cutter included.
            var subdivisions = new ShapeFileFeatureSource(@"./Data/Shapefile/Subdivisions.shp")
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };
            _tiles.FeatureSources.Add("subdivisions", subdivisions);

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _tiles);
            Map.Basemap = new GpuBasemap(style);

            subdivisions.Open();
            var subdivisionsLayerBBox = subdivisions.GetBoundingBox();
            Map.CenterPoint = subdivisionsLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, subdivisionsLayerBBox, Map.MapWidth, Map.MapHeight) * 1.5;
            subdivisions.Close();

            await Map.RefreshAsync();
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
                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
                style.AddStyle(Editor.Text, _tiles);
                await Map.Basemap.SetStyleAsync(style);
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