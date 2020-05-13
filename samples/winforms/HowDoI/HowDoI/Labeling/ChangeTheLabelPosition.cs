using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class ChangeTheLabelPosition : UserControl
    {
        public ChangeTheLabelPosition()
        {
            InitializeComponent();
        }

        private void ChangeTheLabelPosition_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer worldLabelLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("STATE_NAME", "Arial", 7, DrawingFontStyles.Bold, GeoColor.FromArgb(255, 91, 91, 91));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.PointPlacement = PointPlacement.Center;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.DuplicateRule = LabelDuplicateRule.NoDuplicateLabels;
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("33", new WorldLabelingCandidate("Kansas State", new PointShape(-91.3969, 28.1016)));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("4", new WorldLabelingCandidate("North Dakota State", new PointShape(-101.09, 51.11)));
            worldLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.LabelPositions.Add("24", new WorldLabelingCandidate("California State", new PointShape(-126.2, 36.27)));

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("StatesLayer", statesLayer);
            staticOverlay.Layers.Add("worldLabelLayer", worldLabelLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-14070784, 6240993, -7458406, 2154936);

            winformsMap1.Refresh();
        }

        private WinformsMap winformsMap1;

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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
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
            // DrawAndLabelANiceLookingRoad
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawAndLabelANiceLookingRoad";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ChangeTheLabelPosition_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}