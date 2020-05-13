using System;
using System.Diagnostics;
using System.Globalization;
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
    public class TravelingSalesmanProblemAnalysis : UserControl
    {
        private static RoutingEngine routingEngine;
        private static RoutingSource routingSource;
        private static string rootPath = Samples.rootDirectory;

        public TravelingSalesmanProblemAnalysis()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            routingEngine = new RoutingEngine(routingSource, new AStarRoutingAlgorithm(), featureSource);

            RenderMap();
        }

        private void btnFindBestRoute_Click(object sender, EventArgs e)
        {
            LayerOverlay overlay = (LayerOverlay)winformsMap1.Overlays["RoutingOverlay"];
            RoutingLayer routingLayer = (RoutingLayer)overlay.Layers["RoutingLayer"];

            Stopwatch watch = new Stopwatch();
            watch.Start();
            RoutingResult routingResult = routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.StopPoints, int.Parse(txtIterations.Text));
            watch.Stop();
            // Render the route
            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingResult.Route);
            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);

            routingLayer.StopPoints.Clear();
            foreach (PointShape stop in routingResult.OrderedStops)
            {
                routingLayer.StopPoints.Add(stop);
            }
            routingLayer.ShowStopOrder = true;
            txtTimeUsed.Text = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + " ms";
            txtDistance.Text = routingResult.Distance.ToString(CultureInfo.InvariantCulture);
            winformsMap1.Refresh(overlay);
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            RoutingLayer routingLayer = new RoutingLayer();
            LayerOverlay routingOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);

            string[] startCoordinates = txtStartCoordinate.Text.Split(',');
            routingLayer.StartPoint = new PointShape(double.Parse(startCoordinates[0], CultureInfo.InvariantCulture), double.Parse(startCoordinates[1], CultureInfo.InvariantCulture));
            foreach (object item in lsbPoints.Items)
            {
                string[] coordinate = item.ToString().Split(',');
                PointShape pointNeedVisit = new PointShape(double.Parse(coordinate[0], CultureInfo.InvariantCulture), double.Parse(coordinate[1], CultureInfo.InvariantCulture));
                routingLayer.StopPoints.Add(pointNeedVisit);
            }
            routingLayer.ShowStopOrder = false;

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private TextBox txtStartCoordinate;
        private ListBox lsbPoints;
        private Button btnFindBestRoute;
        private GroupBox groupBox2;
        private Label label4;
        private TextBox txtIterations;
        private TextBox txtTimeUsed;
        private Label label6;
        private TextBox txtDistance;
        private Label label5;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TravelingSalesmanProblemAnalysis));
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimeUsed = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lsbPoints = new System.Windows.Forms.ListBox();
            this.btnFindBestRoute = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIterations = new System.Windows.Forms.TextBox();
            this.txtStartCoordinate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.txtTimeUsed);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDistance);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnFindBestRoute);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtIterations);
            this.groupBox1.Controls.Add(this.txtStartCoordinate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 402);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // txtTimeUsed
            // 
            this.txtTimeUsed.Location = new System.Drawing.Point(92, 337);
            this.txtTimeUsed.Name = "txtTimeUsed";
            this.txtTimeUsed.ReadOnly = true;
            this.txtTimeUsed.Size = new System.Drawing.Size(120, 20);
            this.txtTimeUsed.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 342);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Time Used:";
            // 
            // txtDistance
            // 
            this.txtDistance.Location = new System.Drawing.Point(92, 311);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.ReadOnly = true;
            this.txtDistance.Size = new System.Drawing.Size(120, 20);
            this.txtDistance.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 315);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Total Distance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Iterations:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lsbPoints);
            this.groupBox2.Location = new System.Drawing.Point(9, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 122);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visit Locations List";
            // 
            // lsbPoints
            // 
            this.lsbPoints.FormattingEnabled = true;
            this.lsbPoints.Items.AddRange(new object[] {
            "-96.735022,32.850551",
            "-96.826500,32.830000",
            "-96.783500,32.855100",
            "-96.780600,32.855300"});
            this.lsbPoints.Location = new System.Drawing.Point(6, 20);
            this.lsbPoints.Name = "lsbPoints";
            this.lsbPoints.Size = new System.Drawing.Size(197, 95);
            this.lsbPoints.TabIndex = 19;
            // 
            // btnFindBestRoute
            // 
            this.btnFindBestRoute.Location = new System.Drawing.Point(43, 368);
            this.btnFindBestRoute.Name = "btnFindBestRoute";
            this.btnFindBestRoute.Size = new System.Drawing.Size(153, 23);
            this.btnFindBestRoute.TabIndex = 20;
            this.btnFindBestRoute.Text = "Find The Best Route";
            this.btnFindBestRoute.UseVisualStyleBackColor = true;
            this.btnFindBestRoute.Click += new System.EventHandler(this.btnFindBestRoute_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 91);
            this.label3.TabIndex = 14; this.label3.Text = "This sample demonstrates how to generate\r\nthe optimal sequence of visiting locations,\r\nthis is the traveling salesman problem, or \r\nTSP.The iterations is a number that you can\r\nset to improve the accuracy of RoutingResult,\r\nthe bigger, the more accurate, but consume\r\nmore time.";
            // 
            // txtIterations
            // 
            this.txtIterations.Location = new System.Drawing.Point(96, 156);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(118, 20);
            this.txtIterations.TabIndex = 1;
            this.txtIterations.Text = "100";
            // 
            // txtStartCoordinate
            // 
            this.txtStartCoordinate.Enabled = false;
            this.txtStartCoordinate.Location = new System.Drawing.Point(96, 130);
            this.txtStartCoordinate.Name = "txtStartCoordinate";
            this.txtStartCoordinate.Size = new System.Drawing.Size(118, 20);
            this.txtStartCoordinate.TabIndex = 1;
            this.txtStartCoordinate.Text = "-96.736022,32.860551";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Coordinates:";
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
            // TravelingSalesmanProblemAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "TravelingSalesmanProblemAnalysis";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
