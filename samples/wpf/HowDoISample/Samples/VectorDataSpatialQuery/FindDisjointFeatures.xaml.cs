using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer are disjoint from a shape
    /// </summary>
    public partial class FindDisjointFeatures : IDisposable
    {
        public FindDisjointFeatures()
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
                // Set the Map Unit to meters (used in Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

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

                // Create a feature layer to hold the Frisco zoning data
                var friscoLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

                // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                var projectionConverter = new ProjectionConverter(2276, 3857);
                friscoLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Add a style to use to draw the Frisco zoning polygons
                friscoLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                friscoLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Create a layer to hold the feature we will perform the spatial query against
                var queryLayer = new InMemoryFeatureLayer();
                queryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
                queryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a layer to hold features found by the spatial query
                var highlightLayer = new InMemoryFeatureLayer();
                highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
                highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add each feature layer to its own overlay
                // We do this, so we can control and refresh/redraw each layer individually
                var friscoOverlay = new LayerOverlay();
                friscoOverlay.TileType = TileType.SingleTile;
                friscoOverlay.Layers.Add("FriscoLayer", friscoLayer);
                MapView.Overlays.Add("FriscoOverlay", friscoOverlay);

                var highlightOverlay = new LayerOverlay();
                highlightOverlay.TileType = TileType.SingleTile;
                MapView.Overlays.Add("HighlightOverlay", highlightOverlay);
                highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
                highlightOverlay.Layers.Add("QueryLayer", queryLayer);

                // Add an event to handle new shapes that are drawn on the map
                MapView.TrackOverlay.TrackEnded += OnPolygonDrawn;

                // Set the map extent to the sample shapes
                MapView.CenterPoint = new PointShape(-10778020, 3914693);
                MapView.CurrentScale = 32900; 
                
                MapView.TrackOverlay.TrackMode = TrackMode.Polygon;
                await MapView.RefreshAsync();

                // Add a sample shape to the map for the initial query
                var sampleShape = new PolygonShape("POLYGON((-10780418 3915973,-10780428 3913422,-10775737 3913413,-10775612 3915954,-10780418 3915973))");
                await GetFeaturesDisjointAsync(sampleShape);
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesDisjointAsync(BaseShape polygon)
        {
            // Find the layers we will be modifying in the MapView
            var highlightOverlay = (LayerOverlay)MapView.Overlays["HighlightOverlay"];
            var highlightLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["HighlightLayer"];
            var queryLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["QueryLayer"];

            var friscoOverlay = (LayerOverlay)MapView.Overlays["FriscoOverlay"];
            var friscoLayer = (FeatureLayer)friscoOverlay.Layers["FriscoLayer"];

            // Clear the query shape layer and add the newly drawn shape
            queryLayer.InternalFeatures.Clear();
            queryLayer.InternalFeatures.Add(new Feature(polygon));

            // Perform the spatial query using the drawn shape and highlight features that were found
            friscoLayer.Open();
            var queriedFeatures = friscoLayer.QueryTools.GetFeaturesDisjointed(polygon, ReturningColumnsType.AllColumns);

            highlightLayer.InternalFeatures.Clear();

            foreach (var feature in queriedFeatures)
            {
                highlightLayer.InternalFeatures.Add(feature);
            }

            // Highlight the found features
            await highlightOverlay.RefreshAsync();

            // Update the number of matching features found in the UI
            TxtNumberOfFeaturesFound.Text =
                $"Number of features disjoint from the drawn shape: {queriedFeatures.Count}";

            // Disable map drawing and clear the drawn shape
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            await MapView.TrackOverlay.RefreshAsync();
        }

        /// <summary>
        /// Performs the spatial query when a new polygon is drawn
        /// </summary>
        private void OnPolygonDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            _ = GetFeaturesDisjointAsync((PolygonShape)e.TrackShape);
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