using NetTopologySuite.Geometries;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the ElevationCloudClient class to get elevation data from the ThinkGeo Cloud
    /// </summary>
    public partial class ElevationCloudServicesSample : UserControl, IDisposable
    {
        private ElevationCloudClient elevationCloudClient;

        public ElevationCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layers for the shape to be queried and the returned elevation points
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new InMemoryFeatureLayer to hold the shape drawn for the elevation query
            InMemoryFeatureLayer drawnShapeLayer = new InMemoryFeatureLayer();
            
            // Create Point, Line, and Polygon styles to display the drawn shape, and apply them across all zoom levels
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new InMemoryFeatureLayer to display the elevation points returned from the query
            InMemoryFeatureLayer elevationPointsLayer = new InMemoryFeatureLayer();

            // Create a point style for the elevation points
            elevationPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            elevationPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the feature layers to an overlay, and add the overlay to the map
            LayerOverlay elevationFeaturesOverlay = new LayerOverlay();
            elevationFeaturesOverlay.Layers.Add("Elevation Points Layer", elevationPointsLayer);
            elevationFeaturesOverlay.Layers.Add("Drawn Shape Layer", drawnShapeLayer);
            mapView.Overlays.Add("Elevation Features Overlay", elevationFeaturesOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Add an event to trigger the elevation query when a new shape is drawn
            mapView.TrackOverlay.TrackEnded += OnShapeDrawn;

            // Initialize the ElevationCloudClient with our ThinkGeo Cloud credentials
            elevationCloudClient = new ElevationCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Create a sample line and get elevation along that line
            LineShape sampleShape = new LineShape("LINESTRING(-10776298.0601626 3912306.29684573,-10776496.3187036 3912399.45447343,-10776675.4679876 3912478.28015841,-10776890.4471285 3912516.49867234,-10777189.0292686 3912509.33270098,-10777329.9600387 3912442.4503016,-10777664.3720356 3912174.92070409)");
            PerformElevationQuery(sampleShape);
        }

        /// <summary>
        /// Get elevation data using the ElevationCloudClient and update the UI
        /// </summary>
        private async void PerformElevationQuery(BaseShape queryShape)
        {
            // Get feature layers from the MapView
            LayerOverlay elevationPointsOverlay = (LayerOverlay)mapView.Overlays["Elevation Features Overlay"];
            InMemoryFeatureLayer drawnShapesLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Drawn Shape Layer"];
            InMemoryFeatureLayer elevationPointsLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Elevation Points Layer"];

            // Clear the existing shapes from the map
            elevationPointsLayer.Open();
            elevationPointsLayer.Clear();
            elevationPointsLayer.Close();
            drawnShapesLayer.Open();
            drawnShapesLayer.Clear();
            drawnShapesLayer.Close();

            // Add the drawn shape to the map
            drawnShapesLayer.InternalFeatures.Add(new Feature(queryShape));

            // Set options from the UI and run the query using the ElevationCloudClient
            Collection<CloudElevationPointResult> elevationPoints = new Collection<CloudElevationPointResult>();
            int projectionInSrid = 3857;

            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // The point interval distance determines how many elevation points are retrieved for line and area queries
            int pointIntervalDistance = (int)intervalDistance.Value;
            switch (queryShape.GetWellKnownType())
            {
                case WellKnownType.Point:
                    PointShape drawnPoint = (PointShape)queryShape;
                    double elevation = await elevationCloudClient.GetElevationOfPointAsync(drawnPoint.X, drawnPoint.Y, projectionInSrid);

                    // The API for getting the elevation of a single point returns a double, so we manually create a CloudElevationPointResult to use as a data source for the Elevations list
                    elevationPoints.Add(new CloudElevationPointResult(elevation, drawnPoint));

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", elevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", elevation, drawnPoint);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", elevation, drawnPoint);
                    break;
                case WellKnownType.Line:
                    LineShape drawnLine = (LineShape)queryShape;
                    var result = await elevationCloudClient.GetElevationOfLineAsync(drawnLine, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter, DistanceUnit.Feet);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", result.AverageElevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", result.HighestElevationPoint.Elevation, result.HighestElevationPoint.Point);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", result.LowestElevationPoint.Elevation, result.LowestElevationPoint.Point);
                    break;
                case WellKnownType.Polygon:
                    PolygonShape drawnPolygon = (PolygonShape)queryShape;
                    result = await elevationCloudClient.GetElevationOfAreaAsync(drawnPolygon, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter, DistanceUnit.Feet);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", result.AverageElevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", result.HighestElevationPoint.Elevation, result.HighestElevationPoint.Point);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", result.LowestElevationPoint.Elevation, result.LowestElevationPoint.Point);
                    break;
                default:
                    break;
            }

            // Add the elevation result points to the map and list box
            foreach (CloudElevationPointResult elevationPoint in elevationPoints)
            {
                elevationPointsLayer.InternalFeatures.Add(new Feature(elevationPoint.Point));
            }
            lsbElevations.ItemsSource = elevationPoints;

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            // Set the map extent to the elevation query feature
            drawnShapesLayer.Open();
            mapView.CurrentExtent = drawnShapesLayer.GetBoundingBox();
            await mapView.ZoomToScaleAsync(mapView.CurrentScale * 2);
            drawnShapesLayer.Close();
            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Disable map drawing after a shape is drawn
        /// </summary>
        private void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Validate shape size to avoid queries that are too large
            // Maximum length of a line is 10km
            // Maximum area of a polygon is 10km^2
            if(e.TrackShape.GetWellKnownType() == WellKnownType.Polygon)
            {
                if(((PolygonShape)e.TrackShape).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers) > 5)
                {
                    MessageBox.Show("Please draw a smaller polygon (limit: 5km^2)", "Error");
                    return;
                }
            } 
            else if (e.TrackShape.GetWellKnownType() == WellKnownType.Line)
            {
                if (((LineShape)e.TrackShape).GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer) > 5)
                {
                    MessageBox.Show("Please draw a shorter line (limit: 5km)", "Error");
                    return;
                }
            }

            // Get elevation data for the drawn shape and update the UI
            PerformElevationQuery(e.TrackShape);
        }

        /// <summary>
        /// Center the map on a point when it's selected in the UI
        /// </summary>
        private async void lsbElevations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbElevations.SelectedItem != null)
            {
                // Set the map extent to the selected point
                CloudElevationPointResult elevationPoint = (CloudElevationPointResult)lsbElevations.SelectedItem;
                mapView.CurrentExtent = elevationPoint.Point.GetBoundingBox();
                await mapView.RefreshAsync();
            }
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Point' button
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Point'
            mapView.TrackOverlay.TrackMode = TrackMode.Point;
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Line' button
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Line'
            mapView.TrackOverlay.TrackMode = TrackMode.Line;
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Polygon' button
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;
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
