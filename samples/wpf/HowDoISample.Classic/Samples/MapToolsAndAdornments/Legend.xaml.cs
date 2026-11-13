using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// A LegendAdornmentLayer with a title and one item per class, each item carrying the style the layer draws with.
    /// </summary>
    public partial class Legend : IDisposable
    {
        private bool _initialized;

        public Legend()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var states = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "USStates_3857.shp"));
            var classes = new ClassBreakStyle("POP1990");
            var breaks = new[] { 0d, 2000000d, 6000000d, 12000000d };
            var colors = new[]
            {
                GeoColors.PastelBlue, GeoColors.LightBlue, GeoColors.MediumBlue, GeoColors.DarkBlue
            };

            var legend = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    TextStyle = new TextStyle("Population 1990", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.LowerRight
            };

            for (var i = 0; i < breaks.Length; i++)
            {
                // The legend item and the map share one style object, so the swatch always matches the map.
                var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(colors[i]));
                classes.ClassBreaks.Add(new ClassBreak(breaks[i], areaStyle));
                legend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = areaStyle,
                    TextStyle = new TextStyle(
                        i == breaks.Length - 1
                            ? FormattableString.Invariant($"over {breaks[i] / 1000000:0}M")
                            : FormattableString.Invariant($"{breaks[i] / 1000000:0}M to {breaks[i + 1] / 1000000:0}M"),
                        new GeoFont("Verdana", 9), GeoBrushes.Black)
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
            states.Close();

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
