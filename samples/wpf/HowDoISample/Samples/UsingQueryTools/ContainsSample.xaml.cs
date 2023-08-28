using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer contain a shape
    /// </summary>
    public partial class ContainsSample : UserControl, IDisposable
    {
        public ContainsSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco zoning data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold the Frisco zoning data
            ShapeFileFeatureLayer zoningLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco zoning polygons
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10781137.28, 3917162.59, -10774579.34, 3911241.35);

            // Create a layer to hold features found by the spatial query
            InMemoryFeatureLayer highlightedFeaturesLayer = new InMemoryFeatureLayer();
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay zoningOverlay = new LayerOverlay();
            zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
            mapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

            LayerOverlay highlightedFeaturesOverlay = new LayerOverlay();
            highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
            mapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

            // Add a MarkerOverlay to the map to display the selected point for the query
            SimpleMarkerOverlay queryFeatureMarkerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Query Feature Marker Overlay", queryFeatureMarkerOverlay);

            // Add a sample point to the map for the initial query
            PointShape sampleShape = new PointShape(-10779425.2690712, 3914970.73561765);
            GetFeaturesContaining(sampleShape);

            // Set the map extent to the sample shape
            mapView.CurrentExtent = new RectangleShape(-10781407.8544813, 3916678.62545891, -10777442.6836611, 3913262.84577639);

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Perform the 'Contains' spatial query using the layer's QueryTools
        /// </summary>
        private Collection<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.QueryTools.GetFeaturesContaining(shape, ReturningColumnsType.AllColumns);
            layer.Close();

            return features;
        }

        /// <summary>
        /// Highlight the features that were found by the spatial query
        /// </summary>
        private async Task HighlightQueriedFeaturesAsync(IEnumerable<Feature> features)
        {
            // Find the layers we will be modifying in the MapView dictionary
            LayerOverlay highlightedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Highlighted Features Overlay"];
            InMemoryFeatureLayer highlightedFeaturesLayer = (InMemoryFeatureLayer)highlightedFeaturesOverlay.Layers["Highlighted Features"];

            // Clear the currently highlighted features
            highlightedFeaturesLayer.Open();
            highlightedFeaturesLayer.InternalFeatures.Clear();

            // Add new features to the layer
            foreach (var feature in features)
            {
                highlightedFeaturesLayer.InternalFeatures.Add(feature);
            }
            highlightedFeaturesLayer.Close();

            // Refresh the overlay so the layer is redrawn
            await highlightedFeaturesOverlay.RefreshAsync();

            // Update the number of matching features found in the UI
            txtNumberOfFeaturesFound.Text = string.Format("Number of features containing the drawn shape: {0}", features.Count());
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async void GetFeaturesContaining(PointShape point)
        {
            // Find the layers we will be modifying in the MapView
            SimpleMarkerOverlay queryFeatureMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["Query Feature Marker Overlay"];
            ShapeFileFeatureLayer zoningLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query point marker overlaylayer and add a marker on the newly drawn point
            queryFeatureMarkerOverlay.Markers.Clear();

            // Create a marker with a static marker image and add it to the map
            var marker = CreateNewMarker(point);
            queryFeatureMarkerOverlay.Markers.Add(marker);
            await queryFeatureMarkerOverlay.RefreshAsync();

            // Perform the spatial query using the drawn point and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(point, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Clear the drawn point
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        /// <summary>
        /// Perform the spatial query when a new point is drawn
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            GetFeaturesContaining(e.WorldLocation);
        }

        /// <summary>
        /// Create a new map marker using preloaded image assets
        /// </summary>
        private Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
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
