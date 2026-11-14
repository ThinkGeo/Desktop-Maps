using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Drag Greenland toward the equator and watch the area Web Mercator gives the same ground shrink.
    /// </summary>
    public partial class MeasureInTheRightProjection
    {
        // An equal-area projection about a latitude and Greenland's own longitude.
        private const string EqualArea = "+proj=laea +lat_0={0} +lon_0={1} +x_0=0 +y_0=0 +datum=WGS84 +units=m +no_defs";

        // 84.5 degrees north or south in Web Mercator; past it the stretch runs away towards infinity.
        private const double MaxMercatorY = 19363000;

        private const double MetresPerDegree = 20037508.342789244 / 180;

        private readonly DynamicSource _greenland = new DynamicSource();
        private readonly ProjectionConverter _toDegrees = new ProjectionConverter(3857, 4326);
        private MultipolygonShape _flat;
        private MultipolygonShape _placed;
        private BaseShape _copy;
        private double _homeLatitude;
        private double _homeLongitude;
        private double _groundArea;
        private double _latitude;
        private double _drawnLatitude = double.NaN;
        private double _shift;
        private PointShape _grab;
        private bool _drawQueued;
        private bool _initialized;

        public MeasureInTheRightProjection()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var tileSource = new FeatureSourceVectorTileSource();
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Countries02.shp")) { ProjectionConverter = new ProjectionConverter(4326, 3857) });
            Map.Basemap = new GpuBasemap(new MapStyle()
                .SetBackground(GeoColor.FromHtml("#C9DCE8"))
                .AddStyle((string)Resources["StyleJson"], tileSource)
                .AddDynamic(_greenland));
            Map.CenterPoint = new PointShape(-4608600, 5621500);
            Map.CurrentScale = 130000000;

            // Greenland in metres of an equal-area projection about its own middle: the ground it
            // covers, wherever it is put down.
            var countries = new ShapeFileFeatureSource(SampleShared.Shapefile("Countries02.shp"));
            countries.Open();
            var greenland = (MultipolygonShape)countries.GetAllFeatures(new[] { "CNTRY_NAME" })
                .First(feature => feature.ColumnValues["CNTRY_NAME"].Trim() == "Greenland").GetShape();
            countries.Close();
            var home = greenland.GetCenterPoint();
            _homeLatitude = home.Y;
            _homeLongitude = home.X;

            var flatten = new ProjectionConverter(new Projection(4326), new Projection(string.Format(CultureInfo.InvariantCulture, EqualArea, _homeLatitude, _homeLongitude)));
            flatten.Open();
            var flat = ((MultipolygonShape)flatten.ConvertToExternalProjection(greenland)).Simplify(4000, SimplificationType.DouglasPeucker);
            flatten.Close();
            _flat = new MultipolygonShape(flat.Polygons
                .Where(polygon => polygon.OuterRing.GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers) > 500)
                .Select(polygon => new PolygonShape(polygon.OuterRing)));
            _groundArea = _flat.GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers);
            _toDegrees.Open();

            var atHome = Place(_homeLatitude);
            _greenland.SetArea("home", new Feature(atHome), GeoColor.FromArgb(46, 90, 90, 90), GeoColor.FromHtml("#666666"), 1.5f);
            Truth.Text = FormattableString.Invariant($"{_groundArea / 1e6:0.00} M km²");
            AtHome.Text = FormattableString.Invariant($"{atHome.GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers) / 1e6:0.00} M km²");

            _latitude = _homeLatitude;
            Draw();
        }

        // The same ground put down about another latitude, in Web Mercator.
        private MultipolygonShape Place(double latitude)
        {
            var converter = new ProjectionConverter(new Projection(string.Format(CultureInfo.InvariantCulture, EqualArea, latitude, _homeLongitude)), new Projection(3857));
            converter.Open();
            var placed = (MultipolygonShape)converter.ConvertToExternalProjection(_flat);
            converter.Close();
            return placed;
        }

        private void Draw()
        {
            _drawQueued = false;
            if (_latitude != _drawnLatitude)
            {
                var candidate = Place(_latitude);
                var bounds = candidate.GetBoundingBox();
                if (Math.Max(Math.Abs(bounds.UpperLeftPoint.Y), Math.Abs(bounds.LowerRightPoint.Y)) > MaxMercatorY)
                {
                    _latitude = _drawnLatitude;
                }
                else
                {
                    _placed = candidate;
                    _drawnLatitude = _latitude;

                    // A sideways move costs Web Mercator nothing, so the figures follow the latitude alone.
                    var here = _placed.GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers);
                    var blown = here / _groundArea;
                    Here.Text = FormattableString.Invariant($"{here / 1e6:0.00} M km²");
                    Where.Text = FormattableString.Invariant($"{_latitude:0.0}°");
                    Blow.Text = FormattableString.Invariant($"{blown:0.00} times too big");
                    Blow.Foreground = blown < 1.05 ? new SolidColorBrush(Color.FromRgb(46, 125, 50)) : new SolidColorBrush(Color.FromRgb(192, 57, 43));
                    BlowNote.Text = blown < 1.05
                        ? "At the equator Web Mercator tells the truth."
                        : FormattableString.Invariant($"The map draws {blown:0.0} times the ground it covers.");
                }
            }

            _copy = new Feature(_placed).GetShape();
            _copy.TranslateByOffset(_shift * MetresPerDegree, 0);
            _greenland.SetArea("copy", new Feature(_copy), GeoColor.FromArgb(115, 200, 60, 30), GeoColor.FromHtml("#C0392B"), 2f);
        }

        // Whatever was taken hold of stays under the pointer while the map holds still.
        private void Map_MapMouseDown(object sender, MapMouseDownMapViewEventArgs e)
        {
            if (e.MouseButton != MapMouseButton.Left || _copy == null || !_copy.Contains(e.WorldLocation)) return;

            var degrees = (PointShape)_toDegrees.ConvertToExternalProjection(e.WorldLocation);
            _grab = new PointShape(degrees.X - _shift, degrees.Y - _latitude);
            Map.ExtentOverlay.PanMode = MapPanMode.Disabled;
        }

        private void Map_MapMouseMove(object sender, MapMouseMoveMapViewEventArgs e)
        {
            // A button let go outside the map sends no MapMouseUp.
            if (_grab != null && Mouse.LeftButton != MouseButtonState.Pressed)
            {
                Map_MapMouseUp(sender, null);
            }

            if (_grab == null)
            {
                Map.Cursor = _copy != null && _copy.Contains(e.WorldLocation) ? Cursors.Hand : null;
                return;
            }

            var degrees = (PointShape)_toDegrees.ConvertToExternalProjection(e.WorldLocation);
            _shift = degrees.X - _grab.X;
            _latitude = degrees.Y - _grab.Y;
            if (_drawQueued) return;

            _drawQueued = true;
            Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(Draw));
        }

        private void Map_MapMouseUp(object sender, MapMouseUpMapViewEventArgs e)
        {
            if (_grab == null) return;

            _grab = null;
            Map.ExtentOverlay.PanMode = MapPanMode.StandardPanning;
        }

        private void Equator_Click(object sender, RoutedEventArgs e) => TravelTo(0);

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            _shift = 0;
            TravelTo(_homeLatitude);
        }

        // The buttons travel the way a hand would, so the shrinking is still something watched.
        private void TravelTo(double target)
        {
            if (!_initialized) return;

            var from = _latitude;
            var clock = Stopwatch.StartNew();
            EventHandler step = null;
            step = (s, e) =>
            {
                var part = Math.Min(1, clock.ElapsedMilliseconds / 900.0);
                _latitude = from + (target - from) * (part < 0.5 ? 2 * part * part : 1 - 2 * (1 - part) * (1 - part));
                Draw();
                if (part >= 1) CompositionTarget.Rendering -= step;
            };
            CompositionTarget.Rendering += step;
        }
    }
}
