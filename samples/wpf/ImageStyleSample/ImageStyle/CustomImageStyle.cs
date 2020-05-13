using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ImageStyle
{
    class ImageAreaStyle : Style
    {
        private GeoImage geoImage = null;

        public ImageAreaStyle(GeoImage geoImage)
        {
            this.geoImage = geoImage;
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            // Loop through all of the features being passed in to draw.
            foreach (Feature feature in features)
            {
                WellKnownType shapeWellKnownType = feature.GetWellKnownType();
                if (shapeWellKnownType == WellKnownType.Polygon)
                {
                    PolygonShape polygonShape = new PolygonShape(feature.GetWellKnownBinary());
                    canvas.DrawArea(polygonShape, new GeoTextureBrush(geoImage), DrawingLevel.LevelOne);

                }
                else if (shapeWellKnownType == WellKnownType.Multipolygon)
                {
                    MultipolygonShape multiPolygonShape = new MultipolygonShape(feature.GetWellKnownBinary());
                    canvas.DrawArea(multiPolygonShape, new GeoTextureBrush(geoImage), DrawingLevel.LevelOne);
                }
            }
        }

    }

    class ImageLineStyle : Style
    {
        private GeoImage geoImage = null;
        private int lineWidth;

        public ImageLineStyle(GeoImage geoImage, int lineWidth)
        {
            this.geoImage = geoImage;
            this.lineWidth = lineWidth;
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            // Loop through all of the features being passed in to draw.
            foreach (Feature feature in features)
            {
                WellKnownType shapeWellKnownType = feature.GetWellKnownType();

                //We Get the distance in world coordinate to always have the same width of the line in screen coordinate regardless of the zoom level
                double worldDistance = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(canvas.CurrentWorldExtent, new ScreenPointF(0, 0), new ScreenPointF(lineWidth, 0), canvas.Width,
                       canvas.Height, canvas.MapUnit, DistanceUnit.Meter);

                if (shapeWellKnownType == WellKnownType.Line)
                {
                    LineShape lineShape = new LineShape(feature.GetWellKnownBinary());
                    //We get the buffer of the lineShape so that we can fill it with the GeoImage
                    MultipolygonShape multiPolygonShapeBuffer = lineShape.Buffer(worldDistance, canvas.MapUnit, DistanceUnit.Meter);
                    canvas.DrawArea(multiPolygonShapeBuffer, new GeoTextureBrush(geoImage), DrawingLevel.LevelOne);
                }
                else if (shapeWellKnownType == WellKnownType.Multiline)
                {
                    MultilineShape multilineShape = new MultilineShape(feature.GetWellKnownBinary());
                    //We get the buffer of the multiLineShape so that we can fill it with the GeoImage
                    MultipolygonShape multiPolygonShapeBuffer = multilineShape.Buffer(worldDistance, canvas.MapUnit, DistanceUnit.Meter);
                    canvas.DrawArea(multiPolygonShapeBuffer, new GeoTextureBrush(geoImage), DrawingLevel.LevelOne);
                }
            }
        }
    }
}
