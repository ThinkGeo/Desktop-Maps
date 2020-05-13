using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class DetermineTheLengthOfALineFeature : UserControl
    {
        public DetermineTheLengthOfALineFeature()
        {
            InitializeComponent();
        }

        private void DetermineTheLengthOfALineFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-97.745827547760484, 30.297694742808115, -97.728208518132988, 30.285123327073894);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));
            winformsMap1.MapClick += new EventHandler<MapClickWinformsMapEventArgs>(winformsMap1_MapClick);

            ShapeFileFeatureLayer roadLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\austinstreets.shp");
            roadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            roadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.LocalRoad1;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("RoadLayer", roadLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("RoadLayer");

            // Find the road the user clicked on.
            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.DecimalDegree, 1, new string[1] { "FENAME" });
            worldLayer.Close();

            //Determine the length of the road.
            if (selectedFeatures.Count > 0)
            {
                LineBaseShape lineShape = (LineBaseShape)selectedFeatures[0].GetShape();
                double length = lineShape.GetLength(GeographyUnit.DecimalDegree, DistanceUnit.Meter);
                MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "{0} has a length of {1:F2} meters.", selectedFeatures[0].ColumnValues["FENAME"].Trim(), length), "Length", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        #region Component Designer generated code

        private Label lblLength;
        private GroupBox gbxDescrition;
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
            this.lblLength = new System.Windows.Forms.Label();
            this.gbxDescrition = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // lblLength
            //
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(6, 26);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(191, 13);
            this.lblLength.TabIndex = 0;
            this.lblLength.Text = "Click on a road to get the length.";
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.lblLength);
            this.gbxDescrition.Location = new System.Drawing.Point(535, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(202, 57);
            this.gbxDescrition.TabIndex = 5;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Length (DecimalDegree, Meter)";
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
            // DetermineTheLengthOfALineFeature
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DetermineTheLengthOfALineFeature";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DetermineTheLengthOfALineFeature_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}