using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for PanAroundTheMap.xaml
    /// </summary>
    public partial class PanAroundTheMap : UserControl
    {
        public PanAroundTheMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);
            mapView.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Tag.ToString();
            switch (buttonText)
            {
                case "Up":
                case "Down":
                case "Right":
                case "Left":
                    PanDirection panDirection = (PanDirection)Enum.Parse(typeof(PanDirection), buttonText);
                    mapView.Pan(panDirection, 20);
                    break;

                case "Pan":
                    float degree = float.Parse(tbDegree.Text);
                    int percentage = Int32.Parse(tbPercentage.Text);
                    mapView.Pan(degree, percentage);
                    break;
            }
        }
    }
}