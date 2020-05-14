using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ZoomToAFeatureOrFeatures : UserControl
    {
        public ZoomToAFeatureOrFeatures()
        {
            InitializeComponent();
        }

        private void ZoomToAFeatureOrFeatures_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer("SampleData/Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Converts the layer from Decimal Degree projection to Spherical Mercator which is the projection the base map is using. 
            worldLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);
        }

        private void btnOneFeature_Click(object sender, EventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            mapView.CurrentExtent = worldLayer.FeatureSource.GetBoundingBoxById("137"); // For Mexico
            worldLayer.Close();

            mapView.Refresh();
        }

        private void btnMultipleFeatures_Click(object sender, EventArgs e)
        {
            Collection<string> featureIDs = new Collection<string>();
            featureIDs.Add("63");  // For US
            featureIDs.Add("6");   // For Canada
            featureIDs.Add("137"); // For Mexico

            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            Collection<Feature> features = worldLayer.FeatureSource.GetFeaturesByIds(featureIDs, ReturningColumnsType.NoColumns);
            worldLayer.Close();

            mapView.CurrentExtent = MapUtil.GetBoundingBoxOfItems(features);
            mapView.Refresh();
        }

        private MapView mapView;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnMultipleFeatures = new System.Windows.Forms.Button();
            this.btnOneFeature = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnMultipleFeatures
            //
            this.btnMultipleFeatures.Location = new System.Drawing.Point(15, 48);
            this.btnMultipleFeatures.Name = "btnMultipleFeatures";
            this.btnMultipleFeatures.Size = new System.Drawing.Size(141, 23);
            this.btnMultipleFeatures.TabIndex = 6;
            this.btnMultipleFeatures.Text = "Zoom To Multiple Features";
            this.btnMultipleFeatures.UseVisualStyleBackColor = true;
            this.btnMultipleFeatures.Click += new System.EventHandler(this.btnMultipleFeatures_Click);
            //
            // btnOneFeature
            //
            this.btnOneFeature.Location = new System.Drawing.Point(15, 19);
            this.btnOneFeature.Name = "btnOneFeature";
            this.btnOneFeature.Size = new System.Drawing.Size(141, 23);
            this.btnOneFeature.TabIndex = 5;
            this.btnOneFeature.Text = "Zoom To One Feature";
            this.btnOneFeature.UseVisualStyleBackColor = true;
            this.btnOneFeature.Click += new System.EventHandler(this.btnOneFeature_Click);
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnMultipleFeatures);
            this.groupBox1.Controls.Add(this.btnOneFeature);
            this.groupBox1.Location = new System.Drawing.Point(565, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // winformsMap1
            //
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.Name = "winformsMap1";
            this.mapView.Size = new System.Drawing.Size(740, 528);
            this.mapView.TabIndex = 8;
            this.mapView.Text = "winformsMap1";
            //
            // ZoomToAFeatureOrFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapView);
            this.Name = "ZoomToAFeatureOrFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ZoomToAFeatureOrFeatures_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Button btnMultipleFeatures;
        private Button btnOneFeature;
        private GroupBox groupBox1;

        #endregion Component Designer generated code
    }
}