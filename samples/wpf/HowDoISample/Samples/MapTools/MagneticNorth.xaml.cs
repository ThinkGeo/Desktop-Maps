using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class MagneticNorth : IDisposable
    {
        public MagneticNorth()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the cloudOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            var magneticDeclinationAdornmentLayer = new MagneticDeclinationAdornmentLayer(AdornmentLocation.LowerLeft);
            var proj4Projection = new Projection(3857);
            magneticDeclinationAdornmentLayer.Projection = proj4Projection;
            magneticDeclinationAdornmentLayer.TrueNorthPointStyle.SymbolSize = 25;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.OuterPen.Width = 5f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.OuterPen.Width = 5f;

            MapView.AdornmentOverlay.Layers.Add(magneticDeclinationAdornmentLayer);

            // Set the current extent to a neighborhood in Frisco Texas.
            MapView.CenterPoint = new PointShape(-10779700, 3912000);
            MapView.CurrentScale = 18100;

            // Refresh the map.
            _ = MapView.RefreshAsync();
        }

        private void MapView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _ = MapView.AdornmentOverlay.RefreshAsync();
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