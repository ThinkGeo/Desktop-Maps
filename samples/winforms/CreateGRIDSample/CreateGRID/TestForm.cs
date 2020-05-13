using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace  CreateGRID
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.Snow);

            //Background map with Open Street Map.
            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            winformsMap1.Overlays.Add(openStreetMapOverlay);

            //Applies class break style to show sample points of pH level of a field.
            ClassBreakStyle classBreakStyle = new ClassBreakStyle("PH");
            int Alpha = 180;
            int symbolSize = 10;
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.SimpleColors.Transparent), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 0, 0)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 128, 0)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 245, 210, 10)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 225, 255, 0)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 224, 251, 132)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 128, 255, 128)), symbolSize)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 0, 255, 0)), symbolSize)));

            ShapeFileFeatureLayer samplesLayer = new ShapeFileFeatureLayer(@"..\..\data\sample_ph_2.shp");
            samplesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            samplesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay gridOverlay = new LayerOverlay();
            gridOverlay.Layers.Add("GridFeatureLayer", samplesLayer);
            winformsMap1.Overlays.Add("GridFeatureOverlay", gridOverlay);

            samplesLayer.Open();
            winformsMap1.CurrentExtent = samplesLayer.GetBoundingBox();
            samplesLayer.Close();

            winformsMap1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = @"..\..\data\test.grd"; 
            GenerateGrid(filename);
            LoadGrid(filename);
        }

        private void GenerateGrid(string filename)
        {
            //Point based shapefile used to create the GRID (sample points of pH level of a field)
            ShapeFileFeatureLayer pointLayer = new ShapeFileFeatureLayer(@"..\..\data\sample_ph_2.shp");

            //Sets the extent of the grid based on the extent of the sample points shapefile and slightly larger.
            pointLayer.Open();
            RectangleShape gridExtent = pointLayer.GetBoundingBox();
            gridExtent.ScaleUp(5);

            //Gets the data (points with their pH value) to build the GRID.
            Collection<Feature> features = pointLayer.FeatureSource.GetAllFeatures(new string[] { "PH" });
            Dictionary<PointShape, double> dataPoints = new Dictionary<PointShape, double>();
            pointLayer.Close();

            foreach (Feature feature in features)
            {
                PointShape pointShape = (PointShape) feature.GetShape();
                double value = double.Parse(feature.ColumnValues["PH"]);
                dataPoints.Add(pointShape, value);
            }

            //Cell size based on the width of the extent divided by 100.
            double cellSize = gridExtent.Width / 100; 
            //Sets the definition of the GRID with its extent, the cell size, the non value, and the data (point locations with their value)
            //For more information on GRID definition see http://en.wikipedia.org/wiki/ASCII_GRID
            GridDefinition definition = new GridDefinition(gridExtent, cellSize, -9999, dataPoints);
            
            //Inverse Distance Weighted (IDW) is the interpolation model used for the GRID for assigning values to unknown points 
            //based on known neighbor points.
            //http://en.wikipedia.org/wiki/Inverse_distance_weighting
            GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel();

            FileStream outputStream = new FileStream(filename, FileMode.Create);
            GridFeatureSource.GenerateGrid(definition, interpolationModel, outputStream);
        }

        private void LoadGrid(string filename)
        {
            //Shows how to set the class breaks to display grid cell with color according to its value.
            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(filename);
            ClassBreakStyle gridClassBreakStyle = new ClassBreakStyle("CellValue");
            int Alpha = 150;
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColor.SimpleColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 0, 255, 0)))));
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = winformsMap1.Overlays["GridFeatureOverlay"] as LayerOverlay;
            if (layerOverlay.Layers.Contains("GridFeatureLayer"))
            {
                layerOverlay.Layers.Remove("GridFeatureLayer");
            }
            layerOverlay.Layers.Insert(0, "GridFeatureLayer", gridFeatureLayer);

            btnGRID.Enabled = false;

            winformsMap1.Refresh();
        }

        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
