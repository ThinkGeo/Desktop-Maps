using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw lines of equal mosquito counts, interpolated from sample points, with a DynamicIsoLineLayer.
    /// </summary>
    public partial class DisplayISOLine : IDisposable
    {
        private bool _initialized;

        public DisplayISOLine()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // Each line is x,y (EPSG:3857),count.
            var mosquitoCounts = new Dictionary<PointShape, double>();
            foreach (var line in File.ReadLines(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "Frisco_Mosquitos.csv")))
            {
                var parts = line.Split(',');
                if (parts.Length < 3
                    || !double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
                    || !double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var y)
                    || !double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var count))
                {
                    continue;
                }
                mosquitoCounts[new PointShape(x, y)] = count;
            }

            var isoLineLevels = IsoLineLayer.GetIsoLineLevels(mosquitoCounts, 25);
            var isoLineLayer = new DynamicIsoLineLayer(mosquitoCounts, isoLineLevels, new InverseDistanceWeightedGridInterpolationModel(), IsoLineType.LinesOnly)
            {
                CellHeightInPixel = (int)(Map.MapHeight / 80),
                CellWidthInPixel = (int)(Map.MapWidth / 80)
            };

            var colors = GeoColor.GetColorsInQualityFamily(GeoColors.Blue, GeoColors.Red, isoLineLevels.Count, ColorWheelDirection.Clockwise);
            var lineStyle = new ClassBreakStyle(isoLineLayer.DataValueColumnName);
            lineStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new LineStyle(new GeoPen(colors[0], 3))));
            for (var i = 0; i < colors.Count - 1; i++)
            {
                lineStyle.ClassBreaks.Add(new ClassBreak(isoLineLevels[i], new LineStyle(new GeoPen(colors[i + 1], 3))));
            }
            isoLineLayer.CustomStyles.Add(lineStyle);

            var textStyle = TextStyle.CreateSimpleTextStyle(isoLineLayer.DataValueColumnName, "Noto Sans", 10, DrawingFontStyles.Bold, GeoColors.Black, 0, 0);
            textStyle.HaloPen = new GeoPen(GeoColors.White, 2);
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            textStyle.TextLineSegmentRatio = 9999999;
            textStyle.FittingLineInScreen = true;
            textStyle.SuppressPartialLabels = true;
            isoLineLayer.CustomStyles.Add(textStyle);

            var isoLineOverlay = new LayerOverlay { DrawingQuality = DrawingQuality.Standard };
            isoLineOverlay.Layers.Add(isoLineLayer);
            Map.Overlays.Add(isoLineOverlay);

            var mosquitoSites = new ShapeFileFeatureSource(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco_Mosquitos.shp"))
            {
                // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
                ProjectionConverter = new ProjectionConverter(2276, 3857)
            };
            mosquitoSites.Open();
            var boundingBox = mosquitoSites.GetBoundingBox();
            mosquitoSites.Close();
            Map.CenterPoint = boundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
