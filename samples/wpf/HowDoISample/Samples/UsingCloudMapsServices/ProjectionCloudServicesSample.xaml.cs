using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Learn how to use the ProjectionCloudClient to access the Projection APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ProjectionCloudServicesSample : UserControl, IDisposable
    {
        private ProjectionCloudClient projectionCloudClient;

        public ProjectionCloudServicesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer for the reprojected features
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

            // Create a new feature layer to display the shapes we will be reprojecting
            InMemoryFeatureLayer reprojectedFeaturesLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to an overlay
            LayerOverlay reprojectedFeaturesOverlay = new LayerOverlay();
            reprojectedFeaturesOverlay.Layers.Add("Reprojected Features Layer", reprojectedFeaturesLayer);

            // Add the overlay to the map
            mapView.Overlays.Add("Reprojected Features Overlay", reprojectedFeaturesOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the ProjectionCloudClient with our ThinkGeo Cloud credentials
            projectionCloudClient = new ProjectionCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject a single feature
        /// </summary>
        private async Task<Feature> ReprojectAFeature(Feature decimalDegreeFeature)
        {
            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            Feature reprojectedFeature = await projectionCloudClient.ProjectAsync(decimalDegreeFeature, 4326, 3857);

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeature;
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject multiple features
        /// </summary>
        private async Task<Collection<Feature>> ReprojectMultipleFeatures(Collection<Feature> decimalDegreeFeatures)
        {
            // Show a loading graphic to let users know the request is running
            loadingImage.Visibility = Visibility.Visible;

            Collection<Feature> reprojectedFeatures = await projectionCloudClient.ProjectAsync(decimalDegreeFeatures, 4326, 3857);

            // Hide the loading graphic
            loadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeatures;
        }

        /// <summary>
        /// Draw reprojected features on the map
        /// </summary>
        private void ClearMapAndAddFeatures(Collection<Feature> features)
        {
            // Get the layer we prepared from the MapView
            InMemoryFeatureLayer reprojectedFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Reprojected Features Layer");

            // Clear old features from the feature layer and add the newly reprojected features
            reprojectedFeatureLayer.InternalFeatures.Clear();

            foreach (Feature sphericalMercatorFeature in features)
            {
                reprojectedFeatureLayer.InternalFeatures.Add(sphericalMercatorFeature);
            }

            // Set the map extent to zoom into the feature and refresh the map
            reprojectedFeatureLayer.Open();
            mapView.CurrentExtent = reprojectedFeatureLayer.GetBoundingBox();

            ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
            mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel18.Scale);

            reprojectedFeatureLayer.Close();
            mapView.Refresh();
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject a single feature
        /// </summary>
        private async void ReprojectFeature_Click(object sender, RoutedEventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            Feature decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            Feature sphericalMercatorFeature = await ReprojectAFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { sphericalMercatorFeature });
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject multiple different features
        /// </summary>
        private async void ReprojectMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            // Create features based on the WKT in the textbox in the UI
            Collection<Feature> decimalDegreeFeatures = new Collection<Feature>();
            string[] wktStrings = txtWKT.Text.Split('\n');
            foreach (string wktString in wktStrings)
            {
                try
                {
                    Feature wktFeature = new Feature(wktString);
                    decimalDegreeFeatures.Add(wktFeature);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            Collection<Feature> sphericalMercatorFeatures = await ReprojectMultipleFeatures(decimalDegreeFeatures);

            // Add the reprojected features to the map
            ClearMapAndAddFeatures(sphericalMercatorFeatures);
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
