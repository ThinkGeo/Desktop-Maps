using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayRasterMBTilesFile.xaml
    /// </summary>
    public partial class DisplayRasterMBTilesFile : IDisposable
    {
        private LayerOverlay layerOverlay;
        private RasterMbTilesLayer rasterMbTilesLayer = new RasterMbTilesLayer(@".\Data\Mbtiles\test.mbtiles");
        private bool isMapViewLoaded = false;
        
        public DisplayRasterMBTilesFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            isMapViewLoaded = false;

            layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);
            layerOverlay.Layers.Add(rasterMbTilesLayer);
            await MapView.RefreshAsync();

            isMapViewLoaded = true;
        }

        private async void TileType_Checked(object sender, RoutedEventArgs e)
        {
            if (layerOverlay == null) return;

            var radioButton = sender as RadioButton;
            if (radioButton?.Tag == null) return;

            switch (radioButton.Tag.ToString())
            {
                case "SingleTile":
                    layerOverlay.TileType = TileType.SingleTile;
                    break;
                case "MultiTile":
                    layerOverlay.TileType = TileType.MultiTile;
                    break;
                default:
                    return;
            }

            await MapView.RefreshAsync();
        }

        private async void TileCache_Checked(object sender, RoutedEventArgs e)
        {
            if (layerOverlay == null) return;

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "test");
            
            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            var radioButton = sender as RadioButton;
            if (radioButton?.Tag == null) return;

            switch (radioButton.Tag.ToString())
            {
                case "Raw":
                    rasterMbTilesLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                    break;
                case "Projected":
                    rasterMbTilesLayer.TileCache = new FileRasterTileCache(cachePath, "projected");
                    break;
                default:
                    return;
            }

            await MapView.RefreshAsync();
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!isMapViewLoaded || rasterMbTilesLayer == null) return;

            var radioButton = sender as RadioButton;
            if (radioButton?.Tag == null) return;

            switch (radioButton.Tag.ToString())
            {
                case "3857":
                    MapView.MapUnit = GeographyUnit.Meter;
                    rasterMbTilesLayer.ProjectionConverter = null;
                    break;

                case "4326":
                    MapView.MapUnit = GeographyUnit.DecimalDegree;
                    var projectionConverter = new GdalProjectionConverter(3857, 4326);
                    projectionConverter.Open(); 
                    rasterMbTilesLayer.ProjectionConverter = projectionConverter;
                    break;

                default:
                    return;
            }

            layerOverlay.Layers.Clear();
            layerOverlay.Layers.Add(rasterMbTilesLayer);
            rasterMbTilesLayer.Open();
            MapView.CurrentExtent = rasterMbTilesLayer.GetBoundingBox();

            if (radioButton.Tag.ToString() == "4326")
            {
                await MapView.ZoomInAsync();
            }

            await MapView.RefreshAsync();
        }

        private void RenderBeyondMaxZoomCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (rasterMbTilesLayer != null)
            {
                rasterMbTilesLayer.RenderBeyondMaxZoom = true;
            }
        }

        private void RenderBeyondMaxZoomCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (rasterMbTilesLayer != null)
            {
                rasterMbTilesLayer.RenderBeyondMaxZoom = false;
            }
        }

        private async void TransparencyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (rasterMbTilesLayer != null)
            {
                rasterMbTilesLayer.Transparency = (int)Transparency.Value;
                await MapView.RefreshAsync();
            }
        }

        private async void TransparencyCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (rasterMbTilesLayer != null)
            {
                rasterMbTilesLayer.Transparency = 255;
                await MapView.RefreshAsync();
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
