/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DelayDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShapeFileFeatureLayer delayRefreshLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize map control
            wpfMap.MapUnit = GeographyUnit.Meter;
            wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap.Overlays.Add(baseOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            wpfMap.Overlays.Add("layerOverlay", layerOverlay);

            delayRefreshLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\USStates.shp");
            delayRefreshLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.GetRandomGeoColor(RandomColorType.All), GeoColor.GetRandomGeoColor(RandomColorType.All));
            delayRefreshLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add("layer", delayRefreshLayer);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan delayTime = TimeSpan.Zero;
            if ((bool)ckbDelayDrawing.IsChecked)
            {
                // Calculate the delay time
                delayTime = new TimeSpan(0, 0, 0, cmbDelayTime.SelectedIndex + 1, 0);
                // Show a timer prgress bar
                DoProgressBarAnimation();
            }
            // Change the style of the layer after refresh
            delayRefreshLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.GetRandomGeoColor(RandomColorType.All), GeoColor.GetRandomGeoColor(RandomColorType.All));
            wpfMap.Refresh(delayTime);
        }

        private void DoProgressBarAnimation()
        {
            double totalMilliseconds = new TimeSpan(0, 0, cmbDelayTime.SelectedIndex + 1).TotalMilliseconds;

            Thread delayTimeThread = new Thread(() =>
            {
                int secondsConter = 0;
                while (secondsConter < totalMilliseconds)
                {
                    secondsConter++;
                    TimeSpan currentTime = TimeSpan.FromMilliseconds(totalMilliseconds - secondsConter);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        string[] tempTime = currentTime.ToString(@"ss\:fff").Split(':');
                        txtDelayTime.Text = String.Format(CultureInfo.InvariantCulture, @"{0} Seconds, {1} MilliSeconds", tempTime[0], tempTime[1]);
                        TimeProgressBar.Value = secondsConter / totalMilliseconds * 100;
                    }));
                    Thread.Sleep(1);
                }
            });

            delayTimeThread.Start();
        }
    }
}