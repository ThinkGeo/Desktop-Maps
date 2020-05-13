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
    public class RouteOnlyInASpecificArea : UserControl
    {
        private FeatureSource featureSource;
        private PolygonShape allowArea;
        private RoutingEngine routingEngine;
        private Collection<string> allowFeatureIds;
        private static string rootPath = Samples.rootDirectory;

        public RouteOnlyInASpecificArea()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            allowArea = new PolygonShape(txtAvoidWkt.Text);
            featureSource.Open();
            Collection<Feature> features = featureSource.SpatialQuery(allowArea, QueryType.Within, ReturningColumnsType.NoColumns);
            featureSource.Close();
            allowFeatureIds = new Collection<string>();
            foreach (Feature item in features)
            {
                allowFeatureIds.Add(item.Id);
            }
            RoutingSource routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            routingEngine = new RoutingEngine(routingSource, new AStarRoutingAlgorithm(), featureSource);

            RenderMap();
        }

        void chkIsAvoidArea_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsAvoidArea.Checked)
            {
                routingEngine.RoutingAlgorithm.FindingRoute += new EventHandler<FindingRouteRoutingAlgorithmEventArgs>(Algorithm_FindingPath);
            }
            else
            {
                routingEngine.RoutingAlgorithm.FindingRoute -= new EventHandler<FindingRouteRoutingAlgorithmEventArgs>(Algorithm_FindingPath);
            }
            Route();
        }

        private void Algorithm_FindingPath(object sender, FindingRouteRoutingAlgorithmEventArgs e)
        {
            Collection<string> beContainedFeatureIds = new Collection<string>();

            featureSource.Open();
            Collection<string> startPointAdjacentIds = e.RouteSegment.StartPointAdjacentIds;
            Collection<string> endPointAdjacentIds = e.RouteSegment.EndPointAdjacentIds;
            featureSource.Close();

            foreach (string id in startPointAdjacentIds)
            {
                if (!allowFeatureIds.Contains(id))
                {
                    beContainedFeatureIds.Add(id);
                }
            }
            foreach (string id in endPointAdjacentIds)
            {
                if (!allowFeatureIds.Contains(id))
                {
                    beContainedFeatureIds.Add(id);
                }
            }

            // Remove the ones that be contained in the avoidable area
            foreach (string id in beContainedFeatureIds)
            {
                if (e.RouteSegment.StartPointAdjacentIds.Contains(id))
                {
                    e.RouteSegment.StartPointAdjacentIds.Remove(id);
                }
                if (e.RouteSegment.EndPointAdjacentIds.Contains(id))
                {
                    e.RouteSegment.EndPointAdjacentIds.Remove(id);
                }
            }
        }

        private void Route()
        {
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

            ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            streetsLayer.Open();

            RoutingLayer routingLayer = new RoutingLayer();
            streetsLayer.Open();
            routingLayer.StartPoint = streetsLayer.FeatureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = streetsLayer.FeatureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            streetsLayer.Close();
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer avoidableAreaLayer = new InMemoryFeatureLayer();
            avoidableAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColor.StandardColors.Blue));
            avoidableAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            avoidableAreaLayer.InternalFeatures.Add("avoidableArea", new Feature(allowArea));
            routingOverlay.Layers.Add("avoidableAreaLayer", avoidableAreaLayer);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            Route();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndId;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private TextBox txtAvoidWkt;
        private Label label4;
        private CheckBox chkIsAvoidArea;
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsAvoidArea = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAvoidWkt = new System.Windows.Forms.TextBox();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsAvoidArea);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAvoidWkt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(497, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 247);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // chkIsAvoidArea
            // 
            this.chkIsAvoidArea.AutoSize = true;
            this.chkIsAvoidArea.Location = new System.Drawing.Point(83, 219);
            this.chkIsAvoidArea.Name = "chkIsAvoidArea";
            this.chkIsAvoidArea.Size = new System.Drawing.Size(100, 17);
            this.chkIsAvoidArea.TabIndex = 21;
            this.chkIsAvoidArea.Text = "Specify The Area";
            this.chkIsAvoidArea.UseVisualStyleBackColor = true;
            this.chkIsAvoidArea.CheckedChanged += new System.EventHandler(this.chkIsAvoidArea_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 39);
            this.label4.TabIndex = 16;
            this.label4.Text = "The sample demonstrates how to route only in \r\na specific area when finding the shortest path \r\nby FindingPath event.";
            // 
            // txtAvoidWkt
            // 
            this.txtAvoidWkt.Enabled = false;
            this.txtAvoidWkt.Location = new System.Drawing.Point(6, 93);
            this.txtAvoidWkt.Multiline = true;
            this.txtAvoidWkt.Name = "txtAvoidWkt";
            this.txtAvoidWkt.Size = new System.Drawing.Size(229, 60);
            this.txtAvoidWkt.TabIndex = 15;
            this.txtAvoidWkt.Text = "POLYGON((-96.74534 32.87589,-96.72522 32.86075,-96.84560 32.74416,-96.86297 32.76204,-96.8497313274053 32.77391,-96.74534 32.87589))";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "The WKT of allow area:";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(85, 160);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(136, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "StartFeatureId:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(85, 188);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(136, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "EndFeatureId:";
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
            // RouteAvoidingCertainArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "RouteAvoidingCertainArea";
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
