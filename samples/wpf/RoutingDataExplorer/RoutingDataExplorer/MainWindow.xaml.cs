/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace RoutingDataExplorer
{
    public partial class MainWindow : Window
    {
        Proj4Projection projection;
        RoutingEngine routingEngine;
        SimpleMarkerOverlay simpleMarkerOverlay;
        InMemoryFeatureLayer routingLayer;
        InMemoryFeatureLayer boundingBoxLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadMap(object sender, RoutedEventArgs e)
        {
            Initialize(sender, e);
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            routingEngine = new RoutingEngine();
            routingEngine.SearchRadiusInMeters = 500;

            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            map.Overlays.Clear();
            map.MouseMove -= Map_MouseMove;
            map.MouseMove += Map_MouseMove;

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(baseOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            boundingBoxLayer = new InMemoryFeatureLayer();
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoSolidBrush(GeoColor.SimpleColors.Red), 3));
            boundingBoxLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(boundingBoxLayer);

            routingLayer = new InMemoryFeatureLayer();
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(new GeoSolidBrush(GeoColor.SimpleColors.Red), 2));
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(routingLayer);
            map.Overlays.Add(layerOverlay);

            simpleMarkerOverlay = new SimpleMarkerOverlay();
            map.Overlays.Add(simpleMarkerOverlay);

            map.CurrentExtent = new RectangleShape(-13583621.7146857, 7249548.96186797, -8652516.14595243, 2827363.72967118);
            map.Refresh();
        }

        private void Map_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point currentPoint = e.GetPosition(map);
            PointShape worldPoint = ExtentHelper.ToWorldCoordinate(map.CurrentExtent, new ScreenPointF((float)currentPoint.X, (float)currentPoint.Y), (float)map.ActualWidth, (float)map.ActualHeight);

            coordinatesTextBlock.Text = string.Format("X: {0}, Y: {1}", worldPoint.X, worldPoint.Y);
        }

        private void LoadRoutingFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "RoutingFile(*.rtg)|*rtg";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var shapeFilePath = Path.ChangeExtension(openFileDialog.FileName, ".shp");
                if (!File.Exists(shapeFilePath))
                {
                    System.Windows.MessageBox.Show($"Could not find file {shapeFilePath} in the same folder where you seleted.");
                    return;
                }

                routingFileNameTextBlock.Text = openFileDialog.FileName;
                var routingSource = new RtgRoutingSource(openFileDialog.FileName);
                routingEngine.RoutingSource = routingSource;

                var featureSource = new ShapeFileFeatureSource(shapeFilePath);
                routingEngine.FeatureSource = featureSource;

                featureSource.Open();
                var boundingBox = featureSource.GetBoundingBox();
                if (boundingBox.UpperLeftPoint.X < -180 || boundingBox.UpperLeftPoint.Y > 90 || boundingBox.LowerRightPoint.X > 180 || boundingBox.LowerRightPoint.Y < -90)
                {
                    routingEngine.GeographyUnit = GeographyUnit.Meter;
                    projection = null;
                }
                else
                {
                    routingEngine.GeographyUnit = GeographyUnit.DecimalDegree;
                    projection = new Proj4Projection(4326, 3857);
                    projection.Open();
                }

                boundingBox = projection == null ? boundingBox : projection.ConvertToExternalProjection(boundingBox);
                boundingBoxLayer.InternalFeatures.Clear();
                boundingBoxLayer.InternalFeatures.Add(new Feature(boundingBox));

                map.CurrentExtent = boundingBox;
                map.Refresh();
            }
        }

        private void MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (string.IsNullOrEmpty(routingFileNameTextBlock.Text))
                return;

            Marker startMarker = null;
            if (simpleMarkerOverlay.Markers.Contains("StartMarker"))
                startMarker = simpleMarkerOverlay.Markers["StartMarker"];

            Marker endMarker = null;
            if (simpleMarkerOverlay.Markers.Contains("EndMarker"))
                endMarker = simpleMarkerOverlay.Markers["EndMarker"];

            if (e.MouseButton == MapMouseButton.Left)
            {
                if (startMarker == null)
                {
                    startMarker = new Marker();
                    startMarker.ImageSource = new BitmapImage(new Uri("/../../Resource/StartMarker.png", UriKind.RelativeOrAbsolute));
                    startMarker.YOffset = -16;
                    simpleMarkerOverlay.Markers.Add("StartMarker", startMarker);
                }

                startMarker.Position = new Point(e.WorldX, e.WorldY);
                simpleMarkerOverlay.Refresh();
            }
            else if (e.MouseButton == MapMouseButton.Right)
            {
                if (endMarker == null)
                {
                    endMarker = new Marker();
                    endMarker.YOffset = -16;
                    endMarker.ImageSource = new BitmapImage(new Uri("/../../Resource/EndMarker.png", UriKind.RelativeOrAbsolute));
                    simpleMarkerOverlay.Markers.Add("EndMarker", endMarker);
                }

                endMarker.Position = new Point(e.WorldX, e.WorldY);
                simpleMarkerOverlay.Refresh();
            }

            if (startMarker != null && endMarker != null)
            {
                routingLayer.InternalFeatures.Clear();

                var startMarkerPosition = startMarker.Position;
                var startPoint = new PointShape(startMarkerPosition.X, startMarkerPosition.Y);
                startPoint = projection == null ? startPoint : (PointShape)projection.ConvertToInternalProjection(startPoint);
                var endMarkerPosition = endMarker.Position;
                var endPoint = new PointShape(endMarkerPosition.X, endMarkerPosition.Y);
                endPoint = projection == null ? endPoint : (PointShape)projection.ConvertToInternalProjection(endPoint);

                RoutingResult routingResult = routingEngine.GetRoute(startPoint, endPoint);
                var route = routingResult.Route;
                if (route.Vertices.Count > 0)
                {
                    route = projection == null ? route : (LineShape)projection.ConvertToExternalProjection(route);
                    routingLayer.InternalFeatures.Add(new Feature(route));
                }
                else
                {
                    string errorMessage = String.Format("There’s no road within {0} {1} to where you clicked on", routingEngine.SearchRadiusInMeters, Enum.GetName(typeof(GeographyUnit), map.MapUnit));
                    System.Windows.MessageBox.Show(errorMessage);
                }

                map.Refresh();
            }
        }
    }
}
