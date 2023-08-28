using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to find an optimized route through a set of waypoints with the ThinkGeo Cloud
    /// </summary>
    public partial class RoutingTSPCloudServicesSample : UserControl, IDisposable
    {
        private RoutingCloudClient routingCloudClient;
        private Collection<PointShape> routingWaypoints;

        public RoutingTSPCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as a feature layer to display the route
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the route
            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();

            // Add point, line, and text styles to display the waypoints, route, and labels for the route
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 3, false);
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateMaskTextStyle("SequenceNumber", new GeoFont("Verdana", 20), GeoBrushes.White, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.Black), GeoColors.Black, 2), 0, 0);
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = TextPlacement.Upper;
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.YOffsetInPixel = -8;
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a feature layer to highlight selected features
            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();

            // Add styles to display the highlighted route features
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.BrightYellow, 6, GeoColors.Black, 2, false);
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to an overlay, and add the overlay to the mapview
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("Routing Layer", routingLayer);
            routingOverlay.Layers.Add("Highlight Layer", highlightLayer);
            mapView.Overlays.Add("Routing Overlay", routingOverlay);

            // Create a set of preset waypoints to route through
            routingWaypoints = new Collection<PointShape>()
            {
                new PointShape(-10776986.85, 3908680.24),
                new PointShape(-10776836.12, 3912348.04),
                new PointShape(-10778917.01, 3909965.17),
                new PointShape(-10779631.80, 3915721.82),
                new PointShape(-10774900.61, 3912552.36)
            };

            // Add the waypoints to the map to be displayed
            foreach(PointShape routingWaypoint in routingWaypoints)
            {
                routingLayer.InternalFeatures.Add(new Feature(routingWaypoint));
            }

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            routingCloudClient = new RoutingCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Run the routing request
            await RouteWaypointsAsync();
        }

        /// <summary>
        /// Make a request to the ThinkGeo Cloud for an optimized route
        /// </summary>
        private async Task<CloudRoutingOptimizationResult> GetOptimizedRoute()
        {
            // Set up options for the TSP routing request
            CloudRoutingOptimizationOptions options = new CloudRoutingOptimizationOptions();

            // Enable turn-by-turn so we get turn by turn instructions
            options.TurnByTurn = true;

            // A specific starting and ending location can be specified, if desired. 
            // For this example, any point can be used as the start and end
            options.Destination = CloudRoutingTspFixDestinationCoordinate.Any;
            options.Source = CloudRoutingTspFixSourceCoordinate.Any;

            // The 'roundtrip' option specifies whether the route returns to the starting point or not
            options.Roundtrip = true;

            // Send the TSP routing request
            CloudRoutingOptimizationResult optimizedRoutingResult = await routingCloudClient.GetOptimizedRouteAsync(routingWaypoints, 3857, options);

            return optimizedRoutingResult;
        }

        /// <summary>
        /// Draw the result of an optimized routing request on the map
        /// </summary>
        private async Task DrawOptimizedRouteAsync(CloudRoutingOptimizationResult optimizedRoutingResult)
        {
            // Get the routing feature layer from the MapView
            InMemoryFeatureLayer routingLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Routing Layer");

            // Clear the previous features from the routing layer
            routingLayer.InternalFeatures.Clear();

            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing
            List<CloudRoutingSegment> routeSegments = new List<CloudRoutingSegment>();

            // CloudRoutingOptimizationResult.TspResult.VisitSequences is an ordered array of integers
            // Each integer corresponds to the index of the corresponding waypoint in the original set of waypoints passed into the query
            // For example, if the second element in 'VisitSequences' is '3', the second stop on the route is originalWaypointArray[3]

            int index = 0;
            // Add the route visit points and route segments to the map
            foreach (int waypointIndex in optimizedRoutingResult.TspResult.VisitSequences)
            {
                Dictionary<string, string> columnValues = new Dictionary<string, string>();

                // Get the order of the stops and label the point
                // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
                columnValues.Add("SequenceNumber", (index == 0 || index == optimizedRoutingResult.TspResult.VisitSequences.Count - 1 ? "Start/End Point" : "Stop " + index));
                PointShape waypoint = routingWaypoints[waypointIndex];

                // Add the point to the map
                routingLayer.InternalFeatures.Add(new Feature(waypoint, columnValues));

                // Increment the index for labeling purposes
                index++;
            }
            foreach (CloudRoutingRoute route in optimizedRoutingResult.TspResult.Routes)
            {
                routingLayer.InternalFeatures.Add(new Feature(route.Shape));
                routeSegments.AddRange(route.Segments);
            }

            // Set the data source for the list box to the route segments
            lsbRouteSegments.ItemsSource = routeSegments;

            // Set the map extent to the newly displayed route
            routingLayer.Open();
            mapView.CurrentExtent = routingLayer.GetBoundingBox();
            ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
            await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel13.Scale);
            routingLayer.Close();
            await mapView.RefreshAsync();
        }


        /// <summary>
        /// Perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async Task RouteWaypointsAsync()
        {
            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // Run the optimized routing query
            CloudRoutingOptimizationResult optimizedRoutingResult = await GetOptimizedRoute();

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

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
        private async void lsbRouteSegments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox routeSegments = (ListBox)sender;
            if (routeSegments.SelectedItem != null)
            {
                InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Highlight Layer");
                highlightLayer.InternalFeatures.Clear();

                // Highlight the selected route segment
                highlightLayer.InternalFeatures.Add(new Feature(((CloudRoutingSegment)routeSegments.SelectedItem).Shape));

                // Zoom to the selected feature and zoom out to an appropriate level
                mapView.CurrentExtent = ((CloudRoutingSegment)routeSegments.SelectedItem).Shape.GetBoundingBox();
                ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
                if (mapView.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel15.Scale);
                }
                await mapView.RefreshAsync();
            }
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
