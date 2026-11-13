using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw the coyote sightings whose behavior matches a regular expression with a RegexStyle.
    /// </summary>
    public partial class RenderBasedOnRegex : IDisposable
    {
        private bool _initialized;

        public RenderBasedOnRegex()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var coyoteSightings = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco_Coyote_Sightings.shp"));
            // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            coyoteSightings.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var regexStyle = new RegexStyle { ColumnName = "Behavior" };
            regexStyle.RegexItems.Add(new RegexItem("injured|sick", new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Red)));

            coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new PointStyle(PointSymbolType.Circle, 5, GeoBrushes.Green));
            coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(regexStyle);
            coyoteSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(coyoteSightings);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10778600, 3915300);
            Map.CurrentScale = 22320;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
