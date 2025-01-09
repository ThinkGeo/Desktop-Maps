using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GPX Layer on the map
    /// </summary>
    public partial class DisplayGPXFile : IDisposable
    {
        public DisplayGPXFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the GPX layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
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
                var gpxOverlay = new LayerOverlay();
                MapView.Overlays.Add(gpxOverlay);

                // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
                var gpxLayer = new GpxFeatureLayer(@"./Data/Gpx/Hike_Bike.gpx")
                {
                    FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
                };

                // Add the layer to the overlay we created earlier.
                gpxOverlay.Layers.Add("Hike Bike Trails", gpxLayer);

                // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
                gpxLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Black);
                gpxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Open the layer and set the map view current extent to the bounding box of the layer.  
                gpxLayer.Open();
                MapView.CurrentExtent = gpxLayer.GetBoundingBox();

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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