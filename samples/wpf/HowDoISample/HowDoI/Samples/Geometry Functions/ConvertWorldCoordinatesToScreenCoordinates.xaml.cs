using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class ConvertWorldCoordinatesToScreenCoordinates : UserControl
    {
        public ConvertWorldCoordinatesToScreenCoordinates()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.TileType = TileType.SingleTile;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(worldOverlay);

            mapView.Refresh();
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            ScreenPointF screenPoint = MapUtil.ToScreenCoordinate(mapView.CurrentExtent, new PointShape(Double.Parse(longitudeTextBox.Text, CultureInfo.InvariantCulture), Double.Parse(latitudeTextBox.Text, CultureInfo.InvariantCulture)), 740, 528);
            screenPositionTextBox.Text = string.Format(CultureInfo.InvariantCulture, "({0}, {1})", screenPoint.X.ToString("N0", CultureInfo.InvariantCulture), screenPoint.Y.ToString("N0", CultureInfo.InvariantCulture));
        }
    }
}