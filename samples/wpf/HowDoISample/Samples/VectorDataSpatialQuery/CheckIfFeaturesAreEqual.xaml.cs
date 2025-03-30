using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer are topologically equal to a shape
    /// </summary>
    public partial class CheckIfFeaturesAreEqual : IDisposable
    {
        public CheckIfFeaturesAreEqual()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco zoning data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the Map Unit to meters (used in Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create a feature layer to hold and display the zoning data
                var zoningLayer = new InMemoryFeatureLayer();

                // Add a style to use to draw the Frisco zoning polygons
                zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Import the features from the Frisco zoning data shapefile
                var zoningDataFeatureSource = new ShapeFileFeatureSource(@"./Data/Shapefile/Zoning.shp");

                // Create a ProjectionConverter to convert the shapefile data from North Central Texas (2276) to Spherical Mercator (3857)
                var projectionConverter = new ProjectionConverter(3857, 2276);

                // For this sample, we have to reproject the features before adding them to the feature layer
                // This is because the topological equality query often does not work when used on a feature layer with a ProjectionConverter, due to rounding issues between projections
                zoningDataFeatureSource.Open();
                projectionConverter.Open();
                foreach (var zoningFeature in zoningDataFeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns))
                {
                    var reprojectedFeature = projectionConverter.ConvertToInternalProjection(zoningFeature);
                    zoningLayer.InternalFeatures.Add(reprojectedFeature);
                }
                zoningDataFeatureSource.Close();
                projectionConverter.Close();

                // Create a layer to hold the feature we will perform the spatial query against
                var queryFeatureLayer = new InMemoryFeatureLayer();
                queryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
                queryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a layer to hold features found by the spatial query
                var highlightedFeaturesLayer = new InMemoryFeatureLayer();
                highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
                highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add each feature layer to its own overlay
                // We do this, so we can control and refresh/redraw each layer individually
                var zoningOverlay = new LayerOverlay();
                zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
                MapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

                var queryFeaturesOverlay = new LayerOverlay();
                queryFeaturesOverlay.Layers.Add("Query Feature", queryFeatureLayer);
                MapView.Overlays.Add("Query Features Overlay", queryFeaturesOverlay);

                var highlightedFeaturesOverlay = new LayerOverlay();
                highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
                MapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

                // Add a sample shape to the map for the initial query
                // To ensure topological equality for this sample, we create a new shape using the same geometry as an existing feature
                zoningLayer.Open();
                var sampleShape = zoningLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).First().GetShape();
                zoningLayer.Close();
                await GetFeaturesEqualAsync(sampleShape);

                // Set the map extent to the sample shape
                MapView.CenterPoint = new PointShape(-10776520,3919250);
                MapView.CurrentScale = 18060;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Perform the 'Equals' spatial query using the layer's QueryTools
        /// </summary>
        private static IEnumerable<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.QueryTools.GetFeaturesTopologicalEqual(shape, ReturningColumnsType.AllColumns);
            layer.Close();

            return features;
        }

        /// <summary>
        /// Highlight the features that were found by the spatial query
        /// </summary>
        private async Task HighlightQueriedFeaturesAsync(IEnumerable<Feature> features)
        {
            // Find the layers we will be modifying in the MapView dictionary
            var highlightedFeaturesOverlay = (LayerOverlay)MapView.Overlays["Highlighted Features Overlay"];
            var highlightedFeaturesLayer = (InMemoryFeatureLayer)highlightedFeaturesOverlay.Layers["Highlighted Features"];

            // Clear the currently highlighted features
            highlightedFeaturesLayer.Open();
            highlightedFeaturesLayer.InternalFeatures.Clear();

            // Add new features to the layer
            var enumerable = features as Feature[] ?? features.ToArray();
            foreach (var feature in enumerable)
            {
                highlightedFeaturesLayer.InternalFeatures.Add(feature);
            }
            highlightedFeaturesLayer.Close();

            // Refresh the overlay so the layer is redrawn
            await highlightedFeaturesOverlay.RefreshAsync();

            // Update the number of matching features found in the UI
            TxtNumberOfFeaturesFound.Text =
                $"Number of features topologically equal to the drawn shape: {enumerable.Length}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesEqualAsync(BaseShape shape)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeaturesOverlay = (LayerOverlay)MapView.Overlays["Query Features Overlay"];
            var queryFeatureLayer = (InMemoryFeatureLayer)queryFeaturesOverlay.Layers["Query Feature"];
            var zoningLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query shape layer and add the newly drawn shape
            queryFeatureLayer.InternalFeatures.Clear();
            queryFeatureLayer.InternalFeatures.Add(new Feature(shape));
            await queryFeaturesOverlay.RefreshAsync();

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(shape, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            MapView.TrackOverlay.TrackMode = TrackMode.None;
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
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