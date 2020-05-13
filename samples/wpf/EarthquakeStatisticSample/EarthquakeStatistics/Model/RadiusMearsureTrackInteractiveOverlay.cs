using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class RadiusMearsureTrackInteractiveOverlay : TrackInteractiveOverlay
    {
        private AreaStyle areaStyle;
        private LineStyle lineStyle;
        private PointStyle pointStyle;
        private Vertex circleCenterVertex;
        private LineShape radiusLine;
        private TextBlock textBlock;

        public RadiusMearsureTrackInteractiveOverlay()
            : this(DistanceUnit.Mile, GeographyUnit.DecimalDegree)
        {
        }

        public RadiusMearsureTrackInteractiveOverlay(DistanceUnit distanceUnit, GeographyUnit mapUnit)
        {
            DistanceUnit = distanceUnit;
            MapUnit = mapUnit;

            textBlock = new TextBlock();
            textBlock.Visibility = Visibility.Collapsed;
            OverlayCanvas.Children.Add(textBlock);

            PolygonTrackMode = PolygonTrackMode.LineWithFill;
            RenderMode = RenderMode.DrawingVisual;

            areaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColor.SimpleColors.LightGreen), GeoColor.SimpleColors.White, 2);
            pointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.DarkBlue, 8);
            lineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.White, 3, true);

            SetStylesForInMemoryFeatureLayer(TrackShapeLayer);
            SetStylesForInMemoryFeatureLayer(TrackShapesInProcessLayer);
        }

        public DistanceUnit DistanceUnit { get; set; }

        public GeographyUnit MapUnit { get; set; }

        protected override RectangleShape GetBoundingBoxCore()
        {
            return ExtentHelper.GetBoundingBoxOfItems(TrackShapeLayer.InternalFeatures);
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            if (TrackMode == TrackMode.Circle)
            {
                circleCenterVertex = new Vertex(interactionArguments.WorldX, interactionArguments.WorldY);
                TrackShapeLayer.InternalFeatures.Add(Resources.CenterFeatureKey, new Feature(circleCenterVertex));
            }

            return base.MouseDownCore(interactionArguments);
        }

        protected override InteractiveResult MouseMoveCore(InteractionArguments interactionArguments)
        {
            InteractiveResult arguments = base.MouseMoveCore(interactionArguments);
            if (Vertices.Count == 0)
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
            else if (TrackMode == TrackMode.Circle && interactionArguments.MouseButton == MapMouseButton.Left)
            {
                Vertex currentVertex = new Vertex(interactionArguments.WorldX, interactionArguments.WorldY);
                radiusLine = new LineShape(new[] { circleCenterVertex, currentVertex });
                TrackShapeLayer.InternalFeatures[Resources.RadiusFeatureKey] = new Feature(radiusLine);

                Canvas.SetLeft(textBlock, interactionArguments.ScreenX + 10);
                Canvas.SetTop(textBlock, interactionArguments.ScreenY + 10);
            }

            return arguments;
        }

        protected override void OnDrawing(DrawingOverlayEventArgs e)
        {
            MeasureRadius();
            base.OnDrawing(e);
        }

        protected override void OnMapMouseUp(MapMouseUpInteractiveOverlayEventArgs e)
        {
            base.OnMapMouseUp(e);

            if (TrackMode == TrackMode.Circle)
            {
                if (TrackShapeLayer.InternalFeatures.Contains(Resources.CenterFeatureKey))
                {
                    TrackShapeLayer.InternalFeatures.Remove(Resources.CenterFeatureKey);
                }

                if (TrackShapeLayer.InternalFeatures.Contains(Resources.RadiusFeatureKey))
                {
                    TrackShapeLayer.InternalFeatures.Remove(Resources.RadiusFeatureKey);
                }
            }
        }

        protected override void OnTrackEnded(TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            textBlock.Visibility = Visibility.Collapsed;
            base.OnTrackEnded(e);
        }

        protected override void OnTrackStarting(TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            if (TrackMode == TrackMode.Circle)
            {
                textBlock.Visibility = Visibility.Visible;
            }
            base.OnTrackStarting(e);
        }

        private static string GetAbbreviateDistanceUnit(DistanceUnit unit)
        {
            switch (unit)
            {
                case DistanceUnit.Mile:
                    return "mi";
                case DistanceUnit.Kilometer:
                    return "km";
                case DistanceUnit.Meter:
                    return "m";
                case DistanceUnit.Inch:
                    return "in";
                default:
                    return ".";
            }
        }

        private void MeasureRadius()
        {
            if (TrackMode == TrackMode.Circle)
            {
                if (TrackShapeLayer.InternalFeatures.Count > 0 && radiusLine != null)
                {
                    double radius = radiusLine.GetLength(MapUnit, DistanceUnit);
                    textBlock.Text = string.Format(CultureInfo.InvariantCulture, "{0:N3} {1}", radius, GetAbbreviateDistanceUnit(DistanceUnit));
                }
            }
        }

        private void SetStylesForInMemoryFeatureLayer(FeatureLayer featureLayer)
        {
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = lineStyle;
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = pointStyle;

            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
    }
}