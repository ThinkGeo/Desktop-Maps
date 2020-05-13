/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Cloud;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoCloudReverseGeocoding
{

    public partial class MainWindow : Window
    {
        private const string GisServerUri = "https://gisserver1.thinkgeo.com";

        private string clientId;
        private string clientSecret;

        private ReverseGeocodingClient reverseGeocodingClient;

        private Collection<ReverseGeocodingLocation> serachedPlaces;
        private ReverseGeocodingResult searchResult;
        private PointShape searchPoint;
        public static string ApiKey = string.Empty;
        private bool isincludeInresectons = true;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        public ObservableCollection<PlaceCategory> PlacesCategories { get; } = new ObservableCollection<PlaceCategory>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!TryReadClientIdSecretFromConfig())
            {
                ShowClientIdSecretInputer();
            }
            UpdateIdSecretToClient();

            WpfMap.MapUnit = GeographyUnit.Meter;
            WpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            WpfMap.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            baseOverlay.WrappingMode = WrappingMode.WrapDateline;
            WpfMap.Overlays.Add(baseOverlay);

            // Add marker overlay for showing the best matching place.
            var bestmatchingMarkerOverlay = new SimpleMarkerOverlay();
            WpfMap.Overlays.Add("BestMatchingMarkerOverlay", bestmatchingMarkerOverlay);

            // Add marker overlay for showing result.
            var nearbysMarkerOverlay = new SimpleMarkerOverlay();
            WpfMap.Overlays.Add("NearbysMarkerOverlay", nearbysMarkerOverlay);

            // Add layer for searchRadius.
            var seachRadiusFeatureLayer = new InMemoryFeatureLayer();
            seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue)), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add layer for showing geometry in result.
            var resultGeometryFeatureLayer = new InMemoryFeatureLayer();
            resultGeometryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            resultGeometryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay searchResultOverlay = new LayerOverlay() { DrawingQuality = DrawingQuality.HighQuality };
            searchResultOverlay.Layers.Add("ResultGeometryFeatureLayer", resultGeometryFeatureLayer);
            searchResultOverlay.Layers.Add("SerachRadiusFeatureLayer", seachRadiusFeatureLayer);
            WpfMap.Overlays.Add("SearchResult", searchResultOverlay);
        }

        public async Task SearchPlaceAndNearbysAsync()
        {
            BusyIndicator.Visibility = Visibility.Visible;
            // Reset UI and clear existing before search again.
            ClearupPreviousSearchResult();
            searchPoint = GetSearchPoint();
            // Create the searchPreference by UI controls.
            SearchPreference searchPreference = GetSearchPreferenceFromUI();

            try
            {
                double searchRadius = 200;
                if (searchPreference.PlaceSearchRadiusInMeter.HasValue)
                {
                    searchRadius = searchPreference.PlaceSearchRadiusInMeter.Value;
                }
                var reverseGeocodingOption = new ReverseGeocodingOptions()
                {
                    MaxResults = searchPreference.MaxResults,
                    // TODO: not support Intersection
                    //includeIntersections = searchPreference.IncludedIntersection,
                };
                if (searchPreference.PlaceCategoriesToFind.Count > 0)
                {
                    var placeCategories = (LocationCategories)Enum.Parse(typeof(LocationCategories), searchPreference.PlaceCategoriesToFind[0]);
                    for (int i = 1; i < searchPreference.PlaceCategoriesToFind.Count; i++)
                    {
                        placeCategories |= (LocationCategories)Enum.Parse(typeof(LocationCategories), searchPreference.PlaceCategoriesToFind[i]);
                    }
                    reverseGeocodingOption.LocationCategories = placeCategories;
                }

                var response = await reverseGeocodingClient.SearchPointAsync(searchPoint.X, searchPoint.Y, 3857, searchRadius, DistanceUnit.Meter, reverseGeocodingOption);
                searchResult = response;
                if (searchResult != null)
                {
                    // Update search result to map markers.
                    DisplaySearchResult(searchResult);

                    // Update search result to left panel list.
                    tabSearchResult.SelectedIndex = 0;
                    TabSearchResult_SelectionChanged(tabSearchResult, null);
                }
                BusyIndicator.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                ShowErrorAlert(ex);
            }
            finally
            {
                BusyIndicator.Visibility = Visibility.Hidden;
            }
        }

        private async void NearbySearchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            if (nearbySearchCategory.SelectedIndex == 0 || nearbySearchCategory.SelectedIndex == 1 || nearbySearchCategory.SelectedIndex == 2)
            {
                searchCategoriesPanel.Visibility = Visibility.Collapsed;
                await SearchPlaceAndNearbysAsync();
            }
            else
            {
                if (PlacesCategories.Count == 0)
                {
                    var excludedCategories = new[] { "None", "Common", "All" };
                    foreach (var placeCategory in Enum.GetNames(typeof(LocationCategories)))
                    {
                        if (!excludedCategories.Contains(placeCategory))
                        {
                            PlacesCategories.Add(new PlaceCategory { Name = placeCategory });
                        }
                    }
                }
                searchCategoriesPanel.Visibility = Visibility.Visible;
            }
        }

        private void TabSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchResult != null)
            {
                // Clear the result layer,radius layer, markers and add geometry of best matching place.
                var nearbysMarkerOverlay = (SimpleMarkerOverlay)WpfMap.Overlays["NearbysMarkerOverlay"];
                nearbysMarkerOverlay.Markers.Clear();
                var resultGeometryFeatureLayer = (InMemoryFeatureLayer)((LayerOverlay)WpfMap.Overlays["SearchResult"]).Layers["ResultGeometryFeatureLayer"];
                resultGeometryFeatureLayer.InternalFeatures.Clear();
                var seachRadiusFeatureLayer = (InMemoryFeatureLayer)((LayerOverlay)WpfMap.Overlays["SearchResult"]).Layers["SerachRadiusFeatureLayer"];
                seachRadiusFeatureLayer.InternalFeatures.Clear();

                if (tabSearchResult.SelectedIndex == 2) // Nearby Addresses Tab
                {
                    AddSearchRadius(Convert.ToDouble(searchRadius.Text, CultureInfo.InvariantCulture));
                    int i = 0;
                    foreach (var location in searchResult.NearbyLocations.Where(p => p.LocationType == "Address"))
                    {
                        AddMarkerToMap(location, i);
                        i++;
                    }
                }
                else if (tabSearchResult.SelectedIndex == 1) // Nearby Intersections Tab
                {
                    AddSearchRadius(Convert.ToDouble(searchRadius.Text, CultureInfo.InvariantCulture));
                    int i = 0;
                    foreach (var location in searchResult.NearbyLocations.Where(p => p.LocationType == "Intersection"))
                    {
                        AddMarkerToMap(location, i);
                        i++;
                    }
                }
                else if (tabSearchResult.SelectedIndex == 0) // Nearby Places Tab
                {
                    AddSearchRadius(Convert.ToDouble(searchRadius.Text, CultureInfo.InvariantCulture));

                    for (int i = 0; i < serachedPlaces.Count; i++)
                    {
                        AddMarkerToMap(serachedPlaces[i], i);
                    }
                }
                seachRadiusFeatureLayer.Open();
                WpfMap.CurrentExtent = seachRadiusFeatureLayer.GetBoundingBox();
                WpfMap.Refresh();
            }
        }

        private void DisplaySearchResult(ReverseGeocodingResult searchResult)
        {
            Collection<ReverseGeocodingLocation> serachedAddresses = new Collection<ReverseGeocodingLocation>();
            Collection<ReverseGeocodingLocation> serachedIntersection = new Collection<ReverseGeocodingLocation>();
            if (searchResult?.BestMatchLocation != null)
            {
                // Display address of the BestMatchingPlace in the left panel and add a marker.
                txtBestMatchingPlace.Text = searchResult.BestMatchLocation.Address;
                Marker marker = CreateMarkerByCategory("BestMatchingPlace", searchPoint, searchResult.BestMatchLocation.Address);
                var bestmatchingMarkerOverlay = (SimpleMarkerOverlay)WpfMap.Overlays["BestMatchingMarkerOverlay"];
                bestmatchingMarkerOverlay.Markers.Add(marker);

                var nearbyLocations = new List<ReverseGeocodingLocation>(searchResult.NearbyLocations);
                int intersectionIndex = 0;
                var intersections = nearbyLocations.FindAll(p => p.LocationCategory.ToLower().Contains("intersection"));
                foreach (var intersection in intersections)
                {
                    serachedIntersection.Add(intersection);
                    AddPropertiesForPlace(intersection, intersectionIndex);
                    intersectionIndex++;
                    nearbyLocations.Remove(intersection);
                }
                int addressIndex = 0;
                var addressPoints = nearbyLocations.FindAll(p => p.LocationCategory.ToLower().Contains("addresspoint"));
                foreach (var address in addressPoints)
                {
                    serachedAddresses.Add(address);
                    AddPropertiesForPlace(address, addressIndex);
                    addressIndex++;
                    nearbyLocations.Remove(address);
                }

                int placeIndex = 0;
                foreach (var place in nearbyLocations)
                {
                    if (!nameof(LocationCategories.Aeroway).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                        && !nameof(LocationCategories.Road).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                        && !nameof(LocationCategories.Rail).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase)
                        && !nameof(LocationCategories.Waterway).Equals(place.LocationCategory, StringComparison.InvariantCultureIgnoreCase))
                    {
                        serachedPlaces.Add(place);
                        AddPropertiesForPlace(place, placeIndex);
                        placeIndex++;
                    }
                }
                // Bind addresses,intersections,places to listbox.
                lsbAddress.ItemsSource = serachedAddresses;
                lsbPlaces.ItemsSource = serachedPlaces;
                lsbIntersection.ItemsSource = serachedIntersection;
            }
        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchCategoriesPanel.Visibility = Visibility.Hidden;
            await SearchPlaceAndNearbysAsync();
        }

        private void Addresses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbAddress != null && lsbAddress.Items.Count > 0)
            {
                WpfMap.CurrentExtent = ((ReverseGeocodingLocation)lsbAddress.SelectedItem).LocationFeature.GetBoundingBox();
                WpfMap.Refresh();
                e.Handled = true;
            }
        }

        private void Intersection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbIntersection != null && lsbIntersection.Items.Count > 0)
            {
                WpfMap.CurrentExtent = ((ReverseGeocodingLocation)lsbIntersection.SelectedItem).LocationFeature.GetBoundingBox();
                WpfMap.Refresh();
                e.Handled = true;
            }
        }

        private void Places_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbPlaces != null && lsbPlaces.Items.Count > 0)
            {
                WpfMap.CurrentExtent = ((ReverseGeocodingLocation)lsbPlaces.SelectedItem).LocationFeature.GetBoundingBox();
                WpfMap.Refresh();
                e.Handled = true;
            }
        }

        private Marker CreateMarkerByCategory(string category, PointShape pointShape, string tooltip)
        {
            Marker marker = null;
            // The icon of place is determined by category.
            string imageUri = string.Format("/Resources/{0}.png", category);
            marker = new Marker(pointShape)
            {
                Width = 24,
                Height = 24,
                ImageSource = new BitmapImage(new Uri(imageUri, UriKind.RelativeOrAbsolute)),
                ToolTip = tooltip
            };

            if (category == "BestMatchingPlace")
            {
                marker.Width = 32;
                marker.Height = 32;
                marker.YOffset = -10;
            }
            return marker;
        }

        private Marker GetMarkerByPlaceRecord(ReverseGeocodingLocation place)
        {
            Marker marker = null;
            PointShape pointShape = null;
            if (place.Properties.ContainsKey("CenterLongitude") && place.Properties.ContainsKey("CenterLatitude"))
            {
                pointShape = new PointShape(Convert.ToDouble(place.Properties["CenterLongitude"], CultureInfo.InvariantCulture), Convert.ToDouble(place.Properties["CenterLatitude"], CultureInfo.InvariantCulture));
                marker = CreateMarkerByCategory(place.LocationCategory.ToLower(), pointShape, string.Empty);
            }
            else if (place.LocationType == "Intersection")
            {
                pointShape = place.LocationFeature.GetShape().GetCenterPoint();
                marker = CreateMarkerByCategory(place.LocationType.ToLower(), pointShape, string.Empty);
            }
            else
            {
                pointShape = place.LocationFeature.GetShape().GetCenterPoint();
                marker = CreateMarkerByCategory(place.LocationCategory.ToString().ToLower(), pointShape, string.Empty);
            }

            return marker;
        }

        private SearchPreference GetSearchPreferenceFromUI()
        {
            // Set parameters for searchPreference.
            SearchPreference searchPreference = new SearchPreference()
            {
                PlaceSearchRadiusInMeter = Convert.ToDouble(searchRadius.Text, CultureInfo.InvariantCulture),
                IncludedIntersection = isincludeInresectons,
                MaxResults = Convert.ToInt32(maxResults.Text)
            };

            // Read the search categories for nearbys.
            if (nearbySearchCategory.SelectedIndex <= 2)
            {
                searchPreference.PlaceCategoriesToFind.Add(((ComboBoxItem)nearbySearchCategory.SelectedValue).Content.ToString());
            }
            else
            {
                foreach (PlaceCategory item in searchCategoriesPanel.Items)
                {
                    if (item.IsSelected)
                    {
                        searchPreference.PlaceCategoriesToFind.Add(item.Name);
                    }
                }
            }
            return searchPreference;
        }

        private PointShape GetSearchPoint()
        {
            string[] coordinate = txtCoordinate.Text.Split(',');
            double lat = Convert.ToDouble(coordinate[0].Trim(), CultureInfo.InvariantCulture);
            double lon = Convert.ToDouble(coordinate[1].Trim(), CultureInfo.InvariantCulture);

            return new PointShape(lon, lat);
        }

        private void ClearupPreviousSearchResult()
        {
            // Clear existing before doing search.
            searchResult = null;
            lsbPlaces.ItemsSource = null;
            lsbAddress.ItemsSource = null;
            lsbIntersection.ItemsSource = null;
            serachedPlaces = new Collection<ReverseGeocodingLocation>();
            var bestmatchingMarkerOverlay = (SimpleMarkerOverlay)WpfMap.Overlays["BestMatchingMarkerOverlay"];
            bestmatchingMarkerOverlay.Markers.Clear();
        }

        private async void WpfMap_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Set the searchPoint value to coordinat textbox.
                txtCoordinate.Text = string.Format("{0},{1}", e.WorldY.ToString("0.000000"), e.WorldX.ToString("0.000000"));
                await SearchPlaceAndNearbysAsync();
            }
        }

        private void AddMarkerToMap(object reverseGeocoderRecord, int index)
        {
            Marker marker = null;
            if (reverseGeocoderRecord is ReverseGeocodingLocation locationResource)
            {
                marker = GetMarkerByPlaceRecord(locationResource);
                marker.ToolTip = locationResource.Address;
                var resultGeometryFeatureLayer = (InMemoryFeatureLayer)((LayerOverlay)WpfMap.Overlays["SearchResult"]).Layers["ResultGeometryFeatureLayer"];
                resultGeometryFeatureLayer.InternalFeatures.Add(locationResource.LocationFeature);
            }

            marker.FontSize = 15;
            marker.Content = new TextBlock()
            {
                Text = (index + 1).ToString(CultureInfo.InvariantCulture),
                Margin = new Thickness(0, 0, 0, 35),
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Foreground = Brushes.Green
            };
            var nearbysMarkerOverlay = (SimpleMarkerOverlay)WpfMap.Overlays["NearbysMarkerOverlay"];
            nearbysMarkerOverlay.Markers.Add(marker);
        }

        private void AddSearchRadius(double searchDistanceInMeters)
        {
            //double searchDistance = DecimalDegreesHelper.GetLongitudeDifferenceFromDistance(searchDistanceInMeters, DistanceUnit.Meter, searchPoint.Y);
            var seachRadiusFeatureLayer = (InMemoryFeatureLayer)((LayerOverlay)WpfMap.Overlays["SearchResult"]).Layers["SerachRadiusFeatureLayer"];
            seachRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(searchPoint, searchDistanceInMeters)));
        }

        private void AddPropertiesForPlace(ReverseGeocodingLocation reverseGeocodingLocation, int index)
        {
            int distance = (int)searchPoint.GetDistanceTo(reverseGeocodingLocation.LocationFeature.GetShape(), GeographyUnit.Meter, DistanceUnit.Meter);

            reverseGeocodingLocation.Properties.Add("index", (index + 1).ToString());
            reverseGeocodingLocation.Properties.Add("distance", distance.ToString());
            reverseGeocodingLocation.Properties.Add("direction", GetDirectionBetweenTwoPoints(searchPoint, reverseGeocodingLocation.LocationFeature.GetShape().GetCenterPoint()));
        }

        private string GetDirectionBetweenTwoPoints(PointShape firstPoint, PointShape secondPoint)
        {
            string southNorthDirection, eastWestDirection, direction = string.Empty;
            double slope = (firstPoint.Y - secondPoint.Y) / (firstPoint.X - secondPoint.X);

            if (firstPoint.Y > secondPoint.Y)
            {
                southNorthDirection = "South";
            }
            else
            {
                southNorthDirection = "North";
            }
            if (firstPoint.X > secondPoint.X)
            {
                eastWestDirection = "West";
            }
            else
            {
                eastWestDirection = "East";
            }

            // If the slope is greater than 3, it think the direction is south or north.
            if (Math.Abs(slope) > 3)
            {
                direction = southNorthDirection;
            }
            // If the slope is less than 0.33, it think the direction is east or west.
            else if (Math.Abs(slope) < 0.33)
            {
                direction = eastWestDirection;
            }
            // If the slope is greater than 0.3 and less than 3, it will take the corresponding direction.
            else
            {
                direction = southNorthDirection + eastWestDirection.ToLower();
            }

            return direction;
        }

        public void ShowErrorAlert(Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            ShowClientIdSecretInputer();
        }

        private void BtnChangeApiKey_Click(object sender, RoutedEventArgs e)
        {
            ShowClientIdSecretInputer();
        }

        private async void IsInclude_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            if (isInclude.SelectedIndex == 0)
            {
                isincludeInresectons = true;
                await SearchPlaceAndNearbysAsync();
            }
            else if (isInclude.SelectedIndex == 1)
            {
                isincludeInresectons = false;
                await SearchPlaceAndNearbysAsync();
            }
        }

        private void UpdateIdSecretToClient()
        {
            reverseGeocodingClient?.Dispose();
            reverseGeocodingClient = new ReverseGeocodingClient(clientId, clientSecret);
            reverseGeocodingClient.BaseUris.Add(new Uri(GisServerUri));
        }

        private bool TryReadClientIdSecretFromConfig()
        {
            var id = ConfigurationManager.AppSettings["ClientId"];
            var secret = ConfigurationManager.AppSettings["ClientSecret"];
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(secret))
            {
                return false;
            }
            clientId = id.Trim();
            clientSecret = secret.Trim();
            return true;
        }


        private void ShowClientIdSecretInputer()
        {
            var clientIdSecretInputer = new ClientIdSecretInputer
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            clientIdSecretInputer.BaseUris.Add(new Uri(GisServerUri));
            if (clientIdSecretInputer.ShowDialog() != true)
            {
                Environment.Exit(0);
            }
            clientId = clientIdSecretInputer.ClientId;
            clientSecret = clientIdSecretInputer.ClientSecret;
        }
    }

    public class SearchPreference
    {
        public double? PlaceSearchRadiusInMeter { get; set; }

        public bool IncludedIntersection { get; set; }

        public Collection<string> PlaceCategoriesToFind { get; } = new Collection<string>();

        public int MaxResults { get; set; }
    }

    public class PlaceCategory
    {
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}