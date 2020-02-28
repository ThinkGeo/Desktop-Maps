using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class FindTheDistanceBetweenTwoFeatures : UserControl
    {
        public FindTheDistanceBetweenTwoFeatures()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14283508, 20037508, 14676953, -9263005);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer usInMemoryLayer = new InMemoryFeatureLayer();
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get(@"United States.png"));
            usInMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            usInMemoryLayer.InternalFeatures.Add("US", new Feature(-10973875, 4803651, "1"));

            InMemoryFeatureLayer chinaInMemoryLayer = new InMemoryFeatureLayer();
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get("China.png"));
            chinaInMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            chinaInMemoryLayer.InternalFeatures.Add("CHINA", new Feature(11657377, 4089387, "2"));

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("USInMemoryFeatureLayer", usInMemoryLayer);
            staticOverlay.Layers.Add("ChinaInMemoryFeatureLayer", chinaInMemoryLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }

        private void btnGetDistance_Click(object sender, RoutedEventArgs e)
        {
            InMemoryFeatureLayer usInMemoryLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("USInMemoryFeatureLayer");
            InMemoryFeatureLayer chinaInMemoryLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("ChinaInMemoryFeatureLayer");

            ProjectionConverter project = new ProjectionConverter(3857, 4326);
            project.Open();
            BaseShape usShape = project.ConvertToExternalProjection(usInMemoryLayer.InternalFeatures["US"].GetShape());
            BaseShape chinaShape = project.ConvertToExternalProjection(chinaInMemoryLayer.InternalFeatures["CHINA"].GetShape());
            project.Close();

            double distance = usShape.GetDistanceTo(chinaShape, GeographyUnit.DecimalDegree, DistanceUnit.Kilometer);
            txtDistance.Text = string.Format(CultureInfo.InvariantCulture, "{0 :N4} Km", distance);
        }
    }
}