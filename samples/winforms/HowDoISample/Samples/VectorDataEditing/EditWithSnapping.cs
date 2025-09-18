using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Interaction logic for EditWithSnapping.cs
    /// </summary>
    public partial class EditWithSnapping : UserControl
    {
        private bool _initialized;
        private const float Tolerance = 25;
        private ShapeFileFeatureLayer _parksLayer;

        public EditWithSnapping()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (_initialized)
                return;
            _initialized = true;

            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load the Frisco data to a layer
            _parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Schools.shp");
            _parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TolerancePointStyle(Tolerance));
            _parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project the layer's data from its native projection (2276) to spherical mercator(3857)
            _parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add(_parksLayer);
            mapView.Overlays.Add("Parks Overlay", inMemoryOverlay);

            mapView.EditOverlay.VertexMoving += EditOverlay_VertexMoving;

            var lineShape = new LineShape();
            lineShape.Vertices.Add(new Vertex(-10783003, 3918370));
            lineShape.Vertices.Add(new Vertex(-10783070, 3917335));
            lineShape.Vertices.Add(new Vertex(-10781292, 3916438));
            mapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(new Feature(lineShape));
            
            mapView.EditOverlay.CalculateAllControlPoints();

            // Add this so the edit overlay is drawn immediately
            mapView.Overlays.Add("Edit Overlay", mapView.EditOverlay);

            mapView.CenterPoint = new PointShape(-10782870, 3917470);
            mapView.CurrentScale = 30000;

            _ = mapView.RefreshAsync();
        }

        private void EditOverlay_VertexMoving(object sender, VertexMovingEditInteractiveOverlayEventArgs e)
        {
            var toSnapInMemoryFeatures = _parksLayer.QueryTools.GetFeaturesNearestTo(e.TargetVertex, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);

            if (toSnapInMemoryFeatures.Count == 0)
                return;

            var toSnapPointShape = toSnapInMemoryFeatures[0].GetShape() as PointShape;
            var screenDistance = MapUtil.GetScreenDistanceBetweenTwoWorldPoints(
                mapView.CurrentExtent, toSnapPointShape, e.TargetVertex,
                (float)mapView.MapWidth, (float)mapView.MapHeight);

            if (screenDistance >= Tolerance) return;
            if (toSnapPointShape == null) return;

            e.TargetVertex.X = toSnapPointShape.X;
            e.TargetVertex.Y = toSnapPointShape.Y;
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(1202, 611);
            mapView.TabIndex = 0;
            // 
            // EditWithSnapping
            // 
            Controls.Add(mapView);
            Name = "EditWithSnapping";
            Size = new System.Drawing.Size(1202, 611);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }

    internal class TolerancePointStyle : PointStyle
    {
        public float Tolerance { get; set; }

        public TolerancePointStyle(float tolerance)
        {
            Tolerance = tolerance;
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas,
            Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                // Draws the vertex.
                var pointShape = (PointShape)feature.GetShape();
                canvas.DrawEllipse(pointShape, 5, 5, new GeoSolidBrush(GeoColors.Black), DrawingLevel.LevelOne);

                // Draws the tolerance circle.
                var screenPointF = MapUtil.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
                canvas.DrawEllipse(screenPointF, Tolerance * 2, Tolerance * 2, new GeoPen(GeoColors.Black),
                    new GeoSolidBrush(), DrawingLevel.LevelFour, 0, 0, PenBrushDrawingOrder.PenFirst);
            }
        }
    }
}
