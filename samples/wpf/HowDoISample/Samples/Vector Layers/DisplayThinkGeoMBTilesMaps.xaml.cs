using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DisplayThinkGeoMBTilesMaps : UserControl
    {
        private readonly LayerOverlay layerOverlay;
        private ThinkGeoMBTilesLayer thinkGeoMBTilesFeatureLayer;
        private RasterTileCache tmpBitmapTileCache;

        public DisplayThinkGeoMBTilesMaps()
        {
            layerOverlay = new LayerOverlay();
            var styleJsonUri = new Uri(SampleHelper.Get("thinkgeo-world-streets-light.json"), UriKind.Relative);
            thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesLayer(SampleHelper.Get("tiles_Frisco.mbtiles"), styleJsonUri);
            tmpBitmapTileCache = null;

            InitializeComponent();
        }

        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            layerOverlay.TileWidth = 512;
            layerOverlay.TileHeight = 512;
            layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);

            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);
            Map.Overlays.Add(layerOverlay);
            Map.Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxDisableRasterCache != null && thinkGeoMBTilesFeatureLayer != null)
            {
                Uri styleJsonUri = thinkGeoMBTilesFeatureLayer.StyleJsonUri;
                string senderName = (sender as RadioButton).Name;
                if (senderName == "RadioButtonLight")
                {
                    styleJsonUri = new Uri(SampleHelper.Get("thinkgeo-world-streets-light.json"), UriKind.Relative);
                }
                else if (senderName == "RadioButtionDark")
                {
                    styleJsonUri = new Uri(SampleHelper.Get("thinkgeo-world-streets-dark.json"), UriKind.Relative);
                }
                else if (senderName == "RadioButtionMuteblue")
                {
                    styleJsonUri = new Uri(SampleHelper.Get("mutedblue.json"), UriKind.Relative);
                }

                layerOverlay.Layers.Clear();
                thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesLayer(SampleHelper.Get("tiles_Frisco.mbtiles"),
                    styleJsonUri);
                UpdateTileCache(CheckBoxDisableRasterCache.IsChecked.Value);
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);
                Map.Refresh();
            }
        }

        private void CheckBoxDisableRasterCache_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTileCache(CheckBoxDisableRasterCache.IsChecked.Value);
        }

        private void UpdateTileCache(bool disableTileCache)
        {
            if (thinkGeoMBTilesFeatureLayer != null && thinkGeoMBTilesFeatureLayer.BitmapTileCache != null)
            {
                tmpBitmapTileCache = thinkGeoMBTilesFeatureLayer.BitmapTileCache;
            }

            if (disableTileCache)
            {
                thinkGeoMBTilesFeatureLayer.BitmapTileCache = null;
            }
            else
            {
                thinkGeoMBTilesFeatureLayer.BitmapTileCache = tmpBitmapTileCache;
            }
        }
    }
}
