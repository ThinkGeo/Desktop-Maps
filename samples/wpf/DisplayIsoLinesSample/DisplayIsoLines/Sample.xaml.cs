/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DisplayIsoLines
{
    public partial class Sample : Window
    {
        private string wellDepthPointDataFilePath = @"..\..\Data\GrayCountyIrrigationWellDepths.csv";
        private Dictionary<PointShape, double> wellDepthPointData;
        private GridCell[,] gridCellMatrix;
        private Stopwatch stopwatch;

        public Sample()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            btnGenerateGridFile.IsEnabled = true;

            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map1.CurrentExtent = new RectangleShape(-11204955.4870063, 4543351.66721459, -11200273.7233416, 4539757.60752901);

            //Load the well depth points and depth data from a text file into the dictionary
            //We cache this at the class level to prevent form loading it multiple times
            wellDepthPointData = GetWellDepthPointDataFromCSV(wellDepthPointDataFilePath);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            Map1.Overlays.Add(baseOverlay);

            //Add the grid layer, the grid cells, and the well points to the map
            LayerOverlay isoLineOverlay = new LayerOverlay();
            isoLineOverlay.Drawn += new EventHandler<DrawnOverlayEventArgs>(isoLineOverlay_Drawn);
            isoLineOverlay.Drawing += new EventHandler<DrawingOverlayEventArgs>(isoLineOverlay_Drawing);

            Map1.Overlays.Add("isoLineOverlay", isoLineOverlay);

            isoLineOverlay.Layers.Add("IsoLineLayer", GetDynamicIsoLineLayer());
            isoLineOverlay.Layers.Add("GridCellsLayer", GetDynamicGridFeatureLayer());
            isoLineOverlay.Layers.Add("WellsLayer", GetWellDepthPointLayer());
            isoLineOverlay.DrawingQuality = DrawingQuality.HighQuality;

            Map1.Refresh();
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

        private void CreateGridCellMatrix()
        {
            //Get the current extent since we use that to gerenate the grid.  Of course this is just for
            //the demo and in the real world you can use any extent you like
            RectangleShape currentDrawingExtent = ExtentHelper.GetDrawingExtent(Map1.CurrentExtent, (float)Map1.ActualWidth, (float)Map1.ActualHeight);

            //Calculate the cell size based on how many rows and columns you specified
            double cellSize = Math.Min(currentDrawingExtent.Width / double.Parse(txtGridIsoLineCellColumnCount.Text), currentDrawingExtent.Height / Int32.Parse(txtGridIsoLineCellRowCount.Text));

            //Greate the grid definition based on the extent, cell size etc.
            GridDefinition gridDefinition = new GridDefinition(currentDrawingExtent, cellSize, -9999, wellDepthPointData);

            //Generate the grid based on Inverse Distance Weighted interpolation model.  You can define your own model if needed.
            gridCellMatrix = GridFeatureSource.GenerateGridMatrix(gridDefinition, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue));
        }

        private InMemoryGridIsoLineLayer GetGridIsoLineLayer()
        {
            //Create a grid cell matrix based on the textboxes and the current extent
            CreateGridCellMatrix();

            //Load the grid we just created into the GridIsoLineLayer using the number of breaks defines
            //on the screen
            Collection<double> isoLineLevels = GridIsoLineLayer.GetIsoLineLevels(wellDepthPointData, Convert.ToInt32(txtGridIsoLineLevelCount.Text));
            InMemoryGridIsoLineLayer isoLineLayer = new InMemoryGridIsoLineLayer(gridCellMatrix, isoLineLevels);
            if (rdoLinesOnlyType.IsChecked.Value.Equals(true))
            {
                isoLineLayer.IsoLineType = IsoLineType.LinesOnly;
            }
            else if (rdoClosedLinesAsPolygonsType.IsChecked.Value.Equals(true))
            {
                isoLineLayer.IsoLineType = IsoLineType.ClosedLinesAsPolygons;
            }
            else
            {
                isoLineLayer.IsoLineType = IsoLineType.LinesOnly;
            }

            //Create a series of colors from blue to red that we will use for the breaks
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColor.StandardColors.Blue, GeoColor.StandardColors.Red, isoLineLevels.Count, ColorWheelDirection.CounterClockwise);

            //Setup a class break style based on the isoline levels and the colors
            ClassBreakStyle classBreakStyle = new ClassBreakStyle(isoLineLayer.DataValueColumnName);

            Collection<ThinkGeo.MapSuite.Styles.Style> firstStyles = new Collection<ThinkGeo.MapSuite.Styles.Style>();
            firstStyles.Add(new LineStyle(new GeoPen(colors[0], 3)));
            firstStyles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[0]))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, firstStyles));
            for (int i = 0; i < colors.Count - 1; i++)
            {
                Collection<ThinkGeo.MapSuite.Styles.Style> styles = new Collection<ThinkGeo.MapSuite.Styles.Style>();
                styles.Add(new LineStyle(new GeoPen(colors[i + 1], 3)));
                styles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[i + 1]))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(isoLineLevels[i], styles));
            }
            isoLineLayer.CustomStyles.Add(classBreakStyle);

            //Create the text styles to label the lines
            TextStyle textStyle = TextStyles.CreateSimpleTextStyle(isoLineLayer.DataValueColumnName, "Arial", 8, DrawingFontStyles.Bold, GeoColor.StandardColors.Black, 0, 0);
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 2);
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            textStyle.TextLineSegmentRatio = 9999999;
            textStyle.FittingLineInScreen = true;
            textStyle.SuppressPartialLabels = true;
            isoLineLayer.CustomStyles.Add(textStyle);

            return isoLineLayer;
        }

        private InMemoryGridFeatureLayer GetGridFeatureLayer()
        {
            //Get the line breaks
            Collection<double> isoLineBreaks = GridIsoLineLayer.GetIsoLineLevels(wellDepthPointData, Convert.ToInt32(txtGridIsoLineLevelCount.Text));

            //Load a new GridFeatureLayer based on the current grid file
            InMemoryGridFeatureLayer gridFeatureLayer = new InMemoryGridFeatureLayer(gridCellMatrix);

            //Create a series of colors from blue to red that we will use for the breaks
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColor.StandardColors.Blue, GeoColor.StandardColors.Red, isoLineBreaks.Count, ColorWheelDirection.CounterClockwise);

            //Create a class break style
            ClassBreakStyle classBreakLineStyle = new ClassBreakStyle(gridFeatureLayer.DataValueColumnName);

            //Setup a class break style based on the isoline levels and the colors
            AreaStyle firstStyle = new AreaStyle(new GeoPen(GeoColor.FromArgb(50, colors[0]), 1), new GeoSolidBrush(GeoColor.FromArgb(50, colors[0])));
            classBreakLineStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, firstStyle));
            for (int i = 1; i < colors.Count - 1; i++)
            {
                AreaStyle style = new AreaStyle(new GeoPen(GeoColor.FromArgb(50, colors[i]), 1), new GeoSolidBrush(GeoColor.FromArgb(50, colors[i])));
                classBreakLineStyle.ClassBreaks.Add(new ClassBreak(isoLineBreaks[i], style));
            }

            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakLineStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return gridFeatureLayer;
        }

        private InMemoryFeatureLayer GetWellDepthPointLayer()
        {
            //Create an in memory layer to hold the well data from the text file
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.FeatureSource.Open();
            //Make sure to specify the depth column
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn("Depth", "String", 10));

            //Loop through all the point data and add it to the in memory layer
            foreach (KeyValuePair<PointShape, double> wellDepth in wellDepthPointData)
            {
                Feature feature = new Feature(wellDepth.Key);
                feature.ColumnValues.Add("Depth", wellDepth.Value.ToString());
                inMemoryFeatureLayer.InternalFeatures.Add(feature);
            }
            //Now that all of the data is added we can build an in memory index to make the lookups fast
            inMemoryFeatureLayer.BuildIndex();

            //Create the well point style
            PointStyle pointStyle1 = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.White, 4, GeoColor.SimpleColors.Black, 2);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle1);

            //Create the text style with a halo
            TextStyle textStyle = TextStyles.CreateSimpleTextStyle("Depth", "Arial", 10, DrawingFontStyles.Regular, GeoColor.SimpleColors.Black);
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 3);
            textStyle.PointPlacement = PointPlacement.UpperCenter;
            textStyle.YOffsetInPixel = 5;

            //Apply these styles at all levels and add then to the custom styles for the layer
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            return inMemoryFeatureLayer;
        }

        private DynamicIsoLineLayer GetDynamicIsoLineLayer()
        {
            Collection<double> isoLineLevels = GridIsoLineLayer.GetIsoLineLevels(wellDepthPointData, Convert.ToInt32(txtGridIsoLineLevelCount.Text));

            //Create the new dynamicIsoLineLayer using the well data and the number of isoline levels from
            //the screen
            DynamicIsoLineLayer dynamicIsoLineLayer;
            if (rdoLinesOnlyType.IsChecked.Value.Equals(true))
            {
                dynamicIsoLineLayer = new DynamicIsoLineLayer(wellDepthPointData, isoLineLevels, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue), IsoLineType.LinesOnly);
            }
            else if (rdoClosedLinesAsPolygonsType.IsChecked.Value.Equals(true))
            {
                dynamicIsoLineLayer = new DynamicIsoLineLayer(wellDepthPointData, isoLineLevels, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue), IsoLineType.ClosedLinesAsPolygons);
            }
            else
            {
                dynamicIsoLineLayer = new DynamicIsoLineLayer(wellDepthPointData, isoLineLevels, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue), IsoLineType.LinesOnly);
            }
            dynamicIsoLineLayer.CellHeightInPixel = (int)(Map1.ActualHeight / int.Parse(txtDynamicIsoLineCellRowCount.Text));
            dynamicIsoLineLayer.CellWidthInPixel = (int)(Map1.ActualWidth / int.Parse(txtDynamicIsoLineCellColumnCount.Text));

            //Create a series of colors from blue to red that we will use for the breaks
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColor.StandardColors.Blue, GeoColor.StandardColors.Red, isoLineLevels.Count, ColorWheelDirection.Clockwise);

            //Setup a class break style based on the isoline levels and the colors
            ClassBreakStyle classBreakStyle = new ClassBreakStyle(dynamicIsoLineLayer.DataValueColumnName);

            Collection<ThinkGeo.MapSuite.Styles.Style> firstStyles = new Collection<ThinkGeo.MapSuite.Styles.Style>();
            firstStyles.Add(new LineStyle(new GeoPen(colors[0], 3)));
            firstStyles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[0]))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, firstStyles));
            for (int i = 0; i < colors.Count - 1; i++)
            {
                Collection<ThinkGeo.MapSuite.Styles.Style> styles = new Collection<ThinkGeo.MapSuite.Styles.Style>();
                styles.Add(new LineStyle(new GeoPen(colors[i + 1], 3)));
                styles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.LightBlue, 3), new GeoSolidBrush(new GeoColor(150, colors[i + 1]))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(isoLineLevels[i], styles));
            }
            dynamicIsoLineLayer.CustomStyles.Add(classBreakStyle);

            //Create the text styles to label the lines
            TextStyle textStyle = TextStyles.CreateSimpleTextStyle(dynamicIsoLineLayer.DataValueColumnName, "Arial", 8, DrawingFontStyles.Bold, GeoColor.StandardColors.Black, 0, 0);
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 2);
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.UnlimitedDuplicateLabels;
            textStyle.TextLineSegmentRatio = 9999999;
            textStyle.FittingLineInScreen = true;
            textStyle.SuppressPartialLabels = true;
            dynamicIsoLineLayer.CustomStyles.Add(textStyle);

            return dynamicIsoLineLayer;
        }

        private DynamicGridFeatureLayer GetDynamicGridFeatureLayer()
        {
            DynamicGridFeatureLayer dynamicGridFeatureLayer = new DynamicGridFeatureLayer(wellDepthPointData);
            dynamicGridFeatureLayer.CellHeightInPixel = Map1.ActualHeight / double.Parse(txtDynamicIsoLineCellRowCount.Text);
            dynamicGridFeatureLayer.CellWidthInPixel = Map1.ActualWidth / double.Parse(txtDynamicIsoLineCellColumnCount.Text);

            Collection<double> isoLineBreaks = GridIsoLineLayer.GetIsoLineLevels(wellDepthPointData, Convert.ToInt32(txtGridIsoLineLevelCount.Text));

            //Create a series of colors from blue to red that we will use for the breaks
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColor.StandardColors.Blue, GeoColor.StandardColors.Red, isoLineBreaks.Count, ColorWheelDirection.CounterClockwise);

            //Create a class break style
            ClassBreakStyle classBreakLineStyle = new ClassBreakStyle(dynamicGridFeatureLayer.DataValueColumnName);

            //Setup a class break style based on the isoline levels and the colors
            AreaStyle firstStyle = new AreaStyle(new GeoPen(GeoColor.FromArgb(50, colors[0]), 1), new GeoSolidBrush(GeoColor.FromArgb(50, colors[0])));
            classBreakLineStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, firstStyle));
            for (int i = 1; i < colors.Count - 1; i++)
            {
                AreaStyle style = new AreaStyle(new GeoPen(GeoColor.FromArgb(50, colors[i]), 1), new GeoSolidBrush(GeoColor.FromArgb(50, colors[i])));
                classBreakLineStyle.ClassBreaks.Add(new ClassBreak(isoLineBreaks[i], style));
            }

            dynamicGridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakLineStyle);
            dynamicGridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return dynamicGridFeatureLayer;
        }

        #region Control Events

        private void btnGenerateGridFile_Click(object sender, RoutedEventArgs e)
        {
            rdoClosedLinesAsPolygonsType.IsEnabled = true;
            btnGenerateGridFile.IsEnabled = true;

            //Close the previous isoLineLayer and get ready to replace it with a new one
            LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["isoLineOverlay"];
            layerOverlay.Layers["IsoLineLayer"].Close();
            layerOverlay.Layers["GridCellsLayer"].Close();

            layerOverlay.Layers["IsoLineLayer"] = GetGridIsoLineLayer();
            layerOverlay.Layers["GridCellsLayer"] = GetGridFeatureLayer();
            layerOverlay.Layers["GridCellsLayer"].IsVisible = cbxGridLine.IsChecked.Value;

            cbxGridLine.IsEnabled = true;
            layerOverlay.Refresh();
        }

        private void btnGenerateWithFixedBreaks_Click(object sender, RoutedEventArgs e)
        {
            rdoClosedLinesAsPolygonsType.IsEnabled = false;
            rdoLinesOnlyType.IsChecked = true;

            btnGenerateGridFile.IsEnabled = true;

            //Close the previous dynamicIsoLineLayer and get ready to replace it with a new one
            LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["isoLineOverlay"];
            layerOverlay.Layers["IsoLineLayer"].Close();

            //Set the layer to the new dynamicIsoLineLayer
            ((LayerOverlay)Map1.Overlays["isoLineOverlay"]).Layers["IsoLineLayer"] = GetDynamicIsoLineLayer();

            //Turn the grid cell layer off and disable the checkbox
            ((LayerOverlay)Map1.Overlays["isoLineOverlay"]).Layers["GridCellsLayer"] = GetDynamicGridFeatureLayer();
            //cbxGridLine.IsEnabled = false;
            //cbxGridLine.IsChecked = false;

            stopwatch.Reset();
            stopwatch.Start();

            layerOverlay.Refresh();
        }

        private void cbxGridLine_Checked(object sender, RoutedEventArgs e)
        {
            if (Map1.Overlays.Contains("isoLineOverlay"))
            {
                LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["isoLineOverlay"];
                layerOverlay.Layers["GridCellsLayer"].IsVisible = true;
                layerOverlay.Refresh();
            }
        }

        private void cbxGridLine_Unchecked(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["isoLineOverlay"];
            layerOverlay.Layers["GridCellsLayer"].IsVisible = false;
            layerOverlay.Refresh();
        }

        private void isoLineOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            stopwatch.Reset();
            stopwatch.Start();

            if (((LayerOverlay)Map1.Overlays["isoLineOverlay"]).Layers["IsoLineLayer"] is IsoLineLayer)
            {
                btnGenerateGridFile.IsEnabled = false;
            }
        }

        private void isoLineOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            stopwatch.Stop();
            txtElapseTime.Text = "ElapseTime: " + stopwatch.ElapsedMilliseconds.ToString() + "ms";

            if (((LayerOverlay)Map1.Overlays["isoLineOverlay"]).Layers["IsoLineLayer"] is IsoLineLayer)
            {
                btnGenerateGridFile.IsEnabled = true;
            }
        }

        #endregion Control Events
    }
}