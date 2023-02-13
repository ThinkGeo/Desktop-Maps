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
    public partial class PolygonValidationSample : UserControl, IDisposable
    {
        public PolygonValidationSample()
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

            rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.IsChecked = true;
        }

        /// <summary>
        /// Validate polygons based on whether their boundaries overlap with the boundaries of a second set of polygons, and display the results on the map
        /// </summary>
        private void CheckIfPolygonBoundariesOverlapPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature coveredPolygonFeature = new Feature("POLYGON((0 0,50 0,50 50,0 50,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { coveredPolygonFeature };
            TopologyValidationResult result = TopologyValidator.PolygonBoundariesMustOverlapPolygonBoundaries(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { coveredPolygonFeature }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether their boundaries overlap with a separate set of lines, and display the results on the map
        /// </summary>
        private void CheckIfPolygonBoundariesOverlapLines(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon and line features to use for the validation
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature lineFeature = new Feature("LINESTRING(-50 0,100 0,100 150)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.PolygonBoundariesMustOverlapLines(polygons, lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether they overlap a second set of polygons, and display the results on the map
        /// </summary>
        private void CheckIfPolygonsOverlapPolygons(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustOverlapPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nOverlapping regions are shown in green. \n\nNon-overlapping regions are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether they lie within other polygons, and display the results on the map
        /// </summary>
        private void CheckIfPolygonsAreWithinPolygons(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustBeWithinPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons fully within polygons are shown in green. \n\nPolygons not within polygons are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether they contain points, and display the results on the map
        /// </summary>
        private void CheckIfPolygonsContainPoints(object sender, RoutedEventArgs e)
        {
            // Create a sample set of points and polygon features to use for the validation
            Feature pointFeature = new Feature("POINT(50 50)");
            Feature polygonWithPointFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature polygonFeature = new Feature("POLYGON((150 0,250 0,250 100,150 100,150 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature, polygonWithPointFeature };
            Collection<Feature> points = new Collection<Feature>() { pointFeature };
            TopologyValidationResult result = TopologyValidator.PolygonsMustContainPoint(polygons, points);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature, polygonWithPointFeature }, invalidResultFeatures, new Collection<Feature>() { pointFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons containing points are shown in green. \n\nPolygons not containing points are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether they overlap each other, and display the results on the map. Unlike other validations, this function validates and returns invalid polygons from both input sets
        /// </summary>
        private void CheckIfPolygonsCoverEachOther(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature polygonFeature2 = new Feature("POLYGON((-50 -50,50 -50,50 50,-50 50,-50 -50))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> firstPolygonsSet = new Collection<Feature>() { polygonFeature1 };
            Collection<Feature> secondPolygonsSet = new Collection<Feature>() { polygonFeature2 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustOverlapEachOther(firstPolygonsSet, secondPolygonsSet);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "All non-overlapping regions from two different sets of polygons are shown in red. \n\nOverlapping regions are shown in green";
        }

        /// <summary>
        /// Validate polygons based on whether the union of the polygons has any interior gaps, and display the results on the map
        /// </summary>
        private void CheckIfPolygonsHaveGaps(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((0 0,40 0,40 40,0 40,0 0))");
            Feature polygonFeature2 = new Feature("POLYGON((30 30,70 30,70 70,30 70,30 30))");
            Feature polygonFeature3 = new Feature("POLYGON((60 0,100 0,100 40,60 40,60 0))");
            Feature polygonFeature4 = new Feature("POLYGON((30 10,70 10,70 -30,30 -30,30 10))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotHaveGaps(polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Features being validated are shown in green. \n\nGaps (Inner rings) within the union of the polygons are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether polygons within the same set overlap, and display the results on the map
        /// </summary>
        private void CheckPolygonsMustNotOverlap(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature polygonFeature4 = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotOverlap(polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Features being validated are shown in green. \n\nOverlapping polygon regions are shown in red.";
        }

        /// <summary>
        /// Validate polygons based on whether they overlap polygons from a separate set, and display the results on the map
        /// </summary>
        private void CheckPolygonsMustNotOverlapPolygons(object sender, RoutedEventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotOverlapPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nNon-overlapping polygon regions are shown in green. \n\nOverlapping polygon regions are shown in red.";
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
