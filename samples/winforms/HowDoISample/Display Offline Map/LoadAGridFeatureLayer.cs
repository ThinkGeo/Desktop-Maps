using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAGridFeatureLayer : UserControl
    {
        public LoadAGridFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //Load the well depth points and depth data from a text file into the dictionary
            //We cache this at the class level to prevent form loading it multiple times
            string wellDepthPointDataFilePath = SampleHelper.Get("GrayCountyIrrigationWellDepths.csv");
            Dictionary<PointShape, double> wellDepthPointData = GetWellDepthPointDataFromCSV(wellDepthPointDataFilePath);

            //Get the current extent since we use that to gerenate the grid.  Of course this is just for
            //the demo and in the real world you can use any extent you like
            RectangleShape currentDrawingExtent = MapUtil.GetDrawingExtent(mapView.CurrentExtent, (float)mapView.ActualWidth, (float)mapView.ActualHeight);

            //Calculate the cell size based on how many rows and columns you specified
            double cellSize = Math.Min(currentDrawingExtent.Width / 4, currentDrawingExtent.Height / 4);

            //Greate the grid definition based on the extent, cell size etc.
            GridDefinition gridDefinition = new GridDefinition(currentDrawingExtent, cellSize, -9999, wellDepthPointData);

            //Generate the grid based on Inverse Distance Weighted interpolation model.  You can define your own model if needed.
            GridCell[,] gridCellMatrix = GridFeatureSource.GenerateGridMatrix(gridDefinition, new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue));

            //Load a new GridFeatureLayer based on the current grid file
            InMemoryGridFeatureLayer gridFeatureLayer = new InMemoryGridFeatureLayer(gridCellMatrix);

            gridFeatureLayer.Open();
            var currentExtent = gridFeatureLayer.GetBoundingBox();
            gridFeatureLayer.Close();

            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Add the grid layer, the grid cells, and the well points to the map
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(gridFeatureLayer);

            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = currentExtent;
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.Overlays.Add(layerOverlay);
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
        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}