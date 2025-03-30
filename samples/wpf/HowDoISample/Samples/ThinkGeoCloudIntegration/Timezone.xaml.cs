using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the TimezoneCloudClient to access the Timezone APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class Timezone : IDisposable
    {
        private TimeZoneCloudClient _timeZoneCloudClient;

        public Timezone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay
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

                // Create a PopupOverlay to display time zone information based on locations input by the user
                var timezoneInfoPopupOverlay = new PopupOverlay();

                // Add the overlay to the map
                MapView.Overlays.Add("Timezone Info Popup Overlay", timezoneInfoPopupOverlay);

                // Add a new InMemoryFeatureLayer to hold the timezone shapes
                var timezonesFeatureLayer = new InMemoryFeatureLayer();

                // Add a style to use to draw the timezone polygons
                timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Add the layer to an overlay, and add it to the map
                var timezonesLayerOverlay = new LayerOverlay();
                timezonesLayerOverlay.Layers.Add("Timezone Feature Layer", timezonesFeatureLayer);
                MapView.Overlays.Add("Timezone Layer Overlay", timezonesLayerOverlay);

                // Initialize the TimezoneCloudClient with our ThinkGeo Cloud credentials
                _timeZoneCloudClient = new TimeZoneCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                // Set the Map Extent
                MapView.CenterPoint = new PointShape(-10618080,4557170);
                MapView.CurrentScale = 33258550;

                // Get Timezone info for Frisco, TX
                await GetTimeZoneInfoAsync(-10779572.80, 3915268.68);
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Perform the timezone query when the user clicks on the map
        /// </summary>
        private async void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            try
            {
                if (e.MouseButton == MapMouseButton.Left)
                {
                    // Run the timezone info query
                    await GetTimeZoneInfoAsync(e.WorldX, e.WorldY);
                }
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Use the TimezoneCloudClient to query for timezone information
        /// </summary>
        private async Task GetTimeZoneInfoAsync(double lon, double lat)
        {
            CloudTimeZoneResult result;
            try
            {
                // Show a loading graphic to let users know the request is running
                LoadingImage.Visibility = Visibility.Visible;

                // Get timezone info based on the lon, lat, and input projection (Spherical Mercator in this case)
                result = await _timeZoneCloudClient.GetTimeZoneByCoordinateAsync(lon, lat, 3857);

                // Hide the loading graphic
                LoadingImage.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                // Hide the loading graphic
                LoadingImage.Visibility = Visibility.Hidden;

                MessageBox.Show(ex.Message, "Error");
                return;
            }

            // Get the timezone info popup overlay from the mapview
            var timezoneInfoPopupOverlay = (PopupOverlay)MapView.Overlays["Timezone Info Popup Overlay"];

            // Clear the existing info popups from the map
            timezoneInfoPopupOverlay.Popups.Clear();

            // Generate a new info popup and add it to the map
            var timezoneInfoString = new StringBuilder();
            timezoneInfoString.AppendLine($"Time Zone: {result.TimeZone}");
            timezoneInfoString.AppendLine($"Current Local Time: {result.CurrentLocalTime}");
            timezoneInfoString.AppendLine($"Daylight Savings Active: {result.DaylightSavingsActive}");
            var popup = new Popup(new PointShape(lon, lat))
            {
                Content = timezoneInfoString.ToString(),
                FontSize = 10d,
                FontFamily = new System.Windows.Media.FontFamily("Verdana")
            };
            timezoneInfoPopupOverlay.Popups.Add(popup);

            // Clear the timezone feature layer of previous features
            var timezonesFeatureLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Timezone Feature Layer");
            timezonesFeatureLayer.Open();
            timezonesFeatureLayer.InternalFeatures.Clear();

            // Use a ProjectionConverter to convert the shape to Spherical Mercator
            var converter = new ProjectionConverter(3857, 4326);
            converter.Open();

            // Add the new timezone polygon to the map
            timezonesFeatureLayer.InternalFeatures.Add(new Feature(converter.ConvertToInternalProjection(result.Shape)));
            converter.Close();
            timezonesFeatureLayer.Close();

            // Refresh and redraw the map
            await MapView.RefreshAsync();
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