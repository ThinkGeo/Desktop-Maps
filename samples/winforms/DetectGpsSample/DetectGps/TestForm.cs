/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace DetectGPSDesktop
{
    public partial class TestForm : Form
    {
        Collection<PointShape> GPSlocations = new Collection<PointShape>();
        int index;
        private Timer timer;

        public TestForm()
        {
            InitializeComponent();
            timer = new Timer();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-10608547.0975155, 4716401.65832099, -10606999.7565935, 4715227.75695909);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(backgroundOverlay);

            //InMemoryFeatureLayer for the two reference points en red.
            InMemoryFeatureLayer pointLayer = new InMemoryFeatureLayer();
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Red, 12, GeoColor.StandardColors.Black);
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Feature feature1 = new Feature(new PointShape(-10608179.7431959, 4715514.0618386));
            Feature feature2 = new Feature(new PointShape(-10607445.0345567, 4715514.0618386));

            pointLayer.InternalFeatures.Add(feature1);
            pointLayer.InternalFeatures.Add(feature2);

            LayerOverlay pointOverlay = new LayerOverlay();
            pointOverlay.Layers.Add("PointLayer", pointLayer);
            winformsMap1.Overlays.Add("PointOverlay", pointOverlay);

            //--------------------------------------------------
            //Collection of PointShapes for the GPS locations.
            GPSlocations.Add(new PointShape(-95.2946, 38.9602));
            GPSlocations.Add(new PointShape(-95.2928, 38.9602));
            GPSlocations.Add(new PointShape(-95.2903, 38.9602));
            GPSlocations.Add(new PointShape(-95.2883, 38.9602));
            GPSlocations.Add(new PointShape(-95.2883, 38.9587));
            GPSlocations.Add(new PointShape(-95.2883, 38.957));
            GPSlocations.Add(new PointShape(-95.2897, 38.957));
            GPSlocations.Add(new PointShape(-95.2918, 38.957));
            GPSlocations.Add(new PointShape(-95.2934, 38.957));
            GPSlocations.Add(new PointShape(-95.2947, 38.957));
            GPSlocations.Add(new PointShape(-95.2946, 38.9582));
            GPSlocations.Add(new PointShape(-95.2946, 38.9593));

            InMemoryFeatureLayer vehicleLayer = new InMemoryFeatureLayer();
            vehicleLayer.FeatureSource.Projection = new Proj4Projection(4326, 3857);
            vehicleLayer.Open();
            vehicleLayer.Columns.Add(new FeatureSourceColumn("DETECT", "string", 3));
            vehicleLayer.Close();

            // Draw features based on values
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "DETECT";
            valueStyle.ValueItems.Add(new ValueItem("No", new PointStyle(new GeoImage("../../data/Top.png"))));
            valueStyle.ValueItems.Add(new ValueItem("yes", new PointStyle(new GeoImage("../../data/Top2.png"))));
            vehicleLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            vehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Feature GPSfeature = new Feature(GPSlocations[0]);
            GPSfeature.ColumnValues.Add("DETECT", "No");
            vehicleLayer.InternalFeatures.Add("GPSFeature1", GPSfeature);

            LayerOverlay vehicleOverlay = new LayerOverlay();
            vehicleOverlay.Layers.Add("VehicleLayer", vehicleLayer);
            winformsMap1.Overlays.Add("VehicleOverlay", vehicleOverlay);

            //Sets timer properties
            timer.Interval = 1500;
            timer.Tick += new EventHandler(timer_Tick);

            winformsMap1.Refresh();

            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (index == GPSlocations.Count - 1) { index = 0; }
            else { index = index + 1; }

            LayerOverlay vehicleOverlay = (LayerOverlay)winformsMap1.Overlays["VehicleOverlay"];
            InMemoryFeatureLayer vehicleLayer = (InMemoryFeatureLayer)vehicleOverlay.Layers["VehicleLayer"];
            Feature feature1 = vehicleLayer.InternalFeatures["GPSFeature1"];
            PointShape pointShape = feature1.GetShape() as PointShape;

            double lon = GPSlocations[index].X;
            double lat = GPSlocations[index].Y;

            pointShape.X = lon;
            pointShape.Y = lat;
            pointShape.Id = "GPSFeature1";

            //If the new GPS location is within 200 meters of a reference point, it displays with a different icon.
            LayerOverlay pointOverlay = (LayerOverlay)winformsMap1.Overlays["PointOverlay"];
            InMemoryFeatureLayer pointLayer = (InMemoryFeatureLayer)pointOverlay.Layers["PointLayer"];
            pointLayer.Open();
            Collection<Feature> features = pointLayer.QueryTools.GetFeaturesWithinDistanceOf(new PointShape(lon, lat), winformsMap1.MapUnit,
                                                                            DistanceUnit.Meter, 200, ReturningColumnsType.NoColumns);
            pointLayer.Close();

            if (features.Count > 0)
            {
                feature1.ColumnValues["DETECT"] = "Yes";
            }
            else
            {
                feature1.ColumnValues["DETECT"] = "No";
            }

            vehicleLayer.Open();
            vehicleLayer.EditTools.BeginTransaction();
            vehicleLayer.EditTools.Update(pointShape);
            vehicleLayer.EditTools.CommitTransaction();
            vehicleLayer.Close();

            winformsMap1.Refresh(vehicleOverlay);
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
