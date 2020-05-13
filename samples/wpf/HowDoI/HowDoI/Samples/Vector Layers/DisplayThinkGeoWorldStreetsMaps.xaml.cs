using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayThinkGeoWorldStreetsMaps.xaml
    /// </summary>
    public partial class DisplayThinkGeoWorldStreetsMaps : UserControl
    {
        string currentExtentChangedSource = null;

        private readonly ThinkGeoCloudVectorMapsLayer thinkGeoMBTilesFeatureLayerLight;
        private readonly ThinkGeoCloudVectorMapsLayer thinkGeoMBTilesFeatureLayerDark;
        private readonly ThinkGeoCloudVectorMapsLayer thinkGeoMBTilesFeatureLayerTransparent;

        private readonly LayerOverlay layerOverlay;
        private readonly ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay;

        public DisplayThinkGeoWorldStreetsMaps()
        {
            InitializeComponent();

            thinkGeoMBTilesFeatureLayerLight = new ThinkGeoCloudVectorMapsLayer(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, new System.Uri(SampleHelper.Get("thinkgeo-world-streets-light.json"), System.UriKind.Relative)) { BitmapTileCache = null };
            thinkGeoMBTilesFeatureLayerDark = new ThinkGeoCloudVectorMapsLayer(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, new System.Uri(SampleHelper.Get("thinkgeo-world-streets-dark.json"), System.UriKind.Relative)) { BitmapTileCache = null };
            thinkGeoMBTilesFeatureLayerTransparent = new ThinkGeoCloudVectorMapsLayer(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, new System.Uri(SampleHelper.Get("thinkgeo-world-streets-hybrid.json"), System.UriKind.Relative)) { BitmapTileCache = null };
            thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret) { TileCache = null };
            thinkGeoCloudRasterMapsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
            layerOverlay = new LayerOverlay();
        }

        private void MapA_Loaded(object sender, RoutedEventArgs e)
        {
            MapA.CurrentExtentChanging += MapA_CurrentExtentChanging;
            MapA.CurrentExtentChanged += MapA_CurrentExtentChanged;

            MapA.MapUnit = GeographyUnit.Meter;
            MapA.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet(512);
            MapA.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);

            layerOverlay.TileCache = null;
            layerOverlay.TileWidth = 512;
            layerOverlay.TileHeight = 512;
            layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayerLight);

            MapA.Overlays.Add(layerOverlay);

            lightRbtn.IsChecked = true;
        }

        private void MapB_Loaded(object sender, RoutedEventArgs e)
        {
            MapB.CurrentExtentChanging += MapB_CurrentExtentChanging;
            MapB.CurrentExtentChanged += MapB_CurrentExtentChanged;

            MapB.MapUnit = GeographyUnit.Meter;
            MapB.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            MapB.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);

            MapB.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            lightRbtn.IsChecked = true;
        }

        private void MapA_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            if (currentExtentChangedSource == null)
            {
                currentExtentChangedSource = "MapA";
            }
        }

        private void MapB_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            if (currentExtentChangedSource == null)
            {
                currentExtentChangedSource = "MapB";
            }
        }

        private void MapA_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            if (currentExtentChangedSource == "MapA")
            {
                MapB.CurrentExtent = e.NewExtent;
                MapB.Refresh();
                currentExtentChangedSource = null;
            }
        }

        private void MapB_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            if (currentExtentChangedSource == "MapB")
            {
                MapA.CurrentExtent = e.NewExtent;
                MapA.Refresh();
                currentExtentChangedSource = null;
            }
        }

        private void LightRbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (lightRbtn.IsChecked.Value)
            {
                darkRbtn.IsChecked = false;
                transparentRbtn.IsChecked = false;

                thinkGeoCloudRasterMapsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
                thinkGeoCloudRasterMapsOverlay.Refresh();

                layerOverlay.Layers.Clear();
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayerLight);
                layerOverlay.Refresh();
            }
        }

        private void DarkRbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (darkRbtn.IsChecked.Value)
            {
                lightRbtn.IsChecked = false;
                transparentRbtn.IsChecked = false;

                thinkGeoCloudRasterMapsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark;
                thinkGeoCloudRasterMapsOverlay.Refresh();

                layerOverlay.Layers.Clear();
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayerDark);
                layerOverlay.Refresh();
            }
        }

        private void TransparentRbtn_Checked(object sender, RoutedEventArgs e)
        {
            if (transparentRbtn.IsChecked.Value)
            {
                lightRbtn.IsChecked = false;
                darkRbtn.IsChecked = false;

                thinkGeoCloudRasterMapsOverlay.MapType = ThinkGeoCloudRasterMapsMapType.TransparentBackground;
                thinkGeoCloudRasterMapsOverlay.Refresh();

                layerOverlay.Layers.Clear();
                layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayerTransparent);
                layerOverlay.Refresh();
            }
        }
    }
}
