using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayGeoPackageFile
    /// </summary>
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
            mapView = new MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(950, 575);
            mapView.TabIndex = 0;
            // 
            // DisplayGeoPackageFile
            // 
            Controls.Add(mapView);
            Name = "DisplayGeoPackageFile";
            Size = new System.Drawing.Size(950, 575);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
