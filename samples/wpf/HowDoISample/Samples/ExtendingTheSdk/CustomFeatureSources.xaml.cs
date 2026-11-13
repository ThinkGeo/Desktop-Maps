using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A feature source of your own - here over a CSV file of points - drawn by the style document like any other.
    /// </summary>
    public partial class CustomFeatureSources
    {
        private bool _initialized;

        public CustomFeatureSources()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            // The CSV is in latitude and longitude (4326); the map is in Spherical Mercator (3857).
            var vehicleRoute = new SimpleCsvFeatureSource(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "vehicle-route.csv"))
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857)
            };

            vehicleRoute.Open();
            var extent = vehicleRoute.GetBoundingBox();
            vehicleRoute.Close();
            Map.CenterPoint = extent.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;

            // The source has no file name of its own to be known by, so the style is given one.
            var tileSource = new FeatureSourceVectorTileSource();
            tileSource.FeatureSources.Add("vehicle-route", vehicleRoute);

            var cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, cloud).AddStyle((string)FindResource("StyleJson"), tileSource));

            _ = Map.RefreshAsync();
        }
    }

    /// <summary>
    /// A CSV feature source with the least a FeatureSource needs: all of its features. The base
    /// class answers every other query from them, which is fine for a small file; for a large one,
    /// override the spatial query methods as well.
    /// </summary>
    public class SimpleCsvFeatureSource : FeatureSource
    {
        private Collection<Feature> _features = new Collection<Feature>();

        public SimpleCsvFeatureSource(string csvPathFileName)
        {
            CsvPathFileName = csvPathFileName;
        }

        public string CsvPathFileName { get; }

        // Each line is "latitude,longitude". The file is read once, on open: tiles can be cut from the source on several threads at once.
        protected override void OpenCore()
        {
            var features = new Collection<Feature>();
            foreach (var line in File.ReadAllLines(CsvPathFileName))
            {
                var parts = line.Split(',');
                features.Add(new Feature(double.Parse(parts[1], CultureInfo.InvariantCulture), double.Parse(parts[0], CultureInfo.InvariantCulture)));
            }
            _features = features;
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            return _features;
        }
    }
}
