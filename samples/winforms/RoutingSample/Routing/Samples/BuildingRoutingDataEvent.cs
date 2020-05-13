using System;
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
    public class BuildingRoutingDataEvent : UserControl
    {
        private static int processedCount;
        private static string rootPath = Samples.rootDirectory;
        private Panel panel1;
        private bool cancel;

        public BuildingRoutingDataEvent()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
            RtgRoutingSource.BuildingRoutingData += new EventHandler<BuildingRoutingDataRtgRoutingSourceEventArgs>(RtgRoutingSource_BuildingRoadData);
            RtgRoutingSource.GeneratingRoutableShapeFile += new EventHandler<GeneratingRoutableShapeFileRoutingSourceEventArgs>(RtgRoutingSource_GeneratingRoutableShapeFile);
        }

        private void btnRaiseEvent_Click(object sender, EventArgs e)
        {
            cancel = false;
            gbProgress.Visible = true;
            processedCount = 0;
            pgBuildingData.Minimum = 0;
            pgBuildingData.Value = 0;

            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            featureSource.Open();
            pgBuildingData.Maximum = featureSource.GetCount();
            lbTotalCount.Text = pgBuildingData.Maximum.ToString(CultureInfo.InvariantCulture);
            featureSource.Close();

            RtgRoutingSource.GenerateRoutingData(Path.Combine(rootPath, "BuildingRoutingDataEvent.rtg"), featureSource, BuildRoutingDataMode.Rebuild, "DallasCounty-4326.shp");
            if (cancel)
            {
                MessageBox.Show("Building routing data has been cancelled!");
            }
            else
            {
                MessageBox.Show("Finish building routing data!");
            }
            gbProgress.Visible = false;
        }

        void RtgRoutingSource_GeneratingRoutableShapeFile(object sender, GeneratingRoutableShapeFileRoutingSourceEventArgs e)
        {
            processedCount++;
            pgBuildingData.PerformStep();
            panel1.Visible = false;
            label1.Text = "Generating routable ShapeFile...";
            Application.DoEvents();
        }

        private void RtgRoutingSource_BuildingRoadData(object sender, BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            if (panel1.Visible == false)
            {
                processedCount = 0;
                pgBuildingData.Value = 0;
                panel1.Visible = true;
                btnCancel.Visible = true;
            }

            e.Cancel = cancel;
            processedCount++;
            pgBuildingData.PerformStep();
            lbProcessedCount.Text = processedCount.ToString(CultureInfo.InvariantCulture);
            Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancel = true;
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.9055649057617, 32.9262169589844, -96.6515060678711, 32.7449425449219);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnRaiseEvent;
        private GroupBox groupBox1;
        private WinformsMap winformsMap1;
        private Label label3;
        private ProgressBar pgBuildingData;
        private GroupBox gbProgress;
        private Label lbProcessedCount;
        private Label label4;
        private Label lbTotalCount;
        private Label label1;
        private Button btnCancel;
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
            this.btnRaiseEvent = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pgBuildingData = new System.Windows.Forms.ProgressBar();
            this.gbProgress = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lbProcessedCount = new System.Windows.Forms.Label();
            this.lbTotalCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.winformsMap1 = new WinformsMap();
            this.groupBox1.SuspendLayout();
            this.gbProgress.SuspendLayout();
            this.panel1.SuspendLayout();
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
            // btnRaiseEvent
            // 
            this.btnRaiseEvent.Location = new System.Drawing.Point(94, 66);
            this.btnRaiseEvent.Name = "btnRaiseEvent";
            this.btnRaiseEvent.Size = new System.Drawing.Size(112, 23);
            this.btnRaiseEvent.TabIndex = 3;
            this.btnRaiseEvent.Text = "Raise the Event";
            this.btnRaiseEvent.UseVisualStyleBackColor = true;
            this.btnRaiseEvent.Click += new System.EventHandler(this.btnRaiseEvent_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnRaiseEvent);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 102);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 39);
            this.label3.TabIndex = 15;
            this.label3.Text = "Click the button below to raise the \r\nBuildingRoutingData event and show the\r\npro" +
                "gress of building data";
            // 
            // pgBuildingData
            // 
            this.pgBuildingData.Location = new System.Drawing.Point(11, 45);
            this.pgBuildingData.Name = "pgBuildingData";
            this.pgBuildingData.Size = new System.Drawing.Size(400, 23);
            this.pgBuildingData.Step = 1;
            this.pgBuildingData.TabIndex = 10;
            // 
            // gbProgress
            // 
            this.gbProgress.Controls.Add(this.panel1);
            this.gbProgress.Controls.Add(this.label1);
            this.gbProgress.Controls.Add(this.btnCancel);
            this.gbProgress.Controls.Add(this.pgBuildingData);
            this.gbProgress.Location = new System.Drawing.Point(136, 184);
            this.gbProgress.Name = "gbProgress";
            this.gbProgress.Size = new System.Drawing.Size(430, 100);
            this.gbProgress.TabIndex = 11;
            this.gbProgress.TabStop = false;
            this.gbProgress.Text = "Building Routing Data";
            this.gbProgress.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lbProcessedCount);
            this.panel1.Controls.Add(this.lbTotalCount);
            this.panel1.Location = new System.Drawing.Point(91, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 24);
            this.panel1.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "of";
            // 
            // lbProcessedCount
            // 
            this.lbProcessedCount.AutoSize = true;
            this.lbProcessedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProcessedCount.ForeColor = System.Drawing.Color.Red;
            this.lbProcessedCount.Location = new System.Drawing.Point(3, 6);
            this.lbProcessedCount.Name = "lbProcessedCount";
            this.lbProcessedCount.Size = new System.Drawing.Size(14, 13);
            this.lbProcessedCount.TabIndex = 14;
            this.lbProcessedCount.Text = "0";
            // 
            // lbTotalCount
            // 
            this.lbTotalCount.AutoSize = true;
            this.lbTotalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCount.ForeColor = System.Drawing.Color.Red;
            this.lbTotalCount.Location = new System.Drawing.Point(71, 6);
            this.lbTotalCount.Name = "lbTotalCount";
            this.lbTotalCount.Size = new System.Drawing.Size(14, 13);
            this.lbTotalCount.TabIndex = 12;
            this.lbTotalCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Building record";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(321, 71);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790D;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            // 
            // BuildingRoutingDataEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbProgress);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "BuildingRoutingDataEvent";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.ParentChanged += new System.EventHandler(this.BuildingRoutingDataEvent_ParentChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProgress.ResumeLayout(false);
            this.gbProgress.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void BuildingRoutingDataEvent_ParentChanged(object sender, EventArgs e)
        {
            RtgRoutingSource.BuildingRoutingData -= new EventHandler<BuildingRoutingDataRtgRoutingSourceEventArgs>(RtgRoutingSource_BuildingRoadData);
            RtgRoutingSource.GeneratingRoutableShapeFile -= new EventHandler<GeneratingRoutableShapeFileRoutingSourceEventArgs>(RtgRoutingSource_GeneratingRoutableShapeFile);
        }

        #endregion
    }
}
