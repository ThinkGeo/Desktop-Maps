using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the MapsQueryClient to query the WorldMaps dataset available from the ThinkGeo Cloud
    /// </summary>
    public partial class WorldMapsQueryCloudServicesSample : UserControl
    {
        private MapsQueryCloudClient mapsQueryCloudClient;

        public WorldMapsQueryCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and feature layers for the queried shapes
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the query shape used to perform the query
            InMemoryFeatureLayer queryShapeFeatureLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the the query shape will be drawn
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));

            // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new feature layer to display the shapes returned by the query.
            InMemoryFeatureLayer queriedFeaturesLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the returned shapes will be drawn
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.OrangeRed);
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.OrangeRed);
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.OrangeRed, new GeoSolidBrush(new GeoColor(10, GeoColors.OrangeRed)));
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the feature layers to an overlay, then add that overlay to the map
            LayerOverlay queriedFeaturesOverlay = new LayerOverlay();
            queriedFeaturesOverlay.Layers.Add("Queried Features Layer", queriedFeaturesLayer);
            queriedFeaturesOverlay.Layers.Add("Query Shape Layer", queryShapeFeatureLayer);
            mapView.Overlays.Add("Queried Features Overlay", queriedFeaturesOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Add an event to handle new shapes that are drawn on the map
            mapView.TrackOverlay.TrackEnded += OnShapeDrawn;

            // Initialize the MapsQueryCloudClient with our ThinkGeo Cloud credentials
            mapsQueryCloudClient = new MapsQueryCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Create a sample shape and add it to the query shape layer
            RectangleShape sampleShape = new RectangleShape(-10779877.70, 3915441.00, -10779248.97, 3915119.63);
            queryShapeFeatureLayer.InternalFeatures.Add(new Feature(sampleShape));

            // Run the world maps query
            PerformWorldMapsQuery();
        }

        /// <summary>
        /// Get features from the WorldMapsQuery service based on the UI parameters
        /// </summary>
        private async void PerformWorldMapsQuery()
        {
            // Get the feature layers from the MapView
            LayerOverlay queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            InMemoryFeatureLayer queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            InMemoryFeatureLayer queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Show an error if trying to query with no query shape
            if (queryShapeFeatureLayer.InternalFeatures.Count == 0)
            {
                MessageBox.Show("Please draw a shape to use for the query", "Error");
                return;
            }

            // Set the MapsQuery parameters based on the drawn query shape and the UI
            BaseShape queryShape = queryShapeFeatureLayer.InternalFeatures[0].GetShape();
            int projectionInSrid = 3857;
            string queryLayer = ((ComboBoxItem)cboQueryLayer.SelectedItem).Content.ToString().ToLower();

            CloudMapsQueryResult result = new CloudMapsQueryResult();

            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            // Perform the world maps query
            try
            {
                switch (((ComboBoxItem)cboQueryType.SelectedItem).Content.ToString())
                {
                    case "Containing":
                        result = await mapsQueryCloudClient.GetFeaturesContainingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Nearest":
                        result = await mapsQueryCloudClient.GetFeaturesNearestAsync(queryLayer, queryShape, projectionInSrid, (int)maxResults.Value);
                        break;
                    case "Intersecting":
                        result = await mapsQueryCloudClient.GetFeaturesIntersectingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Overlapping":
                        result = await mapsQueryCloudClient.GetFeaturesOverlappingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Within":
                        result = await mapsQueryCloudClient.GetFeaturesWithinAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors returned from the maps query service
                if (ex is ArgumentException)
                {
                    MessageBox.Show(string.Format("{0} {1}", ex.InnerException.Message, ex.Message), "Invalid Request");
                    mapView.Refresh();
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Unexpected Error");
                    mapView.Refresh();
                    return;
                }
            }
            finally
            {
                // Hide the loading graphic
                loadingImage.Visibility = Visibility.Hidden;
            }

            if (result.Features.Count > 0)
            {
                // Add any features found by the query to the map
                foreach (Feature feature in result.Features)
                {
                    queriedFeaturesLayer.InternalFeatures.Add(feature);
                }

                // Set the map extent to the extent of the query results
                queriedFeaturesLayer.Open();
                mapView.CurrentExtent = queriedFeaturesLayer.GetBoundingBox();
                queriedFeaturesLayer.Close();
            }
            else
            {
                MessageBox.Show("No features found in the selected area");
            }

            // Refresh and redraw the map
            mapView.Refresh();
        }

        /// <summary>
        /// Disable drawing mode and draw the new query shape on the map when finished drawing a shape
        /// </summary>
        private void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Get the query shape layer from the MapView
            LayerOverlay queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            InMemoryFeatureLayer queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];

            // Add the newly drawn shape, then redraw the overlay
            queryShapeFeatureLayer.InternalFeatures.Add(new Feature(e.TrackShape));
            queriedFeaturesOverlay.Refresh();

            PerformWorldMapsQuery();
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Query Point' button
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Point'
            mapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Clear the old shapes from the map
            ClearQueryShapes();
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Query Line' button
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Line'
            mapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Clear the old shapes from the map
            ClearQueryShapes();
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Query Polygon' button
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Clear the old shapes from the map
            ClearQueryShapes();
        }

        /// <summary>
        /// Clear the query shapes from the map
        /// </summary>
        private void ClearQueryShapes()
        {
            // Get the query shape layer from the MapView
            LayerOverlay queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            InMemoryFeatureLayer queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            InMemoryFeatureLayer queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Clear the old query result and query shape from the map
            queriedFeaturesLayer.InternalFeatures.Clear();
            queryShapeFeatureLayer.InternalFeatures.Clear();
            queriedFeaturesOverlay.Refresh();
        }
    }
}
