using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.WorldMapKit;
using System.Collections.Generic;

namespace HowDoISamples
{
    public class GetClosestFacility : UserControl
    {
        private FeatureSource featureSource;
        private int facilityCount = 1;

        public GetClosestFacility()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            RtgRoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\Austinstreets.rtg");
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);
            routingEngine.GeneratingServiceArea += new EventHandler<GeneratingServiceAreaRoutingEngineEventArgs>(routingEngine_GeneratingServiceArea);
            float averageSpeed = float.Parse(txtAverageSpeed.Text);
            int drivingMinutes = int.Parse(txtDrivingMinutes.Text);

            PolygonShape polygonShape = routingEngine.GenerateServiceArea(txtStartId.Text, new TimeSpan(0, drivingMinutes, 0), averageSpeed);
            InMemoryFeatureLayer routingLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];

            routingLayer.InternalFeatures.Remove("ServiceArea");
            if (polygonShape.Validate(ShapeValidationMode.Simple).IsValid)
            {
                routingLayer.InternalFeatures.Add("ServiceArea", new Feature(polygonShape));
                routingLayer.Open();
                winformsMap1.CurrentExtent = routingLayer.GetBoundingBox();
                routingLayer.Close();
            }

            winformsMap1.Overlays["RoutingOverlay"].Lock.IsDirty = true;

            winformsMap1.Refresh();
        }

        void routingEngine_GeneratingServiceArea(object sender, GeneratingServiceAreaRoutingEngineEventArgs e)
        {
            Collection<string> featureIds = GetFeatureIds(e.AccessibleFeatureIds);

            RectangleShape rectangleShape = GetBoundingBoxByIds(featureIds);
            ShapeFileFeatureSource source = new ShapeFileFeatureSource(@"..\..\SampleData\ghospitl.shp");
            source.Open();
            Collection<Feature> features = source.GetFeaturesInsideBoundingBox(rectangleShape, new string[] { "NAME" });
            source.Close();
            if (features.Count >= facilityCount)
            {
                InMemoryFeatureLayer facilityLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["FacilityLayer"];
                foreach (Feature item in features)
                {
                    facilityLayer.InternalFeatures.Add(item);
                }

                e.IsTermination = true;
            }
        }

        public RectangleShape GetBoundingBoxByIds(Collection<string> ids)
        {
            RectangleShape resultRectangleShape = null;

            if (ids.Count > 0)
            {
                resultRectangleShape = featureSource.GetBoundingBoxById(ids[0]);
                for (int i = 1; i < ids.Count; i++)
                {
                    RectangleShape currentRectangleShape = featureSource.GetBoundingBoxById(ids[i]);
                    resultRectangleShape.ExpandToInclude(currentRectangleShape);
                }
            }

            return resultRectangleShape;
        }

        private GeoCollection<string> GetFeatureIds(IEnumerable<string> adjacentIds)
        {
            GeoCollection<string> featureIds = new GeoCollection<string>();
            foreach (string item in adjacentIds)
            {
                string featureId = item.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries)[0];
                featureIds.Add(item, featureId);
            }
            return featureIds;
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-97.7717794617797, 30.3120942308639, -97.7082647523071, 30.2667756273482);

            WorldMapKitWmsDesktopOverlay worldMapKitsOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add(worldMapKitsOverlay);

            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();
            featureSource = new ShapeFileFeatureSource(@"..\..\SampleData\Austinstreets.shp");
            featureSource.Open();
            Feature startRoad = featureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns);
            featureSource.Close();
            Feature startPoint = new Feature(startRoad.GetShape().GetCenterPoint().GetWellKnownBinary(), txtStartId.Text);
            routingLayer.InternalFeatures.Add(txtStartId.Text, startPoint);

            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City1;
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColor.StandardColors.Blue));
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Color = GeoColor.StandardColors.Red;
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.815409, 30.369949, -97.657999, 30.217922)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            InMemoryFeatureLayer facilityLayer = new InMemoryFeatureLayer();
            facilityLayer.Open();
            facilityLayer.Columns.Add(new FeatureSourceColumn("NAME"));
            facilityLayer.Close();
            facilityLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.Capital1;
            facilityLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingOverlay.Layers.Add("FacilityLayer", facilityLayer);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnFind;
        private Label label1;
        private TextBox txtDrivingMinutes;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private GroupBox groupBox1;
        private TextBox txtAverageSpeed;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label cmbUnit;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceAreaDefinition));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAverageSpeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDrivingMinutes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.cmbUnit = new System.Windows.Forms.Label();
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAverageSpeed);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDrivingMinutes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.cmbUnit);
            this.groupBox1.Location = new System.Drawing.Point(509, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 215);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(225, 65);
            this.label7.TabIndex = 19;
            this.label7.Text = "Click the Button below to get the closest\r\nfacility around the source road. For instance,\r\nthe 2 minutes service area for a point includes\r\nall the roads that can be reached whithin 2\r\nminutes with a specified speed.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 18;
            // 
            // txtAverageSpeed
            // 
            this.txtAverageSpeed.Location = new System.Drawing.Point(98, 104);
            this.txtAverageSpeed.Name = "txtAverageSpeed";
            this.txtAverageSpeed.Size = new System.Drawing.Size(45, 20);
            this.txtAverageSpeed.TabIndex = 15;
            this.txtAverageSpeed.ReadOnly = true;
            this.txtAverageSpeed.Text = "80";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Speed (per hour):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Minutes";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(98, 159);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(114, 20);
            this.txtStartId.TabIndex = 12;
            this.txtStartId.Text = "7700";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Source Feature Id:";
            // 
            // txtDrivingMinutes
            // 
            this.txtDrivingMinutes.Location = new System.Drawing.Point(98, 133);
            this.txtDrivingMinutes.Name = "txtDrivingMinutes";
            this.txtDrivingMinutes.Size = new System.Drawing.Size(45, 20);
            this.txtDrivingMinutes.TabIndex = 8;
            this.txtDrivingMinutes.ReadOnly = true;
            this.txtDrivingMinutes.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Driving Minutes:";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(98, 185);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(114, 23);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "Get Closest Facility";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // cmbUnit
            // 
            this.cmbUnit.Location = new System.Drawing.Point(149, 104);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(76, 21);
            this.cmbUnit.TabIndex = 14;
            this.cmbUnit.Text = "Kilometer";
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
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.DesktopEdition.MapFocusMode.Default;
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
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // ServiceAreaDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ServiceAreaDefinition";
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
