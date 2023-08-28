using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class DisplayCompassSample : UserControl, IDisposable
    {
        public DisplayCompassSample()
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
            ThinkGeoCloudVectorMapsOverlay cloudOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~");
            cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
            // Set up the tile cache for the cloudOverlay, passing in the location and an ID to distinguish the cache. 
            cloudOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add("Cloud Overlay", cloudOverlay);
            
            // Set the current extent to a neighborhood in Frisco Texas.
            mapView.CurrentExtent = new RectangleShape(-10781708.9749424, 3913502.90429046, -10777685.1114043, 3910360.79646662);

            // Refresh the map.
            await mapView.RefreshAsync();
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
