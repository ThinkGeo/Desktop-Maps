using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class RoutingServiceArea : UserControl
    {
        private RoutingCloudClient routingCloudClient;
        private Collection<TimeSpan> serviceAreaIntervals;
        public RoutingServiceArea()
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

            // Create a new feature layer to display the service areas
            var serviceAreasLayer = new InMemoryFeatureLayer();

            // Add a class break style to display the service areas
            // We will display a different color for 15, 30, 45, and 60 minute travel times
            var serviceAreasClassBreaks = new Collection<ClassBreak>
            {
                new ClassBreak(15, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Green), GeoColors.Green)),
                new ClassBreak(30, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Yellow), GeoColors.Yellow)),
                new ClassBreak(45, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Orange), GeoColors.Orange)),
                new ClassBreak(60, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Red), GeoColors.Red))
            };

            var serviceAreasClassBreakStyle = new ClassBreakStyle("TravelTimeFromCenterPoint", BreakValueInclusion.IncludeValue, serviceAreasClassBreaks);
            serviceAreasLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(serviceAreasClassBreakStyle);
            serviceAreasLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set up the legend adornment
            SetUpLegendAdornment(serviceAreasClassBreaks);

            // Add the layer to an overlay, and add the overlay to the mapview
            var serviceAreaOverlay = new LayerOverlay();
            serviceAreaOverlay.Layers.Add("Service Area Layer", serviceAreasLayer);
            mapView.Overlays.Add("Service Area Overlay", serviceAreaOverlay);

            // Add a simple marker overlay to display the center point of the service area
            var serviceAreaMarkerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Service Area Marker Overlay", serviceAreaMarkerOverlay);

            mapView.CurrentExtent = new RectangleShape(-10895153.061011, 4016319.51333112, -10653612.0529718, 3797709.61365001);

            // Create a new set of time spans for 15, 30, 45, 60 minutes. These will be used to create the class breaks for the routing service area request
            serviceAreaIntervals = new Collection<TimeSpan>() {
                new TimeSpan(0, 15, 0),
                new TimeSpan(0, 30, 0),
                new TimeSpan(0, 45, 0),
                new TimeSpan(1, 0, 0)
            };

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            routingCloudClient = new RoutingCloudClient(SampleKeys.ClientId2, SampleKeys.ClientSecret2);

            // Run a sample query
            var samplePoint = new PointShape(-10776836.140633, 3912350.714164);
            await GetAndDrawServiceAreaAsync(samplePoint);
        }

        private async Task<CloudRoutingGetServiceAreaResult> GetServiceArea(PointShape centerpoint)
        {
            // Set options for the service area request
            // We can control options like Travel Direction and Contour Granularity
            var options = new CloudRoutingGetServiceAreaOptions
            {
                DistanceUnit = DistanceUnit.Meter
            };

            // Set the srid for the query to 3857 (Spherical Mercator)
            int srid = 3857;

            // Run the service area query
            // Pass in the service area intervals. These will be used as the service areas for the query (15, 30, 45 60 minutes)
            var getServiceAreaResult = await routingCloudClient.GetServiceAreaAsync(centerpoint, srid, serviceAreaIntervals, options);
            return getServiceAreaResult;
        }

        private async Task DrawServiceAreaAsync(CloudRoutingGetServiceAreaResult result)
        {
            var serviceAreaResult = result.ServiceAreaResult;

            // Get the simple marker overlay from the map
            var serviceAreaMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["Service Area Marker Overlay"];

            // Clear the previous markers
            serviceAreaMarkerOverlay.Markers.Clear();

            // Add the service area center point marker to the map
            serviceAreaMarkerOverlay.Markers.Add(CreateNewMarker(new PointShape(serviceAreaResult.Waypoint.Coordinate)));

            // Get the service area polygons layer from the map
            var serviceAreaLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Service Area Layer");

            // Clear the previous polygons
            serviceAreaLayer.InternalFeatures.Clear();

            // Add the new service area polygons to the map
            for (int i = 0; i < serviceAreaIntervals.Count; i++)
            {
                // Add a 'TravelTimeFromCenterPoint' attribute for the class break style
                var columnValues = new Dictionary<string, string> { { "TravelTimeFromCenterPoint", serviceAreaIntervals[i].TotalMinutes.ToString() } };

                // Add each polygon to the feature layer
                var serviceAreaPolygon = serviceAreaResult.ServiceAreas[i];
                serviceAreaLayer.InternalFeatures.Add(new Feature(serviceAreaPolygon, columnValues));
            }

            // Zoom to the extent of the service area and refresh the map
            serviceAreaLayer.Open();
            mapView.CurrentExtent = serviceAreaLayer.GetBoundingBox();
            serviceAreaLayer.Close();

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Draw the new point and service area on the map
        /// </summary>
        private async Task GetAndDrawServiceAreaAsync(PointShape point)
        {
            // Show a loading graphic to let users know the request is running
            //loadingImage.Visibility = Visibility.Visible;

            // Run the service area query
            var getServiceAreaResult = await GetServiceArea(point);

            // Hide the loading graphic
            //loadingImage.Visibility = Visibility.Hidden;

            // Handle an exception returned from the service
            if (getServiceAreaResult.Exception != null)
            {
                MessageBox.Show(getServiceAreaResult.Exception.Message, "Error");
                return;
            }

            // Draw the result on the map
            await DrawServiceAreaAsync(getServiceAreaResult);
        }

        private void SetUpLegendAdornment(Collection<ClassBreak> classBreaks)
        {
            // Create a legend adornment based on the service area class breaks
            var legend = new LegendAdornmentLayer
            {
                // Set up the legend adornment
                Title = new LegendItem()
                {
                    TextStyle = new TextStyle("Travel Times", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.LowerRight
            };

            mapView.AdornmentOverlay.Layers.Add(legend);

            // Add a LegendItems to the legend adornment for each ClassBreak
            foreach (var classBreak in classBreaks)
            {
                var legendItem = new LegendItem()
                {
                    ImageStyle = classBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle($@"<{classBreak.Value} minutes", new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }
        }

        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            await GetAndDrawServiceAreaAsync(e.WorldLocation);
        }
        private Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label2;
        private Label label1;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
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
            this.mapView.Location = new System.Drawing.Point(0, -3);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(911, 581);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(917, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 581);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click On Map To Caculate A new\r\nService Area";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find the Service Area of a Location";
            // 
            // RoutingServiceArea
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "RoutingServiceArea";
            this.Size = new System.Drawing.Size(1219, 581);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}