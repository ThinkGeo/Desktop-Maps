using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Demonstrates XYZ raster layer reprojection with
    /// a fixed CenterPoint and CurrentScale per projection
    /// to ensure stable and predictable demo behavior.
    /// </summary>
    public partial class ProjectTheWorldRaster : UserControl
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private int _logIndex;
        private bool _initialized;

        // Original Web Mercator demo extent (EPSG:3857)
        private static readonly RectangleShape OriginalExtent3857 = new RectangleShape(166021, 9328006, 833978, 0);

        // Holds a stable demo view configuration for a projection.
        private class ProjectionView
        {
            public GeographyUnit MapUnit { get; set; }
            public PointShape Center { get; set; }
            public double Scale { get; set; }
        }

        // Each projection has a fixed CenterPoint and Scale.
        // Adjust ONLY these values to fine-tune demo appearance.
        private static readonly Dictionary<string, ProjectionView> ProjectionViews
            = new Dictionary<string, ProjectionView>
        {
            {
                "3857",
                new ProjectionView
                {
                    MapUnit = GeographyUnit.Meter,
                    Center = OriginalExtent3857.GetCenterPoint(),
                    Scale = MapUtil.GetScale(GeographyUnit.Meter,OriginalExtent3857,800, 600)
                }
            },

            {
                "4326",
                new ProjectionView
                {
                    MapUnit = GeographyUnit.DecimalDegree,
                    Center = new PointShape(0.6, 33.8),
                    Scale = 40000000
                }
            },

            {
                "Polar",
                new ProjectionView
                {
                    MapUnit = GeographyUnit.Meter,
                    Center = new PointShape(7273160, -3414200),
                    Scale = 36978700
                }
            },

            {
                "Albers",
                new ProjectionView
                {
                    MapUnit = GeographyUnit.Meter,
                    Center = new PointShape(8968290, 5188500),
                    Scale = 18489340
                }
            }
        };


        // Mapping projection tags to EPSG codes or Proj strings
        private static readonly Dictionary<string, object> ProjectionMap
            = new Dictionary<string, object>
        {
            { "3857", 3857 },
            { "4326", 4326 },
            { "Polar", SampleKeys.ProjString3 },
            { "Albers", SampleKeys.ProjString2 }
        };

        public ProjectTheWorldRaster()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void MapView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
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

            await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

            MapView.CenterPoint = OriginalExtent3857.GetCenterPoint();
            MapView.CurrentScale = ProjectionViews["3857"].Scale;

            _initialized = true;
            await MapView.RefreshAsync();
        }

        private async Task ApplyProjectionDynamicAsync(string projectionTag)
        {
            if (!_initialized) return;
            if (!ProjectionMap.TryGetValue(projectionTag, out var proj)) return;
            if (!ProjectionViews.TryGetValue(projectionTag, out var view)) return;

            GdalProjectionConverter converter = null;

            // Create projection converter (3857 -> target)
            if (projectionTag != "3857")
            {
                if (proj is int epsg)
                    converter = new GdalProjectionConverter(3857, epsg);
                else if (proj is string projString)
                    converter = new GdalProjectionConverter(3857, projString);
            }

            await ApplyProjectionAsync(projectionTag,view.MapUnit,converter,fixedCenter: view.Center,fixedScale: view.Scale);
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

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            if (sender is RadioButton rb && rb.Tag != null)
            {
                SetTileCachesForProjection(rb.Tag.ToString());
                await ApplyProjectionDynamicAsync(rb.Tag.ToString());
            }
        }

        private async Task ApplyProjectionAsync(string projectionTag, GeographyUnit mapUnit, ProjectionConverter projectionConverter, PointShape fixedCenter, double fixedScale)
        {
            MapView.MapUnit = mapUnit;
            _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = projectionConverter;

            if (projectionConverter is GdalProjectionConverter gdal)
                gdal.Open();

            await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
            await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

            MapView.CenterPoint = fixedCenter;
            MapView.CurrentScale = fixedScale;

            await MapView.RefreshAsync();
        }

        private void SetTileCachesForProjection(string projectionTag)
        {
            // IMPORTANT: Guard against early calls before layer initialization
            if (!_initialized || _thinkGeoRasterMapsAsyncLayer == null)
                return;

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
