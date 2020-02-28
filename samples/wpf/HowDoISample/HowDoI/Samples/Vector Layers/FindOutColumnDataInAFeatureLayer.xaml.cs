using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class FindOutColumnDataInAFeatureLayer : UserControl
    {
        public FindOutColumnDataInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14283508, 20037508, 14676953, -9263005);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitDesktopOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add("PopupOverlay", popupOverlay);

            mapView.Refresh();
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");

            worldLayer.Open();
            Collection<Feature> selectedFeatures = worldLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, new string[2] { "CNTRY_NAME", "POP_CNTRY" });
            worldLayer.Close();

            if (selectedFeatures.Count > 0)
            {
                StringBuilder info = new StringBuilder();
                info.AppendLine(String.Format(CultureInfo.InvariantCulture, "CNTRY_NAME:\t{0}", selectedFeatures[0].ColumnValues["CNTRY_NAME"]));
                info.AppendLine(String.Format(CultureInfo.InvariantCulture, "POP_CNTRY:\t{0}", double.Parse(selectedFeatures[0].ColumnValues["POP_CNTRY"]).ToString("n0")));
                TBInfo.Text = info.ToString();

                PopupOverlay popupOverlay = (PopupOverlay)mapView.Overlays["PopupOverlay"];
                Popup popup = new Popup(e.WorldLocation);
                popup.Content = info.ToString();
                popup.FontSize = 10d;
                popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");

                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);
                popupOverlay.Refresh();
            }
        }
    }
}