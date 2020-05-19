using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawDirectionPointStyleWithLineStyle : UserControl
    {
        public DrawDirectionPointStyleWithLineStyle()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            var vertices = new Collection<Vertex>
            {
                new Vertex(-170,-80),
                new Vertex(-105,-10),
                new Vertex(-0,0),
                new Vertex(10,40)
            };
            var vertices1 = new Collection<Vertex>
            {
                new Vertex(10,40),
                new Vertex(120,40),
                new Vertex(170,80)
            };
            var vertices2 = new Collection<Vertex>
            {
                new Vertex(-170,-80),
                new Vertex(-105,-30),
                new Vertex(-0,-20),
                new Vertex(10,20)
            };
            var vertices3 = new Collection<Vertex>
            {
                new Vertex(10,20),
                new Vertex(120,20),
                new Vertex(170,80)
            };
            var vertices4 = new Collection<Vertex>
            {
                new Vertex(-170,-80),
                new Vertex(-105,10),
                new Vertex(-0,30),
                new Vertex(10,60)
            };
            var vertices5 = new Collection<Vertex>
            {
                new Vertex(10,60),
                new Vertex(120,60),
                new Vertex(170,80)
            };

            var lineShape = new LineShape(vertices);
            var lineShape1 = new LineShape(vertices1);
            var lineShape2 = new LineShape(vertices2);
            var lineShape3 = new LineShape(vertices3);
            var lineShape4 = new LineShape(vertices4);
            var lineShape5 = new LineShape(vertices5);

            var lineStyle = new LineStyle(new GeoPen(GeoColors.Black, 16) { StartCap = DrawingLineCap.Round, EndCap = DrawingLineCap.Round }, new GeoPen(GeoColors.White, 13) { StartCap = DrawingLineCap.Round, EndCap = DrawingLineCap.Round });
            lineStyle.DirectionPointStyle = new PointStyle(new GeoImage(Convert.FromBase64String("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABIAAAAFCAYAAABIHbx0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAABOSURBVChTYzAxMSEKX758efulS5essMmBMFZBbPjKlSv/ofgo0FBfdHkUDpJighhoWAOyXhSD8GEkQw5fvXrVCV0ehYMPAw3YjTuMTBgA4r93NU8NzhcAAAAASUVORK5CYII=".Substring(22))));
            lineStyle.DrawingDirectionPoint += LineStyle_DrawingPointStyle;

            var inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = lineStyle;
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape, new Dictionary<string, string> { { "name", "test" } }));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape1, new Dictionary<string, string> { { "name", "test1" } }));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape2, new Dictionary<string, string> { { "name", "test2" } }));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape3, new Dictionary<string, string> { { "name", "test3" } }));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape4, new Dictionary<string, string> { { "name", "test4" } }));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(lineShape5, new Dictionary<string, string> { { "name", "test5" } }));

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(inMemoryFeatureLayer);

            mapView.Overlays.Add(layerOverlay);
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.Refresh();
        }

        private void LineStyle_DrawingPointStyle(object sender, DrawingDirectionPointEventArgs e)
        {
            if (e.LineFeature.ColumnValues["name"].Equals("test1"))
                e.RotationAngle = 90;

            if (e.LineFeature.ColumnValues["name"].Equals("test3"))
                e.Cancel = true;
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}