using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Learn how to reproject features using the ProjectionConverter class
    /// </summary>
    public partial class ProjectLatLonCoordinatesSample : IDisposable
    {
        public ProjectLatLonCoordinatesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the shapes we will be reprojecting
            var reprojectedFeaturesLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to an overlay
            var reprojectedFeaturesOverlay = new LayerOverlay();
            reprojectedFeaturesOverlay.Layers.Add("Reprojected Features Layer", reprojectedFeaturesLayer);

            // Add the overlay to the map
            MapView.Overlays.Add("Reprojected Features Overlay", reprojectedFeaturesOverlay);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10779751.80, 3915369.33, -10779407.60, 3915141.57);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Use the ProjectionConverter to reproject a single feature
        /// </summary>
        private static Feature ReprojectFeature(Feature decimalDegreeFeature)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            var sphericalMercatorFeature = projectionConverter.ConvertToExternalProjection(decimalDegreeFeature);
            projectionConverter.Close();

            // Return the reprojected feature
            return sphericalMercatorFeature;
        }

        /// <summary>
        /// Use the ProjectionConverter to reproject multiple features
        /// </summary>
        private static Collection<Feature> ReprojectMultipleFeatures(IEnumerable<Feature> decimalDegreeFeatures)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            var sphericalMercatorFeatures = projectionConverter.ConvertToExternalProjection(decimalDegreeFeatures);
            projectionConverter.Close();

            // Return the reprojected features
            return sphericalMercatorFeatures;
        }

        /// <summary>
        /// Draw reprojected features on the map
        /// </summary>
        private async Task ClearMapAndAddFeaturesAsync(Collection<Feature> reprojectedFeatures)
        {
            // Get the layer we prepared from the MapView
            var reprojectedFeatureLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Reprojected Features Layer");

            // Clear old features from the feature layer and add the newly reprojected features
            reprojectedFeatureLayer.InternalFeatures.Clear();
            foreach (var sphericalMercatorFeature in reprojectedFeatures)
            {
                reprojectedFeatureLayer.InternalFeatures.Add(sphericalMercatorFeature);
            }

            // Set the map extent to zoom into the feature and refresh the map
            reprojectedFeatureLayer.Open();
            MapView.CurrentExtent = reprojectedFeatureLayer.GetBoundingBox();

            var standardZoomLevelSet = new ZoomLevelSet();
            await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);

            reprojectedFeatureLayer.Close();

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject a single feature
        /// </summary>
        private async void ReprojectFeature_Click(object sender, RoutedEventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            var decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Convert the feature to Spherical Mercator
            var sphericalMercatorFeature = ReprojectFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { sphericalMercatorFeature });
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject multiple different features
        /// </summary>
        private async void ReprojectMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            // Create features based on the WKT in the textbox in the UI
            var decimalDegreeFeatures = new Collection<Feature>();
            var wktStrings = txtWKT.Text.Split('\n');
            foreach (var wktString in wktStrings)
            {
                try
                {
                    var wktFeature = new Feature(wktString);
                    decimalDegreeFeatures.Add(wktFeature);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // Convert the features to Spherical Mercator
            var sphericalMercatorFeatures = ReprojectMultipleFeatures(decimalDegreeFeatures);

            // Add the reprojected features to the map
            await ClearMapAndAddFeaturesAsync(sphericalMercatorFeatures);
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