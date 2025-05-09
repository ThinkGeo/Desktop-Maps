using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer overlap a shape
    /// </summary>
    public partial class FindOverlappingFeatures : IDisposable
    {
        public FindOverlappingFeatures()
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

                // Create a feature layer to hold the Frisco zoning data
                var zoningLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

                // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                var projectionConverter = new ProjectionConverter(2276, 3857);
                zoningLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Add a style to use to draw the Frisco zoning polygons
                zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Set the map extent to Frisco, TX
                MapView.CenterPoint = new PointShape(-10777860, 3914200);
                MapView.CurrentScale = 31300;

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
                zoningOverlay.TileType = TileType.SingleTile;
                zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
                MapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

                var queryFeaturesOverlay = new LayerOverlay();
                queryFeaturesOverlay.TileType = TileType.SingleTile;
                queryFeaturesOverlay.Layers.Add("Query Feature", queryFeatureLayer);
                MapView.Overlays.Add("Query Features Overlay", queryFeaturesOverlay);

                var highlightedFeaturesOverlay = new LayerOverlay();
                highlightedFeaturesOverlay.TileType = TileType.SingleTile;
                highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
                MapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

                // Add an event to handle new shapes that are drawn on the map
                MapView.TrackOverlay.TrackEnded += OnPolygonDrawn;

                // Add a sample shape to the map for the initial query
                var sampleShape = new PolygonShape("POLYGON((-10779549.4792414 3915352.92061116,-10777495.2341177 3915859.31592073,-10776214.913901 3914827.41589883,-10776081.1491022 3913384.66699796,-10777906.0831424 3912553.41431997,-10779702.3532971 3914110.81876263,-10779549.4792414 3915352.92061116))");
                await GetFeaturesOverlapsAsync(sampleShape);

                // Set the map extent to the sample shapes
                var sampleShapeBBox = sampleShape.GetBoundingBox();
                MapView.CenterPoint = sampleShapeBBox.GetCenterPoint();
                var MapScale = MapUtil.GetScale(MapView.MapUnit, sampleShapeBBox, MapView.MapWidth, MapView.MapHeight);
                MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Perform the 'Overlaps' spatial query using the layer's QueryTools
        /// </summary>
        private static IEnumerable<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.QueryTools.GetFeaturesOverlapping(shape, ReturningColumnsType.AllColumns);
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
            TxtNumberOfFeaturesFound.Text = $"Number of features overlapping the drawn shape: {enumerable.Length}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesOverlapsAsync(BaseShape polygon)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeaturesOverlay = (LayerOverlay)MapView.Overlays["Query Features Overlay"];
            var queryFeatureLayer = (InMemoryFeatureLayer)queryFeaturesOverlay.Layers["Query Feature"];
            var zoningLayer = (ShapeFileFeatureLayer)MapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query shape layer and add the newly drawn shape
            queryFeatureLayer.InternalFeatures.Clear();
            queryFeatureLayer.InternalFeatures.Add(new Feature(polygon));
            await queryFeaturesOverlay.RefreshAsync();

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(polygon, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            MapView.TrackOverlay.TrackMode = TrackMode.None;
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            await MapView.TrackOverlay.RefreshAsync();
        }

        /// <summary>
        /// Performs the spatial query when a new polygon is drawn
        /// </summary>
        private void OnPolygonDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            _ = GetFeaturesOverlapsAsync((PolygonShape)e.TrackShape);
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks on the map without panning
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (MapView.TrackOverlay.TrackMode != TrackMode.Polygon)
            {
                // Set the drawing mode to 'Polygon'
                MapView.TrackOverlay.TrackMode = TrackMode.Polygon;
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