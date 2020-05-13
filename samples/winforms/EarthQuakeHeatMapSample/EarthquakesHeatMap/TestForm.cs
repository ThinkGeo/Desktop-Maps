/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace EarthQuakesHeatMap
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
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-13914936, 5942074, -7458406, 2875745);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //Point based ShapeFileFeatureSource on earth quakes used for the HeatOverlay.
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(@"../../data/quksigx020.shp");
            //Creates the HeatOverlay with the point based ShapeFileFeatureSource for earthquakes.
            HeatLayer heatLayer = new HeatLayer(featureSource);
            //Creates the HeatStyle to set the properties determining the display of the heat map with earth quake data.
            //Notice that each point is treated with an intensity depending on the value of the column "other_magn1".
            //So, in addition to the density of points location, the value for each point is also going to be counted into account
            //for the coloring of the map.
            HeatStyle heatStyle = new HeatStyle();
            heatStyle.IntensityColumnName = "other_mag1";
            heatStyle.IntensityRangeStart = 0;
            heatStyle.IntensityRangeEnd = 12;
            //Sets the size of each point 100 kilometers of diameter.
            heatStyle.Alpha = 180;
            heatStyle.PointRadius = 60;
            heatStyle.PointRadiusUnit = DistanceUnit.Kilometer;

            heatLayer.HeatStyle = heatStyle;

            LayerOverlay heatMapOverlay = new LayerOverlay();
            heatMapOverlay.Layers.Add(heatLayer);

            winformsMap1.Overlays.Add("HeatOverlay", heatMapOverlay);

            winformsMap1.Refresh();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
