using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ConvertAFeatureToAndFromWkb : UserControl
    {
        public ConvertAFeatureToAndFromWkb()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-12933325, 14195325, 16027137, -8510143);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }

        private void convertWkbToFeature_Click(object sender, RoutedEventArgs e)
        {
            byte[] wellKnownBinary = Convert.FromBase64String(wkbTextBox.Text);
            Feature feature = new Feature(wellKnownBinary);

            wktResultTextBox.Text = feature.GetWellKnownText();
        }

        private void convertFeatureToWkb_Click(object sender, RoutedEventArgs e)
        {
            Feature feature = new Feature(wktTextBox.Text);
            byte[] wellKnownBinary = feature.GetWellKnownBinary();

            wkbResultTextBox.Text = Convert.ToBase64String(wellKnownBinary);
        }
    }
}