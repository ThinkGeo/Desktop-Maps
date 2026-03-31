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

        private bool _initialized;
        public TextMarker()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);

            // Set the map extent
            Map.CurrentExtent = MaxExtents.SphericalMercator;

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

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
