using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style polygon data using an AreaStyle
    /// </summary>
    public partial class CreateAreaStyleSample : IDisposable
    {
        public CreateAreaStyleSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            // Create a layer with polygon data
            var friscoSubdivisions = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp")
            {
                FeatureSource =
                {
                    // Project the layer's data to match the projection of the map
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoSubdivisions);

            // Add the overlay to the map
            MapView.Overlays.Add(layerOverlay);

            // Add the area style to the historicSites layer
            AddAreaStyle(friscoSubdivisions);
            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Create a areaStyle and add it to the Historic Sites layer
        /// </summary>
        private static void AddAreaStyle(FeatureLayer layer)
        {
            // Create an area style
            var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(new GeoColor(128, GeoColors.ForestGreen)));

            // Add the area style to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}