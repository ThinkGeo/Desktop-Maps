using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class WFS
    {
        public WFS()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;

                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                var helsinkiParcelsLayer = new WfsV2FeatureLayer("https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows", "cp:CadastralParcel")
                {
                    TimeoutInSeconds = 500,
                };
                helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
                helsinkiParcelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                helsinkiParcelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(3067, 3857);

                var overlay = new WfsV2Overlay()
                {
                    FeatureLayer = helsinkiParcelsLayer,
                    DrawingBulkCount = 500
                };

                MapView.CurrentExtent = new RectangleShape(2775135, 8437158, 2780320, 8433276);
                MapView.Overlays.Add("LayerOverlay", overlay);
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}