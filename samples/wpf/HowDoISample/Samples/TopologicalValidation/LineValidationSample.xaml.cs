using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.TopologicalValidation
{
    /// <summary>
    /// Learn how to use the TopologyValidator APIs to perform validation on points
    /// </summary>
    public partial class LineValidationSample : UserControl, IDisposable
    {
        public LineValidationSample()
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
            InMemoryFeatureLayer validatedFeaturesLayer = new InMemoryFeatureLayer();
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Green), GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 3, false);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the shapes to perform the validation against
            // Add styles to display points, lines, and polygons on this layer in blue
            InMemoryFeatureLayer filterFeaturesLayer = new InMemoryFeatureLayer();
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 12, GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Blue), GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 3, false);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the resultf features from the validation API
            // Add styles to display points, lines, and polygons on this layer in red
            InMemoryFeatureLayer resultFeaturesLayer = new InMemoryFeatureLayer();
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 12, GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Red), GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 3, false);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layers to an overlay, and add the overlay to the map
            LayerOverlay featuresOverlay = new LayerOverlay();
            featuresOverlay.Layers.Add("Filter Features", filterFeaturesLayer);
            featuresOverlay.Layers.Add("Validated Features", validatedFeaturesLayer);
            featuresOverlay.Layers.Add("Result Features", resultFeaturesLayer);
            mapView.Overlays.Add("Features Overlay", featuresOverlay);

            // Set a default extent for the map
            mapView.CurrentExtent = new RectangleShape(0, 200, 200, 0);

            rdoCheckLineEndpointsMustTouchPoints.IsChecked = true;
        }

        /// <summary>
        /// Validate lines based on whether their endpoints are touching points, and display the results on the map
        /// </summary>
        private void CheckLineEndpointsMustTouchPoints(object sender, RoutedEventArgs e)
        {
            // Create a sample set of point and line features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0,100 50)");
            Feature pointOnEndpointFeature = new Feature("POINT(0 0)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            Collection<Feature> points = new Collection<Feature>() { pointOnEndpointFeature };
            TopologyValidationResult result = TopologyValidator.LineEndPointsMustTouchPoints(lines, points);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { pointOnEndpointFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine endpoints touching points are shown in green. \n\nInvalid endpoints are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are overlapping polygon boundaries, and display the results on the map
        /// </summary>
        private void CheckLinesMustOverlapPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line and polygon features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(-50 0,150 0)");
            Feature lineOnBoundaryFeature = new Feature("LINESTRING(-50 0,150 0)");
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature, lineOnBoundaryFeature };
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustOverlapPolygonBoundaries(lines, polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature, lineOnBoundaryFeature }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping polygon boundaries are shown in green. \n\nInvalid line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are overlapping lines from a separate set of features, and display the results on the map
        /// </summary>
        private void CheckLinesMustOverlapLines(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            Feature coveringLineFeature = new Feature("LINESTRING(0 -50,50 0,100 0,100 150)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringLines = new Collection<Feature>() { coveringLineFeature };
            Collection<Feature> coveredLines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustBeCoveredByLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { coveringLineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping lines are shown in green. \n\nInvalid line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they are composed of a single part, and display the results on the map
        /// </summary>
        private void CheckLinesMustBeSinglePart(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature singleLineFeature = new Feature("MULTILINESTRING((0 -50,100 -50,100 -100,0 -100))");
            Feature multiLineFeature = new Feature("MULTILINESTRING((0 0,100 0),(100 100,0 100))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { singleLineFeature, multiLineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustBeSinglePart(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { singleLineFeature, multiLineFeature }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines made of single segments are shown in green. \n\nLines with disjoint segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they form a closed polygon, and display the results on the map
        /// </summary>
        private void CheckLinesMustFormClosedPolygon(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100,20 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 0,-50 0,-50 100,0 100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2 };
            TopologyValidationResult result = TopologyValidator.LinesMustFormClosedPolygon(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(lines, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nLine endpoints that do not form a closed polygon are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they form pseudonodes, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotHavePseudonodes(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineSegmentFeature1 = new Feature("LINESTRING(0 0,50 0,50 50,0 0)");
            Feature lineSegmentFeature2 = new Feature("LINESTRING(-50 0,-50 50)");
            Feature lineSegmentFeature3 = new Feature("LINESTRING(-100 0,-50 50)");
            Feature lineSegmentFeature4 = new Feature("LINESTRING(-50 -50,-50 -100)");
            Feature lineSegmentFeature5 = new Feature("LINESTRING(-100 -50,-50 -100)");
            Feature lineSegmentFeature6 = new Feature("LINESTRING(-50 -100,0 -100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineSegmentFeature1, lineSegmentFeature2, lineSegmentFeature3, lineSegmentFeature4, lineSegmentFeature5, lineSegmentFeature6 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotHavePseudonodes(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(lines, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nPseudonodes are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they intersect other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotIntersect(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotIntersect(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersections are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they intersect or touch other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfIntersectOrTouch(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfIntersectOrTouch(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersecting points and overlapping segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they overlap other lines, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotOverlap(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotOverlap(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they overlap other lines from a separate set of features, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotOverlapLines(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature overlappingLineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            Feature overlappedLineFeature = new Feature("LINESTRING(150 0,100 30,100 60,150 100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringLines = new Collection<Feature>() { overlappingLineFeature };
            Collection<Feature> coveredLines = new Collection<Feature>() { overlappedLineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustNotOverlapLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { overlappedLineFeature }, invalidResultFeatures, new Collection<Feature>() { overlappingLineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLines being validated are shown in green. \n\nOverlapping line segments are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they self-intersect, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfIntersect(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature selfIntersectingLine = new Feature("LINESTRING(0 0,100 0,100 100,50 100,50 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { selfIntersectingLine };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfIntersect(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { selfIntersectingLine }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nSelf-intersections are shown in red.";
        }

        /// <summary>
        /// Validate lines based on whether they elf-overlap, and display the results on the map
        /// </summary>
        private void CheckLinesMustNotSelfOverlap(object sender, RoutedEventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature selfOverlappingLine = new Feature("LINESTRING(0 0,100 0,100 100,0 100,20 0,40 0,40 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { selfOverlappingLine };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfOverlap(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { selfOverlappingLine }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";
        }

        /// <summary>
        /// Clear the previously displayed features from the map, and add new features
        /// </summary>
        private void ClearMapAndAddFeatures(Collection<Feature> validatedFeatures, Collection<Feature> resultFeatures, Collection<Feature> filterFeatures = null)
        {
            // Get the InMemoryFeatureLayers from the MapView
            InMemoryFeatureLayer validatedFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Validated Features");
            InMemoryFeatureLayer filterFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Filter Features");
            InMemoryFeatureLayer resultFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Features");

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
                foreach (Feature filterFeature in filterFeatures)
                {
                    filterFeaturesLayer.InternalFeatures.Add(filterFeature);
                }
            }

            // Add (green) validated features to the map
            foreach (Feature validatedFeature in validatedFeatures)
            {
                validatedFeaturesLayer.InternalFeatures.Add(validatedFeature);
            }

            // Add (red) invalid features to the map
            foreach (Feature resultFeature in resultFeatures)
            {
                resultFeaturesLayer.InternalFeatures.Add(resultFeature);
            }

            // Refresh/redraw the layers and reset the map extent
            LayerOverlay featureOverlay = (LayerOverlay)mapView.Overlays["Features Overlay"];
            mapView.CurrentExtent = featureOverlay.GetBoundingBox();
            mapView.Refresh();

            validatedFeaturesLayer.Close();
            filterFeaturesLayer.Close();
            resultFeaturesLayer.Close();
            mapView.ZoomOut();
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
