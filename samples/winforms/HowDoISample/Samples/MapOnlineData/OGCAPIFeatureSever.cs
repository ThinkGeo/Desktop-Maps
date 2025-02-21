using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class OGCAPIFeatureSever : UserControl
    {
        public OGCAPIFeatureSever()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var ignLayer = new OgcApiFeatureLayer("https://api-features.ign.es", "namedplace")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };

            // Create a new text style and set various settings to make it look good.
            var ignNamedPlacesTextStyle = new TextStyle("etiqueta", new GeoFont("Arial", 14), GeoBrushes.DarkRed)
            {
                MaskType = MaskType.RoundedCorners,
                OverlappingRule = LabelOverlappingRule.NoOverlapping,
                Mask = new AreaStyle(GeoBrushes.WhiteSmoke),
                SuppressPartialLabels = true,
                YOffsetInPixel = -12
            };

            ignLayer.ZoomLevelSet.ZoomLevel13.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Black, 10);
            ignLayer.ZoomLevelSet.ZoomLevel13.DefaultTextStyle = ignNamedPlacesTextStyle;
            ignLayer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var overlay = new OgcApiFeaturesOverlay()
            {
                FeatureLayer = ignLayer,
                DrawingBulkCount = 100
            };

            mapView.CurrentExtent = new RectangleShape(237159, 5069993, 247529, 5062228);
            mapView.Overlays.Add("LayerOverlay", overlay);

            await mapView.RefreshAsync();
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
            mapView.Size = new System.Drawing.Size(1377, 743);
            mapView.TabIndex = 0;            
            // 
            // NOAAWeatherWarnings
            // 
            Controls.Add(mapView);
            Name = "OGCAPIFeatureSever";
            Size = new System.Drawing.Size(1377, 743);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
