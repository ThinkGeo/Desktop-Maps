using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the features of a Web Feature Service: Helsinki land parcels from the Finnish cadastre.
    /// </summary>
    public partial class WFS : IDisposable
    {
        private bool _initialized;

        public WFS()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // The service answers in the Finnish national grid (EPSG 3067).
            var parcelsLayer = new WfsV2AsyncLayer("https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows", "cp:CadastralParcel")
            {
                ProjectionConverter = new ProjectionConverter(3067, 3857),
                TimeoutInSeconds = 60
            };
            parcelsLayer.ZoomLevelSet.ZoomLevel13.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            parcelsLayer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var parcelsOverlay = new LayerOverlay();
            parcelsOverlay.Layers.Add(parcelsLayer);
            Map.Overlays.Add(parcelsOverlay);

            Map.CenterPoint = new PointShape(2777730, 8435220);
            Map.CurrentScale = 20520;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
