using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ReverseGeocoding : UserControl
    {
        private ReverseGeocodingCloudClient reverseGeocodingCloudClient;

        public ReverseGeocoding()
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

            // Create a new feature layer to display the search radius of the reverse geocode and create a style for it
            var searchRadiusFeatureLayer = new InMemoryFeatureLayer();
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue)), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red);
            searchRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new feature layer to display selected locations returned from the reverse geocode and create styles for it
            var selectedResultItemFeatureLayer = new InMemoryFeatureLayer();
            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            selectedResultItemFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an overlay and add the feature layers to it
            var searchFeaturesOverlay = new LayerOverlay();
            searchFeaturesOverlay.Layers.Add("Search Radius", searchRadiusFeatureLayer);
            searchFeaturesOverlay.Layers.Add("Result Feature Geometry", selectedResultItemFeatureLayer);

            // Create a popup overlay to display the best match
            var bestMatchPopupOverlay = new PopupOverlay();

            // Add the overlays to the map
            mapView.Overlays.Add("Search Features Overlay", searchFeaturesOverlay);
            mapView.Overlays.Add("Best Match Popup Overlay", bestMatchPopupOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the ReverseGeocodingCloudClient with our ThinkGeo Cloud credentials
            reverseGeocodingCloudClient = new ReverseGeocodingCloudClient(SampleKeys.ClientId2, SampleKeys.ClientSecret2);

            cboLocationCategories.SelectedIndex = 0;
        }

        private async Task PerformReverseGeocodeAsync()
        {
            //Perform some simple validation on the input text boxes
            if (ValidateSearchParameters())
            {
                var options = new CloudReverseGeocodingOptions();

                // Set up the CloudReverseGeocodingOptions object based on the parameters set in the UI
                string[] coordinates = txtCoordinates.Text.Split(',');
                double lat = double.Parse(coordinates[0].Trim());
                double lon = double.Parse(coordinates[1].Trim());
                int searchRadius = int.Parse(txtSearchRadius.Text);
                var searchRadiusDistanceUnit = DistanceUnit.Meter;
                int pointProjectionInSrid = 3857;
                var searchPoint = new PointShape(lon, lat);
                options.MaxResults = int.Parse(txtMaxResults.Text);

                switch (cboLocationCategories.SelectedItem.ToString())
                {
                    case "All":
                        options.LocationCategories = CloudLocationCategories.All;
                        break;
                    case "Common":
                        options.LocationCategories = CloudLocationCategories.Common;
                        break;
                    case "None":
                        options.LocationCategories = CloudLocationCategories.None;
                        break;
                    default:
                        options.LocationCategories = CloudLocationCategories.All;
                        break;
                }

                // Show a loading graphic to let users know the request is running
                //loadingImage.Visibility = Visibility.Visible;

                // Run the reverse geocode
                CloudReverseGeocodingResult searchResult;
                try
                {
                    searchResult = await reverseGeocodingCloudClient.SearchPointAsync(lon, lat, pointProjectionInSrid, searchRadius, searchRadiusDistanceUnit, options);
                }
                catch (System.ArgumentNullException)
                {
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return;
                }
                // Hide the loading graphic
                // loadingImage.Visibility = Visibility.Hidden;

                // Handle an exception returned from the service
                if (searchResult.Exception != null)
                {
                    MessageBox.Show(searchResult.Exception.Message, "Error");
                    return;
                }

                // Update the UI
                await DisplaySearchResultsAsync(searchPoint, searchRadius, searchResult);
            }
        }

        private async Task DisplaySearchResultsAsync(PointShape searchPoint, int searchRadius, CloudReverseGeocodingResult searchResult)
        {
            // Get the 'Search Radius' layer from the MapView
            var searchRadiusFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Search Radius");

            // Clear the existing features and add new features showing the area that was searched by the reverse geocode
            searchRadiusFeatureLayer.Clear();
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(searchPoint, searchRadius)));
            searchRadiusFeatureLayer.InternalFeatures.Add(new Feature(searchPoint));

            // Get the 'Result Feature' layer and clear it
            var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Feature Geometry");
            selectedResultItemFeatureLayer.Clear();

            // If a match was found for the geocode, update the UI
            if (searchResult?.BestMatchLocation != null)
            {
                // Get the 'Best Match' PopupOverlay from the MapView and clear it
                var bestMatchPopupOverlay = (PopupOverlay)mapView.Overlays["Best Match Popup Overlay"];
                bestMatchPopupOverlay.Popups.Clear();

                // Get the location of the 'Best Match' found within the search radius
                var bestMatchLocation = searchResult.BestMatchLocation.LocationFeature.GetShape().GetClosestPointTo(searchPoint, GeographyUnit.Meter);
                if (bestMatchLocation == null)
                {
                    bestMatchLocation = searchResult.BestMatchLocation.LocationFeature.GetShape().GetCenterPoint();
                }

                // Create a popup to display the best match, and add it to the PopupOverlay
                var bestMatchPopup = new Popup(bestMatchLocation)
                {
                    Content = "Best Match: " + searchResult.BestMatchLocation.Address,
                    FontSize = 10d,
                    FontFamily = new System.Windows.Media.FontFamily("Verdana")
                };
                bestMatchPopupOverlay.Popups.Add(bestMatchPopup);

                // Sort the locations found into three groups (Addresses, Places, Roads) based on their LocationCategory
                Collection<CloudReverseGeocodingLocation> nearbyLocations = new Collection<CloudReverseGeocodingLocation>(searchResult.NearbyLocations);
                Collection<CloudReverseGeocodingLocation> nearbyAddresses = new Collection<CloudReverseGeocodingLocation>();
                Collection<CloudReverseGeocodingLocation> nearbyPlaces = new Collection<CloudReverseGeocodingLocation>();
                Collection<CloudReverseGeocodingLocation> nearbyRoads = new Collection<CloudReverseGeocodingLocation>();
                foreach (var foundLocation in nearbyLocations)
                {
                    if (foundLocation.LocationCategory.ToLower().Contains("addresspoint"))
                    {
                        nearbyAddresses.Add(foundLocation);
                    }
                    else if (nameof(CloudLocationCategories.Aeroway).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Road).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Rail).Equals(foundLocation.LocationCategory)
                        || nameof(CloudLocationCategories.Waterway).Equals(foundLocation.LocationCategory))
                    {
                        nearbyRoads.Add(foundLocation);
                    }
                    else if (!nameof(CloudLocationCategories.Intersection).Equals(foundLocation.LocationCategory))
                    {
                        nearbyPlaces.Add(foundLocation);
                    }
                }

                // Set the data sources for the addresses, roads, and places list boxes
                lsbAddresses.DataSource = nearbyAddresses;
                lsbAddresses.DisplayMember = "Address";

                lsbRoads.DataSource = nearbyRoads;
                lsbRoads.DisplayMember = "LocationName";

                lsbPlaces.DataSource = nearbyPlaces;
                lsbPlaces.DisplayMember = "LocationName";

                txtSearchResultsBestMatch.Text = "Best Match: " + searchResult.BestMatchLocation.Address;
            }
            else
            {
                txtSearchResultsBestMatch.Text = "No address or place matches found for this location";
            }

            // Set the map extent to show the results of the search
            mapView.CurrentExtent = searchRadiusFeatureLayer.GetBoundingBox();
            var standardZoomLevelSet = new ZoomLevelSet();
            if (mapView.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
            {
                await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
            }
            await mapView.RefreshAsync();
        }
        private bool ValidateSearchParameters()
        {
            // Check if the 'Location' text box has a valid value
            if (!string.IsNullOrWhiteSpace(txtCoordinates.Text))
            {
                string[] coordinates = txtCoordinates.Text.Split(',');

                if (coordinates.Count() != 2)
                {
                    txtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }

                if (!(double.TryParse(coordinates[0].Trim(), out double lat) && double.TryParse(coordinates[1].Trim(), out double lon)))
                {
                    txtCoordinates.Focus();
                    MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                    return false;
                }
            }
            else
            {
                txtCoordinates.Focus();
                MessageBox.Show("Please enter a valid set of coordinates to search", "Error");
                return false;
            }

            // Check if the 'Search Radius' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtSearchRadius.Text) || !(int.TryParse(txtSearchRadius.Text, out int searchRadiusInt) && searchRadiusInt > 0))
            {
                txtSearchRadius.Focus();
                MessageBox.Show("Please enter an integer greater than 0", "Error");
                return false;
            }

            // Check if the 'Max Results' text box has a valid value
            if (string.IsNullOrWhiteSpace(txtMaxResults.Text) || !(int.TryParse(txtMaxResults.Text, out int maxResultsInt) && maxResultsInt > 0))
            {
                txtMaxResults.Focus();
                MessageBox.Show("Please enter an integer greater than 0", "Error");
                return false;
            }

            return true;
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // Run the reverse geocode using the coordinates in the 'Location' text box
            await PerformReverseGeocodeAsync();
        }

        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Set the coordinates in the UI
                txtCoordinates.Text = string.Format("{0},{1}", e.WorldY.ToString("0.000000"), e.WorldX.ToString("0.000000"));

                // Run the reverse geocode
                await PerformReverseGeocodeAsync();
            }
        }

        private void cboLocationCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxContent = cboLocationCategories.SelectedItem.ToString();

            if (comboBoxContent != null)
            {
                switch (comboBoxContent)
                {
                    case "All":
                        txtLocationCategoriesDescription.Text = "(Includes all available location types in the search)";
                        break;
                    case "Common":
                        txtLocationCategoriesDescription.Text = "(Includes only commonly-used 'Place' types in the search)";
                        break;
                    case "None":
                        txtLocationCategoriesDescription.Text = "(Only the best matching result will be returned)";
                        break;
                    default:
                        break;
                }
            }
        }

        private async void lsbAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedResultList = (ListBox)sender;
            if (selectedResultList.SelectedItem != null)
            {
                // Get the selected location
                var locationFeature = ((CloudReverseGeocodingLocation)selectedResultList.SelectedItem).LocationFeature;

                // Get the 'Result Feature' layer from the MapView
                var selectedResultItemFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Feature Geometry");

                // Clear the existing features and add the geometry of the selected location
                selectedResultItemFeatureLayer.Clear();
                selectedResultItemFeatureLayer.InternalFeatures.Add(new Feature(locationFeature.GetShape()));

                // Center the map on the chosen location
                mapView.CurrentExtent = locationFeature.GetBoundingBox();
                var standardZoomLevelSet = new ZoomLevelSet();
                if (mapView.CurrentScale < standardZoomLevelSet.ZoomLevel18.Scale)
                {
                    await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                }
                await mapView.RefreshAsync();
            }

        }

        #region Component Designer generated code
        private Panel panel1;
        private TabControl tabControl;
        private TabPage nearbyAddressesTabItem;
        private TabPage nearbyRoadsTabItem;
        private TabPage nearbyPlacesTabItem;
        private TextBox txtSearchResultsBestMatch;
        private Button btnSearch;
        private TextBox txtLocationCategoriesDescription;
        private ComboBox cboLocationCategories;
        private TextBox txtMaxResults;
        private TextBox txtSearchRadius;
        private Label label5;
        private Label label4;
        private Label label3;
        private TextBox txtCoordinates;
        private Label label2;
        private Label label1;
        private ListBox lsbRoads;
        private ListBox lsbPlaces;
        private ListBox lsbAddresses;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.nearbyAddressesTabItem = new System.Windows.Forms.TabPage();
            this.lsbAddresses = new System.Windows.Forms.ListBox();
            this.nearbyRoadsTabItem = new System.Windows.Forms.TabPage();
            this.lsbRoads = new System.Windows.Forms.ListBox();
            this.nearbyPlacesTabItem = new System.Windows.Forms.TabPage();
            this.lsbPlaces = new System.Windows.Forms.ListBox();
            this.txtSearchResultsBestMatch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtLocationCategoriesDescription = new System.Windows.Forms.TextBox();
            this.cboLocationCategories = new System.Windows.Forms.ComboBox();
            this.txtMaxResults = new System.Windows.Forms.TextBox();
            this.txtSearchRadius = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCoordinates = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.nearbyAddressesTabItem.SuspendLayout();
            this.nearbyRoadsTabItem.SuspendLayout();
            this.nearbyPlacesTabItem.SuspendLayout();
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
            this.mapView.Size = new System.Drawing.Size(855, 588);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Controls.Add(this.txtSearchResultsBestMatch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtLocationCategoriesDescription);
            this.panel1.Controls.Add(this.cboLocationCategories);
            this.panel1.Controls.Add(this.txtMaxResults);
            this.panel1.Controls.Add(this.txtSearchRadius);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCoordinates);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(861, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 588);
            this.panel1.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.nearbyAddressesTabItem);
            this.tabControl.Controls.Add(this.nearbyRoadsTabItem);
            this.tabControl.Controls.Add(this.nearbyPlacesTabItem);
            this.tabControl.Location = new System.Drawing.Point(6, 371);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(292, 214);
            this.tabControl.TabIndex = 12;
            // 
            // nearbyAddressesTabItem
            // 
            this.nearbyAddressesTabItem.Controls.Add(this.lsbAddresses);
            this.nearbyAddressesTabItem.Location = new System.Drawing.Point(4, 25);
            this.nearbyAddressesTabItem.Name = "nearbyAddressesTabItem";
            this.nearbyAddressesTabItem.Padding = new System.Windows.Forms.Padding(3);
            this.nearbyAddressesTabItem.Size = new System.Drawing.Size(284, 185);
            this.nearbyAddressesTabItem.TabIndex = 0;
            this.nearbyAddressesTabItem.Text = "Address";
            this.nearbyAddressesTabItem.UseVisualStyleBackColor = true;
            // 
            // lsbAddresses
            // 
            this.lsbAddresses.FormattingEnabled = true;
            this.lsbAddresses.ItemHeight = 16;
            this.lsbAddresses.Location = new System.Drawing.Point(0, 6);
            this.lsbAddresses.Name = "lsbAddresses";
            this.lsbAddresses.Size = new System.Drawing.Size(285, 180);
            this.lsbAddresses.TabIndex = 4;
            this.lsbAddresses.SelectedIndexChanged += new System.EventHandler(this.lsbAddresses_SelectedIndexChanged);
            // 
            // nearbyRoadsTabItem
            // 
            this.nearbyRoadsTabItem.Controls.Add(this.lsbRoads);
            this.nearbyRoadsTabItem.Location = new System.Drawing.Point(4, 25);
            this.nearbyRoadsTabItem.Name = "nearbyRoadsTabItem";
            this.nearbyRoadsTabItem.Padding = new System.Windows.Forms.Padding(3);
            this.nearbyRoadsTabItem.Size = new System.Drawing.Size(284, 185);
            this.nearbyRoadsTabItem.TabIndex = 1;
            this.nearbyRoadsTabItem.Text = "Road";
            this.nearbyRoadsTabItem.UseVisualStyleBackColor = true;
            // 
            // lsbRoads
            // 
            this.lsbRoads.FormattingEnabled = true;
            this.lsbRoads.ItemHeight = 16;
            this.lsbRoads.Location = new System.Drawing.Point(-1, 3);
            this.lsbRoads.Name = "lsbRoads";
            this.lsbRoads.Size = new System.Drawing.Size(285, 180);
            this.lsbRoads.TabIndex = 3;
            this.lsbRoads.SelectedIndexChanged += new System.EventHandler(this.lsbAddresses_SelectedIndexChanged);
            // 
            // nearbyPlacesTabItem
            // 
            this.nearbyPlacesTabItem.Controls.Add(this.lsbPlaces);
            this.nearbyPlacesTabItem.Location = new System.Drawing.Point(4, 25);
            this.nearbyPlacesTabItem.Name = "nearbyPlacesTabItem";
            this.nearbyPlacesTabItem.Padding = new System.Windows.Forms.Padding(3);
            this.nearbyPlacesTabItem.Size = new System.Drawing.Size(284, 185);
            this.nearbyPlacesTabItem.TabIndex = 2;
            this.nearbyPlacesTabItem.Text = "Place";
            this.nearbyPlacesTabItem.UseVisualStyleBackColor = true;
            // 
            // lsbPlaces
            // 
            this.lsbPlaces.FormattingEnabled = true;
            this.lsbPlaces.ItemHeight = 16;
            this.lsbPlaces.Location = new System.Drawing.Point(-1, 6);
            this.lsbPlaces.Name = "lsbPlaces";
            this.lsbPlaces.Size = new System.Drawing.Size(285, 180);
            this.lsbPlaces.TabIndex = 3;
            this.lsbPlaces.SelectedIndexChanged += new System.EventHandler(this.lsbAddresses_SelectedIndexChanged);
            // 
            // txtSearchResultsBestMatch
            // 
            this.txtSearchResultsBestMatch.BackColor = System.Drawing.Color.Gray;
            this.txtSearchResultsBestMatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchResultsBestMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchResultsBestMatch.Location = new System.Drawing.Point(6, 318);
            this.txtSearchResultsBestMatch.Multiline = true;
            this.txtSearchResultsBestMatch.Name = "txtSearchResultsBestMatch";
            this.txtSearchResultsBestMatch.Size = new System.Drawing.Size(295, 47);
            this.txtSearchResultsBestMatch.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSearch.Location = new System.Drawing.Point(3, 283);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(295, 29);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtLocationCategoriesDescription
            // 
            this.txtLocationCategoriesDescription.BackColor = System.Drawing.Color.Gray;
            this.txtLocationCategoriesDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocationCategoriesDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLocationCategoriesDescription.Location = new System.Drawing.Point(3, 230);
            this.txtLocationCategoriesDescription.Multiline = true;
            this.txtLocationCategoriesDescription.Name = "txtLocationCategoriesDescription";
            this.txtLocationCategoriesDescription.Size = new System.Drawing.Size(295, 47);
            this.txtLocationCategoriesDescription.TabIndex = 9;
            // 
            // cboLocationCategories
            // 
            this.cboLocationCategories.FormattingEnabled = true;
            this.cboLocationCategories.Items.AddRange(new object[] {
            "All",
            "Common",
            "None"});
            this.cboLocationCategories.Location = new System.Drawing.Point(171, 200);
            this.cboLocationCategories.Name = "cboLocationCategories";
            this.cboLocationCategories.Size = new System.Drawing.Size(127, 24);
            this.cboLocationCategories.TabIndex = 8;
            this.cboLocationCategories.Text = "All";
            this.cboLocationCategories.SelectedIndexChanged += new System.EventHandler(this.cboLocationCategories_SelectedIndexChanged);
            // 
            // txtMaxResults
            // 
            this.txtMaxResults.Location = new System.Drawing.Point(171, 167);
            this.txtMaxResults.Name = "txtMaxResults";
            this.txtMaxResults.Size = new System.Drawing.Size(127, 22);
            this.txtMaxResults.TabIndex = 7;
            this.txtMaxResults.Text = "10";
            // 
            // txtSearchRadius
            // 
            this.txtSearchRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSearchRadius.Location = new System.Drawing.Point(171, 135);
            this.txtSearchRadius.Name = "txtSearchRadius";
            this.txtSearchRadius.Size = new System.Drawing.Size(127, 23);
            this.txtSearchRadius.TabIndex = 6;
            this.txtSearchRadius.Text = "400";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(21, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Categorys:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(21, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximum Radius:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Search Results:";
            // 
            // txtCoordinates
            // 
            this.txtCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCoordinates.Location = new System.Drawing.Point(24, 89);
            this.txtCoordinates.Name = "txtCoordinates";
            this.txtCoordinates.Size = new System.Drawing.Size(274, 23);
            this.txtCoordinates.TabIndex = 2;
            this.txtCoordinates.Text = "3915241.03,-10779570.57";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click on the Map or enter a Location\r\nto Reverse Geocode";
            // 
            // ReverseGeocoding
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ReverseGeocoding";
            this.Size = new System.Drawing.Size(1162, 588);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.nearbyAddressesTabItem.ResumeLayout(false);
            this.nearbyRoadsTabItem.ResumeLayout(false);
            this.nearbyPlacesTabItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion Component Designer generated code

    }
}