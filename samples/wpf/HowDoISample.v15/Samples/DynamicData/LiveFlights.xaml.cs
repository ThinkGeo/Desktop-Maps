using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Every aircraft in the sky right now: one live fetch from the OpenSky network -
    /// ten-thousand-plus airborne planes with position, track and speed - then dead
    /// reckoning flies them all forward, sped up by the time slider. Each tick is one
    /// InMemoryGeometrySource.UpdatePoints carrying the whole sky; the renderer redraws the markers and
    /// nothing else changes. Hover a plane for its callsign, altitude and speed - and
    /// it starts drawing its trail, through the same source's line channel.
    /// </summary>
    public partial class LiveFlights : IDisposable
    {
        private const string OpenSkyUrl = "https://opensky-network.org/api/states/all";

        private static readonly HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(20) };

        private readonly DispatcherTimer _timer;

        private InMemoryGeometrySource _tracks;
        private Flight[] _flights;
        private DateTime _lastTick;
        private bool _built;

        // The hovered aircraft keeps a trail: a ring buffer of where it has been,
        // pushed through the same source's line channel each tick.
        private Flight _trailedFlight;
        private readonly List<Vertex> _trail = new List<Vertex>();

        private sealed class Flight
        {
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

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_built)
            {
                return;
            }

            _built = true;
            try
            {
                // Coordinates arrive in WGS84 and the source converts.
                _tracks = new InMemoryGeometrySource(new Projection(4326));

                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
                style.AddGeometry(_tracks);
                Map.Basemap = new GpuBasemap(style);

                // The lower 48, the densest continuous airspace; zoom out for the world.
                Map.CurrentExtent = new RectangleShape(-14026255, 6446275, -7235766, 2632018);

                Status.Text = "Fetching the sky…";
                var json = await HttpClient.GetStringAsync(OpenSkyUrl);
                _flights = ParseOpenSky(json);
                Status.Text = FormattableString.Invariant($"{_flights.Length} aircraft airborne right now");

                _lastTick = DateTime.UtcNow;
                _timer.Start();
            }
            catch (Exception ex)
            {
                Status.Text = "OpenSky unreachable: " + ex.Message;
            }

            await Map.RefreshAsync();
        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SpeedLabel != null)
            {
                SpeedLabel.Text = FormattableString.Invariant($"time x{e.NewValue:0}");
            }
        }

        private void Map_MapMouseMove(object sender, MapMouseMoveMapViewEventArgs e) =>
            ShowHover(e.WorldX, e.WorldY, e.ScreenX, e.ScreenY);

        private static Flight[] ParseOpenSky(string json)
        {
            using var document = JsonDocument.Parse(json);
            var flights = new List<Flight>(16384);
            foreach (var state in document.RootElement.GetProperty("states").EnumerateArray())
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

        /// <summary>
        /// One tick: every aircraft flies straight ahead at its reported speed and
        /// track (dead reckoning), sped up by the time slider, and the whole sky goes
        /// down as one snapshot. Longitudes wrap, so the Pacific crossings keep going.
        /// </summary>
        private void Advance()
        {
            if (_flights == null || _tracks == null)
            {
                return;
            }

            var now = DateTime.UtcNow;
            var dt = (now - _lastTick).TotalSeconds * SpeedSlider.Value;
            _lastTick = now;

            var points = new List<TrackPoint>(_flights.Length);
            foreach (var flight in _flights)
            {
                var radians = flight.TrackDegrees * Math.PI / 180;
                var meters = flight.VelocityMetersPerSecond * dt;
                flight.Lat += meters * Math.Cos(radians) / 110540.0;
                flight.Lon += meters * Math.Sin(radians) / (111320.0 * Math.Max(0.2, Math.Cos(flight.Lat * Math.PI / 180)));
                if (flight.Lat > 85 || flight.Lat < -85)
                {
                    continue;
                }

                if (flight.Lon > 180) flight.Lon -= 360;
                if (flight.Lon < -180) flight.Lon += 360;

                points.Add(new TrackPoint(flight.Lon, flight.Lat, flight.TrackDegrees,
                    ColorForAltitude(flight.AltitudeMeters), 11f));
            }

            _tracks.UpdatePoints(points);

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
                    _tracks.UpdateLines(new BaseShape[] { new LineShape(_trail) },
                        ColorForAltitude(_trailedFlight.AltitudeMeters), 2f);
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
            var lon = worldX / 6378137.0 * 180 / Math.PI;
            var lat = ((2 * Math.Atan(Math.Exp(worldY / 6378137.0))) - (Math.PI / 2)) * 180 / Math.PI;
            var degreesPerPixel = Map.CurrentExtent.Width / 6378137.0 * 180 / Math.PI / Map.ActualWidth;
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
                // The card hides, but the last hovered aircraft keeps its trail - a
                // trail you had to keep the cursor on would never grow long enough
                // to say anything.
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

        /// <summary>Climbing out amber, mid-level green, cruising blue.</summary>
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
