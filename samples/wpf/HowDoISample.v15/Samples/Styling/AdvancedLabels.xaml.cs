using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Everything a label will do, one lesson per document: bend along its line,
    /// anchor where its own data says, yield to a higher priority, wrap and recase,
    /// keep its icon when its name cannot fit, and write CJK vertically. The data is
    /// built in code to give each lesson exactly the situation it is about; the
    /// document sits beside the map, editable while it runs.
    /// </summary>
    public partial class AdvancedLabels
    {
        private readonly DispatcherTimer _applyTimer;
        private string _lesson = "LessonCurved";
        private bool _initialized;

        public AdvancedLabels()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var tiles = new FeatureSourceVectorTileSource();
            tiles.FeatureSources.Add("lessons", BuildLessons());
            Map.Basemap = new GpuBasemap(Compose(Editor.Text, tiles));

            FrameLesson();
            _ = Map.RefreshAsync();
        }

        private void Lesson_Checked(object sender, RoutedEventArgs e)
        {
            if (Editor != null && sender is RadioButton radio && radio.Tag is string key)
            {
                _lesson = key;
                Editor.Text = (string)FindResource(key);
                if (_initialized)
                {
                    FrameLesson();
                }
            }
        }

        private void Editor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
                await Map.Basemap.SetStyleAsync(Compose(Editor.Text, null));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r') +
                              "   (the map is still drawing the last document that worked)";
            }
        }

        /// <summary>
        /// The document plus the one image it references: the dot the text-optional
        /// lesson keeps when a name yields. A point style drawn once becomes the icon.
        /// </summary>
        private static MapStyle Compose(string json, FeatureSourceVectorTileSource tiles)
        {
            var style = tiles == null ? new MapStyle(json) : new MapStyle(json, tiles);
            style.Images.Add("dot", PointStyle.CreateSimpleCircleStyle(GeoColor.FromHtml("#5E35B1"), 10, GeoColors.White, 2));
            return style;
        }

        /// <summary>Each lesson has its own patch of ground, framed to show it whole.</summary>
        private void FrameLesson()
        {
            var (center, scale) = _lesson switch
            {
                "LessonAnchors" => (new PointShape(-10770000, 3916000), 22000d),
                "LessonPriority" => (new PointShape(-10786000, 3904000), 40000d),
                "LessonWrap" => (new PointShape(-10770000, 3904000), 30000d),
                "LessonOptional" => (new PointShape(-10786000, 3892000), 20000d),
                "LessonCjk" => (new PointShape(-10770000, 3892000), 45000d),
                _ => (new PointShape(-10786000, 3916000), 45000d),
            };

            Map.CenterPoint = center;
            Map.CurrentScale = scale;
            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// The lessons' data, crafted rather than loaded: winding lines for the text
        /// to bend along, a grid of points whose positions match the anchors they
        /// name, a crowd for priority to thin, long names to wrap, close-set places
        /// for text-optional, and a north-running line with a Chinese name.
        /// </summary>
        private static InMemoryFeatureSource BuildLessons()
        {
            var features = new List<Feature>();

            void Add(BaseShape shape, string kind, string name, string rank = "", string anchor = "")
            {
                var feature = new Feature(shape);
                feature.ColumnValues["KIND"] = kind;
                feature.ColumnValues["NAME"] = name;
                feature.ColumnValues["RANK"] = rank;
                feature.ColumnValues["ANCHOR"] = anchor;
                features.Add(feature);
            }

            // Curved text: two parkways winding east-west.
            Add(Winding(-10786000, 3917500, 1500, 0), "curve", "Meandering Creek Parkway");
            Add(Winding(-10786000, 3914000, 1800, 1.2), "curve", "Old Mill Heritage Trail");

            // Anchors: each point sits in the grid where its own anchor points.
            var anchors = new[]
            {
                new[] { "top-left", "top", "top-right" },
                new[] { "left", "center", "right" },
                new[] { "bottom-left", "bottom", "bottom-right" },
            };
            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    var anchor = anchors[row][col];
                    Add(new PointShape(-10770000 + ((col - 1) * 1400), 3916000 - ((row - 1) * 1100)),
                        "anchor", anchor, anchor: anchor);
                }
            }

            // Priority: a spiral of ranked labels, far too many for the space.
            for (var i = 1; i <= 12; i++)
            {
                var angle = i * 2.399963;
                var radius = 350 + (i * 230);
                Add(new PointShape(-10786000 + (radius * Math.Cos(angle)), 3904000 + (radius * Math.Sin(angle))),
                    "rank", "Rank " + i, rank: i.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }

            // Wrapping: names longer than anyone wants on one line, and one that is not.
            Add(new PointShape(-10772000, 3904800), "wrap", "Frisco Heritage Botanical Conservatory and Community Arboretum");
            Add(new PointShape(-10768000, 3903600), "wrap", "Panther Creek Environmental Education Center");
            Add(new PointShape(-10770000, 3901800), "wrap", "City Hall");

            // text-optional: places set close enough that not every name can fit.
            var pois = new[]
            {
                "Coffee Roastery", "Corner Bakery", "Book Cellar", "Night Market", "Tea House",
                "Vinyl Shop", "Print Studio", "Flower Cart", "Cheese Shop",
            };
            for (var i = 0; i < pois.Length; i++)
            {
                var angle = i * 2.399963;
                var radius = 200 + (i * 160);
                Add(new PointShape(-10786000 + (radius * Math.Cos(angle)), 3892000 + (radius * Math.Sin(angle))),
                    "poi", pois[i]);
            }

            // CJK vertical: the line runs north, so the name writes downward.
            Add(WindingNorth(-10770000, 3892000), "cjk", "银杏河滨绿道");

            var columns = new[]
            {
                new FeatureSourceColumn("NAME"), new FeatureSourceColumn("KIND"),
                new FeatureSourceColumn("RANK"), new FeatureSourceColumn("ANCHOR"),
            };
            return new InMemoryFeatureSource(columns, features);
        }

        private static LineShape Winding(double centerX, double centerY, double amplitude, double phase)
        {
            var vertices = new Collection<Vertex>();
            for (var i = 0; i <= 48; i++)
            {
                var t = i / 48.0;
                vertices.Add(new Vertex(
                    centerX - 4200 + (t * 8400),
                    centerY + (amplitude * Math.Sin(phase + (t * Math.PI * 3)))));
            }

            return new LineShape(vertices);
        }

        private static LineShape WindingNorth(double centerX, double centerY)
        {
            var vertices = new Collection<Vertex>();
            for (var i = 0; i <= 48; i++)
            {
                var t = i / 48.0;
                vertices.Add(new Vertex(
                    centerX + (1200 * Math.Sin(t * Math.PI * 2.5)),
                    centerY - 4200 + (t * 8400)));
            }

            return new LineShape(vertices);
        }
    }
}
