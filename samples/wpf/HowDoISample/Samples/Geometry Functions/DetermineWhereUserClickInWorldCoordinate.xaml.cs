using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DetermineWhereUserClickInWorldCoordinate : UserControl
    {
        public DetermineWhereUserClickInWorldCoordinate()
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

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(worldOverlay);

            mapView.Refresh();
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            ProjectionConverter project = new ProjectionConverter(3857, 4326);
            project.Open();
            var worldPointInDecimalDegree = project.ConvertToExternalProjection(e.WorldX, e.WorldY);
            TBLonLat.Text = string.Format(CultureInfo.InvariantCulture, "X={0}, Y={1}", worldPointInDecimalDegree.X.ToString("N4", CultureInfo.InvariantCulture), worldPointInDecimalDegree.Y.ToString("N4", CultureInfo.InvariantCulture));
            project.Close();
        }
    }
}