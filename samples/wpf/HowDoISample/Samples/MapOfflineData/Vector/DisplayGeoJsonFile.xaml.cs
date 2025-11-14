using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Linq;
using ThinkGeo.Core;
using System.Collections.Generic;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a GeoTiff Layer on the map
    /// </summary>
    public partial class DisplayGeoJsonFile : IDisposable
    {
        public DisplayGeoJsonFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add an In-Memory Feature Layer with GeoJSON features to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
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

            // Create the Overlay and new InMemoryFeatureLayer and set up a Projection Converter to convert from the json's Decimal Degree data to Spherical Mercator.
            var layerOverlay = new LayerOverlay();
            var pittsburghHistoricalDistrictsLayer = new InMemoryFeatureLayer();
            pittsburghHistoricalDistrictsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Load all of our geojson features.
            var jsonText = File.ReadAllText(@"./Data/GeoJson/pittsburghpacity-designated-historic-districts.geojson");
            Collection<Feature> historicalDistrictFeatures = Feature.CreateFeaturesFromGeoJson(jsonText);

            // Add each json feature to our layer - use Internal Features since the json is in the unprojected 4326.  If you use FeatureSource.AddFeature(f), then you need to reproject the feature first.
            historicalDistrictFeatures.ToList().ForEach(f => pittsburghHistoricalDistrictsLayer.InternalFeatures.Add(f));
            pittsburghHistoricalDistrictsLayer.BuildIndex();

            // Add a simple area style and text style, and add the layer to the overlay.
            pittsburghHistoricalDistrictsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(200, GeoColors.LightOrange), GeoColors.Red);
            pittsburghHistoricalDistrictsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("historic_name", "Segoe UI", 12, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2);
            pittsburghHistoricalDistrictsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(pittsburghHistoricalDistrictsLayer);
            MapView.Overlays.Add(layerOverlay);

            // Manually set the map's center point and scale.
            MapView.CenterPoint = new PointShape(-8906301, 4931861);
            MapView.CurrentScale = 22000;

            _ = MapView.RefreshAsync();
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