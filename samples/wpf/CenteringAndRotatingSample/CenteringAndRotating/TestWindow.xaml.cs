using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;


namespace CenteringAndRotating
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private StreamReader mGPSData = new StreamReader(@"..\..\data\GPSinfo1.txt");
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private double previousLong;
        private double previousLat;
        private RotationProjection rotateProjection = new RotationProjection();
        private int count;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-66.8532, 10.4947, -66.8511, 10.4932);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(243, 234, 167));

            LoadBackgroundLayers();
            LoadGPSlocations();

            //Make sure to call this function to apply the RotationProjection to all the verctor layers.
            ApplyRotationProjection();

            wpfMap1.Refresh();

            dispatcherTimer.Start();
        }

        private void ApplyRotationProjection()
        {
            foreach (LayerOverlay layerOverlay in wpfMap1.Overlays)
            {
                foreach (FeatureLayer featureLayer in layerOverlay.Layers)
                {
                    featureLayer.FeatureSource.Projection = rotateProjection;
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();

            LayerOverlay dynamicOverlay = (LayerOverlay)wpfMap1.Overlays["DynamicOverlay"];
            InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["GPSlocations"];

            PointShape currentPointShape = (PointShape)inMemoryFeatureLayer.InternalFeatures[count].GetShape();

            //Step 1: Center the map on the moving vehicle.
            PointShape rotatedPointShape = (PointShape)rotateProjection.ConvertToExternalProjection(currentPointShape);
            //Notice that we use Register method instead of CenterAt to avoid refreshing the map when centering.
            RectangleShape newExtent = (RectangleShape)wpfMap1.CurrentExtent.Register(wpfMap1.CurrentExtent.GetCenterPoint(), rotatedPointShape, DistanceUnit.Meter, GeographyUnit.Meter);

            //Step 2: Rotate the map based on the vehicle direction.
            //Gets the angle based on the current GPS position and the previous one to get the direction of the vehicle.
            double angle = GetAngleFromTwoVertices(new Vertex(previousLong, previousLat), new Vertex(currentPointShape.X, currentPointShape.Y));

            rotateProjection.Angle = angle;
            wpfMap1.CurrentExtent = rotateProjection.GetUpdatedExtent(newExtent);

            count += 1;

            if (count >= inMemoryFeatureLayer.InternalFeatures.Count) count = 0;

            previousLong = currentPointShape.X;
            previousLat = currentPointShape.Y;

            //Step 3: Update the location of the moving vehicle.
            InMemoryFeatureLayer carInMemoryFeatureLayer = (InMemoryFeatureLayer)dynamicOverlay.Layers["CarLayer"];

            PointShape pointShape = (PointShape)carInMemoryFeatureLayer.InternalFeatures[0].GetShape();
            pointShape.X = currentPointShape.X;
            pointShape.Y = currentPointShape.Y;
            pointShape.Id = "Car";

            carInMemoryFeatureLayer.InternalFeatures.Clear();
            carInMemoryFeatureLayer.InternalFeatures.Add(new Feature(pointShape));

            wpfMap1.Refresh();

            dispatcherTimer.Start();
        }

        private void LoadGPSlocations()
        {
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle
                                                       (GeoColor.FromArgb(100, GeoColor.StandardColors.Red), 10);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            string strLattitude = "";
            string strLongitude = "";

            // Read the next line from the text file with GPS data in it.
            string strCurrentText = mGPSData.ReadLine();

            if (strCurrentText == "")
            {
                mGPSData.BaseStream.Seek(0, SeekOrigin.Begin);
                strCurrentText = mGPSData.ReadLine();
            }

            while (strCurrentText != "") 
            {
                if (strCurrentText.Trim() != "/")
                {
                    string[] strSplit = strCurrentText.Split(','); 
                    strLongitude = strSplit[0];
                    strLattitude = strSplit[1];
                }
                strCurrentText = mGPSData.ReadLine();

                PointShape pointShape = new PointShape(Convert.ToDouble(strLongitude), Convert.ToDouble(strLattitude));

                Feature feature = new Feature(pointShape);
                inMemoryFeatureLayer.InternalFeatures.Add(new Feature(pointShape));

            }

            InMemoryFeatureLayer carInMemoryFeatureLayer = new InMemoryFeatureLayer();
            carInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Bitmap;
            carInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(@"..\..\data\sedan.png");
            carInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.RotationAngle = 90;
            carInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            carInMemoryFeatureLayer.InternalFeatures.Add("Car", new Feature(new PointShape()));

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.TileType = TileType.SingleTile;
            dynamicOverlay.Layers.Add("GPSlocations", inMemoryFeatureLayer);
            dynamicOverlay.Layers.Add("CarLayer", carInMemoryFeatureLayer);
            wpfMap1.Overlays.Add("DynamicOverlay", dynamicOverlay);
        }

        private void LoadBackgroundLayers()
        {
            ShapeFileFeatureLayer ParksShapeLayer = new ShapeFileFeatureLayer(@"..\..\Data\parks.shp");
            ShapeFileFeatureLayer.BuildIndexFile(@"..\..\Data\parks.shp", BuildIndexMode.DoNotRebuild);

            ParksShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            ParksShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = WorldStreetsAreaStyles.Park();

            ShapeFileFeatureLayer ParksLabelLayer = new ShapeFileFeatureLayer(@"..\..\Data\parks.shp");
            ParksLabelLayer.RequireIndex = false;
            ParksLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("Name", "Arial", 10, DrawingFontStyles.Bold, GeoColor.StandardColors.SeaGreen, GeoColor.StandardColors.White, 2);
            ParksLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer StreetsShapeLayer = new ShapeFileFeatureLayer(@"..\..\Data\streets.shp");
            ShapeFileFeatureLayer.BuildIndexFile(@"..\..\Data\streets.shp", BuildIndexMode.DoNotRebuild);

            StreetsShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "Type";
            valueStyle.ValueItems.Add(new ValueItem("A", WorldStreetsLineStyles.RoadFill(10f)));
            valueStyle.ValueItems.Add(new ValueItem("M", WorldStreetsLineStyles.RoadFill(10f)));
            StreetsShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            StreetsShapeLayer.FeatureSource.Projection = rotateProjection;

            ShapeFileFeatureLayer StreetsLabelLayer = new ShapeFileFeatureLayer(@"..\..\Data\streets.shp");
            StreetsLabelLayer.RequireIndex = false;
            TextStyle textStyle = new TextStyle();
            textStyle.TextColumnName = "St_name";
            textStyle.TextSolidBrush = new GeoSolidBrush(GeoColor.StandardColors.Black);
            textStyle.Font = new GeoFont("Arial", 7);
            textStyle.TextLineSegmentRatio = 10;
            textStyle.GridSize = 100;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.OneDuplicateLabelPerQuadrant;
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.GridSize = 5;
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            StreetsLabelLayer.FeatureSource.Projection = rotateProjection;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add("ParksShapeLayer", ParksShapeLayer);
            staticOverlay.Layers.Add("StreetsShapeLayer", StreetsShapeLayer);
            staticOverlay.Layers.Add("ParksLabelLayer", ParksLabelLayer);
            staticOverlay.Layers.Add("StreetsLabelLayer", StreetsLabelLayer);

            wpfMap1.Overlays.Add("StaticOverlay", staticOverlay);
        }

        private double GetAngleFromTwoVertices(Vertex b, Vertex c)
        {
            double alpha = 0;
            double tangentAlpha = (c.Y - b.Y) / (c.X - b.X);
            double Peta = Math.Atan(tangentAlpha);

            if (c.X > b.X)
            {
                alpha = 90 - (Peta * (180 / Math.PI));
            }
            else if (c.X < b.X)
            {
                alpha = 270 - (Peta * (180 / Math.PI));
            }
            else
            {
                if (c.Y > b.Y) alpha = 0;
                if (c.Y < b.Y) alpha = 180;
            }
            return alpha;
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            PointShape unrotatedPointShape = (PointShape)rotateProjection.ConvertToInternalProjection(pointShape);

            textBox1.Text = "X: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(unrotatedPointShape.X) +
                          "  Y: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(unrotatedPointShape.Y);

        }
    }
}
