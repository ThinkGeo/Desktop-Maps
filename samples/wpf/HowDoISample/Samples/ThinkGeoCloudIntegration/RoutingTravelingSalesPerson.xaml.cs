using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to find an optimized route through a set of waypoints with the ThinkGeo Cloud
    /// </summary>
    public partial class RoutingTravelingSalesPerson
    {
        private RoutingCloudClient _routingCloudClient;
        private Collection<PointShape> _routingWaypoints;

        public RoutingTravelingSalesPerson()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as a feature layer to display the route
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

                // Set the map's unit of measurement to meters (Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create a new feature layer to display the route
                var routingLayer = new InMemoryFeatureLayer();

                // Add point, line, and text styles to display the waypoints, route, and labels for the route
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 3, false);
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateMaskTextStyle("SequenceNumber", new GeoFont("Verdana", 20), GeoBrushes.White, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.Black), GeoColors.Black, 2), 0, 0);
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = TextPlacement.Upper;
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.YOffsetInPixel = -8;
                routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
                routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a feature layer to highlight selected features
                var highlightLayer = new InMemoryFeatureLayer();

                // Add styles to display the highlighted route features
                highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.BrightYellow, 6, GeoColors.Black, 2, false);
                highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add the layer to an overlay, and add the overlay to the mapview
                var routingOverlay = new LayerOverlay();
                routingOverlay.Layers.Add("Routing Layer", routingLayer);
                routingOverlay.Layers.Add("Highlight Layer", highlightLayer);
                MapView.Overlays.Add("Routing Overlay", routingOverlay);

                // Create a set of preset waypoints to route through
                _routingWaypoints = new Collection<PointShape>()
                {
                new PointShape(-10776986.85, 3908680.24),
                new PointShape(-10776836.12, 3912348.04),
                new PointShape(-10778917.01, 3909965.17),
                new PointShape(-10779631.80, 3915721.82),
                new PointShape(-10774900.61, 3912552.36)
                };

                // Add the waypoints to the map to be displayed
                foreach (var routingWaypoint in _routingWaypoints)
                {
                    routingLayer.InternalFeatures.Add(new Feature(routingWaypoint));
                }

                // Set the map extent to Frisco, TX
                MapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

                // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
                _routingCloudClient = new RoutingCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                // Run the routing request
                await RouteWaypointsAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Make a request to the ThinkGeo Cloud for an optimized route
        /// </summary>
        private async Task<CloudRoutingOptimizationResult> GetOptimizedRoute()
        {
            // Set up options for the TSP routing request
            var options = new CloudRoutingOptimizationOptions
            {
                // Enable turn-by-turn so we get turn by turn instructions
                TurnByTurn = true,

                // A specific starting and ending location can be specified, if desired. 
                // For this example, any point can be used as the start and end
                Destination = CloudRoutingTspFixDestinationCoordinate.Any,
                Source = CloudRoutingTspFixSourceCoordinate.Any,

                // The 'roundtrip' option specifies whether the route returns to the starting point or not
                Roundtrip = true
            };

            // Send the TSP routing request
            var optimizedRoutingResult = await _routingCloudClient.GetOptimizedRouteAsync(_routingWaypoints, 3857, options);

            return optimizedRoutingResult;
        }

        /// <summary>
        /// Draw the result of an optimized routing request on the map
        /// </summary>
        private async Task DrawOptimizedRouteAsync(CloudRoutingOptimizationResult optimizedRoutingResult)
        {
            // Get the routing feature layer from the MapView
            var routingLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Routing Layer");

            // Clear the previous features from the routing layer
            routingLayer.InternalFeatures.Clear();

            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing
            var routeSegments = new List<CloudRoutingSegment>();

            // CloudRoutingOptimizationResult.TspResult.VisitSequences is an ordered array of integers
            // Each integer corresponds to the index of the corresponding waypoint in the original set of waypoints passed into the query
            // For example, if the second element in 'VisitSequences' is '3', the second stop on the route is originalWaypointArray[3]

            var index = 0;
            // Add the route visit points and route segments to the map
            foreach (var waypointIndex in optimizedRoutingResult.TspResult.VisitSequences)
            {
                var columnValues = new Dictionary<string, string> {
                    // Get the order of the stops and label the point
                    // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
                    { "SequenceNumber", (index == 0 || index == optimizedRoutingResult.TspResult.VisitSequences.Count - 1 ? "Start/End Point" : "Stop " + index) } };

                var waypoint = _routingWaypoints[waypointIndex];

                // Add the point to the map
                routingLayer.InternalFeatures.Add(new Feature(waypoint, columnValues));

                // Increment the index for labeling purposes
                index++;
            }
            foreach (var route in optimizedRoutingResult.TspResult.Routes)
            {
                routingLayer.InternalFeatures.Add(new Feature(route.Shape));
                routeSegments.AddRange(route.Segments);
            }

            // Set the data source for the list box to the route segments
            LsbRouteSegments.ItemsSource = routeSegments;

            // Set the map extent to the newly displayed route
            routingLayer.Open();
            MapView.CurrentExtent = routingLayer.GetBoundingBox();
            var standardZoomLevelSet = new ZoomLevelSet();
            await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel13.Scale);
            routingLayer.Close();
            await MapView.RefreshAsync();
        }


        /// <summary>
        /// Perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async Task RouteWaypointsAsync()
        {
            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Run the optimized routing query
            var optimizedRoutingResult = await GetOptimizedRoute();

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (optimizedRoutingResult.Exception != null)
            {
                MessageBox.Show(optimizedRoutingResult.Exception.Message, "Error");
                return;
            }

            // Draw the result on the map
            await DrawOptimizedRouteAsync(optimizedRoutingResult);
        }

        /// <summary>
        /// When a route segment is selected in the UI, center the map on it
        /// </summary>
        private async void LsbRouteSegments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            { 
                var routeSegments = (ListBox)sender;
                if (routeSegments.SelectedItem == null) return;
                var highlightLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Highlight Layer");
                highlightLayer.InternalFeatures.Clear();

                // Highlight the selected route segment
                highlightLayer.InternalFeatures.Add(new Feature(((CloudRoutingSegment)routeSegments.SelectedItem).Shape));

                // Zoom to the selected feature and zoom out to an appropriate level
                MapView.CurrentExtent = ((CloudRoutingSegment)routeSegments.SelectedItem).Shape.GetBoundingBox();
                var standardZoomLevelSet = new ZoomLevelSet();
                if (MapView.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel15.Scale);
                }
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }
    }
}