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
        public AnimatedMarker()
        {
            InitializeComponent();
        }

        CustomIcon _icon = new CustomIcon();

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
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

                _icon = new CustomIcon();
                _icon.AnimationStarted = true;

                var marker = new Marker(-10777932, 3912260)
                {
                    Content = _icon,
                    ImageSource = null,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Top
                };

                var markerOverlay = new SimpleMarkerOverlay();
                markerOverlay.Markers.Add(marker);
                MapView.Overlays.Add(markerOverlay);

                // Set the map extent
                MapView.CenterPoint = new PointShape(-10778000, 3912000);
                MapView.CurrentScale = 77000;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _icon.AnimationStarted = !_icon.AnimationStarted;
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
