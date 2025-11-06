using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class WFS : IDisposable
    {
        public WFS()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Create WFS v2 overlay
            var wfsOverlay = new WfsV2Overlay
            {
                DrawingBulkCount = 500,
                AsyncLayer = CreateHelsinkiParcelsLayer(),
                IsVisible = false // start hidden
            };
            MapView.Overlays.Add("WfsOverlay", wfsOverlay);

            // Create LayerOverlay
            var layerOverlay = new LayerOverlay() ;
            layerOverlay.Layers.Add(CreateHelsinkiParcelsLayer());
            MapView.Overlays.Add("LayerOverlay", layerOverlay);

            MapView.CenterPoint = new PointShape(2777730, 8435220);
            MapView.CurrentScale = 20520;

            _ = MapView.RefreshAsync();
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

            if (MapView.Overlays.Contains("WfsOverlay") &&
                MapView.Overlays.Contains("LayerOverlay"))
            {
                switch (radio.Content.ToString())
                {
                    case "WfsV2Overlay":
                        MapView.Overlays["WfsOverlay"].IsVisible = true;
                        MapView.Overlays["LayerOverlay"].IsVisible = false;
                        break;

                    case "LayerOverlay":
                        MapView.Overlays["WfsOverlay"].IsVisible = false;
                        MapView.Overlays["LayerOverlay"].IsVisible = true;
                        break;
                }
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