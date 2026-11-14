using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show where coyote sightings crowd together with a HeatmapStyle.
    /// </summary>
    public partial class DisplayHeatMap : IDisposable
    {
        private bool _initialized;

        public DisplayHeatMap()
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

            coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new HeatmapStyle
            {
                Radius = 22,
                Intensity = 0.55f,
                Opacity = 0.85f
            });
            coyoteSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(coyoteSightings);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10778000, 3912500);
            Map.CurrentScale = 68000;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
