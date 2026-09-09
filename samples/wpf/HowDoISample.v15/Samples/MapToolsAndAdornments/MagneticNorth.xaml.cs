using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class MagneticNorth
    {

        private bool _initialized;
        public MagneticNorth()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = cloudOverlay;

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

            Map.CenterPoint = new PointShape(-10779700, 3912000);
            Map.CurrentScale = 18100;

            // Refresh the map.
            _ = Map.RefreshAsync();
        }
    }
}
