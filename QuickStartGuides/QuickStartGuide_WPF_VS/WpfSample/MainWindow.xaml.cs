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

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {            
            mapView.MapUnit = GeographyUnit.Meter;
            // Add a base map overlay.
            var baseOverlay = new ThinkGeoCloudVectorMapsOverlay("USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~", "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~", ThinkGeoCloudVectorMapsMapType.Light);
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
            // Have set the default to have 20 zoom levels in the layer and applied the style to zoomlevel01, which can also be applied to other zoomlevels.
            capitalLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = capitalStyle;
            capitalLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // The SHP file uses the Decimal Degrees projection, while the map uses the Spherical Mercator projection.
            // To ensure compatibility, it is necessary to convert the SHP files from Decimal Degrees projection to the map's Spherical Mercator projection.
            capitalLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(Projection.GetDecimalDegreesProjString(), Projection.GetSphericalMercatorProjString());

            // Create an overlay to add the layer to and add that overlay to the map.
            var customDataOverlay = new LayerOverlay();
            customDataOverlay.Layers.Add(capitalLayer);
            mapView.Overlays.Add(customDataOverlay);

            mapView.Refresh(); 
        }
    }
}
