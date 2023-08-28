using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class BasicNavigationSample : UserControl, IDisposable
    {
        public BasicNavigationSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Zoom in on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double left clicking on the map, or by using the the scroll wheel.
        /// </summary>
        private async void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            await mapView.ZoomInAsync();
        }

        /// <summary>
        /// Zoom out on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double right clicking on the map, or by using the the scroll wheel.
        /// </summary>
        private async void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            await mapView.ZoomOutAsync();
        }

        /// <summary>
        /// Pan the map in a direction using the PanDirection enum and set how far to pan based on percentage.
        /// The same effect can be achieved by using the ZoomPanBar arrows on the upper left of the map or by left click dragging anywhere on the map.
        /// </summary>
        private async void PanArrow_Click(object sender, RoutedEventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    await mapView.PanAsync(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    await mapView.PanAsync(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    await mapView.PanAsync(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    await mapView.PanAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        /// <summary>
        /// Rotate the map at an angle using the value of the rotateAngle Slider. Since this is just setting a property, you must refresh the map in order for the rotation to show.
        /// The same effect can be achieved by holding down the ALT key and left click dragging anywhere on the map.
        /// </summary>
        private async void Rotate_Click(object sender, RoutedEventArgs e)
        {
            mapView.RotatedAngle = (float)rotateAngle.Value;
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
