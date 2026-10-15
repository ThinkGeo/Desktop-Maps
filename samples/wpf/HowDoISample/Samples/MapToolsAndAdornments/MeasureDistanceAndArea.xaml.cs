using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Measuring on the map with the track overlays that ship for it.
    ///
    /// Distance is MeasureInteractiveOverlay, a ruler: press, drag and release, and it
    /// labels the length and the bearing. Area is a TrackInteractiveOverlay drawing a
    /// polygon - click the corners, double-click the last one - and the finished shape
    /// is labelled with its area. Either one is installed as the map's track overlay,
    /// so the two never claim the same drags.
    /// </summary>
    public partial class MeasureDistanceAndArea
    {
        private bool _initialized;

        public MeasureDistanceAndArea()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            Map.TrackOverlay = new MeasureInteractiveOverlay(DistanceUnit.Kilometer);
            Map.CurrentExtent = new RectangleShape(-10_790_000, 3_925_000, -10_770_000, 3_905_000); // Frisco, TX
            await Map.RefreshAsync();
        }

        private async void Distance_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;
            Map.TrackOverlay = new MeasureInteractiveOverlay(DistanceUnit.Kilometer);
            await Map.RefreshAsync();
        }

        private async void Area_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;
            Map.TrackOverlay = NewAreaOverlay();
            await Map.RefreshAsync();
        }

        // A fresh overlay of the same kind is an empty one.
        private async void Clear_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay = Map.TrackOverlay is MeasureInteractiveOverlay
                ? new MeasureInteractiveOverlay(DistanceUnit.Kilometer)
                : NewAreaOverlay();
            await Map.RefreshAsync();
        }

        private static TrackInteractiveOverlay NewAreaOverlay()
        {
            var overlay = new TrackInteractiveOverlay { TrackMode = TrackMode.Polygon };
            var zoomLevel = overlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01;
            zoomLevel.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.CornflowerBlue), GeoColors.CornflowerBlue, 2);
            zoomLevel.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("label", "Noto Sans", 11, DrawingFontStyles.Regular, GeoColors.Black, GeoColors.White, 4);
            zoomLevel.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            overlay.TrackEnded += async (_, args) =>
            {
                if (args.TrackShape is not PolygonShape polygon) return;

                // Measured on the ground, not on the web-mercator plane, which grows
                // areas away from the equator - by about 40% in Texas.
                var toLonLat = new ProjectionConverter(3857, 4326);
                toLonLat.Open();
                var onGround = (PolygonShape)toLonLat.ConvertToExternalProjection(polygon);
                var area = onGround.GetArea(GeographyUnit.DecimalDegree, AreaUnit.SquareKilometers);
                var drawn = overlay.TrackShapeLayer.InternalFeatures.Last();
                drawn.ColumnValues["label"] = FormattableString.Invariant($"{area:N2} km²");
                await overlay.RefreshAsync();
            };
            return overlay;
        }
    }
}
