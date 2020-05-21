using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DetermineTheAreaOfAnAreaFeature : UserControl
    {
        public DetermineTheAreaOfAnAreaFeature()
        {
            InitializeComponent();
        }

        private void DetermineTheAreaOfAAreaFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            
            winformsMap1.MapClick += new EventHandler<MapClickMapViewEventArgs>(winformsMap1_MapClick);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");

            // Find the country the user clicked on.
            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, new string[1] { "CNTRY_NAME" });
            worldLayer.Close();

            // Determine the area of the country.
            if (selectedFeatures.Count > 0)
            {
                AreaBaseShape areaShape = (AreaBaseShape)selectedFeatures[0].GetShape();
                double area = areaShape.GetArea(GeographyUnit.DecimalDegree, AreaUnit.SquareKilometers);
                MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "{0} has an area of {1:N0} square kilometers.", selectedFeatures[0].ColumnValues["CNTRY_NAME"].Trim(), area), "Area", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        #region Component Designer generated code

        private Label lblArea;
        private GroupBox gbxDescrition;
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
            this.lblArea = new System.Windows.Forms.Label();
            this.gbxDescrition = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // lblArea
            //
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(31, 27);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(167, 13);
            this.lblArea.TabIndex = 0;
            this.lblArea.Text = "Click on map and select a country";
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.lblArea);
            this.gbxDescrition.Location = new System.Drawing.Point(522, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(215, 57);
            this.gbxDescrition.TabIndex = 4;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Area (DecimalDegree, SquareKilometers)";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 5;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DetermineTheAreaOfAnAreaFeature
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DetermineTheAreaOfAnAreaFeature";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DetermineTheAreaOfAAreaFeature_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}