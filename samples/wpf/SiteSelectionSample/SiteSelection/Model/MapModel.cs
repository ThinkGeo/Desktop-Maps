/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class MapModel
    {
        private static readonly string imageUriTemplate = @"pack://application:,,,/SiteSelection;component/Image/{0}";
        private static readonly Cursor handCursor = new Cursor(GetImageStream("cursor_hand.cur"));
        private static readonly RectangleShape globeExtent = new RectangleShape(-10782584.5066971, 3916291.54400321, -10769504.2529497, 3907774.56943023);

        private WpfMap mapControl;
        private PointShape plottedPoint;
        private BaseShape restrictionShape;
        private ShapeFileFeatureLayer hotelsLayer;
        private SimpleMarkerOverlay markerOverlay;
        private ShapeFileFeatureLayer schoolsLayer;
        private ShapeFileFeatureLayer restrictedLayer;
        private InMemoryFeatureLayer serviceAreaLayer;
        private ShapeFileFeatureLayer restaurantsLayer;
        private ShapeFileFeatureLayer medicalFacilitesLayer;
        private ShapeFileFeatureLayer publicFacilitesLayer;
        private ShapeFileFeatureLayer queryingFeatureLayer;
        private InMemoryFeatureLayer highlightFeatureLayer;
        private GeoCollection<ShapeFileFeatureLayer> poiFeatureLayers;

        public event EventHandler<PlottedPointChangedMapModelEventArgs> PlottedPointChanged;

        public MapModel()
            : this(null)
        { }

        public MapModel(WpfMap wpfMap)
            : base()
        {
            mapControl = wpfMap;

            InitializeMapControl();
            InitializeMapTools();
            InitializeOverlays();
            InitializeAdornments();
        }

        public WpfMap MapControl
        {
            get { return mapControl; }
            set { mapControl = value; }
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

        public PointShape PlottedPoint
        {
            get { return plottedPoint; }
            set { plottedPoint = value; }
        }

        public Collection<Feature> ServiceAreaFeatures
        {
            get { return serviceAreaLayer.InternalFeatures; }
        }

        public ShapeFileFeatureLayer QueryingFeatureLayer
        {
            get { return queryingFeatureLayer; }
            set { queryingFeatureLayer = value; }
        }

        public GeoCollection<ShapeFileFeatureLayer> PoiFeatureLayers
        {
            get { return poiFeatureLayers; }
        }

        public void UpdateUnitSystem(UnitSystem unitSystem)
        {
            ScaleBarAdornmentLayer adornmentLayer = mapControl.AdornmentOverlay.Layers[Resources.ScaleBarLayerKey] as ScaleBarAdornmentLayer;
            if (adornmentLayer != null)
            {
                adornmentLayer.UnitFamily = unitSystem;
                mapControl.AdornmentOverlay.Refresh();
            }
        }

        public Collection<string> GetColumnValueCandidates()
        {
            string columnName = MapSuiteSampleHelper.GetDefaultColumnNameByPoiType(queryingFeatureLayer.Name);
            Collection<string> candidates = new Collection<string>();
            if (columnName.Equals("Hotels"))
            {
                candidates.Add("1 ~ 50");
                candidates.Add("50 ~ 100");
                candidates.Add("100 ~ 150");
                candidates.Add("150 ~ 200");
                candidates.Add("200 ~ 300");
                candidates.Add("300 ~ 400");
                candidates.Add("400 ~ 500");
                candidates.Add(">= 500");
            }
            else
            {
                QueryingFeatureLayer.Open();
                IEnumerable<string> distinctColumnValues = QueryingFeatureLayer.FeatureSource.GetDistinctColumnValues(columnName).Select(v => v.ColumnValue);
                foreach (var distinctColumnValue in distinctColumnValues)
                {
                    candidates.Add(distinctColumnValue);
                }
            }
            return candidates;
        }

        public void AddMarkersByFeatures(IEnumerable<Feature> features)
        {
            markerOverlay.Markers.Clear();

            highlightFeatureLayer.InternalFeatures.Clear();
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = queryingFeatureLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle;

            if (plottedPoint != null)
            {
                markerOverlay.Markers.Add(CreateMarker(plottedPoint, "drawPoint.png"));
            }

            if (features != null)
            {
                foreach (Feature feature in features)
                {
                    highlightFeatureLayer.InternalFeatures.Add(feature);
                    PopupUserControl toolTip = new PopupUserControl(feature);
                    Marker marker = CreateMarker((PointShape)feature.GetShape(), "selectedHalo.png", 0, toolTip);
                    markerOverlay.Markers.Add(marker);
                }
            }
        }

        public Collection<Feature> GetFeaturesWithinServiceArea()
        {
            Collection<Feature> resultFeatures = new Collection<Feature>();
            if (queryingFeatureLayer != null && ServiceAreaFeatures.Count > 0)
            {
                queryingFeatureLayer.Open();
                BaseShape pinnedServiceArea = ServiceAreaFeatures[0].GetShape();
                Collection<Feature> tempFeatures = QueryingFeatureLayer.QueryTools.GetFeaturesWithin(pinnedServiceArea, ReturningColumnsType.AllColumns);
                foreach (var tempFeature in tempFeatures)
                {
                    resultFeatures.Add(tempFeature);
                }
            }

            return resultFeatures;
        }

        protected void OnPlacedPositionChanged(PointShape point)
        {
            EventHandler<PlottedPointChangedMapModelEventArgs> handler = PlottedPointChanged;
            if (handler != null)
            {
                handler(this, new PlottedPointChangedMapModelEventArgs(point));
            }
        }

        private void InitializeMapControl()
        {
            mapControl.MapUnit = GeographyUnit.Meter;
            mapControl.CurrentExtent = globeExtent;
            mapControl.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
        }

        private void InitializeOverlays()
        {
            poiFeatureLayers = new GeoCollection<ShapeFileFeatureLayer>();
            string cacheFolder = Path.Combine(Path.GetTempPath(), "TileCache");

            mapControl.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay lightMapOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            lightMapOverlay.Name = Resources.ThinkGeoCloudLightMap;
            lightMapOverlay.TileHeight = 512;
            lightMapOverlay.TileWidth = 512;
            lightMapOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            lightMapOverlay.IsVisible = true;
            lightMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudLightMap);
            mapControl.Overlays.Add(lightMapOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay aerialMapOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            aerialMapOverlay.Name = Resources.ThinkGeoCloudAerialMap;
            aerialMapOverlay.TileHeight = 512;
            aerialMapOverlay.TileWidth = 512;
            aerialMapOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
            aerialMapOverlay.IsVisible = false;
            aerialMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudAerialMap);
            mapControl.Overlays.Add(aerialMapOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialWithLabelsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialWithLabelsOverlay.Name = Resources.ThinkGeoCloudHybrid;
            worldMapKitAerialWithLabelsOverlay.TileHeight = 512;
            worldMapKitAerialWithLabelsOverlay.TileWidth = 512;
            worldMapKitAerialWithLabelsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            worldMapKitAerialWithLabelsOverlay.IsVisible = false;
            worldMapKitAerialWithLabelsOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudHybrid);
            mapControl.Overlays.Add(worldMapKitAerialWithLabelsOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OSMOverlayName;
            openStreetMapOverlay.TileWidth = 512;
            openStreetMapOverlay.TileHeight = 512;
            openStreetMapOverlay.IsVisible = false;
            openStreetMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.OSMOverlayName);
            mapControl.Overlays.Add(Resources.OSMOverlayName, openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingMapsAerialOverlayName;
            bingMapsAerialOverlay.TileWidth = 512;
            bingMapsAerialOverlay.TileHeight = 512;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.MapType = Wpf.BingMapsMapType.Aerial;
            bingMapsAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsAerialOverlayName);
            mapControl.Overlays.Add(Resources.BingMapsAerialOverlayName, bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingMapsRoadOverlayName;
            bingMapsRoadOverlay.TileWidth = 512;
            bingMapsRoadOverlay.TileHeight = 512;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.MapType = Wpf.BingMapsMapType.Road;
            bingMapsRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsRoadOverlayName);
            mapControl.Overlays.Add(Resources.BingMapsRoadOverlayName, bingMapsRoadOverlay);

            LayerOverlay customOverlay = new LayerOverlay();
            customOverlay.Name = Resources.CustomOverlayKey;
            customOverlay.TileType = TileType.SingleTile;
            mapControl.Overlays.Add(Resources.CustomOverlayKey, customOverlay);

            Proj4Projection wgs84ToMercatorProj4Projection = new Proj4Projection();
            wgs84ToMercatorProj4Projection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            wgs84ToMercatorProj4Projection.ExternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();
            wgs84ToMercatorProj4Projection.Open();

            restrictedLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["RestrictedShapeFilePathName"]);
            restrictedLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.White, 5.5f), new GeoSolidBrush(GeoColor.SimpleColors.Transparent)));
            restrictedLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.Red, 1.5f) { DashStyle = LineDashStyle.Dash }, new GeoSolidBrush(GeoColor.SimpleColors.Transparent)));
            restrictedLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            restrictedLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            customOverlay.Layers.Add(Resources.RestrictedLayerKey, restrictedLayer);

            serviceAreaLayer = new InMemoryFeatureLayer();
            serviceAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(new GeoColor(120, GeoColor.FromHtml("#1749c9")), GeoColor.FromHtml("#fefec1"), 3, LineDashStyle.Solid);
            serviceAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            customOverlay.Layers.Add(Resources.ServiceAreaLayerKey, serviceAreaLayer);

            LayerOverlay poiOverlay = new LayerOverlay();
            poiOverlay.TileType = TileType.SingleTile;
            mapControl.Overlays.Add(Resources.PoiOverlayKey, poiOverlay);

            hotelsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["HotelsShapeFilePathName"]);
            hotelsLayer.Name = Resources.HotelsLayerKey;
            hotelsLayer.Transparency = 120f;
            hotelsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage(GetImageStream("Hotel.png")));
            hotelsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            hotelsLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            poiOverlay.Layers.Add(Resources.HotelsLayerKey, hotelsLayer);

            medicalFacilitesLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["MedicalFacilitiesShapeFilePathName"]);
            medicalFacilitesLayer.Name = Resources.MedicalFacilitesLayerKey;
            medicalFacilitesLayer.Transparency = 120f;
            medicalFacilitesLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage(GetImageStream("DrugStore.png")));
            medicalFacilitesLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            medicalFacilitesLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            poiOverlay.Layers.Add(Resources.MedicalFacilitesLayerKey, medicalFacilitesLayer);

            publicFacilitesLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["PublicFacilitiesShapeFilePathName"]);
            publicFacilitesLayer.Name = Resources.PublicFacilitesLayerKey;
            publicFacilitesLayer.Transparency = 120f;
            publicFacilitesLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage(GetImageStream("public_facility.png")));
            publicFacilitesLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            publicFacilitesLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            poiOverlay.Layers.Add(Resources.PublicFacilitesLayerKey, publicFacilitesLayer);

            restaurantsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["RestaurantsShapeFilePathName"]);
            restaurantsLayer.Name = Resources.RestaurantsLayerKey;
            restaurantsLayer.Transparency = 120f;
            restaurantsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage(GetImageStream("restaurant.png")));
            restaurantsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            restaurantsLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            poiOverlay.Layers.Add(Resources.RestaurantsLayerKey, restaurantsLayer);

            schoolsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["SchoolsShapeFilePathName"]);
            schoolsLayer.Name = Resources.SchoolsLayerKey;
            schoolsLayer.Transparency = 120f;
            schoolsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage(GetImageStream("school.png")));
            schoolsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            schoolsLayer.FeatureSource.Projection = wgs84ToMercatorProj4Projection;
            poiOverlay.Layers.Add(Resources.SchoolsLayerKey, schoolsLayer);

            highlightFeatureLayer = new InMemoryFeatureLayer();
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            markerOverlay = new SimpleMarkerOverlay();
            mapControl.Overlays.Add(Resources.MakerOverlayKey, markerOverlay);

            poiFeatureLayers.Add(hotelsLayer.Name, hotelsLayer);
            poiFeatureLayers.Add(medicalFacilitesLayer.Name, medicalFacilitesLayer);
            poiFeatureLayers.Add(restaurantsLayer.Name, restaurantsLayer);
            poiFeatureLayers.Add(schoolsLayer.Name, schoolsLayer);
            poiOverlay.Layers.Add(Resources.HighlightFeatureLayerKey, highlightFeatureLayer);
        }

        private void InitializeAdornments()
        {
            LegendAdornmentLayer legendlayer = new LegendAdornmentLayer();
            legendlayer.Height = 135;
            legendlayer.Location = AdornmentLocation.LowerRight;
            mapControl.AdornmentOverlay.Layers.Add(legendlayer);

            AddLegendItem(hotelsLayer, legendlayer);
            AddLegendItem(medicalFacilitesLayer, legendlayer);
            AddLegendItem(publicFacilitesLayer, legendlayer);
            AddLegendItem(restaurantsLayer, legendlayer);
            AddLegendItem(schoolsLayer, legendlayer);

            ScaleBarAdornmentLayer scaleBar = new ScaleBarAdornmentLayer();
            scaleBar.Location = AdornmentLocation.LowerLeft;
            mapControl.AdornmentOverlay.Layers.Add(Resources.ScaleBarLayerKey, scaleBar);
        }

        private void InitializeMapTools()
        {
            mapControl.MapTools.Logo.IsEnabled = false;
            mapControl.MapTools.MouseCoordinate.IsEnabled = true;
            mapControl.MapTools.MouseCoordinate.Visibility = Visibility.Hidden;
            mapControl.MapTools.MouseCoordinate.Margin = new Thickness(0, 0, 180, 5);
            mapControl.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
            mapControl.MapTools.PanZoomBar.GlobeButtonClick += (s, e) => e.NewExtent = globeExtent;

            OverlaySwitcher overlaySwitcher = new OverlaySwitcher();
            overlaySwitcher.Initialize(mapControl);
            overlaySwitcher.OverlayChanged += OverlaySwitcher_BaseOverlayChanged;
            mapControl.MapTools.Add(overlaySwitcher);
        }

        private BaseShape GetRestictionShape()
        {
            if (restrictionShape == null)
            {
                restrictedLayer.Open();
                restrictionShape = restrictedLayer.QueryTools.GetFeatureById("1", ReturningColumnsType.NoColumns).GetShape();
            }

            return restrictionShape;
        }

        private void OverlaySwitcher_BaseOverlayChanged(object sender, OverlayChangedOverlaySwitcherEventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = e.Overlay as BingMapsOverlay;
            if (bingMapsOverlay != null)
            {
                bool cancel = false;
                string bingMapsKey = MapSuiteSampleHelper.GetBingMapsApplicationId();
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

                e.Cancel = cancel;
            }

            if (e.Overlay.Name.Equals(Resources.ThinkGeoCloudAerialMap) ||
                e.Overlay.Name.Equals(Resources.ThinkGeoCloudHybrid) ||
                e.Overlay.Name.Equals(Resources.ThinkGeoCloudHybrid))
                mapControl.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            else
                mapControl.ZoomLevelSet = new SphericalMercatorZoomLevelSet();
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            if (mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Count > 0)
            {
                restrictionShape = GetRestictionShape();

                PointShape clickedShape = mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures[0].GetShape() as PointShape;
                if (QueryingFeatureLayer != null && clickedShape != null && restrictionShape != null && clickedShape.IsWithin(restrictionShape))
                {
                    plottedPoint = clickedShape;
                    OnPlacedPositionChanged(plottedPoint);
                }
                else
                {
                    SiteSelectionWarningWindow warningWindow = new SiteSelectionWarningWindow();
                    warningWindow.Owner = Application.Current.MainWindow;
                    warningWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    warningWindow.ShowDialog();
                }

                mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            }
        }

        private static Stream GetImageStream(string imageName)
        {
            Stream imageStream = null;
            Uri imageUri = new Uri(string.Format(CultureInfo.InvariantCulture, imageUriTemplate, imageName), UriKind.RelativeOrAbsolute);
            StreamResourceInfo imageStreamInfo = Application.GetResourceStream(imageUri);
            if (imageStreamInfo != null)
                imageStream = imageStreamInfo.Stream;
            return imageStream;
        }

        private static Marker CreateMarker(PointShape point, string imageName, double yOffsetRatio = 0.5, object toolTip = null)
        {
            Marker marker = new Marker(point);
            marker.ImageSource = new BitmapImage(new Uri(string.Format(CultureInfo.InvariantCulture, imageUriTemplate, imageName), UriKind.RelativeOrAbsolute));
            marker.YOffset = -marker.ImageSource.Height * yOffsetRatio;
            if (toolTip != null)
                marker.ToolTip = toolTip;

            return marker;
        }

        private static void AddLegendItem(FeatureLayer featureLayer, LegendAdornmentLayer legendLayer)
        {
            LegendItem legendItem = new LegendItem();
            legendItem.ImageStyle = featureLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle;
            legendItem.TextStyle = WorldStreetsTextStyles.GeneralPurpose(featureLayer.Name, 10);
            legendLayer.LegendItems.Add(featureLayer.Name, legendItem);
        }
    }
}