/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class MapModel
    {
        private static readonly RectangleShape defaultExtent = new RectangleShape(-19062735.6816748, 9273256.52450252, -5746827.16371793, 2673516.56066139);
        private static Cursor handCursor = new Cursor(MapSuiteSampleHelper.GetImageStream("/Image/cursor_hand.cur"));

        private WpfMap mapControl;
        private InMemoryFeatureLayer markerMemoryLayer;
        private InMemoryFeatureLayer markerMemoryHighlightLayer;

        public event EventHandler<QueryAreaChangedMapModelEventArgs> QueryAreaChanged;

        public MapModel()
            : this(null)
        { }

        public MapModel(WpfMap map)
        {
            MapControl = map;
        }

        public WpfMap MapControl
        {
            get { return mapControl; }
            set
            {
                mapControl = value;
                InitializeMap();
            }
        }

        public TrackMode TrackMode
        {
            get { return mapControl.TrackOverlay.TrackMode; }
            set
            {
                mapControl.TrackOverlay.TrackMode = value;
                mapControl.Cursor = value == TrackMode.None ? handCursor : Cursors.Arrow;
            }
        }

        public LayerOverlay StyleLayerOverlay
        {
            get { return (LayerOverlay)mapControl.Overlays[Resources.StyleLayerOverLayKey]; }
        }

        public void ClearTrackResult()
        {
            mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            markerMemoryLayer.InternalFeatures.Clear();
            markerMemoryHighlightLayer.InternalFeatures.Clear();

            mapControl.Refresh(mapControl.TrackOverlay);
            mapControl.Refresh(mapControl.Overlays[Resources.MarkerOverlayKey]);
        }

        public void UpdateHighlightMarker(Feature epicenterFeature)
        {
            markerMemoryHighlightLayer.InternalFeatures.Clear();
            markerMemoryHighlightLayer.InternalFeatures.Add(epicenterFeature);

            mapControl.Refresh(mapControl.Overlays[Resources.MarkerOverlayKey]);
            mapControl.CenterAt(epicenterFeature);
        }

        public void SetEarthquakeLegendsVisible(Layer currentLayer)
        {
            EarthquakeIsoLineFeatureLayer earthquakeFeatureLayer = currentLayer as EarthquakeIsoLineFeatureLayer;
            LegendAdornmentLayer earthquakeLegendLayer = GetEarthquakeLegendLayer(earthquakeFeatureLayer);
            if (earthquakeLegendLayer != null)
            {
                earthquakeLegendLayer.IsVisible = earthquakeFeatureLayer != null;
            }
        }

        public void RefreshMarkersByFeatures(IEnumerable<Feature> features)
        {
            markerMemoryLayer.InternalFeatures.Clear();
            markerMemoryHighlightLayer.InternalFeatures.Clear();

            if (features != null)
            {
                foreach (Feature item in features)
                {
                    markerMemoryLayer.InternalFeatures.Add(item);
                }
            }

            mapControl.Overlays[Resources.MarkerOverlayKey].Refresh();
        }

        protected void OnQueryAreaChanged(BaseShape queringArea)
        {
            EventHandler<QueryAreaChangedMapModelEventArgs> handler = QueryAreaChanged;
            if (handler != null) handler(this, new QueryAreaChangedMapModelEventArgs(new Feature(queringArea)));
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            try
            {
                PolygonShape[] allPolygonFeatures = mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Select(f => f.GetShape()).OfType<PolygonShape>().ToArray();
                BaseShape trackedShape = SqlTypesGeometryHelper.Union(allPolygonFeatures);
                if (trackedShape != null)
                {
                    OnQueryAreaChanged(trackedShape);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void InitializeMap()
        {
            mapControl.MapUnit = GeographyUnit.Meter;
            mapControl.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapControl.CurrentExtent = defaultExtent;
            mapControl.MapTools.Logo.IsEnabled = false;
            mapControl.MapTools.MouseCoordinate.IsEnabled = true;
            mapControl.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
            mapControl.MapTools.MouseCoordinate.Visibility = Visibility.Hidden;
            mapControl.MapTools.PanZoomBar.GlobeButtonClick += (s, e) => e.NewExtent = defaultExtent;

            OverlaySwitcher overlaySwitcher = new OverlaySwitcher();
            overlaySwitcher.Initialize(mapControl);
            overlaySwitcher.OverlayChanged += OverlaySwitcherOverlayChanged;
            mapControl.MapTools.Add(overlaySwitcher);

            InitializeOverlays();
        }

        private void OverlaySwitcherOverlayChanged(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                e.Cancel = ApplyBingMapsKey();
            }
        }

        private bool ApplyBingMapsKey()
        {
            bool cancel = false;
            string bingMapsKey = MapSuiteSampleHelper.GetBingMapsKey();
            if (!string.IsNullOrEmpty(bingMapsKey))
            {
                foreach (BingMapsOverlay bingOverlay in MapControl.Overlays.OfType<BingMapsOverlay>())
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
            ThinkGeoCloudRasterMapsOverlay worldMapKitRoadOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitRoadOverlay.Name = Resources.ThinkGeoCloudMapsLight;
            worldMapKitRoadOverlay.TileHeight = 512;
            worldMapKitRoadOverlay.TileWidth = 512;
            worldMapKitRoadOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            worldMapKitRoadOverlay.IsVisible = true;
            worldMapKitRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsLight);
            mapControl.Overlays.Add(worldMapKitRoadOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialOverlay.Name = Resources.ThinkGeoCloudMapsAerial;
            worldMapKitAerialOverlay.TileHeight = 512;
            worldMapKitAerialOverlay.TileWidth = 512;
            //worldMapKitAerialOverlay.Projection = WorldMapKitProjection.SphericalMercator;
            worldMapKitAerialOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
            worldMapKitAerialOverlay.IsVisible = false;
            worldMapKitAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsAerial);
            mapControl.Overlays.Add(worldMapKitAerialOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialWithLabelsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialWithLabelsOverlay.Name = Resources.ThinkGeoCloudMapsHybrid;
            worldMapKitAerialWithLabelsOverlay.TileHeight = 512;
            worldMapKitAerialWithLabelsOverlay.TileWidth = 512;
            //worldMapKitAerialWithLabelsOverlay.Projection = WorldMapKitProjection.SphericalMercator;
            worldMapKitAerialWithLabelsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            worldMapKitAerialWithLabelsOverlay.IsVisible = false;
            worldMapKitAerialWithLabelsOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsHybrid);
            mapControl.Overlays.Add(worldMapKitAerialWithLabelsOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OSMOverlayName;
            openStreetMapOverlay.TileHeight = 512;
            openStreetMapOverlay.TileWidth = 512;
            openStreetMapOverlay.IsVisible = false;
            openStreetMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.OSMOverlayName);
            mapControl.Overlays.Add(openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingMapsAerialOverlayName;
            bingMapsAerialOverlay.TileHeight = 512;
            bingMapsAerialOverlay.TileWidth = 512;
            bingMapsAerialOverlay.MapType = Wpf.BingMapsMapType.Aerial;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsAerialOverlayName);
            mapControl.Overlays.Add(bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingMapsRoadOverlayName;
            bingMapsRoadOverlay.TileHeight = 512;
            bingMapsRoadOverlay.TileWidth = 512;
            bingMapsRoadOverlay.MapType = Wpf.BingMapsMapType.Road;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsRoadOverlayName);
            mapControl.Overlays.Add(bingMapsRoadOverlay);

            LayerOverlay styleLayersOverLay = new LayerOverlay();
            styleLayersOverLay.TileType = TileType.SingleTile;
            mapControl.Overlays.Add(Resources.StyleLayerOverLayKey, styleLayersOverLay);

            Proj4Projection wgs84ToMercatorProjection = new Proj4Projection();
            wgs84ToMercatorProjection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            wgs84ToMercatorProjection.ExternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();

            EarthquakeHeatFeatureLayer heatLayer = new EarthquakeHeatFeatureLayer(new ShapeFileFeatureSource(ConfigurationManager.AppSettings["DataShapefileFileName"]));
            heatLayer.HeatStyle = new HeatStyle(10, 100, Resources.MagnitudeColumnName, 0, 12, 100, DistanceUnit.Kilometer);
            heatLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            heatLayer.Name = Resources.HeatStyleLayerName;
            styleLayersOverLay.Layers.Add(heatLayer);

            ShapeFileFeatureLayer pointLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["DataShapefileFileName"]);
            pointLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            pointLayer.Name = Resources.PointStyleLayerName;
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.Red, 6, GeoColor.StandardColors.White, 1);
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            styleLayersOverLay.Layers.Add(pointLayer);

            EarthquakeIsoLineFeatureLayer isoLineLayer = new EarthquakeIsoLineFeatureLayer(new ShapeFileFeatureSource(ConfigurationManager.AppSettings["DataShapefileFileName"]));
            isoLineLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            isoLineLayer.Name = Resources.IsolineStyleLayerName;
            styleLayersOverLay.Layers.Add(isoLineLayer);

            //Setup TarckOverlay.
            mapControl.TrackOverlay = new RadiusMearsureTrackInteractiveOverlay(DistanceUnit.Mile, mapControl.MapUnit);
            mapControl.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            LayerOverlay markerOverlay = new LayerOverlay();
            mapControl.Overlays.Add(Resources.MarkerOverlayKey, markerOverlay);

            PointStyle highLightStyle = new PointStyle();
            highLightStyle.CustomPointStyles.Add(PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(50, GeoColor.SimpleColors.Blue), 20, GeoColor.SimpleColors.LightBlue, 1));
            highLightStyle.CustomPointStyles.Add(PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.LightBlue, 9, GeoColor.SimpleColors.Blue, 1));

            markerMemoryLayer = new InMemoryFeatureLayer();
            markerMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.Orange, 8, GeoColor.SimpleColors.White, 1);
            markerMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            markerOverlay.Layers.Add(markerMemoryLayer);

            markerMemoryHighlightLayer = new InMemoryFeatureLayer();
            markerMemoryHighlightLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = highLightStyle;
            markerMemoryHighlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            markerOverlay.Layers.Add(markerMemoryHighlightLayer);

            ScaleBarAdornmentLayer scaleBarAdornmentLayer = new ScaleBarAdornmentLayer();
            scaleBarAdornmentLayer.UnitFamily = UnitSystem.Imperial;
            mapControl.AdornmentOverlay.Layers.Add(scaleBarAdornmentLayer);
        }

        private LegendAdornmentLayer GetEarthquakeLegendLayer(EarthquakeIsoLineFeatureLayer earthquakeFeatureLayer)
        {
            LegendAdornmentLayer isoLevelLegendLayer = null;
            if (mapControl.AdornmentOverlay.Layers.Contains(Resources.IsoLineLevelLegendLayerName))
            {
                isoLevelLegendLayer = (LegendAdornmentLayer)mapControl.AdornmentOverlay.Layers[Resources.IsoLineLevelLegendLayerName];
            }
            else if (earthquakeFeatureLayer != null)
            {
                isoLevelLegendLayer = new LegendAdornmentLayer();
                isoLevelLegendLayer.Width = 85;
                isoLevelLegendLayer.Height = 320;
                isoLevelLegendLayer.Location = AdornmentLocation.LowerRight;
                isoLevelLegendLayer.ContentResizeMode = LegendContentResizeMode.Fixed;
                mapControl.AdornmentOverlay.Layers.Add(Resources.IsoLineLevelLegendLayerName, isoLevelLegendLayer);

                LegendItem flagLegendItem = new LegendItem();
                flagLegendItem.TextStyle = new TextStyle("Magnitude", new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Black));
                flagLegendItem.TextLeftPadding = -20;
                isoLevelLegendLayer.LegendItems.Add(flagLegendItem);

                for (int i = 0; i < earthquakeFeatureLayer.IsoLineLevels.Count; i++)
                {
                    LegendItem legendItem = new LegendItem();
                    legendItem.TextStyle = new TextStyle(earthquakeFeatureLayer.IsoLineLevels[i].ToString("f2"), new GeoFont("Arial", 10), new GeoSolidBrush(GeoColor.StandardColors.Black));
                    legendItem.ImageStyle = earthquakeFeatureLayer.LevelClassBreakStyle.ClassBreaks[i].DefaultAreaStyle;
                    legendItem.ImageWidth = 25;

                    isoLevelLegendLayer.LegendItems.Add(legendItem);
                }
            }

            return isoLevelLegendLayer;
        }
    }
}