using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAGridFeatureLayer : UserControl
    {
        public LoadAGridFeatureLayer()
        {
            InitializeComponent();
        }

        private void LoadAGridFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-96.115612954611919, 29.954823851075187, -96.105810851850961, 29.947472274004468);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.Snow);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ClassBreakStyle gridClassBreakStyle = new ClassBreakStyle("CellValue");

            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 0, 255, 0)))));

            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(SampleHelper.Get("PhValues.grd"));
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
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
            // LoadAGridFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.winformsMap1);
            this.Name = "LoadAGridFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadAGridFeatureLayer_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}