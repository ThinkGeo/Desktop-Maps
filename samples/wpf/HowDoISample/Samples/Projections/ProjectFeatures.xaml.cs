using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Project features into the map's projection - by their source as it reads them, by one ProjectionConverter call, or by the ThinkGeo Cloud.
    /// </summary>
    public partial class ProjectFeatures
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
        private readonly FeatureSourceVectorTileSource _tiles = new FeatureSourceVectorTileSource();
        private readonly DynamicSource _projected = new DynamicSource();
        private readonly ProjectionCloudClient _projectionCloudClient = new ProjectionCloudClient
        {
            ClientId = SampleShared.ClientId,
            ClientSecret = SampleShared.ClientSecret,
        };

        private RectangleShape _subdivisionsBounds;
        private bool _initialized;

        public ProjectFeatures()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The converter belongs to the source, so it applies wherever the source is
            // read - the tile cutter included.
            var subdivisions = new ShapeFileFeatureSource(SampleShared.Shapefile("Subdivisions.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };

            // Nothing is converted by hand: the extent the source reports, like every
            // feature it reads, is already in web mercator. It is read before the tile
            // cutter starts reading the same source on its own threads.
            subdivisions.Open();
            _subdivisionsBounds = subdivisions.GetBoundingBox();
            subdivisions.Close();
            _tiles.FeatureSources.Add("subdivisions", subdivisions);

            await ShowModeAsync();
        }

        private async void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowModeAsync();
        }

        private async void Project_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await ProjectTheWktAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private async Task ShowModeAsync()
        {
            try
            {
                var fromSource = ModeSource.IsChecked == true;
                WktPanel.Visibility = fromSource ? Visibility.Collapsed : Visibility.Visible;

                var style = new MapStyle(ThinkGeoVectorStyles.Dark, _cloud);
                if (fromSource)
                {
                    style.AddStyle((string)Resources["StyleJson"], _tiles);
                }
                else
                {
                    // The star the points are drawn with, registered under the name they ask for.
                    style.Images.Add("star", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
                    style.AddDynamic(_projected);
                }

                if (Map.Basemap == null)
                {
                    Map.Basemap = new GpuBasemap(style);
                }
                else
                {
                    await Map.Basemap.SetStyleAsync(style);
                }

                if (fromSource)
                {
                    await ShowTheSourceProjectedAsItReadsAsync();
                }
                else
                {
                    await ProjectTheWktAsync();
                }
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private async Task ShowTheSourceProjectedAsItReadsAsync()
        {
            Map.CenterPoint = _subdivisionsBounds.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, _subdivisionsBounds, Map.MapWidth, Map.MapHeight) * 1.5;
            await Map.RefreshAsync();
        }

        private static Collection<Feature> ConvertTheGeometryExplicitly(IEnumerable<Feature> decimalDegreeFeatures)
        {
            var projectionConverter = new ProjectionConverter(4326, 3857);
            projectionConverter.Open();
            var sphericalMercatorFeatures = projectionConverter.ConvertToExternalProjection(decimalDegreeFeatures);
            projectionConverter.Close();

            return sphericalMercatorFeatures;
        }

        private async Task<Collection<Feature>> ConvertWithTheThinkGeoCloudAsync(IEnumerable<Feature> decimalDegreeFeatures)
        {
            Status.Text = "asking the ThinkGeo Cloud...";
            var sphericalMercatorFeatures = await _projectionCloudClient.ProjectAsync(decimalDegreeFeatures, 4326, 3857);
            Status.Text = string.Empty;

            return sphericalMercatorFeatures;
        }

        /// <summary>
        /// Reads the WKT in the box, projects it the way the chosen mode does, and draws it.
        /// </summary>
        private async Task ProjectTheWktAsync()
        {
            var decimalDegreeFeatures = new Collection<Feature>();
            var errors = new List<string>();
            foreach (var wkt in TxtWkt.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    decimalDegreeFeatures.Add(new Feature(wkt.Trim()));
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }

            Collection<Feature> sphericalMercatorFeatures;
            try
            {
                sphericalMercatorFeatures = ModeCloud.IsChecked == true
                    ? await ConvertWithTheThinkGeoCloudAsync(decimalDegreeFeatures)
                    : ConvertTheGeometryExplicitly(decimalDegreeFeatures);
            }
            catch (Exception ex)
            {
                Status.Text = "not projected - " + ex.Message;
                return;
            }

            Status.Text = errors.Count == 0
                ? string.Empty
                : errors.Count + " line(s) are not WKT and were skipped: " + string.Join("; ", errors);

            // Each feature is added by its kind - the points as stars, the line as a
            // ribbon, the polygon as a fill - and the Clear and the Adds land as one rebuild.
            _projected.Clear();
            for (var i = 0; i < sphericalMercatorFeatures.Count; i++)
            {
                var id = "feature-" + i;
                switch (sphericalMercatorFeatures[i].GetShape())
                {
                    case AreaBaseShape _:
                        _projected.SetArea(id, sphericalMercatorFeatures[i], new GeoColor(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2f);
                        break;
                    case LineBaseShape _:
                        _projected.SetLine(id, sphericalMercatorFeatures[i], GeoColors.MediumPurple, 6f);
                        break;
                    default:
                        _projected.SetPoint(id, sphericalMercatorFeatures[i], sizeInPixel: 24f, iconName: "star");
                        break;
                }
            }

            if (sphericalMercatorFeatures.Count > 0)
            {
                Map.CenterPoint = MapUtil.GetBoundingBoxOfItems(sphericalMercatorFeatures).GetCenterPoint();
                Map.CurrentScale = new ZoomLevelSet().ZoomLevel18.Scale;
            }

            await Map.RefreshAsync();
        }
    }
}
