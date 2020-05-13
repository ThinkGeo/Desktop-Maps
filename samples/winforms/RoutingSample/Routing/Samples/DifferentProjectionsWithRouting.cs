using System;
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
    public class DifferentProjectionsWithRouting : UserControl
    {
        private static RoutingEngine routingEngine;
        private static RoutingSource RoutingSourceForShortest;
        private static RoutingSource RoutingSourceForFastest;
        private static string rootPath = Samples.rootDirectory;

        public DifferentProjectionsWithRouting()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-3857.shp"));

            RoutingSourceForShortest = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-3857.shortest.rtg"));
            RoutingSourceForFastest = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-3857.fastest.rtg"));
            routingEngine = new RoutingEngine(RoutingSourceForShortest, featureSource);
            routingEngine.GeographyUnit = GeographyUnit.Meter;

            RenderMap();

            FindPath();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtShortest.Checked)
            {
                routingEngine.RoutingSource = RoutingSourceForShortest;
            }
            else
            {
                routingEngine.RoutingSource = RoutingSourceForFastest;
            }

            FindPath();
        }

        private void FindPath()
        {
            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];

            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingEngine.GetRoute(txtStartId.Text, txtEndId.Text).Route);

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-10787478.039515, 3885514.4616168, -10759196.432323, 3861498.009642);

            GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay();
            googleMapsOverlay.TileCache = new FileBitmapTileCache("C:\\ImageCache");
            winformsMap1.Overlays.Add(googleMapsOverlay);

            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-3857.shp"));
            featureSource.Open();
            RoutingLayer routingLayer = new RoutingLayer();
            routingLayer.StartPoint = featureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = featureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-10806916.7603168, 3897094.80494128, -10738458.3904247, 3827820.76830281)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            winformsMap1.Refresh();
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
        private RadioButton rbtFastest;
        private RadioButton rbtShortest;
        private GroupBox groupBox2;
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtShortest = new System.Windows.Forms.RadioButton();
            this.rbtFastest = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 223);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtShortest);
            this.groupBox2.Controls.Add(this.rbtFastest);
            this.groupBox2.Location = new System.Drawing.Point(6, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 60);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Find The Path";
            // 
            // rbtShortest
            // 
            this.rbtShortest.AutoSize = true;
            this.rbtShortest.Checked = true;
            this.rbtShortest.Location = new System.Drawing.Point(16, 27);
            this.rbtShortest.Name = "rbtShortest";
            this.rbtShortest.Size = new System.Drawing.Size(64, 17);
            this.rbtShortest.TabIndex = 15;
            this.rbtShortest.TabStop = true;
            this.rbtShortest.Text = "Shortest";
            this.rbtShortest.UseVisualStyleBackColor = true;
            this.rbtShortest.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbtFastest
            // 
            this.rbtFastest.AutoSize = true;
            this.rbtFastest.Location = new System.Drawing.Point(120, 27);
            this.rbtFastest.Name = "rbtFastest";
            this.rbtFastest.Size = new System.Drawing.Size(62, 17);
            this.rbtFastest.TabIndex = 16;
            this.rbtFastest.Text = "Fastest ";
            this.rbtFastest.UseVisualStyleBackColor = true;
            this.rbtFastest.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 65);
            this.label3.TabIndex = 14;
            this.label3.Text = "The sample demonstrates how to get route\r\n under Google projection.Please select radio \r\nbutton below to" +
                " get the shortestor fastest \r\npath between the start and endroads whose\r\n featur" +
                "e ids are set as below.";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(85, 98);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(130, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "StartFeatureId:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(85, 127);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(130, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 130);
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
            // DifferentProjectionsWithRouting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DifferentProjectionsWithRouting";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
