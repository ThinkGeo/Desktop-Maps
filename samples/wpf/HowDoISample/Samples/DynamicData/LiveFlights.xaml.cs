using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Every airborne aircraft from OpenSky, flown forward by dead reckoning through one DynamicSource.
    /// </summary>
    public partial class LiveFlights : IDisposable
    {
        private const string OpenSkyUrl = "https://opensky-network.org/api/states/all";

        private static readonly HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };

        private readonly DispatcherTimer _timer;

        private DynamicSource _tracks;
        private Flight[] _flights;
        private DateTime _lastTick;
        private bool _built;

        private Flight _trailedFlight;
        private readonly List<Vertex> _trail = new List<Vertex>();

        private sealed class Flight
        {
            public string Id;
            public string Callsign;
            public double Lon;
            public double Lat;
            public float VelocityMetersPerSecond;
            public float TrackDegrees;
            public float AltitudeMeters;
        }

        public LiveFlights()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };
            _timer.Tick += (_, __) => Advance();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_built || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _built = true;

            // Coordinates arrive in WGS84 and the source converts.
            _tracks = new DynamicSource(new Projection(4326));

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.AddDynamic(_tracks);
            Map.Basemap = new GpuBasemap(style);

            Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);
            await Map.RefreshAsync();

            Status.Text = "Fetching the sky…";
            try
            {
                var json = await HttpClient.GetStringAsync(OpenSkyUrl);
                _flights = ParseOpenSky(json);
            }
            catch (Exception ex)
            {
                Status.Text = "OpenSky (opensky-network.org) did not answer, so there are no aircraft to show. Try again later. (" + ex.Message + ")";
                return;
            }

            if (_flights.Length == 0)
            {
                Status.Text = "OpenSky (opensky-network.org) answered without any aircraft. Try again later.";
                return;
            }

            Status.Text = FormattableString.Invariant($"{_flights.Length} aircraft airborne right now");
            _lastTick = DateTime.UtcNow;
            _timer.Start();
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedLabel != null)
            {
                SpeedLabel.Text = FormattableString.Invariant($"time x{e.NewValue:0}");
            }
        }

        private void Map_MapMouseMove(object sender, MapMouseMoveMapViewEventArgs e) =>
            ShowHover(e.WorldLocation.X, e.WorldLocation.Y, e.ScreenX, e.ScreenY);

        private static Flight[] ParseOpenSky(string json)
        {
            using var document = JsonDocument.Parse(json);
            var flights = new List<Flight>(16384);
            var states = document.RootElement.GetProperty("states");
            if (states.ValueKind != JsonValueKind.Array)
            {
                return flights.ToArray();
            }

            foreach (var state in states.EnumerateArray())
            {
                // Indexes per the OpenSky API: 1 callsign, 5 lon, 6 lat,
                // 7 baro altitude, 8 on-ground, 9 velocity, 10 true track.
                var lon = state[5];
                var lat = state[6];
                var track = state[10];
                if (lon.ValueKind == JsonValueKind.Null || lat.ValueKind == JsonValueKind.Null ||
                    track.ValueKind == JsonValueKind.Null || state[8].GetBoolean())
                {
                    continue;
                }

                flights.Add(new Flight
                {
                    Id = "flight-" + flights.Count,
                    Callsign = state[1].ValueKind == JsonValueKind.Null ? string.Empty : state[1].GetString().Trim(),
                    Lon = lon.GetDouble(),
                    Lat = lat.GetDouble(),
                    AltitudeMeters = state[7].ValueKind == JsonValueKind.Null ? 0f : (float)state[7].GetDouble(),
                    VelocityMetersPerSecond = state[9].ValueKind == JsonValueKind.Null ? 0f : (float)state[9].GetDouble(),
                    TrackDegrees = (float)track.GetDouble(),
                });
            }

            return flights.ToArray();
        }

        private void Advance()
        {
            var now = DateTime.UtcNow;
            var dt = (now - _lastTick).TotalSeconds * SpeedSlider.Value;
            _lastTick = now;

            foreach (var flight in _flights)
            {
                var radians = flight.TrackDegrees * Math.PI / 180;
                var meters = flight.VelocityMetersPerSecond * dt;
                flight.Lat += meters * Math.Cos(radians) / 110540.0;
                flight.Lon += meters * Math.Sin(radians) / (111320.0 * Math.Max(0.2, Math.Cos(flight.Lat * Math.PI / 180)));
                if (flight.Lat > 85 || flight.Lat < -85)
                {
                    _tracks.Remove(flight.Id);
                    continue;
                }

                if (flight.Lon > 180) flight.Lon -= 360;
                if (flight.Lon < -180) flight.Lon += 360;

                _tracks.SetPoint(flight.Id, new Feature(flight.Lon, flight.Lat), flight.TrackDegrees, ColorForAltitude(flight.AltitudeMeters), 11f);
            }

            if (_trailedFlight != null)
            {
                // A dateline crossing would draw as a line around the world; start over.
                if (_trail.Count > 0 && Math.Abs(_trailedFlight.Lon - _trail[_trail.Count - 1].X) > 180)
                {
                    _trail.Clear();
                }

                _trail.Add(new Vertex(_trailedFlight.Lon, _trailedFlight.Lat));
                if (_trail.Count > 240)
                {
                    _trail.RemoveAt(0);
                }

                if (_trail.Count > 1)
                {
                    _tracks.SetLine("trail", new Feature(new LineShape(_trail)), ColorForAltitude(_trailedFlight.AltitudeMeters), 2f);
                }
            }
        }

        /// <summary>The nearest plane within a finger's width of the cursor, on a card beside it.</summary>
        private void ShowHover(double worldX, double worldY, float screenX, float screenY)
        {
            if (_flights == null || Map.ActualWidth <= 0)
            {
                HoverCard.Visibility = Visibility.Collapsed;
                return;
            }

            // The cursor in degrees, and a 14-pixel pick radius converted the same way.
            var toDegrees = ProjectionConverter.DecimalDegreesToSphericalMercator;
            var cursor = toDegrees.ConvertToInternalProjection(worldX, worldY);
            var lon = cursor.X;
            var lat = cursor.Y;
            var degreesPerPixel = toDegrees.ConvertToInternalProjection(Map.CurrentExtent.Width, 0).X / Map.ActualWidth;
            var pick = degreesPerPixel * 14;
            var cosLat = Math.Max(0.2, Math.Cos(lat * Math.PI / 180));

            Flight nearest = null;
            var best = pick * pick;
            foreach (var flight in _flights)
            {
                var dx = (flight.Lon - lon) * cosLat;
                var dy = flight.Lat - lat;
                var d = (dx * dx) + (dy * dy);
                if (d < best)
                {
                    best = d;
                    nearest = flight;
                }
            }

            if (nearest == null)
            {
                // The last hovered aircraft keeps its trail after the card hides.
                HoverCard.Visibility = Visibility.Collapsed;
                return;
            }

            if (!ReferenceEquals(nearest, _trailedFlight))
            {
                _trailedFlight = nearest;
                _trail.Clear();
            }

            var callsign = string.IsNullOrEmpty(nearest.Callsign) ? "(no callsign)" : nearest.Callsign;
            HoverText.Text = FormattableString.Invariant(
                $"{callsign}\naltitude {nearest.AltitudeMeters:0} m\nspeed {nearest.VelocityMetersPerSecond * 3.6:0} km/h\nheading {nearest.TrackDegrees:0}°");
            HoverCard.Margin = new Thickness(screenX + 16, screenY + 12, 0, 0);
            HoverCard.Visibility = Visibility.Visible;
        }

        private static GeoColor ColorForAltitude(float meters) => meters switch
        {
            < 3000f => GeoColor.FromArgb(235, 240, 150, 30),
            < 8000f => GeoColor.FromArgb(235, 70, 180, 120),
            _ => GeoColor.FromArgb(235, 60, 130, 230),
        };

        public void Dispose()
        {
            _timer.Stop();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
