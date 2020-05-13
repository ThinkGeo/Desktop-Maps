/*===========================================
   Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
   a Client ID and Secret. These were sent to you via email when you signed up
   with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Overlays
{
    public partial class Form1 : Form
    {
        private Dictionary<string, List<string>> baseMaps = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();

            baseMaps.Add("Cloud Maps", new List<string>() { "Light", "Dark", "Aerial", "Hybrid", "TransparentBackground" });
            baseMaps.Add("Google Maps", new List<string>() { "RoadMap", "Mobile", "Satellite", "Terrain", "Hybrid" });
            baseMaps.Add("Bing Maps", new List<string>() { "Road", "AerialWithLabels", "Aerial" });
            baseMaps.Add("Open Street Map", new List<string>() { "" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.CurrentExtent = new RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);

            GoogleMapsOverlay googleMapOverlay = new GoogleMapsOverlay();
            googleMapOverlay.IsVisible = false;
            winformsMap1.Overlays.Add("Google Maps", googleMapOverlay);
            cboBaseMap.Items.Add("Google Maps");

            BingMapsOverlay bingMapOverlay = new BingMapsOverlay();
            bingMapOverlay.ApplicationId = ""; //Please set your application id.
            bingMapOverlay.IsVisible = false;
            winformsMap1.Overlays.Add("Bing Maps", bingMapOverlay);
            cboBaseMap.Items.Add("Bing Maps");

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeoClientId", "ThinkGeoClientSecret");
            worldOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            worldOverlay.IsVisible = true;
            winformsMap1.Overlays.Add("Cloud Maps", worldOverlay);
            cboBaseMap.Items.Add("Cloud Maps");
            cboBaseMap.SelectedItem = "Cloud Maps";

            OpenStreetMapOverlay osmOverlay = new OpenStreetMapOverlay();
            osmOverlay.IsVisible = false;
            winformsMap1.Overlays.Add("Open Street Map", osmOverlay);
            cboBaseMap.Items.Add("Open Street Map");

            winformsMap1.Refresh();
        }

        private void SelectedBaseMapChanged(object sender, EventArgs e)
        {
            foreach (var overlay in winformsMap1.Overlays)
                overlay.IsVisible = false;

            var selectedOverlay = winformsMap1.Overlays[cboBaseMap.SelectedItem.ToString()];
            selectedOverlay.IsVisible = true;

            cboMapType.Items.Clear();
            var mapTypes = baseMaps[cboBaseMap.SelectedItem.ToString()];
            foreach (var mapType in mapTypes)
                cboMapType.Items.Add(mapType);

            cboMapType.SelectedIndex = 0;
        }

        private void SelectedMapTypeChanged(object sender, EventArgs e)
        {
            var selectedOverlay = winformsMap1.Overlays[cboBaseMap.SelectedItem.ToString()];

            switch (cboBaseMap.SelectedItem.ToString())
            {
                case "Cloud Maps":
                    var worldOverlay = selectedOverlay as ThinkGeoCloudRasterMapsOverlay;
                    worldOverlay.MapType = (ThinkGeoCloudRasterMapsMapType)Enum.Parse(typeof(ThinkGeoCloudRasterMapsMapType), cboMapType.SelectedItem.ToString());
                    break;
                case "Google Maps":
                    var googleMapsOverlay = selectedOverlay as GoogleMapsOverlay;
                    googleMapsOverlay.MapType = (GoogleMapsMapType)Enum.Parse(typeof(GoogleMapsMapType), cboMapType.SelectedItem.ToString());
                    break;
                case "Bing Maps":
                    var bingMapsOverlay = selectedOverlay as BingMapsOverlay;
                    bingMapsOverlay.MapType = (BingMapsMapType)Enum.Parse(typeof(BingMapsMapType), cboMapType.SelectedItem.ToString());
                    break;
                case "Open Street Map":
                default:
                    break;
            }

            winformsMap1.Refresh(selectedOverlay);
        }
    }
}
