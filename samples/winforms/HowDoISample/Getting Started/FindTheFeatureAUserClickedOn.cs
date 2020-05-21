using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class FindTheFeatureAUserClickedOn : UserControl
    {
        public FindTheFeatureAUserClickedOn()
        {
            InitializeComponent();
        }

        private void SelectFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.MapClick += new EventHandler<MapClickMapViewEventArgs>(winformsMap1_MapClick);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");

            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, new string[1] { "CNTRY_NAME" });
            worldLayer.Close();

            if (selectedFeatures.Count > 0)
            {
                string text = "The country you selected is: " + selectedFeatures[0].ColumnValues["CNTRY_NAME"].Trim();
                MessageBox.Show(text, "Note", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        private MapView winformsMap1;

        #region Component Designer generated code

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
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindTheFeatureAUserClickedOn
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindTheFeatureAUserClickedOn";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.SelectFeatures_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}