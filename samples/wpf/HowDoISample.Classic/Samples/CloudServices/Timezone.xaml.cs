using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Click the map to get the time zone there: its name, the local time, and the area it covers.
    /// </summary>
    public partial class Timezone : IDisposable
    {
        private bool _initialized;
        private TimeZoneCloudClient _timeZoneCloudClient;
        private InMemoryFeatureLayer _timeZoneLayer;
        private LayerOverlay _timeZoneOverlay;
        private PopupOverlay _popupOverlay;

        public Timezone()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _timeZoneLayer = new InMemoryFeatureLayer();
            _timeZoneLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _timeZoneLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _timeZoneOverlay = new LayerOverlay();
            _timeZoneOverlay.Layers.Add(_timeZoneLayer);
            Map.Overlays.Add(_timeZoneOverlay);

            _popupOverlay = new PopupOverlay();
            Map.Overlays.Add(_popupOverlay);

            _timeZoneCloudClient = new TimeZoneCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            Map.CenterPoint = new PointShape(-10618080, 4557170);
            Map.CurrentScale = 33258550;

            try
            {
                await Map.RefreshAsync();
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }

            // Frisco, TX
            await GetTimeZoneAsync(new PointShape(-10779572.80, 3915268.68));
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton != MapMouseButton.Left) return;

            _ = GetTimeZoneAsync(e.WorldLocation);
        }

        private async Task GetTimeZoneAsync(PointShape point)
        {
            try
            {
                LoadingImage.Visibility = Visibility.Visible;

                // The point is in the map's Spherical Mercator (3857), so the service is told so.
                var result = await _timeZoneCloudClient.GetTimeZoneByCoordinateAsync(point.X, point.Y, 3857);

                Status.Text = $"Time zone: {result.TimeZone}\nLocal time: {result.CurrentLocalTime}\nDaylight saving time: {(result.DaylightSavingsActive ? "yes" : "no")}";
                _popupOverlay.Popups.Clear();
                _popupOverlay.Popups.Add(new Popup(point) { Content = Status.Text, FontSize = 10d });

                // The zone's shape comes back in latitude and longitude (4326); it is drawn on the
                // map in Spherical Mercator (3857).
                var toMapProjection = new ProjectionConverter(4326, 3857);
                toMapProjection.Open();
                _timeZoneLayer.InternalFeatures.Clear();
                _timeZoneLayer.InternalFeatures.Add(new Feature(toMapProjection.ConvertToExternalProjection(result.Shape)));
                toMapProjection.Close();

                await _timeZoneOverlay.RefreshAsync();
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
