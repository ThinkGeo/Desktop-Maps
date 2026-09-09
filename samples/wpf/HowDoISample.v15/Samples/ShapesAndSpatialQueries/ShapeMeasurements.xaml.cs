using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Measure a shape: its area, its length, its center point, the shortest line from
    /// it to somewhere else, or a stretch of it starting a given distance along. Click
    /// a feature and the answer is one call on the shape it carries.
    /// </summary>
    public partial class ShapeMeasurements
    {
        // Toyota Stadium, the fixed end of the shortest-line measurement.
        private static readonly PointShape Stadium = new PointShape(-10779651.5, 3915933.0);

        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        private readonly DispatcherTimer _applyTimer;
        private GpuBasemap _basemap;
        private ShapeFileFeatureSource _data;
        private FeatureSourceVectorTileSource _tiles;
        private readonly InMemoryGeometrySource _measured = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _stadium = new InMemoryGeometrySource();
        private bool _ready;

        public ShapeMeasurements()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            Map.MapUnit = GeographyUnit.Meter;

            _stadium.UpdatePoints(new[] { Stadium }, sizeInPixels: 18f, iconName: "stadium");

            ChooseData();
            _basemap = new GpuBasemap(ComposeStyle());
            Map.Basemap = _basemap;
            Map.CenterPoint = new PointShape(-10778340, 3915490);
            Map.CurrentScale = 36110;

            Map.MapClick += (s, args) => Measure(args.WorldLocation);

            _ready = true;
            ShowMeasurement();
            await Map.RefreshAsync();
        }

        private async void Measurement_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;

            // The measurements do not all read the same file, so switching one restyles
            // the map over a new source rather than rebuilding the basemap - which would
            // start from an empty scene and flash.
            ChooseData();
            await _basemap.SetStyleAsync(ComposeStyle());
            ShowMeasurement();
            await Map.RefreshAsync();
        }

        // The feature the numbers on screen are about, so changing a parameter
        // re-measures it rather than needing another click.
        private Feature _measuring;

        private void Parameter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (_ready) Measure(_measuring);
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ready) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await _basemap.SetStyleAsync(ComposeStyle());
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        /// <summary>
        /// The data the chosen measurement reads, and the tiles it is cut into. Parks
        /// for the ones that measure an area, trails for the ones that measure along a
        /// line.
        /// </summary>
        private void ChooseData()
        {
            var lines = MeaLength.IsChecked == true || MeaSubLine.IsChecked == true;
            var file = lines ? "Hike_Bike.shp" : "Parks.shp";

            // Nothing here is drawn from a layer, so nothing here needs one: a query runs
            // on a FeatureSource.
            _data = new ShapeFileFeatureSource(SampleShared.Shapefile(file))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };

            // A second reader over the same file: the tile cutter queries its own source,
            // and this sample queries _data on every click.
            var drawn = new ShapeFileFeatureSource(SampleShared.Shapefile(file))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };

            _tiles = new FeatureSourceVectorTileSource();
            _tiles.FeatureSources.Add("data", drawn);
        }

        /// <summary>
        /// The chosen data under the document, over the cloud basemap, with what was
        /// measured and the stadium as geometry.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _tiles);

            // Ringed circles are no shape the renderer has of its own, so they are drawn
            // once and registered under the names the point markers ask for.
            style.Images.Add("stadium", PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 14, GeoColors.White, 3));
            style.Images.Add("measured", PointStyle.CreateSimpleCircleStyle(GeoColors.DarkGreen, 12, GeoColors.White, 3));
            style.AddGeometry(_stadium);
            style.AddGeometry(_measured, new GeometryStyle
            {
                FillColor = new GeoColor(96, GeoColors.Green),
                OutlineColor = GeoColors.DarkGreen,
                OutlineWidthInPixels = 2,
                LineColor = GeoColors.DarkGreen,
                LineWidthInPixels = 5,
            });
            return style;
        }

        private void ShowMeasurement()
        {
            SubLinePanel.Visibility = MeaSubLine.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Hint.Text = MeaSubLine.IsChecked == true
                ? "Click a trail, then change how far along the stretch starts and how long it runs."
                : MeaLength.IsChecked == true ? "Click a trail." : "Click a park.";

            // Measure the biggest thing in the file straight away, so the sample says
            // what it does before it is clicked - and says it about a feature large
            // enough for the number to mean something. The nearest feature to wherever
            // the map happens to open can be a strip of grass.
            _data.Open();
            var biggest = _data.GetAllFeatures(ReturningColumnsType.NoColumns)
                .OrderByDescending(candidate =>
                {
                    var box = candidate.GetBoundingBox();
                    return box.Width + box.Height;
                })
                .FirstOrDefault();
            _data.Close();

            if (biggest != null)
            {
                var bounds = biggest.GetBoundingBox();
                bounds.ScaleUp(60);
                Map.CenterPoint = bounds.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, bounds, Map.MapWidth, Map.MapHeight);
                Measure(biggest);
            }
        }

        /// <summary>
        /// The nearest feature to the click.
        /// </summary>
        private void Measure(PointShape where)
        {
            if (!_ready || where == null) return;

            _data.Open();
            var feature = _data
                .GetFeaturesNearestTo(where, GeographyUnit.Meter, 1, ReturningColumnsType.NoColumns)
                .FirstOrDefault();
            _data.Close();
            Measure(feature);
        }

        /// <summary>
        /// The one call that measures the chosen feature, and what it draws.
        /// </summary>
        private void Measure(Feature feature)
        {
            if (!_ready || feature == null) return;

            _measuring = feature;
            var shape = feature.GetShape();
            var drawn = new List<BaseShape>();

            if (MeaArea.IsChecked == true)
            {
                drawn.Add(shape);
                var area = ((AreaBaseShape)shape).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers);
                Result.Text = area.ToString("f3", CultureInfo.CurrentCulture) + " sq km";
            }
            else if (MeaLength.IsChecked == true)
            {
                drawn.Add(shape);
                var length = ((LineBaseShape)shape).GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer);
                Result.Text = length.ToString("f3", CultureInfo.CurrentCulture) + " km";
            }
            else if (MeaCenter.IsChecked == true)
            {
                var center = shape.GetCenterPoint();
                drawn.Add(shape);
                drawn.Add(center);
                Result.Text = center.X.ToString("f0", CultureInfo.CurrentCulture) + ", " +
                              center.Y.ToString("f0", CultureInfo.CurrentCulture);
            }
            else if (MeaShortest.IsChecked == true)
            {
                var line = shape.GetShortestLineTo(Stadium, GeographyUnit.Meter);
                drawn.Add(shape);
                drawn.Add(line);
                var length = line.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer);
                Result.Text = length.ToString("f3", CultureInfo.CurrentCulture) + " km to the stadium";
            }
            else
            {
                // A shapefile line can be several strands; the measurement runs along one.
                var line = shape as LineShape ?? ((MultilineShape)shape).Lines.First();
                var subLine = line.GetLineOnALine(
                    StartingPoint.FirstPoint,
                    Number(StartingOffset, 500),
                    Number(RunLength, 1000),
                    GeographyUnit.Meter,
                    DistanceUnit.Meter);

                var got = subLine?.GetLength(GeographyUnit.Meter, DistanceUnit.Meter) ?? 0;
                if (got > 0)
                {
                    drawn.Add(subLine);
                    Result.Text = got.ToString("f0", CultureInfo.CurrentCulture) + " m of it";
                }
                else
                {
                    // Asking for a stretch that starts past the end of the line is not an
                    // error; it just has nothing in it.
                    Result.Text = "Nothing there - this trail is "
                        + line.GetLength(GeographyUnit.Meter, DistanceUnit.Meter).ToString("f0", CultureInfo.CurrentCulture)
                        + " m long";
                }
            }

            // A measurement can draw a park, a trail, a center point or a line to the
            // stadium, so the same list goes to all three calls and each takes its own.
            _measured.UpdateAreas(drawn);
            _measured.UpdateLines(drawn);
            _measured.UpdatePoints(drawn, sizeInPixels: 16f, iconName: "measured");

            _ = Map.RefreshAsync();
        }

        /// <summary>A half-typed number is a number the user is still writing, not an error.</summary>
        private static double Number(System.Windows.Controls.TextBox box, double fallback) =>
            double.TryParse(box.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value) ? value : fallback;
    }
}
