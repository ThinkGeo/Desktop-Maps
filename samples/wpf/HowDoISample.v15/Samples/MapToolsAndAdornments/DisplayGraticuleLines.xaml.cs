using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to display a Graticule Layer on the map
    /// </summary>
    public partial class DisplayGraticuleLines
    {

        private bool _initialized;
        public DisplayGraticuleLines()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the graticule layer to the map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = thinkGeoCloudVectorMapsOverlay;

            // Deliberately the per-frame WPF overlay, not the GPU tile path: a graticule
            // generates its grid per EXTENT (line density follows the whole view, and the
            // degree labels sit at the screen edges), which a per-tile cutter cannot
            // express - each tile would pick its own density and no tile owns the edge.
            var layerOverlay = new FeatureLayerWpfDrawingOverlay();
            Map.Overlays.Add(layerOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            var graticuleFeatureLayer = new GraticuleFeatureLayer
            {
                
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            // We set the pen color to the graticule layer.
            graticuleFeatureLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // Add the layer to the overlay we created earlier.
            layerOverlay.FeatureLayers.Add("graticule", graticuleFeatureLayer);

            Map.CenterPoint = new PointShape(-10777200, 3911500);
            Map.CurrentScale = 36200;

            _ = Map.RefreshAsync();
        }
    }
}