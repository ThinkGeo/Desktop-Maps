using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class DisplayGdalFile : IDisposable
    {

        private bool _initialized;
        public DisplayGdalFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the GeoTiff layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.DecimalDegree;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Create the new layer and dd the layer to the overlay we created earlier.
            var worldLayer = new GdalRasterLayer("./Data/GeoTiff/World.tif")
            {
                LowerScale = 0,
                UpperScale = double.MaxValue
            };

            worldLayer.Open();
            var worldLayerBBox = worldLayer.GetBoundingBox();
            Map.CenterPoint = worldLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit,worldLayerBBox, Map.MapWidth, Map.MapHeight);
            worldLayer.Close();

            // Create a new overlay that will hold our new layer and add it to the map.
            var staticOverlay = new LayerOverlay();

            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            Map.Overlays.Add(staticOverlay);

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}