using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the TopologyValidator APIs to perform validation on points
    /// </summary>
    public partial class ValidateLineTopology : IDisposable
    {
        public ValidateLineTopology()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up feature layers in the MapView to display the validated features
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create an InMemoryFeatureLayer to hold the shapes to be validated
            // Add styles to display points, lines, and polygons on this layer in green
            var validatedFeaturesLayer = new InMemoryFeatureLayer();
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Green), GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 3, false);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the shapes to perform the validation against
            // Add styles to display points, lines, and polygons on this layer in blue
            var filterFeaturesLayer = new InMemoryFeatureLayer();
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 12, GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Blue), GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 3, false);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the result features from the validation API
            // Add styles to display points, lines, and polygons on this layer in red
            var resultFeaturesLayer = new InMemoryFeatureLayer();
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 12, GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Red), GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 3, false);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layers to an overlay, and add the overlay to the map
            var featuresOverlay = new LayerOverlay();
            featuresOverlay.Layers.Add("Filter Features", filterFeaturesLayer);
            featuresOverlay.Layers.Add("Validated Features", validatedFeaturesLayer);
            featuresOverlay.Layers.Add("Result Features", resultFeaturesLayer);
            MapView.Overlays.Add("Features Overlay", featuresOverlay);

            // Set a default extent for the map
            MapView.CenterPoint = new PointShape(100, 100);
            MapView.CurrentScale = 1060;

            RdoCheckLineEndpointsMustTouchPoints.IsChecked = true;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Validate lines based on whether their endpoints are touching points, and display the results on the map
        /// </summary>
        private void CheckLineEndpointsMustTouchPoints(object sender, RoutedEventArgs e)
        {
            // Create a sample set of point and line features to use for the validation
            var lineFeature = new Feature("LINESTRING(0 0,100 0,100 50)");
            var pointOnEndpointFeature = new Feature("POINT(0 0)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature };
            var points = new Collection<Feature>() { pointOnEndpointFeature };
            var result = TopologyValidator.LineEndPointsMustTouchPoints(lines, points);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { pointOnEndpointFeature });

            // Update the help text
            TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine endpoints touching points are shown in green. \n\nInvalid endpoints are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are overlapping polygon boundaries, and display the results on the map
        /// </summary>
        private void CheckLinesMustOverlapPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line and polygon features to use for the validation
            var lineFeature = new Feature("LINESTRING(-50 0,150 0)");
            var lineOnBoundaryFeature = new Feature("LINESTRING(-50 0,150 0)");
            var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature, lineOnBoundaryFeature };
            var polygons = new Collection<Feature>() { polygonFeature };
            var result = TopologyValidator.LinesMustOverlapPolygonBoundaries(lines, polygons);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature, lineOnBoundaryFeature }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

            // Update the help text
            TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping polygon boundaries are shown in green. \n\nInvalid line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are overlapping lines from a separate set of features, and display the results on the map
        /// </summary>
        private void CheckLinesMustOverlapLines(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            var coveringLineFeature = new Feature("LINESTRING(0 -50,50 0,100 0,100 150)");

            // Use the TopologyValidator API to validate the sample data
            var coveringLines = new Collection<Feature>() { coveringLineFeature };
            var coveredLines = new Collection<Feature>() { lineFeature };
            var result = TopologyValidator.LinesMustBeCoveredByLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { coveringLineFeature });

            // Update the help text
            TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping lines are shown in green. \n\nInvalid line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are composed of a single part, and display the results on the map
        /// </summary>
        private void CheckLinesMustBeSinglePart(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var singleLineFeature = new Feature("MULTILINESTRING((0 -50,100 -50,100 -100,0 -100))");
            var multiLineFeature = new Feature("MULTILINESTRING((0 0,100 0),(100 50,0 50))");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { singleLineFeature, multiLineFeature };
            var result = TopologyValidator.LinesMustBeSinglePart(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { singleLineFeature, multiLineFeature }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines made of single segments are shown in green. \n\nLines with disjoint segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they form a closed polygon, and display the results on the map
        /// </summary>
        private void CheckLinesMustFormClosedPolygon(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100,20 100)");
            var lineFeature2 = new Feature("LINESTRING(0 0,-50 0,-50 100,0 100)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature1, lineFeature2 };
            var result = TopologyValidator.LinesMustFormClosedPolygon(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(lines, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nLine endpoints that do not form a closed polygon are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they form pseudonodes, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotHavePseudonodes(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineSegmentFeature1 = new Feature("LINESTRING(0 0,50 0,50 50,0 0)");
            var lineSegmentFeature2 = new Feature("LINESTRING(-50 0,-50 50)");
            var lineSegmentFeature3 = new Feature("LINESTRING(-100 0,-50 50)");
            var lineSegmentFeature4 = new Feature("LINESTRING(-50 -50,-50 -100)");
            var lineSegmentFeature5 = new Feature("LINESTRING(-100 -50,-50 -100)");
            var lineSegmentFeature6 = new Feature("LINESTRING(-50 -100,0 -100)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineSegmentFeature1, lineSegmentFeature2, lineSegmentFeature3, lineSegmentFeature4, lineSegmentFeature5, lineSegmentFeature6 };
            var result = TopologyValidator.LinesMustNotHavePseudonodes(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(lines, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nPseudonodes are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they intersect other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotIntersect(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            var lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            var lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            var result = TopologyValidator.LinesMustNotIntersect(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersections are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they intersect or touch other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfIntersectOrTouch(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            var lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            var lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            var result = TopologyValidator.LinesMustNotSelfIntersectOrTouch(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersecting points and overlapping segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they overlap other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotOverlap(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            var lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            var result = TopologyValidator.LinesMustNotOverlap(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they overlap other lines from a separate set of features, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotOverlapLines(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var overlappingLineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            var overlappedLineFeature = new Feature("LINESTRING(150 0,100 30,100 60,150 100)");

            // Use the TopologyValidator API to validate the sample data
            var coveringLines = new Collection<Feature>() { overlappingLineFeature };
            var coveredLines = new Collection<Feature>() { overlappedLineFeature };
            var result = TopologyValidator.LinesMustNotOverlapLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { overlappedLineFeature }, invalidResultFeatures, new Collection<Feature>() { overlappingLineFeature });

            // Update the help text
            TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLines being validated are shown in green. \n\nOverlapping line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they self-intersect, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfIntersect(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var selfIntersectingLine = new Feature("LINESTRING(0 0,100 0,100 100,50 100,50 -50)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { selfIntersectingLine };
            var result = TopologyValidator.LinesMustNotSelfIntersect(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { selfIntersectingLine }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nSelf-intersections are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they elf-overlap, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfOverlap(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            var selfOverlappingLine = new Feature("LINESTRING(0 0,100 0,100 100,0 100,20 0,40 0,40 -50)");

            // Use the TopologyValidator API to validate the sample data
            var lines = new Collection<Feature>() { selfOverlappingLine };
            var result = TopologyValidator.LinesMustNotSelfOverlap(lines);

            // Get the invalid features returned from the API
            var invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { selfOverlappingLine }, invalidResultFeatures);

            // Update the help text
            TxtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";
        }

        /// <summary>
        /// Clear the previously displayed features from the map, and add new features
        /// </summary>
        private async Task ClearMapAndAddFeaturesAsync(Collection<Feature> validatedFeatures, Collection<Feature> resultFeatures, Collection<Feature> filterFeatures = null)
        {
            // Get the InMemoryFeatureLayers from the MapView
            var validatedFeaturesLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Validated Features");
            var filterFeaturesLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Filter Features");
            var resultFeaturesLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Result Features");

            validatedFeaturesLayer.Open();
            filterFeaturesLayer.Open();
            resultFeaturesLayer.Open();

            // Clear the existing features from each layer
            validatedFeaturesLayer.Clear();
            filterFeaturesLayer.Clear();
            resultFeaturesLayer.Clear();

            // Add (blue) filter features to the map, if there are any
            if (filterFeatures != null)
            {
                foreach (var filterFeature in filterFeatures)
                {
                    filterFeaturesLayer.InternalFeatures.Add(filterFeature);
                }
            }

            // Add (green) validated features to the map
            foreach (var validatedFeature in validatedFeatures)
            {
                validatedFeaturesLayer.InternalFeatures.Add(validatedFeature);
            }

            // Add (red) invalid features to the map
            // ReSharper disable once SuggestVarOrType_SimpleTypes
            foreach (Feature resultFeature in resultFeatures)
            {
                resultFeaturesLayer.InternalFeatures.Add(resultFeature);
            }

            // Refresh/redraw the layers and reset the map extent
            var featureOverlay = (LayerOverlay)MapView.Overlays["Features Overlay"];
            var featureOverlayBBox = featureOverlay.GetBoundingBox();
            MapView.CenterPoint = featureOverlayBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(featureOverlayBBox, MapView.ActualWidth, MapView.MapUnit) * 2.0; // Multiply the current scale by 2.0 to enhance the map extent.
            await MapView.RefreshAsync();

            validatedFeaturesLayer.Close();
            filterFeaturesLayer.Close();
            resultFeaturesLayer.Close();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}