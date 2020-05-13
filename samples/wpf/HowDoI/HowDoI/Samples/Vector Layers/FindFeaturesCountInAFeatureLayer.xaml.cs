using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class FindFeaturesCountInAFeatureLayer : UserControl
    {
        public FindFeaturesCountInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitDesktopOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }

        private void btnGetCount_Click(object sender, RoutedEventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            txtCount.Content = worldLayer.QueryTools.GetCount().ToString(CultureInfo.InvariantCulture);
            worldLayer.Close();
        }
    }
}