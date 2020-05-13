using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace DraggedPointStyleWithLabel
{
    class DragInteractiveOverlay : EditInteractiveOverlay
    {
        private PointStyle controlPointStyle;
        private PointStyle draggedControlPointStyle;
        private BaseShape referenceShape;

        public PointStyle ControlPointStyle
        {
            get { return controlPointStyle; }
            set { controlPointStyle = value; }
        }

        public PointStyle DraggedControlPointStyle
        {
            get { return draggedControlPointStyle; }
            set { draggedControlPointStyle = value; }
        }

        public BaseShape ReferenceShape
        {
            get { return referenceShape; }
            set { referenceShape = value; }
        }

        //Overrides the DrawCore function.
        protected override void DrawCore(GeoCanvas canvas)
        {
            //Draws the Edit Shapes as default.
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            EditShapesLayer.Open();
            EditShapesLayer.Draw(canvas, labelsInAllLayers);
            canvas.Flush();

            //Draws the control points. 
            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            //Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Looks at the value of "state" to draw the control point as dragged or not.
                Feature[] features = new Feature[1] { feature };
                draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);

                PointShape pointShape = feature.GetShape() as PointShape;
                PointShape closestPointShape = referenceShape.GetClosestPointTo(pointShape, GeographyUnit.Meter);
                //Draws the closest point on the reference shape and the distance to it from the dragged control point.
                if (closestPointShape != null)
                {
                    double Dist = System.Math.Round(closestPointShape.GetDistanceTo(pointShape, GeographyUnit.Meter, DistanceUnit.Meter));
                    ScreenPointF ScreenPointF = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
                    canvas.DrawTextWithScreenCoordinate(System.Convert.ToString(Dist) + " m", new GeoFont("Arial", 12, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.StandardColors.Black),
                                                      ScreenPointF.X + 35, ScreenPointF.Y, DrawingLevel.LabelLevel);
                    canvas.DrawEllipse(closestPointShape, 12, 12, new GeoSolidBrush(GeoColor.StandardColors.Purple), DrawingLevel.LevelFour);
                }
            }
            foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
            {
                Feature[] features = new Feature[1] { feature };
                controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
            }
        }
    }
}
