using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style line data using a LineStyle
    /// </summary>
    public partial class CreateLineStyleSample : UserControl
    {        
        public CreateLineStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, load Frisco Streets shapefile data and add it to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudRasterMapsMapType.Aerial);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);             

            // Create a layer with line data
            ShapeFileFeatureLayer friscoStreets = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Streets.shp");

            // Project the layer's data to match the projection of the map
            friscoStreets.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoStreets);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Add the line style to the historicSites layer
            AddLineStyle(friscoStreets);
        }

        /// <summary>
        /// Create a lineStyle and add it to the Frisco Streets layer
        /// </summary>
        private void AddLineStyle(ShapeFileFeatureLayer layer)
        {
            // Create a line style
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 4), new GeoPen(GeoBrushes.WhiteSmoke, 2));

            // Add the line style to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
    }
}
