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
    public class UseDifferentAlgorithms : UserControl
    {
        private static string rootPath = Samples.rootDirectory;

        public UseDifferentAlgorithms()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();

            this.cbmAlgorithm.Text = "Dijkstra";
        }

        void cbmAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            RoutingSource routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            RoutingEngine routingEngine = new RoutingEngine(routingSource, GetAlgorithm(), featureSource);
            RoutingResult routingResult = routingEngine.GetRoute(txtStartId.Text, txtEndId.Text);

            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];

            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingResult.Route);

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        private RoutingAlgorithm GetAlgorithm()
        {
            RoutingAlgorithm algorithm = null;
            switch (cbmAlgorithm.Text)
            {
                case "A*":
                    algorithm = new AStarRoutingAlgorithm();
                    break;
                case "Dijkstra":
                    algorithm = new DijkstraRoutingAlgorithm();
                    break;
                case "Bidirectional":
                    algorithm = new BidirectionalRoutingAlgorithm();
                    break;
                default:
                    break;
            }

            return algorithm;
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
            routingLayer.StartPoint = streetsLayer.FeatureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = streetsLayer.FeatureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            streetsLayer.Close();
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
        private ComboBox cbmAlgorithm;
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.btnRoute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbmAlgorithm = new System.Windows.Forms.ComboBox();
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
            this.btnRoute.Location = new System.Drawing.Point(0, 0);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(75, 23);
            this.btnRoute.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbmAlgorithm);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 155);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 26);
            this.label4.TabIndex = 16;
            this.label4.Text = "Please select the different algorithm to find\r\nthe path.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Algorithm:";
            // 
            // cbmAlgorithm
            // 
            this.cbmAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbmAlgorithm.FormattingEnabled = true;
            this.cbmAlgorithm.Items.AddRange(new object[] {
            "A*",
            "Dijkstra",
            "Bidirectional"});
            this.cbmAlgorithm.Location = new System.Drawing.Point(97, 117);
            this.cbmAlgorithm.Name = "cbmAlgorithm";
            this.cbmAlgorithm.Size = new System.Drawing.Size(115, 21);
            this.cbmAlgorithm.TabIndex = 14;
            this.cbmAlgorithm.SelectedIndexChanged += new System.EventHandler(this.cbmAlgorithm_SelectedIndexChanged);
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(97, 63);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(115, 20);
            this.txtStartId.TabIndex = 12;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Feature Id:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(97, 91);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(115, 20);
            this.txtEndId.TabIndex = 8;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 94);
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
            // UseDifferentAlgorithms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "UseDifferentAlgorithms";
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
