using System;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoMBTilesMapsSample_ForWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ThinkGeoMBTilesFeatureLayer thinkGeoMBTilesFeatureLayer;
        private LayerOverlay layerOverlay;
        private BitmapTileCache tmpBitmapTileCache;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.wpfMap.MapUnit = GeographyUnit.Meter;
            this.wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create background world map with vector tile requested from loacl Database. 
            thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/thinkgeo-world-streets-light.json", UriKind.Relative));

            layerOverlay = new LayerOverlay();
            layerOverlay.MaxExtent = thinkGeoMBTilesFeatureLayer.GetTileMatrixBoundingBox();
            layerOverlay.TileWidth = 512;
            layerOverlay.TileHeight = 512;

            layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);

            this.wpfMap.Overlays.Add(layerOverlay);
            this.wpfMap.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);
            this.wpfMap.Refresh();
        }

        private void RadioButtonLight_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoMBTilesFeatureLayer != null)
            {
                layerOverlay.Layers.Clear();
                thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/thinkgeo-world-streets-light.json", UriKind.Relative));
                UpdateTileCache(this.chkDisableRasterCache.IsChecked.Value);
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);
                layerOverlay.Refresh();
            }
        }

        private void RadioButtionDark_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoMBTilesFeatureLayer != null)
            {
                layerOverlay.Layers.Clear();
                thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/thinkgeo-world-streets-dark.json", UriKind.Relative));
                UpdateTileCache(this.chkDisableRasterCache.IsChecked.Value);
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);
                layerOverlay.Refresh();
            }
        }

        private void RadioButtionMuteblue_Checked(object sender, RoutedEventArgs e)
        {
            if (thinkGeoMBTilesFeatureLayer != null)
            {
                layerOverlay.Layers.Clear();
                thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/mutedblue.json", UriKind.Relative));
                UpdateTileCache(this.chkDisableRasterCache.IsChecked.Value);
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);
                layerOverlay.Refresh();
            }
        }

        private void ChkDisableRasterCache_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTileCache(this.chkDisableRasterCache.IsChecked.Value);
        }

        private void UpdateTileCache(bool disableTileCache)
        {
            if (thinkGeoMBTilesFeatureLayer != null && this.thinkGeoMBTilesFeatureLayer.BitmapTileCache != null)
            {
                this.tmpBitmapTileCache = this.thinkGeoMBTilesFeatureLayer.BitmapTileCache;
            }

            if (disableTileCache)
            {
                this.thinkGeoMBTilesFeatureLayer.BitmapTileCache = null;
            }
            else
            {
                this.thinkGeoMBTilesFeatureLayer.BitmapTileCache = this.tmpBitmapTileCache;
            }
        }
    }
}
