using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the ProjectionCloudClient to access the Projection APIs available from the ThinkGeo Cloud
    /// </summary>
    public partial class ProjectionCloudServices
    {

        private bool _initialized;
        private ProjectionCloudClient _projectionCloudClient;
        private readonly InMemoryGeometrySource _reprojected = new InMemoryGeometrySource();

        public ProjectionCloudServices()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer for the reprojected features
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

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            // Initialize the ProjectionCloudClient with our ThinkGeo Cloud credentials
            _projectionCloudClient = new ProjectionCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject a single feature
        /// </summary>
        private async Task<Feature> ReprojectAFeature(Feature decimalDegreeFeature)
        {
            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            var reprojectedFeature = await _projectionCloudClient.ProjectAsync(decimalDegreeFeature, 4326, 3857);

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeature;
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject multiple features
        /// </summary>
        private async Task<Collection<Feature>> ReprojectMultipleFeatures(Collection<Feature> decimalDegreeFeatures)
        {
            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            var reprojectedFeatures = new Collection<Feature>();

            foreach (var feature in decimalDegreeFeatures)
            {
                var reprojectedFeature = await _projectionCloudClient.ProjectAsync(feature, 4326, 3857);
                reprojectedFeatures.Add(reprojectedFeature);
            }

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeatures;
        }

        /// <summary>
        /// Draw reprojected features on the map
        /// </summary>
        private async Task ClearMapAndAddFeaturesAsync(Collection<Feature> features)
        {
            // The same collection three times: each call takes the shapes it draws, so
            // the points, the lines and the polygons all arrive without being sorted first.
            _reprojected.UpdateAreas(features);
            _reprojected.UpdateLines(features);
            _reprojected.UpdatePoints(features, sizeInPixels: 24f, iconName: "star");

            Map.CenterPoint = MapUtil.GetBoundingBoxOfItems(features).GetCenterPoint();

            var standardZoomLevelSet = new ZoomLevelSet();
            await Map.ZoomToAsync(standardZoomLevelSet.ZoomLevel18.Scale);
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject a single feature
        /// </summary>
        private async void ReprojectFeature_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a feature with coordinates in Decimal Degrees (4326)
                var decimalDegreeFeature = new Feature(-96.834516, 33.150083);

                // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
                var sphericalMercatorFeature = await ReprojectAFeature(decimalDegreeFeature);

                // Add the reprojected features to the map
                await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { sphericalMercatorFeature });
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject multiple different features
        /// </summary>
        private async void ReprojectMultipleFeatures_Click(object sender, RoutedEventArgs e)
        {
            try
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

                // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
                var sphericalMercatorFeatures = await ReprojectMultipleFeatures(decimalDegreeFeatures);

                // Add the reprojected features to the map
                await ClearMapAndAddFeaturesAsync(sphericalMercatorFeatures);
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }
    }
}