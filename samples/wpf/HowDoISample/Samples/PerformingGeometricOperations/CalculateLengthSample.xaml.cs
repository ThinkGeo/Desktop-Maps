using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to calculate the length of a line
    /// </summary>
    public partial class CalculateLengthSample : IDisposable
    {
        public CalculateLengthSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the friscoTrails and selectedLineLayer layers
        /// into a grouped LayerOverlay and display it on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var friscoTrails = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hike_Bike.shp");
            var selectedLineLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();

            // Project friscoTrails layer to Spherical Mercator to match the map projection
            friscoTrails.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoTrails layer
            friscoTrails.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Orange, 2, false);
            friscoTrails.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style selectedLineLayer
            selectedLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 2, false);
            selectedLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add friscoTrails layer to a LayerOverlay
            layerOverlay.Layers.Add("friscoTrails", friscoTrails);

            // Add selectedLineLayer to the layerOverlay
            layerOverlay.Layers.Add("selectedLineLayer", selectedLineLayer);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10782307.6877106, 3918904.87378907, -10774377.3460701, 3912073.31442403);

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Calculates the length of a line selected on the map and displays it in the lengthResult TextBox
        /// </summary>
        private async void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

            var friscoTrails = (ShapeFileFeatureLayer)layerOverlay.Layers["friscoTrails"];
            var selectedLineLayer = (InMemoryFeatureLayer)layerOverlay.Layers["selectedLineLayer"];

            // Query the friscoTrails layer to get the first feature closest to the map click event
            var feature = friscoTrails.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            // Show the selected feature on the map
            selectedLineLayer.InternalFeatures.Clear();
            selectedLineLayer.InternalFeatures.Add(feature);
            await layerOverlay.RefreshAsync();

            // Get the length of the first feature
            var length = ((LineBaseShape)feature.GetShape()).GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer);

            // Display the selectedLine's length in the lengthResult TextBox
            LengthResult.Text = $"{length:f3} km";
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}