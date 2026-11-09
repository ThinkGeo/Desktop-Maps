using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayGeoPackageFile.xaml
    /// </summary>
    public partial class DisplayGeoPackageFile
    {

        private bool _initialized;
        public DisplayGeoPackageFile()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // Create the Cloud Aerial Overlay as the base overlay 
            var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Aerial2_V2_X1
            };
            Map.Overlays.Add(cloudOverlay);

            // Creat a new layerOverlay to hold the gdalFeatureLayers
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            var projectionConverter = new ProjectionConverter(26910, 3857);
            projectionConverter.Open();

            // Create the gdalFeatureLayers
            var gdalFeatureLayer = new GdalFeatureLayer(@"./Data/GeoPackage/mora_surficial_geology.gpkg");
            gdalFeatureLayer.FeatureSource.ProjectionConverter = projectionConverter;
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.LightRed, 4);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(128, GeoColors.LightSteelBlue), 2F, GeoColors.Black, 2F, false);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(64, GeoColors.LightGreen), GeoColors.Black, 1);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(gdalFeatureLayer);

            Map.Overlays.Add(layerOverlay);

            gdalFeatureLayer.Open();
            var gdalFeatureLayerBBox = gdalFeatureLayer.GetBoundingBox();
            Map.CenterPoint = gdalFeatureLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, gdalFeatureLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            _ = Map.RefreshAsync();

            projectionConverter.Close();
            gdalFeatureLayer.Close();
        }
    }
}
