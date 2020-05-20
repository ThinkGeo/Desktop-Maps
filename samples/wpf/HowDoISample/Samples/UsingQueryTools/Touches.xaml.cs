using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Interaction logic for Touches.xaml
    /// </summary>
    public partial class Touches : UserControl
    {
        private ShapeFileFeatureLayer parcelsLayer;
        private InMemoryFeatureLayer queryShapeLayer;
        private InMemoryFeatureLayer highlightedShapesLayer;

        public Touches()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a feature layer to hold the Frisco parcels data
            parcelsLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoLandRec/Parcels.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            parcelsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco parcel polygons
            parcelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parcelsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black, 2);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10784492.67, 3914751.61, -10783955.56, 3914454.73);

            // Create a layer to hold the feature we will perform the spatial query against
            queryShapeLayer = new InMemoryFeatureLayer();
            queryShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.CadetBlue), GeoColors.DarkBlue);
            queryShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            RectangleShape sampleShape = new RectangleShape(-10784306.88, 3914633.81, -10784208.14, 3914546.46);
            queryShapeLayer.InternalFeatures.Add(new Feature(sampleShape));

            // Create a layer to hold features found by the spatial query
            highlightedShapesLayer = new InMemoryFeatureLayer();
            highlightedShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.GreenYellow), GeoColors.DarkGreen);
            highlightedShapesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay parcelsOverlay = new LayerOverlay();
            parcelsOverlay.Layers.Add("Frisco Parcels", parcelsLayer);
            parcelsOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add("Frisco Parcels Overlay", parcelsOverlay);

            LayerOverlay queryFeaturesOverlay = new LayerOverlay();
            queryFeaturesOverlay.Layers.Add("Query Feature", queryShapeLayer);
            queryFeaturesOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add("Query Features Overlay", queryFeaturesOverlay);

            LayerOverlay highlightFeaturesOverlay = new LayerOverlay();
            highlightFeaturesOverlay.Layers.Add("Highlighted Shapes", highlightedShapesLayer);
            highlightFeaturesOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add("Highlighted Shapes Overlay", highlightFeaturesOverlay);

            // Add an event to handle new shapes that are drawn on the map
            mapView.TrackOverlay.TrackEnded += OnPolygonDrawn;

            // Perform the initial spatial query and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(sampleShape, parcelsLayer);
            HighlightQueriedFeatures(queriedFeatures);

            mapView.Refresh();
        }

        private Collection<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.FeatureSource.SpatialQuery(shape, QueryType.Touches, ReturningColumnsType.AllColumns);
            layer.Close();

            return features;
        }

        private void HighlightQueriedFeatures(IEnumerable<Feature> features)
        {
            // Clear the currently highlighted features
            highlightedShapesLayer.Open();
            highlightedShapesLayer.InternalFeatures.Clear();

            // Add new features to the layer
            foreach (var feature in features)
            {
                highlightedShapesLayer.InternalFeatures.Add(feature);
            }
            highlightedShapesLayer.Close();

            // Refresh the overlay so the layer is redrawn
            mapView.Refresh(mapView.Overlays["Highlighted Shapes Overlay"]);
        }

        private void OnPolygonDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Clear the query shape layer and add the newly drawn shape
            var drawnShape = e.TrackShape;
            queryShapeLayer.InternalFeatures.Clear();
            queryShapeLayer.InternalFeatures.Add(new Feature(drawnShape));
            mapView.Refresh(mapView.Overlays["Query Features Overlay"]);

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(drawnShape, parcelsLayer);
            HighlightQueriedFeatures(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        private void btnDrawANewPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;
        }
    }
}
