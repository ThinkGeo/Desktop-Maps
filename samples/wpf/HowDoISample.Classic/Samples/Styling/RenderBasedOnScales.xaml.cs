using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Give each range of zoom levels its own styles with the layer's ZoomLevelSet. Zoom in and out to see them change.
    /// </summary>
    public partial class RenderBasedOnScales : IDisposable
    {
        private bool _initialized;

        public RenderBasedOnScales()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Background = new SolidColorBrush(Color.FromRgb(234, 232, 226));

            var hotelsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"));
            var streetsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Streets.shp"));
            var parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));

            // The shapefiles are in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            StyleHotelsLayer(hotelsLayer);
            StyleStreetsLayer(streetsLayer);
            StyleParksLayer(parksLayer);

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(parksLayer);
            layerOverlay.Layers.Add(streetsLayer);
            layerOverlay.Layers.Add(hotelsLayer);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9400;

            _ = Map.RefreshAsync();
        }

        private static void StyleHotelsLayer(FeatureLayer hotelsLayer)
        {
            var zoomLevels = hotelsLayer.ZoomLevelSet;

            zoomLevels.ZoomLevel12.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 4, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));
            zoomLevels.ZoomLevel12.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level13;

            zoomLevels.ZoomLevel14.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 8, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));
            zoomLevels.ZoomLevel14.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level15;

            zoomLevels.ZoomLevel16.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));
            zoomLevels.ZoomLevel16.DefaultTextStyle = new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed)
            {
                TextPlacement = TextPlacement.Lower,
                YOffsetInPixel = 4,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };
            zoomLevels.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        private static void StyleStreetsLayer(FeatureLayer streetsLayer)
        {
            var zoomLevels = streetsLayer.ZoomLevelSet;

            zoomLevels.ZoomLevel10.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 1));
            zoomLevels.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level11;

            zoomLevels.ZoomLevel12.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 2), new GeoPen(GeoBrushes.White, 1));

            zoomLevels.ZoomLevel13.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 4), new GeoPen(GeoBrushes.White, 2));
            zoomLevels.ZoomLevel13.DefaultTextStyle = CreateStreetLabels(6);

            zoomLevels.ZoomLevel14.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 5), new GeoPen(GeoBrushes.White, 4));
            zoomLevels.ZoomLevel14.DefaultTextStyle = CreateStreetLabels(8);

            zoomLevels.ZoomLevel15.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 7), new GeoPen(GeoBrushes.White, 6));
            zoomLevels.ZoomLevel15.DefaultTextStyle = CreateStreetLabels(9);

            zoomLevels.ZoomLevel16.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 9), new GeoPen(GeoBrushes.White, 8));
            zoomLevels.ZoomLevel16.DefaultTextStyle = CreateStreetLabels(9);

            zoomLevels.ZoomLevel17.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 13), new GeoPen(GeoBrushes.White, 12));
            zoomLevels.ZoomLevel17.DefaultTextStyle = CreateStreetLabels(10);
            zoomLevels.ZoomLevel17.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level18;

            zoomLevels.ZoomLevel19.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 15), new GeoPen(GeoBrushes.White, 14));
            zoomLevels.ZoomLevel19.DefaultTextStyle = CreateStreetLabels(12);
            zoomLevels.ZoomLevel19.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        private static TextStyle CreateStreetLabels(float fontSize)
        {
            return new TextStyle("FULL_NAME", new GeoFont("Noto Sans", fontSize, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };
        }

        private static void StyleParksLayer(FeatureLayer parksLayer)
        {
            var zoomLevels = parksLayer.ZoomLevelSet;

            zoomLevels.ZoomLevel10.DefaultAreaStyle = new AreaStyle(GeoBrushes.PastelGreen);
            zoomLevels.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level14;

            zoomLevels.ZoomLevel15.DefaultAreaStyle = new AreaStyle(GeoBrushes.PastelGreen);
            zoomLevels.ZoomLevel15.DefaultTextStyle = new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };
            zoomLevels.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
