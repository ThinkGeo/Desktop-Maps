using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class SQLiteLayer : UserControl
    {
        public SQLiteLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay restuarantsOverlay = new LayerOverlay();
            mapView.Overlays.Add(restuarantsOverlay);

            SqliteFeatureLayer restaurantsLayer = new SqliteFeatureLayer(@"Data Source=../../../Data/frisco-restaurants.sqlite;", "restaurants", "id","geometry" );
            restaurantsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var textStyle = new TextStyle("Name", new GeoFont("Arial", 12), GeoBrushes.Black);

            textStyle.MaskType = MaskType.RoundedCorners;
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.Mask = new AreaStyle(GeoBrushes.WhiteSmoke);
            textStyle.SuppressPartialLabels = true;
            textStyle.YOffsetInPixel = -5;

            restaurantsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 6, GeoBrushes.Blue);
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            restuarantsOverlay.Layers.Add("Frisco Restaurants", restaurantsLayer);

            mapView.CurrentExtent = new RectangleShape(-10776971.1234695, 3915454.06613793, -10775965.157585, 3914668.53918197);

            mapView.Refresh();
        }
    }
}
