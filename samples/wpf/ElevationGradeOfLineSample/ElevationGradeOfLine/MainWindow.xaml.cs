/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Elevation;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.DebugSamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string sourceDir = @"..\..\AppData";
        private Marker startMarker;
        private Marker endMarker;
        private LineShape gradeLine;
        private Marker waypointMarker;
        private System.Timers.Timer timer;

        public MainWindow()
        {
            this.ChartAxisLabels = new ObservableCollection<double>();
            this.ChartData = new ChartValues<ChartInformation>();
            this.gradeLine = new LineShape();

            InitializeComponent();
        }

        public ObservableCollection<double> ChartAxisLabels
        {
            get;
            set;
        }

        public ChartValues<ChartInformation> ChartData
        {
            get;
            set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-13044244, 4331269, -13024981, 4318225);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
            wpfMap1.Overlays.Add(baseOverlay);

            // Add gradeline layer.
            InMemoryFeatureLayer gradeLineLayer = new InMemoryFeatureLayer();
            gradeLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromHtml("#b5e858"), 3, true);
            gradeLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            gradeLineLayer.DrawingQuality = DrawingQuality.HighQuality;

            // Add start and end point layer.
            InMemoryFeatureLayer pointFeatureLayer = new InMemoryFeatureLayer();
            pointFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColors.Black, 7, GeoColors.White);
            pointFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            pointFeatureLayer.DrawingQuality = DrawingQuality.HighQuality;

            // Add a broundary where the elevation data is only available inside.
            InMemoryFeatureLayer validBoundaryFeatureLayer = new InMemoryFeatureLayer();
            validBoundaryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Yellow, 2);
            validBoundaryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            validBoundaryFeatureLayer.DrawingQuality = DrawingQuality.HighQuality;
            validBoundaryFeatureLayer.InternalFeatures.Add(new Feature(new RectangleShape(new PointShape(-13046957.5853926, 4338200.21850381), new PointShape(-13024790.8471899, 4315280.49759674))));

            // waypoint hover marker
            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            waypointMarker = new Marker()
            {
                ImageSource = new BitmapImage(new Uri("/Images/waypoint.png", UriKind.RelativeOrAbsolute)),
                YOffset = -16
            };
            markerOverlay.Markers.Add(waypointMarker);
            wpfMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            LayerOverlay elevationLayerOverlay = new LayerOverlay();
            elevationLayerOverlay.Layers.Add("elevationLayer", gradeLineLayer);
            elevationLayerOverlay.Layers.Add("pointFeatureLayer", pointFeatureLayer);
            wpfMap1.Overlays.Add("elevationLayerOverlay", elevationLayerOverlay);

            LayerOverlay rectangleLayerOverlay = new LayerOverlay();
            rectangleLayerOverlay.Layers.Add("rectangleLayer", validBoundaryFeatureLayer);
            wpfMap1.Overlays.Add("rectangleLayerOverlay", rectangleLayerOverlay);

            // Load first load data
            Initialization();

            wpfMap1.Refresh();
        }

        private void Initialization()
        {
            // Set markders
            SimpleMarkerOverlay markOverlay = new SimpleMarkerOverlay();
            wpfMap1.Overlays.Add("markOverlay", markOverlay);

            startMarker = new Marker(new PointShape(-13040922, 4327858))
            {
                YOffset = -24,
                ImageSource = new BitmapImage(new Uri("/Images/start.png", UriKind.RelativeOrAbsolute))
            };
            markOverlay.Markers.Add(startMarker);
            endMarker = new Marker(new PointShape(-13038907, 4324920))
            {
                ImageSource = new BitmapImage(new Uri("/Images/end.png", UriKind.RelativeOrAbsolute)),
                YOffset = -24
            };
            markOverlay.Markers.Add(endMarker);

            // Add example line points
            gradeLine.Vertices.Add(new Vertex(-13040922, 4327858));
            gradeLine.Vertices.Add(new Vertex(-13029512, 4321446));
            gradeLine.Vertices.Add(new Vertex(-13028766, 4326396));
            gradeLine.Vertices.Add(new Vertex(-13034978, 4324148));
            gradeLine.Vertices.Add(new Vertex(-13038907, 4324920));
        }

        private void CreateChartByLine(LineShape line, int pointNumber)
        {
            Collection<Feature> lineFeatures = GetElevationByLine(line, pointNumber);
            ShowElevationOnChart(lineFeatures);
            DrawElevationLineOnMap(lineFeatures);
        }

        private void CreateChartByPoint(PointShape point)
        {
            Feature pointFeature = GetElevationByPoint(point);
            Collection<Feature> features = new Collection<Feature>();
            features.Add(pointFeature);
            ShowElevationOnChart(features);
        }

        private void ShowElevationOnChart(Collection<Feature> features)
        {
            ChartAxisLabels.Clear();
            ChartData.Clear();

            double distance = 0.0;
            int index = 0;
            PointShape lastPoint = new PointShape();

            foreach (var feature in features)
            {
                PointShape point = new PointShape(feature.ColumnValues["point"]);
                if (index++ != 0)
                {
                    LineShape line = new LineShape(new Collection<Vertex> { new Vertex(lastPoint), new Vertex(point) });
                    distance += line.GetAccurateLength(4326, DistanceUnit.Meter, DistanceCalculationMode.Haversine);
                }

                double tmpDistance = Math.Round(distance / 1000.0, 2);
                double value = Math.Round(double.Parse(feature.ColumnValues["elevation"]), 2);
                ChartAxisLabels.Add(tmpDistance);
                ChartData.Add(new ChartInformation(value, point.X, point.Y, tmpDistance));

                lastPoint = point;
            }

            var mapper = Mappers.Xy<ChartInformation>().X(value => value.Distance).Y(value => value.Elevation);
            Charting.For<ChartInformation>(mapper);
            DataContext = this;
        }

        private Collection<Feature> GetElevationByLine(LineShape line, int pointNumber)
        {
            Elevation.Elevation elevation = new Elevation.Elevation();
            elevation.ElevationFeatureSourcesInDecimalDegrees.Add(new SrtmElevationFeatureSource(sourceDir));
            elevation.Open();
            Collection<Feature> features = elevation.GetElevationByLine(line, 3857, DistanceUnit.Meter, pointNumber);

            elevation.Close();
            return features;
        }

        private void DrawElevationLineOnMap(Collection<Feature> lineFeatures)
        {
            PointShape endPoint = lineFeatures[lineFeatures.Count - 1].GetShape() as PointShape;
            endMarker.Position = new Point(endPoint.X, endPoint.Y);

            // Draw the line
            var elevationlayer = ((LayerOverlay)wpfMap1.Overlays["elevationLayerOverlay"]).Layers["elevationLayer"] as InMemoryFeatureLayer;
            var pointlayer = ((LayerOverlay)wpfMap1.Overlays["elevationLayerOverlay"]).Layers["pointFeatureLayer"] as InMemoryFeatureLayer;
            pointlayer.InternalFeatures.Clear();

            LineShape lineshape = new LineShape();
            foreach (Feature feature in lineFeatures)
            {
                lineshape.Vertices.Add(new Vertex(feature.GetShape() as PointShape));
                pointlayer.InternalFeatures.Add(new Feature(feature.GetShape() as PointShape));
            }
            elevationlayer.InternalFeatures.Clear();
            elevationlayer.InternalFeatures.Add(new Feature(lineshape));

            wpfMap1.Overlays["markOverlay"].Refresh();
            wpfMap1.Overlays["elevationLayerOverlay"].Refresh();
        }

        private Feature GetElevationByPoint(PointShape point)
        {
            Elevation.Elevation elevation = new Elevation.Elevation();
            elevation.ElevationFeatureSourcesInDecimalDegrees.Add(new SrtmElevationFeatureSource(sourceDir));
            elevation.Open();
            var features = elevation.GetElevationByPoint(point, 3857, DistanceUnit.Meter);
            elevation.Close();
            return features;
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer(200);
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            }
            else
            {
                timer.Stop();
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                if (gradeLine.Vertices.Count > 1)
                {
                    Collection<Feature> lineFeatures = GetElevationByLine(gradeLine, (int)slider.Value);
                    ShowElevationOnChart(lineFeatures);
                    DrawElevationLineOnMap(lineFeatures);

                }
                if (timer != null)
                {
                    timer.Stop();
                    timer = null;
                }
            });

        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            // Clear chart
            this.ChartAxisLabels.Clear();
            this.ChartData.Clear();

            // Clear point selected
            startMarker.Position = new Point(0, 0);
            endMarker.Position = new Point(0, 0);
            gradeLine.Vertices.Clear();

            // Clear line style
            var elevationlayer = ((LayerOverlay)wpfMap1.Overlays["elevationLayerOverlay"]).Layers["elevationLayer"] as InMemoryFeatureLayer;
            elevationlayer.InternalFeatures.Clear();
            var pointlayer = ((LayerOverlay)wpfMap1.Overlays["elevationLayerOverlay"]).Layers["pointFeatureLayer"] as InMemoryFeatureLayer;
            pointlayer.InternalFeatures.Clear();
            wpfMap1.Overlays["elevationLayerOverlay"].Refresh();

            // Clear slider
            slider.Value = 0;
        }

        private void SelectLinePoint_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (gradeLine.Vertices.Count > 0)
            {
                gradeLine.Vertices.Add(new Vertex(e.WorldLocation.X, e.WorldLocation.Y));
                slider.Value = 15;
            }
            else
            {
                startMarker.Position = new Point(e.WorldLocation.X, e.WorldLocation.Y);
                gradeLine.Vertices.Add(new Vertex(e.WorldLocation.X, e.WorldLocation.Y));
                wpfMap1.Overlays["markOverlay"].Refresh();
                CreateChartByPoint(new PointShape(new Vertex(e.WorldLocation.X, e.WorldLocation.Y)));
                slider.Value = 1;
            }
        }

        private void lvcChart_DataHover(object sender, ChartPoint chartPoint)
        {
            ChartInformation instance = (ChartInformation)chartPoint.Instance;
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)wpfMap1.Overlays["MarkerOverlay"];
            markerOverlay.Markers[0].Position = new Point(instance.Longitude, instance.Latitude);
            markerOverlay.Refresh();
        }

        private void lvcChart_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)wpfMap1.Overlays["MarkerOverlay"];
            markerOverlay.Markers[0].Position = new Point(-179.0, -89.0);
            markerOverlay.Refresh();
        }
    }
}
