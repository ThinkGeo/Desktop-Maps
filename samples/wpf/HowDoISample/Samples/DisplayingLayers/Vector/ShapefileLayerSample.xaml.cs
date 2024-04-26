using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Shapefile Layer on the map
    /// </summary>
    public partial class ShapefileLayerSample : IDisposable
    {
        public ShapefileLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the shapefile layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var parksOverlay = new LayerOverlay();
            MapView.Overlays.Add("parks overlay", parksOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            var parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Add the layer to the overlay we created earlier.
            parksOverlay.Layers.Add("Frisco Parks", parksLayer);

            // Create a dashed pen that we will use below.
            var dashedPen = new GeoPen(GeoColors.Green, 5);
            dashedPen.DashPattern.Add(1);
            dashedPen.DashPattern.Add(1);

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(dashedPen, new GeoSolidBrush(GeoColors.Transparent));
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the current extent of the map to a few parks in the area
            MapView.CurrentExtent = new RectangleShape(-10785086.173498387, 3913489.693302595, -10779919.030415015, 3910065.3144544438);

            await MapView.RefreshAsync();
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