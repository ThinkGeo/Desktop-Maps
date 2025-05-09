using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to route through a set of waypoints with the ThinkGeo Cloud
    /// </summary>
    public partial class Routing : IDisposable
    {
        private RoutingCloudClient _routingCloudClient;

        public Routing()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as a feature layer to display the route
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Add styles to display the route and waypoints
            // Add a point, line, and text style to the layer. These styles control how the route will be drawn and labeled
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

            // Add the layers to an overlay, and add the overlay to the mapview
            var routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("Routing Layer", routingLayer);
            routingOverlay.Layers.Add("Highlight Layer", highlightLayer);
            MapView.Overlays.Add("Routing Overlay", routingOverlay);

            // Set the map extent to Frisco, TX
            MapView.CenterPoint = new PointShape(-10778720, 3915154);
            MapView.CurrentScale = 202090;

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            _routingCloudClient = new RoutingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            // Run the routing request
            _ = RouteWaypointsAsync();
        }

        /// <summary>
        /// Set options and perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async Task<CloudRoutingGetRouteResult> GetRoute(Collection<PointShape> waypoints)
        {
            // Set up options for the routing request
            // Enable turn-by-turn, so we get turn by turn instructions
            var options = new CloudRoutingGetRouteOptions
            {
                TurnByTurn = true
            };

            return await _routingCloudClient.GetRouteAsync(waypoints, 3857, options);
        }

        /// <summary>
        /// Draw the result of a Cloud Routing request on the map
        /// </summary>
        private async Task DrawRouteAsync(CloudRoutingGetRouteResult routingResult)
        {
            // Get the routing feature layer from the MapView
            var routingLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Routing Layer");

            // Clear the previous features from the routing layer
            routingLayer.InternalFeatures.Clear();

            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing
            var routeSegments = new List<CloudRoutingSegment>();

            var index = 0;
            // Add the route waypoints and route segments to the map
            foreach (var waypoint in routingResult.RouteResult.Waypoints)
            {
                var columnValues = new Dictionary<string, string> {
                    // Get the order of the stops and label the point
                    // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
                    { "SequenceNumber", (index == 0 ? "Start Point" : "Stop " + index) } };

                var routeWaypoint = new PointShape(waypoint.Coordinate);

                // Add the point to the map
                routingLayer.InternalFeatures.Add(new Feature(routeWaypoint, columnValues));

                // Increment the index for labeling purposes
                index++;
            }
            foreach (var route in routingResult.RouteResult.Routes)
            {
                routingLayer.InternalFeatures.Add(new Feature(route.Shape));
                routeSegments.AddRange(route.Segments);
            }

            // Set the data source for the list box to the route segments
            LsbRouteSegments.ItemsSource = routeSegments;

            // Set the map extent to the newly displayed route
            routingLayer.Open();
            var routingLayerBBox = routingLayer.GetBoundingBox();
            MapView.CenterPoint = routingLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, routingLayerBBox, MapView.MapWidth, MapView.MapHeight);
            var standardZoomLevelSet = new ZoomLevelSet();
            await MapView.ZoomToAsync(standardZoomLevelSet.ZoomLevel13.Scale);
            routingLayer.Close();
            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async Task RouteWaypointsAsync()
        {
            // Create a set of preset waypoints to route through
            var startPoint = new PointShape(-10776986.85, 3908680.24);
            var waypoint1 = new PointShape(-10776836.12, 3912348.04);
            var waypoint2 = new PointShape(-10778917.01, 3909965.17);
            var endPoint = new PointShape(-10779631.80, 3915721.82);

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Send the routing request
            var routingResult = await GetRoute(new Collection<PointShape> { startPoint, waypoint1, waypoint2, endPoint });

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (routingResult.Exception != null)
            {
                MessageBox.Show(routingResult.Exception.Message, "Error");
                return;
            }

            // Draw the result on the map
            await DrawRouteAsync(routingResult);
        }

        /// <summary>
        /// When a route segment is selected in the UI, highlight it
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
                var routeSegmentsBBox = ((CloudRoutingSegment)routeSegments.SelectedItem).Shape.GetBoundingBox();
                MapView.CenterPoint = routeSegmentsBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, routeSegmentsBBox, MapView.MapWidth, MapView.MapHeight);
                var standardZoomLevelSet = new ZoomLevelSet();
                if (MapView.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    await MapView.ZoomToAsync(standardZoomLevelSet.ZoomLevel15.Scale);
                }
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