using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class Geocoding : UserControl
    {
        private GeocodingCloudClient geocodingCloudClient;
        public Geocoding()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display selected locations returned from the geocode and create styles for it
            var selectedResultItemFeatureLayer = new InMemoryFeatureLayer();
            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new overlay to display the selected locations returned from the geocode and add it to the map
            var searchFeaturesOverlay = new LayerOverlay();
            searchFeaturesOverlay.Layers.Add("Result Feature Geometry", selectedResultItemFeatureLayer);
            mapView.Overlays.Add("Search Features Overlay", searchFeaturesOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the GeocodingCloudClient using our ThinkGeo Cloud credentials
            geocodingCloudClient = new GeocodingCloudClient(SampleKeys.ClientId2, SampleKeys.ClientSecret2);

            _ = mapView.RefreshAsync();
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient
        /// </summary>
        private async Task<CloudGeocodingResult> PerformGeocodingQuery()
        {
            var options = new CloudGeocodingOptions
            {
                // Set up the CloudGeocodingOptions object based on the parameters set in the UI
                MaxResults = int.Parse(txtMaxResults.Text),
                Autocomplete = chkAutocomplete.Checked,
                ResultProjectionInSrid = 3857
            };

            if (chkRestrictToExtent.Checked)
            {
                options.BBox = mapView.CurrentExtent;
            }

            var countries = (txtCountryCodes.Text ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
            if (countries.Length > 0)
            {
                options.Countries = countries;
            }

            var languages = (txtLanguage.Text ?? string.Empty)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToArray();
            if (languages.Length > 0)
            {
                options.Language = languages;
            }

            // Run the geocode
            var searchString = txtSearchString.Text.Trim();
            var searchResult = await geocodingCloudClient.SearchAsync(searchString, options);

            return searchResult;
        }

        /// <summary>
        /// Update the UI based on the results of a Cloud Geocoding Query
        /// </summary>
        private Task UpdateSearchResultsOnUIAsync(CloudGeocodingResult searchResult)
        {
            // Clear the locations list and existing location markers on the map
            var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Feature Geometry");
            selectedResultItemFeatureLayer.Open();
            selectedResultItemFeatureLayer.Clear();
            lsbLocations.DataSource = null;

            if (searchResult.Locations != null)
            {
                // Update the UI with the number of results found and the list of locations found
                txtSearchResultsDescription.Text = $"Found {searchResult.Locations.Count} matching locations.";
                lsbLocations.DataSource = searchResult.Locations;
                lsbLocations.DisplayMember = "LocationName";
                if (searchResult.Locations.Count > 0)
                {
                    lsbLocations.SelectedIndex = 0;
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Search for an address using the GeocodingCloudClient and update the UI
        /// </summary>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Perform some simple validation on the input text boxes
                if (ValidateSearchParameters())
                {
                    // Run the Cloud Geocoding query
                    var searchResult = await PerformGeocodingQuery();

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
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// When a location is selected in the UI, add a marker at that location and center the map on it
        /// </summary>
        private async void lsbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the selected location
                var chosenLocation = lsbLocations.SelectedItem as CloudGeocodingLocation;
                if (chosenLocation == null) return;

                // Get the InMemoryFeatureLayer from the MapView
                var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Feature Geometry");

                // Clear the existing markers and add a new marker at the chosen location
                selectedResultItemFeatureLayer.Open();
                selectedResultItemFeatureLayer.Clear();
                selectedResultItemFeatureLayer.InternalFeatures.Add(new Feature(chosenLocation.Shape));

                // Center the map on the chosen location
                mapView.CurrentExtent = chosenLocation.BoundingBox;
                var standardZoomLevelSet = new ZoomLevelSet();
                mapView.CurrentScale = standardZoomLevelSet.ZoomLevel18.Scale;
                await mapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Helper function to perform simple validation on the input text boxes
        /// </summary>
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

        #region Component Designer generated code
        private Panel panel1;
        private Label label1;
        private Label label2;
        private TextBox txtSearchString;
        private Label label3;
        private TextBox txtMaxResults;
        private Label label4;
        private TextBox txtCountryCodes;
        private Label label5;
        private TextBox txtLanguage;
        private CheckBox chkAutocomplete;
        private CheckBox chkRestrictToExtent;
        private Button btnSearch;
        private TextBox txtSearchResultsDescription;
        private ListBox lsbLocations;
        private ToolTip toolTip1;

        private System.ComponentModel.IContainer components;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lsbLocations = new System.Windows.Forms.ListBox();
            this.txtSearchResultsDescription = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkRestrictToExtent = new System.Windows.Forms.CheckBox();
            this.chkAutocomplete = new System.Windows.Forms.CheckBox();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCountryCodes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxResults = new System.Windows.Forms.TextBox();
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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1244, 532);
            this.mapView.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.lsbLocations);
            this.panel1.Controls.Add(this.txtSearchResultsDescription);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.chkRestrictToExtent);
            this.panel1.Controls.Add(this.chkAutocomplete);
            this.panel1.Controls.Add(this.txtLanguage);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCountryCodes);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMaxResults);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSearchString);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(933, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 531);
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
            this.lsbLocations.Location = new System.Drawing.Point(3, 316);
            this.lsbLocations.Name = "lsbLocations";
            this.lsbLocations.Size = new System.Drawing.Size(295, 209);
            this.lsbLocations.TabIndex = 13;
            this.lsbLocations.SelectedIndexChanged += new System.EventHandler(this.lsbLocations_SelectedIndexChanged);
            //
            // txtSearchResultsDescription
            //
            this.txtSearchResultsDescription.BackColor = System.Drawing.Color.Gray;
            this.txtSearchResultsDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchResultsDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchResultsDescription.ForeColor = System.Drawing.Color.White;
            this.txtSearchResultsDescription.Location = new System.Drawing.Point(3, 272);
            this.txtSearchResultsDescription.Multiline = true;
            this.txtSearchResultsDescription.Name = "txtSearchResultsDescription";
            this.txtSearchResultsDescription.Size = new System.Drawing.Size(295, 40);
            this.txtSearchResultsDescription.TabIndex = 12;
            //
            // btnSearch
            //
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(3, 236);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(295, 30);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // chkRestrictToExtent
            //
            this.chkRestrictToExtent.AutoSize = true;
            this.chkRestrictToExtent.Checked = true;
            this.chkRestrictToExtent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestrictToExtent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkRestrictToExtent.ForeColor = System.Drawing.Color.White;
            this.chkRestrictToExtent.Location = new System.Drawing.Point(20, 206);
            this.chkRestrictToExtent.Name = "chkRestrictToExtent";
            this.chkRestrictToExtent.Size = new System.Drawing.Size(200, 21);
            this.chkRestrictToExtent.TabIndex = 10;
            this.chkRestrictToExtent.Text = "Restrict to current map extent";
            this.chkRestrictToExtent.UseVisualStyleBackColor = true;
            //
            // chkAutocomplete
            //
            this.chkAutocomplete.AutoSize = true;
            this.chkAutocomplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkAutocomplete.ForeColor = System.Drawing.Color.White;
            this.chkAutocomplete.Location = new System.Drawing.Point(20, 182);
            this.chkAutocomplete.Name = "chkAutocomplete";
            this.chkAutocomplete.Size = new System.Drawing.Size(180, 21);
            this.chkAutocomplete.TabIndex = 9;
            this.chkAutocomplete.Text = "Autocomplete (prefix match)";
            this.chkAutocomplete.UseVisualStyleBackColor = true;
            //
            // txtLanguage
            //
            this.txtLanguage.Location = new System.Drawing.Point(174, 152);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.Size = new System.Drawing.Size(124, 22);
            this.txtLanguage.TabIndex = 8;
            this.toolTip1.SetToolTip(this.txtLanguage, "ISO 639-1, comma-separated (e.g. en,fr)");
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Language:";
            //
            // txtCountryCodes
            //
            this.txtCountryCodes.Location = new System.Drawing.Point(174, 125);
            this.txtCountryCodes.Name = "txtCountryCodes";
            this.txtCountryCodes.Size = new System.Drawing.Size(124, 22);
            this.txtCountryCodes.TabIndex = 6;
            this.toolTip1.SetToolTip(this.txtCountryCodes, "ISO 3166-1 alpha-2, comma-separated (e.g. us,ca)");
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Country Codes:";
            //
            // txtMaxResults
            //
            this.txtMaxResults.Location = new System.Drawing.Point(174, 99);
            this.txtMaxResults.Name = "txtMaxResults";
            this.txtMaxResults.Size = new System.Drawing.Size(124, 22);
            this.txtMaxResults.TabIndex = 4;
            this.txtMaxResults.Text = "100";
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
            // Geocoding
            //
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "Geocoding";
            this.Size = new System.Drawing.Size(1244, 532);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
