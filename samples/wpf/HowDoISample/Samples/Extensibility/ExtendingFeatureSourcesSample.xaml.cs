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
    public partial class ExtendingFeatureSourcesSample : IDisposable
    {
        public ExtendingFeatureSourcesSample()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // See the implementation of the new layer and feature source below.
            var csvLayer = new SimpleCsvFeatureLayer(@"./Data/Csv/vehicle-route.csv");

            // Set the points image to a car icon and then apply it to all zoom levels
            var vehiclePointStyle = new PointStyle(new GeoImage(@"./Resources/vehicle-location.png"))
            {
                YOffsetInPixel = -12
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
            MapView.CurrentExtent = csvLayer.GetBoundingBox();

            await MapView.RefreshAsync();
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
                _features.Add(new Feature(double.Parse(location.Split(',')[0]), double.Parse(location.Split(',')[1])));
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