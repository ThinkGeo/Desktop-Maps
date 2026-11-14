using System;
using System.IO;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Color census areas by their number of housing units with a ClassBreakStyle, and list the ranges in a legend.
    /// </summary>
    public partial class RenderBasedOnClassBreaks : IDisposable
    {
        private bool _initialized;

        public RenderBasedOnClassBreaks()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var housingUnitsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco 2010 Census Housing Units.shp"));
            // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var legend = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    TextStyle = new TextStyle("Housing Units", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.LowerRight
            };
            Map.AdornmentOverlay.Layers.Add(legend);

            // A feature takes the highest break that is less than or equal to its value.
            var housingUnitsStyle = new ClassBreakStyle("H_UNITS", BreakValueInclusion.IncludeValue);
            var breakValues = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            var colors = GeoColor.GetColorsInHueFamily(GeoColors.Red, breakValues.Length).Reverse().ToList();
            for (var i = 0; i < breakValues.Length; i++)
            {
                var classBreak = new ClassBreak(breakValues[i], AreaStyle.CreateSimpleAreaStyle(new GeoColor(192, colors[i]), GeoColors.White));
                housingUnitsStyle.ClassBreaks.Add(classBreak);

                var text = i < breakValues.Length - 1
                    ? $"{breakValues[i]} - {breakValues[i + 1] - 1} units"
                    : $"{breakValues[i]} units or more";
                legend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = classBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle(text, new GeoFont("Verdana", 10), GeoBrushes.Black)
                });
            }

            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(housingUnitsStyle);
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(housingUnitsLayer);
            Map.Overlays.Add(layerOverlay);

            housingUnitsLayer.Open();
            var boundingBox = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();
            Map.CenterPoint = boundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight) * 1.5;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
