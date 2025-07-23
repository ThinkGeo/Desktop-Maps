using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for AnimatedMarker.xaml
    /// </summary>
    public partial class TextMarker : IDisposable
    {
        public TextMarker()
        {
            InitializeComponent();
        }

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

            var markerOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add(markerOverlay);

            // Set the map extent
            MapView.CurrentExtent = MaxExtents.SphericalMercator;

            var marker = new Marker(0, 0)
            {
                Width = 100,    // give enough room for your text
                Height = 30,
                ImageSource = null,
                YOffset = -15,  // so the text sits above the point
                Content = new TextBlock
                {
                    Text = "Hello, World!",
                    FontSize = 25,
                    Foreground = Brushes.DarkBlue,
                    Padding = new Thickness(4),
                    TextAlignment = TextAlignment.Center
                }
            };

            // Add the marker to the overlay
            markerOverlay.Markers.Add(marker);
            markerOverlay.DragMode = MarkerDragMode.Drag;

            _ = MapView.RefreshAsync();
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
