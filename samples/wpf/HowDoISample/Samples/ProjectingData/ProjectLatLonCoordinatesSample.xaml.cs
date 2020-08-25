using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Learn how to reproject features using the ProjectionConverter class
    /// </summary>
    public partial class ProjectLatLonCoordinatesSample : UserControl, IDisposable
    {
        public ProjectLatLonCoordinatesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
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
            mapView.CurrentExtent = new RectangleShape(-10779751.80, 3915369.33, -10779407.60, 3915141.57);
        }

        /// <summary>
        /// Use the ProjectionConverter to reproject a single feature
        /// </summary>
        private Feature ReprojectFeature(Feature decimalDegreeFeature)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            Feature sphericalMercatorFeature = projectionConverter.ConvertToExternalProjection(decimalDegreeFeature);
            projectionConverter.Close();

            // Return the reprojected feature
            return sphericalMercatorFeature;
        }

        /// <summary>
        /// Use the ProjectionConverter to reproject multiple features
        /// </summary>
        private Collection<Feature> ReprojectMultipleFeatures(Collection<Feature> decimalDegreeFeatures)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            Collection<Feature> sphericalMercatorFeatures = projectionConverter.ConvertToExternalProjection(decimalDegreeFeatures);
            projectionConverter.Close();

            // Return the reprojected features
            return sphericalMercatorFeatures;
        }

        /// <summary>
        /// Draw reprojected features on the map
        /// </summary>
        private void ClearMapAndAddFeatures(Collection<Feature> reprojectedFeatures)
        {
            // Get the layer we prepared from the MapView
            InMemoryFeatureLayer reprojectedFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Reprojected Features Layer");

            // Clear old features from the feature layer and add the newly reprojected features
            reprojectedFeatureLayer.InternalFeatures.Clear();
            foreach(Feature sphericalMercatorFeature in reprojectedFeatures)
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
        /// Use the ProjectionConverter class to reproject a single feature
        /// </summary>
        private void ReprojectFeature_Click(object sender, RoutedEventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            Feature decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Convert the feature to Spherical Mercator
            Feature sphericalMercatorFeature = ReprojectFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { sphericalMercatorFeature });
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject multiple different features
        /// </summary>
        private void ReprojectMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            // Create features based on the WKT in the textbox in the UI
            Collection<Feature> decimalDegreeFeatures = new Collection<Feature>();
            string[] wktStrings = txtWKT.Text.Split('\n');
            foreach(string wktString in wktStrings)
            {
                try
                {
                    Feature wktFeature = new Feature(wktString);
                    decimalDegreeFeatures.Add(wktFeature);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // Convert the features to Spherical Mercator
            Collection<Feature> sphericalMercatorFeatures = ReprojectMultipleFeatures(decimalDegreeFeatures);
            
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
