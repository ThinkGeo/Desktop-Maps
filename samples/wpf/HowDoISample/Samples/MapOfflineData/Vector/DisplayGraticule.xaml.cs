using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Graticule Layer on the map
    /// </summary>
    public partial class DisplayGraticule : IDisposable
    {

        private bool _initialized;
        public DisplayGraticule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the graticule layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create the new layer. It generates its lines in srid 4326, so set the projection our background is in, srid 3857 (spherical mercator).
            var graticuleLayer = new GraticuleAdornmentLayer
            {
                Projection = new Projection(3857)
            };
            // We set the pen color to the graticule layer.
            graticuleLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // Add the layer to the map's adornment overlay, so its labels stay pinned to the edges of the map while panning.
            Map.AdornmentOverlay.Layers.Add("graticule", graticuleLayer);

            // Set the current extent of the map to start in Frisco TX
            Map.CenterPoint = new PointShape(-10777200, 3911500);
            Map.CurrentScale = 36200;

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