using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Check points, lines and polygons against topology rules with the TopologyValidator.
    /// </summary>
    public partial class ValidateTopology : IDisposable
    {
        private bool _initialized;
        private InMemoryFeatureLayer _validatedFeaturesLayer;
        private InMemoryFeatureLayer _filterFeaturesLayer;
        private InMemoryFeatureLayer _resultFeaturesLayer;
        private LayerOverlay _featuresOverlay;

        public ValidateTopology()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            _validatedFeaturesLayer = new InMemoryFeatureLayer();
            _validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.Green);
            _validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Green), GeoColors.Green);
            _validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 3, false);
            _validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _filterFeaturesLayer = new InMemoryFeatureLayer();
            _filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 12, GeoColors.Blue);
            _filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Blue), GeoColors.Blue);
            _filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 3, false);
            _filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _resultFeaturesLayer = new InMemoryFeatureLayer();
            _resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 12, GeoColors.Red);
            _resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Red), GeoColors.Red);
            _resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 3, false);
            _resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _featuresOverlay = new LayerOverlay();
            _featuresOverlay.Layers.Add(_filterFeaturesLayer);
            _featuresOverlay.Layers.Add(_validatedFeaturesLayer);
            _featuresOverlay.Layers.Add(_resultFeaturesLayer);
            Map.Overlays.Add(_featuresOverlay);

            RdoCheckIfPointsAreTouchingLines.IsChecked = true;
        }

        private void CheckIfPointsAreTouchingLines(object sender, RoutedEventArgs e)
        {
            var coveredPointFeature1 = new Feature("POINT(0 0)");
            var coveredPointFeature2 = new Feature("POINT(50 0)");
            var uncoveredPointFeature = new Feature("POINT(150 0)");
            var lineFeature = new Feature("LINESTRING(0 0,100 0)");

            var points = new Collection<Feature> { coveredPointFeature1, coveredPointFeature2, uncoveredPointFeature };
            var lines = new Collection<Feature> { lineFeature };
            var result = TopologyValidator.PointsMustTouchLines(points, lines);

            ShowFeatures(points, result.InvalidFeatures, lines,
                "Features being validated against are shown in blue. \n\nPoints touching lines are shown in green. \n\nPoints not touching lines are shown in red.");
        }

        private void CheckIfPointsAreTouchingLineEndpoints(object sender, RoutedEventArgs e)
        {
            var pointFeature1 = new Feature("POINT(0 0)");
            var pointFeature2 = new Feature("POINT(50 0)");
            var pointFeatureOnEndpoint = new Feature("POINT(100 0)");
            var lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");

            var points = new Collection<Feature> { pointFeature1, pointFeature2, pointFeatureOnEndpoint };
            var lines = new Collection<Feature> { lineFeature };
            var result = TopologyValidator.PointsMustTouchLineEndpoints(points, lines);

            ShowFeatures(points, result.InvalidFeatures, lines,
                "Features being validated against are shown in blue. \n\nPoints touching line endpoints are shown in green. \n\nPoints not touching line endpoints are shown in red.");
        }

        private void CheckIfPointsAreTouchingPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            var pointFeature1 = new Feature("POINT(150 0)");
            var pointFeature2 = new Feature("POINT(50 50)");
            var pointFeatureOnBoundary = new Feature("POINT(0 0)");
            var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            var points = new Collection<Feature> { pointFeature1, pointFeature2, pointFeatureOnBoundary };
            var polygons = new Collection<Feature> { polygonFeature };
            var result = TopologyValidator.PointsMustTouchPolygonBoundaries(points, polygons);

            ShowFeatures(points, result.InvalidFeatures, polygons,
                "Features being validated against are shown in blue. \n\nPoints touching polygon boundaries are shown in green. \n\nPoints not touching polygon boundaries are shown in red.");
        }

        private void CheckIfPointsAreWithinPolygons(object sender, RoutedEventArgs e)
        {
            var pointFeature1 = new Feature("POINT(150 0)");
            var pointFeature2 = new Feature("POINT(0 0)");
            var pointFeatureInsidePolygon = new Feature("POINT(50 50)");
            var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            var points = new Collection<Feature> { pointFeature1, pointFeature2, pointFeatureInsidePolygon };
            var polygons = new Collection<Feature> { polygonFeature };
            var result = TopologyValidator.PointsMustBeWithinPolygons(points, polygons);

            ShowFeatures(points, result.InvalidFeatures, polygons,
                "Features being validated against are shown in blue. \n\nPoints within polygons are shown in green. \n\nPoints not within polygons are shown in red.");
        }

        private void CheckLineEndpointsMustTouchPoints(object sender, RoutedEventArgs e)
        {
            var lineFeature = new Feature("LINESTRING(0 0,100 0,100 50)");
            var pointOnEndpointFeature = new Feature("POINT(0 0)");

            var lines = new Collection<Feature> { lineFeature };
            var points = new Collection<Feature> { pointOnEndpointFeature };
            var result = TopologyValidator.LineEndPointsMustTouchPoints(lines, points);

            ShowFeatures(lines, result.InvalidFeatures, points,
                "Features being validated against are shown in blue. \n\nLine endpoints touching points are shown in green. \n\nInvalid endpoints are shown in red.");
        }

        private void CheckLinesMustOverlapPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            var lineFeature = new Feature("LINESTRING(-50 0,150 0)");
            var lineOnBoundaryFeature = new Feature("LINESTRING(0 100,100 100)");
            var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            var lines = new Collection<Feature> { lineFeature, lineOnBoundaryFeature };
            var polygons = new Collection<Feature> { polygonFeature };
            var result = TopologyValidator.LinesMustOverlapPolygonBoundaries(lines, polygons);

            ShowFeatures(lines, result.InvalidFeatures, polygons,
                "Features being validated against are shown in blue. \n\nLine segments overlapping polygon boundaries are shown in green. \n\nInvalid line segments are shown in red.");
        }

        private void CheckLinesMustBeCoveredByLines(object sender, RoutedEventArgs e)
        {
            var lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            var coveringLineFeature = new Feature("LINESTRING(0 -50,50 0,100 0,100 150)");

            var coveringLines = new Collection<Feature> { coveringLineFeature };
            var coveredLines = new Collection<Feature> { lineFeature };
            var result = TopologyValidator.LinesMustBeCoveredByLines(coveringLines, coveredLines);

            ShowFeatures(coveredLines, result.InvalidFeatures, coveringLines,
                "Features being validated against are shown in blue. \n\nLine segments covered by lines are shown in green. \n\nInvalid line segments are shown in red.");
        }

        private void CheckLinesMustBeSinglePart(object sender, RoutedEventArgs e)
        {
            var singleLineFeature = new Feature("MULTILINESTRING((0 -50,100 -50,100 -100,0 -100))");
            var multiLineFeature = new Feature("MULTILINESTRING((0 0,100 0),(100 50,0 50))");

            var lines = new Collection<Feature> { singleLineFeature, multiLineFeature };
            var result = TopologyValidator.LinesMustBeSinglePart(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines made of single segments are shown in green. \n\nLines with disjoint segments are shown in red.");
        }

        private void CheckLinesMustFormClosedPolygon(object sender, RoutedEventArgs e)
        {
            var lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100,20 100)");
            var lineFeature2 = new Feature("LINESTRING(0 0,-50 0,-50 100,0 100)");

            var lines = new Collection<Feature> { lineFeature1, lineFeature2 };
            var result = TopologyValidator.LinesMustFormClosedPolygon(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nLine endpoints that do not form a closed polygon are shown in red.");
        }

        private void CheckLinesMustNotHavePseudonodes(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature>
            {
                new Feature("LINESTRING(0 0,50 0,50 50,0 0)"),
                new Feature("LINESTRING(-50 0,-50 50)"),
                new Feature("LINESTRING(-100 0,-50 50)"),
                new Feature("LINESTRING(-50 -50,-50 -100)"),
                new Feature("LINESTRING(-100 -50,-50 -100)"),
                new Feature("LINESTRING(-50 -100,0 -100)")
            };
            var result = TopologyValidator.LinesMustNotHavePseudonodes(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nPseudonodes are shown in red.");
        }

        private void CheckLinesMustNotIntersect(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature>
            {
                new Feature("LINESTRING(0 0,100 0,100 100)"),
                new Feature("LINESTRING(0 -50,30 0,60 0,100 50)"),
                new Feature("LINESTRING(20 50,20 -50)")
            };
            var result = TopologyValidator.LinesMustNotIntersect(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nIntersections are shown in red.");
        }

        private void CheckLinesMustNotSelfIntersectOrTouch(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature>
            {
                new Feature("LINESTRING(0 0,100 0,100 100)"),
                new Feature("LINESTRING(0 -50,30 0,60 0,100 50)"),
                new Feature("LINESTRING(20 50,20 -50)")
            };
            var result = TopologyValidator.LinesMustNotSelfIntersectOrTouch(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nIntersecting points and overlapping segments are shown in red.");
        }

        private void CheckLinesMustNotOverlap(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature>
            {
                new Feature("LINESTRING(0 0,100 0,100 100)"),
                new Feature("LINESTRING(0 -50,30 0,60 0,100 50)"),
                new Feature("LINESTRING(20 50,20 -50)")
            };
            var result = TopologyValidator.LinesMustNotOverlap(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.");
        }

        private void CheckLinesMustNotOverlapLines(object sender, RoutedEventArgs e)
        {
            var overlappingLineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            var overlappedLineFeature = new Feature("LINESTRING(150 0,100 30,100 60,150 100)");

            var coveringLines = new Collection<Feature> { overlappingLineFeature };
            var coveredLines = new Collection<Feature> { overlappedLineFeature };
            var result = TopologyValidator.LinesMustNotOverlapLines(coveringLines, coveredLines);

            ShowFeatures(coveredLines, result.InvalidFeatures, coveringLines,
                "Features being validated against are shown in blue. \n\nLines being validated are shown in green. \n\nOverlapping line segments are shown in red.");
        }

        private void CheckLinesMustNotSelfIntersect(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature> { new Feature("LINESTRING(0 0,100 0,100 100,50 100,50 -50)") };
            var result = TopologyValidator.LinesMustNotSelfIntersect(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nSelf-intersections are shown in red.");
        }

        private void CheckLinesMustNotSelfOverlap(object sender, RoutedEventArgs e)
        {
            var lines = new Collection<Feature> { new Feature("LINESTRING(0 0,100 0,100 100,0 100,20 0,40 0,40 -50)") };
            var result = TopologyValidator.LinesMustNotSelfOverlap(lines);

            ShowFeatures(lines, result.InvalidFeatures, null,
                "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.");
        }

        private void CheckIfPolygonBoundariesOverlapPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            var coveringPolygons = new Collection<Feature> { new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))") };
            var coveredPolygons = new Collection<Feature> { new Feature("POLYGON((0 0,50 0,50 50,0 50,0 0))") };
            var result = TopologyValidator.PolygonBoundariesMustOverlapPolygonBoundaries(coveringPolygons, coveredPolygons);

            ShowFeatures(coveredPolygons, result.InvalidFeatures, coveringPolygons,
                "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.");
        }

        private void CheckIfPolygonBoundariesOverlapLines(object sender, RoutedEventArgs e)
        {
            var polygons = new Collection<Feature> { new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))") };
            var lines = new Collection<Feature> { new Feature("LINESTRING(-50 0,100 0,100 150)") };
            var result = TopologyValidator.PolygonBoundariesMustOverlapLines(polygons, lines);

            ShowFeatures(polygons, result.InvalidFeatures, lines,
                "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.");
        }

        private void CheckIfPolygonsOverlapPolygons(object sender, RoutedEventArgs e)
        {
            var coveringPolygons = new Collection<Feature> { new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))") };
            var coveredPolygons = new Collection<Feature>
            {
                new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))"),
                new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))"),
                new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))")
            };
            var result = TopologyValidator.PolygonsMustOverlapPolygons(coveringPolygons, coveredPolygons);

            ShowFeatures(coveredPolygons, result.InvalidFeatures, coveringPolygons,
                "Features being validated against are shown in blue. \n\nOverlapping regions are shown in green. \n\nNon-overlapping regions are shown in red.");
        }

        private void CheckIfPolygonsAreWithinPolygons(object sender, RoutedEventArgs e)
        {
            var coveringPolygons = new Collection<Feature> { new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))") };
            var coveredPolygons = new Collection<Feature>
            {
                new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))"),
                new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))"),
                new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))")
            };
            var result = TopologyValidator.PolygonsMustBeWithinPolygons(coveringPolygons, coveredPolygons);

            ShowFeatures(coveredPolygons, result.InvalidFeatures, coveringPolygons,
                "Features being validated against are shown in blue. \n\nPolygons fully within polygons are shown in green. \n\nPolygons not within polygons are shown in red.");
        }

        private void CheckIfPolygonsContainPoints(object sender, RoutedEventArgs e)
        {
            var polygons = new Collection<Feature>
            {
                new Feature("POLYGON((150 0,250 0,250 100,150 100,150 0))"),
                new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))")
            };
            var points = new Collection<Feature> { new Feature("POINT(50 50)") };
            var result = TopologyValidator.PolygonsMustContainPoint(polygons, points);

            ShowFeatures(polygons, result.InvalidFeatures, points,
                "Features being validated against are shown in blue. \n\nPolygons containing points are shown in green. \n\nPolygons not containing points are shown in red.");
        }

        private void CheckIfPolygonsCoverEachOther(object sender, RoutedEventArgs e)
        {
            var polygonFeature1 = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            var polygonFeature2 = new Feature("POLYGON((-50 -50,50 -50,50 50,-50 50,-50 -50))");

            var firstPolygonsSet = new Collection<Feature> { polygonFeature1 };
            var secondPolygonsSet = new Collection<Feature> { polygonFeature2 };
            var result = TopologyValidator.PolygonsMustOverlapEachOther(firstPolygonsSet, secondPolygonsSet);

            ShowFeatures(new Collection<Feature> { polygonFeature1, polygonFeature2 }, result.InvalidFeatures, null,
                "All non-overlapping regions from two different sets of polygons are shown in red. \n\nOverlapping regions are shown in green.");
        }

        private void CheckIfPolygonsHaveGaps(object sender, RoutedEventArgs e)
        {
            var polygons = new Collection<Feature>
            {
                new Feature("POLYGON((0 0,40 0,40 40,0 40,0 0))"),
                new Feature("POLYGON((30 30,70 30,70 70,30 70,30 30))"),
                new Feature("POLYGON((60 0,100 0,100 40,60 40,60 0))"),
                new Feature("POLYGON((30 10,70 10,70 -30,30 -30,30 10))")
            };
            var result = TopologyValidator.PolygonsMustNotHaveGaps(polygons);

            ShowFeatures(polygons, result.InvalidFeatures, null,
                "Features being validated are shown in green. \n\nGaps (inner rings) within the union of the polygons are shown in red.");
        }

        private void CheckPolygonsMustNotOverlap(object sender, RoutedEventArgs e)
        {
            var polygons = new Collection<Feature>
            {
                new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))"),
                new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))"),
                new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))"),
                new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))")
            };
            var result = TopologyValidator.PolygonsMustNotOverlap(polygons);

            ShowFeatures(polygons, result.InvalidFeatures, null,
                "Features being validated are shown in green. \n\nOverlapping polygon regions are shown in red.");
        }

        private void CheckPolygonsMustNotOverlapPolygons(object sender, RoutedEventArgs e)
        {
            var coveringPolygons = new Collection<Feature> { new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))") };
            var coveredPolygons = new Collection<Feature>
            {
                new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))"),
                new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))"),
                new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))")
            };
            var result = TopologyValidator.PolygonsMustNotOverlapPolygons(coveringPolygons, coveredPolygons);

            ShowFeatures(coveredPolygons, result.InvalidFeatures, coveringPolygons,
                "Features being validated against are shown in blue. \n\nNon-overlapping polygon regions are shown in green. \n\nOverlapping polygon regions are shown in red.");
        }

        private void ShowFeatures(Collection<Feature> validatedFeatures, Collection<Feature> invalidFeatures, Collection<Feature> filterFeatures, string info)
        {
            _validatedFeaturesLayer.InternalFeatures.Clear();
            _filterFeaturesLayer.InternalFeatures.Clear();
            _resultFeaturesLayer.InternalFeatures.Clear();

            if (filterFeatures != null)
            {
                foreach (var feature in filterFeatures)
                {
                    _filterFeaturesLayer.InternalFeatures.Add(feature);
                }
            }
            foreach (var feature in validatedFeatures)
            {
                _validatedFeaturesLayer.InternalFeatures.Add(feature);
            }
            foreach (var feature in invalidFeatures)
            {
                _resultFeaturesLayer.InternalFeatures.Add(feature);
            }

            var featuresBBox = _featuresOverlay.GetBoundingBox();
            Map.CenterPoint = featuresBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, featuresBBox, Map.MapWidth, Map.MapHeight) * 1.5;
            TxtValidationInfo.Text = info;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
