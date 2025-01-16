using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for RasterMBTiles.xaml
    /// </summary>
    public partial class RasterXYZServer : IDisposable
    {
        private ThinkGeoRasterMapsAsyncLayer thinkGeoRasterMapsAsyncLayer;
        private OpenStreetMapAsyncLayer openStreetMapAsyncLayer;
        private LayerOverlay layerOverlay;
        private string currentProjection = "3857"; // Default to 3857

        public RasterXYZServer()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);

                // Add Cloud Maps as a background overlay
                thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
                };

                await LoadThinkGeoRasterMapsAsyncLayer();

                layerOverlay.Drawn += LayerOverlayOnDrawn;
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async Task LoadThinkGeoRasterMapsAsyncLayer()
        {
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(thinkGeoRasterMapsAsyncLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "thinkGeoRasterMapsOnlineLayerCache");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            thinkGeoRasterMapsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            thinkGeoRasterMapsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected")
            { EnableDebugInfo = true };

            thinkGeoRasterMapsAsyncLayer.TileCache.GottenCacheTile += TileCache_GottenCacheTile;
            thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenCacheTile += ProjectedTileCache_GottenCacheTile;
            await thinkGeoRasterMapsAsyncLayer.CloseAsync();
            await thinkGeoRasterMapsAsyncLayer.OpenAsync();
        }

        private async Task LoadOpenStreetMapAsyncLayer()
        {
            layerOverlay.Layers.Add(openStreetMapAsyncLayer);

            string openStreetMapCachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "openStreetMapOnlineLayerCache");

            if (!System.IO.Directory.Exists(openStreetMapCachePath))
            {
                System.IO.Directory.CreateDirectory(openStreetMapCachePath);
            }

            openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(openStreetMapCachePath, "raw");
            openStreetMapAsyncLayer.ProjectedTileCache = new FileRasterTileCache(openStreetMapCachePath, "projected")
            { EnableDebugInfo = true };

            openStreetMapAsyncLayer.TileCache.GottenCacheTile += TileCache_GottenCacheTile;
            openStreetMapAsyncLayer.ProjectedTileCache.GottenCacheTile += ProjectedTileCache_GottenCacheTile;
            await openStreetMapAsyncLayer.CloseAsync();
            await openStreetMapAsyncLayer.OpenAsync();
        }

        private void LayerOverlayOnDrawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                LogTextBox.ScrollToEnd();
            });
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit:" : "Projection Cache Hit:";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

                LogTextBox.Text += message + Environment.NewLine;
            });
        }

        private void TileCache_GottenCacheTile(object sender, GottenCacheImageBitmapTileCacheEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit:" : "Cache Hit:";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.Column}-{e.Tile.Row}";

                LogTextBox.Text += message + Environment.NewLine;
            });
        }


        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (layerOverlay == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                var selectedProjection = radioButton.Tag.ToString();
                LayerBase activeLayer = layerOverlay.Layers.Count > 0 ? layerOverlay.Layers[layerOverlay.Layers.Count - 1] : null;

                if (activeLayer is ThinkGeoRasterMapsAsyncLayer rasterLayer)
                {
                    switch (selectedProjection)
                    {
                        case "3857":
                            MapView.MapUnit = GeographyUnit.Meter;
                            thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;
                            currentProjection = "3857";
                            break;

                        case "4326":
                            MapView.MapUnit = GeographyUnit.DecimalDegree;
                            thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                            currentProjection = "4326";
                            break;

                        default:
                            return;
                    }

                    await thinkGeoRasterMapsAsyncLayer.CloseAsync();
                    await thinkGeoRasterMapsAsyncLayer.OpenAsync();
                    MapView.CurrentExtent = thinkGeoRasterMapsAsyncLayer.GetBoundingBox();
                }
                else if (activeLayer is OpenStreetMapAsyncLayer osmLayer)
                {
                    switch (selectedProjection)
                    {
                        case "3857":
                            MapView.MapUnit = GeographyUnit.Meter;
                            osmLayer.ProjectionConverter = null;
                            currentProjection = "3857";
                            break;

                        case "4326":
                            MapView.MapUnit = GeographyUnit.DecimalDegree;
                            osmLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                            currentProjection = "4326";
                            break;

                        default:
                            return;
                    }

                    await osmLayer.CloseAsync();
                    await osmLayer.OpenAsync();
                    MapView.CurrentExtent = osmLayer.GetBoundingBox();
                }

                await MapView.RefreshAsync();

            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void Server_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (layerOverlay == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                // Remove all layers from the overlay before adding the new layer
                layerOverlay.Layers.Clear();

                if (currentProjection == "3857")
                {
                    switch (radioButton.Tag.ToString())
                    {
                        case "ThinkGeoRasterMapsAsyncLayer":
                            thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
                            {
                                ClientId = SampleKeys.ClientId,
                                ClientSecret = SampleKeys.ClientSecret,
                                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
                            };
                            await LoadThinkGeoRasterMapsAsyncLayer();
                            break;

                        case "OpenStreetMapAsyncLayer":
                            openStreetMapAsyncLayer = new OpenStreetMapAsyncLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
                            await LoadOpenStreetMapAsyncLayer();
                            break;

                        default:
                            return;
                    }

                }
                else if (currentProjection == "4326")
                {
                    switch (radioButton.Tag.ToString())
                    {
                        case "ThinkGeoRasterMapsAsyncLayer":
                            MapView.MapUnit = GeographyUnit.DecimalDegree;
                            thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
                            {
                                ClientId = SampleKeys.ClientId,
                                ClientSecret = SampleKeys.ClientSecret,
                                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
                            };
                            thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                            await LoadThinkGeoRasterMapsAsyncLayer();
                            break;

                        case "OpenStreetMapAsyncLayer":
                            MapView.MapUnit = GeographyUnit.DecimalDegree;
                            openStreetMapAsyncLayer = new OpenStreetMapAsyncLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
                            openStreetMapAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                            await LoadOpenStreetMapAsyncLayer();
                            break;

                        default:
                            return;
                    }
                }

                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
