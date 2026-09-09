using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// 3D terrain over the Grand Canyon: an elevation source shapes the ground and aerial
    /// imagery drapes over it. The elevation source is built IN CODE - a DemSource handed
    /// to SetTerrain, no style document declaring it - and lands in the style under the id
    /// <c>terrain-dem</c>, which the relief layer in the editor references. (A style
    /// document carrying its own <c>raster-dem</c> and root <c>terrain</c> lights up
    /// exactly the same with no code at all.) The checkbox flips terrain at runtime
    /// through SetTerrain / RemoveTerrain and one SetStyleAsync. Click two points for an
    /// elevation profile read from the same DEM.
    /// </summary>
    public partial class Terrain3D
    {
        // The south rim, looking north across the gorge.
        private static readonly RectangleShape StartExtent =
            new RectangleShape(-12_534_000, 4_346_000, -12_484_000, 4_314_000);

        private readonly ThinkGeoRasterTileSource _aerial =
            new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoRasterMapType.Aerial);

        // The AWS open terrarium tiles (no key), as a code-built DemSource.
        private readonly DemSource _dem = new DemSource(
            "https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png",
            DemEncoding.Terrarium);

        private readonly GpuBasemap _basemap;
        private readonly DispatcherTimer _applyTimer;
        private MapStyle _style;

        /// <summary>
        /// The line being measured, drawn by the renderer rather than by an overlay
        /// above it: an overlay draws flat, and on a map tilted to 63 degrees a flat
        /// line lies across the picture instead of along the ground it measures. The
        /// renderer puts every vertex at the terrain height under it, so the line
        /// climbs the ridges it crosses and hides where one stands in front of it.
        /// </summary>
        private readonly InMemoryGeometrySource _profileLine = new InMemoryGeometrySource();

        private bool _initialized;
        private PointShape _profileStart;

        public Terrain3D()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.TiltInteractionEnabled = true;

            // The profile is two clicks, and two clicks arriving quickly are a double
            // click: under the default MutuallyExclusive the pair raises MapDoubleClick
            // and no MapClick at all, so the second point never lands and the map zooms
            // instead. Asking for both clicks to be reported is what makes the gesture
            // this sample describes actually work.
            Map.ClickDoubleClickMode = MapClickDoubleClickMode.RaiseClickThenDoubleClick;

            _style = ComposeStyle();
            _basemap = new GpuBasemap(_style);
            Map.Basemap = _basemap;

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            Map.CurrentExtent = StartExtent;
            Map.TiltAngle = 63;
            await Map.RefreshAsync();
        }

        /// <summary>
        /// The aerial imagery goes in first (bottom of the layer order), the DEM lands
        /// under its id, and the relief layer in the editor reads that id. The checkbox
        /// decides whether the terrain it declares is also raised.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle()
                .AddRaster(_aerial, "thinkgeo")
                .SetTerrain(_dem, 1.35)
                .AddStyle(Editor.Text);
            if (TerrainToggle.IsChecked != true)
            {
                style.RemoveTerrain();
            }

            style.AddGeometry(_profileLine, new GeometryStyle
            {
                Color = GeoColors.Yellow,
                LineColor = GeoColors.Yellow,
                LineWidthInPixels = 3,
            });
            return style;
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                _style = ComposeStyle();
                await _basemap.SetStyleAsync(_style);
                Status.Foreground = System.Windows.Media.Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = System.Windows.Media.Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        // Terrain lives on the style; re-handing the style applies the toggle
        // without rebuilding a tile (the document itself did not change).
        private async void TerrainToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _style.SetTerrain("terrain-dem", 1.35);
            await _basemap.SetStyleAsync(_style);
            _ = Map.RefreshAsync();
        }

        private async void TerrainToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            _style.RemoveTerrain();
            await _basemap.SetStyleAsync(_style);
            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// The elevation query asks in degrees; this map is in Spherical Mercator meters,
        /// so a click has to be converted rather than passed through. Handing it meters
        /// answers from the corner of the world instead of failing, so the number looks
        /// like an answer.
        /// </summary>
        private static PointShape ToDegrees(double worldX, double worldY) =>
            (PointShape)ProjectionConverter.Convert(3857, 4326, new PointShape(worldX, worldY));

        private async void Map_MapClick(object sender, MapClickMapViewEventArgs e)
            => await AddProfilePointAsync(new PointShape(e.WorldX, e.WorldY));

        private async Task AddProfilePointAsync(PointShape clicked)
        {
            var inDegrees = ToDegrees(clicked.X, clicked.Y);
            var elevation = await _basemap.GetElevationAsync(inDegrees.X, inDegrees.Y);
            if (elevation == null)
            {
                ProfileText.Text = "no elevation here - the DEM does not cover this point";
                _profileStart = null;
                return;
            }

            if (_profileStart == null)
            {
                _profileStart = clicked;
                // The first click on its own is a point, so there is something on the
                // ground to aim the second one from.
                _profileLine.UpdatePoints(new[] { _profileStart }, sizeInPixels: 9f);
                _profileLine.UpdateLines(Array.Empty<LineShape>());
                ProfileText.Text = string.Format(CultureInfo.InvariantCulture, "start {0:0} m - click a second point", elevation.Value);
                return;
            }

            _profileLine.UpdatePoints(new[] { _profileStart, clicked }, sizeInPixels: 9f);
            await ShowProfileAsync(_profileStart, clicked);
            _profileStart = null;
        }

        /// <summary>
        /// One call gives the whole profile: the line goes in with its srid, the samples
        /// come back evenly spaced on the ground with the extremes precomputed. The
        /// tiles are the ones the terrain is already drawn from.
        /// </summary>
        private async Task ShowProfileAsync(PointShape from, PointShape to)
        {
            var line = new LineShape(new[] { new Vertex(from), new Vertex(to) });
            var profile = await _basemap.GetElevationProfileAsync(line, 3857, intervalInMeters: 100);

            if (profile.MinElevationInMeters == null)
            {
                ProfileText.Text = "not enough elevation along that line";
                _profileLine.UpdateLines(new[] { line });
                ChartPanel.Visibility = Visibility.Collapsed;
                return;
            }

            // Drawn as the profile's own samples rather than as the two clicks: each
            // vertex is put on the ground under it, so a two-point line would be lifted
            // only at its ends and tunnel through everything in between.
            var samples = profile.Points.Select(sample => new Vertex(sample.Longitude, sample.Latitude));
            _profileLine.UpdateLines(new[] { new LineShape(ProjectionConverter.Convert(4326, 3857, samples)) });

            ProfileText.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.0} km - min {1:0} m, max {2:0} m, relief {3:0} m",
                profile.LengthInMeters / 1000.0,
                profile.MinElevationInMeters,
                profile.MaxElevationInMeters,
                profile.MaxElevationInMeters - profile.MinElevationInMeters);

            DrawChart(profile);
        }

        /// <summary>
        /// The profile as a chart: distance along the line across, elevation up, scaled
        /// to the samples' own range so a canyon and a plain both fill the box.
        /// </summary>
        private void DrawChart(ElevationProfile profile)
        {
            Chart.Children.Clear();

            var min = profile.MinElevationInMeters.Value;
            var max = profile.MaxElevationInMeters.Value;
            var span = Math.Max(1, max - min);

            var points = new System.Windows.Media.PointCollection();
            foreach (var sample in profile.Points)
            {
                if (sample.ElevationInMeters is not { } elevation)
                {
                    continue;
                }

                var x = sample.DistanceInMeters / profile.LengthInMeters * Chart.Width;
                var y = Chart.Height - ((elevation - min) / span * (Chart.Height - 8)) - 4;
                points.Add(new System.Windows.Point(x, y));
            }

            Chart.Children.Add(new System.Windows.Shapes.Polyline
            {
                Points = points,
                Stroke = System.Windows.Media.Brushes.DarkGreen,
                StrokeThickness = 1.5,
            });

            ChartTitle.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.0} km, {1:0} - {2:0} m",
                profile.LengthInMeters / 1000.0, min, max);
            ChartPanel.Visibility = Visibility.Visible;
        }
    }
}
