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
    public class OptimizeRoadType : UserControl
    {
        private RoutingEngine routingEngine;
        private FeatureSource featureSource;
        private static string rootPath = Samples.rootDirectory;

        public OptimizeRoadType()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();

            featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            featureSource.Open();
            RoutingSource routingSource = new RtgRoutingSource(Path.Combine(rootPath, "HighwayFirst.rtg"));
            routingEngine = new RoutingEngine(routingSource, featureSource);
            routingEngine.RoutingAlgorithm.FindingRoute += new EventHandler<FindingRouteRoutingAlgorithmEventArgs>(Algorithm_FindingPath);

            this.cbmPrioritiy.Text = "On Foot";
        }

        void cbmPrioritiy_SelectedIndexChanged(object sender, EventArgs e)
        {
            RoutingResult routingResult = routingEngine.GetRoute(txtStartId.Text, txtEndId.Text);

            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingResult.Route);

            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);
            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        void Algorithm_FindingPath(object sender, FindingRouteRoutingAlgorithmEventArgs e)
        {
            Collection<string> localRoads = new Collection<string>();
            Collection<string> highways = new Collection<string>();

            foreach (string item in e.RouteSegment.StartPointAdjacentIds)
            {
                RouteSegment road = routingEngine.RoutingSource.GetRouteSegmentByFeatureId(item);
                if (road.RouteSegmentType == 2)
                {
                    highways.Add(item);
                }
                else
                {
                    localRoads.Add(item);
                }
            }

            foreach (string item in e.RouteSegment.EndPointAdjacentIds)
            {
                RouteSegment road = routingEngine.RoutingSource.GetRouteSegmentByFeatureId(item);
                if (road.RouteSegmentType == 2)
                {
                    highways.Add(item);
                }
                else
                {
                    localRoads.Add(item);
                }
            }

            if (cbmPrioritiy.Text == "On Foot")
            {
                if (localRoads.Count > 0)
                {
                    foreach (string item in highways)
                    {
                        e.RouteSegment.StartPointAdjacentIds.Remove(item);
                        e.RouteSegment.EndPointAdjacentIds.Remove(item);
                    }
                }
            }
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
        private Label label1;
        private TextBox txtEndId;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private ComboBox cbmPrioritiy;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbmPrioritiy = new System.Windows.Forms.ComboBox();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbmPrioritiy);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 185);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Route Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 52);
            this.label4.TabIndex = 16;
            this.label4.Text = "You have two chioces,\"By Car\" is normal way,\r\nthe result of \"On Foot\" is optimize" +
                "d for walking.\r\nFor example, the route will avoid highway\r\nroads, use pedestrian" +
                " walkways.";
            // 
            // cbmPrioritiy
            // 
            this.cbmPrioritiy.FormattingEnabled = true;
            this.cbmPrioritiy.Items.AddRange(new object[] {
            "On Foot",
            "By Car"});
            this.cbmPrioritiy.Location = new System.Drawing.Point(90, 146);
            this.cbmPrioritiy.Name = "cbmPrioritiy";
            this.cbmPrioritiy.Size = new System.Drawing.Size(99, 21);
            this.cbmPrioritiy.TabIndex = 14;
            this.cbmPrioritiy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbmPrioritiy.SelectedIndexChanged += new EventHandler(cbmPrioritiy_SelectedIndexChanged);
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(90, 92);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(99, 20);
            this.txtStartId.TabIndex = 12;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start FeatureId:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(90, 120);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(99, 20);
            this.txtEndId.TabIndex = 8;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "End FeatureId:";
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
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
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
            // OptimizeRoadType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "OptimizeRoadType";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
