using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace GetZoomLevel
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightGoldenrodYellow);

            //Scale bar to have a graphic representation of the scale
            ScaleBarAdornmentLayer scaleBar = new ScaleBarAdornmentLayer();
            scaleBar.UnitFamily = UnitSystem.Metric;
            scaleBar.Location = AdornmentLocation.LowerLeft;
            winformsMap1.AdornmentOverlay.Layers.Add(scaleBar);

            //Set of 10 zoom levels from 1:200,00 to 1:250 the zoom level below being half in scale compare to the previous zoom level.
            //Notice that the scale parameter for ZoomLevel is the middle scale for that zoom level. The range of scale for the zoom level 
            //will be based on the scale values of the zoom level below and above.
            //As an academic reminder, the scale means the ratio on the map to the corresponding distance on the ground. So, a scale of 1:100,000 
            //means that one unit (for example one centimeter) on the map represents 100,000 units ( or 100,000 centimeters or 1 kilometers on the ground). 

            ZoomLevelSet partitionedZoomLevelSet = new ZoomLevelSet();
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(200000));//Zoom Level 1  1:200,000 (beyond 1:150,000)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(100000));//Zoom Level 2  1:100,000 (from 1:75,000 to 1:150,000)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(50000)); //Zoom Level 3  1: 50,000 (from 1:37,000 to 1:75,000)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(25000)); //Zoom Level 4  1: 25,000 (from 1:17,000 to 1:37,500)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(10000)); //Zoom Level 5  1: 10,000 (from 1:7,500 to 1:17,500)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(5000));  //Zoom Level 6  1:  5,000 (from 1:3,750 to 1:7,500)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(2500));  //Zoom Level 7  1:  2,500 (from 1:1,750 to 1:3,750)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(1000));  //Zoom Level 8  1:  1,000 (from 1:750 to 1:1,750)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(500));   //Zoom Level 9  1:    500 (from 1:375 to 1:750)
            partitionedZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(250));   //Zoom Level 10 1:    250 (below 1:375)
            winformsMap1.ZoomLevelSet = partitionedZoomLevelSet;


            //Uses the Name property of ZoomLevel to give the characteristics such as the scale range.
            Collection<ZoomLevel> zoomLevels = winformsMap1.ZoomLevelSet.GetZoomLevels();
            for (int i = 0; i < zoomLevels.Count; i++)
            {
                string ScaleRange;
                double BottomScale, TopScale;
                if (i == 0)
                {
                    BottomScale = zoomLevels[i + 1].Scale + ((zoomLevels[i].Scale - zoomLevels[i + 1].Scale) / 2);
                    ScaleRange = "Beyond 1: " + String.Format("{0:0,0}", BottomScale);
                }
                else if (i < zoomLevels.Count - 1)
                {
                    BottomScale = zoomLevels[i + 1].Scale + ((zoomLevels[i].Scale - zoomLevels[i + 1].Scale) / 2);
                    TopScale = zoomLevels[i].Scale + ((zoomLevels[i - 1].Scale - zoomLevels[i].Scale) / 2);
                    ScaleRange = "From 1: " + String.Format("{0:0,0}", BottomScale) + " to 1: " + String.Format("{0:0,0}", TopScale);
                }
                else
                {
                    TopScale = zoomLevels[i].Scale + ((zoomLevels[i - 1].Scale - zoomLevels[i].Scale) / 2);
                    ScaleRange = "Below 1: " + String.Format("{0:0,0}", TopScale);
                }


                zoomLevels[i].Name = "Zoom Level " + (i + 1).ToString() + "                        " + ScaleRange;
            }


            ShapeFileFeatureLayer layer1 = new ShapeFileFeatureLayer("../../data/Austinstreets.shp");
            //Sets the custom Zoom levels to ZoomLevelSet
            layer1.ZoomLevelSet = partitionedZoomLevelSet;
            //Set a different style at each zoom level to help distinguish when we change zoom level when zooming in and out on the map.
            layer1.ZoomLevelSet.CustomZoomLevels[0].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Red, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[1].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Green, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[2].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Pink, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[3].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Blue, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[4].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Orange, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[5].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Turquoise, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[6].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Violet, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[7].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.YellowGreen, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[8].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.Brown, 2, true);
            layer1.ZoomLevelSet.CustomZoomLevels[9].DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.DarkGreen, 2, true);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(layer1);
            winformsMap1.Overlays.Add(layerOverlay);

            layer1.Open();
            winformsMap1.CurrentExtent = layer1.GetBoundingBox();
            layer1.Close();

            winformsMap1.Refresh();

            lblZoom.Text = winformsMap1.ZoomLevelSet.GetZoomLevel(winformsMap1.CurrentExtent, winformsMap1.Width, winformsMap1.MapUnit).Name;
        }

        //Updates the zoom level info each time the map changes scale.
        private void winformsMap1_CurrentScaleChanged(object sender, CurrentScaleChangedWinformsMapEventArgs e)
        {
            lblZoom.Text = winformsMap1.ZoomLevelSet.GetZoomLevel(winformsMap1.CurrentExtent, winformsMap1.Width, winformsMap1.MapUnit).Name; // + 

            txtScale.Text = String.Format("{0:0,0}", winformsMap1.CurrentScale);
        }

        //For setting the map at the desired scale.
        private void txtScale_KeyPress(object sender, KeyPressEventArgs e)
        {
            double tempScale;
            if (e.KeyChar == (char)13 && double.TryParse(txtScale.Text, out tempScale))
            {
                tempScale = Math.Max(tempScale, winformsMap1.MinimumScale);
                tempScale = Math.Min(tempScale, winformsMap1.MaximumScale);
                winformsMap1.CurrentScale = tempScale;
                winformsMap1.Refresh();
            }
        }


        private void btnZoomToPrevious_Click(object sender, EventArgs e)
        {
            winformsMap1.ZoomToPreviousExtent();
            winformsMap1.Refresh();
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            winformsMap1.ToggleMapExtents();
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
