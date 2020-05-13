/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace CenteringOnMovingVehicleWithTolerance
{
    public partial class TestForm : Form
    {
        private PointShape[] carGPSPositions;
        private int carGPSPositionIndexer = -1;
        private Timer timer;
        private double previousLong;
        private double previousLat;

        public TestForm()
        {
            InitializeComponent();
            carGPSPositions = ReadCarGPSPositions();
            timer = new Timer();
            timer.Interval = 800;
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-10882493, 3543795, -10879443, 3541706);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //InMemoryFeatureLayer for vehicle.
            InMemoryFeatureLayer carLayer = new InMemoryFeatureLayer();
            carLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Bitmap;
            carLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(@"../../data/Sedan.png");
            carLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.RotationAngle = 45;
            carLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            carLayer.InternalFeatures.Add(new Feature(new PointShape() { Id = "Car" }));

            LayerOverlay vehicleOverlay = new LayerOverlay();
            vehicleOverlay.Layers.Add("CarLayer", carLayer);
            winformsMap1.Overlays.Add("VehicleOverlay", vehicleOverlay);

            //InMemoryFeatureLayer for Tolerance RectangleShape. (Displayed for the purpose of the project. It does not need to be displayed for a real world application)
            InMemoryFeatureLayer toleranceLayer = new InMemoryFeatureLayer();
            toleranceLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Transparent, GeoColor.StandardColors.Green, 2);
            toleranceLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Uses a Rectangle 60% smaller than the current extent of the map for the tolerance.
            RectangleShape toleranceRectangleShape = new RectangleShape(winformsMap1.CurrentExtent.UpperLeftPoint, winformsMap1.CurrentExtent.LowerRightPoint)
            {
                Id = "Tolerance"
            };
            toleranceRectangleShape.ScaleDown(60);
            toleranceLayer.InternalFeatures.Add(new Feature(toleranceRectangleShape));

            LayerOverlay toleranceOverlay = new LayerOverlay();
            toleranceOverlay.Layers.Add("ToleranceLayer", toleranceLayer);
            winformsMap1.Overlays.Add("ToleranceOverlay", toleranceOverlay);

            winformsMap1.Refresh();

            timer.Start();
        }


        void Timer_Tick(object sender, EventArgs e)
        {
            LayerOverlay vehicleOverlay = (LayerOverlay)winformsMap1.Overlays["VehicleOverlay"];
            InMemoryFeatureLayer carLayer = vehicleOverlay.Layers["CarLayer"] as InMemoryFeatureLayer;
            PointShape pointShape = carLayer.InternalFeatures[0].GetShape() as PointShape;

            //Gets the next GPS info 
            var carNextPosition = GetNextCarPosition();

            double lng = carNextPosition.X;
            double Lat = carNextPosition.Y;

            if (previousLong == 0)
            {
                previousLong = lng;
                previousLat = Lat;
            }

            //Gets the angle based on the current GPS position and the previous one to get the direction of the vehicle.
            double angle = GetAngleFromTwoVertices(new Vertex(previousLong, previousLat), new Vertex(lng, Lat));

            carLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.RotationAngle = 90 - (float)angle;

            pointShape.X = lng;
            pointShape.Y = Lat;
            pointShape.Id = "Car";

            carLayer.Open();
            carLayer.EditTools.BeginTransaction();
            carLayer.EditTools.Update(pointShape);
            carLayer.EditTools.CommitTransaction();
            carLayer.Close();

            previousLong = lng;
            previousLat = Lat;

            //Function to center the map to a point (the moving moving vehicle feature)if it goes outside the tolerance.
            RectangleShape toleranceRectangleShape = new RectangleShape(winformsMap1.CurrentExtent.UpperLeftPoint, winformsMap1.CurrentExtent.LowerRightPoint);
            toleranceRectangleShape.ScaleDown(60);
            if (toleranceRectangleShape.Contains(new PointShape(lng, Lat)) == false)
            {
                winformsMap1.CenterAt(new PointShape(lng, Lat));

                //Resets the RectangleShape of the tolerance layer (for displaying only)
                LayerOverlay toleranceOverlay = (LayerOverlay)winformsMap1.Overlays["ToleranceOverlay"];
                InMemoryFeatureLayer toleranceLayer = toleranceOverlay.Layers["ToleranceLayer"] as InMemoryFeatureLayer;

                RectangleShape newToleranceRectangleShape = new RectangleShape(winformsMap1.CurrentExtent.UpperLeftPoint, winformsMap1.CurrentExtent.LowerRightPoint);
                newToleranceRectangleShape.ScaleDown(60);
                newToleranceRectangleShape.Id = "Tolerance";

                toleranceLayer.Open();
                toleranceLayer.EditTools.BeginTransaction();
                toleranceLayer.EditTools.Update(newToleranceRectangleShape);
                toleranceLayer.EditTools.CommitTransaction();
                toleranceLayer.Close();

                winformsMap1.Refresh();
            }
            else
            {
                winformsMap1.Refresh(vehicleOverlay);
            }
        }

        private void WinformsMap1_MouseMove(object sender, MouseEventArgs e)
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


        //We assume that the angle is based on a third point that is on top of b on the same x axis.
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

        private PointShape GetNextCarPosition()
        {
            carGPSPositionIndexer++;
            carGPSPositionIndexer %= carGPSPositions.Length;
            var result = carGPSPositions[carGPSPositionIndexer];
            return result;
        }

        private PointShape[] ReadCarGPSPositions()
        {
            var positionTexts = File.ReadAllLines(@"../../data/GPSinfo.txt");
            var pointList = new List<PointShape>(positionTexts.Length);
            foreach (var positionText in positionTexts)
            {
                string[] strSplit = positionText.Split(',');
                if (strSplit.Length == 2 && double.TryParse(strSplit[0], out var lng) && double.TryParse(strSplit[1], out var lat))
                {
                    pointList.Add(new PointShape(lng, lat));
                }
            }
            return pointList.ToArray();
        }
    }
}
