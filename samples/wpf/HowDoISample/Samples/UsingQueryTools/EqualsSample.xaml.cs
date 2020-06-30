using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer are topologically equal to a shape
    /// </summary>
    public partial class EqualsSample : UserControl
    {
        public EqualsSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco zoning data
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold and display the zoning data
            InMemoryFeatureLayer zoningLayer = new InMemoryFeatureLayer();

            // Add a style to use to draw the Frisco zoning polygons
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Import the features from the Frisco zoning data shapefile
            ShapeFileFeatureSource zoningDataFeatureSource = new ShapeFileFeatureSource(@"../../../Data/Shapefile/Zoning.shp");

            // Create a ProjectionConverter to convert the shapefile data from North Central Texas (2276) to Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(3857, 2276);

            // For this sample, we have to reproject the features before adding them to the feature layer
            // This is because the topological equality query often does not work when used on a feature layer with a ProjectionConverter, due to rounding issues between projections
            zoningDataFeatureSource.Open();
            projectionConverter.Open();
            foreach (Feature zoningFeature in zoningDataFeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns))
            {
                Feature reprojectedFeature = projectionConverter.ConvertToInternalProjection(zoningFeature);
                zoningLayer.InternalFeatures.Add(reprojectedFeature);
            }
            zoningDataFeatureSource.Close();
            projectionConverter.Close();

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10779646.71, 3920258.95, -10774442.97, 3915699.48);

            // Create a layer to hold the feature we will perform the spatial query against
            InMemoryFeatureLayer queryFeatureLayer = new InMemoryFeatureLayer();
            queryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
            queryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a layer to hold features found by the spatial query
            InMemoryFeatureLayer highlightedFeaturesLayer = new InMemoryFeatureLayer();
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay zoningOverlay = new LayerOverlay();
            zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
            mapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

            LayerOverlay queryFeaturesOverlay = new LayerOverlay();
            queryFeaturesOverlay.Layers.Add("Query Feature", queryFeatureLayer);
            mapView.Overlays.Add("Query Features Overlay", queryFeaturesOverlay);

            LayerOverlay highlightedFeaturesOverlay = new LayerOverlay();
            highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
            mapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

            // Add a sample shape to the map for the initial query
            // To ensure topological equality for this sample, we create a new shape using the same geometry as an existing feature
            zoningLayer.Open();
            var sampleShape = zoningLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).First().GetShape();
            zoningLayer.Close();
            GetFeaturesEqual(sampleShape);

            // Set the map extent to the sample shape
            mapView.CurrentExtent = new RectangleShape(-10778499.3056056, 3920954.39858245, -10774534.1347853, 3917538.61889993);
        }

        /// <summary>
        /// Perform the 'Equals' spatial query using the layer's QueryTools
        /// </summary>
        private Collection<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.QueryTools.GetFeaturesTopologicalEqual(shape, ReturningColumnsType.AllColumns);
            layer.Close();

            return features;
        }

        /// <summary>
        /// Highlight the features that were found by the spatial query
        /// </summary>
        private void HighlightQueriedFeatures(IEnumerable<Feature> features)
        {
            // Find the layers we will be modifying in the MapView dictionary
            LayerOverlay highlightedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Highlighted Features Overlay"];
            InMemoryFeatureLayer highlightedFeaturesLayer = (InMemoryFeatureLayer)highlightedFeaturesOverlay.Layers["Highlighted Features"];

            // Clear the currently highlighted features
            highlightedFeaturesLayer.Open();
            highlightedFeaturesLayer.InternalFeatures.Clear();

            // Add new features to the layer
            foreach (var feature in features)
            {
                highlightedFeaturesLayer.InternalFeatures.Add(feature);
            }
            highlightedFeaturesLayer.Close();

            // Refresh the overlay so the layer is redrawn
            highlightedFeaturesOverlay.Refresh();

            // Update the number of matching features found in the UI
            txtNumberOfFeaturesFound.Text = string.Format("Number of features topologically equal to the drawn shape: {0}", features.Count());
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private void GetFeaturesEqual(BaseShape shape)
        {
            // Find the layers we will be modifying in the MapView
            LayerOverlay queryFeaturesOverlay = (LayerOverlay)mapView.Overlays["Query Features Overlay"];
            InMemoryFeatureLayer queryFeatureLayer = (InMemoryFeatureLayer)queryFeaturesOverlay.Layers["Query Feature"];
            InMemoryFeatureLayer zoningLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query shape layer and add the newly drawn shape
            queryFeatureLayer.InternalFeatures.Clear();
            queryFeatureLayer.InternalFeatures.Add(new Feature(shape));
            queryFeaturesOverlay.Refresh();

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(shape, zoningLayer);
            HighlightQueriedFeatures(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }
    }
}
