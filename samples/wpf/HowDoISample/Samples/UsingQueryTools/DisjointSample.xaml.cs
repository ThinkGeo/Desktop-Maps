using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Learn how to use layer query tools to find which features in a layer are disjoint from a shape
    /// </summary>
    public partial class DisjointSample : UserControl, IDisposable
    {
        public DisjointSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco zoning data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a feature layer to hold the Frisco zoning data
            ShapeFileFeatureLayer friscoLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            friscoLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco zoning polygons
            friscoLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            friscoLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10781137.28, 3917162.59, -10774579.34, 3911241.35);

            // Create a layer to hold the feature we will perform the spatial query against
            InMemoryFeatureLayer queryLayer = new InMemoryFeatureLayer();
            queryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
            queryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a layer to hold features found by the spatial query
            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay friscoOverlay = new LayerOverlay();
            friscoOverlay.Layers.Add("FriscoLayer", friscoLayer);
            mapView.Overlays.Add("FriscoOverlay", friscoOverlay);

            LayerOverlay highlightOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            mapView.Overlays.Add("HighlightOverlay", highlightOverlay);
            highlightOverlay.Layers.Add("HighlightLayer", highlightLayer);
            highlightOverlay.Layers.Add("QueryLayer", queryLayer);                       

            // Add an event to handle new shapes that are drawn on the map
            mapView.TrackOverlay.TrackEnded += OnPolygonDrawn;

            // Add a sample shape to the map for the initial query
            PolygonShape sampleShape = new PolygonShape("POLYGON((-10780418.9504333 3915973.97146252,-10780428.5050618 3913422.88551189,-10775737.1824769 3913413.33088341,-10775612.9723066 3915954.86220556,-10780418.9504333 3915973.97146252))");
            await GetFeaturesDisjointAsync(sampleShape);

            // Set the map extent to the sample shapes
            mapView.CurrentExtent = sampleShape.GetBoundingBox();
            await mapView.ZoomOutAsync();
            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesDisjointAsync(PolygonShape polygon)
        {
            // Find the layers we will be modifying in the MapView
            LayerOverlay highlightOverlay = (LayerOverlay)mapView.Overlays["HighlightOverlay"];
            InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["HighlightLayer"];
            InMemoryFeatureLayer queryLayer = (InMemoryFeatureLayer)highlightOverlay.Layers["QueryLayer"];

            LayerOverlay friscoOverlay = (LayerOverlay)mapView.Overlays["FriscoOverlay"];
            FeatureLayer friscoLayer = (FeatureLayer)friscoOverlay.Layers["FriscoLayer"];

            // Clear the query shape layer and add the newly drawn shape
            queryLayer.InternalFeatures.Clear();
            queryLayer.InternalFeatures.Add(new Feature(polygon));

            // Perform the spatial query using the drawn shape and highlight features that were found
            friscoLayer.Open();
            var queriedFeatures = friscoLayer.QueryTools.GetFeaturesDisjointed(polygon, ReturningColumnsType.AllColumns);            

            highlightLayer.InternalFeatures.Clear();

            foreach (var feature in queriedFeatures)
            { 
                highlightLayer.InternalFeatures.Add(feature);
            }

            // Highlight the found features
            await highlightOverlay.RefreshAsync();            

            // Update the number of matching features found in the UI
            txtNumberOfFeaturesFound.Text = string.Format("Number of features disjoint from the drawn shape: {0}", queriedFeatures.Count());

            // Disable map drawing and clear the drawn shape
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            await mapView.TrackOverlay.RefreshAsync();
        }

        /// <summary>
        /// Performs the spatial query when a new polygon is drawn
        /// </summary>
        private async void OnPolygonDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            await GetFeaturesDisjointAsync((PolygonShape)e.TrackShape);
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks on the map without panning
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (!(mapView.TrackOverlay.TrackMode == TrackMode.Polygon))
            {
                // Set the drawing mode to 'Polygon'
                mapView.TrackOverlay.TrackMode = TrackMode.Polygon;
            }
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
