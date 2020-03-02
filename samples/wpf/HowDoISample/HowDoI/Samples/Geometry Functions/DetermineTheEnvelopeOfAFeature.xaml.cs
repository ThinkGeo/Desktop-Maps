using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DetermineTheEnvelopeOfAFeature : UserControl
    {
        public DetermineTheEnvelopeOfAFeature()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14774792, 5894765, -7534677, 33744);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            // Setup the BoundingBox InMemoryFeatureLayer.
            InMemoryFeatureLayer boundingBoxLayer = new InMemoryFeatureLayer();
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Green;
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay boundingboxOverlay = new LayerOverlay();
            boundingboxOverlay.TileType = TileType.SingleTile;
            boundingboxOverlay.Layers.Add("BoundingBoxLayer", boundingBoxLayer);
            mapView.Overlays.Add("BoundingBoxOverlay", boundingboxOverlay);

            mapView.Refresh();
        }

        private void btnGetEnvelope_Click(object sender, RoutedEventArgs e)
        {
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.Open();
            RectangleShape usBoundingBox = worldLayer.QueryTools.GetFeatureById("137", new string[0]).GetBoundingBox();
            worldLayer.Close();

            InMemoryFeatureLayer boundingBoxLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("BoundingBoxLayer");
            if (!boundingBoxLayer.InternalFeatures.Contains("BoundingBox"))
            {
                boundingBoxLayer.InternalFeatures.Add("BoundingBox", new Feature(usBoundingBox.GetWellKnownBinary(), "BoundingBox"));
            }

            mapView.Overlays["BoundingBoxOverlay"].Refresh();
        }
    }
}