/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/


using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeoCloudMapsSample
{
    public partial class Form1 : Form
    {
        ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            // Tiles will be cached in the TEMP folder (%USERPROFILE%\AppData\Local\Temp\MapSuite\PersistentCaches) by default if the TileCache property is not set.
            thinkGeoCloudMapsOverlay.TileCache = new XyzFileBitmapTileCache("ThinkGeoCloudMapsTileCache");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.CurrentExtent = new ThinkGeo.MapSuite.Shapes.RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);
            winformsMap1.Refresh();
        }

        private void MapTypeChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            thinkGeoCloudMapsOverlay.MapType = (ThinkGeoCloudRasterMapsMapType)Enum.Parse(typeof(ThinkGeoCloudRasterMapsMapType), radioButton.Text);
            winformsMap1.Refresh();
        }
    }
}
