using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UsingTileCache : UserControl
    {
        public UsingTileCache()
        {
            InitializeComponent();
        }

        private void DisplayMap_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer("SampleData/Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightYellow, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Converts the layer from Decimal Degree projection to Spherical Mercator which is the projection the base map is using. 
            worldLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            FileRasterTileCache bitmapTileCache = new FileRasterTileCache();
            bitmapTileCache.CacheDirectory = Path.Combine(Path.GetTempPath(), "ThinkGeo", "UsingTileCache-WinForms");
            bitmapTileCache.CacheId = "World02CachedTiles";
            bitmapTileCache.ImageFormat = RasterTileFormat.Png;

            staticOverlay.TileCache = bitmapTileCache;

            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);

        }

        private MapView mapView;

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
            ThinkGeo.UI.WinForms.EditInteractiveOverlay editInteractiveOverlay1 = new ThinkGeo.UI.WinForms.EditInteractiveOverlay();
            ThinkGeo.UI.WinForms.ExtentInteractiveOverlay extentInteractiveOverlay1 = new ThinkGeo.UI.WinForms.ExtentInteractiveOverlay();
            ThinkGeo.UI.WinForms.TrackInteractiveOverlay trackInteractiveOverlay1 = new ThinkGeo.UI.WinForms.TrackInteractiveOverlay();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.mapView.BackColor = System.Drawing.Color.Gray;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            editInteractiveOverlay1.IsVisible = true;
            editInteractiveOverlay1.Name = null;
            this.mapView.EditOverlay = editInteractiveOverlay1;
            extentInteractiveOverlay1.IsVisible = true;
            extentInteractiveOverlay1.Name = null;
            this.mapView.ExtentOverlay = extentInteractiveOverlay1;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapUnit = GeographyUnit.DecimalDegree;
            this.mapView.MaximumScale = 80000000000000;
            this.mapView.MinimumScale = 200;
            this.mapView.Name = "winformsMap1";
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.Size = new System.Drawing.Size(740, 528);
            this.mapView.TabIndex = 0;
            this.mapView.Text = "winformsMap1";
            trackInteractiveOverlay1.IsVisible = true;
            trackInteractiveOverlay1.Name = null;
            trackInteractiveOverlay1.TrackMode = ThinkGeo.UI.WinForms.TrackMode.None;
            this.mapView.TrackOverlay = trackInteractiveOverlay1;
            // this.winformsMap1.ZoomLevelSnapping = ThinkGeo.UI.WinForms.ZoomLevelSnappingMode.Default;
            this.mapView.ExtentOverlay.ZoomPercentage = 40;
            //
            // DisplayShapeMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mapView);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayMap_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}