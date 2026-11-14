using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Find the features that are containing, crossing, disjoint from, within, intersecting, overlapping, touching or equal to a shape, or within a distance of it.
    /// </summary>
    public partial class FindFeaturesBySpatialRelation : IDisposable
    {
        private bool _initialized;
        private string _relation = "intersecting";
        private BaseShape _queryShape;
        private InMemoryFeatureLayer _zoningLayer;
        private InMemoryFeatureLayer _platsLayer;
        private InMemoryFeatureLayer _queryLayer;
        private InMemoryFeatureLayer _highlightLayer;
        private LayerOverlay _dataOverlay;
        private LayerOverlay _queryOverlay;

        public FindFeaturesBySpatialRelation()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // The data is projected once into memory, so a query shape copied from it matches its
            // boundaries exactly - touching and equal compare coordinates without tolerance.
            _zoningLayer = new InMemoryFeatureLayer();
            _zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LoadProjectedFeatures("Zoning.shp", _zoningLayer);

            _platsLayer = new InMemoryFeatureLayer { IsVisible = false };
            _platsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _platsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LoadProjectedFeatures("Subdivisions.shp", _platsLayer);

            _queryLayer = new InMemoryFeatureLayer();
            _queryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
            _queryLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.LightRed, 3, false);
            _queryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.LightRed, 12, GeoColors.White, 2);
            _queryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _highlightLayer = new InMemoryFeatureLayer();
            _highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
            _highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _dataOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _dataOverlay.Layers.Add(_zoningLayer);
            _dataOverlay.Layers.Add(_platsLayer);
            Map.Overlays.Add(_dataOverlay);

            _queryOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _queryOverlay.Layers.Add(_highlightLayer);
            _queryOverlay.Layers.Add(_queryLayer);
            Map.Overlays.Add(_queryOverlay);

            Map.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            _ = ShowRelationAsync();
        }

        private static void LoadProjectedFeatures(string shapefileName, InMemoryFeatureLayer layer)
        {
            var featureSource = new ShapeFileFeatureSource(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", shapefileName));
            var projectionConverter = new ProjectionConverter(2276, 3857);

            featureSource.Open();
            projectionConverter.Open();
            foreach (var feature in featureSource.GetAllFeatures(ReturningColumnsType.NoColumns))
            {
                layer.InternalFeatures.Add(projectionConverter.ConvertToExternalProjection(feature));
            }
            projectionConverter.Close();
            featureSource.Close();
        }

        private void Relation_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _relation = (string)((RadioButton)sender).Tag;
            _ = ShowRelationAsync();
        }

        private async Task ShowRelationAsync()
        {
            BaseShape seed;
            switch (_relation)
            {
                case "containing":
                    Map.TrackOverlay.TrackMode = TrackMode.None;
                    Map.CenterPoint = new PointShape(-10779430, 3914970);
                    Map.CurrentScale = 18060;
                    seed = new PointShape(-10779425, 3914970);
                    Status.Text = "Click the map to query another point.";
                    break;
                case "crossing":
                    Map.TrackOverlay.TrackMode = TrackMode.Line;
                    Map.CenterPoint = new PointShape(-10776670, 3914800);
                    Map.CurrentScale = 27870;
                    seed = new LineShape("LINESTRING(-10774628 3914024,-10776902 3915582,-10778030 3914368,-10778708 3914445)");
                    Status.Text = "Click to start drawing a line, double-click to finish.";
                    break;
                case "disjoint":
                    Map.TrackOverlay.TrackMode = TrackMode.Polygon;
                    Map.CenterPoint = new PointShape(-10778020, 3914693);
                    Map.CurrentScale = 32900;
                    seed = new PolygonShape("POLYGON((-10780418 3915973,-10780428 3913422,-10775737 3913413,-10775612 3915954,-10780418 3915973))");
                    Status.Text = "Click to start drawing a polygon, double-click to finish.";
                    break;
                case "distance":
                    Map.TrackOverlay.TrackMode = TrackMode.None;
                    Map.CenterPoint = new PointShape(-10779430, 3914970);
                    Map.CurrentScale = 18060;
                    seed = new PointShape(-10779430, 3914970);
                    Status.Text = "Click the map to query another point.";
                    break;
                case "within":
                    Map.TrackOverlay.TrackMode = TrackMode.Polygon;
                    Map.CenterPoint = new PointShape(-10778569, 3914000);
                    Map.CurrentScale = 33100;
                    seed = new PolygonShape("POLYGON((-10779148 3916088,-10779960 3913862,-10777189 3911913,-10777179 3915754,-10779148 3916088))");
                    Status.Text = "Click to start drawing a polygon, double-click to finish.";
                    break;
                case "overlapping":
                    Map.TrackOverlay.TrackMode = TrackMode.Polygon;
                    Map.CenterPoint = new PointShape(-10777891, 3914206);
                    Map.CurrentScale = 26200;
                    seed = new PolygonShape("POLYGON((-10779549 3915352,-10777495 3915859,-10776214 3914827,-10776081 3913384,-10777906 3912553,-10779702 3914110,-10779549 3915352))");
                    Status.Text = "Click to start drawing a polygon, double-click to finish.";
                    break;
                case "touching":
                    Map.TrackOverlay.TrackMode = TrackMode.None;
                    Map.CenterPoint = new PointShape(-10776520, 3919250);
                    Map.CurrentScale = 18060;
                    seed = TouchingSeed();
                    Status.Text = "Touching needs boundaries that match exactly, which a drawn shape never does, so the query shape is built from one zoning area's own vertices.";
                    break;
                case "equal":
                    Map.TrackOverlay.TrackMode = TrackMode.None;
                    Map.CenterPoint = new PointShape(-10779350, 3917407);
                    Map.CurrentScale = 6000;
                    seed = new PointShape(-10779350, 3917407);
                    Status.Text = "Click a plat. The smallest plat under the click is the query shape; a lot platted again keeps its exact boundary under a new record.";
                    break;
                default:
                    Map.TrackOverlay.TrackMode = TrackMode.Polygon;
                    Map.CenterPoint = new PointShape(-10777924, 3914325);
                    Map.CurrentScale = 11220;
                    seed = new PolygonShape("POLYGON((-10778718 3914865,-10778746 3913709,-10777103 3913766,-10777179 3914942,-10778718 3914865))");
                    Status.Text = "Click to start drawing a polygon, double-click to finish.";
                    break;
            }

            DistancePanel.Visibility = _relation == "distance" ? Visibility.Visible : Visibility.Collapsed;
            _zoningLayer.IsVisible = _relation != "equal";
            _platsLayer.IsVisible = _relation == "equal";

            await RunQueryAsync(seed);
            try
            {
                await Map.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private BaseShape TouchingSeed()
        {
            var firstZone = (MultipolygonShape)_zoningLayer.InternalFeatures[0].GetShape();
            var vertices = firstZone.Polygons[0].OuterRing.Vertices;

            return new MultipolygonShape(new Collection<PolygonShape> { new PolygonShape(new RingShape(new Collection<Vertex> { vertices[0], vertices[1], vertices[2] })) });
        }

        private async Task RunQueryAsync(BaseShape shape)
        {
            _queryShape = shape;
            var queriedLayer = _zoningLayer;
            if (_relation == "equal")
            {
                shape = SmallestPlatAt((PointShape)shape);
                queriedLayer = _platsLayer;
            }

            _queryLayer.InternalFeatures.Clear();
            _highlightLayer.InternalFeatures.Clear();
            if (shape == null)
            {
                Result.Text = "No plat under the click.";
            }
            else
            {
                queriedLayer.Open();
                var features = Query(queriedLayer.QueryTools, shape);
                queriedLayer.Close();

                _queryLayer.InternalFeatures.Add(new Feature(shape));
                foreach (var feature in features)
                {
                    _highlightLayer.InternalFeatures.Add(feature);
                }
                Result.Text = $"Features found: {features.Count}";
            }

            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            try
            {
                await _queryOverlay.RefreshAsync();
                await Map.TrackOverlay.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private Collection<Feature> Query(QueryTools queryTools, BaseShape shape)
        {
            switch (_relation)
            {
                case "containing":
                    return queryTools.GetFeaturesContaining(shape, ReturningColumnsType.NoColumns);
                case "crossing":
                    return queryTools.GetFeaturesCrossing(shape, ReturningColumnsType.NoColumns);
                case "disjoint":
                    return queryTools.GetFeaturesDisjointed(shape, ReturningColumnsType.NoColumns);
                case "distance":
                    return queryTools.GetFeaturesWithinDistanceOf(shape, GeographyUnit.Meter, DistanceUnit.Meter, SearchRadius.Value, ReturningColumnsType.NoColumns);
                case "within":
                    return queryTools.GetFeaturesWithin(shape, ReturningColumnsType.NoColumns);
                case "overlapping":
                    return queryTools.GetFeaturesOverlapping(shape, ReturningColumnsType.NoColumns);
                case "touching":
                    return queryTools.GetFeaturesTouching(shape, ReturningColumnsType.NoColumns);
                case "equal":
                    return queryTools.GetFeaturesTopologicalEqual(shape, ReturningColumnsType.NoColumns);
                default:
                    return queryTools.GetFeaturesIntersecting(shape, ReturningColumnsType.NoColumns);
            }
        }

        private BaseShape SmallestPlatAt(PointShape point)
        {
            _platsLayer.Open();
            var plat = _platsLayer.QueryTools.GetFeaturesContaining(point, ReturningColumnsType.NoColumns)
                .OrderBy(feature => ((AreaBaseShape)feature.GetShape()).GetArea(GeographyUnit.Meter, AreaUnit.SquareMeters))
                .FirstOrDefault();

            return plat?.GetShape();
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (_relation == "containing" || _relation == "distance" || _relation == "equal")
            {
                _ = RunQueryAsync(e.WorldLocation);
            }
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            _ = RunQueryAsync(e.TrackShape);
        }

        private void SearchRadius_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_initialized && _relation == "distance")
            {
                _ = RunQueryAsync(_queryShape);
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
