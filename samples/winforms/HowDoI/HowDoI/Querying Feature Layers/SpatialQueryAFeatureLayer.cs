/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class SpatialQueryAFeatureLayer : UserControl
    {
        public SpatialQueryAFeatureLayer()
        {
            InitializeComponent();
        }

        private void SpatialQueryAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            InMemoryFeatureLayer rectangleLayer = new InMemoryFeatureLayer();
            rectangleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(50, 100, 100, 200)));
            rectangleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.StandardColors.DarkBlue;
            rectangleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            rectangleLayer.InternalFeatures.Add("Rectangle", new Feature("POLYGON((-5565974.53966368 -2273030.92698769,-5565974.53966368 2273030.92698769,5565974.53966368 2273030.92698769,5565974.53966368 -2273030.92698769,-5565974.53966368 -2273030.92698769))", "Rectangle"));

            InMemoryFeatureLayer spatialQueryResultLayer = new InMemoryFeatureLayer();
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(200, GeoColor.SimpleColors.PastelRed)));
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.StandardColors.Red;
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay spatialQueryResultOverlay = new LayerOverlay();
            spatialQueryResultOverlay.Layers.Add("RectangleLayer", rectangleLayer);
            spatialQueryResultOverlay.Layers.Add("SpatialQueryResultLayer", spatialQueryResultLayer);
            winformsMap1.Overlays.Add("SpatialQueryResultOverlay", spatialQueryResultOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        private void cboSpatialQueryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("WorldLayer");
            InMemoryFeatureLayer rectangleLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("RectangleLayer");
            InMemoryFeatureLayer spatialQueryResultLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SpatialQueryResultLayer");

            Feature rectangleFeature = rectangleLayer.InternalFeatures["Rectangle"];
            Collection<Feature> spatialQueryResults;
            worldLayer.Open();
            switch (cboSpatialQueryType.SelectedItem.ToString())
            {
                case "Within":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesWithin(rectangleFeature, new string[0]);
                    break;

                case "Containing":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesContaining(rectangleFeature, new string[0]);
                    break;

                case "Disjointed":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesDisjointed(rectangleFeature, new string[0]);
                    break;

                case "Intersecting":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesIntersecting(rectangleFeature, new string[0]);
                    break;

                case "Overlapping":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesOverlapping(rectangleFeature, new string[0]);
                    break;

                case "TopologicalEqual":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesTopologicalEqual(rectangleFeature, new string[0]);
                    break;

                case "Touching":
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesTouching(rectangleFeature, new string[0]);
                    break;

                default:
                    spatialQueryResults = worldLayer.QueryTools.GetFeaturesWithin(rectangleFeature, new string[0]);
                    break;
            }
            worldLayer.Close();

            spatialQueryResultLayer.InternalFeatures.Clear();
            foreach (Feature feature in spatialQueryResults)
            {
                spatialQueryResultLayer.InternalFeatures.Add(feature.Id, feature);
            }

            winformsMap1.Refresh(winformsMap1.Overlays["SpatialQueryResultOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private ComboBox cboSpatialQueryType;
        private Label label1;
        private WinformsMap winformsMap1;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboSpatialQueryType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.cboSpatialQueryType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(388, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 57);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // cboSpatialQueryType
            //
            this.cboSpatialQueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpatialQueryType.FormattingEnabled = true;
            this.cboSpatialQueryType.Items.AddRange(new object[] {
            "Within",
            "Containing",
            "Disjointed",
            "Intersecting",
            "Overlapping",
            "TopologicalEqual",
            "Touching"});
            this.cboSpatialQueryType.Location = new System.Drawing.Point(155, 19);
            this.cboSpatialQueryType.Name = "cboSpatialQueryType";
            this.cboSpatialQueryType.Size = new System.Drawing.Size(182, 21);
            this.cboSpatialQueryType.SelectedIndex = 1;
            this.cboSpatialQueryType.TabIndex = 2;
            this.cboSpatialQueryType.SelectedIndexChanged += new System.EventHandler(this.cboSpatialQueryType_SelectedIndexChanged);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose SpatialQuery Type:";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 6;
            this.winformsMap1.Text = "winformsMap1";
            //
            // SpatialQueryAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "SpatialQueryAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.SpatialQueryAFeatureLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}