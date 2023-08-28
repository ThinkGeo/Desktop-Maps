using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Diagnostics;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudRasterMaps Layer on the map
    /// </summary>
    public partial class CloudMapsRasterLayerSample : UserControl, IDisposable
    {
        public CloudMapsRasterLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the map zoom level set to the Cloud Maps zoom level set.
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create the layer overlay with some additional settings and add to the map.
            ThinkGeoCloudRasterMapsOverlay cloudOverlay = new ThinkGeoCloudRasterMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~");
            cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
            mapView.Overlays.Add("Cloud Overlay",cloudOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            mapView.CurrentExtent = new RectangleShape(-10781708.9749424, 3913502.90429046, -10777685.1114043, 3910360.79646662);
            
            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private async void rbMapType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                ThinkGeoCloudRasterMapsOverlay cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Content.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                    default:
                        break;
                }
                await mapView.RefreshAsync();
            }
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
