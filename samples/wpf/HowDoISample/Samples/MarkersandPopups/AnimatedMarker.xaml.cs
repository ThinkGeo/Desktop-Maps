using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for AnimatedMarker.xaml
    /// </summary>
    public partial class AnimatedMarker : IDisposable
    {

        private bool _initialized;
        public AnimatedMarker()
        {
            InitializeComponent();
        }

        CustomIcon _icon = new CustomIcon();

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

            _icon = new CustomIcon();
            _icon.AnimationStarted = true;

            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);
            
            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            var marker = new Marker(-10777932, 3912260)
            {
                Content = _icon,
                ImageSource = null,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top
            };
            markerOverlay.Markers.Add(marker);

            _ = Map.RefreshAsync();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _icon.AnimationStarted = !_icon.AnimationStarted;
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
