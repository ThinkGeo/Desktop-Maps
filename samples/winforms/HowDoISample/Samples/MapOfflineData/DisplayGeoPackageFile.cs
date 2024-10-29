using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayGeoPackageFile : UserControl
    {
        public DisplayGeoPackageFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the Cloud Aerial Overlay as the base overlay 
            var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1
            };
            mapView.Overlays.Add(cloudOverlay);

            // Creat a new layerOverlay to hold the gdalFeatureLayers
            var layerOverlay = new LayerOverlay();
            var projectionConverter = new ProjectionConverter(26910, 3857);
            projectionConverter.Open();

            // Create the gdalFeatureLayers
            var gdalFeatureLayer = new GdalFeatureLayer(@"../../../Data/GeoPackage/mora_surficial_geology.gpkg");
            gdalFeatureLayer.FeatureSource.ProjectionConverter = projectionConverter;
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.LightRed, 4);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(128, GeoColors.LightSteelBlue), 2F, GeoColors.Black, 2F, false);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(64, GeoColors.LightGreen), GeoColors.Black, 1);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(gdalFeatureLayer);

            mapView.Overlays.Add(layerOverlay);

            gdalFeatureLayer.Open();
            mapView.CurrentExtent = gdalFeatureLayer.GetBoundingBox();

            await mapView.RefreshAsync();

            projectionConverter.Close();
            gdalFeatureLayer.Close();
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(950, 575);
            this.mapView.TabIndex = 0;
            // 
            // DisplayGeoPackageFile
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayGeoPackageFile";
            this.Size = new System.Drawing.Size(950, 575);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
