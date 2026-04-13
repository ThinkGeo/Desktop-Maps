using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class WFS : IDisposable
    {

        private bool _initialized;
        public WFS()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create WFS v2 overlay
            var wfsOverlay = new WfsV2Overlay
            {
                DrawingBulkCount = 500,
                AsyncLayer = CreateHelsinkiParcelsLayer(),
                IsVisible = false // start hidden
            };
            Map.Overlays.Add("WfsOverlay", wfsOverlay);

            // Create LayerOverlay
            var layerOverlay = new LayerOverlay() ;
            layerOverlay.Layers.Add(CreateHelsinkiParcelsLayer());
            Map.Overlays.Add("LayerOverlay", layerOverlay);

            Map.CenterPoint = new PointShape(2777730, 8435220);
            Map.CurrentScale = 20520;

            _ = Map.RefreshAsync();
        }

        private WfsV2AsyncLayer CreateHelsinkiParcelsLayer()
        {
            var layer = new WfsV2AsyncLayer(
                "https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows",
                "cp:CadastralParcel")
            {
                TimeoutInSeconds = 500
            };

            layer.ZoomLevelSet.ZoomLevel13.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            layer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layer.ProjectionConverter = new ProjectionConverter(3067, 3857);

            // For .NET Framework, add a reference to System.Net.Http for this to compile.
            // You don't need to do this for .NET 8+.
            //
            //layer.SendingHttpRequest += (sender, e) =>
            //    System.Diagnostics.Debug.WriteLine($"Sending Request: {e.HttpRequestMessage.RequestUri}");

            return layer;
        }

        private void OverlayType_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radio) || radio.IsChecked == false)
                return;

            if (Map.Overlays.Contains("WfsOverlay") &&
                Map.Overlays.Contains("LayerOverlay"))
            {
                switch (radio.Content.ToString())
                {
                    case "WfsV2Overlay":
                        Map.Overlays["WfsOverlay"].IsVisible = true;
                        Map.Overlays["LayerOverlay"].IsVisible = false;
                        break;

                    case "LayerOverlay":
                        Map.Overlays["WfsOverlay"].IsVisible = false;
                        Map.Overlays["LayerOverlay"].IsVisible = true;
                        break;
                }
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}