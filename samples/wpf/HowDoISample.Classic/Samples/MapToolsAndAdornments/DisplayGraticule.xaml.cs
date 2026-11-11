using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw latitude and longitude lines over the map with a GraticuleAdornmentLayer.
    /// </summary>
    public partial class DisplayGraticule : IDisposable
    {
        private bool _initialized;

        public DisplayGraticule()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // The graticule is computed in EPSG:4326; the projection tells it the map is in EPSG:3857.
            var graticuleLayer = new GraticuleAdornmentLayer
            {
                Projection = new Projection(3857)
            };
            graticuleLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // On the adornment overlay the labels stay pinned to the map's edges while panning.
            Map.AdornmentOverlay.Layers.Add("graticule", graticuleLayer);

            Map.CenterPoint = new PointShape(-10777200, 3911500);
            Map.CurrentScale = 36200;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
