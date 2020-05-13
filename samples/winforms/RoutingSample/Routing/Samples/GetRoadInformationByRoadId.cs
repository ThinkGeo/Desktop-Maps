using System;
using System.Collections.ObjectModel;
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
    public class GetRoadInformationByRoadId : UserControl
    {
        private static string rootPath = Samples.rootDirectory;

        public GetRoadInformationByRoadId()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnGetInformation_Click(object sender, EventArgs e)
        {
            ShapeFileFeatureLayer dallasStreetsLayer = new ShapeFileFeatureLayer(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            dallasStreetsLayer.Open();

            RtgRoutingSource routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.rtg"));
            routingSource.ReadEndPoints = true;
            routingSource.Open();
            RouteSegment road = routingSource.GetRouteSegmentByFeatureId(txtRoadId.Text);
            // render routeSegment information
            RenderRoadInformation(dallasStreetsLayer, road);
            // render adjacent routeSegments information
            RenderAdjacentRoadsInformation(dallasStreetsLayer, road);

            dallasStreetsLayer.Close();
            routingSource.Close();
            winformsMap1.Refresh(new Overlay[] { winformsMap1.Overlays["currentRoadOverlay"], winformsMap1.Overlays["adjacentRoadsOverlay"] });
        }

        private void RenderAdjacentRoadsInformation(ShapeFileFeatureLayer featureLayer, RouteSegment road)
        {
            InMemoryFeatureLayer adjacentRoadsLayer = ((LayerOverlay)winformsMap1.Overlays["adjacentRoadsOverlay"]).Layers["adjacentRoadsLayer"] as InMemoryFeatureLayer;
            Collection<string> adjacentIds = road.StartPointAdjacentIds;
            foreach (string id in road.EndPointAdjacentIds)
            {
                adjacentIds.Add(id);
            }
            Collection<Feature> features = featureLayer.FeatureSource.GetFeaturesByIds(adjacentIds, ReturningColumnsType.AllColumns);
            adjacentRoadsLayer.InternalFeatures.Clear();

            foreach (Feature feature in features)
            {
                adjacentRoadsLayer.InternalFeatures.Add(feature);
            }
        }

        private void RenderRoadInformation(ShapeFileFeatureLayer featureLayer, RouteSegment road)
        {
            InMemoryFeatureLayer currentRoadLayer = ((LayerOverlay)winformsMap1.Overlays["currentRoadOverlay"]).Layers["currentRoadLayer"] as InMemoryFeatureLayer;
            string featureId = road.FeatureId;
            Feature currentRoadFeature = featureLayer.FeatureSource.GetFeatureById(featureId, ReturningColumnsType.AllColumns);

            currentRoadLayer.InternalFeatures.Clear();
            currentRoadLayer.InternalFeatures.Add(currentRoadFeature);

            txtStartPoint.Text = String.Format("{0}, {1}", road.StartPoint.X.ToString("F4", CultureInfo.InvariantCulture), road.StartPoint.Y.ToString("F4", CultureInfo.InvariantCulture));
            txtEndPoint.Text = String.Format("{0}, {1}", road.EndPoint.X.ToString("F4", CultureInfo.InvariantCulture), road.EndPoint.Y.ToString("F4", CultureInfo.InvariantCulture));
            LineShape line = ((MultilineShape)currentRoadFeature.GetShape()).Lines[0];
            txtLength.Text = Math.Round(line.GetLength(GeographyUnit.DecimalDegree, DistanceUnit.Meter), 4).ToString(CultureInfo.InvariantCulture);

            switch (road.RouteSegmentType)
            {
                case 0:
                    txtRoadType.Text = "Local Road";
                    break;
                case 1:
                    txtRoadType.Text = "Major Road";
                    break;
                case 2:
                    txtRoadType.Text = "High Way";
                    break;
                default:
                    break;
            }
        }


        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.8058, 32.7943, -96.7796, 32.7773);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            ShapeFileFeatureLayer featureLayer = new ShapeFileFeatureLayer(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            featureLayer.Open();
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.LightGray, 1));
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer adjacentRoadsLayer = new InMemoryFeatureLayer();
            adjacentRoadsLayer.Open();
            adjacentRoadsLayer.Columns.Add(new FeatureSourceColumn("FENAME"));
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.LightGreen, 6));
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = WorldStreetsTextStyles.GeneralPurpose("FENAME", 10);
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay adjacentRoadsOverlay = new LayerOverlay();
            adjacentRoadsOverlay.Layers.Add("afda", featureLayer);
            adjacentRoadsOverlay.Layers.Add("adjacentRoadsLayer", adjacentRoadsLayer);
            winformsMap1.Overlays.Add("adjacentRoadsOverlay", adjacentRoadsOverlay);

            InMemoryFeatureLayer currentRoadLayer = new InMemoryFeatureLayer();
            currentRoadLayer.Open();
            currentRoadLayer.Columns.Add(new FeatureSourceColumn("FENAME"));
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.LightRed, 6));
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = WorldStreetsTextStyles.GeneralPurpose("FENAME", 10);
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay currentRoadOverlay = new LayerOverlay();
            currentRoadOverlay.Layers.Add("currentRoadLayer", currentRoadLayer);
            winformsMap1.Overlays.Add("currentRoadOverlay", currentRoadOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnGetInformation;
        private GroupBox gbInstruction;
        private TextBox txtRoadId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox txtLength;
        private Label label5;
        private TextBox txtRoadType;
        private Label label4;
        private TextBox txtStartPoint;
        private Label label3;
        private Label label6;
        private TextBox txtEndPoint;
        private Label label7;
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
            this.btnGetInformation = new System.Windows.Forms.Button();
            this.gbInstruction = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRoadType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRoadId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.gbInstruction.SuspendLayout();
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
            // btnGetInformation
            // 
            this.btnGetInformation.Location = new System.Drawing.Point(89, 92);
            this.btnGetInformation.Name = "btnGetInformation";
            this.btnGetInformation.Size = new System.Drawing.Size(116, 23);
            this.btnGetInformation.TabIndex = 3;
            this.btnGetInformation.Text = "Get Road Information";
            this.btnGetInformation.UseVisualStyleBackColor = true;
            this.btnGetInformation.Click += new System.EventHandler(this.btnGetInformation_Click);
            // 
            // gbInstruction
            // 
            this.gbInstruction.Controls.Add(this.groupBox2);
            this.gbInstruction.Controls.Add(this.label1);
            this.gbInstruction.Controls.Add(this.txtRoadId);
            this.gbInstruction.Controls.Add(this.label2);
            this.gbInstruction.Controls.Add(this.btnGetInformation);
            this.gbInstruction.Location = new System.Drawing.Point(514, 0);
            this.gbInstruction.Name = "gbInstruction";
            this.gbInstruction.Size = new System.Drawing.Size(226, 276);
            this.gbInstruction.TabIndex = 8;
            this.gbInstruction.TabStop = false;
            this.gbInstruction.Text = "Instructions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtLength);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRoadType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtEndPoint);
            this.groupBox2.Controls.Add(this.txtStartPoint);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 138);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Road Information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Meter";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(69, 106);
            this.txtLength.Name = "txtLength";
            this.txtLength.ReadOnly = true;
            this.txtLength.Size = new System.Drawing.Size(95, 20);
            this.txtLength.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 110);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Length:";
            // 
            // txtRoadType
            // 
            this.txtRoadType.AcceptsReturn = true;
            this.txtRoadType.Location = new System.Drawing.Point(69, 80);
            this.txtRoadType.Name = "txtRoadType";
            this.txtRoadType.ReadOnly = true;
            this.txtRoadType.Size = new System.Drawing.Size(140, 20);
            this.txtRoadType.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Road Type:";
            // 
            // txtStartPoint
            // 
            this.txtStartPoint.Location = new System.Drawing.Point(69, 23);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.ReadOnly = true;
            this.txtStartPoint.Size = new System.Drawing.Size(140, 20);
            this.txtStartPoint.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Start Point:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 26);
            this.label1.TabIndex = 14;
            this.label1.Text = "Click the Button below to show the road and \r\nits adjacent roads\' information of current road.";
            // 
            // txtRoadId
            // 
            this.txtRoadId.Enabled = false;
            this.txtRoadId.Location = new System.Drawing.Point(89, 66);
            this.txtRoadId.Name = "txtRoadId";
            this.txtRoadId.Size = new System.Drawing.Size(116, 20);
            this.txtRoadId.TabIndex = 1;
            this.txtRoadId.Text = "58420";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Feature Id:";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "End Point:";
            // 
            // txtEndPoint
            // 
            this.txtEndPoint.Location = new System.Drawing.Point(69, 49);
            this.txtEndPoint.Name = "txtEndPoint";
            this.txtEndPoint.ReadOnly = true;
            this.txtEndPoint.Size = new System.Drawing.Size(140, 20);
            this.txtEndPoint.TabIndex = 1;
            // 
            // GetRoadInformationByRoadId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInstruction);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "GetRoadInformationByRoadId";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.gbInstruction.ResumeLayout(false);
            this.gbInstruction.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
