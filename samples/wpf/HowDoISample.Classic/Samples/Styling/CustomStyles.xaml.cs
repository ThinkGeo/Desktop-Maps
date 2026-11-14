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
    /// Draw capital cities with Style subclasses that decide in DrawCore how each feature looks.
    /// </summary>
    public partial class CustomStyles : IDisposable
    {
        private bool _initialized;
        private ShapeFileFeatureLayer _capitalsLayer;

        public CustomStyles()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _capitalsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "WorldCapitals.shp"));
            _capitalsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            _capitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(_capitalsLayer);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(450060, 1074670);
            Map.CurrentScale = 147914800;

            _initialized = true;
            DaylightStyle.IsChecked = true;
        }

        private void DaylightStyle_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _capitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            _capitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TimeBasedPointStyle
            {
                TimeZoneColumnName = "TIMEZONE",
                DaytimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Yellow, 12, GeoColors.Black),
                NighttimePointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Gray, 12, GeoColors.Black)
            });
            _ = Map.RefreshAsync();
        }

        private void PopulationStyle_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _capitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            _capitalsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new SizedPointStyle
            {
                PointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 1),
                SizeColumnName = "POPULATION",
                Ratio = 500000
            });
            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    // Yellow where it is between 7:00 and 19:59 local time now, gray elsewhere.
    internal class TimeBasedPointStyle : Core.Style
    {
        public PointStyle DaytimePointStyle { get; set; } = new PointStyle();

        public PointStyle NighttimePointStyle { get; set; } = new PointStyle();

        public string TimeZoneColumnName { get; set; } = string.Empty;

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                if (!double.TryParse(feature.ColumnValues[TimeZoneColumnName], NumberStyles.Float, CultureInfo.InvariantCulture, out var offsetToUtc)) continue;

                var localTime = DateTime.UtcNow.AddHours(offsetToUtc);
                var pointStyle = localTime.Hour >= 7 && localTime.Hour <= 19 ? DaytimePointStyle : NighttimePointStyle;
                pointStyle.Draw(new Collection<Feature> { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            var columns = new Collection<string> { TimeZoneColumnName };
            foreach (var column in DaytimePointStyle.GetRequiredColumnNames())
            {
                if (!columns.Contains(column)) columns.Add(column);
            }
            foreach (var column in NighttimePointStyle.GetRequiredColumnNames())
            {
                if (!columns.Contains(column)) columns.Add(column);
            }
            return columns;
        }
    }

    // A circle whose size in pixels is the population divided by Ratio.
    internal class SizedPointStyle : Core.Style
    {
        public PointStyle PointStyle { get; set; } = new PointStyle();

        public float Ratio { get; set; } = 1;

        public string SizeColumnName { get; set; } = string.Empty;

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                if (!double.TryParse(feature.ColumnValues[SizeColumnName], NumberStyles.Float, CultureInfo.InvariantCulture, out var population)) continue;

                var size = population / Ratio;
                // A new PointStyle per feature: tiles draw in parallel, so the shared one must not change.
                var sizedPointStyle = new PointStyle(PointStyle.SymbolType, (int)Math.Round(size), PointStyle.FillBrush, PointStyle.OutlinePen);
                sizedPointStyle.Draw(new Collection<Feature> { feature }, canvas, labelsInThisLayer, labelsInAllLayers);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            var columns = PointStyle.GetRequiredColumnNames();
            if (!columns.Contains(SizeColumnName)) columns.Add(SizeColumnName);
            return columns;
        }
    }
}
