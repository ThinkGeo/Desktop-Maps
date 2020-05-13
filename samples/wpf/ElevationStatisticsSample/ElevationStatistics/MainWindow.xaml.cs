/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
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
        private string sourceDir = @"..\..\Resource";

        public MainWindow()
        {
            InitializeComponent();
        }

        public List<double> ChartAxisLabels { get; set; }

        public ChartValues<CustomerVm> ChartData { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
            wpfMap1.Overlays.Add(baseOverlay);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            Marker marker = new Marker();
            markerOverlay.Markers.Add(marker);
            wpfMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            GpxFeatureLayer gpxFeatureLayer = new GpxFeatureLayer(sourceDir + @"\Telescope-Peak.gpx");
            gpxFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColors.GreenYellow, 2, GeoColors.White, 6, true);
            gpxFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay gpxOverlay = new LayerOverlay();
            gpxOverlay.Layers.Add("gpxlayer", gpxFeatureLayer);
            wpfMap1.Overlays.Add("gpxoverlay", gpxOverlay);

            wpfMap1.CurrentExtent = new RectangleShape(-13037957.125312, 4333890.22845191, -13023090.1185952, 4319062.53066272);

            CreateChartByGpxFile();
        }

        private void CreateChartByGpxFile()
        {
            Collection<PointShape> points = GetPointsByGpxFile();
            Collection<Feature> features = GetElevationByPoints(points);
            AddElevationToChart(features);
        }

        private void AddElevationToChart(Collection<Feature> features)
        {
            ChartAxisLabels = new List<double>();
            ChartData = new ChartValues<CustomerVm>();

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
                ChartData.Add(new CustomerVm(value, point.X, point.Y, tmpDistance));

                lastPoint = point;
            }

            var mapper = Mappers.Xy<CustomerVm>().X(value => value.Distance).Y(value => value.Elevation);
            Charting.For<CustomerVm>(mapper);
            DataContext = this;
        }

        private Collection<Feature> GetElevationByPoints(Collection<PointShape> points)
        {
            Elevation.Elevation elevation = new Elevation.Elevation();
            elevation.ElevationFeatureSourcesInDecimalDegrees.Add(new SrtmElevationFeatureSource(sourceDir));
            elevation.Open();
            var features = elevation.GetElevationByPoints(points, 3857, DistanceUnit.Meter);
            elevation.Close();
            return features;
        }

        private Collection<PointShape> GetPointsByGpxFile()
        {
            Collection<PointShape> points = new Collection<PointShape>();
            string filePath = sourceDir + @"\Telescope-Peak.gpx";

            GpxFeatureLayer gpxFeatureLayer = new GpxFeatureLayer(sourceDir + @"\Telescope-Peak.gpx");
            gpxFeatureLayer.Open();
            var features = gpxFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);

            foreach (var feature in features)
            {
                if (feature.GetWellKnownType() == WellKnownType.Point)
                {
                    points.Add(new PointShape(feature.GetWellKnownText()));
                }
            }

            gpxFeatureLayer.Close();
            return points;
        }

        private void lvcChart_DataHover(object sender, ChartPoint chartPoint)
        {
            CustomerVm instance = (CustomerVm)chartPoint.Instance;
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)wpfMap1.Overlays["MarkerOverlay"];
            markerOverlay.Markers[0] = new Marker(instance.Longitude, instance.Latitude);
            markerOverlay.Refresh();
        }
    }
}
