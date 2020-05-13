using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;
using System.Globalization;
using System.IO;


namespace HowDoISamples
{
    public class UseMultiShapeFiles : UserControl
    {
        public UseMultiShapeFiles()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnGenerateRoadData_Click(object sender, EventArgs e)
        {
            MultipleShapeFileFeatureLayer.BuildIndex(@"..\..\SampleData\testdata?.shp", BuildIndexMode.DoNotRebuild);
            MultipleShapeFileFeatureSource featureSource = new MultipleShapeFileFeatureSource(@"..\..\SampleData\testdata?.shp", @"..\..\SampleData\testdata?.midx");
            Collection<string> shapePathFileNames = featureSource.GetShapePathFileNames();
            string fileNames = String.Empty;
            foreach (string fileName in shapePathFileNames)
            {
                fileNames += Path.GetFileName(fileName) + ",";
            }
            RtgRoutingSource.GenerateRoutingData(@"..\..\SampleData\testdata.rtg", featureSource, fileNames.TrimEnd(','), BuildRoutingDataMode.DoNotRebuild);
            MessageBox.Show("Finish building routing data!");
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            MultipleShapeFileFeatureSource featureSource = new MultipleShapeFileFeatureSource(@"..\..\SampleData\testdata?.shp", @"..\..\SampleData\testdata?.midx");
            RoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\testdata.rtg");
            RoutingEngine routingEngine = new RoutingEngine(routingSource);
            RoutingResult routingResult = routingEngine.GetShortestPath(featureSource, txtStartId.Text, txtEndId.Text);

            InMemoryFeatureLayer routingLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            winformsMap1.Overlays["RoutingOverlay"].Lock.EnterWriteLock();
            try
            {
                routingLayer.InternalFeatures.Clear();
                foreach (Feature feature in routingResult.Features)
                {
                    routingLayer.InternalFeatures.Add(feature);
                }
            }
            finally
            {
                winformsMap1.Overlays["RoutingOverlay"].Lock.ExitWriteLock();
            }

            winformsMap1.Refresh();
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-119.97901851416, 58.8754168925781, -119.72495967627, 58.6941424785156);

            ShapeFileFeatureLayer testdata0 = new ShapeFileFeatureLayer(@"..\..\SampleData\testdata0.shp");
            testdata0.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 7), new GeoPen(GeoColor.StandardColors.White, 4));
            testdata0.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay testdata0Overlay = new LayerOverlay();
            testdata0Overlay.Layers.Add("testdata0", testdata0);
            winformsMap1.Overlays.Add("testdata0Overlay", testdata0Overlay);

            ShapeFileFeatureLayer testdata1 = new ShapeFileFeatureLayer(@"..\..\SampleData\testdata1.shp");
            testdata1.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 7), new GeoPen(GeoColor.StandardColors.White, 4));
            testdata1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay testdata1Overlay = new LayerOverlay();
            testdata1Overlay.Layers.Add("testdata1", testdata1);
            winformsMap1.Overlays.Add("testdata1Overlay", testdata1Overlay);

            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(100, GeoColor.StandardColors.Purple), 5);
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            winformsMap1.Refresh();
        }

        private void chkTestdata1_CheckedChanged(object sender, EventArgs e)
        {
            winformsMap1.Overlays["testdata0Overlay"].Lock.EnterWriteLock();
            winformsMap1.Overlays["testdata1Overlay"].Lock.EnterWriteLock();
            try
            {
                winformsMap1.Overlays["testdata0Overlay"].IsVisible = chkTestdata0.Checked;
                winformsMap1.Overlays["testdata1Overlay"].IsVisible = chkTestdata1.Checked;
            }
            finally
            {
                winformsMap1.Overlays["testdata0Overlay"].Lock.ExitWriteLock();
                winformsMap1.Overlays["testdata1Overlay"].Lock.ExitWriteLock();
            }

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
        private Button btnGenerateRoadData;
        private GroupBox groupBox2;
        private CheckBox chkTestdata1;
        private CheckBox chkTestdata0;
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
            this.btnRoute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTestdata1 = new System.Windows.Forms.CheckBox();
            this.chkTestdata0 = new System.Windows.Forms.CheckBox();
            this.btnGenerateRoadData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
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
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(67, 236);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(153, 23);
            this.btnRoute.TabIndex = 3;
            this.btnRoute.Text = "Get Shortest Path";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnGenerateRoadData);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 270);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTestdata1);
            this.groupBox2.Controls.Add(this.chkTestdata0);
            this.groupBox2.Location = new System.Drawing.Point(6, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 51);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "The Layer Switcher";
            // 
            // chkTestdata1
            // 
            this.chkTestdata1.AutoSize = true;
            this.chkTestdata1.Checked = true;
            this.chkTestdata1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTestdata1.Location = new System.Drawing.Point(114, 23);
            this.chkTestdata1.Name = "chkTestdata1";
            this.chkTestdata1.Size = new System.Drawing.Size(74, 17);
            this.chkTestdata1.TabIndex = 1;
            this.chkTestdata1.Text = "Testdata1";
            this.chkTestdata1.UseVisualStyleBackColor = true;
            this.chkTestdata1.CheckedChanged += new System.EventHandler(this.chkTestdata1_CheckedChanged);
            // 
            // chkTestdata0
            // 
            this.chkTestdata0.AutoSize = true;
            this.chkTestdata0.Checked = true;
            this.chkTestdata0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTestdata0.Location = new System.Drawing.Point(13, 23);
            this.chkTestdata0.Name = "chkTestdata0";
            this.chkTestdata0.Size = new System.Drawing.Size(74, 17);
            this.chkTestdata0.TabIndex = 0;
            this.chkTestdata0.Text = "Testdata0";
            this.chkTestdata0.UseVisualStyleBackColor = true;
            this.chkTestdata0.CheckedChanged += new System.EventHandler(this.chkTestdata1_CheckedChanged);
            // 
            // btnGenerateRoadData
            // 
            this.btnGenerateRoadData.Location = new System.Drawing.Point(65, 140);
            this.btnGenerateRoadData.Name = "btnGenerateRoadData";
            this.btnGenerateRoadData.Size = new System.Drawing.Size(155, 23);
            this.btnGenerateRoadData.TabIndex = 15;
            this.btnGenerateRoadData.Text = "Generate Routing Data";
            this.btnGenerateRoadData.UseVisualStyleBackColor = true;
            this.btnGenerateRoadData.Click += new System.EventHandler(this.btnGenerateRoadData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 52);
            this.label3.TabIndex = 14;
            this.label3.Text = "Select the button with text \"Generate Routing\r\nData\" to build routing data for tw" +
                "o shapefiles,\r\nafter that, you can click another button to \r\nroute on the map";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(82, 180);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(138, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "StartRoadID:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(82, 208);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(138, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "70";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "EndRoadID:";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // UseMultiShapeFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "UseMultiShapeFiles";
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
