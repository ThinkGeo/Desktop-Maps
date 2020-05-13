using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace PerformanceSample
{
    internal class CustomLayerOverlay : LayerOverlay
    {
        public GeoImage BackgroundImage { get; set; }

        public CustomLayerOverlay(GeoImage backgroundImage = null)
        {
            BackgroundImage = backgroundImage;
            if (BackgroundImage != null)
            {
                // set the background image as a control of this overlay
                var wpfBitmap = new BitmapImage();
                wpfBitmap.BeginInit();
                wpfBitmap.StreamSource = BackgroundImage.GetImageStream();
                wpfBitmap.EndInit();

                var imageControl = new Image()
                {
                    Source = wpfBitmap
                };
                imageControl.SetValue(Panel.ZIndexProperty, -100);
                OverlayCanvas.Children.Add(imageControl);
            }
        }

        public GeoImage ToGeoImage(RectangleShape worldExtent, GeographyUnit drawingMapUnit)
        {
            // only support SingleTile mode, otherwise return the background image directly
            if (TileType != TileType.SingleTile)
            {
                if (BackgroundImage == null)
                {
                    return null;
                }
                else
                {
                    return new GeoImage(BackgroundImage.GetImageStream());
                }
            }

            // find any layer tiles in this overlay
            var layerTiles = new List<LayerTile>();
            foreach (var overlayUIElement in OverlayCanvas.Children)
            {
                if (overlayUIElement is Canvas)
                {
                    foreach (var canvasUIElement in (overlayUIElement as Canvas).Children)
                    {
                        if (canvasUIElement is LayerTile)
                        {
                            layerTiles.Add(canvasUIElement as LayerTile);
                        }
                    }
                }
            }

            GeoImage geoImage = null;
            if (layerTiles.Count > 0)
            {
                int width = Convert.ToInt32(layerTiles.First().ActualWidth);
                int height = Convert.ToInt32(layerTiles.First().ActualHeight);
                if (width > 0 && height > 0)
                {
                    // initialize geoCanvas by using current BackgroundImage
                    var geoCanvas = new PlatformGeoCanvas();
                    if (BackgroundImage == null)
                    {
                        geoImage = new GeoImage(width, height);
                        geoCanvas.BeginDrawing(geoImage, worldExtent, drawingMapUnit);
                        geoCanvas.Clear(new GeoSolidBrush(GeoColors.Transparent));
                    }
                    else
                    {
                        geoImage = new GeoImage(BackgroundImage.GetImageStream());
                        geoCanvas.BeginDrawing(geoImage, worldExtent, drawingMapUnit);
                    }

                    // draw each layer tiles
                    foreach (var layerTile in layerTiles)
                    {
                        layerTile.Draw(geoCanvas);
                    }

                    geoCanvas.EndDrawing();
                }
            }

            return geoImage;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (BackgroundImage != null)
            {
                BackgroundImage.Dispose();
            }
        }
    }
}
