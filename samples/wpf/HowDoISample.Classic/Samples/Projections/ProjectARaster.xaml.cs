using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Reproject a GeoTIFF raster layer with a GdalProjectionConverter on its image source.
    /// </summary>
    public partial class ProjectARaster : IDisposable
    {
        private bool _initialized;

        public ProjectARaster()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var worldRasterLayer = new GeoTiffRasterLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoTiff", "World.tif"));

            // From WGS 84 (EPSG:4326) to US National Atlas Equal Area (EPSG:9311).
            worldRasterLayer.ImageSource.ProjectionConverter = new GdalProjectionConverter(4326, 9311);

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(worldRasterLayer);
            Map.Overlays.Add(layerOverlay);

            try
            {
                worldRasterLayer.Open();
                var worldRasterLayerBBox = worldRasterLayer.GetBoundingBox();
                worldRasterLayer.Close();
                Map.CenterPoint = worldRasterLayerBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, worldRasterLayerBBox, Map.MapWidth, Map.MapHeight) * 1.5;

                await Map.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
