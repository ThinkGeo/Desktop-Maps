using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class TinyGeoLayer : UserControl
    {
        public TinyGeoLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay tinyGeoOverlay = new LayerOverlay();
            mapView.Overlays.Add(tinyGeoOverlay);

            TinyGeoFeatureLayer tinyGeoLayer = new TinyGeoFeatureLayer(@"../../../Data/Zoning.tgeo");
            tinyGeoLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            tinyGeoLayer.Open();

            tinyGeoLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            tinyGeoLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Black, new GeoSolidBrush(new GeoColor(50,GeoColors.Blue)));

            tinyGeoOverlay.Layers.Add("Zoning", tinyGeoLayer);

            mapView.CurrentExtent = tinyGeoLayer.GetBoundingBox();

            mapView.Refresh();
        }
    }
}
