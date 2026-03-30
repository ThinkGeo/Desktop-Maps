using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to restrict the map's panning area and zoom range using RestrictExtent, MaximumScale, and MinimumScale.
    /// </summary>
    public partial class RestrictMapExtent : IDisposable
    {
        // Restrict extent: Dallas/Frisco, TX area in meters (EPSG:3857)
        private readonly RectangleShape _restrictExtent = new RectangleShape(-10809000, 3900000, -10747000, 3840000);
        private const double MaxScale = 500000;
        private const double MinScale = 10000;
        private bool _initialized;

        public RestrictMapExtent()
        {
            InitializeComponent();
        }

        private void MapView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters (Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add ThinkGeo Cloud Maps as the background
            var backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(backgroundOverlay);

            // Add a boundary layer to visualize the restrict extent as a red border
            var boundaryLayer = new InMemoryFeatureLayer();
            boundaryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(
                GeoColors.Transparent, GeoColor.FromArgb(180, GeoColors.Red), 2);
            boundaryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            boundaryLayer.InternalFeatures.Add(new Feature(_restrictExtent));

            var highlightOverlay = new LayerOverlay();
            highlightOverlay.Layers.Add("BoundaryLayer", boundaryLayer);
            MapView.Overlays.Add(highlightOverlay);

            // Apply restrictions and set initial extent
            ApplyRestrictions();
            MapView.CurrentExtent = _restrictExtent;
            _ = MapView.RefreshAsync();
        }

        private void ApplyRestrictions()
        {
            // RestrictExtent prevents panning beyond a defined bounding box
            MapView.RestrictExtent = ChkRestrictExtent.IsChecked == true ? _restrictExtent : null;

            // MaximumScale prevents zooming out beyond a scale level (larger value = more zoomed out)
            MapView.MaximumScale = ChkMaximumScale.IsChecked == true ? MaxScale : double.MaxValue;

            // MinimumScale prevents zooming in beyond a scale level (smaller value = more zoomed in)
            MapView.MinimumScale = ChkMinimumScale.IsChecked == true ? MinScale : 0;
        }

        private void ChkRestriction_Click(object sender, RoutedEventArgs e)
        {
            if (!MapView.IsLoaded) return;
            ApplyRestrictions();
            _ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
