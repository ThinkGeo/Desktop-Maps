using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the TimezoneCloudClient to access the Timezone APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class Timezone
    {

        private bool _initialized;
        private TimeZoneCloudClient _timeZoneCloudClient;
        private readonly InMemoryGeometrySource _timezoneArea = new InMemoryGeometrySource();

        public Timezone()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            // Create a PopupOverlay to display time zone information based on locations input by the user
            var timezoneInfoPopupOverlay = new PopupOverlay();

            // Add the overlay to the map
            Map.Overlays.Add("Timezone Info Popup Overlay", timezoneInfoPopupOverlay);

            // The timezone the service answers with goes to the GPU as geometry, drawn
            // with the basemap in one pass. An overlay would draw it to raster tiles,
            // and the shape would then stretch through a fractional zoom while the map
            // under it did not.
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.AddGeometry(_timezoneArea, new GeometryStyle
            {
                FillColor = new GeoColor(50, GeoColors.MediumPurple),
                OutlineColor = GeoColors.MediumPurple,
                OutlineWidthInPixels = 2,
            });
            Map.Basemap = new GpuBasemap(style);

            // Initialize the TimezoneCloudClient with our ThinkGeo Cloud credentials
            _timeZoneCloudClient = new TimeZoneCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            // Set the Map Extent
            Map.CenterPoint = new PointShape(-10618080, 4557170);
            Map.CurrentScale = 33258550;

            // Get Timezone info for Frisco, TX
            _ = GetTimeZoneInfoAsync(-10779572.80, 3915268.68);
        }

        /// <summary>
        /// Perform the timezone query when the user clicks on the map
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Run the timezone info query
                _ = GetTimeZoneInfoAsync(e.WorldX, e.WorldY);
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

            // Get the timezone info popup overlay from the map
            var timezoneInfoPopupOverlay = (PopupOverlay)Map.Overlays["Timezone Info Popup Overlay"];

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

            // Use a ProjectionConverter to convert the shape to Spherical Mercator
            var converter = new ProjectionConverter(3857, 4326);
            converter.Open();

            // Replace whatever was there with the timezone just returned.
            _timezoneArea.UpdateAreas(new[] { converter.ConvertToInternalProjection(result.Shape) });
            converter.Close();

            // Refresh and redraw the map
            await Map.RefreshAsync();
        }
    }
}
