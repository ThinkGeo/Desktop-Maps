using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This overlay is to highlight a feature when mouse over.
    public class HighlightOverlay : Overlay
    {
        private bool isPanningMap;
        private double currentScale;
        private Feature highlightFeature;
        private InMemoryFeatureLayer highlightFeatureLayer;
        private TranslateTransform translateTransform;

        public HighlightOverlay()
            : base()
        {
            isPanningMap = false;

            highlightFeatureLayer = new InMemoryFeatureLayer();
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(150, GeoColor.FromHtml("#449FBC")), GeoColor.FromHtml("#014576"), 1);
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            translateTransform = new TranslateTransform();
            OverlayCanvas.RenderTransform = translateTransform;
        }

        public bool IsPanningMap
        {
            get { return isPanningMap; }
        }

        public Feature HighlightFeature
        {
            get { return highlightFeature; }
            set { highlightFeature = value; }
        }

        protected override void DrawCore(RectangleShape targetExtent, OverlayRefreshType overlayRefreshType)
        {
            LayerTile tile = GetTile(targetExtent, MapArguments.ActualWidth, MapArguments.ActualHeight, 0, 0, MapArguments.GetSnappedZoomLevelIndex(targetExtent));
            if (overlayRefreshType == OverlayRefreshType.Pan)
            {
                isPanningMap = true;
                PanOverlay(targetExtent);
            }
            else
            {
                isPanningMap = false;
                RefreshHighlightLayerTileAndPopup(targetExtent, tile);
            }
        }

        private void RefreshHighlightLayerTileAndPopup(RectangleShape targetExtent, LayerTile tile)
        {
            bool isRefreshTile = false;
            if (translateTransform.X != 0 || translateTransform.Y != 0 || MapArguments.CurrentScale != currentScale)
            {
                translateTransform.X = 0;
                translateTransform.Y = 0;
                tile = DrawHighlightTile(targetExtent, tile);
                isRefreshTile = true;
            }

            if (highlightFeature != null)
            {
                string key = highlightFeature.ColumnValues.Select(k => k.Key).ToArray()[0];
                if (highlightFeatureLayer.InternalFeatures.Count == 0 || !highlightFeatureLayer.InternalFeatures[0].ColumnValues.ContainsKey(key) ||
                    (highlightFeatureLayer.InternalFeatures.Count > 0 && (highlightFeature.ColumnValues[key] != highlightFeatureLayer.InternalFeatures[0].ColumnValues[key])))
                {
                    highlightFeatureLayer.InternalFeatures.Clear();
                    highlightFeatureLayer.InternalFeatures.Add(highlightFeature);
                    tile = DrawHighlightTile(targetExtent, tile);
                    isRefreshTile = true;
                }
            }
            else if (highlightFeatureLayer.InternalFeatures.Count > 0)
            {
                highlightFeatureLayer.InternalFeatures.Clear();
                tile = DrawHighlightTile(targetExtent, tile);
                isRefreshTile = true;
            }

            RefreshOverlayCanvas(targetExtent, tile, isRefreshTile);
            currentScale = MapArguments.CurrentScale;
        }

        private void RefreshOverlayCanvas(RectangleShape targetExtent, LayerTile tile, bool isRefreshTile)
        {
            if (isRefreshTile)
            {
                OverlayCanvas.Children.Clear();

                Canvas.SetTop(tile, 0);
                Canvas.SetLeft(tile, 0);

                OverlayCanvas.Children.Add(tile);
            }
        }

        private LayerTile DrawHighlightTile(RectangleShape targetExtent, LayerTile tile)
        {
            tile.DrawingLayers.Clear();
            if (highlightFeatureLayer.InternalFeatures.Count > 0)
            {
                tile.DrawingLayers.Add(highlightFeatureLayer);
            }

            GeoCanvas geoCanvas = new PlatformGeoCanvas()
            {
                CompositingQuality = CompositingQuality.HighSpeed,
                DrawingQuality = DrawingQuality.HighSpeed,
                SmoothingMode = SmoothingMode.HighSpeed
            };
            Bitmap bitmap = new Bitmap((int)tile.Width, (int)tile.Height);
            geoCanvas.BeginDrawing(bitmap, targetExtent, MapArguments.MapUnit);
            tile.Draw(geoCanvas);
            geoCanvas.EndDrawing();
            tile.CommitDrawing(geoCanvas, MapSuiteSampleHelper.GetImageSourceFromNativeImage(bitmap));

            return tile;
        }

        private LayerTile GetTile(RectangleShape targetExtent, double tileScreenWidth, double tileScreenHeight, long tileColumnIndex, long tileRowIndex, int zoomLevelIndex)
        {
            LayerTile layerTile = new LayerTile();
            layerTile.Width = tileScreenWidth;
            layerTile.Height = tileScreenHeight;
            layerTile.IsAsync = false;
            layerTile.RowIndex = tileRowIndex;
            layerTile.ColumnIndex = tileColumnIndex;
            layerTile.TargetExtent = targetExtent;
            layerTile.ZoomLevelIndex = zoomLevelIndex;
            layerTile.DrawingExceptionMode = DrawingExceptionMode;
            layerTile.Background = new SolidColorBrush(Colors.Transparent);

            return layerTile;
        }

        private void PanOverlay(RectangleShape targetExtent)
        {
            if (PreviousExtent != null)
            {
                double resolution = MapArguments.CurrentResolution;
                double worldOffsetX = targetExtent.UpperLeftPoint.X - PreviousExtent.UpperLeftPoint.X;
                double worldOffsetY = targetExtent.UpperLeftPoint.Y - PreviousExtent.UpperLeftPoint.Y;
                double screenOffsetX = worldOffsetX / resolution;
                double screenOffsetY = worldOffsetY / resolution;

                translateTransform.X -= screenOffsetX;
                translateTransform.Y += screenOffsetY;
            }
        }
    }
}