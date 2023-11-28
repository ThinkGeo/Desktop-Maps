using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the TimezoneCloudClient to access the Timezone APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class TimezoneCloudServicesSample : UserControl, IDisposable
    {
        private TimeZoneCloudClient timeZoneCloudClient;

        public TimezoneCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a PopupOverlay to display time zone information based on locations input by the user
            PopupOverlay timezoneInfoPopupOverlay = new PopupOverlay();

            // Add the overlay to the map
            mapView.Overlays.Add("Timezone Info Popup Overlay", timezoneInfoPopupOverlay);

            // Add a new InMemoryFeatureLayer to hold the timezone shapes
            InMemoryFeatureLayer timezonesFeatureLayer = new InMemoryFeatureLayer();

            // Add a style to use to draw the timezone polygons
            timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Add the layer to an overlay, and add it to the map
            LayerOverlay timezonesLayerOverlay = new LayerOverlay();
            timezonesLayerOverlay.Layers.Add("Timezone Feature Layer", timezonesFeatureLayer);
            mapView.Overlays.Add("Timezone Layer Overlay", timezonesLayerOverlay);

            // Initialize the TimezoneCloudClient with our ThinkGeo Cloud credentials
            timeZoneCloudClient = new TimeZoneCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Set the Map Extent
            mapView.CurrentExtent = new RectangleShape(-14269933.09, 6354969.40, -6966221.89, 2759371.58);

            // Get Timezone info for Frisco, TX
            await GetTimeZoneInfoAsync(-10779572.80, 3915268.68);
        }

        /// <summary>
        /// Perform the timezone query when the user clicks on the map
        /// </summary>
        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Run the timezone info query
                await GetTimeZoneInfoAsync(e.WorldX, e.WorldY);
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
                loadingImage.Visibility = Visibility.Visible;

                // Get timezone info based on the lon, lat, and input projection (Spherical Mercator in this case)
                result = await timeZoneCloudClient.GetTimeZoneByCoordinateAsync(lon, lat, 3857);

                // Hide the loading graphic
                loadingImage.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                // Hide the loading graphic
                loadingImage.Visibility = Visibility.Hidden;

                MessageBox.Show(ex.Message, "Error");
                return;
            }

            // Get the timezone info popup overlay from the mapview
            PopupOverlay timezoneInfoPopupOverlay = (PopupOverlay)mapView.Overlays["Timezone Info Popup Overlay"];

            // Clear the existing info popups from the map
            timezoneInfoPopupOverlay.Popups.Clear();

            // Generate a new info popup and add it to the map
            StringBuilder timezoneInfoString = new StringBuilder();
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Time Zone", result.TimeZone));
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Current Local Time", result.CurrentLocalTime));
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Daylight Savings Active", result.DaylightSavingsActive));
            Popup popup = new Popup(new PointShape(lon, lat));
            popup.Content = timezoneInfoString.ToString();
            popup.FontSize = 10d;
            popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            timezoneInfoPopupOverlay.Popups.Add(popup);

            // Clear the timezone feature layer of previous features
            InMemoryFeatureLayer timezonesFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Timezone Feature Layer");
            timezonesFeatureLayer.Open();
            timezonesFeatureLayer.InternalFeatures.Clear();

            // Use a ProjectionConverter to convert the shape to Spherical Mercator
            ProjectionConverter converter = new ProjectionConverter(3857, 4326);
            converter.Open();

            // Add the new timezone polygon to the map
            timezonesFeatureLayer.InternalFeatures.Add(new Feature(converter.ConvertToInternalProjection(result.Shape)));
            converter.Close();
            timezonesFeatureLayer.Close();

            // Refresh and redraw the map
            await mapView.RefreshAsync();
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
