using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for UsingTileCache.xaml
    /// </summary>
    public partial class UsingTileCache : UserControl
    {
        public UsingTileCache()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);

            LayerOverlay worldOverlay = new LayerOverlay();
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            BackgroundLayer backgroundLayer = new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean));
            worldOverlay.Layers.Add(backgroundLayer);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);

            // If you want to use file cache which saves images to the disk;
            // When loading back the same tileView, we'll find from the disk first.
            // Turn the cache on enhance the performance a lot;
            // if your map image is static, we recommend to turn the cache on.
            // It's off by default.
            FileRasterTileCache bitmapTileCache = new FileRasterTileCache();
            bitmapTileCache.CacheDirectory = Path.Combine(Path.GetTempPath(), "ThinkGeo", "TileCaches", "UsingTileCache-Wpf");
            bitmapTileCache.CacheId = "World02CachedTiles";
            bitmapTileCache.TileAccessMode = TileAccessMode.ReadOnly;
            bitmapTileCache.ImageFormat = RasterTileFormat.Png;
            worldOverlay.TileCache = bitmapTileCache;

            mapView.Refresh();
        }
    }
}