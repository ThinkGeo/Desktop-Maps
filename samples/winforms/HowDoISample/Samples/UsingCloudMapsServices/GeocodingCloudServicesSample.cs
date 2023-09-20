using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GeocodingCloudServicesSample: UserControl
    {
        private GeocodingCloudClient geocodingCloudClient;
        public GeocodingCloudServicesSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a marker overlay to display the geocoded locations that will be generated, and add it to the map
            MarkerOverlay geocodedLocationsOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Geocoded Locations Overlay", geocodedLocationsOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the GeocodingCloudClient using our ThinkGeo Cloud credentials
            geocodingCloudClient = new GeocodingCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            cboSearchType.SelectedIndex = 0;
            cboLocationType.SelectedIndex = 0;

            await mapView.RefreshAsync();
        }

        private async Task<CloudGeocodingResult> PerformGeocodingQuery()
        {
            // Show a loading graphic to let users know the request is running
            //loadingImage.Visibility = Visibility.Visible;
            
            CloudGeocodingOptions options = new CloudGeocodingOptions();

            // Set up the CloudGeocodingOptions object based on the parameters set in the UI
            options.MaxResults = int.Parse(txtMaxResults.Text);
            options.SearchMode = cboSearchType.SelectedItem.ToString() == "Fuzzy" ? CloudGeocodingSearchMode.FuzzyMatch : CloudGeocodingSearchMode.ExactMatch;
            options.LocationType = (CloudGeocodingLocationType)Enum.Parse(typeof(CloudGeocodingLocationType), cboLocationType.SelectedItem.ToString());
            options.ResultProjectionInSrid = 3857;

            // Run the geocode
            string searchString = txtSearchString.Text.Trim();
            CloudGeocodingResult searchResult = await geocodingCloudClient.SearchAsync(searchString, options);

            // Hide the loading graphic
            //loadingImage.Visibility = Visibility.Hidden;

            return searchResult;
        }

        /// <summary>
        /// Update the UI based on the results of a Cloud Geocoding Query
        /// </summary>
        private async Task UpdateSearchResultsOnUIAsync(CloudGeocodingResult searchResult)
        {
            // Clear the locations list and existing location markers on the map
            SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];
            geocodedLocationOverlay.Markers.Clear();
            lsbLocations.DataSource = null;
            await geocodedLocationOverlay.RefreshAsync();

            // Update the UI with the number of results found and the list of locations found
            txtSearchResultsDescription.Text = $"Found {searchResult.Locations.Count} matching locations.";
            lsbLocations.DataSource = searchResult.Locations;
            lsbLocations.DisplayMember = "LocationName";
            if (searchResult.Locations.Count > 0)
            {
                //lsbLocations.Visibility = Visibility.Visible;
                lsbLocations.SelectedIndex = 0;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // Perform some simple validation on the input text boxes
            if (ValidateSearchParameters())
            {
                // Run the Cloud Geocoding query
                CloudGeocodingResult searchResult = await PerformGeocodingQuery();

                // Handle an error returned from the geocoding service
                if (searchResult.Exception != null)
                {
                    MessageBox.Show(searchResult.Exception.Message, "Error");
                    return;
                }

                // Update the UI based on the results
                await UpdateSearchResultsOnUIAsync(searchResult);
            }
        }

        private async void lsbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected location
            var chosenLocation = lsbLocations.SelectedItem as CloudGeocodingLocation;
            if (chosenLocation != null)
            {
                // Get the MarkerOverlay from the MapView
                SimpleMarkerOverlay geocodedLocationOverlay = (SimpleMarkerOverlay)mapView.Overlays["Geocoded Locations Overlay"];

                // Clear the existing markers and add a new marker at the chosen location
                geocodedLocationOverlay.Markers.Clear();
                geocodedLocationOverlay.Markers.Add(CreateNewMarker(chosenLocation.LocationPoint));

                // Center the map on the chosen location
                mapView.CurrentExtent = chosenLocation.BoundingBox;
                ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
                await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                await mapView.RefreshAsync();
            }
        }

        private void cboSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxContent = cboSearchType.SelectedItem.ToString();

            if (comboBoxContent != null)
            {
                switch (comboBoxContent)
                {
                    case "Fuzzy":
                        txtSearchTypeDescription.Text = "(Returns both exact and approximate matches for the search address)";
                        break;
                    case "Exact":
                        txtSearchTypeDescription.Text = "(Only returns exact matches for the search address)";
                        break;
                    default:
                        break;
                }
            }
        }

        private void cboLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxContent = cboLocationType.SelectedItem.ToString();

            if (comboBoxContent != null)
            {
                switch (comboBoxContent)
                {
                    case "Default":
                        txtLocationTypeDescription.Text = "(Searches for any matches to the search string)";
                        break;
                    case "Address":
                        txtLocationTypeDescription.Text = "(Searches for addresses matching the search string)";
                        break;
                    case "Street":
                        txtLocationTypeDescription.Text = "(Searches for streets matching the search string)";
                        break;
                    case "City":
                        txtLocationTypeDescription.Text = "(Searches for cities matching the search string)";
                        break;
                    case "County":
                        txtLocationTypeDescription.Text = "(Searches for counties matching the search string)";
                        break;
                    case "ZipCode":
                        txtLocationTypeDescription.Text = "(Searches for zip codes matching the search string)";
                        break;
                    case "State":
                        txtLocationTypeDescription.Text = "(Searches for states matching the search string)";
                        break;
                    default:
                        break;
                }
            }

        }
        private bool ValidateSearchParameters()
        {
            // Check if the address text box is empty
            if (string.IsNullOrWhiteSpace(txtSearchString.Text))
            {
                txtSearchString.Focus();
                MessageBox.Show("Please enter an address to search", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtMaxResults.Text) || !(int.TryParse(txtMaxResults.Text, out int result) && result > 0 && result < 101))
            {
                txtMaxResults.Focus();
                MessageBox.Show("Please enter a number between 1 - 100", "Error");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a new map marker using preloaded image assets
        /// </summary>
        private Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label4;
        private Label label3;
        private TextBox txtSearchString;
        private Label label2;
        private Label label1;
        private TextBox txtSearchTypeDescription;
        private ComboBox cboSearchType;
        private TextBox txtMaxResults;
        private ListBox lsbLocations;
        private TextBox txtSearchResultsDescription;
        private Button btnSearch;
        private TextBox txtLocationTypeDescription;
        private ComboBox cboLocationType;
        private Label label5;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsbLocations = new System.Windows.Forms.ListBox();
            this.txtSearchResultsDescription = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLocationTypeDescription = new System.Windows.Forms.TextBox();
            this.cboLocationType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSearchType = new System.Windows.Forms.ComboBox();
            this.txtMaxResults = new System.Windows.Forms.TextBox();
            this.txtSearchTypeDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(940, 532);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.lsbLocations);
            this.panel1.Controls.Add(this.txtSearchResultsDescription);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLocationTypeDescription);
            this.panel1.Controls.Add(this.cboLocationType);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cboSearchType);
            this.panel1.Controls.Add(this.txtMaxResults);
            this.panel1.Controls.Add(this.txtSearchTypeDescription);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSearchString);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(943, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 532);
            this.panel1.TabIndex = 1;
            // 
            // lsbLocations
            // 
            this.lsbLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbLocations.BackColor = System.Drawing.Color.White;
            this.lsbLocations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsbLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lsbLocations.ForeColor = System.Drawing.Color.Black;
            this.lsbLocations.FormattingEnabled = true;
            this.lsbLocations.ItemHeight = 20;
            this.lsbLocations.Location = new System.Drawing.Point(3, 356);
            this.lsbLocations.Name = "lsbLocations";
            this.lsbLocations.Size = new System.Drawing.Size(295, 160);
            this.lsbLocations.TabIndex = 13;
            this.lsbLocations.SelectedIndexChanged += new System.EventHandler(this.lsbLocations_SelectedIndexChanged);
            // 
            // txtSearchResultsDescription
            // 
            this.txtSearchResultsDescription.BackColor = System.Drawing.Color.Gray;
            this.txtSearchResultsDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchResultsDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchResultsDescription.Location = new System.Drawing.Point(3, 328);
            this.txtSearchResultsDescription.Multiline = true;
            this.txtSearchResultsDescription.Name = "txtSearchResultsDescription";
            this.txtSearchResultsDescription.Size = new System.Drawing.Size(295, 44);
            this.txtSearchResultsDescription.TabIndex = 12;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(3, 291);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(295, 30);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLocationTypeDescription
            // 
            this.txtLocationTypeDescription.BackColor = System.Drawing.Color.Gray;
            this.txtLocationTypeDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocationTypeDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLocationTypeDescription.ForeColor = System.Drawing.Color.White;
            this.txtLocationTypeDescription.Location = new System.Drawing.Point(7, 246);
            this.txtLocationTypeDescription.Multiline = true;
            this.txtLocationTypeDescription.Name = "txtLocationTypeDescription";
            this.txtLocationTypeDescription.Size = new System.Drawing.Size(294, 38);
            this.txtLocationTypeDescription.TabIndex = 10;
            // 
            // cboLocationType
            // 
            this.cboLocationType.FormattingEnabled = true;
            this.cboLocationType.Items.AddRange(new object[] {
            "Default",
            "Address",
            "Street",
            "City",
            "County",
            "Zipcode",
            "State"});
            this.cboLocationType.Location = new System.Drawing.Point(174, 216);
            this.cboLocationType.Name = "cboLocationType";
            this.cboLocationType.Size = new System.Drawing.Size(124, 24);
            this.cboLocationType.TabIndex = 9;
            this.cboLocationType.Text = "Default";
            this.cboLocationType.SelectedIndexChanged += new System.EventHandler(this.cboLocationType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Location Type";
            // 
            // cboSearchType
            // 
            this.cboSearchType.FormattingEnabled = true;
            this.cboSearchType.Items.AddRange(new object[] {
            "Fuzzy",
            "Exact"});
            this.cboSearchType.Location = new System.Drawing.Point(174, 128);
            this.cboSearchType.Name = "cboSearchType";
            this.cboSearchType.Size = new System.Drawing.Size(124, 24);
            this.cboSearchType.TabIndex = 7;
            this.cboSearchType.Text = "Fuzzy";
            this.cboSearchType.SelectedIndexChanged += new System.EventHandler(this.cboSearchType_SelectedIndexChanged);
            // 
            // txtMaxResults
            // 
            this.txtMaxResults.Location = new System.Drawing.Point(174, 99);
            this.txtMaxResults.Name = "txtMaxResults";
            this.txtMaxResults.Size = new System.Drawing.Size(124, 22);
            this.txtMaxResults.TabIndex = 6;
            this.txtMaxResults.Text = "10";
            // 
            // txtSearchTypeDescription
            // 
            this.txtSearchTypeDescription.BackColor = System.Drawing.Color.Gray;
            this.txtSearchTypeDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchTypeDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchTypeDescription.ForeColor = System.Drawing.Color.White;
            this.txtSearchTypeDescription.Location = new System.Drawing.Point(3, 167);
            this.txtSearchTypeDescription.Multiline = true;
            this.txtSearchTypeDescription.Name = "txtSearchTypeDescription";
            this.txtSearchTypeDescription.Size = new System.Drawing.Size(295, 43);
            this.txtSearchTypeDescription.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Search Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Maximum Results:";
            // 
            // txtSearchString
            // 
            this.txtSearchString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtSearchString.ForeColor = System.Drawing.Color.Black;
            this.txtSearchString.Location = new System.Drawing.Point(20, 70);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(278, 23);
            this.txtSearchString.TabIndex = 2;
            this.txtSearchString.Text = "6101 Frisco Square Blvd, Frisco, TX 75034";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Text:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "GeoCode";
            // 
            // GeocodingCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GeocodingCloudServicesSample";
            this.Size = new System.Drawing.Size(1244, 532);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}