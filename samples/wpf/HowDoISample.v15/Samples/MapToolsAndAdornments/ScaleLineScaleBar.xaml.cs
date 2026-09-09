using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn to render a ScaleLine on the map in a variety of different of styles.
    /// </summary>
    public partial class ScaleLineScaleBar
    {
        private bool _initialized;
        private ScaleBarAdornmentLayer _scaleBarAdornmentLayer;
        private ScaleLineAdornmentLayer _scaleLineAdornmentLayer;

        public ScaleLineScaleBar()
        {
            InitializeComponent();

            foreach (ScaleLineUnitSystem unitSystem in Enum.GetValues(typeof(ScaleLineUnitSystem)))
            {
                CboUnitSystems.Items.Add(unitSystem);
            }
            CboUnitSystems.SelectedItem = ScaleLineUnitSystem.ImperialAndMetric;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = thinkGeoCloudVectorMapsOverlay;

            BuildAdornments();

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            _initialized = true;
            _ = Map.RefreshAsync();
        }
        /// <summary>
        /// The adornment reads Projection when it opens, so switching it means
        /// building the layers again rather than assigning the property.
        /// </summary>
        private void BuildAdornments()
        {
            // Setting it to the map's own projection is what lets the adornment
            // convert the bar's endpoints to lat/lon and measure the real distance
            // between them. Left null it reports map units, which this far from the
            // equator are considerably longer than the meters they claim to be.
            var projection = new Projection(3857);
            var selectedUnitSystem = CboUnitSystems.SelectedItem is ScaleLineUnitSystem unitSystem ? unitSystem : ScaleLineUnitSystem.ImperialAndMetric;

            _scaleLineAdornmentLayer = new ScaleLineAdornmentLayer
            {
                Projection = projection,
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red),
                UnitSystem = selectedUnitSystem
            };

            _scaleBarAdornmentLayer = new ScaleBarAdornmentLayer
            {
                YOffsetInPixel = -50,
                Projection = projection,
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightBlue, GeoColors.Red)
            };

            Map.AdornmentOverlay.Layers.Clear();
            Map.AdornmentOverlay.Layers.Add(_scaleLineAdornmentLayer);
            Map.AdornmentOverlay.Layers.Add(_scaleBarAdornmentLayer);
        }

        private void CboUnitSystems_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized)
                return;

            if (!(CboUnitSystems.SelectedItem is ScaleLineUnitSystem unitSystem)) return;

            _scaleLineAdornmentLayer.UnitSystem = unitSystem;
            _ = Map.AdornmentOverlay.RefreshAsync();
        }

        private void ChkCustomTextStyles_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

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
            if (!_initialized)
                return;

            // set them to null and internally the map will pick up the default styles.
            _scaleLineAdornmentLayer.AboveLabelTextStyle = null;
            _scaleLineAdornmentLayer.BelowLabelTextStyle = null;

            _ = Map.AdornmentOverlay.RefreshAsync();
        }
    }
}
