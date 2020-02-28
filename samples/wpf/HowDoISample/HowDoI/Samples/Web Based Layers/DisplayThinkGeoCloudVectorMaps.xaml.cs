using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayThinkGeoCloudVectorMaps : UserControl
    {
        public DisplayThinkGeoCloudVectorMaps()
        {
            InitializeComponent();
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map.CurrentExtent = new RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);

            var overlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            overlay.MapType = ThinkGeoCloudVectorMapsMapType.Default;
            Map.Overlays.Add("ThinkGeoCloudVectorMapsOverlay", overlay);
            Map.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded && Map.Overlays.Contains("ThinkGeoCloudVectorMapsOverlay"))
            {
                var overlay = (ThinkGeoCloudVectorMapsOverlay)Map.Overlays["ThinkGeoCloudVectorMapsOverlay"];
                overlay.MapType = (ThinkGeoCloudVectorMapsMapType)Enum.Parse(typeof(ThinkGeoCloudVectorMapsMapType),
                    (sender as RadioButton).Content.ToString());
                if (overlay.MapType == ThinkGeoCloudVectorMapsMapType.CustomizedByStyleJson)
                {
                    overlay.StyleJsonUri = new Uri(SampleHelper.Get("mutedblue.json"), UriKind.Relative);
                }
                Map.Refresh();
            }
        }
    }
}