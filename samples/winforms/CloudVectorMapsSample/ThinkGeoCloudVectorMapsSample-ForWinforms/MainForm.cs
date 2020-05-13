using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeoCloudVectorMapsOverlayOnlineSample_winform
{
    public partial class MainForm : Form
    {
        private const string cloudServiceClientId = "Your-ThinkGeo-Cloud-Service-Cliend-ID";    // Get it from https://cloud.thinkgeo.com
        private const string cloudServiceClientSecret = "Your-ThinkGeo-Cloud-Service-Cliend-Secret";

        private ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay;
        private ThinkGeoCloudMapsOverlay satelliteOVerlay;

        private BitmapTileCache bitmapTileCache;
        private VectorTileCache vectorTileCache;

        public MainForm()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.winformsMap.MapUnit = GeographyUnit.Meter;
            this.winformsMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            this.thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(cloudServiceClientId, cloudServiceClientSecret);

            // Create the overlay for satellite and overlap with trasparent_background as hybrid map.
            this.satelliteOVerlay = new ThinkGeoCloudMapsOverlay(cloudServiceClientId, cloudServiceClientSecret, ThinkGeoCloudMapsMapType.Aerial);
            this.satelliteOVerlay.IsVisible = false;

            this.winformsMap.Overlays.Add(this.satelliteOVerlay);
            this.winformsMap.Overlays.Add(this.thinkGeoCloudVectorMapsOverlay);

            // Set a proper extent for the map.
            this.winformsMap.CurrentExtent = new RectangleShape(-15854768.9660284, 8157197.18459117, -6002341.82291805, 1034489.1804359);

            this.winformsMap.Refresh();
        }

        private void radioButtonLight_CheckedChanged(object sender, EventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null && radioButtonLight.Checked)
            {
                // Hide Raster Overlay.
                this.satelliteOVerlay.IsVisible = false;
                // Set Vector Overlay Style as WorldStreets-Light.
                this.thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                // Refresh the map.
                this.winformsMap.Refresh();
            }
        }

        private void radioButtonDark_CheckedChanged(object sender, EventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null && radioButtonDark.Checked)
            {
                // Hide Raster Overlay.
                this.satelliteOVerlay.IsVisible = false;
                // Set Vector Overlay Style as WorldStreets-Dark.
                this.thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                // Refresh the map.
                this.winformsMap.Refresh();
            }
        }

        private void radioButtonHybrid_CheckedChanged(object sender, EventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null && radioButtonHybrid.Checked)
            {
                // Display Raster Overlay.
                this.satelliteOVerlay.IsVisible = true;
                // Set Vector Overlay Style as WorldStreets-TransparentBackground.
                this.thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                // Refresh the map.
                this.winformsMap.Refresh();
            }
        }

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            if (thinkGeoCloudVectorMapsOverlay != null && radioButtonCustom.Checked)
            {
                // Display Raster Overlay.
                this.satelliteOVerlay.IsVisible = false;
                // Set Vector Overlay Style as a stylejson uri, the overlay will display the style in stylejson.

                // Relative Path.
                var uri0 = new Uri("mutedblue.json", UriKind.Relative);
                //// Web Address.
                //var uri1 = new Uri("http://cdn.thinkgeo.com/worldstreets-styles/1.0.0/mutedblue.json");
                //// Absolute Path
                //var uri2 = new Uri("C:/temp/mutedblue.json");

                this.thinkGeoCloudVectorMapsOverlay.StyleJsonUri = uri0;
                // Refresh the map.
                this.winformsMap.Refresh();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the page about world streets styles.
            Process.Start(linkLabel1.Text.ToString());
        }
    }
}
