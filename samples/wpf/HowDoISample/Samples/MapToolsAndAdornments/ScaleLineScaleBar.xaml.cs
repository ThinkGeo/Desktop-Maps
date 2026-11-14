using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Render a ScaleLine and a ScaleBar, choose the scale line's unit system and text styles.
    /// </summary>
    public partial class ScaleLineScaleBar
    {
        private ScaleLineAdornmentLayer _scaleLineAdornmentLayer;
        private bool _initialized;

        public ScaleLineScaleBar()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            // The map's own projection lets the adornments convert the bar's ends to
            // lat/lon and measure the ground distance between them.
            var projection = new Projection(3857);

            _scaleLineAdornmentLayer = new ScaleLineAdornmentLayer
            {
                Projection = projection,
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red),
                UnitSystem = ScaleLineUnitSystem.ImperialAndMetric
            };

            var scaleBarAdornmentLayer = new ScaleBarAdornmentLayer
            {
                YOffsetInPixel = -50,
                Projection = projection,
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red)
            };

            Map.AdornmentOverlay.Layers.Add(_scaleLineAdornmentLayer);
            Map.AdornmentOverlay.Layers.Add(scaleBarAdornmentLayer);

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            _ = Map.RefreshAsync();
        }

        private void UnitSystem_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _scaleLineAdornmentLayer.UnitSystem = Enum.Parse<ScaleLineUnitSystem>((string)((RadioButton)sender).Tag);
            _ = Map.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _scaleLineAdornmentLayer.AboveLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Arial", 16, DrawingFontStyles.Italic), GeoBrushes.Blue);
            _scaleLineAdornmentLayer.AboveLabelTextStyle.TextPlacement = TextPlacement.Left;
            _scaleLineAdornmentLayer.AboveLabelTextStyle.YOffsetInPixel = -2;

            _scaleLineAdornmentLayer.BelowLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Arial", 16), GeoBrushes.Red);
            _scaleLineAdornmentLayer.BelowLabelTextStyle.TextPlacement = TextPlacement.Left;
            _scaleLineAdornmentLayer.BelowLabelTextStyle.YOffsetInPixel = 2;

            _ = Map.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            // Null text styles fall back to the defaults.
            _scaleLineAdornmentLayer.AboveLabelTextStyle = null;
            _scaleLineAdornmentLayer.BelowLabelTextStyle = null;

            _ = Map.AdornmentOverlay.RefreshAsync();
        }
    }
}
