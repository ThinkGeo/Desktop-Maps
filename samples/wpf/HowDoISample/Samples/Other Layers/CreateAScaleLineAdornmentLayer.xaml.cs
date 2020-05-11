using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class CreateAScaleLineAdornmentLayer : UserControl
    {
        public CreateAScaleLineAdornmentLayer()
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

            ScaleLineAdornmentLayer scaleLineAdornmentLayer = new ScaleLineAdornmentLayer();
            mapView.AdornmentOverlay.Layers.Add("ScaleLineAdornmentLayer", scaleLineAdornmentLayer);

            mapView.Refresh();
        }

        private void cbxScaleLineLocation_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                ScaleLineAdornmentLayer currentScaleLineAdornmentLayer = (ScaleLineAdornmentLayer)mapView.AdornmentOverlay.Layers["ScaleLineAdornmentLayer"];
                currentScaleLineAdornmentLayer.Location = (AdornmentLocation)cbxScaleLineLocation.SelectedIndex;

                mapView.Refresh(mapView.AdornmentOverlay);
            }
        }
    }
}