using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.RoutingSamples
{
    public class ExtendingRoutingSource : UserControl
    {
        private static string rootPath = Samples.rootDirectory;

        public ExtendingRoutingSource()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            RoutingSource routingSource = new CustomRoutingSource(featureSource);
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);
            RoutingResult routingResult = routingEngine.GetRoute(txtStartId.Text, txtEndId.Text);

            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingResult.Route);

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            ShapeFileFeatureLayer dallasStreetsLayer = new ShapeFileFeatureLayer(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            dallasStreetsLayer.Open();

            RoutingLayer routingLayer = new RoutingLayer();
            dallasStreetsLayer.Open();
            routingLayer.StartPoint = dallasStreetsLayer.FeatureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = dallasStreetsLayer.FeatureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            dallasStreetsLayer.Close();
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnRoute;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndId;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendingRoutingSource));
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.btnRoute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(204, 234);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(0, 13);
            this.lblCoordinate.TabIndex = 6;
            this.lblCoordinate.Visible = false;
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(81, 180);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(130, 23);
            this.btnRoute.TabIndex = 7;
            this.btnRoute.Text = "Get Shortest Path";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 212);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 78);
            this.label3.TabIndex = 14;
            this.label3.Text = "This sample demonstrates how to extend the\r\nRoutingSource class. We will create a routing\r\nsource that queries a shape file in real-time \r\ninstead of using an optimized routing index. \r\nthis is going to be much slower but shows \r\nthe flexibility of the struture.";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(92, 124);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(119, 20);
            this.txtStartId.TabIndex = 12;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Feature Id:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(92, 153);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(119, 20);
            this.txtEndId.TabIndex = 8;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "End Feature Id:";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            // 
            // ExtendingRoutingSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ExtendingRoutingSource";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }

    /// <summary>
    /// It shows how to exploler custom RoutingSource
    /// </summary>
    public class CustomRoutingSource : RoutingSource
    {
        private FeatureSource featureSource;

        public CustomRoutingSource()
            : this(null)
        {
        }

        public CustomRoutingSource(FeatureSource featureSource)
            : base()
        {
            this.featureSource = featureSource;
        }

        protected override int GetRouteSegmentCountCore()
        {
            return featureSource.GetCount();
        }

        protected override void OpenCore()
        {
            base.OpenCore();
            featureSource.Open();
        }

        protected override void CloseCore()
        {
            base.CloseCore();
            featureSource.Close();
        }

        protected override RouteSegment GetRouteSegmentByFeatureIdCore(string roadId)
        {
            Feature feature = featureSource.GetFeatureById(roadId, ReturningColumnsType.NoColumns);

            LineBaseShape shape = feature.GetShape() as LineBaseShape;
            LineShape sourceShape = ConvertLineBaseShapeToLines(shape)[0];
            sourceShape.Id = feature.Id;
            Collection<string> startIds = new Collection<string>();
            Collection<string> endIds = new Collection<string>();
            GetAdjacentFeataureIds(featureSource, sourceShape, startIds, endIds);
            PointShape startPoint = new PointShape(sourceShape.Vertices[0]);
            PointShape endPoint = new PointShape(sourceShape.Vertices[sourceShape.Vertices.Count - 1]);
            float length = (float)sourceShape.GetLength(GeographyUnit.Meter, DistanceUnit.Meter);
            RouteSegment road = new RouteSegment(feature.Id, 0, length, startPoint, startIds, endPoint, endIds);

            return road;
        }

        private static void GetAdjacentFeataureIds(FeatureSource featureSource, LineShape sourceShape, Collection<string> startIds, Collection<string> endIds)
        {
            Vertex startVertex = sourceShape.Vertices[0];
            Vertex endVertex = sourceShape.Vertices[sourceShape.Vertices.Count - 1];
            Collection<Feature> tempfeatures = featureSource.GetFeaturesInsideBoundingBox(sourceShape.GetBoundingBox(), ReturningColumnsType.NoColumns);
            foreach (Feature tempFeature in tempfeatures)
            {
                LineShape tempShape = ((MultilineShape)tempFeature.GetShape()).Lines[0];
                if (sourceShape.Id == tempFeature.Id)
                {
                    continue;
                }

                if (tempShape.Vertices.Contains(startVertex))
                {
                    startIds.Add(tempFeature.Id);
                }
                else if (tempShape.Vertices.Contains(endVertex))
                {
                    endIds.Add(tempFeature.Id);
                }
            }
        }

        private static Collection<LineShape> ConvertLineBaseShapeToLines(LineBaseShape sourceShape)
        {
            Collection<LineShape> lines = null;
            LineShape lineShape = sourceShape as LineShape;
            if (lineShape != null)
            {
                lines = new Collection<LineShape>();
                lines.Add(lineShape);
            }
            else
            {
                lines = ((MultilineShape)sourceShape).Lines;
            }
            return lines;
        }
    }
}
