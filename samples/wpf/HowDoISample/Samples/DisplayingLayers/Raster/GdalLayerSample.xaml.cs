using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class GdalLayerSample : UserControl, IDisposable
    {
        public GdalLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Create the new layer and dd the layer to the overlay we created earlier.
            GdalRasterLayer worldLayer = new GdalRasterLayer("./Data/GeoTiff/World.tif");
            worldLayer.LowerThreshold = 0;
            worldLayer.UpperThreshold = double.MaxValue;

            worldLayer.Open();
            mapView.CurrentExtent = worldLayer.GetBoundingBox();
            worldLayer.Close();

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.DrawingExceptionMode = DrawingExceptionMode.DrawException;

            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            // Refresh the map.
            mapView.Refresh();
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
