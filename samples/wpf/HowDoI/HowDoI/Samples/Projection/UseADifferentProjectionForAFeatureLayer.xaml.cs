using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class UseADifferentProjectionForAFeatureLayer : UserControl
    {
        public UseADifferentProjectionForAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // If want to know more srids, please refer Projections.rtf in Documentation folder.
            ProjectionConverter proj4Projection = new ProjectionConverter(3857, 2163);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.FeatureSource.ProjectionConverter = proj4Projection;

            worldLayer.Open();
            mapView.CurrentExtent = worldLayer.GetBoundingBox();
            worldLayer.Close();

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }
    }
}