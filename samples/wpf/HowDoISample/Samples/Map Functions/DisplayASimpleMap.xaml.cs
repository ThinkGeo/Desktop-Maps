using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayASimpleMap : UserControl
    {
        public DisplayASimpleMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret,ThinkGeoCloudVectorMapsMapType.Light);
            Map1.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            Map1.Refresh();
        }
    }
}