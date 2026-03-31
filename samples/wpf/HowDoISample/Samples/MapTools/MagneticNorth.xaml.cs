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

        private bool _initialized;
        public MagneticNorth()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the cloudOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add("Cloud Overlay", cloudOverlay);

            var magneticDeclinationAdornmentLayer = new MagneticDeclinationAdornmentLayer(AdornmentLocation.UpperRight);
            var proj4Projection = new Projection(3857);
            magneticDeclinationAdornmentLayer.Projection = proj4Projection;
            magneticDeclinationAdornmentLayer.TrueNorthPointStyle.SymbolSize = 25;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.OuterPen.Width = 5f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.OuterPen.Width = 5f;

            Map.AdornmentOverlay.Layers.Add(magneticDeclinationAdornmentLayer);
            Map.AdornmentOverlay.Layers.Add(new LogoAdornmentLayer(new GeoImage(@"..\..\..\Resources\generic-logo.png"))
            {
                Location = AdornmentLocation.LowerRight
            });

            // Set the current extent to a neighborhood in Frisco Texas.
            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;

            // Refresh the map.
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
