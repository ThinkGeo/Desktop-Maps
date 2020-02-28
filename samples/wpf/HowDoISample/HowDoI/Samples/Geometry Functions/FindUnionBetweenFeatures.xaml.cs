using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class FindUnionBetweenFeatures : UserControl
    {
        public FindUnionBetweenFeatures()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-18473122, 20037508, -3992891, -711306);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            worldLayer.Open();
            Collection<Feature> allFeatures = worldLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            worldLayer.Close();

            // Setup the inMemoryLayer.
            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green)));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Green;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            foreach (Feature feature in allFeatures)
            {
                inMemoryLayer.InternalFeatures.Add(feature);
            }

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.TileType = TileType.SingleTile;
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            mapView.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            mapView.Refresh();
        }

        private void btnUnion_Click(object sender, RoutedEventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("InMemoryFeatureLayer");

            if (inMemoryLayer.InternalFeatures.Count > 1)
            {
                GeoCollection<AreaBaseShape> areaBaseShapes = new GeoCollection<AreaBaseShape>();

                GeoCollection<Feature> features = inMemoryLayer.InternalFeatures;
                foreach (Feature feature in features)
                {
                    AreaBaseShape targetAreaBaseShape = feature.GetShape() as AreaBaseShape;
                    if (targetAreaBaseShape != null)
                    {
                        areaBaseShapes.Add(targetAreaBaseShape);
                    }
                }

                AreaBaseShape unionedAreaBaseShape = AreaBaseShape.Union(areaBaseShapes);
                unionedAreaBaseShape.Id = "UnionedFeature";
                inMemoryLayer.InternalFeatures.Clear();
                inMemoryLayer.InternalFeatures.Add("UnionedFeature", new Feature(unionedAreaBaseShape));
                inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));

                mapView.Overlays["InMemoryOverlay"].Refresh();
            }
        }
    }
}