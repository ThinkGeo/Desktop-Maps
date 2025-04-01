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
    public partial class ValidatePointTopology : IDisposable
    {
        public ValidatePointTopology()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up feature layers in the MapView to display the validated features
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
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
                featuresOverlay.TileType = TileType.SingleTile;
                featuresOverlay.Layers.Add("Filter Features", filterFeaturesLayer);
                featuresOverlay.Layers.Add("Validated Features", validatedFeaturesLayer);
                featuresOverlay.Layers.Add("Result Features", resultFeaturesLayer);
                MapView.Overlays.Add("Features Overlay", featuresOverlay);

                // Set a default extent for the map
                MapView.CenterPoint = new PointShape(100,100);
                MapView.CurrentScale = 1060;

                RdoCheckIfPointsAreTouchingLines.IsChecked = true;

                await MapView.RefreshAsync();
                //await MapView.ZoomInAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Validate points based on whether they are touching lines, and display the results on the map
        /// </summary>
        private async void CheckIfPointsAreTouchingLines(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a sample set of point and line features to use for the validation
                var uncoveredPointFeature1 = new Feature("POINT(0 0)");
                var uncoveredPointFeature2 = new Feature("POINT(50 0)");
                var coveredPointFeature = new Feature("POINT(150 0)");
                var lineFeature = new Feature("LINESTRING(0 0,100 0)");

                // Use the TopologyValidator API to validate the sample data
                var points = new Collection<Feature>() { uncoveredPointFeature1, uncoveredPointFeature2, coveredPointFeature };
                var lines = new Collection<Feature>() { lineFeature };
                var result = TopologyValidator.PointsMustTouchLines(points, lines);

                // Get the invalid features returned from the API
                var invalidResultFeatures = result.InvalidFeatures;

                // Clear the MapView and add the new valid/invalid features to the map
                await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { uncoveredPointFeature1, uncoveredPointFeature2, coveredPointFeature }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

                // Update the help text
                TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching lines are shown in green. \n\nPoints not touching lines are shown in red.";
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Validate points based on whether they are touching line endpoints, and display the results on the map
        /// </summary>
        private async void CheckIfPointsAreTouchingLineEndpoints(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a sample set of point and line features to use for the validation
                var pointFeature1 = new Feature("POINT(0 0)");
                var pointFeature2 = new Feature("POINT(50 0)");
                var pointFeatureOnEndpoint = new Feature("POINT(100 0)");
                var lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");

                // Use the TopologyValidator API to validate the sample data
                var points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnEndpoint };
                var lines = new Collection<Feature>() { lineFeature };
                var result = TopologyValidator.PointsMustTouchLineEndpoints(points, lines);

                // Get the invalid features returned from the API
                var invalidResultFeatures = result.InvalidFeatures;

                // Clear the MapView and add the new valid/invalid features to the map
                await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnEndpoint }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

                // Update the help text
                TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching line endpoints are shown in green. \n\nPoints not touching line endpoints are shown in red.";
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Validate points based on whether they are touching polygon boundaries, and display the results on the map
        /// </summary>
        private async void CheckIfPointsAreTouchingPolygonBoundaries(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a sample set of point and polygon features to use for the validation
                var pointFeature1 = new Feature("POINT(150 0)");
                var pointFeature2 = new Feature("POINT(50 50)");
                var pointFeatureOnBoundary = new Feature("POINT(0 0)");
                var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

                // Use the TopologyValidator API to validate the sample data
                var points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnBoundary };
                var polygons = new Collection<Feature>() { polygonFeature };
                var result = TopologyValidator.PointsMustTouchPolygonBoundaries(points, polygons);

                // Get the invalid features returned from the API
                var invalidResultFeatures = result.InvalidFeatures;

                // Clear the MapView and add the new valid/invalid features to the map
                await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnBoundary }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

                // Update the help text
                TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching polygon boundaries are shown in green. \n\nPoints not touching polygon boundaries are shown in red.";
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Validate points based on whether they are within polygons, and display the results on the map
        /// </summary>
        private async void CheckIfPointsAreWithinPolygons(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a sample set of point and polygon features to use for the validation
                var pointFeature1 = new Feature("POINT(150 0)");
                var pointFeature2 = new Feature("POINT(0 0)");
                var pointFeatureInsidePolygon = new Feature("POINT(50 50)");
                var polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

                // Use the TopologyValidator API to validate the sample data
                var points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureInsidePolygon };
                var polygons = new Collection<Feature>() { polygonFeature };
                var result = TopologyValidator.PointsMustBeWithinPolygons(points, polygons);

                // Get the invalid features returned from the API
                var invalidResultFeatures = result.InvalidFeatures;

                // Clear the MapView and add the new valid/invalid features to the map
                await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureInsidePolygon }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

                // Update the help text
                TxtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints within polygons are shown in green. \n\nPoints not within polygons are shown in red.";
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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
            foreach (var resultFeature in resultFeatures)
            {
                resultFeaturesLayer.InternalFeatures.Add(resultFeature);
            }

            // Refresh/redraw the layers and reset the map extent
            var featureOverlay = (LayerOverlay)MapView.Overlays["Features Overlay"];
            var featureOverlayBBox = featureOverlay.GetBoundingBox();
            MapView.CenterPoint = featureOverlayBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(featureOverlayBBox, MapView.ActualWidth, MapView.MapUnit);
            await MapView.RefreshAsync();

            validatedFeaturesLayer.Close();
            filterFeaturesLayer.Close();
            resultFeaturesLayer.Close();
            await MapView.ZoomOutAsync();
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