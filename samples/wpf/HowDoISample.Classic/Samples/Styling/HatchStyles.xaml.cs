using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Fill parks with a hatch pattern made by AreaStyle.CreateHatchStyle.
    /// </summary>
    public partial class HatchStyles : IDisposable
    {
        private bool _initialized;
        private ShapeFileFeatureLayer _parksLayer;

        public HatchStyles()
        {
            InitializeComponent();
            HatchPatterns.ItemsSource = Enum.GetValues(typeof(GeoHatchStyle)).Cast<GeoHatchStyle>()
                .OrderBy(style => style.ToString(), StringComparer.OrdinalIgnoreCase);
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Background = new SolidColorBrush(Color.FromRgb(234, 232, 226));

            _parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            _parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _parksLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("NAME", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                FittingPolygon = true,
                FittingPolygonInScreen = true,
                AllowLineCarriage = true,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel
            };
            _parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(_parksLayer);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10777610, 3909120);
            Map.CurrentScale = 2260;

            _initialized = true;
            HatchPatterns.SelectedItem = GeoHatchStyle.Cross;
        }

        private void HatchPatterns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized || !(HatchPatterns.SelectedItem is GeoHatchStyle hatchStyle)) return;

            _parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateHatchStyle(
                hatchStyle, GeoColors.Black, GeoColors.Transparent, GeoColors.Blue, 5, LineDashStyle.Solid, 0f, 0f);
            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
