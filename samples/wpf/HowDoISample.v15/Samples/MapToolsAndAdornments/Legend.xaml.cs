using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A legend, as the subject rather than as scenery.
    ///
    /// Six samples in this gallery already put a LegendAdornmentLayer on the map while
    /// demonstrating something else, and none of them is findable by someone looking for
    /// a legend. This one builds the legend and nothing else: a title, one item per
    /// class, each item carrying the very style the layer draws with, so the swatches
    /// cannot drift from the map.
    /// </summary>
    public partial class Legend
    {
        private bool _initialized;

        public Legend()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;

            var states = new ShapeFileFeatureLayer(SampleShared.Shapefile("USStates_3857.shp"));
            var classes = new ClassBreakStyle("POP1990");
            var breaks = new[] { 0d, 2_000_000d, 6_000_000d, 12_000_000d };
            var colors = new[]
            {
                GeoColors.PastelBlue, GeoColors.LightBlue, GeoColors.MediumBlue, GeoColors.DarkBlue,
            };

            var legend = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    TextStyle = new TextStyle("Population 1990",
                        new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black),
                },
                Location = AdornmentLocation.LowerRight,
            };

            for (var i = 0; i < breaks.Length; i++)
            {
                // The legend item and the map share one style object: a swatch that
                // is built separately is a swatch that can lie.
                var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(colors[i]));
                classes.ClassBreaks.Add(new ClassBreak(breaks[i], areaStyle));
                legend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = areaStyle,
                    TextStyle = new TextStyle(
                        i == breaks.Length - 1
                            ? FormattableString.Invariant($"over {breaks[i] / 1_000_000:0}M")
                            : FormattableString.Invariant($"{breaks[i] / 1_000_000:0}M to {breaks[i + 1] / 1_000_000:0}M"),
                        new GeoFont("Verdana", 9), GeoBrushes.Black),
                });
            }

            states.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classes);
            states.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var overlay = new LayerOverlay();
            overlay.Layers.Add(states);
            Map.Overlays.Add(overlay);
            Map.AdornmentOverlay.Layers.Add(legend);

            states.Open();
            Map.CurrentExtent = states.GetBoundingBox();
            await Map.RefreshAsync();
        }
    }
}
