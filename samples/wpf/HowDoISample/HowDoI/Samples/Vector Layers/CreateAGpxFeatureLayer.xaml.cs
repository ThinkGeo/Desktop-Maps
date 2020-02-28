using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CreateAGpxFeatureLayer.xaml
    /// </summary>
    public partial class CreateAGpxFeatureLayer : UserControl
    {
        private ProjectionConverter projectionConverter;

        public CreateAGpxFeatureLayer()
        {
            InitializeComponent();

            projectionConverter = new ProjectionConverter(4326, 3857);
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.MapTools.Logo.IsEnabled = true;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            var baseOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(baseOverlay);

            var gpsOverlay = new LayerOverlay();
            mapView.Overlays.Add("GPSOverlay", gpsOverlay);


            var dwgFiles = Directory.GetFiles(Path.Combine(SampleHelper.Root, "Gpx"));
            foreach (var dwgFile in dwgFiles)
            {
                sampleFileListBox.Items.Add(Path.GetFileName(dwgFile));
            }
            // Display the first file in the list
            sampleFileListBox.SelectedIndex = 0;
        }

        private void sampleFileListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GpxFeatureLayer shapeLayer = new GpxFeatureLayer(SampleHelper.Get(sampleFileListBox.SelectedItem.ToString()));
            shapeLayer.FeatureSource.ProjectionConverter = projectionConverter;

            ValueStyle pointStyle = new ValueStyle();
            pointStyle.ColumnName = "IsWayPoint";
            pointStyle.ValueItems.Add(new ValueItem("0", PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Red, 4)));
            pointStyle.ValueItems.Add(new ValueItem("1", PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Green, 8)));
            LineStyle roadstyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 1, true);

            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(roadstyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            GpxFeatureLayer textLayer = new GpxFeatureLayer(SampleHelper.Get(sampleFileListBox.SelectedItem.ToString()));
            textLayer.FeatureSource.ProjectionConverter = projectionConverter;
            TextStyle labelStyle = TextStyle.CreateSimpleTextStyle("name", "Arial", 8, DrawingFontStyles.Bold, GeoColors.Black);
            labelStyle.TextPlacement = TextPlacement.Upper;
            labelStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            labelStyle.YOffsetInPixel = 8;

            textLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(labelStyle);
            textLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            shapeLayer.Open();
            mapView.CurrentExtent = (shapeLayer.GetBoundingBox());

            var gpsOverlay = (LayerOverlay)mapView.Overlays["GPSOverlay"];
            gpsOverlay.Layers.Clear();
            gpsOverlay.Layers.Add(shapeLayer);
            gpsOverlay.Layers.Add(textLayer);

            mapView.Refresh();
        }
    }
}
