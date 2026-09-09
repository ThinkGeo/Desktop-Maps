using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Three editing rules an application enforces from the overlay events, with no
    /// custom overlay: vertices keep a minimum spacing (VertexAdding.Cancel), a shape
    /// tagged as a rectangle stays one when a corner is dragged (rebuild on
    /// VertexMoved), and a dragged vertex can be re-placed at typed exact coordinates.
    /// </summary>
    public partial class EditWithConstraints
    {
        // A vertex may not land closer than this to its neighbors, drawn or edited.
        private const double MinSpacingMeters = 300;

        private bool _initialized;
        private Vertex _lastMovedVertex;
        private string _lastMovedFeatureId;

        public EditWithConstraints()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));

            Map.CenterPoint = new PointShape(-10776500, 3912800);
            Map.CurrentScale = 77000;

            // The rectangle carries its rule as data, so the VertexMoved handler
            // can tell it apart from the free-form polygon next to it.
            var rectangle = new Feature("POLYGON((-10778500 3915600,-10774040 3915600,-10774040 3912400,-10778500 3912400,-10778500 3915600))");
            rectangle.ColumnValues["constraint"] = "rectangle";
            var polygon = new Feature("POLYGON((-10778300 3911600,-10775300 3911800,-10774100 3909900,-10776900 3909200,-10778300 3911600))");
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(rectangle.Id, rectangle);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(polygon.Id, polygon);
            Map.EditOverlay.CalculateAllControlPoints();

            Map.EditOverlay.VertexAdding += EditOverlay_VertexAdding;
            Map.EditOverlay.VertexMoved += EditOverlay_VertexMoved;
            Map.TrackOverlay.VertexAdding += TrackOverlay_VertexAdding;

            Instructions.Text =
                "Edit Shapes Mode — The upper shape is tagged as a rectangle: drag any corner and its two neighbors follow. " +
                "Click a segment to add a vertex; the click is rejected within 300 m of an existing vertex. " +
                "After dragging a vertex, its coordinates appear on the right — correct them and click Apply to place it exactly.";

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Minimum spacing while EDITING: a vertex added on a segment is rejected
        /// when it lands too close to any vertex the shape already has.
        /// </summary>
        private void EditOverlay_VertexAdding(object sender, VertexAddingEditInteractiveOverlayEventArgs e)
        {
            foreach (var vertex in VerticesOf(e.AffectedFeature.GetShape()))
            {
                if (Distance(vertex, e.AddingVertex) < MinSpacingMeters)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Minimum spacing while DRAWING: the shape being tracked already contains a
        /// rubber-band vertex at the click position, so the check walks back to the
        /// last vertex that is NOT the click and measures against that one.
        /// </summary>
        private void TrackOverlay_VertexAdding(object sender, VertexAddingTrackInteractiveOverlayEventArgs e)
        {
            Vertex? previous = null;
            foreach (var vertex in VerticesOf(e.AffectedFeature.GetShape()))
            {
                if (Distance(vertex, e.AddingVertex) > 1e-9)
                {
                    previous = vertex;
                }
            }

            if (previous.HasValue && Distance(previous.Value, e.AddingVertex) < MinSpacingMeters)
            {
                e.Cancel = true;
            }
        }

        private void EditOverlay_VertexMoved(object sender, VertexMovedEditInteractiveOverlayEventArgs e)
        {
            _lastMovedVertex = e.MovedVertex;
            _lastMovedFeatureId = e.AffectedFeature.Id;
            VertexX.Text = e.MovedVertex.X.ToString("0.##", CultureInfo.InvariantCulture);
            VertexY.Text = e.MovedVertex.Y.ToString("0.##", CultureInfo.InvariantCulture);

            if (e.AffectedFeature.ColumnValues.TryGetValue("constraint", out var rule) && rule == "rectangle")
            {
                RestoreRectangle(e.AffectedFeature, e.MovedVertex);
            }
        }

        /// <summary>
        /// Re-places the last dragged vertex at the typed coordinates — dragging gets
        /// the vertex near, the numbers put it exactly where the survey says.
        /// </summary>
        private void ApplyCoordinates_Click(object sender, RoutedEventArgs e)
        {
            if (_lastMovedFeatureId == null ||
                !double.TryParse(VertexX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var x) ||
                !double.TryParse(VertexY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var y) ||
                !Map.EditOverlay.EditShapesLayer.InternalFeatures.Contains(_lastMovedFeatureId))
            {
                return;
            }

            var feature = Map.EditOverlay.EditShapesLayer.InternalFeatures[_lastMovedFeatureId];
            var shape = feature.GetShape();
            MoveNearestVertex(shape, _lastMovedVertex, new Vertex(x, y));
            _lastMovedVertex = new Vertex(x, y);
            ReplaceEditShape(feature, shape);

            if (feature.ColumnValues.TryGetValue("constraint", out var rule) && rule == "rectangle")
            {
                RestoreRectangle(Map.EditOverlay.EditShapesLayer.InternalFeatures[_lastMovedFeatureId], _lastMovedVertex);
            }
        }

        /// <summary>
        /// The rectangle rule: the dragged corner and the corner opposite it define
        /// the shape; the two corners between them are recomputed. The opposite
        /// corner is found by elimination — of the three corners that did not move,
        /// the two ADJACENT ones share no coordinate with each other.
        /// </summary>
        private void RestoreRectangle(Feature feature, Vertex movedTo)
        {
            if (feature.GetShape() is not PolygonShape polygon)
            {
                return;
            }

            var corners = new List<Vertex>();
            foreach (var vertex in VerticesOf(polygon))
            {
                corners.Add(vertex);
            }

            if (corners.Count != 4)
            {
                return;
            }

            var movedIndex = 0;
            for (var i = 1; i < 4; i++)
            {
                if (Distance(corners[i], movedTo) < Distance(corners[movedIndex], movedTo))
                {
                    movedIndex = i;
                }
            }

            var others = new List<Vertex>();
            for (var i = 0; i < 4; i++)
            {
                if (i != movedIndex)
                {
                    others.Add(corners[i]);
                }
            }

            var opposite = others[0];
            if (!SharesAxis(others[0], others[1])) opposite = others[2];
            else if (!SharesAxis(others[0], others[2])) opposite = others[1];

            var moved = corners[movedIndex];
            if (Math.Abs(moved.X - opposite.X) < 1e-6 || Math.Abs(moved.Y - opposite.Y) < 1e-6)
            {
                return;
            }

            var ring = new RingShape();
            ring.Vertices.Add(moved);
            ring.Vertices.Add(new Vertex(moved.X, opposite.Y));
            ring.Vertices.Add(opposite);
            ring.Vertices.Add(new Vertex(opposite.X, moved.Y));
            ring.Vertices.Add(moved);
            ReplaceEditShape(feature, new PolygonShape(ring));
        }

        private void ReplaceEditShape(Feature original, BaseShape shape)
        {
            shape.Id = original.Id;
            var replacement = new Feature(shape);
            foreach (var column in original.ColumnValues)
            {
                replacement.ColumnValues[column.Key] = column.Value;
            }

            Map.EditOverlay.EditShapesLayer.InternalFeatures.Remove(original.Id);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(replacement.Id, replacement);
            Map.EditOverlay.CalculateAllControlPoints();
            _ = Map.RefreshAsync(Map.EditOverlay);
        }

        private static void MoveNearestVertex(BaseShape shape, Vertex from, Vertex to)
        {
            switch (shape)
            {
                case PolygonShape polygon:
                    MoveNearest(polygon.OuterRing.Vertices, from, to, closedRing: true);
                    break;
                case LineShape line:
                    MoveNearest(line.Vertices, from, to, closedRing: false);
                    break;
            }
        }

        private static void MoveNearest(IList<Vertex> vertices, Vertex from, Vertex to, bool closedRing)
        {
            var nearest = 0;
            for (var i = 1; i < vertices.Count; i++)
            {
                if (Distance(vertices[i], from) < Distance(vertices[nearest], from))
                {
                    nearest = i;
                }
            }

            vertices[nearest] = to;
            // A ring's first and last vertex are the same point; moving one end moves both.
            if (closedRing && nearest == 0)
            {
                vertices[vertices.Count - 1] = to;
            }
            else if (closedRing && nearest == vertices.Count - 1)
            {
                vertices[0] = to;
            }
        }

        /// <summary>The shape's vertices, without the ring-closing duplicate.</summary>
        private static IEnumerable<Vertex> VerticesOf(BaseShape shape)
        {
            switch (shape)
            {
                case PolygonShape polygon:
                    var ring = polygon.OuterRing.Vertices;
                    var count = ring.Count > 1 && Distance(ring[0], ring[ring.Count - 1]) < 1e-9
                        ? ring.Count - 1
                        : ring.Count;
                    for (var i = 0; i < count; i++)
                    {
                        yield return ring[i];
                    }

                    break;
                case LineShape line:
                    foreach (var vertex in line.Vertices)
                    {
                        yield return vertex;
                    }

                    break;
            }
        }

        private static bool SharesAxis(Vertex a, Vertex b)
        {
            return Math.Abs(a.X - b.X) < 1e-6 || Math.Abs(a.Y - b.Y) < 1e-6;
        }

        private static double Distance(Vertex a, Vertex b)
        {
            return Math.Sqrt(((a.X - b.X) * (a.X - b.X)) + ((a.Y - b.Y) * (a.Y - b.Y)));
        }

        private void EditShapes_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            // Whatever was drawn joins the editable set.
            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
            {
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            }

            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.EditOverlay.CalculateAllControlPoints();
            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay });

            Instructions.Text =
                "Edit Shapes Mode — The upper shape is tagged as a rectangle: drag any corner and its two neighbors follow. " +
                "Click a segment to add a vertex; the click is rejected within 300 m of an existing vertex. " +
                "After dragging a vertex, its coordinates appear on the right — correct them and click Apply to place it exactly.";
        }

        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;
            Instructions.Text =
                "Draw Polygon Mode — Click to add vertices; a click within 300 m of the previous vertex is rejected. " +
                "Double-click to finish, Esc to cancel, right-click to undo the last vertex. " +
                "Switch back to Edit Shapes to edit the result.";
        }
    }
}
