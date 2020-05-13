using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class FindFeaturesWithinDistance : UserControl
    {
        private PointShape pointShape;
        private ProjectionConverter project;
        public FindFeaturesWithinDistance()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            project = new ProjectionConverter(3857, 4326);
            project.Open();

            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-15963215, 20037508, 12990985, -13516534);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundPointStyle(PointSymbolType.Square, GeoColors.White, GeoColors.Black, 1F, 8F, PointSymbolType.Square, GeoColors.Navy, GeoColors.Transparent, 0F, 4F);
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(150, 154, 205, 50));
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.TileType = TileType.SingleTile;
            dynamicOverlay.Layers.Add("HighlightLayer", highlightLayer);
            mapView.Overlays.Add("HighlightOverlay", dynamicOverlay);

            mapView.MapClick += new EventHandler<MapClickMapViewEventArgs>(mapView_MapClick);
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);
            mapView.Refresh();
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            pointShape = e.WorldLocation;
            FindWithinDistanceFeatures();
        }

        private void cmbDistance_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pointShape != null)
            {
                FindWithinDistanceFeatures();
            }
        }

        private void FindWithinDistanceFeatures()
        {
            if (mapView.Overlays.Count > 0)
            {
                FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
                InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("HighlightLayer");

                // Find the countries within special distance.
                double distance = Convert.ToDouble(cmbDistance.SelectedItem.ToString().Split(':')[1], CultureInfo.InvariantCulture);
                worldLayer.Open();
                worldLayer.FeatureSource.ProjectionConverter = project;
                Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesWithinDistanceOf((PointShape)project.ConvertToExternalProjection(pointShape), GeographyUnit.DecimalDegree, DistanceUnit.Kilometer, distance, new string[0]);
                worldLayer.FeatureSource.ProjectionConverter = null;
                worldLayer.Close();

                if (highlightLayer.InternalFeatures.Count > 0)
                {
                    highlightLayer.InternalFeatures.Clear();
                }

                highlightLayer.InternalFeatures.Add("Point", new Feature(pointShape));
                foreach (Feature feature in selectedFeatures)
                {
                    highlightLayer.InternalFeatures.Add(feature.Id, project.ConvertToInternalProjection(feature));
                }

                mapView.Overlays["HighlightOverlay"].Refresh();
            }
        }
    }
}