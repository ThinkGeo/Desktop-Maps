using System;
using System.Linq;
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

            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(helsinkiParcelsLayer);
            MapView.Overlays.Add(layerOverlay);

            MapView.CenterPoint = new PointShape(2777730, 8435220);
            MapView.CurrentScale = 20520;

            _ = MapView.RefreshAsync();
        }

        private WfsV2FeatureLayer CreateHelsinkiParcelsLayer()
        {
            var layer = new WfsV2FeatureLayer(
                "https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows",
                "cp:CadastralParcel")
            {
                TimeoutInSeconds = 500
            };

            layer.ZoomLevelSet.ZoomLevel13.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            layer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layer.FeatureSource.ProjectionConverter = new ProjectionConverter(3067, 3857);

            // Attach event handlers here so they’re always included
            layer.SendingWebRequest += HelsinkiParcelsLayer_SendingWebRequest;

            var featureSource = (WfsV2FeatureSource)layer.FeatureSource;
            featureSource.RequestingData += WFS_RequestingData;

            return layer;
        }

        private void OverlayType_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radio) || radio.IsChecked == false)
                return;

            // Keep background overlays
            var backgroundOverlays = MapView.Overlays
                                            .Where(o => o.Name != "WfsOverlay" && o.Name != "LayerOverlay")
                                            .ToList();
            MapView.Overlays.Clear();
            foreach (var overlay in backgroundOverlays)
                MapView.Overlays.Add(overlay);

            switch (radio.Content.ToString())
            {
                case "WfsV2Overlay":
                    AddWfsV2Overlay();
                    break;

                case "LayerOverlay":
                    AddLayerOverlay();
                    break;
            }

            _ = MapView.RefreshAsync();
        }


        private void AddLayerOverlay()
        {
            // Try to get an existing overlay with the key
            LayerOverlay overlay;
            if (MapView.Overlays.Contains("LayerOverlay"))
            {
                overlay = MapView.Overlays["LayerOverlay"] as LayerOverlay;
                overlay.Layers.Clear(); // clear existing layers
            }
            else
            {
                overlay = new LayerOverlay { TileType = TileType.SingleTile };
            }

            // Add the WFS layer
            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();
            overlay.Layers.Add(helsinkiParcelsLayer);

            // If it’s a new overlay, add to MapView
            if (!MapView.Overlays.Contains("LayerOverlay"))
            {
                MapView.Overlays.Add("LayerOverlay", overlay);
            }
        }

        private void AddWfsV2Overlay()
        {
            // Try to get an existing overlay with the key
            WfsV2Overlay overlay;
            if (MapView.Overlays.Contains("WfsOverlay"))
            {
                overlay = MapView.Overlays["WfsOverlay"] as WfsV2Overlay;
                overlay.FeatureLayer = null; // clear existing layer
            }
            else
            {
                overlay = new WfsV2Overlay()
                {
                    DrawingBulkCount = 500
                };
            }
            
            // Add the WFS layer
            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();
            overlay.FeatureLayer = helsinkiParcelsLayer;

            // If it’s a new overlay, add to MapView
            if (!MapView.Overlays.Contains("WfsOverlay"))
            {
                MapView.Overlays.Add("WfsOverlay", overlay);
            }
        }

        private void HelsinkiParcelsLayer_SendingWebRequest(object sender, SendingWebRequestEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[WFS v1] Request: {e.WebRequest.RequestUri}");
        }

        private void WFS_RequestingData(object sender, RequestingDataWfsFeatureSourceEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[WFS v2] Request: {e.ServiceUrl}");
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