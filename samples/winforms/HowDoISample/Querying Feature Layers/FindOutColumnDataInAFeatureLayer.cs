using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindOutColumnDataInAFeatureLayer : UserControl
    {
        public FindOutColumnDataInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void FindOutColumnDataInAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-128.31093750000002, 95.25, 131.84531249999998, -63.65625);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.MapClick += new EventHandler<MapClickMapViewEventArgs>(winformsMap1_MapClick);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");

            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, new string[2] { "CNTRY_NAME", "POP_CNTRY" });
            worldLayer.Close();

            if (selectedFeatures.Count > 0)
            {
                dgridFeatures.DataSource = (DataTable)FeatureSource.ConvertToDataTable(selectedFeatures, new string[] { "CNTRY_NAME", "POP_CNTRY" });
            }
        }

        #region Component Designer generated code

        private DataGridView dgridFeatures;
        private Label label1;
        private GroupBox groupBox1;
        private MapView winformsMap1;
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
            this.dgridFeatures = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // dgridFeatures
            //
            this.dgridFeatures.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridFeatures.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgridFeatures.Location = new System.Drawing.Point(0, 451);
            this.dgridFeatures.Name = "dgridFeatures";
            this.dgridFeatures.ReadOnly = true;
            this.dgridFeatures.Size = new System.Drawing.Size(740, 77);
            this.dgridFeatures.TabIndex = 36;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click on the country to show its population.";
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(519, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 44);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 452);
            this.winformsMap1.TabIndex = 38;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindOutColumnDataInAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgridFeatures);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindOutColumnDataInAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindOutColumnDataInAFeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}