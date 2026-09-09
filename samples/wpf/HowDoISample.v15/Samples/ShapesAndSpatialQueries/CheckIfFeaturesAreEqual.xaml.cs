using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer are topologically equal to a shape
    /// </summary>
    public partial class CheckIfFeaturesAreEqual
    {

        private bool _initialized;

        // Nothing here is drawn from a layer, so nothing here needs one: a query runs on
        // a FeatureSource, and QueryTools takes one directly.
        private InMemoryFeatureSource _zoningSource;
        private readonly InMemoryGeometrySource _zoning = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _queryShape = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _highlighted = new InMemoryGeometrySource();

        public CheckIfFeaturesAreEqual()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco zoning data
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            try
            {
                // Set the Map Unit to meters (used in Spherical Mercator)
                Map.MapUnit = GeographyUnit.Meter;

                // The zoning polygons, the query shape and the matches all go to the GPU
                // with the basemap. Drawn on overlays they would be raster tiles, so they
                // would stretch through a fractional zoom while the map under them stayed
                // sharp.
                var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
                style.AddGeometry(_zoning, new GeometryStyle { FillColor = new GeoColor(50, GeoColors.MediumPurple), OutlineColor = GeoColors.MediumPurple, OutlineWidthInPixels = 2 });
                style.AddGeometry(_queryShape, new GeometryStyle { FillColor = new GeoColor(75, GeoColors.LightRed), OutlineColor = GeoColors.LightRed, OutlineWidthInPixels = 2 });
                style.AddGeometry(_highlighted, new GeometryStyle { FillColor = new GeoColor(90, GeoColors.MidnightBlue), OutlineColor = GeoColors.MidnightBlue, OutlineWidthInPixels = 2 });
                Map.Basemap = new GpuBasemap(style);

                // Import the features from the Frisco zoning data shapefile
                var zoningDataFeatureSource = new ShapeFileFeatureSource(@"./Data/Shapefile/Zoning.shp");

                // Create a ProjectionConverter to convert the shapefile data from North Central Texas (2276) to Spherical Mercator (3857)
                var projectionConverter = new ProjectionConverter(3857, 2276);

                // For this sample, we have to reproject the features before adding them to the feature layer
                // This is because the topological equality query often does not work when used on a feature layer with a ProjectionConverter, due to rounding issues between projections
                zoningDataFeatureSource.Open();
                projectionConverter.Open();
                var reprojectedFeatures = zoningDataFeatureSource
                    .GetAllFeatures(ReturningColumnsType.AllColumns)
                    .Select(zoningFeature => projectionConverter.ConvertToInternalProjection(zoningFeature))
                    .ToList();
                var columns = zoningDataFeatureSource.GetColumns();
                zoningDataFeatureSource.Close();
                projectionConverter.Close();

                // The reprojected features, held where the query can reach them
                _zoningSource = new InMemoryFeatureSource(columns, reprojectedFeatures);
                _zoning.UpdateAreas(reprojectedFeatures);

                // Add a sample shape to the map for the initial query
                // To ensure topological equality for this sample, we create a new shape using the same geometry as an existing feature
                var sampleShape = reprojectedFeatures.First().GetShape();
                await GetFeaturesEqualAsync(sampleShape);

                Map.CenterPoint = new PointShape(-10776520,3919250);
                Map.CurrentScale = 18060;

                await Map.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Perform the 'Equals' spatial query. QueryTools takes a FeatureSource, so the
        /// query needs no layer - a layer is for drawing, and nothing here is drawn from
        /// one.
        /// </summary>
        private static IEnumerable<Feature> PerformSpatialQuery(BaseShape shape, FeatureSource source)
        {
            source.Open();
            var features = new QueryTools(source).GetFeaturesTopologicalEqual(shape, ReturningColumnsType.AllColumns);
            source.Close();

            return features;
        }

        /// <summary>
        /// Highlight the features that were found by the spatial query
        /// </summary>
        private async Task HighlightQueriedFeaturesAsync(IEnumerable<Feature> features)
        {
            // Replace whatever was highlighted with the features just found
            var enumerable = features as Feature[] ?? features.ToArray();
            _highlighted.UpdateAreas(enumerable);
            await Map.RefreshAsync();

            // Update the number of matching features found in the UI
            TxtNumberOfFeaturesFound.Text =
                $"Number of features topologically equal to the drawn shape: {enumerable.Length}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesEqualAsync(BaseShape shape)
        {
            // Show the shape being queried with
            _queryShape.UpdateAreas(new[] { shape });

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(shape, _zoningSource);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }
    }
}