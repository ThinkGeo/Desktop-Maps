using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Aerial imagery draped over terrain at the Grand Canyon, with relief shading and an elevation profile between two clicks.
    /// </summary>
    public partial class Terrain3D
    {
        // The south rim, looking north across the gorge.
        private static readonly RectangleShape StartExtent =
            new RectangleShape(-12_534_000, 4_346_000, -12_484_000, 4_314_000);

        private readonly GpuBasemap _basemap;
        private readonly MapStyle _style;

        // The measured line is drawn by the renderer, not by an overlay, so each vertex
        // sits on the terrain under it instead of lying flat across the tilted picture.
        private readonly DynamicSource _profileLine = new DynamicSource();

        private bool _initialized;
        private PointShape _profileStart;

        public Terrain3D()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.IsTiltEnabled = true;

            // Two quick clicks are a double click: under the default MutuallyExclusive the
            // second click raises only MapDoubleClick, so the profile would never get its end.
            Map.ClickDoubleClickMode = MapClickDoubleClickMode.RaiseClickThenDoubleClick;

            // The AWS open terrarium tiles (no key), as a code-built RasterDemSource. SetTerrain
            // adds it under the id "terrain-dem", which the relief layer in the resources reads.
            var dem = new RasterDemSource("https://s3.amazonaws.com/elevation-tiles-prod/terrarium/{z}/{x}/{y}.png", DemEncoding.Terrarium);
            _style = new MapStyle()
                .AddRaster(new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1), sourceId: "thinkgeo")
                .SetTerrain(dem, 1.35)
                .AddStyle((string)Resources["StyleJson"]);
            _style.AddDynamic(_profileLine);
            _basemap = new GpuBasemap(_style);
            Map.Basemap = _basemap;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _initialized = true;
            Map.CurrentExtent = StartExtent;
            Map.TiltAngle = 63;
            await Map.RefreshAsync();
        }

        // Terrain lives on the style; re-handing the style applies the toggle without
        // rebuilding a tile.
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

        private async void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var clicked = new PointShape(e.WorldLocation.X, e.WorldLocation.Y);
            var elevation = await _basemap.GetElevationAsync(clicked, 3857);
            if (elevation == null)
            {
                ProfileText.Text = "No elevation here - the DEM does not cover this point.";
                _profileStart = null;
                return;
            }

            if (_profileStart == null)
            {
                _profileStart = clicked;
                _profileLine.Clear();
                _profileLine.SetPoint("start", new Feature(_profileStart), color: GeoColors.Yellow, sizeInPixel: 9f);
                ProfileText.Text = string.Format(CultureInfo.InvariantCulture, "Start {0:0} m - click a second point.", elevation.Value);
                return;
            }

            _profileLine.SetPoint("end", new Feature(clicked), color: GeoColors.Yellow, sizeInPixel: 9f);
            await ShowProfileAsync(_profileStart, clicked);
            _profileStart = null;
        }

        /// <summary>
        /// One call gives the whole profile, read from the tiles the terrain is already drawn from.
        /// </summary>
        private async Task ShowProfileAsync(PointShape from, PointShape to)
        {
            var line = new LineShape(new[] { new Vertex(from), new Vertex(to) });
            var profile = await _basemap.GetElevationProfileAsync(line, 3857, intervalInMeters: 100);

            if (profile.MinElevationInMeters == null)
            {
                ProfileText.Text = "Not enough elevation along that line.";
                _profileLine.SetLine("profile", new Feature(line), GeoColors.Yellow, 3f);
                ChartPanel.Visibility = Visibility.Collapsed;
                return;
            }

            // Drawn through the profile's own samples rather than the two clicks: a
            // two-point line would sit on the ground only at its ends.
            var samples = profile.Points.Select(sample => new Vertex(sample.Longitude, sample.Latitude));
            _profileLine.SetLine("profile", new Feature(new LineShape(ProjectionConverter.Convert(4326, 3857, samples))), GeoColors.Yellow, 3f);

            var min = profile.MinElevationInMeters.Value;
            var max = profile.MaxElevationInMeters.Value;
            ProfileText.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.0} km - min {1:0} m, max {2:0} m, relief {3:0} m",
                profile.LengthInMeters / 1000.0, min, max, max - min);

            // Distance across, elevation up, scaled to the samples' own range so a
            // canyon and a plain both fill the box.
            var span = Math.Max(1, max - min);
            var points = new PointCollection();
            foreach (var sample in profile.Points)
            {
                if (sample.ElevationInMeters is not { } elevation)
                {
                    continue;
                }

                var x = sample.DistanceInMeters / profile.LengthInMeters * Chart.Width;
                var y = Chart.Height - ((elevation - min) / span * (Chart.Height - 8)) - 4;
                points.Add(new Point(x, y));
            }

            ChartLine.Points = points;
            ChartTitle.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:0.0} km, {1:0} - {2:0} m",
                profile.LengthInMeters / 1000.0, min, max);
            ChartPanel.Visibility = Visibility.Visible;
        }
    }
}
