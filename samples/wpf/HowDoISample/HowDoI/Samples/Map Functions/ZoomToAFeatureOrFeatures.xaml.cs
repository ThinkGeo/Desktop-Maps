using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ZoomToAFeatureOrFeatures : UserControl
    {
        public ZoomToAFeatureOrFeatures()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", staticOverlay);

            mapView.Refresh();
        }

        private void btnMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            Collection<string> featureIDs = new Collection<string>();
            featureIDs.Add("63");  // For US
            featureIDs.Add("6");   // For Canada
            featureIDs.Add("137"); // For Mexico

            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            lock (worldLayer)
            {
                if (!worldLayer.IsOpen) worldLayer.Open();
                Collection<Feature> features = worldLayer.FeatureSource.GetFeaturesByIds(featureIDs, new string[0]);
                mapView.CurrentExtent = MapUtil.GetBoundingBoxOfItems(features);
            }

            mapView.Refresh();
        }

        private void btnOneFeature_Click(object sender, RoutedEventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            lock (worldLayer)
            {
                if (!worldLayer.IsOpen) worldLayer.Open();
                mapView.CurrentExtent = worldLayer.FeatureSource.GetBoundingBoxById("137");
            }

            mapView.Refresh();
        }
    }
}