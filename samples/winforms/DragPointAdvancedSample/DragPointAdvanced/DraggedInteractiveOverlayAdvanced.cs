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

namespace DraggedPointStyleAdvanced
{
    class DragInteractiveOverlayAdvanced : EditInteractiveOverlay
    {
        private PointStyle controlPointStyle;
        private PointStyle draggedControlPointStyle;
        private PointStyle draggedControlPointStyleWithShiftKey;
        private bool IsShiftKeyDown = false;
        private Feature snappingFeature;
        private Collection<Feature> controlPoints;

        public DragInteractiveOverlayAdvanced()
            : base()
        {
            ExistingControlPointsLayer.Open();
            ExistingControlPointsLayer.Columns.Add(new FeatureSourceColumn("nodetype"));
            ExistingControlPointsLayer.Close();
        }

        public Feature SnappingFeature
        {
            get { return snappingFeature; }
            set { snappingFeature = value; }
        }

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

        public PointStyle DraggedControlPointStyleWithShiftKey
        {
            get { return draggedControlPointStyleWithShiftKey; }
            set { draggedControlPointStyleWithShiftKey = value; }
        }

        protected override InteractiveResult KeyDownCore(KeyEventInteractionArguments interactionArguments)
        {
            IsShiftKeyDown = interactionArguments.IsShiftKeyPressed;
            return base.KeyDownCore(interactionArguments);
        }

        protected override InteractiveResult KeyUpCore(KeyEventInteractionArguments interactionArguments)
        {
            IsShiftKeyDown = interactionArguments.IsShiftKeyPressed;
            return base.KeyUpCore(interactionArguments);
        }

        protected override InteractiveResult MouseUpCore(InteractionArguments interactionArguments)
        {
            if (IsShiftKeyDown == true)
            {
                PolygonShape newPolygonShape = (PolygonShape)EditShapesLayer.InternalFeatures[0].GetShape();
                RingShape ringShape = new RingShape();

                foreach (Feature feature in controlPoints)
                {
                    PointShape controlPointShape = (PointShape)feature.GetShape();
                    if (feature.ColumnValues["nodetype"] == "special")
                    {
                        BaseShape baseShape = snappingFeature.GetShape();
                        PointShape pointShape = baseShape.GetClosestPointTo(controlPointShape, GeographyUnit.Meter);
                        ringShape.Vertices.Add(new Vertex(pointShape.X, pointShape.Y));
                    }
                    else
                    {
                        ringShape.Vertices.Add(new Vertex(controlPointShape.X, controlPointShape.Y));
                    }
                }
                ringShape.Vertices.Add(ringShape.Vertices[0]);
                newPolygonShape.OuterRing = ringShape;

                Feature newFeature = new Feature(newPolygonShape, EditShapesLayer.InternalFeatures[0].ColumnValues);

                EditShapesLayer.Open();
                EditShapesLayer.EditTools.BeginTransaction();
                EditShapesLayer.InternalFeatures.Clear();
                EditShapesLayer.InternalFeatures.Add(newFeature);
                EditShapesLayer.EditTools.CommitTransaction();
                EditShapesLayer.Close();

                IsShiftKeyDown = false;

            }
            return base.MouseUpCore(interactionArguments);
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
            controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            foreach (Feature feature in SelectedControlPointLayer.InternalFeatures)
            {
                Feature[] features = new Feature[1] { feature };
                controlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
                controlPoints.Add(feature);
            }
            foreach (Feature feature in controlPoints)
            {
                Feature[] features = new Feature[1] { feature };
                if (IsShiftKeyDown == false)
                {
                    feature.ColumnValues["nodetype"] = "normal";
                    draggedControlPointStyle.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
                }
                else
                {
                    feature.ColumnValues["nodetype"] = "special";
                    draggedControlPointStyleWithShiftKey.Draw(features, canvas, labelsInAllLayers, labelsInAllLayers);
                }
            }
        }
    }
}
