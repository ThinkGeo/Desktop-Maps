using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace RedisenceSearch
{
    /// <summary>
    /// Interaction logic for Sample.xaml
    /// </summary>
    public partial class Sample : Window
    {
        private const string roadOverlayName = "roadOverlay";
        private const string markerOverlayName = "markerOverlay";
        private const string roadHighlightOverlayName = "highlightOverlay";

        public Sample()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;

            // Load roads overlay
            LayerOverlay roadsOverlay = new LayerOverlay();
            Map1.Overlays.Add(roadOverlayName, roadsOverlay);

            // Load road data from a shape file.
            ShapeFileFeatureLayer roadsFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\Roads.shp");
            roadsFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Gray);
            roadsFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("ROAD_NAME", "Arial", 8, DrawingFontStyles.Regular, GeoColors.Black);
            roadsFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            roadsOverlay.Layers.Add(roadsFeatureLayer);

            // Add highlight overlay
            LayerOverlay highlightOverlay = new LayerOverlay();
            Map1.Overlays.Add(roadHighlightOverlayName, highlightOverlay);
            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(new GeoColor(123, GeoColors.Green), 5.0f));
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightOverlay.Layers.Add(highlightLayer);

            // Add marker overlay
            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            Map1.Overlays.Add(markerOverlayName, markerOverlay);
            // Set the map extent
            Map1.CurrentExtent = new RectangleShape(2478794.65996324, 7106797.82702127, 2482540.06498076, 7104117.76042073);
            Map1.Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string address = txtAddressName.Text.Trim();
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter address.");
                return;
            }

            // Initialize geocoder
            Geocoder streetGeocoder = new Geocoder();
            streetGeocoder.MatchingPlugIns.Add(new StreetMatchingPlugin(@"..\..\Data\GeoCoderIndex", MatchMode.ExactMatch));
            try
            {
                streetGeocoder.Open();

                // Get the ShapeFileFeatureLayer which includes the street data.
                FeatureLayer roadFeatureLayer = ((LayerOverlay)Map1.Overlays[roadOverlayName]).Layers[0] as FeatureLayer;
                roadFeatureLayer.Open();

                // Get the marker overlay to display the searched results.
                SimpleMarkerOverlay markerOverlay = Map1.Overlays[markerOverlayName] as SimpleMarkerOverlay;
                markerOverlay.Markers.Clear();

                Collection<SearchResult> searchedResult = new Collection<SearchResult>();
                // Geocoder the address input, such as "6701 PECAN".
                Collection<GeocoderMatch> matchedRoads = streetGeocoder.Match(address);

                Proj4Projection projection = new Proj4Projection();
                projection.InternalProjectionParametersString = Proj4Projection.GetWgs84ParametersString();
                projection.ExternalProjectionParametersString = "+proj=lcc +lat_1=32.1333333333333 +lat_2=33.9666666666667 +lat_0=31.6666666666667 +lon_0=-98.5 +x_0=600000 +y_0=2000000 +ellps=GRS80 +datum=NAD83 +units=us-ft +no_defs";
                projection.Open();

                foreach (GeocoderMatch geocoderMatched in matchedRoads)
                {
                    // Display the searched result as marker.
                    BaseShape streetCenter = new Feature(geocoderMatched.NameValuePairs["CentroidPoint"]).GetShape();
                    PointShape center = projection.ConvertToExternalProjection(streetCenter) as PointShape;
                    markerOverlay.Markers.Add(new Marker(center)
                    {
                        ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                        Width = 20,
                        Height = 34,
                        YOffset = -17,
                        ToolTip = geocoderMatched.NameValuePairs["Street"]
                    });

                    // Get the feature id of the street where the searched result locates.
                    string featureId = geocoderMatched.NameValuePairs["UID"];

                    // Get the street where the searched result locates.
                    Feature feature = roadFeatureLayer.QueryTools.GetFeatureById(featureId, new[] { "ROAD_NAME" });
                    searchedResult.Add(GetSearchResult(feature, geocoderMatched));
                }

                projection.Close();
                roadFeatureLayer.Close();
                searchResults.ItemsSource = searchedResult;

                // Set the highlight layer into the current extent.
                InMemoryFeatureLayer highlightRoadLayer = ((LayerOverlay)Map1.Overlays[roadHighlightOverlayName]).Layers[0] as InMemoryFeatureLayer;
                if (highlightRoadLayer.InternalFeatures.Count > 0)
                {
                    Map1.CurrentExtent = highlightRoadLayer.GetBoundingBox();
                    Map1.ZoomOut();
                }

                Map1.Refresh();
            }
            finally
            {
                streetGeocoder.Close();
            }
        }

        private SearchResult GetSearchResult(Feature feature, GeocoderMatch geocoderMath)
        {
            LineShape roadSegment = GetLineShape(feature);  // Get the line segment of current feature.
            bool isLeftSide = geocoderMath.NameValuePairs["SideOfStreet"].Equals("Left", StringComparison.InvariantCultureIgnoreCase);
            bool isFollowRoadDirection = IsDirectionFollowRoad(geocoderMath);

            // Get the roads which are intersetected with current road
            FeatureLayer roadFeatureLayer = ((LayerOverlay)Map1.Overlays[roadOverlayName]).Layers[0] as FeatureLayer;

            // Get segments intersected with start vertex
            RectangleShape startVertextBbox = BufferVertex(roadSegment.Vertices[0]);
            Collection<Feature> intersectedStartFeatures = roadFeatureLayer.FeatureSource.GetFeaturesInsideBoundingBox(startVertextBbox, new[] { "ROAD_NAME" });
            Feature startCrossFeature = GetCrossRoad(feature, isLeftSide, intersectedStartFeatures);

            // Get segments intersected with end vertex
            RectangleShape endVertextBbox = BufferVertex(roadSegment.Vertices[roadSegment.Vertices.Count - 1]);
            Collection<Feature> intersectedEndFeatures = roadFeatureLayer.FeatureSource.GetFeaturesInsideBoundingBox(endVertextBbox, new[] { "ROAD_NAME" });
            Feature endCrossFeature = GetCrossRoad(feature, isLeftSide, intersectedEndFeatures);

            // highlight the low and high cross streets.
            InMemoryFeatureLayer highlightRoadLayer = ((LayerOverlay)Map1.Overlays[roadHighlightOverlayName]).Layers[0] as InMemoryFeatureLayer;
            highlightRoadLayer.InternalFeatures.Clear();

            if (startCrossFeature != null)
            {
                highlightRoadLayer.InternalFeatures.Add(startCrossFeature);
            }
            if (endCrossFeature != null)
            {
                highlightRoadLayer.InternalFeatures.Add(endCrossFeature);
            }

            SearchResult searchResult = new SearchResult
            {
                LowHouseNo = isLeftSide ? geocoderMath.NameValuePairs["FromAddressLeft"] : geocoderMath.NameValuePairs["FromAddressRight"],
                HighHouseNo = isLeftSide ? geocoderMath.NameValuePairs["ToAddressLeft"] : geocoderMath.NameValuePairs["ToAddressRight"],
                StreetName = geocoderMath.NameValuePairs["Street"],
                StreetType = geocoderMath.NameValuePairs["StreetType"],
                LowCrossStreet = isFollowRoadDirection ? (startCrossFeature != null ? startCrossFeature.ColumnValues["ROAD_NAME"] : "N/A") : (endCrossFeature != null ? endCrossFeature.ColumnValues["ROAD_NAME"] : "N/A"),
                HighCrossStreet = isFollowRoadDirection ? (endCrossFeature != null ? endCrossFeature.ColumnValues["ROAD_NAME"] : "N/A") : (startCrossFeature != null ? startCrossFeature.ColumnValues["ROAD_NAME"] : "N/A"),
            };

            return searchResult;
        }

        /// <summary>
        /// Check if the housenumber is following the vertex sequence of the road.
        /// </summary>
        private static bool IsDirectionFollowRoad(GeocoderMatch geocoderMath)
        {
            int fromLeft = Convert.ToInt32(geocoderMath.NameValuePairs["FromAddressLeft"], CultureInfo.InvariantCulture);
            int toLeft = Convert.ToInt32(geocoderMath.NameValuePairs["ToAddressLeft"], CultureInfo.InvariantCulture);
            int fromRight = Convert.ToInt32(geocoderMath.NameValuePairs["FromAddressRight"], CultureInfo.InvariantCulture);
            int toRight = Convert.ToInt32(geocoderMath.NameValuePairs["ToAddressRight"], CultureInfo.InvariantCulture);

            return (fromRight < toRight) && (fromLeft < toLeft);
        }

        /// <summary>
        /// Get the road which is cross the input road. The return road should have the biggest angle compared with input road.
        /// </summary>
        private static Feature GetCrossRoad(Feature currentRoad, bool isOnLeftSide, Collection<Feature> intersectedFeatures)
        {
            // Loop to find the road which has the biggest angle with current road
            double smallestAngle = double.MaxValue;
            Feature smallestFeature = null;
            LineShape currentLineShape = GetLineShape(currentRoad);
            foreach (Feature feature in intersectedFeatures)
            {
                if (feature.ColumnValues["ROAD_NAME"] != currentRoad.ColumnValues["ROAD_NAME"])
                {
                    LineShape lineShape = GetLineShape(feature);      // Get the line segment of current feature.
                    double angle = GetAngleBetweenTwoLines(currentLineShape, lineShape);

                    // If the searched result is on the left, we just need to check the angle which is nagtive.
                    double offsetAngle = Math.Abs(angle - 90) * (isOnLeftSide ? 1 : -1);

                    if (offsetAngle < smallestAngle)
                    {
                        smallestAngle = offsetAngle;
                        smallestFeature = feature;
                    }
                }
            }

            return smallestFeature;
        }

        /// <summary>
        /// Create a small boundingbox based on the input vertex.
        /// </summary>
        private RectangleShape BufferVertex(Vertex vertex)
        {
            double epsilon = 10e-6;
            return new RectangleShape(vertex.X - epsilon, vertex.Y + epsilon, vertex.X + epsilon, vertex.Y - epsilon);
        }

        /// <summary>
        /// Cauculate the angle between 2 line segments.
        /// </summary>
        private static double GetAngleBetweenTwoLines(LineShape fromLine, LineShape toLine)
        {
            int fromVetexes = fromLine.Vertices.Count;
            int toVetexes = toLine.Vertices.Count;
            double angle = CalculateAngleBetweenTwoLine(fromLine.Vertices[fromVetexes - 1], toLine.Vertices[0], fromLine.Vertices[0], toLine.Vertices[toVetexes - 1]);

            return angle;
        }

        private static double CalculateAngleBetweenTwoLine(Vertex fromEndVertex, Vertex toStartVertex, Vertex fromStartVertex, Vertex toEndVertex)
        {
            Vector vector1 = new Vector(toStartVertex.X - fromStartVertex.X, toStartVertex.Y - fromStartVertex.Y);
            Vector vector2 = new Vector(toEndVertex.X - fromEndVertex.X, toEndVertex.Y - fromEndVertex.Y);
            double angle = Vector.AngleBetween(vector1, vector2);

            return angle;
        }

        private static LineShape GetLineShape(Feature feature)
        {
            LineBaseShape road = feature.GetShape() as LineBaseShape;
            LineShape roadSegment = road as LineShape;
            if (roadSegment == null)
            {
                roadSegment = ((MultilineShape)road).Lines[0];
            }
            roadSegment.Id = feature.Id;

            return roadSegment;
        }
    }
}
