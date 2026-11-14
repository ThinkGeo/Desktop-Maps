using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Group nearby coyote sightings into one symbol with a count, using a ClusterPointStyle.
    /// </summary>
    public partial class DisplayClusterPoints : IDisposable
    {
        private bool _initialized;

        public DisplayClusterPoints()
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

            var unclusteredPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Orange, 10);
            var countTextStyle = new TextStyle("FeatureCount", new GeoFont("Noto Sans", 12, DrawingFontStyles.Bold), GeoBrushes.DimGray)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                YOffsetInPixel = 12
            };
            var clusterPointStyle = new ClusterPointStyle(unclusteredPointStyle, countTextStyle)
            {
                MinimumFeaturesPerCellToCluster = 2,
                ClusteredPointStyle = new PointStyle(new GeoImage(Path.Combine(AppContext.BaseDirectory, "Resources", "coyote_paw.png")))
                {
                    ImageScale = .25,
                    Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                    MaskType = MaskType.RoundedCorners
                }
            };
            coyoteSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);
            coyoteSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(coyoteSightings);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10780320, 3915120);
            Map.CurrentScale = 288900;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
