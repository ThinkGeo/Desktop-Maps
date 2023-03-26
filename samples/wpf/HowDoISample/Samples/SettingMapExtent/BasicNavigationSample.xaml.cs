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
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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
        }

        /// <summary>
        /// Zoom in on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double left clicking on the map, or by using the the scroll wheel.
        /// </summary>
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            mapView.ZoomIn();
        }

        /// <summary>
        /// Zoom out on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double right clicking on the map, or by using the the scroll wheel.
        /// </summary>
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            mapView.ZoomOut();
        }

        /// <summary>
        /// Pan the map in a direction using the PanDirection enum and set how far to pan based on percentage.
        /// The same effect can be achieved by using the ZoomPanBar arrows on the upper left of the map or by left click dragging anywhere on the map.
        /// </summary>
        private void PanArrow_Click(object sender, RoutedEventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    mapView.Pan(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    mapView.Pan(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    mapView.Pan(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    mapView.Pan(PanDirection.Down, percentage);
                    break;
            }
        }

        /// <summary>
        /// Rotate the map at an angle using the value of the rotateAngle Slider. Since this is just setting a property, you must refresh the map in order for the rotation to show.
        /// The same effect can be achieved by holding down the ALT key and left click dragging anywhere on the map.
        /// </summary>
        private void Rotate_Click(object sender, RoutedEventArgs e)
        {
            mapView.RotatedAngle = (float)rotateAngle.Value;
            mapView.Refresh();
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
