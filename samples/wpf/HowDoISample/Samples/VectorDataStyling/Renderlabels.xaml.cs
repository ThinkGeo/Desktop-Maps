using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render labels using a TextStyle.
    /// </summary>
    public partial class RenderLabels : IDisposable
    {
        private LayerOverlay _layerOverlay = new LayerOverlay();
        private FeatureLayerWpfDrawingOverlay _featureLayerWpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();

        public RenderLabels()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and add styles to the Hotels, Streets, and Parks layer.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;


            // Set the map background color
            MapView.Background = new SolidColorBrush(Color.FromRgb(234, 232, 226));

            var hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");
            var streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            var parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Project the layer's data to match the projection of the map
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Styles to the layers
            StyleHotelsLayer(hotelsLayer);
            StyleStreetsLayer(streetsLayer);
            StyleParksLayer(parksLayer);

            // Add layers to a layerOverlay
            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(parksLayer);
            _layerOverlay.Layers.Add(streetsLayer);
            _layerOverlay.Layers.Add(hotelsLayer);
            _layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(_layerOverlay);

            _featureLayerWpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();
            _featureLayerWpfDrawingOverlay.Visibility = Visibility.Hidden;
            _featureLayerWpfDrawingOverlay.FeatureLayers.Add(parksLayer);
            _featureLayerWpfDrawingOverlay.FeatureLayers.Add(streetsLayer);
            _featureLayerWpfDrawingOverlay.FeatureLayers.Add(hotelsLayer);
            MapView.Overlays.Add(_featureLayerWpfDrawingOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10777290, 3908740);
            MapView.CurrentScale = 9000;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Adds a PointStyle and TextStyle to the Hotels Layer
        /// </summary>
        private static void StyleHotelsLayer(FeatureLayer hotelsLayer)
        {
            var pointStyle = new PointStyle(PointSymbolType.Circle, 4, GeoBrushes.Brown, new GeoPen(GeoBrushes.DarkRed, 2));
            var textStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed)
            {
                TextPlacement = TextPlacement.Lower,
                YOffsetInPixel = 2,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds a LineStyle and TextStyle to the Streets Layer
        /// </summary>
        private static void StyleStreetsLayer(FeatureLayer streetsLayer)
        {
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 6), new GeoPen(GeoBrushes.WhiteSmoke, 4));
            var textStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.MidnightBlue)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel
            };
            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);
            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds an AreaStyle and TextStyle to the Parks Layer
        /// </summary>
        private static void StyleParksLayer(FeatureLayer parksLayer)
        {
            var areaStyle = new AreaStyle(GeoPens.DimGray, GeoBrushes.PastelGreen);
            var textStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                FittingPolygon = true,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true,
                FittingPolygonInScreen = true
            };
            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);
            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                _layerOverlay.IsVisible = !checkBox.IsChecked.GetValueOrDefault();
                _featureLayerWpfDrawingOverlay.IsVisible = checkBox.IsChecked.GetValueOrDefault();

                //_ = MapView.RefreshAsync();
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}