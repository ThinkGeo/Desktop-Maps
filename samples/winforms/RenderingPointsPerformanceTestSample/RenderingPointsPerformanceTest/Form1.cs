using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace RenderingPointsPerformanceTest
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private LayerOverlay layerOverlay;
        private Stopwatch stopwatch = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += RefreshPointLayer;

            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = ThinkGeoCloudMapsOverlay.GetZoomLevelSet();

            ThinkGeoCloudMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudMapsOverlay();
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            layerOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-12855175, 6098541, -10047697, 3806707);
            winformsMap1.Refresh();
        }

        private void RefreshPointLayer(object sender, EventArgs e)
        {
            stopwatch.Restart();
            var selectedLayer = layerOverlay.Layers[0];
            if (selectedLayer is CustomInmemoryFeatureLayer)
            {
                foreach (Feature feature in ((CustomInmemoryFeatureLayer)selectedLayer).InternalFeatures)
                {
                    byte[] bytes = feature.GetWellKnownBinary();
                    UpdateWkb(bytes, 20000);
                    feature.SetWellKnownBinary(bytes);
                }
            }

            winformsMap1.Refresh(layerOverlay);
            stopwatch.Stop();
            var refreshCount = Convert.ToInt32(refreshCountLabel.Text) + 1;
            refreshCountLabel.Text = refreshCount.ToString();
            timeLabel.Text = stopwatch.ElapsedMilliseconds.ToString();
        }

        private void InitialPoints()
        {
            CustomInmemoryFeatureLayer pointFeatureLayer = new CustomInmemoryFeatureLayer();
            pointFeatureLayer.GridSizeInPixel = Convert.ToInt32(filterRadiusTextBox.Text);
            pointFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, new GeoSolidBrush(GeoColor.SimpleColors.Blue), 2);
            pointFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Random random = new Random();
            RectangleShape testExtent = new RectangleShape(-12855175, 6098541, -10047697, 3806707);
            double pointCount = Convert.ToDouble(pointCountTextBox.Text);
            for (int i = 0; i < pointCount; i++)
            {
                var randomX = random.NextDouble();
                var x = testExtent.UpperLeftPoint.X + testExtent.Width * randomX;
                var randomY = random.NextDouble();
                var y = testExtent.LowerLeftPoint.Y + testExtent.Height * randomY;

                pointFeatureLayer.InternalFeatures.Add(new Feature(x, y));
            }
            layerOverlay.Layers.Clear();
            layerOverlay.Layers.Add(pointFeatureLayer);

            winformsMap1.CurrentExtent = new RectangleShape(-12855175, 6098541, -10047697, 3806707);
            winformsMap1.Refresh();
        }

        private void StartRefreshButton_Clicked(object sender, EventArgs e)
        {
            InitialPoints();

            filterRadiusTextBox.Enabled = false;
            pointCountTextBox.Enabled = false;
            timer.Start();
        }

        private void StopRefreshButton_Clicked(object sender, EventArgs e)
        {
            timer.Stop();
            stopwatch.Reset();
            refreshCountLabel.Text = timeLabel.Text = "0";

            filterRadiusTextBox.Enabled = true;
            pointCountTextBox.Enabled = true;
        }

        private void UpdateWkb(byte[] wkb, double delta)
        {
            double x = BitConverter.ToDouble(wkb, 5);
            double y = BitConverter.ToDouble(wkb, 13);
            BitConverter.GetBytes(x + delta).CopyTo(wkb, 5);
            BitConverter.GetBytes(y + delta).CopyTo(wkb, 13);
        }
    }
}