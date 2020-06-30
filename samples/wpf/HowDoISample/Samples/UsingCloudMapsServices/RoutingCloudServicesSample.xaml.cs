using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to route through a set of waypoints with the ThinkGeo Cloud
    /// </summary>
    public partial class RoutingCloudServicesSample : UserControl
    {
        private RoutingCloudClient routingCloudClient;

        public RoutingCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay, as well as a feature layer to display the route
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the route
            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();

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
            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();

            // Add styles to display the highlighted route features
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.BrightYellow, 6, GeoColors.Black, 2, false);
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layers to an overlay, and add the overlay to the mapview
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("Routing Layer", routingLayer);
            routingOverlay.Layers.Add("Highlight Layer", highlightLayer);
            mapView.Overlays.Add("Routing Overlay", routingOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            routingCloudClient = new RoutingCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Run the routing request
            RouteWaypoints();
        }

        /// <summary>
        /// Set options and perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async Task<CloudRoutingGetRouteResult> GetRoute(Collection<PointShape> waypoints)
        {
            // Set up options for the routing request
            // Enable turn-by-turn so we get turn by turn instructions
            CloudRoutingGetRouteOptions options = new CloudRoutingGetRouteOptions();
            options.TurnByTurn = true;

            return await routingCloudClient.GetRouteAsync(waypoints, 3857, options);
        }

        /// <summary>
        /// Draw the result of a Cloud Routing request on the map
        /// </summary>
        private void DrawRoute(CloudRoutingGetRouteResult routingResult)
        {
            // Get the routing feature layer from the MapView
            InMemoryFeatureLayer routingLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Routing Layer");

            // Clear the previous features from the routing layer
            routingLayer.InternalFeatures.Clear();

            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing
            List<CloudRoutingSegment> routeSegments = new List<CloudRoutingSegment>();

            int index = 0;
            // Add the route waypoints and route segments to the map
            foreach (CloudRoutingWaypoint waypoint in routingResult.RouteResult.Waypoints)
            {
                Dictionary<string, string> columnValues = new Dictionary<string, string>();

                // Get the order of the stops and label the point
                // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
                columnValues.Add("SequenceNumber", (index == 0 ? "Start Point" : "Stop " + index));
                PointShape routeWaypoint = new PointShape(waypoint.Coordinate);

                // Add the point to the map
                routingLayer.InternalFeatures.Add(new Feature(routeWaypoint, columnValues));

                // Increment the index for labeling purposes
                index++;
            }
            foreach (CloudRoutingRoute route in routingResult.RouteResult.Routes)
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
            mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel13.Scale);
            routingLayer.Close();
            mapView.Refresh();
        }

        /// <summary>
        /// Perform routing using the RoutingCloudClient through a preset set of waypoints
        /// </summary>
        private async void RouteWaypoints()
        {
            // Create a set of preset waypoints to route through
            PointShape startPoint = new PointShape(-10776986.85, 3908680.24);
            PointShape waypoint1 = new PointShape(-10776836.12, 3912348.04);
            PointShape waypoint2 = new PointShape(-10778917.01, 3909965.17);
            PointShape endPoint = new PointShape(-10779631.80, 3915721.82);

            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // Send the routing request
            CloudRoutingGetRouteResult routingResult = await GetRoute(new Collection<PointShape> { startPoint, waypoint1, waypoint2, endPoint });

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (routingResult.Exception != null)
            {
                MessageBox.Show(routingResult.Exception.Message, "Error");
                return;
            }

            // Draw the result on the map
            DrawRoute(routingResult);
        }

        /// <summary>
        /// When a route segment is selected in the UI, highlight it
        /// </summary>
        private void lsbRouteSegments_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                if(mapView.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel15.Scale);
                }
                mapView.Refresh();
            }
        }
    }
}
