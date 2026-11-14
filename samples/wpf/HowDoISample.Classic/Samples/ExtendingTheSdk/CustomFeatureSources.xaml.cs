using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// A feature source of your own - here over a CSV file of points - shown through a feature layer like any other.
    /// </summary>
    public partial class CustomFeatureSources : IDisposable
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
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            // The CSV is in latitude and longitude (4326); the map is in Spherical Mercator (3857).
            var csvLayer = new SimpleCsvFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "vehicle-route.csv"));
            csvLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            csvLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 10, new GeoSolidBrush(GeoColors.Blue), new GeoPen(GeoColors.White, 2));
            csvLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(csvLayer);
            Map.Overlays.Add(layerOverlay);

            csvLayer.Open();
            var extent = csvLayer.GetBoundingBox();
            csvLayer.Close();
            Map.CenterPoint = extent.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
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

        // Each line is "latitude,longitude", written with a period as the decimal mark whatever the culture.
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

    /// <summary>
    /// A layer over the CSV feature source. FeatureLayer does all the work; it only needs the source.
    /// </summary>
    public class SimpleCsvFeatureLayer : FeatureLayer
    {
        public SimpleCsvFeatureLayer(string csvPathFileName)
        {
            FeatureSource = new SimpleCsvFeatureSource(csvPathFileName);
        }

        public override bool HasBoundingBox => true;
    }
}
