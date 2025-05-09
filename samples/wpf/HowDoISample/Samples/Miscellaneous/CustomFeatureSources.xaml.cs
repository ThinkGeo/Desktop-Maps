using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for SampleTemplate.xaml
    /// </summary>
    public partial class CustomFeatureSources : IDisposable
    {
        public CustomFeatureSources()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
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

            // Load CSV data in EPSG:4326 and convert to EPSG:3857 for the map
            var csvLayer = new SimpleCsvFeatureLayer(@"./Data/Csv/vehicle-route.csv")
            {
                FeatureSource = { ProjectionConverter = new ProjectionConverter(4326, 3857) }
            };

            var vehiclePointStyle = new PointStyle
            {
                SymbolType = PointSymbolType.Circle,
                SymbolSize = 10, // adjust size as needed
                FillBrush = new GeoSolidBrush(GeoColors.Blue),
                OutlinePen = new GeoPen(GeoColors.White, 2)
            };

            csvLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = vehiclePointStyle;
            csvLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile
            };
            layerOverlay.Layers.Add(csvLayer);
            MapView.Overlays.Add(layerOverlay);

            csvLayer.Open();
            var csvLayerBBox = csvLayer.GetBoundingBox();
            MapView.CenterPoint = csvLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(MapView.MapUnit, csvLayerBBox, MapView.MapWidth, MapView.MapHeight);
            MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

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

    // Here we are creating a simple CVS feature source using the minimum set of overloads.
    // Since CSV doesn't include a way to do spatial queries we only need to return all the features
    // in the method below and the base class will do the rest.  Of course if you had large dataset this
    // would be slow, so I recommend you look at other overloads and implement optimized versions of these methods
    public class SimpleCsvFeatureSource : FeatureSource
    {
        public string CsvPathFileName { get; set; }

        private readonly Collection<Feature> _features;

        public SimpleCsvFeatureSource(string csvPathFileName)
        {
            this.CsvPathFileName = csvPathFileName;
            _features = new Collection<Feature>();
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            // If we haven't loaded the CSV then load it and return all the features
            if (_features.Count != 0) return _features;
            var locations = File.ReadAllLines(CsvPathFileName);

            foreach (var location in locations)
            {
                _features.Add(new Feature(double.Parse(location.Split(',')[1]), double.Parse(location.Split(',')[0])));
            }
            return _features;
        }
    }

    // We need to create a layer that wraps the feature source.  FeatureLayer has everything we need we just need
    // to provide a constructor and set the feature source and all the methods on the feature layer just work.
    public class SimpleCsvFeatureLayer : FeatureLayer
    {
        public SimpleCsvFeatureLayer(string csvPathFileName)
        {
            this.FeatureSource = new SimpleCsvFeatureSource(csvPathFileName);
        }
        public override bool HasBoundingBox => true;
    }
}