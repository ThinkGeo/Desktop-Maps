using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Measure a distance with the MeasureInteractiveOverlay ruler, or an area with a polygon drawn on the TrackInteractiveOverlay.
    /// </summary>
    public partial class MeasureDistanceAndArea : IDisposable
    {
        private bool _initialized;

        public MeasureDistanceAndArea()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // Frisco, TX
            Map.CurrentExtent = new RectangleShape(-10790000, 3925000, -10770000, 3905000);
            InstallTool();
        }

        private void Tool_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            InstallTool();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InstallTool();
        }

        /// <summary>
        /// Puts a fresh, empty measuring overlay in place of the map's track overlay, so the two tools never claim the same drags.
        /// </summary>
        private void InstallTool()
        {
            if (AreaMode.IsChecked == true)
            {
                Map.TrackOverlay = NewAreaOverlay();
            }
            else
            {
                // With the map's projection the ruler measures on the ground, not on the flat map.
                Map.TrackOverlay = new MeasureInteractiveOverlay(DistanceUnit.Kilometer) { Projection = new Projection(3857) };
            }

            _ = Map.RefreshAsync();
        }

        private static TrackInteractiveOverlay NewAreaOverlay()
        {
            var overlay = new TrackInteractiveOverlay { TrackMode = TrackMode.Polygon };
            var zoomLevel = overlay.TrackShapeLayer.ZoomLevelSet.ZoomLevel01;
            zoomLevel.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.CornflowerBlue), GeoColors.CornflowerBlue, 2);
            zoomLevel.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("label", "Noto Sans", 11, DrawingFontStyles.Regular, GeoColors.Black, GeoColors.White, 4);
            zoomLevel.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            overlay.TrackEnded += (_, args) =>
            {
                if (!(args.TrackShape is PolygonShape polygon)) return;

                // Measured on the ground, not on the Web Mercator plane, which grows areas away from the equator - by about 40% in Texas.
                var toLonLat = new ProjectionConverter(3857, 4326);
                toLonLat.Open();
                var onGround = (PolygonShape)toLonLat.ConvertToExternalProjection(polygon);
                toLonLat.Close();
                var area = onGround.GetArea(GeographyUnit.DecimalDegree, AreaUnit.SquareKilometers);
                var drawn = overlay.TrackShapeLayer.InternalFeatures.Last();
                drawn.ColumnValues["label"] = FormattableString.Invariant($"{area:N2} km²");
                _ = overlay.RefreshAsync();
            };
            return overlay;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
