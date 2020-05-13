/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Configuration;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace HereRealTimeTraffic
{
    public partial class Form1 : Form
    {
        private readonly string AppId = ConfigurationManager.AppSettings["AppId"];
        private readonly string AppCode = ConfigurationManager.AppSettings["AppCode"];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            var overlay = new LayerOverlay();
            var hereLayer = new HereMapRealTimeTrafficLayer(new Uri("https://traffic.cit.api.here.com/traffic/6.2/flow.json"), AppId, AppCode);
            hereLayer.FeatureSource.Projection = new Proj4Projection( 4326, 3857);
            overlay.Layers.Add(hereLayer);
            winformsMap1.Overlays.Add(overlay);

            winformsMap1.CurrentExtent = new RectangleShape(964029, 6469449, 981151, 6452236);
            winformsMap1.Refresh();
        }
    }
}
