using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class GeoPdfLayerSample : UserControl, IDisposable
    {
        public GeoPdfLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.White;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            GeoPdfFeatureLayer geoPdfLayer = new GeoPdfFeatureLayer(@"./Data/GeoPdf/bangalore.pdf");
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 5, GeoBrushes.LightGray, GeoPens.Black);
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true);
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(geoPdfLayer);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
