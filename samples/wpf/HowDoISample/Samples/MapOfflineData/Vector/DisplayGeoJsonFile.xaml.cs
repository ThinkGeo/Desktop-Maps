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

        private bool _initialized;
        public DisplayGeoJsonFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add an In-Memory Feature Layer with GeoJSON features to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

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
            Map.Overlays.Add(layerOverlay);

            // Manually set the map's center point and scale.
            Map.CenterPoint = new PointShape(-8906301, 4931861);
            Map.CurrentScale = 22000;

            _ = Map.RefreshAsync();
        }


        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}