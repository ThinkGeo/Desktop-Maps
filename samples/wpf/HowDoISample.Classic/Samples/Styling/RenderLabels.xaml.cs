using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Label points, lines and areas with a TextStyle, from one column or from several columns.
    /// </summary>
    public partial class RenderLabels : IDisposable
    {
        private bool _initialized;
        private ShapeFileFeatureLayer _hotelsLayer;

        public RenderLabels()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Background = new SolidColorBrush(Color.FromRgb(234, 232, 226));

            var parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            var streetsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Streets.shp"));
            _hotelsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"));

            // The shapefiles are in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(GeoPens.DimGray, GeoBrushes.PastelGreen));
            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                FittingPolygon = true,
                FittingPolygonInScreen = true,
                AllowLineCarriage = true,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel
            });
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new LineStyle(new GeoPen(GeoBrushes.DimGray, 6), new GeoPen(GeoBrushes.WhiteSmoke, 4)));
            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TextStyle("FULL_NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.MidnightBlue)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel
            });
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(parksLayer);
            layerOverlay.Layers.Add(streetsLayer);
            layerOverlay.Layers.Add(_hotelsLayer);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9000;

            _initialized = true;
            OneColumn.IsChecked = true;
        }

        private void OneColumn_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            ShowHotelLabels(new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed));
        }

        private void SeveralColumns_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            ShowHotelLabels(new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed)
            {
                TextContent = "{NAME}" + Environment.NewLine + "{ROOMS} rooms"
            });
        }

        private void ShowHotelLabels(TextStyle labels)
        {
            labels.TextPlacement = TextPlacement.Lower;
            labels.YOffsetInPixel = 2;
            labels.AllowLineCarriage = true;
            labels.HaloPen = new GeoPen(GeoBrushes.White, 2);
            labels.DrawingLevel = DrawingLevel.LabelLevel;

            _hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            _hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new PointStyle(PointSymbolType.Circle, 4, GeoBrushes.Brown, new GeoPen(GeoBrushes.DarkRed, 2)));
            _hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(labels);

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
