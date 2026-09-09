using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to reproject features using the ProjectionConverter class
    /// </summary>
    public partial class ProjectFeatures
    {

        private bool _initialized;
        private readonly InMemoryGeometrySource _reprojected = new InMemoryGeometrySource();

        public ProjectFeatures()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The reprojected shapes go to the GPU with the basemap. Drawn on an overlay
            // they would be raster tiles, so they would stretch through a fractional
            // zoom while the map beneath them stayed sharp.
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));

            // The GPU draws a marker from a named image, so a point style the GPU has no
            // shape of its own for - a star - is drawn once by the CPU and registered.
            style.Images.Add("star", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            style.AddGeometry(_reprojected, new GeometryStyle
            {
                FillColor = new GeoColor(80, GeoColors.MediumPurple),
                OutlineColor = GeoColors.MediumPurple,
                OutlineWidthInPixels = 2,
                LineColor = GeoColors.MediumPurple,
                LineWidthInPixels = 6,
            });
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10779580, 3915255);
            Map.CurrentScale = 1570;

            _ = Map.RefreshAsync();
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
            // The same collection three times: each call takes the shapes it draws, so
            // the points, the line and the polygon all arrive without being sorted first.
            _reprojected.UpdateAreas(reprojectedFeatures);
            _reprojected.UpdateLines(reprojectedFeatures);
            _reprojected.UpdatePoints(reprojectedFeatures, sizeInPixels: 24f, iconName: "star");

            var reprojectedFeaturesBoundingBox = MapUtil.GetBoundingBoxOfItems(reprojectedFeatures);
            Map.CenterPoint = reprojectedFeaturesBoundingBox.GetCenterPoint();

            var standardZoomLevelSet = new ZoomLevelSet();
            await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject a single feature
        /// </summary>
        private void ReprojectFeature_Click(object sender, RoutedEventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            var decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Convert the feature to Spherical Mercator
            var sphericalMercatorFeature = ReprojectFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            _ = ClearMapAndAddFeaturesAsync(new Collection<Feature>() { sphericalMercatorFeature });
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject multiple different features
        /// </summary>
        private void ReprojectMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            // Create features based on the WKT in the textbox in the UI
            var decimalDegreeFeatures = new Collection<Feature>();
            var wktStrings = TxtWkt.Text.Split('\n');
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
            _ = ClearMapAndAddFeaturesAsync(sphericalMercatorFeatures);
        }
    }
}