using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayIsoLines.xaml
    /// </summary>
    public partial class DisplayIsoLines : UserControl
    {
        private string wellDepthPointDataFilePath = SampleHelper.Get("GrayCountyIrrigationWellDepths.csv");
        private Dictionary<PointShape, double> wellDepthPointData;

        public DisplayIsoLines()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-11204955.4870063, 4543351.66721459, -11200273.7233416, 4539757.60752901);

            //Load the well depth points and depth data from a text file into the dictionary
            //We cache this at the class level to prevent form loading it multiple times
            wellDepthPointData = GetWellDepthPointDataFromCSV(wellDepthPointDataFilePath);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            //Add the grid layer, the grid cells, and the well points to the map
            LayerOverlay isoLineOverlay = new LayerOverlay();
            mapView.Overlays.Add("isoLineOverlay", isoLineOverlay);
            isoLineOverlay.Layers.Add("IsoLineLayer", GetDynamicIsoLineLayer());
            isoLineOverlay.DrawingQuality = DrawingQuality.HighQuality;

            mapView.Refresh();
        }

        private static Dictionary<PointShape, double> GetWellDepthPointDataFromCSV(string csvFilePath)
        {
            StreamReader streamReader = null;
            Dictionary<PointShape, double> wellDataPoints = new Dictionary<PointShape, double>();

            try
            {
                streamReader = new StreamReader(csvFilePath);
                string headline = streamReader.ReadLine();
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();

                    string[] parts = line.Split(',');
                    wellDataPoints.Add(new PointShape(double.Parse(parts[0]), double.Parse(parts[1])), double.Parse(parts[2]));
                }
            }
            finally
            {
                if (streamReader != null) { streamReader.Close(); }
            }
            return wellDataPoints;
        }

        private DynamicIsoLineLayer GetDynamicIsoLineLayer()
        {
            Collection<double> isoLineLevels = GridIsoLineLayer.GetIsoLineLevels(wellDepthPointData, 25);

            //Create the new dynamicIsoLineLayer using the well data and the number of isoline levels from
            //the screen
            DynamicIsoLineLayer dynamicIsoLineLayer = new DynamicIsoLineLayer(wellDepthPointData, isoLineLevels, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue), IsoLineType.LinesOnly);

            dynamicIsoLineLayer.CellHeightInPixel = (int)(mapView.ActualHeight / 80);
            dynamicIsoLineLayer.CellWidthInPixel = (int)(mapView.ActualWidth / 80);

            //Create a series of colors from blue to red that we will use for the breaks
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColors.Blue, GeoColors.Red, isoLineLevels.Count, ColorWheelDirection.Clockwise);

            //Setup a class break style based on the isoline levels and the colors
            ClassBreakStyle classBreakStyle = new ClassBreakStyle(dynamicIsoLineLayer.DataValueColumnName);

            Collection<Core.Style> firstStyles = new Collection<Core.Style>();
            firstStyles.Add(new LineStyle(new GeoPen(colors[0], 3)));
            firstStyles.Add(new AreaStyle(new GeoPen(GeoColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[0]))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, firstStyles));
            for (int i = 0; i < colors.Count - 1; i++)
            {
                Collection<Core.Style> styles = new Collection<Core.Style>();
                styles.Add(new LineStyle(new GeoPen(colors[i + 1], 3)));
                styles.Add(new AreaStyle(new GeoPen(GeoColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[i + 1]))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(isoLineLevels[i], styles));
            }
            dynamicIsoLineLayer.CustomStyles.Add(classBreakStyle);

            //Create the text styles to label the lines
            TextStyle textStyle = TextStyle.CreateSimpleTextStyle(dynamicIsoLineLayer.DataValueColumnName, "Arial", 8, DrawingFontStyles.Bold, GeoColors.Black, 0, 0);
            textStyle.HaloPen = new GeoPen(GeoColors.White, 2);
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            textStyle.TextLineSegmentRatio = 9999999;
            textStyle.FittingLineInScreen = true;
            textStyle.SuppressPartialLabels = true;
            dynamicIsoLineLayer.CustomStyles.Add(textStyle);

            return dynamicIsoLineLayer;
        }
    }
}
