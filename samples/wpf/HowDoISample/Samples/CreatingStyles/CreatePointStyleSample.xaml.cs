using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style point data using a PointStyle
    /// </summary>
    public partial class CreatePointStyleSample : UserControl, IDisposable
    {
        public CreatePointStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, load Frisco Hotels shapefile data and add it to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project the layer's data to match the projection of the map
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            layerOverlay.Layers.Add("hotels",hotelsLayer);

            // Add the overlay to the map
            mapView.Overlays.Add("hotels",layerOverlay);

            pointSymbol.IsChecked = true;

            mapView.Refresh();
        }

        /// <summary>
        /// Create a pointStyle using a PointSymbol and add it to the Hotels layer
        /// </summary>
        private void PointSymbol_OnChecked(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
                ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

                // Create a point style
                var pointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Blue, new GeoPen(GeoBrushes.White, 2));

                // Add the point style to the collection of custom styles for ZoomLevel 1.
                hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
                hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                layerOverlay.Refresh();
            }
        }

        /// <summary>
        /// Create a pointStyle using an icon image and add it to the Hotels layer
        /// </summary>
        private void Icon_OnChecked(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
            ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

            // Create a point style
            var pointStyle = new PointStyle(new GeoImage(@"./Resources/hotel_icon.png"))
            {
                ImageScale = .25
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            layerOverlay.Refresh();
        }


        /// <summary>
        /// Create a pointStyle using a font symbol and add it to the Hotels layer
        /// </summary>
        private void Symbol_Checked(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
            ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

            // Create a point style
            var symbolPointStyle = new PointStyle(new GeoFont("Verdana", 16, DrawingFontStyles.Bold), "@", GeoBrushes.Black)
            {
                Mask = new AreaStyle(GeoBrushes.White),
                MaskType = MaskType.Circle
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(symbolPointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            layerOverlay.Refresh();
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
