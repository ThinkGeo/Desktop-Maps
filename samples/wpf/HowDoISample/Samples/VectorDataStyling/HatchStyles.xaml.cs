using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for HatchStyles.xaml
    /// </summary>
    public partial class HatchStyles : UserControl
    {
        private LayerOverlay _layerOverlay = new LayerOverlay();
        private FeatureLayerWpfDrawingOverlay _featureLayerWpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();
        private GeoHatchStyle _currentHatchStyle = GeoHatchStyle.Cross;

        public HatchStyles()
        {
            InitializeComponent();

            // Fill ComboBox from the GeoHatchStyle enum
            foreach (GeoHatchStyle style in Enum.GetValues(typeof(GeoHatchStyle)))
            {
                CboHatchStyles.Items.Add(style);
            }

            // Select a default value
            CboHatchStyles.SelectedItem = GeoHatchStyle.Cross;
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
       
            var parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Project the layer's data to match the projection of the map
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Styles to the layers
            StyleParksLayer(parksLayer);

            // Add layers to a layerOverlay
            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(parksLayer);
            _layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(_layerOverlay);

            _featureLayerWpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();
            _featureLayerWpfDrawingOverlay.Visibility = Visibility.Hidden;
            _featureLayerWpfDrawingOverlay.FeatureLayers.Add(parksLayer);
            MapView.Overlays.Add(_featureLayerWpfDrawingOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10777610, 3909120);
            MapView.CurrentScale = 2260;

            _ = MapView.RefreshAsync();
        }

        private void CboHatchStyles_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CboHatchStyles.SelectedItem is GeoHatchStyle selectedStyle)
            {
                _currentHatchStyle = selectedStyle;

                if (_layerOverlay.Layers.Count > 0 &&
                    _layerOverlay.Layers[0] is ShapeFileFeatureLayer parksLayer)
                {
                    StyleParksLayer(parksLayer);
                    _ = _layerOverlay.RefreshAsync();
                }
            }
        }

        /// <summary>
        /// Adds an AreaStyle and TextStyle to the Parks Layer
        /// </summary>
        private void StyleParksLayer(FeatureLayer parksLayer)
        {
            var areaStyle = AreaStyle.CreateHatchStyle(
                    _currentHatchStyle,
                GeoColor.FromArgb(255, 0, 0, 0),
                GeoColors.Red,
                GeoColors.Yellow,
            3,
            LineDashStyle.Solid,
            0f,
            0f);

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
