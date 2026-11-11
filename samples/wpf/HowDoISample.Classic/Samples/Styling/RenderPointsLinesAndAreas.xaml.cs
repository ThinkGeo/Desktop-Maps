using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw points, lines and areas with a PointStyle, a LineStyle and an AreaStyle.
    /// </summary>
    public partial class RenderPointsLinesAndAreas : IDisposable
    {
        private bool _initialized;
        private readonly LayerOverlay _layerOverlay = new LayerOverlay();
        private ShapeFileFeatureLayer _hotelsLayer;
        private ShapeFileFeatureLayer _railroadLayer;
        private ShapeFileFeatureLayer _parksLayer;

        public RenderPointsLinesAndAreas()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _hotelsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"));
            _railroadLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Railroad", "Railroad.shp"));
            _parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));

            foreach (var layer in new[] { _parksLayer, _railroadLayer, _hotelsLayer })
            {
                // The shapefiles are in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
                layer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                layer.IsVisible = false;
                _layerOverlay.Layers.Add(layer);
            }
            Map.Overlays.Add(_layerOverlay);

            _initialized = true;
            SymbolPoints.IsChecked = true;
        }

        private void SymbolPoints_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var pointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Blue, new GeoPen(GeoBrushes.White, 2));
            ShowLayer(_hotelsLayer, pointStyle, new PointShape(-10777290, 3908740), 9400);
        }

        private void ImagePoints_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var pointStyle = new PointStyle(new GeoImage(Path.Combine(AppContext.BaseDirectory, "Resources", "hotel_icon.png")))
            {
                ImageScale = .25
            };
            ShowLayer(_hotelsLayer, pointStyle, new PointShape(-10777290, 3908740), 9400);
        }

        private void FontPoints_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var pointStyle = new PointStyle(new GeoFont("Verdana", 16, DrawingFontStyles.Bold), "@", GeoBrushes.Black)
            {
                Mask = new AreaStyle(GeoBrushes.White),
                MaskType = MaskType.Circle
            };
            ShowLayer(_hotelsLayer, pointStyle, new PointShape(-10777290, 3908740), 9400);
        }

        private void SolidLines_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));
            ShowLayer(_railroadLayer, lineStyle, new PointShape(-10779420, 3914420), 2260);
        }

        private void DashedLines_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var lineStyle = new LineStyle(
                outerPen: new GeoPen(GeoColors.Black, 12),
                innerPen: new GeoPen(GeoColors.White, 6)
                {
                    DashStyle = LineDashStyle.Custom,
                    DashPattern = { 18f, 18f },
                    StartCap = DrawingLineCap.Flat,
                    EndCap = DrawingLineCap.Flat
                });
            ShowLayer(_railroadLayer, lineStyle, new PointShape(-10779420, 3914420), 2260);
        }

        private void FilledAreas_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(new GeoColor(128, GeoColors.ForestGreen)));
            ShowLayer(_parksLayer, areaStyle, new PointShape(-10778000, 3913000), 65000);
        }

        private void ShowLayer(FeatureLayer layer, Core.Style style, PointShape center, double scale)
        {
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(style);

            if (!layer.IsVisible)
            {
                foreach (var other in _layerOverlay.Layers)
                {
                    other.IsVisible = other == layer;
                }
                Map.CenterPoint = center;
                Map.CurrentScale = scale;
            }

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
