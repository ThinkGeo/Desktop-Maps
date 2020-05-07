using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for PlotALatitudeAndLongitudePointOnTheMap.xaml
    /// </summary>
    public partial class PlotALatitudeAndLongitudePointOnTheMap : UserControl
    {
        public PlotALatitudeAndLongitudePointOnTheMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            InMemoryFeatureLayer pointLayer = new InMemoryFeatureLayer();
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Image;
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(SampleHelper.Get("United States.png"));
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay pointOverlay = new LayerOverlay();
            pointOverlay.TileType = TileType.SingleTile;
            pointOverlay.Layers.Add("PointLayer", pointLayer);
            mapView.Overlays.Add("PointOverlay", pointOverlay);

            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            mapView.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double latitude = double.Parse(tbLat.Text);
            double longitude = double.Parse(tbLon.Text);
            Feature feature = new Feature(longitude, latitude, "Point1");

            LayerOverlay pointOverlay = (LayerOverlay)mapView.Overlays["PointOverlay"];
            InMemoryFeatureLayer pointLayer = (InMemoryFeatureLayer)pointOverlay.Layers["PointLayer"];
            pointLayer.InternalFeatures.Clear();
            if (!pointLayer.InternalFeatures.Contains("Point1"))
            {
                pointLayer.InternalFeatures.Add("Point1", feature);
            }

            pointOverlay.Refresh();
        }
    }
}