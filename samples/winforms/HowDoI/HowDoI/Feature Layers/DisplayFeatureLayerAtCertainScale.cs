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
    public class DisplayFeatureLayerAtCertainScale : UserControl
    {
        public DisplayFeatureLayerAtCertainScale()
        {
            InitializeComponent();
        }

        private void DisplayAFeatureyLayerAtCertainScale_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;

            winformsMap1.CurrentExtent = new RectangleShape(-20037508, 17821912, -2226390, -2273031);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.Name = "Countries02";
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            statesLayer.Name = "USStates";
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle = AreaStyles.State1;
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel04.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer citiesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\MajorCities.shp");
            citiesLayer.Name = "MajorCities";
            citiesLayer.ZoomLevelSet.ZoomLevel06.DefaultPointStyle = PointStyles.City1;
            citiesLayer.ZoomLevelSet.ZoomLevel06.DefaultTextStyle = TextStyles.City1("areaname");
            citiesLayer.ZoomLevelSet.ZoomLevel06.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("StatesLayer", statesLayer);
            staticOverlay.Layers.Add("CitiesLayer", citiesLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void btnLow_Click(object sender, EventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            winformsMap1.CurrentExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel06.Scale, winformsMap1.CurrentExtent, GeographyUnit.Meter, winformsMap1.Width, winformsMap1.Height);

            winformsMap1.Refresh();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            winformsMap1.CurrentExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel05.Scale, winformsMap1.CurrentExtent, GeographyUnit.Meter, winformsMap1.Width, winformsMap1.Height);

            winformsMap1.Refresh();
        }

        private void btnHigh_Click(object sender, EventArgs e)
        {
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            winformsMap1.CurrentExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel03.Scale, winformsMap1.CurrentExtent, GeographyUnit.Meter, winformsMap1.Width, winformsMap1.Height);

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
            this.gbxFunctions = new System.Windows.Forms.GroupBox();
            this.btnNormal = new System.Windows.Forms.Button();
            this.btnHigh = new System.Windows.Forms.Button();
            this.btnLow = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.gbxFunctions.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxFunctions
            //
            this.gbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFunctions.Controls.Add(this.btnNormal);
            this.gbxFunctions.Controls.Add(this.btnHigh);
            this.gbxFunctions.Controls.Add(this.btnLow);
            this.gbxFunctions.Location = new System.Drawing.Point(508, 3);
            this.gbxFunctions.Name = "gbxFunctions";
            this.gbxFunctions.Size = new System.Drawing.Size(229, 57);
            this.gbxFunctions.TabIndex = 1;
            this.gbxFunctions.TabStop = false;
            this.gbxFunctions.Text = "Scale";
            //
            // btnNormal
            //
            this.btnNormal.Location = new System.Drawing.Point(85, 19);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(60, 23);
            this.btnNormal.TabIndex = 2;
            this.btnNormal.Text = "Normal";
            this.btnNormal.UseVisualStyleBackColor = true;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            //
            // btnHigh
            //
            this.btnHigh.Location = new System.Drawing.Point(157, 19);
            this.btnHigh.Name = "btnHigh";
            this.btnHigh.Size = new System.Drawing.Size(60, 23);
            this.btnHigh.TabIndex = 1;
            this.btnHigh.Text = "High";
            this.btnHigh.UseVisualStyleBackColor = true;
            this.btnHigh.Click += new System.EventHandler(this.btnHigh_Click);
            //
            // btnLow
            //
            this.btnLow.Location = new System.Drawing.Point(16, 19);
            this.btnLow.Name = "btnLow";
            this.btnLow.Size = new System.Drawing.Size(60, 23);
            this.btnLow.TabIndex = 0;
            this.btnLow.Text = "Low";
            this.btnLow.UseVisualStyleBackColor = true;
            this.btnLow.Click += new System.EventHandler(this.btnLow_Click);
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
            // DisplayFeatureLayerAtCertainScale
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayFeatureLayerAtCertainScale";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayAFeatureyLayerAtCertainScale_Load);
            this.gbxFunctions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private GroupBox gbxFunctions;
        private Button btnHigh;
        private Button btnLow;
        private Button btnNormal;

        #endregion Component Designer generated code
    }
}