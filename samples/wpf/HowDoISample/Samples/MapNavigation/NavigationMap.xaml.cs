using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class NavigationMap : IDisposable
    {
        public NavigationMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10519865,4578709);
            MapView.CurrentScale = 35000000;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Zoom in on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double left-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomInAsync();
        }

        /// <summary>
        /// Zoom out on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double right-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomOutAsync();
        }

        private void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
             _ = MapView.ZoomToScaleAsync(Convert.ToDouble(ZoomScale.Text));
        }
        /// <summary>
        /// Pan the map in a direction using the PanDirection enum and set how far to pan based on percentage.
        /// The same effect can be achieved by using the ZoomPanBar arrows on the upper left of the map or by left click dragging anywhere on the map.
        /// </summary>
        private void PanArrow_Click(object sender, RoutedEventArgs e)
        {
            var percentage = 50;
            switch (((Button)sender).Name)
            {
                case "PanNorth":
                    _ = MapView.PanByDirectionAsync(PanDirection.Up, percentage);
                    break;
                case "PanEast":
                    _ = MapView.PanByDirectionAsync(PanDirection.Right, percentage);
                    break;
                case "PanWest":
                    _ = MapView.PanByDirectionAsync(PanDirection.Left, percentage);
                    break;
                case "PanSouth":
                    _ = MapView.PanByDirectionAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void RotateAngle_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = MapView.ZoomToAsync(MapView.CurrentExtent.GetCenterPoint(), MapView.CurrentScale,
                RotateAngle.Value, _cancellationTokenSource.Token);
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            switch (radioButton.Content)
            {
                case "PreserveScale":
                    MapView.MapResizeMode = MapResizeMode.PreserveScale;
                    break;
                case "PreserveScaleAndCenter":
                    MapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
                    break;
                case "PreserveExtent":
                    MapView.MapResizeMode = MapResizeMode.PreserveExtent;
                    break;
            }
        }
    }
}