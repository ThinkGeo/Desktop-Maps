using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Geocoding
{
    public partial class GetClosestStreetNumberInChicago : UserControl
    {
        public GetClosestStreetNumberInChicago()
        {
            InitializeComponent();
        }

        private void GetClosestStreetNumberInChicago_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Chicago extent
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-88.3330001640625, 42.5966329101563, -86.9157638359375, 41.1629170898438);

            // Setup the World Map Kit WMS Overlay
            WorldMapKitWmsDesktopOverlay worldMapKitOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add("WorldMapKitOverlay", worldMapKitOverlay);

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
            string dataPath = Path.Combine(SampleHelper.GetDataPath("ChicagoData"));
            UsaGeocoder usaGeocoder = new UsaGeocoder();

            if (chkGetClosestStreetNumber.Checked)
            {
                usaGeocoder = new UsaGeocoder(dataPath, StreetNumberMatchingMode.ClosestMatch);
            }
            else
            {
                usaGeocoder = new UsaGeocoder(dataPath, StreetNumberMatchingMode.Default);
            }

            // Open the geocoder, get any matches and close it
            Collection<GeocoderMatch> matchResult;
            try
            {
                usaGeocoder.Open();
                matchResult = usaGeocoder.Match(cboSourceText.Text);
            }
            finally
            {
                usaGeocoder.Close();
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
                if (matchItem.NameValuePairs.ContainsKey("Street"))
                {
                    if (!lstResult.Items.Contains(new ListItem(matchItem, new string[] { "Street", "Zip", "State" })))
                    {
                        lstResult.Items.Add(new ListItem(matchItem, new string[] { "Street", "Zip", "State" }));
                    }
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
            // Get the centroid point from the WKT string in the MatchPair
            if (!string.IsNullOrEmpty(matchItem.NameValuePairs["CentroidPoint"]))
            {
                markerLayer.MarkerLocation = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);
            }

            // Set the extent that is from the WKT string in the MatchPair and refresh the map
            if (!string.IsNullOrEmpty(matchItem.NameValuePairs["BoundingBox"]))
            {
                winformsMap1.CurrentExtent = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
            }
            winformsMap1.Refresh();
        }
    }
}
