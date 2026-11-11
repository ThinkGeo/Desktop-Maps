using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render a ScaleLine and a ScaleBar, and choose the scale line's unit system and text styles.
    /// </summary>
    public partial class ScaleLineScaleBar : IDisposable
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

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // With the map's projection the adornments measure ground distance, not Mercator map units.
            _scaleLineAdornmentLayer = new ScaleLineAdornmentLayer
            {
                Projection = new Projection(3857),
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red),
                UnitSystem = ScaleLineUnitSystem.ImperialAndMetric
            };

            var scaleBarAdornmentLayer = new ScaleBarAdornmentLayer
            {
                YOffsetInPixel = -50,
                Projection = new Projection(3857),
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

            _scaleLineAdornmentLayer.UnitSystem = (ScaleLineUnitSystem)Enum.Parse(typeof(ScaleLineUnitSystem), (string)((RadioButton)sender).Tag);
            _ = Map.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _scaleLineAdornmentLayer.AboveLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Noto Sans", 16, DrawingFontStyles.Italic), GeoBrushes.Blue);
            _scaleLineAdornmentLayer.AboveLabelTextStyle.TextPlacement = TextPlacement.Left;
            _scaleLineAdornmentLayer.AboveLabelTextStyle.YOffsetInPixel = -2;

            _scaleLineAdornmentLayer.BelowLabelTextStyle = new TextStyle(string.Empty, new GeoFont("Noto Sans", 16), GeoBrushes.Red);
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

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
