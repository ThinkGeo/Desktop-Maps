using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Add, drag and remove markers, and give a marker any WPF element as its content.
    /// </summary>
    public partial class Markers
    {
        private readonly SimpleMarkerOverlay _markerOverlay = new SimpleMarkerOverlay();
        private readonly CustomIcon _animatedIcon = new CustomIcon();

        // Geometry drawn AT the center point, for contrast with the marker image whose tip points at it.
        private readonly DynamicSource _centerDot = new DynamicSource();

        private int _logIndex;
        private bool _initialized;

        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();

        public Markers()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Tilt with the right mouse button or the slider: a marker stays on its ground point under perspective.
            Map.IsTiltEnabled = true;

            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("dot", PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 10));
            style.AddDynamic(_centerDot);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9030;

            _markerOverlay.MarkerDragging += (_, args) => AppendLog($"MarkerDragging: ({args.WorldCoordinate.X:N0}, {args.WorldCoordinate.Y:N0})");
            _markerOverlay.MarkerDragged += (_, args) => AppendLog($"MarkerDragged: ({args.WorldCoordinate.X:N0}, {args.WorldCoordinate.Y:N0})");
            Map.Overlays.Add(_markerOverlay);

            _centerDot.SetPoint("center", new Feature(Map.CenterPoint), sizeInPixel: 10f, iconName: "dot");
            _markerOverlay.Markers.Add(NewMarker(Map.CenterPoint));

            _animatedIcon.AnimationStarted = Animate.IsChecked == true;
            var animatedMarker = new Marker(-10776790, 3908990)
            {
                Content = _animatedIcon,
                ImageSource = null,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top
            };
            animatedMarker.MouseClick += Marker_MouseClick;
            _markerOverlay.Markers.Add(animatedMarker);

            _ = Map.RefreshAsync();
        }

        private Marker NewMarker(PointShape position)
        {
            var marker = new Marker(position)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/marker.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
            marker.PositionChanged += (_, args) => AppendLog($"PositionChanged: ({args.NewPosition.X:N0}, {args.NewPosition.Y:N0})");
            marker.MouseClick += Marker_MouseClick;
            return marker;
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (AddMode.IsChecked != true) return;

            _markerOverlay.Markers.Add(NewMarker(e.WorldLocation));
            AppendLog($"Added: ({e.WorldLocation.X:N0}, {e.WorldLocation.Y:N0})");
            _ = _markerOverlay.RefreshAsync();
        }

        private void Marker_MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (RemoveMode.IsChecked != true) return;

            var marker = (Marker)sender;
            _markerOverlay.Markers.Remove(marker);
            AppendLog($"Removed: ({marker.Position.X:N0}, {marker.Position.Y:N0})");
            _ = _markerOverlay.RefreshAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _markerOverlay.DragMode = DragMode.IsChecked == true ? MarkerDragMode.Drag : MarkerDragMode.None;
        }

        private void Animate_Changed(object sender, RoutedEventArgs e)
        {
            _animatedIcon.AnimationStarted = Animate.IsChecked == true;
        }

        private void AppendLog(string message)
        {
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }
    }
}
