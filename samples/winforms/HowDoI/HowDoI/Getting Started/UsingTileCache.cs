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
    public class UsingTileCache : UserControl
    {
        public UsingTileCache()
        {
            InitializeComponent();
        }

        private void DisplayMap_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            FileBitmapTileCache bitmapTileCache = new FileBitmapTileCache();
            bitmapTileCache.CacheDirectory = Samples.RootDirectory + @"Data\SampleCacheTiles";
            bitmapTileCache.CacheId = "World02CachedTiles";
            bitmapTileCache.TileAccessMode = TileAccessMode.ReadOnly;
            bitmapTileCache.ImageFormat = TileImageFormat.Png;

            staticOverlay.TileCache = bitmapTileCache;

            winformsMap1.CurrentExtent = new RectangleShape(-15963215, 20037508, 12990985, -13516534);

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
            ThinkGeo.MapSuite.WinForms.EditInteractiveOverlay editInteractiveOverlay1 = new ThinkGeo.MapSuite.WinForms.EditInteractiveOverlay();
            ThinkGeo.MapSuite.WinForms.ExtentInteractiveOverlay extentInteractiveOverlay1 = new ThinkGeo.MapSuite.WinForms.ExtentInteractiveOverlay();
            ThinkGeo.MapSuite.WinForms.TrackInteractiveOverlay trackInteractiveOverlay1 = new ThinkGeo.MapSuite.WinForms.TrackInteractiveOverlay();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.Gray;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            editInteractiveOverlay1.IsVisible = true;
            editInteractiveOverlay1.Name = null;
            this.winformsMap1.EditOverlay = editInteractiveOverlay1;
            extentInteractiveOverlay1.IsVisible = true;
            extentInteractiveOverlay1.Name = null;
            this.winformsMap1.ExtentOverlay = extentInteractiveOverlay1;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            trackInteractiveOverlay1.IsVisible = true;
            trackInteractiveOverlay1.Name = null;
            trackInteractiveOverlay1.TrackMode = ThinkGeo.MapSuite.WinForms.TrackMode.None;
            this.winformsMap1.TrackOverlay = trackInteractiveOverlay1;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.ExtentOverlay.ZoomPercentage = 40;
            //
            // DisplayShapeMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayMap_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}