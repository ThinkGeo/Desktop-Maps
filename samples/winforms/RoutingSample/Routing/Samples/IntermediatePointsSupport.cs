using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.RoutingSamples
{
    public class IntermediatePointsSupport : UserControl
    {
        private static string rootPath = Samples.rootDirectory;

        public IntermediatePointsSupport()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void cbxAddStop_CheckedChanged(object sender, EventArgs e)
        {
            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            Route();
            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        private void Route()
        {
            FeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            RoutingSource routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);

            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            Collection<LineShape> paths = new Collection<LineShape>();
            if (cbxAddStop.Checked)
            {
                paths.Add(routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.StopPoints[0]).Route);
                paths.Add(routingEngine.GetRoute(routingLayer.StopPoints[0], routingLayer.EndPoint).Route);
            }
            else
            {
                paths.Add(routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.EndPoint).Route);
            }

            RoutingLayer inmemoryLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            inmemoryLayer.Routes.Clear();
            foreach (LineShape item in paths)
            {
                inmemoryLayer.Routes.Add(item);
            }
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            RoutingLayer routingLayer = new RoutingLayer();
            string[] startCoordinates = txtStartPoint.Text.Split(',');
            PointShape startPoint = new PointShape(double.Parse(startCoordinates[0], CultureInfo.InvariantCulture), double.Parse(startCoordinates[1], CultureInfo.InvariantCulture));
            string[] endCoordinates = txtEndPoint.Text.Split(',');
            PointShape endPoint = new PointShape(double.Parse(endCoordinates[0], CultureInfo.InvariantCulture), double.Parse(endCoordinates[1], CultureInfo.InvariantCulture));
            routingLayer.StartPoint = startPoint;
            routingLayer.EndPoint = endPoint;
            string[] stopCoordinates = txtStopPoint.Text.Split(',');
            PointShape stopPoint = new PointShape(double.Parse(stopCoordinates[0], CultureInfo.InvariantCulture), double.Parse(stopCoordinates[1], CultureInfo.InvariantCulture));
            routingLayer.StopPoints.Add(stopPoint);
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            Route();

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndPoint;
        private TextBox txtStartPoint;
        private Label label2;
        private WinformsMap winformsMap1;
        private CheckBox cbxAddStop;
        private Label label3;
        private TextBox txtStopPoint;
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxAddStop = new System.Windows.Forms.CheckBox();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStopPoint = new System.Windows.Forms.TextBox();
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
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxAddStop);
            this.groupBox1.Controls.Add(this.txtStartPoint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndPoint);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtStopPoint);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 204);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Intermediate Point:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 39);
            this.label3.TabIndex = 15;
            this.label3.Text = "This sample demonstrates how to find the\r\nshortest path consisting of a start, en" +
                "d and \r\nintermediate points.";
            // 
            // cbxAddStop
            // 
            this.cbxAddStop.AutoSize = true;
            this.cbxAddStop.Location = new System.Drawing.Point(57, 173);
            this.cbxAddStop.Name = "cbxAddStop";
            this.cbxAddStop.Size = new System.Drawing.Size(159, 17);
            this.cbxAddStop.TabIndex = 14;
            this.cbxAddStop.Text = "Route Via Intermediate Stop";
            this.cbxAddStop.UseVisualStyleBackColor = true;
            this.cbxAddStop.CheckedChanged += new System.EventHandler(this.cbxAddStop_CheckedChanged);
            // 
            // txtStartId
            // 
            this.txtStartPoint.Enabled = false;
            this.txtStartPoint.Location = new System.Drawing.Point(100, 82);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.Size = new System.Drawing.Size(116, 20);
            this.txtStartPoint.TabIndex = 12;
            this.txtStartPoint.Text = "-96.736022,32.860551";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Point:";
            // 
            // txtEndId
            // 
            this.txtEndPoint.Enabled = false;
            this.txtEndPoint.Location = new System.Drawing.Point(100, 141);
            this.txtEndPoint.Name = "txtEndPoint";
            this.txtEndPoint.Size = new System.Drawing.Size(116, 20);
            this.txtEndPoint.TabIndex = 8;
            this.txtEndPoint.Text = "-96.842102,32.755379";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "End Point:";
            // 
            // txtStopId
            // 
            this.txtStopPoint.Enabled = false;
            this.txtStopPoint.Location = new System.Drawing.Point(100, 112);
            this.txtStopPoint.Name = "txtStopPoint";
            this.txtStopPoint.Size = new System.Drawing.Size(116, 20);
            this.txtStopPoint.TabIndex = 8;
            this.txtStopPoint.Text = "-96.826500,32.830000";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality =DrawingQuality.Default;
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
            // IntermediatePointsSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "IntermediatePointsSupport";
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
