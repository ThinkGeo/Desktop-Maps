using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for XYZRasterLayerProjection.xaml
    /// </summary>
    public partial class XYZRasterLayerProjection : UserControl
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private static readonly RectangleShape OriginalExtent3857 = new RectangleShape(166021, 9328006, 833978, 0);
        private int _logIndex;
        private bool _initialized;

        public XYZRasterLayerProjection()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
            };
            layerOverlay.Layers.Add(_thinkGeoRasterMapsAsyncLayer);

            string cacheBasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "projected_layer");

            if (!System.IO.Directory.Exists(cacheBasePath))
            {
                System.IO.Directory.CreateDirectory(cacheBasePath);
            }

            _thinkGeoRasterMapsAsyncLayer.TileCache = new FileRasterTileCache(cacheBasePath, "Raw3857");
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cacheBasePath, "projected");

            _thinkGeoRasterMapsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            MapView.MapUnit = GeographyUnit.Meter;
            _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;

            await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
            await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

            MapView.CenterPoint = OriginalExtent3857.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, OriginalExtent3857, MapView.MapWidth, MapView.MapHeight);

            _initialized = true;
            await MapView.RefreshAsync();
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit: " : "Projection Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }

        private void TileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit: " : "Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            });
        }

        // Mapping projection tags to either EPSG codes or custom ProjStrings
        private static readonly Dictionary<string, object> ProjectionMap = new Dictionary<string, object>()
        {
            { "3857", 3857 },
            { "4326", 4326 },
            { "25832", 25832 },
            { "Polar", SampleKeys.ProjString3 },
            { "Albers", SampleKeys.ProjString2 }
        };

        private async Task ApplyProjectionDynamicAsync(string projectionTag)
        {
            if (!ProjectionMap.TryGetValue(projectionTag, out var targetProj)) return;

            RectangleShape targetExtent = OriginalExtent3857; // always start from OriginalExtent3857
            GdalProjectionConverter converter = null;
            GeographyUnit mapUnit = GeographyUnit.Meter; // default

            if (projectionTag == "4326")
                mapUnit = GeographyUnit.DecimalDegree;

            // If target projection is not 3857, create converter
            if (!projectionTag.Equals("3857"))
            {
                if (targetProj is int targetEPSG)
                    converter = new GdalProjectionConverter(3857, targetEPSG);
                else if (targetProj is string targetProjString)
                    converter = new GdalProjectionConverter(3857, targetProjString);

                converter.Open();
                targetExtent = (RectangleShape)converter.ConvertToExternalProjection(OriginalExtent3857);
                converter.Close();
            }

            MapView.MapUnit = mapUnit;
            await ApplyProjectionAsync(projectionTag, mapUnit, converter, targetExtent);
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized || _thinkGeoRasterMapsAsyncLayer == null) return;
            if (!(sender is RadioButton radioButton) || radioButton.Tag == null) return;

            try
            {
                string projectionTag = radioButton.Tag.ToString();
                SetTileCachesForProjection(projectionTag);

                await ApplyProjectionDynamicAsync(projectionTag);

                await MapView.RefreshAsync();
            }
            catch
            {
                // handle/log exceptions
            }
        }

        private async Task ApplyProjectionAsync(string projectionTag, GeographyUnit mapUnit, ProjectionConverter projectionConverter,
                                                RectangleShape extent, bool useLayerBBox = false, PointShape fixedCenter = null, double fixedScale = 0)
        {
            if (MapView == null || _thinkGeoRasterMapsAsyncLayer == null) return;

            MapView.MapUnit = mapUnit;
            _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = projectionConverter;

            foreach (var overlay in MapView.Overlays.OfType<LayerOverlay>())
            {
                foreach (var layer in overlay.Layers.OfType<FeatureLayer>())
                {
                    layer.FeatureSource.ProjectionConverter = projectionConverter;
                }

                // Optional: clear tile cache so no old projection tiles get reused
                overlay.TileCache?.ClearCache();
            }

            if (projectionConverter is GdalProjectionConverter gdal)
            {
                gdal.Open();
            }

            await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
            await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

            if (fixedCenter != null && fixedScale > 0)
            {
                MapView.CenterPoint = fixedCenter;
                MapView.CurrentScale = fixedScale;
            }
            else if (useLayerBBox)
            {
                var bbox = _thinkGeoRasterMapsAsyncLayer.GetBoundingBox();
                MapView.CenterPoint = bbox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, bbox, MapView.MapWidth, MapView.MapHeight);
            }
            else if (extent != null)
            {
                MapView.CenterPoint = extent.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, extent, MapView.MapWidth, MapView.MapHeight);
            }
        }

        private void SetTileCachesForProjection(string projectionTag)
        {
            string cacheBasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "projected_layer");

            if (!System.IO.Directory.Exists(cacheBasePath))
            {
                System.IO.Directory.CreateDirectory(cacheBasePath);
            }

            // Create raw cache
            var rawCache = new FileRasterTileCache(cacheBasePath)
            {
                CacheId = "Raw3857"
            };

            // Create projected cache
            var projectedCache = new FileRasterTileCache(cacheBasePath)
            {
                CacheId = projectionTag
            };

            _thinkGeoRasterMapsAsyncLayer.TileCache = rawCache;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache = projectedCache;

            _thinkGeoRasterMapsAsyncLayer.TileCache.GottenTile -= TileCache_GottenCacheTile;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenTile -= ProjectedTileCache_GottenCacheTile;

            _thinkGeoRasterMapsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;
        }

        private void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            if (checkBox.IsChecked.HasValue)
                _thinkGeoRasterMapsAsyncLayer.RenderBeyondMaxZoom = checkBox.IsChecked.Value;

            _ = MapView.RefreshAsync();
        }

        private void DisplayTileIdCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            if (!(sender is CheckBox checkBox))
                return;

            if (!checkBox.IsChecked.HasValue)
                return;

            ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked ?? false;
            _ = MapView.RefreshAsync();
        }

        public void AppendLog(string message)
        {
            // Add log message to the observable collection
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
