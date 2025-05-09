using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the RoutingCloudClient to find the service area of a location with the ThinkGeo Cloud
    /// </summary>
    public partial class RoutingServiceArea : IDisposable
    {
        private RoutingCloudClient _routingCloudClient;
        private Collection<TimeSpan> _serviceAreaIntervals;

        public RoutingServiceArea()
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
            MapView.Overlays.Add("Service Area Overlay", serviceAreaOverlay);

            // Add a simple marker overlay to display the center point of the service area
            var serviceAreaMarkerOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add("Service Area Marker Overlay", serviceAreaMarkerOverlay);

            MapView.CenterPoint = new PointShape(-10774380, 3907010);
            MapView.CurrentScale = 1155580;

            // Create a new set of time spans for 15, 30, 45, 60 minutes. These will be used to create the class breaks for the routing service area request
            _serviceAreaIntervals = new Collection<TimeSpan>()
                {
                new TimeSpan(0, 15, 0),
                new TimeSpan(0, 30, 0),
                new TimeSpan(0, 45, 0),
                new TimeSpan(1, 0, 0)
                };

            // Initialize the RoutingCloudClient with our ThinkGeo Cloud Client credentials
            _routingCloudClient = new RoutingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            // Run a sample query
            var samplePoint = new PointShape(-10776836.140633, 3912350.714164);
            _ = GetAndDrawServiceAreaAsync(samplePoint);
        }

        /// <summary>
        /// Get the service area from a given point on the map
        /// </summary>
        private async Task<CloudRoutingGetServiceAreaResult> GetServiceArea(PointShape centerpoint)
        {
            // Set options for the service area request
            // We can control options like Travel Direction and Contour Granularity
            var options = new CloudRoutingGetServiceAreaOptions
            {
                DistanceUnit = DistanceUnit.Meter
            };

            // Set the srid for the query to 3857 (Spherical Mercator)
            const int srid = 3857;

            // Run the service area query
            // Pass in the service area intervals. These will be used as the service areas for the query (15, 30, 45 60 minutes)
            var getServiceAreaResult = await _routingCloudClient.GetServiceAreaAsync(centerpoint, srid, _serviceAreaIntervals, options);
            return getServiceAreaResult;
        }

        /// <summary>
        /// Draw the ServiceArea polygons on the map
        /// </summary> 
        private async Task DrawServiceAreaAsync(CloudRoutingGetServiceAreaResult result)
        {
            var serviceAreaResult = result.ServiceAreaResult;

            // Get the simple marker overlay from the map
            var serviceAreaMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["Service Area Marker Overlay"];

            // Clear the previous markers
            serviceAreaMarkerOverlay.Markers.Clear();

            // Add the service area center point marker to the map
            serviceAreaMarkerOverlay.Markers.Add(CreateNewMarker(new PointShape(serviceAreaResult.Waypoint.Coordinate)));

            // Get the service area polygons layer from the map
            var serviceAreaLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Service Area Layer");

            // Clear the previous polygons
            serviceAreaLayer.InternalFeatures.Clear();

            // Add the new service area polygons to the map
            for (var i = 0; i < _serviceAreaIntervals.Count; i++)
            {
                // Add a 'TravelTimeFromCenterPoint' attribute for the class break style
                var columnValues = new Dictionary<string, string> { { "TravelTimeFromCenterPoint", _serviceAreaIntervals[i].TotalMinutes.ToString(CultureInfo.InvariantCulture) } };

                // Add each polygon to the feature layer
                var serviceAreaPolygon = serviceAreaResult.ServiceAreas[i];
                serviceAreaLayer.InternalFeatures.Add(new Feature(serviceAreaPolygon, columnValues));
            }

            // Zoom to the extent of the service area and refresh the map
            serviceAreaLayer.Open();
            var serviceAreaLayerBBox = serviceAreaLayer.GetBoundingBox();
            MapView.CenterPoint = serviceAreaLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit,serviceAreaLayerBBox, MapView.MapWidth, MapView.MapHeight);
            serviceAreaLayer.Close();

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Draw the new point and service area on the map
        /// </summary>
        private async Task GetAndDrawServiceAreaAsync(PointShape point)
        {
            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Run the service area query
            var getServiceAreaResult = await GetServiceArea(point);

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

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

            MapView.AdornmentOverlay.Layers.Add(legend);

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

        /// <summary>
        /// Perform the service area query when a new point is drawn
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            _ = GetAndDrawServiceAreaAsync(e.WorldLocation);
        }

        /// <summary>
        /// Create a new map marker using preloaded image assets
        /// </summary>
        private static Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
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