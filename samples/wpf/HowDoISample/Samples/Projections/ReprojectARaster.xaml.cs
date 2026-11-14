using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Reproject a raster - a GeoTIFF file or a raster tile service - with a GdalProjectionConverter on its layer.
    /// </summary>
    public partial class ReprojectARaster
    {
        // Reprojection happens in the layer's draw path, so the layer is drawn by a
        // LayerOverlay, as one image for the whole view.
        private readonly LayerOverlay _overlay = new LayerOverlay { TileType = TileType.SingleTile };
        private bool _initialized;

        public ReprojectARaster()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.Overlays.Add(_overlay);
            await ShowRasterAsync();
        }

        private async void Raster_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowRasterAsync();
        }

        private async void Server_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            try
            {
                // Same view, different tiles: swapping the server does not move the map.
                ReprojectATileService();
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private async Task ShowRasterAsync()
        {
            try
            {
                ServerPanel.Visibility = TileService.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
                if (RasterFile.IsChecked == true)
                {
                    ReprojectARasterFile();
                }
                else
                {
                    ReprojectATileService();

                    // A neighborhood in Frisco, Texas, in longitude and latitude.
                    Map.MapUnit = GeographyUnit.DecimalDegree;
                    Map.CenterPoint = new PointShape(-96.82, 33.11);
                    Map.CurrentScale = 1000000;
                }

                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private void ReprojectARasterFile()
        {
            var world = new GeoTiffRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoTiff", "World.tif"));

            // From World Geodetic System (EPSG:4326) to US National Atlas Equal Area (EPSG:9311).
            world.ImageSource.ProjectionConverter = new GdalProjectionConverter(4326, 9311);

            _overlay.Layers.Clear();
            _overlay.Layers.Add("World", world);

            world.Open();
            var bounds = world.GetBoundingBox();
            world.Close();

            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = bounds.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, bounds, Map.MapWidth, Map.MapHeight) * 1.5;
        }

        private void ReprojectATileService()
        {
            RasterXyzTileAsyncLayer layer = ServerOsm.IsChecked == true
                ? new OpenStreetMapAsyncLayer("ThinkGeo Samples")
                : new ThinkGeoRasterMapsAsyncLayer(SampleShared.ClientId, SampleShared.ClientSecret, ThinkGeoCloudRasterMapsMapType.Dark_V2_X1);

            // The tiles are fetched on their own web-mercator grid and warped into degrees as they draw.
            layer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);

            _overlay.Layers.Clear();
            _overlay.Layers.Add(layer);
        }
    }
}
