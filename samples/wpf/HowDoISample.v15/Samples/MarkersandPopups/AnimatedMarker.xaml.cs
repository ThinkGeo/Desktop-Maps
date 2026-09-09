using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Interaction logic for AnimatedMarker.xaml
    /// </summary>
    public partial class AnimatedMarker
    {

        private bool _initialized;
        public AnimatedMarker()
        {
            InitializeComponent();
        }

        CustomIcon _icon = new CustomIcon();

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = thinkGeoCloudVectorMapsOverlay;

            _icon = new CustomIcon();
            _icon.AnimationStarted = true;

            var markerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(markerOverlay);
            
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
    }
}
