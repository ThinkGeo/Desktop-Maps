using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to calculate the shortest line between two shapes
    /// </summary>
    public partial class GetShortestLine : IDisposable
    {
        public GetShortestLine()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the friscoParks, stadiumLayer, and
        /// shortestLineLayer layers into a grouped LayerOverlay and display it on the map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            var friscoParks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
            var stadiumLayer = new InMemoryFeatureLayer();
            var shortestLineLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;

            // Project friscoParks layer to Spherical Mercator to match the map projection
            friscoParks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoParks layer
            friscoParks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            friscoParks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style stadiumLayer
            stadiumLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 16, GeoColors.White, 4);
            stadiumLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style shortestLineLayer
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 2, false);
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add friscoParks layer to a LayerOverlay
            layerOverlay.Layers.Add("friscoParks", friscoParks);

            // Add stadiumLayer layer to a LayerOverlay
            layerOverlay.Layers.Add("stadiumLayer", stadiumLayer);

            // Add shortestLineLayer to the layerOverlay
            var shortestLineOverlay = new LayerOverlay();
            shortestLineOverlay.Layers.Add("shortestLineLayer", shortestLineLayer);
            MapView.Overlays.Add("shortestLineOverlay", shortestLineOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778340, 3915490);
            MapView.CurrentScale = 36110;

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            // Add Toyota Stadium feature to stadiumLayer
            var stadium = new Feature(new PointShape(-10779651.500992451, 3915933.0023557912));
            stadiumLayer.InternalFeatures.Add(stadium);

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Calculates the shortest line from the selected park to the stadium and displays its length and shows the line on the map
        /// </summary>
        private void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var shortestLineOverlay = (LayerOverlay)MapView.Overlays["shortestLineOverlay"];

            var friscoParks = (ShapeFileFeatureLayer)layerOverlay.Layers["friscoParks"];
            var stadiumLayer = (InMemoryFeatureLayer)layerOverlay.Layers["stadiumLayer"];
            var shortestLineLayer = (InMemoryFeatureLayer)shortestLineOverlay.Layers["shortestLineLayer"];

            // Query the friscoParks layer to get the first feature closest to the map click event
            var park = friscoParks.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            // Get the stadium feature from the stadiumLayer
            var stadium = stadiumLayer.InternalFeatures[0];

            // Get the shortest line from the selected park to the stadium
            var shortestLine = park.GetShape().GetShortestLineTo(stadium, GeographyUnit.Meter);

            // Show the shortestLine on the map
            shortestLineLayer.InternalFeatures.Clear();
            shortestLineLayer.InternalFeatures.Add(new Feature(shortestLine));
            _ = shortestLineOverlay.RefreshAsync();

            // Get the area of the first feature
            var length = shortestLine.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer);

            // Display the shortestLine's length in the distanceResult TextBox
            DistanceResult.Text = $"{length:f3} km";
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