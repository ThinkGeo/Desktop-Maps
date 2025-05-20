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
    public partial class RenderStyleJson : IDisposable
    {
        private LayerOverlay _layerOverlay = new LayerOverlay();
        private FeatureLayerWpfDrawingOverlay _featureLayerWpfDrawingOverlay = new FeatureLayerWpfDrawingOverlay();

        public RenderStyleJson()
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

            MapStyleLoader loader = new MapStyleLoader();
            loader.LoadAsync(@"./Data/Json/styleJsonDemo.json");
            loader.ApplyStyle("Parks.shp", parksLayer.ZoomLevelSet);
            loader.ApplyStyle("Streets.shp", streetsLayer.ZoomLevelSet);
            loader.ApplyStyle("hotelsLayer.shp", hotelsLayer.ZoomLevelSet);


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


        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                _layerOverlay.IsVisible = !checkBox.IsChecked.GetValueOrDefault();
                _featureLayerWpfDrawingOverlay.IsVisible = checkBox.IsChecked.GetValueOrDefault();

                _ = MapView.RefreshAsync();
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