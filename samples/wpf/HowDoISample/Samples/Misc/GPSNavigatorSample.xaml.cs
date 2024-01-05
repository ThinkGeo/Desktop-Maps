using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    public partial class GPSNavigatorSample : UserControl
    {
        private string parentFolder;
        private int index = 0;

        private DispatcherTimer updateTimer = new DispatcherTimer();
        private Collection<PointShape> gpsPoints = new Collection<PointShape>();
        private InMemoryFeatureLayer mapVehicleLayer = new InMemoryFeatureLayer();
        private LayerOverlay staticOverlay1 = new LayerOverlay() { TileBuffer = 0 };
        private LayerOverlay dynamicOverlay1 = new LayerOverlay() { TileType = TileType.SingleTile };
        private LayerOverlay staticOverlay2 = new LayerOverlay() { TileBuffer = 0 };
        private LayerOverlay dynamicOverlay2 = new LayerOverlay() { TileType = TileType.SingleTile };
        private RotatedImageStyle rotatedVehicleImageStyle = null;

        public GPSNavigatorSample()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            parentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly()?.Location);
            LoadTestGPSPoints();

            mapView1.MapUnit = GeographyUnit.DecimalDegree;
            mapView2.MapUnit = GeographyUnit.DecimalDegree;
            mapView1.MapTools.PanZoomBar.IsEnabled = false;
            mapView2.MapTools.PanZoomBar.IsEnabled = false;

            string baseMapImagePath = parentFolder + "\\Data\\GPSNavigator\\GoogleMapImage.bmp";
            NativeImageRasterLayer baseMapImageLayer = new NativeImageRasterLayer(baseMapImagePath);
            staticOverlay1.Layers.Add(baseMapImageLayer);
            staticOverlay2.Layers.Add(baseMapImageLayer);

            rotatedVehicleImageStyle = new RotatedImageStyle();
            mapVehicleLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            mapVehicleLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(rotatedVehicleImageStyle);
            mapVehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapVehicleLayer.DrawingMarginInPixel = 256;
            dynamicOverlay1.Layers.Add(mapVehicleLayer);
            dynamicOverlay2.Layers.Add(mapVehicleLayer);

            mapView1.Overlays.Add(staticOverlay1);
            mapView1.Overlays.Add(dynamicOverlay1);

            mapView2.Overlays.Add(staticOverlay2);
            mapView2.Overlays.Add(dynamicOverlay2);

            mapView1.CurrentExtent = new RectangleShape(-97.06958839140286, 36.78895622299559, -97.04641410551419, 36.75943046615965);
            mapView2.CurrentExtent = new RectangleShape(-97.0618369577802, 36.7800009913563, -97.0568841762454, 36.7724514389474);
            mapView1.CurrentExtentChanged += MapView1_CurrentExtentChanged;
            updateTimer.Interval = TimeSpan.FromMilliseconds(100);
            updateTimer.Tick += new EventHandler(UpdateTimer_Tick);
            updateTimer.Start();
        }

        private void MapView1_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            Debug.WriteLine(e.NewExtent.ToString());
        }

        private void UpdateVehicleLayer()
        {
            var angle = index > 0 ? CalculateAngle(gpsPoints[index - 1], gpsPoints[index]) : 0;
            mapVehicleLayer.InternalFeatures.Clear();
            Feature MainVehicleFeature = new Feature(gpsPoints[index]);
            MainVehicleFeature.ColumnValues["Vehicle"] = "Main";
            MainVehicleFeature.ColumnValues["Angle"] = string.Format("{0:0.00}", -angle);
            mapVehicleLayer.InternalFeatures.Add("Vehicle1", MainVehicleFeature);
        }

        private void UpdateMap2()
        {
            var angle = index > 0 ? CalculateAngle(gpsPoints[index - 1], gpsPoints[index]) : 0;
            mapView2.RotatedAngle = -angle;
            mapView2.RotatedAngle = angle - 90;
            mapView2.CenterAtAsync(gpsPoints[index]);
            dynamicOverlay2.RefreshAsync();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateVehicleLayer();
            dynamicOverlay1.RefreshAsync();
            UpdateMap2();
            index++;
            if (index >= gpsPoints.Count) index = 0;
        }

        private void LoadTestGPSPoints()
        {
            string[] lines = File.ReadAllLines(parentFolder + "\\Data\\GPSNavigator\\gps3.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() != "")
                {
                    string[] parts = lines[i].Trim().Split(',');
                    if (parts[0] == "$GPGGA")
                    {
                        string sLat = parts[2]; string sLatHem = parts[3];
                        string sLon = parts[4]; string sLonHem = parts[5];
                        int LatDecIndex = sLat.IndexOf(".");
                        int degrees = int.Parse(sLat.Substring(0, LatDecIndex - 2));
                        double minutes = double.Parse(sLat.Substring(LatDecIndex - 2), NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-US"));
                        double Lat = degrees + minutes / 60.0;
                        if (sLatHem == "S") Lat = -Lat;

                        int LonDecIndex = sLon.IndexOf(".");
                        degrees = int.Parse(sLon.Substring(0, LonDecIndex - 2));
                        minutes = double.Parse(sLon.Substring(LonDecIndex - 2), NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-US"));
                        double Lon = degrees + minutes / 60.0;
                        if (sLonHem == "W") Lon = -Lon;
                        gpsPoints.Add(new PointShape(Lon, Lat));
                    }
                }
            }
        }

        private int CalculateAngle(PointShape p1, PointShape p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;
            return int.Parse((Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI).ToString("0"));
        }
    }

    internal class RotatedImageStyle : ThinkGeo.Core.Style
    {
        private GeoImage mainVehicleImage;

        public RotatedImageStyle()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream imageStream = asm.GetManifestResourceStream("ThinkGeo.UI.Wpf.HowDoI.Data.GPSNavigator.arrow.png");
            mainVehicleImage = new GeoImage(imageStream);
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (Feature feature in features)
            {
                PointShape FeaturePointShape = (PointShape)feature.GetShape();
                float angleData = float.Parse(feature.ColumnValues["Angle"], NumberStyles.Float, CultureInfo.CreateSpecificCulture("en-US"));
                canvas.DrawWorldImageWithoutScaling(mainVehicleImage, FeaturePointShape.X, FeaturePointShape.Y, DrawingLevel.LevelThree, 0, 0, 360 - angleData);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            return new Collection<string>() { "Angle" , "Vehicle"};
        }
    }
}