using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ScaleUpAndDownAFeature : UserControl
    {
        public ScaleUpAndDownAFeature()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-18473122, 20037508, -3992891, -711306);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("135", new string[0]);
            worldLayer.Close();

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Green;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryLayer.InternalFeatures.Add("135", feature);

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.TileType = TileType.SingleTile;
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            mapView.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            mapView.Refresh();
        }

        private void btnScaleUp_Click(object sender, RoutedEventArgs e)
        {
            UpdateFeatureByScale(25, true);
        }

        private void btnScaleDown_Click(object sender, RoutedEventArgs e)
        {
            UpdateFeatureByScale(20, false);
        }

        private void UpdateFeatureByScale(double percentage, bool isScaleUp)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("InMemoryFeatureLayer");
            inMemoryLayer.Open();
            inMemoryLayer.EditTools.BeginTransaction();
            if (isScaleUp)
            {
                inMemoryLayer.EditTools.ScaleUp("135", percentage);
            }
            else
            {
                inMemoryLayer.EditTools.ScaleDown("135", percentage);
            }
            inMemoryLayer.EditTools.CommitTransaction();
            inMemoryLayer.Close();

            mapView.Overlays["InMemoryOverlay"].Refresh();
        }
    }
}