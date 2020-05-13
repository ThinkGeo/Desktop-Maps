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
    public class DrawCurvedLabels : UserControl
    {
        public DrawCurvedLabels()
        {
            InitializeComponent();
        }

        private void DrawCurvedLabels_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));

            ShapeFileFeatureLayer austinStreetsShapeLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\austinstreets.shp");
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            austinStreetsShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyles.LocalRoad1);

            ShapeFileFeatureLayer austinStreetsLabelLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\austinstreets.shp");
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            TextStyle textStyle = TextStyles.LocalRoad1("FENAME");
            textStyle.TextLineSegmentRatio = double.MaxValue;
            textStyle.SplineType = SplineType.StandardSplining;
            austinStreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("AustinStreetsShapeLayer", austinStreetsShapeLayer);
            staticOverlay.Layers.Add("AustinStreetsLabelLayer", austinStreetsLabelLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-97.6881803712033, 30.3177912428115, -97.6723016938352, 30.3064615919325);

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
            this.Load += new System.EventHandler(this.DrawCurvedLabels_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}