/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.VehicleTracking.Properties;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class MapModel
    {
        private static RectangleShape defaultExtent = new RectangleShape(-10785241.6495495, 3916508.33762434, -10778744.5183967, 3912187.74540771);
        private static Cursor handCursor = new Cursor(Application.GetResourceStream(new Uri("/MapSuiteVehicleTracking;component/Image/cursor_hand.cur", UriKind.RelativeOrAbsolute)).Stream);

        private WpfMap mapControl;

        public MapModel()
            : this(null)
        { }

        public MapModel(WpfMap map)
        {
            mapControl = map;
            InitializeMap();
        }

        public WpfMap MapControl
        {
            get { return mapControl; }
        }

        public PopupOverlay PopupOverlay
        {
            get { return (PopupOverlay)mapControl.Overlays["PopupOverlay"]; }
        }

        public LayerOverlay TraceOverlay
        {
            get { return (LayerOverlay)mapControl.Overlays["TraceOverlay"]; }
        }

        public LayerOverlay SpatialFenceOverlay
        {
            get { return (LayerOverlay)mapControl.Overlays["SpatialFenceOverlay"]; }
        }

        public InMemoryFeatureLayer SpatialFenceLayer
        {
            get { return (InMemoryFeatureLayer)SpatialFenceOverlay.Layers["SpatialFenceLayer"]; }
        }

        public TrackMode TrackMode
        {
            get { return mapControl.TrackOverlay.TrackMode; }
            set
            {
                mapControl.TrackOverlay.TrackMode = value;
                mapControl.Cursor = mapControl.TrackOverlay.TrackMode == TrackMode.None ? handCursor : Cursors.Arrow;
            }
        }

        private void InitializeMap()
        {
            TrackMode = TrackMode.None;
            mapControl.MapClick += WpfMap1_MapClick;
            mapControl.MapUnit = GeographyUnit.Meter;
            mapControl.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapControl.CurrentExtent = defaultExtent;

            InitializeMapTools();
            InitializeOverlays();
        }

        public void UpdateUnitSystem(UnitSystem selectedUnitSystem)
        {
            if (mapControl.AdornmentOverlay.Layers.Contains("ScaleBar"))
            {
                ScaleBarAdornmentLayer scaleBarLayer = (ScaleBarAdornmentLayer)mapControl.AdornmentOverlay.Layers["ScaleBar"];
                scaleBarLayer.UnitFamily = selectedUnitSystem;
                mapControl.AdornmentOverlay.Refresh();
            }
        }

        private void WpfMap1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                RectangleShape clickedArea = MapSuiteSampleHelper.GetBufferedRectangle(e.WorldLocation, mapControl.CurrentResolution);
                PopupOverlay.Popups.Clear();
                foreach (InMemoryFeatureLayer vehicleLayer in TraceOverlay.Layers.OfType<InMemoryFeatureLayer>())
                {
                    vehicleLayer.Open();
                    Collection<Feature> resultFeatures = vehicleLayer.QueryTools.GetFeaturesIntersecting(clickedArea, ReturningColumnsType.AllColumns);
                    if (resultFeatures.Count > 0)
                    {
                        Popup popup = new Popup(e.WorldLocation);
                        PopupUserControl popupUserControl = new PopupUserControl(resultFeatures[0]);
                        popupUserControl.PopupOverlay = PopupOverlay;

                        popup.Content = popupUserControl;
                        PopupOverlay.Popups.Add(popup);
                        PopupOverlay.Refresh();
                        break;
                    }
                }
            }
        }

        private void OverlaySwitcher_BaseOverlayChanged(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                bool cancel = ApplyBingMapsKey();
                e.Cancel = cancel;
            }
        }

        private bool ApplyBingMapsKey()
        {
            bool cancel = false;
            string bingMapsKey = MapSuiteSampleHelper.GetBingMapsKey();
            if (!string.IsNullOrEmpty(bingMapsKey))
            {
                foreach (var bingOverlay in MapControl.Overlays.OfType<BingMapsOverlay>())
                {
                    bingOverlay.ApplicationId = bingMapsKey;
                }
            }
            else
            {
                cancel = true;
            }

            return cancel;
        }

        private void InitializeOverlays()
        {
            string cacheFolder = Path.Combine(Path.GetTempPath(), "TileCache");

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapLightOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapLightOverlay.Name = Resources.ThinkGeoCloudMapHybridOverlay;
            thinkGeoCloudMapLightOverlay.TileHeight = 512;
            thinkGeoCloudMapLightOverlay.TileWidth = 512;
            thinkGeoCloudMapLightOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            thinkGeoCloudMapLightOverlay.IsVisible = false;
            thinkGeoCloudMapLightOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapHybridOverlay);
            mapControl.Overlays.Add(thinkGeoCloudMapLightOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapDarkOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapDarkOverlay.Name = Resources.ThinkGeoCloudMapLightOverlay;
            thinkGeoCloudMapDarkOverlay.TileHeight = 512;
            thinkGeoCloudMapDarkOverlay.TileWidth = 512;
            thinkGeoCloudMapDarkOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark;
            thinkGeoCloudMapDarkOverlay.IsVisible = true;
            thinkGeoCloudMapDarkOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapLightOverlay);
            mapControl.Overlays.Add(thinkGeoCloudMapDarkOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapHybridOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            thinkGeoCloudMapHybridOverlay.Name = Resources.ThinkGeoCloudMapAerialOverlay;
            thinkGeoCloudMapHybridOverlay.TileHeight = 512;
            thinkGeoCloudMapHybridOverlay.TileWidth = 512;
            thinkGeoCloudMapHybridOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            thinkGeoCloudMapHybridOverlay.IsVisible = false;
            thinkGeoCloudMapHybridOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapAerialOverlay);
            mapControl.Overlays.Add(thinkGeoCloudMapHybridOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OpenStreetMapName;
            openStreetMapOverlay.TileHeight = 512;
            openStreetMapOverlay.TileWidth = 512;
            openStreetMapOverlay.IsVisible = false;
            openStreetMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, "OpenStreetMap");
            mapControl.Overlays.Add(Resources.OpenStreetMapKey, openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingMapsAerialMapName;
            bingMapsAerialOverlay.TileHeight = 512;
            bingMapsAerialOverlay.TileWidth = 512;
            bingMapsAerialOverlay.MapType = BingMapsMapType.Aerial;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, "BingMapsAerial");
            mapControl.Overlays.Add(Resources.BingAerialKey, bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingMapsRoadMapName;
            bingMapsRoadOverlay.TileHeight = 512;
            bingMapsRoadOverlay.TileWidth = 512;
            bingMapsRoadOverlay.MapType = BingMapsMapType.Road;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, "BingMapsRoad");
            mapControl.Overlays.Add(Resources.BingRoadKey, bingMapsRoadOverlay);

            InMemoryFeatureLayer spatialFenceLayer = new InMemoryFeatureLayer();
            spatialFenceLayer.Open();
            spatialFenceLayer.Columns.Add(new FeatureSourceColumn(Resources.RestrictedName, "Charater", 10));
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 204, 204, 204), 2), new GeoSolidBrush(GeoColor.FromArgb(112, 255, 0, 0)));
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle(Resources.RestrictedName, "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.SimpleColors.White, 2);
            spatialFenceLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay spatialFenceOverlay = new LayerOverlay();
            spatialFenceOverlay.TileType = TileType.SingleTile;
            spatialFenceOverlay.Name = Resources.SpatialFenceOverlayName;
            spatialFenceOverlay.Layers.Add(Resources.SpatialFenceLayerName, spatialFenceLayer);
            mapControl.Overlays.Add(Resources.SpatialFenceOverlayName, spatialFenceOverlay);

            LayerOverlay traceOverlay = new LayerOverlay();
            traceOverlay.Name = Resources.TraceOverlayName;
            traceOverlay.TileType = TileType.SingleTile;
            mapControl.Overlays.Add(Resources.TraceOverlayName, traceOverlay);

            mapControl.Overlays.Add(Resources.PopupOverlayName, new PopupOverlay { Name = Resources.PopupOverlayName });

            ScaleBarAdornmentLayer scaleBarAdormentLayer = new ScaleBarAdornmentLayer();
            scaleBarAdormentLayer.UnitFamily = UnitSystem.Metric;
            mapControl.AdornmentOverlay.Layers.Add(Resources.ScaleBarName, scaleBarAdormentLayer);
        }

        private void InitializeMapTools()
        {
            mapControl.MapTools.MouseCoordinate.IsEnabled = true;
            mapControl.MapTools.MouseCoordinate.Visibility = Visibility.Hidden;
            mapControl.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
            mapControl.MapTools.PanZoomBar.GlobeButtonClick += PanZoomBar_GlobeButtonClick;

            OverlaySwitcher overlaySwitcher = new OverlaySwitcher();
            overlaySwitcher.Initialize(mapControl);
            overlaySwitcher.OverlayChanged += OverlaySwitcher_BaseOverlayChanged;
            mapControl.MapTools.Add(overlaySwitcher);
        }

        private static void PanZoomBar_GlobeButtonClick(object sender, GlobeButtonClickPanZoomBarMapToolEventArgs e)
        {
            e.NewExtent = defaultExtent;
        }
    }
}