using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayThinkGeoCloudRasterMaps : UserControl
    {
        private readonly ThinkGeoCloudRasterMapsOverlay overlay;

        public DisplayThinkGeoCloudRasterMaps()
        {
            overlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);

            InitializeComponent();
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            overlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;

            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map.CurrentExtent = new RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);
            Map.Overlays.Add(overlay);
            Map.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                overlay.MapType = (ThinkGeoCloudRasterMapsMapType)Enum.Parse(typeof(ThinkGeoCloudRasterMapsMapType),
                    (sender as RadioButton).Content.ToString());
                overlay.Refresh();
            }
        }
    }
}