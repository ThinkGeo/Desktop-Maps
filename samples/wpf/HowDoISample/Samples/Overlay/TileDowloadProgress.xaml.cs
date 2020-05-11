using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class TileDowloadProgress : UserControl
    {
        public TileDowloadProgress()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            ThinkGeoCloudRasterMapsOverlay.DrawTilesProgressChanged += new System.EventHandler<DrawTilesProgressChangedTileOverlayEventArgs>(ThinkGeoCloudRasterMapsOverlay_DrawTilesProgressChanged);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            mapView.Refresh();
        }

        private void ThinkGeoCloudRasterMapsOverlay_DrawTilesProgressChanged(object sender, DrawTilesProgressChangedTileOverlayEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }
    }
}