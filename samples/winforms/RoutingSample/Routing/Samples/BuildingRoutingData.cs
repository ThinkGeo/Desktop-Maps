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
    public class BuildingRoutingData : UserControl
    {
        private const int SpeedOfHighWay = 130;     // The default speed of highway (kph)
        private const int SpeedOfLocalRoad = 40;    // The default speed of local routeSegment (kph)
        private static string rootPath = Samples.rootDirectory;

        private GeoCollection<string> filenames;
        private FeatureSource featureSource;

        public BuildingRoutingData()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            filenames = new GeoCollection<string>();
            filenames.Add("ShortestPath", "ShortestPath");
            filenames.Add("FastestPath", "FastestPath");
            filenames.Add("DallasWithOneWayRoad", "DallasWithOneWayRoad");
            filenames.Add("OptimizeHighway", "OptimizeHighway");
            cmbPurpose.DataSource = filenames.GetKeys();
            RtgRoutingSource.BuildingRoutingData += new EventHandler<BuildingRoutingDataRtgRoutingSourceEventArgs>(RtgRoutingSource_BuildingRoadData);

            RenderMap();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This operation will take a while, click Ok to continue and click Canel to quit.", "Build Routing Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                featureSource = GetFeatureSource();
                RtgRoutingSource.GenerateRoutingData(Path.Combine(rootPath, txtFilename.Text), featureSource, BuildRoutingDataMode.Rebuild, GeographyUnit.DecimalDegree, DistanceUnit.Meter);
                MessageBox.Show("Finish building routing data!");
            }
        }

        private FeatureSource GetFeatureSource()
        {
            FeatureSource featureSource = null;
            switch (cmbPurpose.Text.Trim())
            {
                case "DallasWithOneWayRoad":
                    featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasWithOneWayRoad.shp"));
                    break;
                default:
                    featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
                    break;
            }
            return featureSource;
        }

        private void RtgRoutingSource_BuildingRoadData(object sender, BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            switch (cmbPurpose.Text.Trim())
            {
                case "FastestPath":
                    FastestPath(e);
                    break;
                case "DallasWithOneWayRoad":
                    OneWayRoad(e);
                    break;
                case "OptimizeHighway":
                    OptimizeHighway(e);
                    break;
                default:
                    break;
            }
        }

        private void FastestPath(BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            featureSource.Open();
            Feature feature = featureSource.GetFeatureById(e.LineShape.Id, new string[] { "CFCC" });
            if (feature.ColumnValues["CFCC"].CompareTo("A4") < 0)
            {
                e.RouteSegment.Weight = e.RouteSegment.Weight / SpeedOfHighWay;
            }
            else
            {
                e.RouteSegment.Weight = e.RouteSegment.Weight / SpeedOfLocalRoad;
            }
        }

        private void OneWayRoad(BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            featureSource.Open();
            Feature feature = featureSource.GetFeatureById(e.LineShape.Id, new string[] { "One_way", "Indicator" });

            // One-Way	1=yes, 0=no
            string oneWay = feature.ColumnValues["One_way"];
            // Indicator 1=opposite to the routeSegment direction		
            string indicator = feature.ColumnValues["Indicator"];

            if (oneWay == "1")
            {
                if (indicator == "0")
                {
                    e.RouteSegment.StartPointAdjacentIds.Clear();
                }
                else
                {
                    e.RouteSegment.EndPointAdjacentIds.Clear();
                }
            }
        }

        private void OptimizeHighway(BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            featureSource.Open();
            Feature feature = featureSource.GetFeatureById(e.LineShape.Id, new string[] { "CFCC" });

            if (feature.ColumnValues["CFCC"].CompareTo("A4") < 0)
            {
                e.RouteSegment.RouteSegmentType = 2;
                e.RouteSegment.Weight = e.RouteSegment.Weight * 0.1f;
            }
        }

        private void cmbPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilename.Text = filenames[cmbPurpose.Text] + ".rtg";
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
        private Button btnBuild;
        private GroupBox groupBox1;
        private WinformsMap winformsMap1;
        private Label label3;
        private ComboBox cmbPurpose;
        private TextBox txtFilename;
        private Label label5;
        private Label label2;
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
            this.btnBuild = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPurpose = new System.Windows.Forms.ComboBox();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(131, 129);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 3;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbPurpose);
            this.groupBox1.Controls.Add(this.txtFilename);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnBuild);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 165);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Purpose:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Target File:";
            // 
            // cmbPurpose
            // 
            this.cmbPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurpose.FormattingEnabled = true;
            this.cmbPurpose.Location = new System.Drawing.Point(72, 101);
            this.cmbPurpose.Name = "cmbPurpose";
            this.cmbPurpose.Size = new System.Drawing.Size(148, 21);
            this.cmbPurpose.TabIndex = 17;
            this.cmbPurpose.SelectedIndexChanged += new System.EventHandler(this.cmbPurpose_SelectedIndexChanged);
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(72, 74);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(148, 20);
            this.txtFilename.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 26);
            this.label3.TabIndex = 15;
            this.label3.Text = "Click the button below to build the \r\nrouting data according to different purpose" +
                "s.";
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
            // BuildingRoutingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "BuildingRoutingData";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.ParentChanged += new System.EventHandler(this.BuildingRoutingData_ParentChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void BuildingRoutingData_ParentChanged(object sender, EventArgs e)
        {
            RtgRoutingSource.BuildingRoutingData -= new EventHandler<BuildingRoutingDataRtgRoutingSourceEventArgs>(RtgRoutingSource_BuildingRoadData);
        }

        #endregion

    }
}
