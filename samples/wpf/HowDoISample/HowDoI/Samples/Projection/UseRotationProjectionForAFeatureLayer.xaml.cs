using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class UseRotationProjectionForAFeatureLayer : UserControl
    {
        private RotationProjectionConverter rotateProjection;

        public UseRotationProjectionForAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            rotateProjection = new RotationProjectionConverter();
            mapView.CurrentExtent = rotateProjection.GetUpdatedExtent(new RectangleShape(-20037508, 17821912, 20037508, -20037508));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.FeatureSource.ProjectionConverter = rotateProjection;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.TileType = TileType.SingleTile;
            worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }

        private void btnRotateCounterclockwise_Click(object sender, RoutedEventArgs e)
        {
            rotateProjection.Angle += 20;
            mapView.CurrentExtent = rotateProjection.GetUpdatedExtent(mapView.CurrentExtent);

            mapView.Overlays["WorldOverlay"].Refresh();
        }

        private void btnRotateClockwise_Click(object sender, RoutedEventArgs e)
        {
            rotateProjection.Angle -= 20;
            mapView.CurrentExtent = rotateProjection.GetUpdatedExtent(mapView.CurrentExtent);

            mapView.Overlays["WorldOverlay"].Refresh();
        }
    }
}