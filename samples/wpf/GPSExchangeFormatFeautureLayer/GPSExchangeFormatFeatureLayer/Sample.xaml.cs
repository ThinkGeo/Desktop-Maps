/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.IO;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace GPSExchangeFormatFeatureLayer
{
    public partial class Sample : Window
    {
        private Proj4Projection proj4Projection;

        public Sample()
        {
            InitializeComponent();

            proj4Projection = new Proj4Projection(4326, 3857);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map1.MapTools.Logo.IsEnabled = true;
            Map1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            Map1.Overlays.Add(baseOverlay);

            LayerOverlay gpsOverlay = new LayerOverlay();
            Map1.Overlays.Add("GPSOverlay", gpsOverlay);


            string[] dwgFiles = Directory.GetFiles(@"..\..\Data\");
            foreach (string dwgFile in dwgFiles)
            {
                sampleFileListBox.Items.Add(Path.GetFileName(dwgFile));
            }
            // Display the first file in the list
            sampleFileListBox.SelectedIndex = 0;
        }

        private void sampleFileListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            GpxFeatureLayer shapeLayer = new GpxFeatureLayer(@"..\..\Data\" + sampleFileListBox.SelectedItem.ToString());
            shapeLayer.FeatureSource.Projection = proj4Projection;

            ValueStyle pointStyle = new ValueStyle();
            pointStyle.ColumnName = "IsWayPoint";
            pointStyle.ValueItems.Add(new ValueItem("0", PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Red, 4)));
            pointStyle.ValueItems.Add(new ValueItem("1", PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Green, 8)));
            LineStyle roadstyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Black, 1, true);

            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(roadstyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            GpxFeatureLayer textLayer = new GpxFeatureLayer(@"..\..\Data\" + sampleFileListBox.SelectedItem.ToString());
            textLayer.FeatureSource.Projection = proj4Projection;
            TextStyle labelStyle = TextStyles.CreateSimpleTextStyle("name", "Arial", 8, DrawingFontStyles.Bold, GeoColor.SimpleColors.Black);
            labelStyle.PointPlacement = PointPlacement.UpperCenter;
            labelStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            labelStyle.YOffsetInPixel = 8;

            textLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(labelStyle);
            textLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            shapeLayer.Open();
            Map1.CurrentExtent = (shapeLayer.GetBoundingBox());

            var gpsOverlay = (LayerOverlay)Map1.Overlays["GPSOverlay"];
            gpsOverlay.Layers.Clear();
            gpsOverlay.Layers.Add(shapeLayer);
            gpsOverlay.Layers.Add(textLayer);

            Map1.Refresh();
        }
    }
}

