﻿/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Geocoding
{
    public partial class IpAddressMatchingPlugIn : UserControl
    {
        public IpAddressMatchingPlugIn()
        {
            InitializeComponent();

            cboSourceText.Text = cboSourceText.Items[0].ToString();
        }

        private void IpAddressMatchingPlugIn_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Texas extent
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-9833185, 5250775, -9675419, 5036402);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Setup the marker overlay and add it to the map            
            LayerOverlay markerOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            // Create and add the marker layer to the marker overlay
            MarkerLayer markerLayer = new MarkerLayer();
            markerOverlay.Layers.Add("MarkerLayer", markerLayer);

            winformsMap1.Refresh();       
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the path to the data files and create the Geocoder
            string dataPath = Path.Combine(SampleHelper.GetDataPath("IpAddress"));
            Geocoder geocoder = new Geocoder(dataPath);
            geocoder.MatchingPlugIns.Add(new IPAddressMatchingPlugin(dataPath));

            // Open the geocoder, get any matches and close it
            Collection<GeocoderMatch> matchResult;
            try
            {
                geocoder.Open();
                matchResult = geocoder.Match(cboSourceText.Text);
            }
            finally
            {
                geocoder.Close();
            }

            PopulateAddressResultList(matchResult);
        }

        private void PopulateAddressResultList(Collection<GeocoderMatch> matchResult)
        {
            // Clear the results
            lstResult.Items.Clear();
            dataGridViewDetail.Rows.Clear();

            // Load the matching items into the grid
            foreach (GeocoderMatch matchItem in matchResult)
            {
                if (matchItem.NameValuePairs.ContainsKey("IP"))
                {
                    lstResult.Items.Add(new ListItem(matchItem, new string[] { "IP", "Country"}));
                }
            }

            // If we find addresses then select the first one to zoom in, if not then say we did not find any
            if (lstResult.Items.Count > 0)
            {
                lstResult.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Address can not be found!", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lstResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate the address grid based on the selected address
            GeocoderMatch matchItem = ((ListItem)lstResult.SelectedItem).MatchItem;
            dataGridViewDetail.Rows.Clear();

            foreach (KeyValuePair<string, string> item in matchItem.NameValuePairs)
            {
                dataGridViewDetail.Rows.Add(new object[] { item.Key, item.Value });
            }

            // Set the marker location to the address selected
            LayerOverlay markerOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
            MarkerLayer markerLayer = markerOverlay.Layers["MarkerLayer"] as MarkerLayer;

            Proj4Projection proj4Projection = new Proj4Projection(4326, 3857);
            proj4Projection.Open();
            // Get the centroid point from the WKT string in the MatchPair
            if (!string.IsNullOrEmpty(matchItem.NameValuePairs["CentroidPoint"]))
            {
                var point = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);
                markerLayer.MarkerLocation = (PointShape)proj4Projection.ConvertToExternalProjection(point);
            }

            // Set the extent that is from the WKT string in the MatchPair and refresh the map
            if (!string.IsNullOrEmpty(matchItem.NameValuePairs["BoundingBox"]))
            {
                var rectangle = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
                winformsMap1.CurrentExtent = proj4Projection.ConvertToExternalProjection(rectangle);
            }
            winformsMap1.Refresh();
        }
    }
}