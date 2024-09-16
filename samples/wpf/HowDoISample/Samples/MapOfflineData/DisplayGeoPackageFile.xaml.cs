using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayGeoPackageFile.xaml
    /// </summary>
    public partial class DisplayGeoPackageFile : IDisposable
    {
        public DisplayGeoPackageFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the Cloud Aerial Overlay as the base overlay 
            var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1
            };
            MapView.Overlays.Add(cloudOverlay);

            // Create the gdalFeatureLayer
            var gdalFeatureLayer = new GdalFeatureLayer(@"./Data/GeoPackage/mora_surficial_geology.gpkg");
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.DarkGray, 2F, GeoColors.GhostWhite, 4F, false);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightCyan, GeoColors.Black, 1);
            gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a projection converter from NAD83 UTM Zone 10N (EPSG:26910) to Spherical Mercator (EPSG:3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(26910, 3857);
            gdalFeatureLayer.FeatureSource.ProjectionConverter = projectionConverter;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(gdalFeatureLayer);
            MapView.Overlays.Add(layerOverlay);

            // Set the current extent
            gdalFeatureLayer.Open();
            MapView.CurrentExtent = gdalFeatureLayer.GetBoundingBox();

            await MapView.RefreshAsync();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
