using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Interaction logic for SetMapProjection.xaml
    /// </summary>
    public partial class SetMapProjection : UserControl
    {
        public SetMapProjection()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);
            RectangleShape friscoBoundingBox = new RectangleShape(-96.885836, 33.183707, -96.769308, 33.105552);
            ProjectionConverter projectionConverter = new ProjectionConverter(4326, 3857);
            projectionConverter.Open();
            mapView.CurrentExtent = projectionConverter.ConvertToExternalProjection(friscoBoundingBox);
            projectionConverter.Close();

            mapView.Refresh();
        }
        private void rbMapProjection_Checked(object sender, RoutedEventArgs e)
        {
            // Redraw the map
            mapView.Refresh();
        }
    }
}
