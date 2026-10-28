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
    /// The same vector tiles displayed in any projection, and walked from one to the
    /// next. Tiles and coordinates stay web mercator throughout - a projection changes
    /// only how that plane reaches the screen, without CPU reprojection. Moving to
    /// a regional overview loads tiles for that view. Ten families are coded into the renderer; everything
    /// else is drawn from a grid PROJ fills in, and behaves identically.
    /// </summary>
    public partial class MorphBetweenProjectionsMvt : UserControl
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
            // No formula for these two is coded into the renderer: they are drawn
            // from a grid PROJ filled in, and behave in every way like the ones
            // above - which is the point of having them in the list.
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

        internal const double WorldHalfSize = 20037508.342789244;

        /// <summary>How long the projection morph takes, matching the shapefile
        /// sample's so the two read as the same gesture.</summary>
        private static readonly TimeSpan MorphDuration = TimeSpan.FromMilliseconds(900);

        /// <summary>Whether the checkbox is asking for the animated change.</summary>
        private bool MorphRequested => ChkMorph?.IsChecked == true;

        private Choice _choice = Choices["RdoMercator"];
        private bool _ready;
        private bool _rebuilding;
        private bool _showBusy;
        private double _lastBuildMs;

        public MorphBetweenProjectionsMvt()
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

            await ShowAsync(_choice);
            _ready = true;
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready || sender is not RadioButton button || !Choices.TryGetValue(button.Name, out var choice))
                return;

            if (choice.Definition == _choice.Definition)
                return;

            await ShowAsync(choice);
        }

        private async Task ShowAsync(Choice choice)
        {
            _showBusy = true;
            try
            {
                // Global projections keep the current view. Regional projections
                // start on their own ground instead of warping an unrelated view.
                var before = Map.CurrentExtent;
                _choice = choice;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                var target = ProjectionFor(choice.Definition);

                // A projection change SWITCHES the projection on the running map - the
                // renderer rebuilds its shaders in place and the tiles hand over like a
                // restyle - instead of swapping the basemap, which starts from an empty
                // scene and is the flash this sample kept being caught on.
                DisplayProjection morphTarget = null;
                _rebuilding = true;
                try
                {
                    if (!_ready)
                    {
                        Map.Basemap = new GpuBasemap(new MapStyle(
                            ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
                    }

                    // The morph is deliberately NOT run yet: the camera writes below
                    // decide where the new projection frames the ground, and animating
                    // before them would unroll the world and then jump it.
                    if (MorphRequested && _ready)
                    {
                        morphTarget = target;
                    }
                    else
                    {
                        Map.Basemap.DisplayProjection = target;
                    }

                    // The projected canvas is not cyclic: zoomed out past one world the
                    // renderer tiles it sideways with a second copy. Zoom-out stops where
                    // the world fills the window; mercator keeps its usual freedom.
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

                    if (morphTarget != null)
                    {
                        await Map.Basemap.AnimateDisplayProjectionAsync(morphTarget, MorphDuration);
                    }
                }
                finally
                {
                    _rebuilding = false;
                }

                _lastBuildMs = stopwatch.Elapsed.TotalMilliseconds;
                var label = choice.Definition.Length > 26 ? choice.Definition.Substring(0, 26) : choice.Definition;
                LogTiming(string.Create(CultureInfo.InvariantCulture,
                    $"switch {label} ={_lastBuildMs:0}ms"), stamp: false);

                ShowStatus();
                WarnIfOutsideRegion();
            }
            finally
            {
                _showBusy = false;
            }
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

        // ---- the acceptance run's window into the sample ----------------------

        /// <summary>Settled: ready, and no projection change in flight.</summary>
        internal bool IsIdle => _ready && !_rebuilding && !_showBusy;

        internal string WarnDebug = string.Empty;

        internal RectangleShape GroundNow() => CurrentGround();

        internal double LastBuildMilliseconds => _lastBuildMs;

        /// <summary>
        /// Where a ground point lands on the canvas. The map API speaks web mercator
        /// whatever the display projection is, so this is simply the mercator
        /// coordinate - the projection happens on the way to the screen.
        /// </summary>
        internal PointShape CanvasPointFor(string radioName, double lon, double lat) =>
            ToMercatorPoint(lon, lat);

        // ---- the timing panel -------------------------------------------------

        private readonly Queue<string> _timingLines = new Queue<string>();

        internal void LogTiming(string line, bool stamp = true)
        {
            void Append()
            {
                _timingLines.Enqueue(stamp
                    ? string.Create(CultureInfo.InvariantCulture, $"{DateTime.Now:mm:ss.f} {line}")
                    : line);
                while (_timingLines.Count > 80)
                    _timingLines.Dequeue();

                if (TimingLog != null)
                {
                    TimingLog.Text = string.Join(Environment.NewLine, _timingLines);
                    TimingLog.ScrollToEnd();
                }
            }

            if (Dispatcher.CheckAccess())
                Append();
            else
                Dispatcher.BeginInvoke((Action)Append);
        }

        // ---- ground and view --------------------------------------------------

        /// <summary>What the current view covers, in longitude and latitude.</summary>
        private RectangleShape CurrentGround()
        {
            var view = Map.CurrentExtent;
            return ToGeographic(view);
        }

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

            WarnDebug = string.Create(CultureInfo.InvariantCulture,
                $"ground {ground.MinX:0.#}..{ground.MaxX:0.#} lat {ground.MinY:0.#}..{ground.MaxY:0.#} region {region.MinX:0.#}..{region.MaxX:0.#} outside={outside} entirelyOutside={entirelyOutside}");
            WarningText.Visibility = outside ? Visibility.Visible : Visibility.Collapsed;
            if (!outside)
                return;

            var isMga = _choice == Choices["RdoMga55"];
            var projectionName = isMga ? "MGA Zone 55" : "Albers Conic";
            var regionName = isMga ? "eastern Australia" : "the US region shown by this sample";
            WarningText.Foreground = new System.Windows.Media.SolidColorBrush(
                entirelyOutside
                    ? System.Windows.Media.Color.FromRgb(0xB0, 0x4A, 0x2A)
                    : System.Windows.Media.Color.FromRgb(0x8A, 0x5A, 0x00));
            WarningText.Text = entirelyOutside
                ? $"The current view is outside this projection's intended region. Return to {regionName}."
                : $"{projectionName} is intended for {regionName}. This view extends beyond that region, where distortion may increase. Zoom into the region for a more representative view.";
        }

        private void ShowStatus()
        {
            if (StatusText == null)
                return;

            var ground = CurrentGround();
            var projection = ProjectionFor(_choice.Definition);
            StatusText.Text = string.Create(CultureInfo.InvariantCulture,
                $"1:{Map.CurrentScale:N0}   lon {ground.MinX:0.#} to {ground.MaxX:0.#}   " +
                $"lat {ground.MinY:0.#} to {ground.MaxY:0.#}   " +
                $"{(projection.IsNativelySupported ? "coded family" : "grid-backed")}");
        }
    }
}
