using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class SpatialQueryAFeatureLayer : UserControl
    {
        public SpatialQueryAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer rectangleLayer = new InMemoryFeatureLayer();
            rectangleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(50, 100, 100, 200)));
            rectangleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DarkBlue;
            rectangleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            rectangleLayer.InternalFeatures.Add("Rectangle", new Feature("POLYGON((-5565975 -2273031,-5565975 2273031,5565975 2273031,5565975 -2273031,-5565975 -2273031))", "Rectangle"));

            InMemoryFeatureLayer spatialQueryResultLayer = new InMemoryFeatureLayer();
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(200, GeoColors.PastelRed)));
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Red;
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay spatialQueryResultOverlay = new LayerOverlay();
            spatialQueryResultOverlay.TileType = TileType.SingleTile;
            spatialQueryResultOverlay.Layers.Add("RectangleLayer", rectangleLayer);
            spatialQueryResultOverlay.Layers.Add("SpatialQueryResultLayer", spatialQueryResultLayer);
            mapView.Overlays.Add("SpatialQueryResultOverlay", spatialQueryResultOverlay);

            mapView.Refresh();
        }

        private void cboSpatialQueryType_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("WorldLayer");
                InMemoryFeatureLayer rectangleLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("RectangleLayer");
                InMemoryFeatureLayer spatialQueryResultLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("SpatialQueryResultLayer");

                Feature rectangleFeature = rectangleLayer.InternalFeatures["Rectangle"];
                Collection<Feature> spatialQueryResults;
                worldLayer.Open();
                switch (cboSpatialQueryType.SelectedItem.ToString().Split(':')[1].Trim())
                {
                    case "Within":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesWithin(rectangleFeature, new string[0]);
                        break;

                    case "Containing":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesContaining(rectangleFeature, new string[0]);
                        break;

                    case "Disjointed":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesDisjointed(rectangleFeature, new string[0]);
                        break;

                    case "Intersecting":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesIntersecting(rectangleFeature, new string[0]);
                        break;

                    case "Overlapping":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesOverlapping(rectangleFeature, new string[0]);
                        break;

                    case "TopologicalEqual":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesTopologicalEqual(rectangleFeature, new string[0]);
                        break;

                    case "Touching":
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesTouching(rectangleFeature, new string[0]);
                        break;

                    default:
                        spatialQueryResults = worldLayer.QueryTools.GetFeaturesWithin(rectangleFeature, new string[0]);
                        break;
                }
                worldLayer.Close();

                spatialQueryResultLayer.InternalFeatures.Clear();
                foreach (Feature feature in spatialQueryResults)
                {
                    spatialQueryResultLayer.InternalFeatures.Add(feature.Id, feature);
                }

                mapView.Overlays["SpatialQueryResultOverlay"].Refresh();
            }
        }
    }
}