using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Project features into the map's projection: convert them with a ProjectionConverter, let a layer's FeatureSource project them as it reads, or ask the ThinkGeo Cloud.
    /// </summary>
    public partial class ProjectFeatures : IDisposable
    {
        private bool _initialized;
        private string _mode = "convert";
        private ShapeFileFeatureLayer _subdivisionsLayer;
        private InMemoryFeatureLayer _projectedFeaturesLayer;
        private LayerOverlay _subdivisionsOverlay;
        private LayerOverlay _projectedFeaturesOverlay;
        private ProjectionCloudClient _projectionCloudClient;

        public ProjectFeatures()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _subdivisionsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Subdivisions.shp"));
            _subdivisionsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _subdivisionsOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _subdivisionsOverlay.Layers.Add(_subdivisionsLayer);
            Map.Overlays.Add(_subdivisionsOverlay);

            _projectedFeaturesLayer = new InMemoryFeatureLayer();
            _projectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            _projectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            _projectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _projectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _projectedFeaturesOverlay = new LayerOverlay();
            _projectedFeaturesOverlay.Layers.Add(_projectedFeaturesLayer);
            Map.Overlays.Add(_projectedFeaturesOverlay);

            _projectionCloudClient = new ProjectionCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            _ = RunModeAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _mode = (string)((RadioButton)sender).Tag;
            _ = RunModeAsync();
        }

        private void Project_Click(object sender, RoutedEventArgs e)
        {
            _ = RunModeAsync();
        }

        private async Task RunModeAsync()
        {
            var layerMode = _mode == "layer";
            _subdivisionsOverlay.IsVisible = layerMode;
            _projectedFeaturesOverlay.IsVisible = !layerMode;
            LayerInfo.Visibility = layerMode ? Visibility.Visible : Visibility.Collapsed;
            WktPanel.Visibility = layerMode ? Visibility.Collapsed : Visibility.Visible;
            Status.Text = string.Empty;

            try
            {
                if (layerMode)
                {
                    await SetTheProjectionOfALayerAsync();
                }
                else
                {
                    await ProjectTheWktAsync();
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async Task SetTheProjectionOfALayerAsync()
        {
            _subdivisionsLayer.Open();
            var subdivisionsBBox = _subdivisionsLayer.GetBoundingBox();
            _subdivisionsLayer.Close();

            Map.CenterPoint = subdivisionsBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, subdivisionsBBox, Map.MapWidth, Map.MapHeight) * 1.5;
            await Map.RefreshAsync();
        }

        private async Task ProjectTheWktAsync()
        {
            var decimalDegreeFeatures = new Collection<Feature>();
            foreach (var wkt in TxtWkt.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    decimalDegreeFeatures.Add(new Feature(wkt.Trim()));
                }
                catch (Exception)
                {
                    Status.Text = "Skipped a line that is not WKT: " + wkt;
                }
            }

            Collection<Feature> sphericalMercatorFeatures;
            if (_mode == "cloud")
            {
                var skipped = Status.Text;
                Status.Text = "Asking the ThinkGeo Cloud...";
                sphericalMercatorFeatures = await _projectionCloudClient.ProjectAsync(decimalDegreeFeatures, 4326, 3857);
                Status.Text = skipped;
            }
            else
            {
                sphericalMercatorFeatures = ConvertWithProjectionConverter(decimalDegreeFeatures);
            }

            _projectedFeaturesLayer.InternalFeatures.Clear();
            foreach (var feature in sphericalMercatorFeatures)
            {
                _projectedFeaturesLayer.InternalFeatures.Add(feature);
            }

            if (sphericalMercatorFeatures.Count > 0)
            {
                Map.CenterPoint = MapUtil.GetBoundingBoxOfItems(sphericalMercatorFeatures).GetCenterPoint();
                Map.CurrentScale = new ZoomLevelSet().ZoomLevel18.Scale;
            }
            await Map.RefreshAsync();
        }

        private static Collection<Feature> ConvertWithProjectionConverter(IEnumerable<Feature> decimalDegreeFeatures)
        {
            var projectionConverter = new ProjectionConverter(4326, 3857);
            projectionConverter.Open();
            var sphericalMercatorFeatures = projectionConverter.ConvertToExternalProjection(decimalDegreeFeatures);
            projectionConverter.Close();

            return sphericalMercatorFeatures;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
