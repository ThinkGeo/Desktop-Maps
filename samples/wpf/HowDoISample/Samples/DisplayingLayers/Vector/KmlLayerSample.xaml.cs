using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class KmlLayerSample : IDisposable
    {
        public KmlLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            MapView.Overlays.Add(layerOverlay);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var layer = new KmlFeatureLayer("./Data/Kml/Frisco.kml");
            layer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Diamond, GeoColors.Black, 10);
            layer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 4, true);
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(layer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            MapView.CurrentExtent = new RectangleShape(-10777998.2731192, 3913070.41013283, -10774999.3141042, 3911542.86390418);

            await MapView.RefreshAsync();
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