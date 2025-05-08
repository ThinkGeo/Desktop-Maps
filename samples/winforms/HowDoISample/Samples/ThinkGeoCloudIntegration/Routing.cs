using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class Routing : UserControl
    {
        private RoutingCloudClient routingCloudClient;

        public Routing()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

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
            mapView.Overlays.Add("Routing Overlay", routingOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            routingCloudClient = new RoutingCloudClient(SampleKeys.ClientId2, SampleKeys.ClientSecret2);

            // Run the routing request
            await RouteWaypointsAsync();
        }
        private async Task<CloudRoutingGetRouteResult> GetRoute(Collection<PointShape> waypoints)
        {
            // Set up options for the routing request
            // Enable turn-by-turn, so we get turn by turn instructions
            var options = new CloudRoutingGetRouteOptions
            {
                TurnByTurn = true
            };

            return await routingCloudClient.GetRouteAsync(waypoints, 3857, options);
        }

        /// <summary>
        /// Draw the result of a Cloud Routing request on the map
        /// </summary>
        private async Task DrawRouteAsync(CloudRoutingGetRouteResult routingResult)
        {
            // Get the routing feature layer from the MapView
            var routingLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Routing Layer");

            // Clear the previous features from the routing layer
            routingLayer.InternalFeatures.Clear();

            // Create a collection to hold the route segments. These include information like distance, duration, warnings, and instructions for turn-by-turn routing            
            List<RouteSegemt> displayItems = new List<RouteSegemt>();

            int index = 0;
            // Add the route waypoints and route segments to the map
            foreach (var waypoint in routingResult.RouteResult.Waypoints)
            {
                var columnValues = new Dictionary<string, string>
                {
                    // Get the order of the stops and label the point
                    // '0' represents the start/end point of the route for a round trip route, so we change the label to indicate that for readability
                    { "SequenceNumber", (index == 0 ? "Start Point" : "Stop " + index) }

                };

                var routeWaypoint = new PointShape(waypoint.Coordinate);

                // Add the point to the map
                routingLayer.InternalFeatures.Add(new Feature(routeWaypoint, columnValues));

                // Increment the index for labeling purposes
                index++;
            }
            foreach (var route in routingResult.RouteResult.Routes)
            {
                routingLayer.InternalFeatures.Add(new Feature(route.Shape));
                foreach (var segment in route.Segments)
                {
                    var segmentForDisplay = new RouteSegemt
                    {
                        DisplayInformation = $"{segment.Instruction} Distance: {Math.Round(segment.Distance, 0)} meters.",
                        Shape = new Feature(segment.Shape)
                    };
                    displayItems.Add(segmentForDisplay);
                }
            }

            // Set the data source for the list box to the route segments
            lsbRouteSegments.DataSource = displayItems;
            lsbRouteSegments.DisplayMember = "DisplayInformation";

            // Set the map extent to the newly displayed route
            routingLayer.Open();
            mapView.CurrentExtent = routingLayer.GetBoundingBox();
            var standardZoomLevelSet = new ZoomLevelSet();
            await mapView.ZoomToAsync(standardZoomLevelSet.ZoomLevel13.Scale);
            routingLayer.Close();
            await mapView.RefreshAsync();
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
            //loadingImage.Visibility = Visibility.Visible;

            // Send the routing request
            var routingResult = await GetRoute(new Collection<PointShape> { startPoint, waypoint1, waypoint2, endPoint });

            // Hide the loading graphic
            //loadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (routingResult.Exception != null)
            {
                MessageBox.Show(routingResult.Exception.Message, "Error");
                return;
            }

            // Draw the result on the map
            await DrawRouteAsync(routingResult);
        }

        private async void lsbRouteSegments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var routeSegments = (ListBox)sender;
            if (routeSegments.SelectedItem != null)
            {
                var highlightLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Highlight Layer");
                highlightLayer.InternalFeatures.Clear();

                // Highlight the selected route segment
                highlightLayer.InternalFeatures.Add(((RouteSegemt)routeSegments.SelectedItem).Shape);

                // Zoom to the selected feature and zoom out to an appropriate level
                mapView.CurrentExtent = ((RouteSegemt)routeSegments.SelectedItem).Shape.GetBoundingBox();
                var standardZoomLevelSet = new ZoomLevelSet();
                if (mapView.CurrentScale < standardZoomLevelSet.ZoomLevel15.Scale)
                {
                    await mapView.ZoomToAsync(standardZoomLevelSet.ZoomLevel15.Scale);
                }
                await mapView.RefreshAsync();
            }
        }

        public class RouteSegemt
        {
            public string DisplayInformation { get; set; }
            public Feature Shape { get; set; }
        }

        #region Component Designer generated code
        private Panel panel1;
        private ListBox lsbRouteSegments;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsbRouteSegments = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(895, 556);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.lsbRouteSegments);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(901, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 556);
            this.panel1.TabIndex = 1;
            // 
            // lsbRouteSegments
            // 
            this.lsbRouteSegments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbRouteSegments.FormattingEnabled = true;
            this.lsbRouteSegments.ItemHeight = 16;
            this.lsbRouteSegments.Location = new System.Drawing.Point(3, 85);
            this.lsbRouteSegments.Name = "lsbRouteSegments";
            this.lsbRouteSegments.Size = new System.Drawing.Size(295, 468);
            this.lsbRouteSegments.TabIndex = 1;
            this.lsbRouteSegments.SelectedIndexChanged += new System.EventHandler(this.lsbRouteSegments_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Make a Turn-By-Turn\r\nRouting Request";
            // 
            // Routing
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "Routing";
            this.Size = new System.Drawing.Size(1202, 556);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion Component Designer generated code

    }
}