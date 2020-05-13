using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;

namespace HowDoISamples
{
    public class GetFastestPath : UserControl
    {
        private const int SpeedOfHighWay = 200;     // The default speed of highway (kph)
        private const int SpeedOfLocalRoad = 40;    // The default speed of local road (kph)

        public GetFastestPath()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnGenerateRoadData_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Finish building routing data!");
            return;

            RtgRoutingSource.BuildingRoadData += new EventHandler<BuildingRoutingDataRtgRoutingSourceEventArgs>(RtgRoutingSource_BuildingRoadData);
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(@"..\..\SampleData\Austinstreets.shp");
            RtgRoutingSource.GenerateRoutingData(@"..\..\SampleData\routeDataForFastest.rtg", featureSource, "Austinstreets.shp");
        }

        void RtgRoutingSource_BuildingRoadData(object sender, BuildingRoutingDataRtgRoutingSourceEventArgs e)
        {
            if (e.Feature.ColumnValues["CFCC"].CompareTo("A4") < 0)
            {
                e.Road.Length = e.Road.Length / SpeedOfHighWay;
            }
            else
            {
                e.Road.Length = e.Road.Length / SpeedOfLocalRoad;
            }
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(@"..\..\SampleData\Austinstreets.shp");
            RoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\routeDataForFastest.rtg");
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);
            RoutingResult routingResult = routingEngine.GetShortestPath(txtStartId.Text, txtEndId.Text);

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
            winformsMap1.CurrentExtent = new RectangleShape(-97.763384, 30.299707, -97.712382, 30.259309);

            ShapeFileFeatureLayer austinstreetsLayer = new ShapeFileFeatureLayer(@"..\..\SampleData\Austinstreets.shp");
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(GetRoadStyle());
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay austinstreetsOverlay = new LayerOverlay();
            austinstreetsOverlay.Layers.Add("AustinstreetsLayer", austinstreetsLayer);
            winformsMap1.Overlays.Add("AustinstreetsOverlay", austinstreetsOverlay);

            // The layer for rendering routing result
            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(100, GeoColor.StandardColors.Purple), 5);
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            // The layer for rendering start and end points
            InMemoryFeatureLayer stopLayer = new InMemoryFeatureLayer();
            stopLayer.Open();
            stopLayer.Columns.Add(new FeatureSourceColumn("Order"));
            stopLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Square, new GeoSolidBrush(GeoColor.SimpleColors.LightGreen), new GeoPen(GeoColor.SimpleColors.Black), 12);
            stopLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.Urban1("Order");
            stopLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.XOffsetInPixel = -4;
            stopLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay stopOverlay = new LayerOverlay();
            stopOverlay.Layers.Add("StopLayer", stopLayer);
            winformsMap1.Overlays.Add("StopOverlay", stopOverlay);

            // Add start and end points to the layer
            RoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\Austinstreets.rtg");
            routingSource.Open();
            austinstreetsLayer.Open();
            Feature startRoad = austinstreetsLayer.FeatureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns);
            Feature startPoint = new Feature(startRoad.GetShape().GetCenterPoint().GetWellKnownBinary());
            startPoint.ColumnValues["Order"] = "1";
            stopLayer.InternalFeatures.Add(startPoint);
            Feature endRoad = austinstreetsLayer.FeatureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns);
            Feature endPoint = new Feature(endRoad.GetShape().GetCenterPoint().GetWellKnownBinary());
            endPoint.ColumnValues["Order"] = "2";
            stopLayer.InternalFeatures.Add(endPoint);
            routingSource.Close();
            austinstreetsLayer.Close();

            winformsMap1.Refresh();
        }

        private Style GetRoadStyle()
        {
            string expression = "CFCC.CompareTo(\"A4\")<0";

            FleeBooleanStyle roadCFCCStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this
            // to access the Convert.Toxxx methods to convert variable types
            roadCFCCStyle.StaticTypes.Add(typeof(System.Convert));
            // The math class might be handy to include but in this sample we do not use it
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));

            roadCFCCStyle.ColumnVariables.Add("CFCC");

            roadCFCCStyle.CustomTrueStyles.Add(new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 7), new GeoPen(GeoColor.FromHtml("#f9de4d"), 5)));
            roadCFCCStyle.CustomFalseStyles.Add(new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 5), new GeoPen(GeoColor.StandardColors.White, 3)));

            return roadCFCCStyle;
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
            this.btnGenerateRoadData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
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
            this.btnRoute.Location = new System.Drawing.Point(78, 168);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(127, 23);
            this.btnRoute.TabIndex = 3;
            this.btnRoute.Text = "Get Fastest Path";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateRoadData);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 205);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // btnGenerateRoadData
            // 
            this.btnGenerateRoadData.Location = new System.Drawing.Point(50, 71);
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
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(218, 39);
            this.label3.TabIndex = 14;
            this.label3.Text = "Click the button below to Build the routing\r\ndata, and after that, set the StartR" +
                "oadid and \r\nEndRoadId to find the fastest path.";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(93, 114);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(112, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "4716";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "StartRoadID:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(93, 142);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(112, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "9638";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 145);
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
            // GetFastestPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "GetFastestPath";
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
