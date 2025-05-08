using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class WFS : UserControl
    {
        public WFS()
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

            var helsinkiParcelsLayer = new WfsV2FeatureLayer("https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows", "cp:CadastralParcel")
            {
                TimeoutInSeconds = 500,
            };
            helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            helsinkiParcelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(3067, 3857);

            var overlay = new WfsV2Overlay()
            {
                FeatureLayer = helsinkiParcelsLayer,
                DrawingBulkCount = 500
            };

            mapView.CurrentExtent = new RectangleShape(2775135, 8437158, 2780320, 8433276);
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
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(1377, 743);
            mapView.TabIndex = 0;
            // 
            // NOAAWeatherWarnings
            // 
            Controls.Add(mapView);
            Name = "WFSSever";
            Size = new System.Drawing.Size(1377, 743);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
