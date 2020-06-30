using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to cluster point data dynamically using a ClusterPointStyle
    /// </summary>
    public partial class CreateClusterPointStyleSample : UserControl
    {
        private readonly ShapeFileFeatureLayer coyoteSightings = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Frisco_Coyote_Sightings.shp");

        public CreateClusterPointStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project the layer's data to match the projection of the map
            coyoteSightings.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(coyoteSightings);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Apply HeatStyle
            AddClusterPointStyle();

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10812042.5236828, 3942445.36497713, -10748599.7905585, 3887792.89005685);
        }

        /// <summary>
        /// Create and add a cluster style to the coyote layer
        /// </summary>
        private void AddClusterPointStyle()
        {
            // Create the point style that will serve as the basis of the cluster style
            var pointStyle = new PointStyle(new GeoImage(@"../../../Resources/coyote_paw.png"))
            {
                ImageScale = .25,
                Mask = new AreaStyle(GeoBrushes.White),
                MaskType = MaskType.RoundedCorners
            };

            // Create a text style that will display the number of features within a clustered point
            var textStyle = new TextStyle("FeatureCount", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DimGray)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                YOffsetInPixel = 12
            };

            // Create the cluster point style
            var clusterPointStyle = new ClusterPointStyle(pointStyle, textStyle)
            {
                MinimumFeaturesPerCellToCluster = 2
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map.
            coyoteSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
    }
}
