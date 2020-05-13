/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class MapModel
    {
        private WinformsMap mapControl;
        private PointShape plottedPoint;
        private BaseShape restrictionShape;
        private ShapeFileFeatureLayer queryingFeatureLayer;
        private ObservableCollection<ShapeFileFeatureLayer> poiLayers;

        public event EventHandler<PlottedPointChangedMapModelEventArgs> PlottedPointChanged;

        public MapModel()
            : this(null)
        { }

        public MapModel(WinformsMap winformsMap)
            : base()
        {
            mapControl = winformsMap;

            InitializeMapControl();
            InitializeOverlays();
            InitializeAdornments();

            mapControl.Refresh();
        }

        public WinformsMap MapControl
        {
            get { return mapControl; }
        }

        public PointShape PlottedPoint
        {
            get { return plottedPoint; }
            set { plottedPoint = value; }
        }

        public ShapeFileFeatureLayer QueryingFeatureLayer
        {
            get { return queryingFeatureLayer; }
            set { queryingFeatureLayer = value; }
        }

        public ShapeFileFeatureLayer HotelsLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (ShapeFileFeatureLayer)overlay.Layers[Resources.HotelsLayerKey];
            }
        }

        public ShapeFileFeatureLayer MedicalFacilitesLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (ShapeFileFeatureLayer)overlay.Layers[Resources.MedicalFacilitesLayerKey];
            }
        }

        public ShapeFileFeatureLayer PublicFacilitesLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (ShapeFileFeatureLayer)overlay.Layers[Resources.PublicFacilitesLayerKey];
            }
        }

        public ShapeFileFeatureLayer RestaurantsLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (ShapeFileFeatureLayer)overlay.Layers[Resources.RestaurantsLayerKey];
            }
        }

        public ShapeFileFeatureLayer RestrictionLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.CustomOverlayKey];
                return overlay.Layers[Resources.RestrictionLayerKey] as ShapeFileFeatureLayer;
            }
        }

        public ShapeFileFeatureLayer SchoolsLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (ShapeFileFeatureLayer)overlay.Layers[Resources.SchoolsLayerKey];
            }
        }

        public InMemoryFeatureLayer HighlightFeautreLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.PoiOverlayKey];
                return (InMemoryFeatureLayer)overlay.Layers[Resources.HighlightFeatureLayerKey];
            }
        }

        public InMemoryFeatureLayer ServiceAreaLayer
        {
            get
            {
                LayerOverlay overlay = (LayerOverlay)mapControl.Overlays[Resources.CustomOverlayKey];
                return (InMemoryFeatureLayer)overlay.Layers[Resources.ServiceAreaLayerKey];
            }
        }

        public TrackMode TrackMode
        {
            get { return mapControl.TrackOverlay.TrackMode; }
            set { mapControl.TrackOverlay.TrackMode = value; }
        }

        public void AddMarkersByFeatures(IEnumerable<Feature> features)
        {
            ClearPoiMarkers();

            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)mapControl.Overlays[Resources.MakerOverlayKey];
            HighlightFeautreLayer.InternalFeatures.Clear();
            HighlightFeautreLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = queryingFeatureLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle;
            if (plottedPoint != null)
            {
                Marker plottedMarker = CreateMarker(plottedPoint, Resources.drawPoint);
                markerOverlay.Markers.Add(plottedMarker);
            }
            if (features != null)
            {
                foreach (Feature feature in features)
                {
                    HighlightFeautreLayer.InternalFeatures.Add(feature);
                    Marker marker = CreateMarker((PointShape)feature.GetShape(), Resources.selectedHalo, .5f);
                    marker.ToolTipDelayInMilliseconds = 2000;
                    marker.ToolTipText = GetToolTipsText(feature);
                    markerOverlay.Markers.Add(marker);
                }
            }
        }

        public void ClearPoiMarkers()
        {
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)mapControl.Overlays[Resources.MakerOverlayKey];
            foreach (var item in markerOverlay.Markers)
            {
                item.Dispose();
            }

            markerOverlay.Markers.Clear();
        }

        public ShapeFileFeatureLayer GetPoiFeatureLayerByName(string layerName)
        {
            return poiLayers.FirstOrDefault(l => l.Name.Equals(layerName, StringComparison.OrdinalIgnoreCase));
        }

        public ObservableCollection<string> GetPoiLayerCandidateNames()
        {
            return new ObservableCollection<string>(GetPoiLayerCandidates().Select(l => l.Name));
        }

        public ObservableCollection<ShapeFileFeatureLayer> GetPoiLayerCandidates()
        {
            return poiLayers ?? (poiLayers = new ObservableCollection<ShapeFileFeatureLayer>
            {
                HotelsLayer,
                MedicalFacilitesLayer,
                RestaurantsLayer,
                SchoolsLayer
            });
        }

        public Collection<string> GetColumnValueCandidates()
        {
            string columnName = MapSuiteSampleHelper.GetDefaultColumnNameByPoiType(QueryingFeatureLayer.Name);

            Collection<string> columnValues = new Collection<string>();
            if (columnName.Equals("Hotels", StringComparison.OrdinalIgnoreCase))
            {
                columnValues.Add("1 ~ 50");
                columnValues.Add("50 ~ 100");
                columnValues.Add("100 ~ 150");
                columnValues.Add("150 ~ 200");
                columnValues.Add("200 ~ 300");
                columnValues.Add("300 ~ 400");
                columnValues.Add("400 ~ 500");
                columnValues.Add(">= 500");
            }
            else
            {
                QueryingFeatureLayer.Open();
                IEnumerable<string> distinctColumnValues = QueryingFeatureLayer.FeatureSource.GetDistinctColumnValues(columnName).Select(v => v.ColumnValue);
                foreach (var distinctColumnValue in distinctColumnValues)
                {
                    columnValues.Add(distinctColumnValue);
                }
            }

            return columnValues;
        }

        protected void OnPlottedPointChanged(PlottedPointChangedMapModelEventArgs e)
        {
            EventHandler<PlottedPointChangedMapModelEventArgs> handler = PlottedPointChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void InitializeMapControl()
        {
            mapControl.MapUnit = GeographyUnit.Meter;
            mapControl.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapControl.CurrentExtent = new RectangleShape(-10782584.5066971, 3916291.54400321, -10769504.2529497, 3907774.56943023);
            mapControl.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
        }

        private void InitializeAdornments()
        {
            mapControl.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            LegendAdornmentLayer legendlayer = new LegendAdornmentLayer();
            legendlayer.Height = 135;
            legendlayer.Location = AdornmentLocation.LowerRight;
            mapControl.AdornmentOverlay.Layers.Add(legendlayer);

            AddLegendItem(HotelsLayer, legendlayer);
            AddLegendItem(MedicalFacilitesLayer, legendlayer);
            AddLegendItem(PublicFacilitesLayer, legendlayer);
            AddLegendItem(RestaurantsLayer, legendlayer);
            AddLegendItem(SchoolsLayer, legendlayer);

            ScaleBarAdornmentLayer scaleBar = new ScaleBarAdornmentLayer();
            scaleBar.Location = AdornmentLocation.LowerLeft;
            mapControl.AdornmentOverlay.Layers.Add(Resources.ScaleBarLayerKey, scaleBar);
        }

        private void InitializeOverlays()
        {
            string cacheFolder = Path.Combine(Path.GetTempPath(), "TileCache");
            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitRoadOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitRoadOverlay.Name = Resources.ThinkGeoCloudMapsOverlayLight;
            worldMapKitRoadOverlay.IsVisible = true;
            worldMapKitRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayLight);
            worldMapKitRoadOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            mapControl.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayLight, worldMapKitRoadOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialOverlay.Name = Resources.ThinkGeoCloudMapsOverlayAerial;
            worldMapKitAerialOverlay.IsVisible = false;
            worldMapKitAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayAerial);
            worldMapKitAerialOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
            mapControl.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayAerial, worldMapKitAerialOverlay);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay worldMapKitAerialWithLabelsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            worldMapKitAerialWithLabelsOverlay.Name = Resources.ThinkGeoCloudMapsOverlayHybrid;
            worldMapKitAerialWithLabelsOverlay.IsVisible = false;
            worldMapKitAerialWithLabelsOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.ThinkGeoCloudMapsOverlayHybrid);
            worldMapKitAerialWithLabelsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
            mapControl.Overlays.Add(Resources.ThinkGeoCloudMapsOverlayHybrid, worldMapKitAerialWithLabelsOverlay);

            OpenStreetMapOverlay openStreetMapOverlay = new OpenStreetMapOverlay();
            openStreetMapOverlay.Name = Resources.OSMOverlayName;
            openStreetMapOverlay.IsVisible = false;
            openStreetMapOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.OSMOverlayName);
            mapControl.Overlays.Add(Resources.OSMOverlayName, openStreetMapOverlay);

            BingMapsOverlay bingMapsAerialOverlay = new BingMapsOverlay();
            bingMapsAerialOverlay.Name = Resources.BingMapsAerialOverlayName;
            bingMapsAerialOverlay.MapType = WinForms.BingMapsMapType.Aerial;
            bingMapsAerialOverlay.IsVisible = false;
            bingMapsAerialOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsAerialOverlayName);
            mapControl.Overlays.Add(Resources.BingMapsAerialOverlayName, bingMapsAerialOverlay);

            BingMapsOverlay bingMapsRoadOverlay = new BingMapsOverlay();
            bingMapsRoadOverlay.Name = Resources.BingMapsRoadOverlayName;
            bingMapsRoadOverlay.MapType = WinForms.BingMapsMapType.Road;
            bingMapsRoadOverlay.IsVisible = false;
            bingMapsRoadOverlay.TileCache = new FileBitmapTileCache(cacheFolder, Resources.BingMapsRoadOverlayName);
            mapControl.Overlays.Add(Resources.BingMapsRoadOverlayName, bingMapsRoadOverlay);

            LayerOverlay customOverlay = new LayerOverlay();
            mapControl.Overlays.Add(Resources.CustomOverlayKey, customOverlay);

            Proj4Projection wgs84ToMercatorProjection = new Proj4Projection();
            wgs84ToMercatorProjection.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
            wgs84ToMercatorProjection.ExternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();
            wgs84ToMercatorProjection.Open();

            ShapeFileFeatureLayer restrictionLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["RestrictedShapeFilePathName"]);
            restrictionLayer.FeatureSource.Projection = wgs84ToMercatorProjection;

            AreaStyle extentStyle = new AreaStyle();
            extentStyle.CustomAreaStyles.Add(new AreaStyle(new GeoSolidBrush(GeoColor.SimpleColors.Transparent)) { OutlinePen = new GeoPen(GeoColor.SimpleColors.White, 5.5f) });
            extentStyle.CustomAreaStyles.Add(new AreaStyle(new GeoSolidBrush(GeoColor.SimpleColors.Transparent)) { OutlinePen = new GeoPen(GeoColor.SimpleColors.Red, 1.5f) { DashStyle = LineDashStyle.Dash } });
            restrictionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = extentStyle;
            restrictionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            customOverlay.Layers.Add(Resources.RestrictionLayerKey, restrictionLayer);

            InMemoryFeatureLayer serviceAreaLayer = new InMemoryFeatureLayer();
            serviceAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(new GeoColor(120, GeoColor.FromHtml("#1749c9")), GeoColor.FromHtml("#fefec1"), 3, LineDashStyle.Solid);
            serviceAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            customOverlay.Layers.Add(Resources.ServiceAreaLayerKey, serviceAreaLayer);

            LayerOverlay poiOverlay = new LayerOverlay();
            mapControl.Overlays.Add(Resources.PoiOverlayKey, poiOverlay);

            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["HotelsShapeFilePathName"]);
            hotelsLayer.Name = Resources.HotelsLayerKey;
            hotelsLayer.Transparency = 120f;
            hotelsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage("../../Image/Hotel.png"));
            hotelsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            hotelsLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            poiOverlay.Layers.Add(Resources.HotelsLayerKey, hotelsLayer);

            ShapeFileFeatureLayer medicalFacilitesLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["MedicalFacilitiesShapeFilePathName"]);
            medicalFacilitesLayer.Name = Resources.MedicalFacilitesLayerKey;
            medicalFacilitesLayer.Transparency = 120f;
            medicalFacilitesLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage("../../Image/DrugStore.png"));
            medicalFacilitesLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            medicalFacilitesLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            poiOverlay.Layers.Add(Resources.MedicalFacilitesLayerKey, medicalFacilitesLayer);

            ShapeFileFeatureLayer publicFacilitesLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["PublicFacilitiesShapeFilePathName"]);
            publicFacilitesLayer.Name = Resources.PublicFacilitesLayerKey;
            publicFacilitesLayer.Transparency = 120f;
            publicFacilitesLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage("../../Image/public_facility.png"));
            publicFacilitesLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            publicFacilitesLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            poiOverlay.Layers.Add(Resources.PublicFacilitesLayerKey, publicFacilitesLayer);

            ShapeFileFeatureLayer restaurantsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["RestaurantsShapeFilePathName"]);
            restaurantsLayer.Name = Resources.RestaurantsLayerKey;
            restaurantsLayer.Transparency = 120f;
            restaurantsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage("../../Image/restaurant.png"));
            restaurantsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            restaurantsLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            poiOverlay.Layers.Add(Resources.RestaurantsLayerKey, restaurantsLayer);

            ShapeFileFeatureLayer schoolsLayer = new ShapeFileFeatureLayer(ConfigurationManager.AppSettings["SchoolsShapeFilePathName"]);
            schoolsLayer.Name = Resources.SchoolsLayerKey;
            schoolsLayer.Transparency = 120f;
            schoolsLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoImage("../../Image/school.png"));
            schoolsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            schoolsLayer.FeatureSource.Projection = wgs84ToMercatorProjection;
            poiOverlay.Layers.Add(Resources.SchoolsLayerKey, schoolsLayer);

            InMemoryFeatureLayer highlightFeatureLayer = new InMemoryFeatureLayer();
            highlightFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            poiOverlay.Layers.Add(Resources.HighlightFeatureLayerKey, highlightFeatureLayer);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.MapControl = mapControl;
            mapControl.Overlays.Add(Resources.MakerOverlayKey, markerOverlay);
        }

        private BaseShape GetRestrictionShape()
        {
            if (restrictionShape == null)
            {
                LayerOverlay customOverlay = (LayerOverlay)mapControl.Overlays[Resources.CustomOverlayKey];
                ShapeFileFeatureLayer restictionLayer = (ShapeFileFeatureLayer)customOverlay.Layers[Resources.RestrictionLayerKey];

                restictionLayer.Open();
                Feature firstFeature = restictionLayer.QueryTools.GetFeatureById("1", ReturningColumnsType.NoColumns);
                restrictionShape = firstFeature.GetShape();
            }

            return restrictionShape;
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            if (mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Count > 0)
            {
                BaseShape tempRestrictionShape = GetRestrictionShape();
                PointShape pointShape = mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures[0].GetShape() as PointShape;
                if (QueryingFeatureLayer != null && pointShape != null && tempRestrictionShape != null && pointShape.IsWithin(tempRestrictionShape))
                {
                    plottedPoint = pointShape;
                    OnPlottedPointChanged(new PlottedPointChangedMapModelEventArgs(plottedPoint));
                }
                else
                {
                    MessageBox.Show(Resources.SearchOutOfBoundaryMessage, Resources.WarningText, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                mapControl.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            }
        }

        private static void AddLegendItem(FeatureLayer featureLayer, LegendAdornmentLayer legendLayer)
        {
            LegendItem layerItem = new LegendItem();
            layerItem.ImageStyle = featureLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle;
            layerItem.TextStyle = WorldStreetsTextStyles.GeneralPurpose(featureLayer.Name, 10);
            legendLayer.LegendItems.Add(featureLayer.Name, layerItem);
        }

        private static Marker CreateMarker(PointShape position, Bitmap imageSource, float offsetYRatio = 1)
        {
            Marker marker = new Marker(position);
            marker.Image = imageSource;
            marker.Size = marker.Image.Size;

            marker.YOffset = -marker.Image.Height * offsetYRatio;
            marker.XOffset = -marker.Image.Width * .5f;
            return marker;
        }

        private static string GetToolTipsText(Feature feature)
        {
            string name = string.Empty;
            string address = string.Empty;
            StringBuilder information = new StringBuilder();

            foreach (var item in feature.ColumnValues)
            {
                if (item.Key.Equals("NAME", StringComparison.InvariantCultureIgnoreCase))
                {
                    name = item.Value;
                }
                else if (item.Key.Equals("ADDRESS", StringComparison.InvariantCultureIgnoreCase))
                {
                    address = item.Value;
                }
                else
                {
                    information.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} : {1}", item.Key, item.Value));
                }
            }

            return string.Format(CultureInfo.InvariantCulture, "Name : {0}\n{1}\n{2}", name, address, information);
        }
    }
}