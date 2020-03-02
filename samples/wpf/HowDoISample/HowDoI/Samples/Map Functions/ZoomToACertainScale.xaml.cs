using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ZoomToACertainScale : UserControl
    {
        public ZoomToACertainScale()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            ThinkGeoCloudRasterMapsOverlay worldMapKitWpfOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitWpfOverlay);

            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);
            mapView.Refresh();
        }

        private void cmbScale_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                // Zoom to a certain scale.
                string zoomLevelScale = cmbScale.SelectedItem.ToString();
                double scale = Convert.ToDouble(zoomLevelScale.Split(':')[2], CultureInfo.InvariantCulture);
                mapView.ZoomToScale(scale);
            }
        }
    }
}