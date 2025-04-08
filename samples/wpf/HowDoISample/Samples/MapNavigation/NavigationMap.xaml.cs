using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to zoom, pan, and rotate the map control.
    /// </summary>
    public partial class NavigationMap : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private ThinkGeoCloudRasterMapsOverlay backgroundOverlay;
        private SimpleMarkerOverlay markerOverlay;

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

            // Add ThinkGeo Cloud Maps as the background 
            backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(backgroundOverlay);

            // Create the marker overlay to hold UI elements like labels
            markerOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add(markerOverlay);

            // Convert Lat/Lon (EPSG:4326) to Spherical Mercator (EPSG:3857)
            var empireStateBuilding = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            // Create the image control (Empire State Building)
            var imageControl = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resources/empire_state_building.png")),
                Width = 64,
                Height = 64,
                Stretch = Stretch.Uniform
            };
            // Set the BitmapScalingMode to HighQuality for better image rendering
            RenderOptions.SetBitmapScalingMode(imageControl, BitmapScalingMode.HighQuality);

            //// Create a label as a TextBlock
            //var label = new TextBlock
            //{
            //    Text = "Empire State Building",
            //    Foreground = Brushes.White,
            //    FontWeight = FontWeights.Bold,
            //    FontSize = 14,
            //    Background = Brushes.Black,
            //    Padding = new Thickness(4)
            //};

            //// Create a container to hold both the image and the label
            //var markerContent = new StackPanel
            //{
            //    Orientation = Orientation.Vertical,
            //    HorizontalAlignment = HorizontalAlignment.Center,  // Align horizontally at center
            //    VerticalAlignment = VerticalAlignment.Bottom,  // Align vertically at the bottom
            //    Children =
            //    {
            //        label,    // Place label above the image
            //        imageControl
            //    }
            //};

            // Create a marker with both label and image content
            var marker = new Marker(empireStateBuilding)
            {
                //Content = markerContent,
                Content = imageControl,
                ImageSource = null,
                Width = 64,
                Height = 84  // Make sure the height is enough to accommodate both image and label
            };

            // Add the marker to the overlay
            markerOverlay.Markers.Add(marker);

            // set up the map extent and refresh
            MapView.RotationAngle = -30;
            MapView.CurrentScale = 100000;
            MapView.CenterPoint = empireStateBuilding;

            // Update the position of the marker content if needed (the marker will adjust as required)
            UpdateMarkerPosition(empireStateBuilding);
            MapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            MapView_CurrentExtentChanged(MapView, 
                new CurrentExtentChangedMapViewEventArgs(MapView.CurrentExtent,MapView.CurrentExtent));

            _ = MapView.RefreshAsync();
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = MapView.CurrentExtent.GetCenterPoint();
            txtCoordinate.Text = $"Center Point: X= {center.X:N0}, Y= {center.Y:N0}";
        }

        private void UpdateMarkerPosition(PointShape newPosition)
        {
            Point imagePosition = new Point(newPosition.X, newPosition.Y);
            markerOverlay.Markers[0].Position = imagePosition;
        }

        private void ThemeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            backgroundOverlay.MapType = ThemeCheckBox.IsChecked == true
                ? ThinkGeoCloudRasterMapsMapType.Dark_V2_X2
                : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;

            // You may need to reset the tile cache ID to avoid mixing dark/light tiles:
            backgroundOverlay.TileCache = new FileRasterTileCache(@".\cache",
                ThemeCheckBox.IsChecked == true ? "thinkgeo_vector_dark" : "thinkgeo_vector_light");

            UpdateCancellationToken(); // if you're managing cancellation tokens for refresh
            _ = backgroundOverlay.RefreshAsync(_cancellationTokenSource.Token);
        }

        private void UpdateCancellationToken()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void CompassButton_Click(object sender, RoutedEventArgs e)
        {
            var currentCenter = MapView.CenterPoint;
            var currentScale = MapView.CurrentScale;
            _ = MapView.ZoomToAsync(currentCenter, currentScale, 0);
        }

        private void DefaultExtentButton_Click(object sender, RoutedEventArgs e)
        {
            var empireStateBuilding = ProjectionConverter.Convert(4326, 3857, new PointShape(-73.9856654, 40.74843661));

            // Update the marker's position
            UpdateMarkerPosition(empireStateBuilding);
            _ = MapView.ZoomToAsync(empireStateBuilding, 100000, -30);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}