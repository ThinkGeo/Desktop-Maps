using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Find the features that stand in a spatial relation to a shape - containing, crossing, disjoint, intersecting, overlapping, touching, within, within a distance, or equal.
    /// </summary>
    public partial class FindFeaturesBySpatialRelation
    {
        // Zoning is thousands of static polygons - the tile pipeline's case. The query
        // shape and the matches are a handful of features that change on every click,
        // which is the geometry source's case. Neither is drawn from a layer, so the
        // query side is a FeatureSource: QueryTools takes one directly.
        private QueryTools _zoningQuery;
        private FeatureSource _zoning;

        // The same file read in its own state-plane coordinates, and the converter out
        // of them. Touching needs both - see the comment where it is used.
        private QueryTools _nativeQuery;
        private ShapeFileFeatureSource _native;
        private ProjectionConverter _outward;

        // Frisco's subdivision plats, also in state-plane coordinates: a lot platted again
        // keeps its boundary under a new record, which is what equal finds.
        private QueryTools _platsQuery;
        private ShapeFileFeatureSource _plats;

        private readonly DynamicSource _query = new DynamicSource();
        private readonly DynamicSource _matches = new DynamicSource();
        private bool _ready;

        public FindFeaturesBySpatialRelation()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;

            // The tile cutter reads the source it is given on its own threads, so a source
            // that is not safe to read from two threads at once is queried through a copy.
            var zoning = new ShapeFileFeatureSource(SampleShared.Shapefile("Zoning.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };
            var tiles = new FeatureSourceVectorTileSource();
            tiles.FeatureSources.Add("zoning", zoning);
            _zoning = zoning.ConcurrentAccess == ConcurrentAccess.Safe ? zoning : zoning.CloneDeep();
            _zoningQuery = new QueryTools(_zoning);

            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.AddStyle((string)Resources["StyleJson"], tiles);

            // A ringed circle is no shape the renderer has of its own, so it is drawn
            // once and registered under the name the point marker asks for.
            style.Images.Add("pin", PointStyle.CreateSimpleCircleStyle(GeoColors.Black, 8, GeoColors.White, 2));
            style.AddDynamic(_matches);
            style.AddDynamic(_query);
            Map.Basemap = new GpuBasemap(style);

            _native = new ShapeFileFeatureSource(SampleShared.Shapefile("Zoning.shp"));
            _nativeQuery = new QueryTools(_native);
            _plats = new ShapeFileFeatureSource(SampleShared.Shapefile("Subdivisions.shp"));
            _platsQuery = new QueryTools(_plats);
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

        private void Relation_Checked(object sender, RoutedEventArgs e) => ShowRelation();

        private void Radius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Run(_seed, fromSeed: true);

        /// <summary>
        /// Frames the chosen relation where it has something to show, seeds a query
        /// shape, and puts the map in the drawing mode that relation reads.
        /// </summary>
        private void ShowRelation()
        {
            if (!_ready) return;

            var pointQuery = RelContaining.IsChecked == true || RelWithinDistance.IsChecked == true || RelEqual.IsChecked == true;
            var lineQuery = RelCrossing.IsChecked == true || RelTouching.IsChecked == true;
            RadiusPanel.Visibility = RelWithinDistance.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
            Hint.Text = RelEqual.IsChecked == true
                    ? "Equal asks Frisco's subdivision plats, where a lot platted again keeps its exact boundary under a new record. Click to query the smallest plat there."
                : pointQuery ? "Click the map to query somewhere else."
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
            if (RelEqual.IsChecked == true) return (new PointShape(-10779350, 3917407), 6000);
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
            if (RelEqual.IsChecked == true)
                return new PointShape(-10779350, 3917407);
            return new PointShape(-10779430, 3914970);
        }

        /// <summary>
        /// One edge of one parcel, copied vertex for vertex out of the file - and read
        /// in the file's own state-plane coordinates, not the map's. A line lying along
        /// a polygon's boundary has nothing of its interior inside, which is exactly
        /// what touching asks for, but only while the coordinates still match bit for
        /// bit. See <see cref="FindTouchingFeatures"/>.
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
        /// Touching means the boundaries coincide exactly, and coordinates converted between
        /// projections never quite do, so a source with a ProjectionConverter answers a
        /// touching query with no features by design. This one relation therefore asks the
        /// data in its own state-plane coordinates and converts only what it draws.
        /// </para>
        /// </summary>
        private void FindTouchingFeatures(BaseShape shape, bool alreadyNative)
        {
            var asked = alreadyNative ? shape : _outward.ConvertToInternalProjection(shape);

            _native.Open();
            var found = _nativeQuery.GetFeaturesTouching(asked, ReturningColumnsType.NoColumns).ToList();
            _native.Close();

            Paint(_outward.ConvertToExternalProjection(asked),
                found.Select(feature => _outward.ConvertToExternalProjection(feature)).ToList());

            Result.Text = found.Count + " features touching the query shape";
        }

        /// <summary>
        /// Equal, asked of the plats in the plane they are stored in. Equal compares the
        /// geometry exactly, so like touching it is asked without a ProjectionConverter.
        /// The query is the smallest plat under the clicked point, so it is equal at
        /// least to itself; any other match is the same lot recorded again.
        /// </summary>
        private void FindEqualFeatures(PointShape clicked)
        {
            var asked = _outward.ConvertToInternalProjection(clicked);

            _plats.Open();
            var plat = _platsQuery.GetFeaturesContaining(asked, ReturningColumnsType.NoColumns)
                .OrderBy(feature => ((AreaBaseShape)feature.GetShape()).GetArea(GeographyUnit.Feet, AreaUnit.SquareFeet))
                .FirstOrDefault();
            var found = plat == null
                ? new List<Feature>()
                : _platsQuery.GetFeaturesTopologicalEqual(plat.GetShape(), ReturningColumnsType.NoColumns).ToList();
            _plats.Close();

            Paint(_outward.ConvertToExternalProjection(plat?.GetShape() ?? asked),
                found.Select(feature => _outward.ConvertToExternalProjection(feature)).ToList());

            Result.Text = plat == null
                ? "No plat under the click"
                : found.Count + " plats equal to the query shape";
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
                FindTouchingFeatures(shape, fromSeed);
                Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                return;
            }

            if (RelEqual.IsChecked == true)
            {
                FindEqualFeatures((PointShape)shape);
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
        /// point, a line or a polygon - so it is added by its kind: an outline with no
        /// fill, a black line, or the pin.
        /// </summary>
        private void Paint(BaseShape query, IEnumerable<Feature> matches)
        {
            _matches.Clear();
            var matchIndex = 0;
            foreach (var match in matches)
            {
                _matches.SetArea("match-" + matchIndex++, match, new GeoColor(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue, 2f);
            }

            _query.Clear();
            var asked = new Feature(query);
            switch (query)
            {
                case AreaBaseShape _:
                    _query.SetArea("query", asked, GeoColors.Transparent, GeoColors.Black, 2f);
                    break;
                case LineBaseShape _:
                    _query.SetLine("query", asked, GeoColors.Black, 2f);
                    break;
                default:
                    _query.SetPoint("query", asked, sizeInPixel: 12f, iconName: "pin");
                    break;
            }

            _ = Map.RefreshAsync();
        }
    }
}
