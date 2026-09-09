using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Ask a layer which of its features stand in a given relation to a shape:
    /// containing it, crossing it, disjoint from it, intersecting, overlapping,
    /// touching, inside it, or within a distance of it. Each one is the same call on
    /// the same QueryTools under a different name, so they are one sample.
    /// </summary>
    public partial class FindFeaturesBySpatialRelation
    {
        // Zoning is thousands of static polygons - the tile pipeline's case. The query
        // shape and the matches are a handful of features that change on every click,
        // which is the geometry source's case. Neither is drawn from a layer, so the
        // query side is a FeatureSource: QueryTools takes one directly.
        private QueryTools _zoningQuery;
        private ShapeFileFeatureSource _zoning;

        // The same file read in its own state-plane coordinates, and the converter out
        // of them. Touching needs both - see the comment where it is used.
        private QueryTools _nativeQuery;
        private ShapeFileFeatureSource _native;
        private ProjectionConverter _outward;

        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private readonly FeatureSourceVectorTileSource _tiles = new FeatureSourceVectorTileSource();
        private readonly InMemoryGeometrySource _query = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _matches = new InMemoryGeometrySource();
        private readonly DispatcherTimer _applyTimer;
        private bool _ready;

        public FindFeaturesBySpatialRelation()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            Map.MapUnit = GeographyUnit.Meter;

            // Two readers over the same file: one the tile cutter owns, one this sample
            // queries. A tile source queries its source itself, and a second consumer
            // asking it questions mid-cut is a race.
            _tiles.FeatureSources.Add("zoning", new ShapeFileFeatureSource(SampleShared.Shapefile("Zoning.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });
            Map.Basemap = new GpuBasemap(ComposeStyle());

            _zoning = new ShapeFileFeatureSource(SampleShared.Shapefile("Zoning.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };
            _zoningQuery = new QueryTools(_zoning);

            _native = new ShapeFileFeatureSource(SampleShared.Shapefile("Zoning.shp"));
            _nativeQuery = new QueryTools(_native);
            _outward = new ProjectionConverter(2276, 3857);
            _outward.Open();

            Map.TrackOverlay.TrackEnded += (s, args) => Run(args.TrackShape);
            Map.MapClick += (s, args) =>
            {
                if (Map.TrackOverlay.TrackMode == TrackMode.None)
                {
                    Run(args.WorldLocation);
                }
            };

            _ready = true;
            ShowRelation();
            await Map.RefreshAsync();
        }

        /// <summary>
        /// The zoning under the document, over the cloud basemap, with the query shape
        /// and its matches as geometry.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
            style.AddStyle(Editor.Text, _tiles);

            // A ringed circle is no shape the renderer has of its own, so it is drawn
            // once and registered under the name the point marker asks for.
            style.Images.Add("pin", PointStyle.CreateSimpleCircleStyle(GeoColors.Black, 8, GeoColors.White, 2));
            style.AddGeometry(_matches, new GeometryStyle { FillColor = new GeoColor(90, GeoColors.MidnightBlue), OutlineColor = GeoColors.MidnightBlue, OutlineWidthInPixels = 2 });
            style.AddGeometry(_query, new GeometryStyle { FillColor = GeoColors.Transparent, OutlineColor = GeoColors.Black, OutlineWidthInPixels = 2, LineColor = GeoColors.Black, LineWidthInPixels = 2 });
            return style;
        }

        private void Relation_Checked(object sender, RoutedEventArgs e) => ShowRelation();

        private void Radius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Run(_seed, fromSeed: true);

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
                await Map.Basemap.SetStyleAsync(ComposeStyle());
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
        /// Frames the chosen relation where it has something to show, seeds a query
        /// shape, and puts the map in the drawing mode that relation reads.
        /// </summary>
        private void ShowRelation()
        {
            if (!_ready) return;

            var pointQuery = RelContaining.IsChecked == true || RelWithinDistance.IsChecked == true;
            var lineQuery = RelCrossing.IsChecked == true || RelTouching.IsChecked == true;
            RadiusPanel.Visibility = RelWithinDistance.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Hint.Text = pointQuery ? "Click the map to query somewhere else."
                : RelTouching.IsChecked == true
                    ? "Touching is exact - the seed is one parcel's own edge. A line you draw yourself will almost always match nothing."
                : lineQuery ? "Draw a line to query somewhere else."
                : "Draw a polygon to query somewhere else.";

            Map.TrackOverlay.TrackMode = pointQuery ? TrackMode.None
                : lineQuery ? TrackMode.Line
                : TrackMode.Polygon;

            var (center, scale) = Framing();
            Map.CenterPoint = center;
            Map.CurrentScale = scale;

            Run(Seed(), fromSeed: true);
        }

        private (PointShape Center, double Scale) Framing()
        {
            if (RelCrossing.IsChecked == true) return (new PointShape(-10776670, 3914800), 27870);
            if (RelDisjoint.IsChecked == true) return (new PointShape(-10778020, 3914693), 32900);
            if (RelIntersecting.IsChecked == true) return (new PointShape(-10777924, 3914325), 11220);
            if (RelOverlapping.IsChecked == true) return (new PointShape(-10777891, 3914206), 26200);
            if (RelTouching.IsChecked == true) return (new PointShape(-10776520, 3919250), 18060);
            if (RelWithin.IsChecked == true) return (new PointShape(-10778569, 3914000), 33100);
            return (new PointShape(-10779430, 3914970), 18060);
        }

        /// <summary>
        /// A shape that gives the chosen relation something to match, so the sample
        /// says something before it is clicked.
        /// </summary>
        private BaseShape Seed()
        {
            if (RelCrossing.IsChecked == true)
                return new LineShape("LINESTRING(-10774628 3914024,-10776902 3915582,-10778030 3914368,-10778708 3914445)");
            if (RelDisjoint.IsChecked == true)
                return new PolygonShape("POLYGON((-10780418 3915973,-10780428 3913422,-10775737 3913413,-10775612 3915954,-10780418 3915973))");
            if (RelIntersecting.IsChecked == true)
                return new PolygonShape("POLYGON((-10778718 3914865,-10778746 3913709,-10777103 3913766,-10777179 3914942,-10778718 3914865))");
            if (RelOverlapping.IsChecked == true)
                return new PolygonShape("POLYGON((-10779549 3915352,-10777495 3915859,-10776214 3914827,-10776081 3913384,-10777906 3912553,-10779702 3914110,-10779549 3915352))");
            if (RelTouching.IsChecked == true)
                return TouchingSeed();
            if (RelWithin.IsChecked == true)
                return new PolygonShape("POLYGON((-10779148 3916088,-10779960 3913862,-10777189 3911913,-10777179 3915754,-10779148 3916088))");
            return new PointShape(-10779430, 3914970);
        }

        /// <summary>
        /// One edge of one parcel, copied vertex for vertex out of the file - and read
        /// in the file's own state-plane coordinates, not the map's. A line lying along
        /// a polygon's boundary has nothing of its interior inside, which is exactly
        /// what touching asks for, but only while the coordinates still match bit for
        /// bit. See <see cref="RunTouching"/>.
        /// </summary>
        private BaseShape TouchingSeed()
        {
            _native.Open();
            var first = (MultipolygonShape)_native
                .GetAllFeatures(ReturningColumnsType.NoColumns).First().GetShape();
            _native.Close();

            var vertices = first.Polygons.First().OuterRing.Vertices;
            return new LineShape(new Collection<Vertex> { vertices[0], vertices[1] });
        }

        /// <summary>
        /// Touching, asked in the plane the data is stored in.
        /// <para>
        /// Every other relation on this list tolerates a rounded coordinate. Touching
        /// does not - it means the boundaries coincide and the interiors share nothing,
        /// which is a question about exact equality. Converting these parcels out to
        /// mercator and a query back into state plane moves every vertex by a fraction
        /// of a foot, and that is enough: the same edge that touches three parcels in
        /// the file touches none through the converter. So this one relation queries the
        /// unprojected layer and converts only what it draws.
        /// </para>
        /// </summary>
        private void RunTouching(BaseShape shape, bool alreadyNative)
        {
            var asked = alreadyNative ? shape : _outward.ConvertToInternalProjection(shape);

            _native.Open();
            var found = _nativeQuery.GetFeaturesTouching(asked, ReturningColumnsType.NoColumns).ToList();
            _native.Close();

            Paint(_outward.ConvertToExternalProjection(asked),
                found.Select(feature => _outward.ConvertToExternalProjection(feature)).ToList());

            Result.Text = found.Count + " features touching the query shape";
        }

        private BaseShape _seed;

        /// <summary>
        /// The query itself: one call, chosen by the relation, over the same shape.
        /// </summary>
        /// <param name="fromSeed">
        /// True when the shape came from <see cref="Seed"/> rather than from the map,
        /// which for touching means it is still in the file's own coordinates.
        /// </param>
        private void Run(BaseShape shape, bool fromSeed = false)
        {
            if (!_ready || shape == null) return;

            _seed = shape;
            if (RelTouching.IsChecked == true)
            {
                RunTouching(shape, fromSeed);
                Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                return;
            }

            _zoning.Open();
            IEnumerable<Feature> found;
            string verb;
            if (RelContaining.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesContaining(shape, ReturningColumnsType.NoColumns);
                verb = "containing";
            }
            else if (RelCrossing.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesCrossing(shape, ReturningColumnsType.NoColumns);
                verb = "crossing";
            }
            else if (RelDisjoint.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesDisjointed(shape, ReturningColumnsType.NoColumns);
                verb = "disjoint from";
            }
            else if (RelIntersecting.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesIntersecting(shape, ReturningColumnsType.NoColumns);
                verb = "intersecting";
            }
            else if (RelOverlapping.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesOverlapping(shape, ReturningColumnsType.NoColumns);
                verb = "overlapping";
            }
            else if (RelWithin.IsChecked == true)
            {
                found = _zoningQuery.GetFeaturesWithin(shape, ReturningColumnsType.NoColumns);
                verb = "within";
            }
            else
            {
                found = _zoningQuery.GetFeaturesWithinDistanceOf(
                    shape, GeographyUnit.Meter, DistanceUnit.Meter, Radius.Value, ReturningColumnsType.NoColumns);
                verb = "within " + (int)Radius.Value + " m of";
            }

            _zoning.Close();

            var matches = found.ToList();
            Paint(shape, matches);

            Result.Text = matches.Count + " features " + verb + " the query shape";
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        /// <summary>
        /// Hands both results over. The query shape is whatever the relation reads - a
        /// point, a line or a polygon - so it goes to all three calls and each takes
        /// the one it draws.
        /// </summary>
        private void Paint(BaseShape query, IEnumerable<Feature> matches)
        {
            _matches.UpdateAreas(matches);

            var asked = new[] { query };
            _query.UpdateAreas(asked);
            _query.UpdateLines(asked);
            _query.UpdatePoints(asked, sizeInPixels: 12f, iconName: "pin");

            _ = Map.RefreshAsync();
        }
    }
}
