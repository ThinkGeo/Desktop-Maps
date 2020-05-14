using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindFeaturesWithinDistance : UserControl
    {
        private PointShape pointShape;

        public FindFeaturesWithinDistance()
        {
            InitializeComponent();
        }

        private void FindFeaturesWithinDistance_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundPointStyle(PointSymbolType.Square, GeoColors.White, GeoColors.Black, 1F, 8F, PointSymbolType.Square, GeoColors.Navy, GeoColors.Transparent, 0F, 4F);
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(150, 154, 205, 50));
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.Layers.Add("HighlightLayer", highlightLayer);
            winformsMap1.Overlays.Add("HighlightOverlay", dynamicOverlay);

            winformsMap1.MapClick += new EventHandler<MapClickMapViewEventArgs>(winformsMap1_MapClick);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            pointShape = e.WorldLocation;

            FindWithinDistanceFeatures();
        }

        private void cmbDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pointShape != null)
            {
                FindWithinDistanceFeatures();
            }
        }

        private void FindWithinDistanceFeatures()
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("HighlightLayer");

            // Find the countries within special distance.
            double distance = Convert.ToDouble(cmbDistance.SelectedItem.ToString(), CultureInfo.InvariantCulture);
            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesWithinDistanceOf(pointShape, GeographyUnit.DecimalDegree, DistanceUnit.Kilometer, distance, new string[0]);
            worldLayer.Close();

            if (highlightLayer.InternalFeatures.Count > 0)
            {
                highlightLayer.InternalFeatures.Clear();
            }

            highlightLayer.InternalFeatures.Add("Point", new Feature(pointShape));
            foreach (Feature feature in selectedFeatures)
            {
                highlightLayer.InternalFeatures.Add(feature.Id, feature);
            }

            winformsMap1.Refresh(winformsMap1.Overlays["HighlightOverlay"]);
        }

        private MapView winformsMap1;

        #region Component Designer generated code

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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDistance = new System.Windows.Forms.ComboBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbDistance);
            this.groupBox1.Location = new System.Drawing.Point(318, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(301, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please click on the map to see the result within the distance of";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "KM";
            //
            // cmbDistance
            //
            this.cmbDistance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistance.FormattingEnabled = true;
            this.cmbDistance.Items.AddRange(new object[] {
            "500",
            "1,000",
            "2,000",
            "5,000"});
            this.cmbDistance.Location = new System.Drawing.Point(313, 19);
            this.cmbDistance.Name = "cmbDistance";
            this.cmbDistance.Size = new System.Drawing.Size(73, 21);
            this.cmbDistance.SelectedIndex = 1;
            this.cmbDistance.TabIndex = 0;
            this.cmbDistance.SelectedIndexChanged += new System.EventHandler(this.cmbDistance_SelectedIndexChanged);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 2;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindFeaturesWithinDistance
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindFeaturesWithinDistance";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindFeaturesWithinDistance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private ComboBox cmbDistance;
        private Label label1;
        private Label label2;

        #endregion Component Designer generated code
    }
}