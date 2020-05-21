using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class TABLayer : UserControl
    {
        public TABLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay parksOverlay = new LayerOverlay();
            mapView.Overlays.Add(parksOverlay);

            TabFeatureLayer parksLayer = new TabFeatureLayer(@"../../../Data/Tab/HoustonMuniBdySamp_Boundary.TAB");
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            parksLayer.StylingType = TabStylingType.StandardStyling;
            parksLayer.Open();

            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Green), GeoColors.Green);

            parksOverlay.Layers.Add("Frisco Parks", parksLayer);

            mapView.CurrentExtent = parksLayer.GetBoundingBox();

            mapView.Refresh();
        }
    }
}
