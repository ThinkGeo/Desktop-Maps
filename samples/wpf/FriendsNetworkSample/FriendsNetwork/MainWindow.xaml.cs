/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace FriendsNetwork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            // Create a single-tile overlay to hold the lake image.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            wpfMap1.Overlays.Add("layerOverlay", layerOverlay);

            // Create the size of point circle.
            int symbolSize = 50;

            // Create the point style for friends
            PointStyle friendsPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(new GeoColor(80, GeoColors.Highlight)), new GeoPen(GeoColors.Blue, 1), symbolSize - 15);
            // Create the point style for protagonist
            PointStyle protagonistPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(new GeoColor(80, GeoColors.DarkOrange)), new GeoPen(GeoColors.DarkOrange, 1), symbolSize);

            // Create the ValueStyle for PointStyle.
            ValueStyle pointValueStyle = new ValueStyle();
            pointValueStyle.ColumnName = "role";
            pointValueStyle.ValueItems.Add(new ValueItem("friend", friendsPointStyle));
            pointValueStyle.ValueItems.Add(new ValueItem("protagonist", protagonistPointStyle));

            // Create the text style for friendsPointStyle
            TextStyle friendsTexStyle = new TextStyle("Name", new GeoFont("Arail", 9, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.SimpleColors.Black));
            friendsTexStyle.PointPlacement = PointPlacement.Center;
            friendsTexStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;

            // Create the text style for protagonist
            TextStyle protagonistTextStyle = new TextStyle("Name", new GeoFont("Arail", 9, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.SimpleColors.Black));
            protagonistTextStyle.PointPlacement = PointPlacement.Center;
            protagonistTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;

            // Create the ValueStyle for TextStyle.
            ValueStyle textValueStyle = new ValueStyle();
            textValueStyle.ColumnName = "role";
            textValueStyle.ValueItems.Add(new ValueItem("friend", friendsTexStyle));
            textValueStyle.ValueItems.Add(new ValueItem("protagonist", protagonistTextStyle));

            // Create the layer
            ShapeFileFeatureLayer friendsLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\friends.shp");
            friendsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointValueStyle);
            friendsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textValueStyle);
            friendsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            friendsLayer.Open();
            var allFeature = friendsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            var protagonistPionts = allFeature.Where(f => f.ColumnValues["role"] == "protagonist").ToList();
            var friendPionts = allFeature.Where(f => f.ColumnValues["role"] == "friend").ToList();
            if (protagonistPionts.Count > 0)
            {
                Feature targetFeature = protagonistPionts[0];
                InMemoryFeatureLayer lineLayer = GetTheLineAndRangeLayer(targetFeature, friendPionts);
                layerOverlay.Layers.Add(lineLayer);
            }

            layerOverlay.Layers.Add(friendsLayer);

            // Set the map extent
            wpfMap1.CurrentExtent = new RectangleShape(-10773348.4056994, 3921495.52062368, -10763946.6747371, 3914224.46649529);
            wpfMap1.Refresh();
        }

        private static InMemoryFeatureLayer GetTheLineAndRangeLayer(Feature targetFeature, List<Feature> friendPionts)
        {
            // Get the protagonist as target point
            var targetShape = targetFeature.GetShape() as PointShape;
            Vertex targetVertex = new Vertex(targetShape.X, targetShape.Y);

            // the farthest distance between protagonist and the friends.
            double farthestDistanceToTarget = 0;

            // Create the InMemoryFeatureLayer which shows the line and range.
            InMemoryFeatureLayer lineLayer = new InMemoryFeatureLayer();

            foreach (Feature feature in friendPionts)
            {
                var pointShape = feature.GetShape() as PointShape;
                // Get the distance between current friend and protagonist.
                var distanceToTarget = pointShape.GetDistanceTo(targetShape, GeographyUnit.Meter, DistanceUnit.Meter);
                // Update the farthest distance.
                farthestDistanceToTarget = distanceToTarget > farthestDistanceToTarget ? distanceToTarget : farthestDistanceToTarget;

                List<Vertex> vertexs = new List<Vertex>();
                vertexs.Add(new Vertex(pointShape.X, pointShape.Y));
                vertexs.Add(targetVertex);
                // Get the line shape between current friend and protagonist.
                LineShape lineShape = new LineShape(vertexs);
                lineLayer.InternalFeatures.Add(new Feature(lineShape));
            }

            // Create the circle of friends.
            EllipseShape ellipseShape = new EllipseShape(targetShape, farthestDistanceToTarget, GeographyUnit.Meter, DistanceUnit.Meter);
            lineLayer.InternalFeatures.Add(new Feature(ellipseShape));
            lineLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(60, GeoColors.Highlight)));
            lineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = WorldStreetsLineStyles.PowerLine(3);
            lineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return lineLayer;
        }
    }
}
