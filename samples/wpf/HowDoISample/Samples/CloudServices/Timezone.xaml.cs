using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Click the map to get the time zone there: its name, the local time, and the area it covers.
    /// </summary>
    public partial class Timezone
    {
        private bool _initialized;
        private TimeZoneCloudClient _timeZoneCloudClient;
        private readonly DynamicSource _timezoneArea = new DynamicSource();
        private readonly PopupOverlay _popupOverlay = new PopupOverlay();

        public Timezone()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.AddDynamic(_timezoneArea);
            Map.Basemap = new GpuBasemap(style);
            Map.Overlays.Add(_popupOverlay);

            _timeZoneCloudClient = new TimeZoneCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };

            Map.CenterPoint = new PointShape(-10618080, 4557170);
            Map.CurrentScale = 33258550;

            // Frisco, TX
            _ = GetTimeZoneAsync(new PointShape(-10779572.80, 3915268.68));
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton != MapMouseButton.Left) return;

            _ = GetTimeZoneAsync(e.WorldLocation);
        }

        private async Task GetTimeZoneAsync(PointShape point)
        {
            CloudTimeZoneResult result;
            try
            {
                LoadingImage.Visibility = Visibility.Visible;

                // The point is in the map's Spherical Mercator (3857), so the service is told so.
                result = await _timeZoneCloudClient.GetTimeZoneByCoordinateAsync(point.X, point.Y, 3857);
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
                return;
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }

            Status.Text = $"Time zone: {result.TimeZone}\nLocal time: {result.CurrentLocalTime}\nDaylight saving time: {(result.DaylightSavingsActive ? "yes" : "no")}";

            _popupOverlay.Popups.Clear();
            _popupOverlay.Popups.Add(new Popup(point)
            {
                Content = Status.Text,
                FontSize = 10d,
                FontFamily = new System.Windows.Media.FontFamily("Verdana")
            });

            // The zone's shape comes back in latitude and longitude (4326); it is drawn on the
            // map in Spherical Mercator (3857).
            var toMapProjection = new ProjectionConverter(4326, 3857);
            toMapProjection.Open();
            _timezoneArea.Clear();
            _timezoneArea.SetArea("timezone", new Feature(toMapProjection.ConvertToExternalProjection(result.Shape)),
                new GeoColor(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2f);
            toMapProjection.Close();

            await Map.RefreshAsync();
        }
    }
}
