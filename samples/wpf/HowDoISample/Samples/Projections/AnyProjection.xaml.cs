using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Show a map - vector tiles or satellite imagery - in any projection, and animate the change from one projection to the next.
    /// </summary>
    public partial class AnyProjection : UserControl
    {
        private sealed record Choice(string Definition, RectangleShape Region, RectangleShape InitialView = null);

        private static readonly Dictionary<string, Choice> Choices = new Dictionary<string, Choice>
        {
            ["RdoMercator"] = new Choice("3857", null),
            ["RdoEquirectangular"] = new Choice("4326", null),
            ["RdoRobinson"] = new Choice("+proj=robin +datum=WGS84 +units=m +no_defs", null),
            ["RdoMollweide"] = new Choice("+proj=moll +datum=WGS84 +units=m +no_defs", null),
            ["RdoEckert"] = new Choice("+proj=eck4 +datum=WGS84 +units=m +no_defs", null),
            ["RdoCylindrical"] = new Choice("+proj=cea +datum=WGS84 +units=m +no_defs", null),
            ["RdoSinusoidal"] = new Choice("+proj=sinu +lon_0=0 +datum=WGS84 +units=m +no_defs", null),
            ["RdoBonne"] = new Choice("+proj=bonne +lat_1=45 +lon_0=0 +datum=WGS84 +units=m +no_defs", null),
            ["RdoAlbers"] = new Choice(
                "+proj=aea +lat_1=29.5 +lat_2=45.5 +lat_0=23 +lon_0=-96 +datum=NAD83 +units=m +no_defs",
                new RectangleShape(-170, 72, -52, 12)),
            ["RdoMga55"] = new Choice(
                "+proj=utm +zone=55 +south +datum=WGS84 +units=m +no_defs",
                new RectangleShape(143, -9, 151, -44),
                // Center the Australian overview near 132E rather than the zone's
                // central meridian (147E). Keep the actual zone bounds for warnings.
                new RectangleShape(128, -9, 136, -44)),
        };

        private const double WorldHalfSize = 20037508.342789244;

        private static readonly TimeSpan MorphDuration = TimeSpan.FromMilliseconds(900);

        private Choice _choice = Choices["RdoMercator"];
        private bool _ready;

        public AnyProjection()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
                return;

            // Built once; _ready is only set after the first draw.
            Map.SizeChanged -= Map_SizeChanged;
            Map.MapUnit = GeographyUnit.Meter;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#DCE8EF"));

            Map.CurrentExtentChanged += (_, _) =>
            {
                ShowStatus();
                WarnIfOutsideRegion();
            };

            ShowBasemap();
            await ShowAsync(_choice);
            _ready = true;
        }

        private async void Basemap_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready)
                return;

            try
            {
                ShowBasemap();
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready || sender is not RadioButton button || !Choices.TryGetValue(button.Name, out var choice))
                return;

            if (choice.Definition == _choice.Definition)
                return;

            try
            {
                await ShowAsync(choice);
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// A raster basemap and a vector one are different styles over different sources,
        /// so switching between them builds a new basemap, in the projection already chosen.
        /// </summary>
        private void ShowBasemap()
        {
            var style = MapSatellite.IsChecked == true
                ? new MapStyle().AddRaster(new ThinkGeoRasterTileSource(SampleShared.CloudApiKey, ThinkGeoCloudRasterMapsMapType.Aerial_V2_X2), sourceId: "thinkgeo")
                : new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            Map.Basemap = new GpuBasemap(style);
            Map.Basemap.DisplayProjection = ProjectionFor(_choice.Definition);
        }

        private async Task ShowAsync(Choice choice)
        {
            // Global projections keep the current view. Regional projections
            // start on their own ground instead of warping an unrelated view.
            var before = Map.CurrentExtent;
            _choice = choice;
            var target = ProjectionFor(choice.Definition);

            // The animation waits for the camera writes below: they decide where the new
            // projection frames the ground, and animating before them would unroll the
            // world and then jump it.
            var animate = _ready;
            if (!animate)
            {
                Map.Basemap.DisplayProjection = target;
            }

            // A projected map is not cyclic: zoomed out past one world it would show a
            // second copy sideways. Zoom-out stops where the world fills the window;
            // mercator keeps its usual freedom.
            if (choice.Definition == "3857")
            {
                Map.MaximumScale = double.MaxValue;
            }
            else
            {
                var world = new RectangleShape(-WorldHalfSize, WorldHalfSize, WorldHalfSize, -WorldHalfSize);
                Map.MaximumScale = MapUtil.GetScale(world, Math.Max(1, Map.ActualWidth), GeographyUnit.Meter) * 1.05;
                if (Map.CurrentScale > Map.MaximumScale)
                    Map.CurrentScale = Map.MaximumScale;
            }

            if (choice.Region != null)
            {
                // Regional projections cannot display the previous worldwide
                // view. Frame their ground before refreshing or starting the
                // morph so the first settled view already shows useful data.
                // Map coordinates remain Web Mercator, even for a UTM display.
                Map.CurrentExtent = ToMercator(choice.InitialView ?? choice.Region);
            }
            else if (_ready)
            {
                Map.CurrentExtent = before;
            }
            else
            {
                Map.CurrentExtent = ToMercator(new RectangleShape(-180, 85, 180, -85));
            }

            await Map.RefreshAsync();

            if (animate)
            {
                try
                {
                    await Map.Basemap.AnimateDisplayProjectionAsync(target, MorphDuration);
                }
                catch (OperationCanceledException)
                {
                }
            }

            ShowStatus();
            WarnIfOutsideRegion();
        }

        /// <summary>
        /// The display projection for a definition. "4326" means plate carree here,
        /// which is the equirectangular family - the same picture, drawn in meters
        /// instead of degrees.
        /// </summary>
        private static DisplayProjection ProjectionFor(string definition) => definition switch
        {
            "3857" => DisplayProjection.WebMercator,
            "4326" => DisplayProjection.FromName("equirectangular"),
            _ => DisplayProjection.FromProjString(definition),
        };

        /// <summary>What the current view covers, in longitude and latitude.</summary>
        private RectangleShape CurrentGround()
        {
            var view = Map.CurrentExtent;
            return ToGeographic(view);
        }

        // View arithmetic, not data: a view can reach past the world's edge, and
        // ProjectionConverter clips a rectangle to the world and wraps a longitude past
        // 180, so these stay the plain formulas.
        private static PointShape ToMercatorPoint(double lon, double lat)
        {
            var clamped = Math.Max(-85.05112878, Math.Min(85.05112878, lat));
            return new PointShape(
                lon / 180.0 * WorldHalfSize,
                Math.Log(Math.Tan((90.0 + clamped) * Math.PI / 360.0)) / (Math.PI / 180.0) / 180.0 * WorldHalfSize);
        }

        private static RectangleShape ToMercator(RectangleShape ground)
        {
            var upperLeft = ToMercatorPoint(ground.MinX, ground.MaxY);
            var lowerRight = ToMercatorPoint(ground.MaxX, ground.MinY);
            return new RectangleShape(upperLeft.X, upperLeft.Y, lowerRight.X, lowerRight.Y);
        }

        private static RectangleShape ToGeographic(RectangleShape mercator)
        {
            double Lon(double x) => x / WorldHalfSize * 180.0;
            double Lat(double y) => 180.0 / Math.PI *
                (2.0 * Math.Atan(Math.Exp(y / WorldHalfSize * Math.PI)) - Math.PI / 2.0);

            return new RectangleShape(Lon(mercator.MinX), Lat(mercator.MaxY), Lon(mercator.MaxX), Lat(mercator.MinY));
        }

        /// <summary>Explain the regional projection's limits without treating
        /// a deliberately broad overview as a rendering failure.</summary>
        private void WarnIfOutsideRegion()
        {
            if (WarningText == null)
                return;

            if (_choice.Region == null)
            {
                WarningText.Visibility = Visibility.Collapsed;
                return;
            }

            var ground = CurrentGround();
            var region = _choice.Region;
            var outside = ground.MinX < region.MinX || ground.MaxX > region.MaxX
                || ground.MinY < region.MinY || ground.MaxY > region.MaxY;
            var entirelyOutside = ground.MaxX <= region.MinX || ground.MinX >= region.MaxX
                || ground.MaxY <= region.MinY || ground.MinY >= region.MaxY;

            WarningText.Visibility = outside ? Visibility.Visible : Visibility.Collapsed;
            if (!outside)
                return;

            var isMga = _choice == Choices["RdoMga55"];
            var projectionName = isMga ? "MGA Zone 55" : "Albers Conic";
            var regionName = isMga ? "eastern Australia" : "the US region shown by this sample";
            WarningText.Foreground = new System.Windows.Media.SolidColorBrush(
                entirelyOutside
                    ? System.Windows.Media.Color.FromRgb(0xFA, 0x80, 0x72)
                    : System.Windows.Media.Color.FromRgb(0xE8, 0xB8, 0x4A));
            WarningText.Text = entirelyOutside
                ? $"The current view is outside this projection's intended region. Return to {regionName}."
                : $"{projectionName} is intended for {regionName}. This view extends beyond that region, where distortion may increase. Zoom into the region for a more representative view.";
        }

        private void ShowStatus()
        {
            if (StatusText == null)
                return;

            var ground = CurrentGround();
            StatusText.Text = string.Create(CultureInfo.InvariantCulture,
                $"1:{Map.CurrentScale:N0}   lon {ground.MinX:0.#} to {ground.MaxX:0.#}   " +
                $"lat {ground.MinY:0.#} to {ground.MaxY:0.#}");
        }
    }
}
