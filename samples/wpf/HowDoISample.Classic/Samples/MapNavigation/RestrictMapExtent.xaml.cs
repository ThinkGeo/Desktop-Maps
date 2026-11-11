using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Restrict the map's panning area and zoom range using RestrictExtent, MaximumScale, and MinimumScale.
    /// </summary>
    public partial class RestrictMapExtent : IDisposable
    {
        // Dallas/Frisco, TX area in EPSG:3857
        private readonly RectangleShape _restrictExtent = new RectangleShape(-10809000, 3900000, -10747000, 3840000);
        private const double MaxScale = 500000;
        private const double MinScale = 10000;
        private bool _initialized;

        public RestrictMapExtent()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2
            });

            var boundaryLayer = new InMemoryFeatureLayer();
            boundaryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(
                GeoColors.Transparent, GeoColor.FromArgb(180, GeoColors.Red), 2);
            boundaryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            boundaryLayer.InternalFeatures.Add(new Feature(_restrictExtent));

            var highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("BoundaryLayer", boundaryLayer);
            Map.Overlays.Add(highlightOverlay);

            ApplyRestrictions();
            Map.CurrentExtent = _restrictExtent;
            _ = Map.RefreshAsync();
        }

        private void ApplyRestrictions()
        {
            Map.RestrictExtent = ChkRestrictExtent.IsChecked == true ? _restrictExtent : null;

            // A larger scale is further out, so MaximumScale limits zooming out and MinimumScale zooming in.
            Map.MaximumScale = ChkMaximumScale.IsChecked == true ? MaxScale : double.MaxValue;
            Map.MinimumScale = ChkMinimumScale.IsChecked == true ? MinScale : 0;
        }

        private void ChkRestriction_Click(object sender, RoutedEventArgs e)
        {
            if (!Map.IsLoaded) return;
            ApplyRestrictions();
            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
