using System.Globalization;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class RadiusMearsureTrackInteractiveOverlay : TrackInteractiveOverlay
    {
        private bool isTracking;
        private LineShape radiusLine;
        private GeographyUnit mapUnit;
        private string radiusValueString;
        private Vertex circleCenterVertex;
        private DistanceUnit distanceUnit;
        private ScreenPointF mouseScreenPointF;

        public RadiusMearsureTrackInteractiveOverlay()
            : this(DistanceUnit.Mile, GeographyUnit.DecimalDegree)
        { }

        public RadiusMearsureTrackInteractiveOverlay(DistanceUnit distanceUnit, GeographyUnit mapUnit)
        {
            this.mapUnit = mapUnit;
            this.distanceUnit = distanceUnit;

            TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.DarkBlue, 8);
            TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.White, 3, true);
            TrackShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColor.SimpleColors.LightGreen), GeoColor.SimpleColors.White, 2);
            TrackShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            if (TrackMode == TrackMode.Circle)
            {
                circleCenterVertex = new Vertex(interactionArguments.WorldX, interactionArguments.WorldY);
                TrackShapeLayer.InternalFeatures.Add("center", new Feature(circleCenterVertex));
            }

            return base.MouseDownCore(interactionArguments);
        }

        protected override InteractiveResult MouseMoveCore(InteractionArguments interactionArguments)
        {
            InteractiveResult arguments = base.MouseMoveCore(interactionArguments);
            if (Vertices.Count == 0)
            {
                radiusValueString = string.Empty;
            }
            else if (TrackMode == TrackMode.Circle)
            {
                Vertex currentVertex = new Vertex(interactionArguments.WorldX, interactionArguments.WorldY);
                radiusLine = new LineShape(new[] { circleCenterVertex, currentVertex });
                TrackShapeLayer.InternalFeatures["radius"] = new Feature(radiusLine);
                mouseScreenPointF = new ScreenPointF(interactionArguments.ScreenX + 10, interactionArguments.ScreenY - 10);
            }

            return arguments;
        }

        protected override void OnDrawing(DrawingOverlayEventArgs e)
        {
            DrawMeasureResult(e.GeoCanvas);
            base.OnDrawing(e);
        }

        protected override void OnMapMouseUp(MapMouseUpInteractiveOverlayEventArgs e)
        {
            base.OnMapMouseUp(e);

            if (TrackMode == TrackMode.Circle)
            {
                if (TrackShapeLayer.InternalFeatures.Contains("center"))
                {
                    TrackShapeLayer.InternalFeatures.Remove("center");
                }

                if (TrackShapeLayer.InternalFeatures.Contains("radius"))
                {
                    TrackShapeLayer.InternalFeatures.Remove("radius");
                }
            }
        }

        protected override void OnTrackEnded(TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            isTracking = false;
            base.OnTrackEnded(e);
        }

        protected override void OnTrackStarting(TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            isTracking = true;
            base.OnTrackStarting(e);
        }

        private void DrawMeasureResult(GeoCanvas canvas)
        {
            if (TrackMode == TrackMode.Circle && isTracking)
            {
                if (TrackShapeLayer.InternalFeatures.Count > 0 && radiusLine != null)
                {
                    double r = radiusLine.GetLength(mapUnit, distanceUnit);
                    radiusValueString = string.Format(CultureInfo.InvariantCulture, "{0:N3} {1}", r, GetAbbreviateDistanceUnit(distanceUnit));
                    canvas.DrawTextWithScreenCoordinate(radiusValueString, new GeoFont(),
                        new GeoSolidBrush(GeoColor.SimpleColors.Black), mouseScreenPointF.X,
                        mouseScreenPointF.Y, DrawingLevel.LabelLevel);
                }
            }
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
    }
}