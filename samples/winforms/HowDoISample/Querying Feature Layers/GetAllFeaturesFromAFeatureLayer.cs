using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetAllFeaturesFromAFeatureLayer : UserControl
    {
        public GetAllFeaturesFromAFeatureLayer()
        {
            InitializeComponent();
        }

        private void GetAllFeaturesFromAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-116.18203125000002, 77.671875, 143.97421874999998, -60.4921875);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void btnGetAllFeatures_Click(object sender, EventArgs e)
        {
            string[] returningColumns = new string[] { "CNTRY_NAME", "CURR_TYPE", "RECID" };

            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            Collection<Feature> allFeaturs = worldLayer.FeatureSource.GetAllFeatures(returningColumns);
            worldLayer.Close();

            dgridFeatures.DataSource = (DataTable)FeatureSource.ConvertToDataTable(allFeaturs, returningColumns);
        }

        #region Component Designer generated code

        private DataGridView dgridFeatures;
        private Button btnGetAllFeatures;
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
            this.btnGetAllFeatures = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).BeginInit();
            this.SuspendLayout();
            //
            // dgridFeatures
            //
            this.dgridFeatures.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridFeatures.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgridFeatures.Location = new System.Drawing.Point(0, 431);
            this.dgridFeatures.Name = "dgridFeatures";
            this.dgridFeatures.ReadOnly = true;
            this.dgridFeatures.Size = new System.Drawing.Size(740, 97);
            this.dgridFeatures.TabIndex = 2;
            //
            // btnGetAllFeatures
            //
            this.btnGetAllFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetAllFeatures.Location = new System.Drawing.Point(621, 405);
            this.btnGetAllFeatures.Name = "btnGetAllFeatures";
            this.btnGetAllFeatures.Size = new System.Drawing.Size(116, 23);
            this.btnGetAllFeatures.TabIndex = 34;
            this.btnGetAllFeatures.Text = "Get All  Features";
            this.btnGetAllFeatures.UseVisualStyleBackColor = true;
            this.btnGetAllFeatures.Click += new System.EventHandler(this.btnGetAllFeatures_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 401);
            this.winformsMap1.TabIndex = 35;
            this.winformsMap1.Text = "winformsMap1";
            //
            // GetAllFeaturesFromAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnGetAllFeatures);
            this.Controls.Add(this.dgridFeatures);
            this.Name = "GetAllFeaturesFromAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.GetAllFeaturesFromAFeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}