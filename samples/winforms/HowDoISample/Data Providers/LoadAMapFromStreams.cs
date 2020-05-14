using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAMapFromStreams : UserControl
    {
        public LoadAMapFromStreams()
        {
            InitializeComponent();
        }

        private void LoadAMapFromStreams_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer shapeFileLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            ((ShapeFileFeatureSource)shapeFileLayer.FeatureSource).StreamLoading += new EventHandler<StreamLoadingEventArgs>(LoadAMapFromStreams_StreamLoading);
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", shapeFileLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void LoadAMapFromStreams_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            string fileName = Path.GetFileName(e.AlternateStreamName);
            string ext = Path.GetExtension(fileName);
            fileName = SampleHelper.Get(fileName);
            fileName = Path.ChangeExtension(fileName, ext);
            e.AlternateStream = new FileStream(fileName, (FileMode)e.FileMode, (FileAccess)e.ReadWriteMode);
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
            // LoadAMapFromStreams
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "LoadAMapFromStreams";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadAMapFromStreams_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}