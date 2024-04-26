using System.Windows;
using ThinkGeo.Core;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void mapView_Loaded(object sender, RoutedEventArgs e)
        {            
            mapView.MapUnit = GeographyUnit.Meter;
            // Add a base map overlay.
            var baseOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~",
                "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the base overlay, passing in the location and an ID to distinguish the cache. 
            baseOverlay.TileCache = new FileRasterTileCache(@".\cache", "basemap");
            mapView.Overlays.Add(baseOverlay);

            // Set the extent of the mapView
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

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

            await mapView.RefreshAsync(); 
        }
    }
}
