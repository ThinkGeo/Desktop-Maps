using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to route through a set of waypoints with the ThinkGeo Cloud
    /// </summary>
    public partial class Routing
    {

        private bool _initialized;
        private RoutingCloudClient _routingCloudClient;

        // The route, its stops, their labels and the picked segment all change when
        // a request answers, so they go to the GPU as geometry with the basemap.
        // The stop labels ride the side channel: they draw over everything and
        // collide with nothing, which is what a route's own numbering wants.
        private readonly InMemoryGeometrySource _route = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _highlight = new InMemoryGeometrySource();

        public Routing()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as a feature layer to display the route
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The star each stop draws as, rasterized once from a point style
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.Images.Add("star", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            // Sources draw in the order they are added; the highlight goes last so a
            // picked segment covers the route it belongs to.
            style.AddGeometry(_route, new GeometryStyle { LineColor = GeoColors.MediumPurple, LineWidthInPixels = 3 });
            style.AddGeometry(_highlight, new GeometryStyle { LineColor = GeoColors.BrightYellow, LineWidthInPixels = 6 });
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

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
            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing
            var routeSegments = new List<CloudRoutingSegment>();

            var index = 0;
            // The stops as features: the shape to draw and the label text to read
            // from the SequenceNumber column.
            // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
            var stops = new List<Feature>();
            foreach (var waypoint in routingResult.RouteResult.Waypoints)
            {
                var stop = new Feature(new PointShape(waypoint.Coordinate));
                stop.ColumnValues["SequenceNumber"] = index == 0 ? "Start Point" : "Stop " + index;
                stops.Add(stop);
                index++;
            }

            var routeLines = new List<BaseShape>();
            foreach (var route in routingResult.RouteResult.Routes)
            {
                routeLines.Add(route.Shape);
                routeSegments.AddRange(route.Segments);
            }

            // The route as lines, the stops as stars, and each stop's label lifted
            // just above its marker.
            _route.UpdateLines(routeLines, GeoColors.MediumPurple, 3f);
            _route.UpdatePoints(stops, sizeInPixels: 24f, iconName: "star");
            _route.UpdateLabels(stops, "SequenceNumber", sizeInPixels: 15f, offsetYInPixels: -20f);

            // Set the data source for the list box to the route segments
            LsbRouteSegments.ItemsSource = routeSegments;

            var routingLayerBBox = MapUtil.GetBoundingBoxOfItems(routeLines);
            Map.CenterPoint = routingLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, routingLayerBBox, Map.MapWidth, Map.MapHeight);
            var standardZoomLevelSet = new ZoomLevelSet();
            await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel13.Scale);
            await Map.RefreshAsync();
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

                // Highlight the selected route segment
                _highlight.UpdateLines(new[] { ((CloudRoutingSegment)routeSegments.SelectedItem).Shape },
                    GeoColors.BrightYellow, 6f);

                // Zoom to the selected feature and zoom out to an appropriate level
                var routeSegmentsBBox = ((CloudRoutingSegment)routeSegments.SelectedItem).Shape.GetBoundingBox();
                Map.CenterPoint = routeSegmentsBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, routeSegmentsBBox, Map.MapWidth, Map.MapHeight);
                var standardZoomLevelSet = new ZoomLevelSet();
                if (Map.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel15.Scale);
                }
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }
    }
}
