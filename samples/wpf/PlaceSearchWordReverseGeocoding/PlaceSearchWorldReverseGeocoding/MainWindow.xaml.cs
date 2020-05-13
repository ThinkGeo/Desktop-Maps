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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WorldReverseGeocoding;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;
using System.Linq;

namespace ThinkGeo.MapSuite.PlaceSearchWorldReverseGeocodingSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OsmReverseGeocoder osmReverseGeocoder;
        private ReverseGeocoderResult searchResult;
        private Collection<Place> serachedPlaces;
        private Collection<Place> serachedAddress;
        private Collection<Place> serachedIntersetions;

        private PointShape searchPoint;
        private SimpleMarkerOverlay bestmatchingMarkerOverlay;
        private SimpleMarkerOverlay nearbysMarkerOverlay;
        private InMemoryFeatureLayer seachRadiusFeatureLayer;
        private InMemoryFeatureLayer resultGeometryFeatureLayer;

        private Proj4Projection projection;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the osmReverseGeocoder with the testing SQLiteDatabase.
            osmReverseGeocoder = new OsmReverseGeocoder(ConfigurationManager.ConnectionStrings["SQLiteConnectionString"].ConnectionString);

            projection = new Proj4Projection(4326, 3857);
            projection.Open();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap.MapUnit = GeographyUnit.Meter;
            wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Add background map.
            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap.Overlays.Add(baseOverlay);

            // Add marker overlay for showing the best matching place.
            bestmatchingMarkerOverlay = new SimpleMarkerOverlay();
            wpfMap.Overlays.Add("BestMatchingMarkerOverlay", bestmatchingMarkerOverlay);

            // Add marker overlay for showing result.
            nearbysMarkerOverlay = new SimpleMarkerOverlay();
            wpfMap.Overlays.Add("NearbysMarkerOverlay", nearbysMarkerOverlay);

            // Add layer for searchRadius.
            seachRadiusFeatureLayer = new InMemoryFeatureLayer();
            seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue)), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add layer for showing geometry in result.
            resultGeometryFeatureLayer = new InMemoryFeatureLayer();
            resultGeometryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            resultGeometryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay searchResultOverlay = new LayerOverlay() { DrawingQuality = DrawingQuality.HighQuality };
            searchResultOverlay.Layers.Add("ResultGeometryFeatureLayer", resultGeometryFeatureLayer);
            searchResultOverlay.Layers.Add("SerachRadiusFeatureLayer", seachRadiusFeatureLayer);
            wpfMap.Overlays.Add("SearchResult", searchResultOverlay);

            wpfMap.CurrentExtent = new RectangleShape(-10789506, 3924697, -10767934, 3905588);
            wpfMap.Refresh();

            // Bind search categories to comboxList.
            nearbySearchCategory.ItemsSource = new List<string>() { "None", "Common", "All", "Customized" };
            nearbySearchCategory.SelectedIndex = 2;
        }


        private void SearchPlaceAndNearbys()
        {
            // Reset UI and clear existing before search again.
            ClearupPreviousSearchResult();

            searchPoint = GetSearchPoint();

            // osmReverseGeocoder only support 4326, we need convert point to 4326
            var projectedSearchPoint = (PointShape)projection.ConvertToInternalProjection(searchPoint);
            // Create the searchPreference by UI controls.
            SearchPreference searchPreference = GetSearchPreferenceFromUI();
            searchResult = osmReverseGeocoder.Search(projectedSearchPoint, searchPreference);
            if (searchResult != null)
            {
                // convert result to 3857
                foreach (var place in searchResult.Nearby)
                {
                    place.Geometry = projection.ConvertToExternalProjection(place.Geometry);
                    var point = projection.ConvertToExternalProjection(place.CenterLongitude, place.CenterLatitude);
                    place.CenterLongitude = point.X;
                    place.CenterLatitude = point.Y;
                }

                // Update search result to map markers.
                DisplaySearchResult(searchResult);

                // Update search result to left panel list.
                tabSearchResult.SelectedIndex = 0;
                TabSearchResult_SelectionChanged(tabSearchResult, null);
            }
        }

        private void nearbySearchCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nearbySearchCategory.SelectedIndex == 0 || nearbySearchCategory.SelectedIndex == 1 || nearbySearchCategory.SelectedIndex == 2)
            {
                searchCategoriesPanel.Visibility = Visibility.Collapsed;
                SearchPlaceAndNearbys();
            }
            else
            {
                searchCategoriesPanel.Visibility = Visibility.Visible;
            }
        }

        private void TabSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchResult != null)
            {
                // Clear the result layer,radius layer, markers and add geometry of best matching place.
                nearbysMarkerOverlay.Markers.Clear();
                resultGeometryFeatureLayer.InternalFeatures.Clear();
                seachRadiusFeatureLayer.InternalFeatures.Clear();

                AddSearchPointToMap(searchPoint);
                if (tabSearchResult.SelectedIndex == 0) // Nearby Addresses Tab 
                {
                    AddSearchRadius(Convert.ToDouble(maxSearchIntersectionRadius.Text, CultureInfo.InvariantCulture));

                    for (int i = 0; i < serachedPlaces.Count; i++)
                    {
                        if (serachedPlaces[i] != searchResult.BestMatch)
                        {
                            AddMarkerToMap(serachedPlaces[i], i);
                        }
                    }
                }
                else if (tabSearchResult.SelectedIndex == 1) // Nearby Intersections Tab
                {
                    AddSearchRadius(Convert.ToDouble(maxSearchIntersectionRadius.Text, CultureInfo.InvariantCulture));

                    for (int i = 0; i < serachedIntersetions.Count; i++)
                    {
                        if (serachedIntersetions[i] != searchResult.BestMatch)
                        {
                            AddMarkerToMap(serachedIntersetions[i], i);
                        }
                    }
                }
                else if (tabSearchResult.SelectedIndex == 2) // Nearby Places Tab
                {
                    AddSearchRadius(Convert.ToDouble(maxSearchIntersectionRadius.Text, CultureInfo.InvariantCulture));

                    for (int i = 0; i < serachedAddress.Count; i++)
                    {
                        if (serachedAddress[i] != searchResult.BestMatch)
                        {
                            AddMarkerToMap(serachedAddress[i], i);
                        }
                    }
                }
                wpfMap.CurrentExtent = seachRadiusFeatureLayer.GetBoundingBox();
                wpfMap.Refresh();
            }
        }

        private void DisplaySearchResult(ReverseGeocoderResult searchResult)
        {
            if (searchResult != null && searchResult.BestMatch != null)
            {
                // Display address of the BestMatchingPlace in the left panel and add a marker.
                if (searchResult.BestMatch is Intersection)
                {
                    Intersection intersection = (Intersection)searchResult.BestMatch;
                    string roadInfos = string.Empty;
                    List<string> roadNames = intersection.Description.Split(',').ToList();
                    string lastRoadName = roadNames[roadNames.Count - 1];
                    roadNames.RemoveAt(roadNames.Count - 1);

                    txtBestMatchingPlace.Text = string.Format("Intersection between {0} and {1} in {2}", string.Join(", ", roadNames), lastRoadName, intersection.Address);
                }
                else
                {
                    txtBestMatchingPlace.Text = searchResult.BestMatch.Address;
                }


                // the best mach disctance is samller than 200 meter. if not, it sholud be a larger area place
                var distance1 = searchResult.BestMatch.Geometry.GetDistanceTo(searchPoint, GeographyUnit.Meter, DistanceUnit.Meter);
                // The distance between SearchPoint and the center of BestMatch. 
                PointShape bestMatchGeometry = searchResult.BestMatch.Geometry.GetCenterPoint();
                var distance2 = bestMatchGeometry.GetDistanceTo(searchPoint, GeographyUnit.Meter, DistanceUnit.Meter);

                if (distance1 == 0)
                {
                    // BestMatch contains search point 
                    if (distance2 > 210)
                    {
                        bestMatchGeometry = searchPoint;
                    }
                }

                Marker marker = CreateMarkerByCategory("BestMatchingPlace", bestMatchGeometry, searchResult.BestMatch.Address);
                bestmatchingMarkerOverlay.Markers.Add(marker);

                // Add index,distance and direction for addresses, places and intersections.
                int placeIndex = 0;
                int intersectionIndex = 0;
                int addressIndex = 0;

                foreach (Place place in searchResult.Nearby)
                {
                    if (place.PlaceCategory != PlaceCategories.Road)
                    {
                        if (place.PlaceCategory.ToString().Contains(PlaceCategories.AddressPoint.ToString()) || (string.IsNullOrEmpty(place.Name) && !string.IsNullOrEmpty(place.HouseNumber)))
                        {
                            serachedAddress.Add(place);
                            AddPropertiesForPlace(place, addressIndex);
                            addressIndex++;
                        }
                        else if (place.PlaceCategory.ToString().Contains(PlaceCategories.Intersection.ToString()))
                        {
                            serachedIntersetions.Add(place);
                            AddPropertiesForPlace(place, intersectionIndex);
                            intersectionIndex++;
                        }
                        else
                        {
                            serachedPlaces.Add(place);
                            AddPropertiesForPlace(place, placeIndex);
                            placeIndex++;
                        }
                    }
                }

                // Bind addresses,intersections,places to listbox.
                lsbAddress.ItemsSource = serachedAddress;
                lsbPlaces.ItemsSource = serachedPlaces;
                lsbIntersection.ItemsSource = serachedIntersetions;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchPlaceAndNearbys();
        }

        private void Addresses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbAddress != null && lsbAddress.Items.Count > 0)
            {
                wpfMap.CurrentExtent = ((Place)lsbAddress.SelectedItem).Geometry.GetBoundingBox();
                wpfMap.Refresh();
                e.Handled = true;
            }
        }

        private void Intersection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbIntersection != null && lsbIntersection.Items.Count > 0)
            {
                wpfMap.CurrentExtent = ((Intersection)lsbIntersection.SelectedItem).Location.GetBoundingBox();
                wpfMap.Refresh();
                e.Handled = true;
            }
        }

        private void Places_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbPlaces != null && lsbPlaces.Items.Count > 0)
            {
                wpfMap.CurrentExtent = ((Place)lsbPlaces.SelectedItem).Geometry.GetBoundingBox();
                wpfMap.Refresh();
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
                marker.YOffset = -16;
            }

            return marker;
        }

        private Marker GetMarkerByPlaceRecord(Place place)
        {
            PointShape pointShape = null;
            if (place.Properties.ContainsKey("CenterLongitude") && place.Properties.ContainsKey("CenterLatitude"))
            {
                pointShape = new PointShape(Convert.ToDouble(place.Properties["CenterLongitude"], CultureInfo.InvariantCulture), Convert.ToDouble(place.Properties["CenterLatitude"], CultureInfo.InvariantCulture));
            }
            else
            {
                pointShape = place.Geometry.GetCenterPoint();
            }
            string category = null;
            if (place.PlaceCategory.ToString().Contains(","))
            {
                category = place.PlaceCategory.ToString().Split(',')[1].ToLower().Trim();
            }
            else
            {
                category = place.PlaceCategory.ToString().ToLower();
            }
            return CreateMarkerByCategory(category, pointShape, string.Empty);
        }

        private SearchPreference GetSearchPreferenceFromUI()
        {
            // Set parameters for searchPreference.
            SearchPreference searchPreference = new SearchPreference()
            {
                MaxSearchRadiusInMeter = Convert.ToDouble(maxSearchIntersectionRadius.Text, CultureInfo.InvariantCulture),
                ResultMode = ResultMode.Verbose,
                MaxResultCount = 20
            };

            // Read the search categories for nearbys.
            if (nearbySearchCategory.SelectedIndex == 0)
            {
                searchPreference.NearbyPlaceCategories = PlaceCategories.None;
            }
            else if (nearbySearchCategory.SelectedIndex == 1)
            {
                searchPreference.NearbyPlaceCategories = PlaceCategories.Common;
            }
            else if (nearbySearchCategory.SelectedIndex == 2)
            {
                searchPreference.NearbyPlaceCategories = PlaceCategories.All;
            }
            else
            {
                searchPreference.NearbyPlaceCategories = PlaceCategories.None;
                Collection<PlaceCategories> searchPlaceCategoriesForNearBy = new Collection<PlaceCategories>();
                for (int i = 0; i < categoryGrid.Children.Count; i++)
                {
                    if (categoryGrid.Children[i] is CheckBox)
                    {
                        CheckBox category = (CheckBox)categoryGrid.Children[i];
                        if (category.IsChecked == true)
                        {
                            PlaceCategories selectedCategory = (PlaceCategories)(Convert.ToInt32(category.Tag, CultureInfo.InvariantCulture));
                            searchPreference.NearbyPlaceCategories = searchPreference.NearbyPlaceCategories | selectedCategory;
                        }
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
            serachedPlaces = new Collection<Place>();
            serachedIntersetions = new Collection<Place>();
            serachedAddress = new Collection<Place>();
            bestmatchingMarkerOverlay.Markers.Clear();
        }

        private void wpfMap_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Right)
            {
                // Set the searchPoint value to coordinat textbox.
                txtCoordinate.Text = string.Format("{0},{1}", e.WorldY.ToString("0.000000"), e.WorldX.ToString("0.000000"));
                SearchPlaceAndNearbys();
            }
        }

        private void AddMarkerToMap(object reverseGeocoderRecord, int index)
        {
            Marker marker = null;

            if (reverseGeocoderRecord is Intersection)
            {
                marker = CreateMarkerByCategory("intersection", (reverseGeocoderRecord as Intersection).Location, (reverseGeocoderRecord as Intersection).Address);
            }
            else
            {
                marker = GetMarkerByPlaceRecord(reverseGeocoderRecord as Place);
                marker.ToolTip = (reverseGeocoderRecord as Place).Address;
                resultGeometryFeatureLayer.InternalFeatures.Add(new Feature((reverseGeocoderRecord as Place).Geometry));
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
            nearbysMarkerOverlay.Markers.Add(marker);
        }

        private void AddSearchPointToMap(PointShape searchPoint)
        {
            Marker searchPointMarker = new Marker(searchPoint)
            {
                Width = 32,
                Height = 32,
                ImageSource = new BitmapImage(new Uri("/Resources/searchPoint.png", UriKind.RelativeOrAbsolute)),
                ToolTip = "Search Point"
            };
            searchPointMarker.YOffset = -16;
            nearbysMarkerOverlay.Markers.Add(searchPointMarker);
        }

        private void AddSearchRadius(double searchDistanceInMeters)
        {
            seachRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(searchPoint, searchDistanceInMeters)));
        }

        private void AddPropertiesForPlace(object reverseGeocoderRecord, int index)
        {
            if (reverseGeocoderRecord is Intersection)
            {
                (reverseGeocoderRecord as Intersection).OptionalNames.Add("index", (index + 1).ToString());
                (reverseGeocoderRecord as Intersection).OptionalNames.Add("distance", (reverseGeocoderRecord as Intersection).GetDistanceTo(searchPoint, DistanceUnit.Meter).ToString());
                (reverseGeocoderRecord as Intersection).OptionalNames.Add("direction", (reverseGeocoderRecord as Intersection).GetDirectionFrom(searchPoint).ToString());
            }
            else
            {
                int distance = (int)searchPoint.GetDistanceTo((reverseGeocoderRecord as Place).Geometry, GeographyUnit.Meter, DistanceUnit.Meter);

                (reverseGeocoderRecord as Place).Properties.Add("index", (index + 1).ToString());
                (reverseGeocoderRecord as Place).Properties.Add("distance", distance.ToString());
                (reverseGeocoderRecord as Place).Properties.Add("direction", GetDirectionBetweenTwoPoints(searchPoint, (reverseGeocoderRecord as Place).Geometry.GetCenterPoint()));

            }
        }

        private string GetDirectionBetweenTwoPoints(PointShape firstPoint, PointShape secondPoint)
        {
            string southNorthDirection, eastWestDirection, direction = string.Empty;
            double slope = (firstPoint.Y - secondPoint.Y) / (firstPoint.X - secondPoint.X);

            if (firstPoint.Y > secondPoint.Y)
            {
                southNorthDirection = "south";
            }
            else
            {
                southNorthDirection = "north";
            }
            if (firstPoint.X > secondPoint.X)
            {
                eastWestDirection = "west";
            }
            else
            {
                eastWestDirection = "east";
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
    }
}