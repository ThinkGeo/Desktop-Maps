using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to restrict the map's panning area and zoom range using RestrictExtent, MaximumScale, and MinimumScale.
    /// </summary>
    public partial class RestrictMapExtent
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

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.ZoomStep = 0.15;
            Map.MapUnit = GeographyUnit.Meter;

            // The red border marking the restricted extent goes to the GPU with the
            // basemap. Drawn on an overlay it would be a raster, and this sample is
            // about zooming - the one thing that would show the border stretching while
            // the map under it stayed sharp.
            var boundary = new InMemoryGeometrySource();
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.AddGeometry(boundary, new GeometryStyle
            {
                // A border, not a shaded box - a fill is drawn unless one is asked not to be.
                FillColor = GeoColors.Transparent,
                OutlineColor = new GeoColor(180, GeoColors.Red),
                OutlineWidthInPixels = 2,
            });
            Map.Basemap = new GpuBasemap(style);
            boundary.UpdateAreas(new[] { _restrictExtent });

            // Apply restrictions and set initial extent
            ApplyRestrictions();
            Map.CurrentExtent = _restrictExtent;
            _ = Map.RefreshAsync();
        }

        private void ApplyRestrictions()
        {
            // RestrictExtent prevents panning beyond a defined bounding box
            Map.RestrictExtent = ChkRestrictExtent.IsChecked == true ? _restrictExtent : null;

            // MaximumScale prevents zooming out beyond a scale level (larger value = more zoomed out)
            Map.MaximumScale = ChkMaximumScale.IsChecked == true ? MaxScale : double.MaxValue;

            // MinimumScale prevents zooming in beyond a scale level (smaller value = more zoomed in)
            Map.MinimumScale = ChkMinimumScale.IsChecked == true ? MinScale : 0;
        }

        private void ChkRestriction_Click(object sender, RoutedEventArgs e)
        {
            if (!Map.IsLoaded) return;
            ApplyRestrictions();
            _ = Map.RefreshAsync();
        }
    }
}
