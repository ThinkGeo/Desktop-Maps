using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        private MapView mapView;
        public Form1()
        {
            InitializeComponent();
            mapView = new ThinkGeo.UI.WinForms.MapView();
            mapView.Size = new System.Drawing.Size(800, 450);
            mapView.Dock = DockStyle.Fill;
            Controls.Add(this.mapView);
            this.Load += new System.EventHandler(this.Form_Load);
        }

        private async void Form_Load(object? sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = "AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~",
                ClientSecret = "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~",
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Add a shapefile layer with point style.
            var capitalLayer = new ShapeFileFeatureLayer(@"..\..\..\AppData\WorldCapitals.shp");
            var capitalStyle = new PointStyle()
            {
                SymbolType = PointSymbolType.Circle,
                SymbolSize = 8,
                FillBrush = new GeoSolidBrush(GeoColors.White),
                OutlinePen = new GeoPen(GeoColors.Black, 2)
            };
            // Each layer has 20 preset zoomlevels. Here we set the capitalStyle for ZoomLevel01 and apply the style to the other preset zoomlevels.
            capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = capitalStyle;
            capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // The shapefile is in Decimal Degrees, while the base overlay is in Spherical Mercator.
            // It's why the shapefile needs to be reprojected to match the coordinate system of the base overlay.
            capitalLayer.FeatureSource.ProjectionConverter =
                new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            // Add the layer to an overlay, add that overlay to the map.
            var customDataOverlay = new LayerOverlay();
            customDataOverlay.Layers.Add(capitalLayer);
            mapView.Overlays.Add(customDataOverlay);

            // Set the map extent
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

            await mapView.RefreshAsync();
        }
    }
}
