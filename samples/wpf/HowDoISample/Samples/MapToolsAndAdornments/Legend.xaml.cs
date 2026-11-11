using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A legend with a title and one item per class, each item carrying the style the layer draws with.
    /// </summary>
    public partial class Legend
    {
        private bool _initialized;

        public Legend()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
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
