using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace PresetVertexToTrackedPolygon
{
    //Custom TrackInteractiveOverlay to add preset vertices to polygon while tracking
    class PresetVertexTrackInteractiveOverlay : TrackInteractiveOverlay
    {
        private Collection<PointShape> presetPointShapes = new Collection<PointShape>();
        private Collection<int> addVertexIndexes = new Collection<int>();
        private PointShape newAddedVertex = null;

        public Collection<PointShape> PresetPointShapes
        {
            get { return presetPointShapes; }
            set { presetPointShapes = value; }
        }

        public Collection<int> AddVertexIndexes
        {
            get { return addVertexIndexes; }
            set { addVertexIndexes = value; }
        }

        public PointShape NewAddedVertex
        {
            get { return newAddedVertex; }
            set { newAddedVertex = value; }
        }

        protected override void OnTrackEnded(TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            addVertexIndexes = new Collection<int>();
            presetPointShapes = new Collection<PointShape>();
            base.OnTrackEnded(e);
        }

        protected override BaseShape GetTrackingShapeCore()
        {
            BaseShape baseShape = base.GetTrackingShapeCore();
            if (baseShape is PolygonShape)
            {
                PolygonShape polygon = (PolygonShape)baseShape;
                if (polygon != null)
                {
                    if (newAddedVertex != null)
                    {
                        AddVertexIndexes.Add(Vertices.Count - 2);
                        PresetPointShapes.Add(new PointShape(newAddedVertex.X, newAddedVertex.Y));
                        newAddedVertex = null;
                    }

                    int index = 0;
                    foreach (int addVertexIndex in AddVertexIndexes)
                    {
                        PointShape presetPointShape = presetPointShapes[index];
                        index++;

                        if (presetPointShape != null)
                        {
                            Vertices[addVertexIndex] = new Vertex(presetPointShape);
                        }
                    }
                }
            }
            return baseShape;
        }

        protected override InteractiveResult MouseClickCore(InteractionArguments interactionArguments)
        {
            if (interactionArguments.MouseButton == MapMouseButton.Left)
            {
                return base.MouseClickCore(interactionArguments);
            }
            else
            {
                return new InteractiveResult();
            }
        }
    }
}
