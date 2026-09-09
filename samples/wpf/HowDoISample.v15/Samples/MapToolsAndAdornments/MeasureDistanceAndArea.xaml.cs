using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Measuring on the map, with the overlay that already ships for it.
    ///
    /// MeasureInteractiveOverlay is a TrackInteractiveOverlay that annotates what it
    /// draws - segment lengths as you go, the total when you finish - so measuring is
    /// not something to hand-build out of mouse events. Install it as the map's track
    /// overlay and pick a mode: line for distance, polygon for area. Double-click ends
    /// a shape.
    /// </summary>
    public partial class MeasureDistanceAndArea
    {
        private readonly MeasureInteractiveOverlay _measure;
        private bool _initialized;

        public MeasureDistanceAndArea()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));

            // Replacing the map's track overlay, not adding a second one: two track
            // overlays would both claim the same drags.
            _measure = new MeasureInteractiveOverlay(DistanceUnit.Kilometer);
            Map.TrackOverlay = _measure;
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            _measure.TrackMode = TrackMode.Line;
            Map.CurrentExtent = new RectangleShape(-10_790_000, 3_925_000, -10_770_000, 3_905_000); // Frisco, TX
            await Map.RefreshAsync();
        }

        private void Distance_Checked(object sender, RoutedEventArgs e)
        {
            if (_measure != null)
            {
                _measure.TrackMode = TrackMode.Line;
            }
        }

        private void Area_Checked(object sender, RoutedEventArgs e) => _measure.TrackMode = TrackMode.Polygon;

        private async void Clear_Click(object sender, RoutedEventArgs e)
        {
            _measure.TrackShapeLayer.InternalFeatures.Clear();
            await Map.RefreshAsync();
        }
    }
}
