using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class EarthquakeIsoLineFeatureLayer : FeatureLayer
    {
        private DynamicIsoLineLayer isoLineLayer;
        private ClassBreakStyle levelClassBreakStyle;

        public EarthquakeIsoLineFeatureLayer()
            : this(null)
        { }

        public EarthquakeIsoLineFeatureLayer(ShapeFileFeatureSource featureSource)
        {
            FeatureSource = featureSource;
        }

        public new FeatureSource FeatureSource
        {
            get { return base.FeatureSource; }
            set
            {
                base.FeatureSource = value;
                Initialize();
            }
        }

        public Collection<double> IsoLineLevels
        {
            get { return isoLineLayer.IsoLineLevels; }
        }

        public ClassBreakStyle LevelClassBreakStyle
        {
            get { return levelClassBreakStyle; }
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            isoLineLayer.Draw(canvas, labelsInAllLayers);
        }

        private void Initialize()
        {
            Collection<GeoColor> levelAreaColors = new Collection<GeoColor>();
            levelAreaColors.Add(GeoColor.FromHtml("#FFFFBE"));
            levelAreaColors.Add(GeoColor.FromHtml("#FDFF9E"));
            levelAreaColors.Add(GeoColor.FromHtml("#FDFF37"));
            levelAreaColors.Add(GeoColor.FromHtml("#FDDA04"));
            levelAreaColors.Add(GeoColor.FromHtml("#FFA701"));
            levelAreaColors.Add(GeoColor.FromHtml("#FF6F02"));
            levelAreaColors.Add(GeoColor.FromHtml("#EC0000"));
            levelAreaColors.Add(GeoColor.FromHtml("#B90000"));
            levelAreaColors.Add(GeoColor.FromHtml("#850100"));
            levelAreaColors.Add(GeoColor.FromHtml("#620001"));
            levelAreaColors.Add(GeoColor.FromHtml("#450005"));
            levelAreaColors.Add(GeoColor.FromHtml("#2B0804"));

            FeatureSource.Open();

            Dictionary<PointShape, double> dataPoints = GetDataPoints();
            GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel(3, double.MaxValue);
            isoLineLayer = new DynamicIsoLineLayer(dataPoints, IsoLineLayer.GetIsoLineLevels(dataPoints.Values, 12), interpolationModel, IsoLineType.ClosedLinesAsPolygons);

            IsoLineLayer.GetIsoLineLevels(dataPoints, 12);

            levelClassBreakStyle = new ClassBreakStyle(isoLineLayer.DataValueColumnName);
            levelClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoPen(GeoColor.FromHtml("#FE6B06"), 1), new GeoSolidBrush(new GeoColor(100, levelAreaColors[0])))));
            for (int i = 0; i < IsoLineLevels.Count - 1; i++)
            {
                levelClassBreakStyle.ClassBreaks.Add(new ClassBreak(IsoLineLevels[i + 1], new AreaStyle(new GeoPen(GeoColor.FromHtml("#FE6B06"), 1), new GeoSolidBrush(new GeoColor(100, levelAreaColors[i + 1])))));
            }
            isoLineLayer.CustomStyles.Add(levelClassBreakStyle);

            TextStyle textStyle = TextStyles.CreateSimpleTextStyle(isoLineLayer.DataValueColumnName, "Arial", 8, DrawingFontStyles.Bold, GeoColor.StandardColors.Black, 0, 0);
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 2);
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            textStyle.TextLineSegmentRatio = 9999999;
            textStyle.FittingLineInScreen = true;
            textStyle.SuppressPartialLabels = true;
            textStyle.NumericFormat = "{0:0.00}";
            isoLineLayer.CustomStyles.Add(textStyle);
        }

        private Dictionary<PointShape, double> GetDataPoints()
        {
            return (from feature in FeatureSource.GetAllFeatures(GetReturningColumns())
                    where double.Parse(feature.ColumnValues[Resources.MagnitudeColumnName]) > 0
                    select new PointShape
                    {
                        X = double.Parse(feature.ColumnValues[Resources.LongitudeColumnName], CultureInfo.InvariantCulture),
                        Y = double.Parse(feature.ColumnValues[Resources.LatitudeColumnName], CultureInfo.InvariantCulture),
                        Z = double.Parse(feature.ColumnValues[Resources.MagnitudeColumnName], CultureInfo.InvariantCulture)
                    }).ToDictionary(point => point, point => point.Z);
        }

        private static IEnumerable<string> GetReturningColumns()
        {
            yield return Resources.LongitudeColumnName;
            yield return Resources.LatitudeColumnName;
            yield return Resources.MagnitudeColumnName;
        }
    }
}