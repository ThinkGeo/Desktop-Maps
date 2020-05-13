using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace AjacentRoadSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShapeFileFeatureLayer featureLayer;
        private InMemoryFeatureLayer routeResultLayer;

        private Dictionary<string, PointShape> Points = new Dictionary<string, PointShape>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WpfMap_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.DecimalDegree;
            Map1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 172, 205, 226));

            LayerOverlay layerOverlay = new LayerOverlay();
            Map1.Overlays.Add("worldmapkit", layerOverlay);

            featureLayer = new ShapeFileFeatureLayer(@"..\..\data\Road_Network.shp");
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = WorldStreetsLineStyles.MinorRoadOutline(5f);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(featureLayer);

            ShapeFileFeatureLayer pointsLayer = new ShapeFileFeatureLayer(@"..\..\data\Points.shp");
            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateCompoundPointStyle(PointSymbolType.Square, GeoColor.StandardColors.White, GeoColor.StandardColors.Black, 1F, 10F, PointSymbolType.Square, GeoColor.StandardColors.Navy, GeoColor.StandardColors.Transparent, 0F, 6F);
            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("ID", "Arial", 13, DrawingFontStyles.Bold, GeoColor.StandardColors.Maroon, 5, -10);
            pointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(pointsLayer);

            routeResultLayer = new InMemoryFeatureLayer();
            routeResultLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.SimpleColors.Blue, 3, true);
            routeResultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(routeResultLayer);

            pointsLayer.Open();
            Collection<Feature> features = pointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            foreach (Feature feature in features)
            {
                Points.Add(feature.ColumnValues["ID"], feature.GetShape() as PointShape);
            }
            pointsLayer.Close();

            featureLayer.Open();
            Map1.CurrentExtent = featureLayer.GetBoundingBox();
            featureLayer.Close();

            Map1.Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PointShape start = Points[((ComboBoxItem)cmbStart.SelectedItem).Content.ToString()];
            PointShape end = Points[((ComboBoxItem)cmbEnd.SelectedItem).Content.ToString()];

            LineShape startSegment = GetNearestSegment(start);
            LineShape endSegment = GetNearestSegment(end);

            LineShape route = new LineShape();
            if (startSegment.Id == endSegment.Id)
            {
                route = startSegment.GetLineOnALine(start, end) as LineShape;
            }
            else
            {
                double tolerance = double.Parse(txtTolerance.Text, CultureInfo.InvariantCulture);
                PolygonShape bufferedPolygon = endSegment.Buffer(tolerance, GeographyUnit.DecimalDegree, DistanceUnit.Meter).Polygons[0];
                MultilineShape intersectedLines = startSegment.GetIntersection(bufferedPolygon) as MultilineShape;
                if (intersectedLines.Lines.Count > 0)     // Just check the intersected within allowed tolerance.
                {
                    PointShape intersectedPoint = intersectedLines.Lines[0].GetCenterPoint();

                    PointShape nearestPointOnStart = intersectedPoint.GetClosestPointTo(startSegment, GeographyUnit.DecimalDegree);
                    PointShape nearestPointOnEnd = intersectedPoint.GetClosestPointTo(endSegment, GeographyUnit.DecimalDegree);

                    LineShape routeSegmentOnStart = startSegment.GetLineOnALine(start, nearestPointOnStart) as LineShape;
                    LineShape routeSegmentOnEnd = endSegment.GetLineOnALine(end, nearestPointOnEnd) as LineShape;

                    IEnumerable<Vertex> vertexs = route.Vertices;
                    int index = IndexOf(routeSegmentOnStart.Vertices, new Vertex(nearestPointOnStart));
                    if (index == 0)
                    {
                        vertexs = vertexs.Concat(routeSegmentOnStart.Vertices.Reverse());
                    }
                    else
                    {
                        vertexs = vertexs.Concat(routeSegmentOnStart.Vertices);
                    }

                    index = IndexOf(routeSegmentOnEnd.Vertices, new ThinkGeo.MapSuite.Shapes.Vertex(nearestPointOnEnd));
                    if (index == 0)
                    {
                        vertexs = vertexs.Concat(routeSegmentOnEnd.Vertices);
                    }
                    else
                    {
                        vertexs = vertexs.Concat(routeSegmentOnEnd.Vertices.Reverse());
                    }

                    route = new LineShape(vertexs);
                }
            }

            routeResultLayer.InternalFeatures.Clear();
            if (route.Vertices.Count > 1)
            {
                routeResultLayer.InternalFeatures.Add(new Feature(route));
            }
            else
            {
                MessageBox.Show("No Route Found.");
            }

            Map1.Refresh();
        }

        private int IndexOf(IEnumerable<Vertex> verteces, Vertex vertex)
        {
            int index = -1;
            for (int i = 0; i < verteces.Count(); i++)
            {
                if (IsSameVertex(verteces.ToArray()[i], vertex))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private bool IsSameVertex(Vertex vertex1, Vertex vertex2)
        {
            double toleranceInUnit = 1e-3;
            bool isEqual = false;
            if (Math.Abs(vertex1.X - vertex2.X) < toleranceInUnit && Math.Abs(vertex1.Y - vertex2.Y) < toleranceInUnit)
            {
                isEqual = true;
            }
            return isEqual;
        }

        private LineShape GetNearestSegment(PointShape point)
        {
            LineShape nearestSegment = null;

            featureLayer.Open();
            var features = featureLayer.FeatureSource.GetFeaturesNearestTo(point, GeographyUnit.DecimalDegree, 1, ReturningColumnsType.AllColumns);
            if (features.Count > 0)
            {
                nearestSegment = (features[0].GetShape() as MultilineShape).Lines[0];
                nearestSegment.Id = features[0].Id;
            }

            return nearestSegment;
        }
    }
}
