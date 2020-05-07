using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class CreateARestrictionLayer : UserControl
    {
        public CreateARestrictionLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-4904363, 6313059, 9575868, -5401887);

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add("WorldOverlay", worldMapKitDesktopOverlay);

            RestrictionLayer restrictionLayer = new RestrictionLayer();
            restrictionLayer.Zones.Add(new RectangleShape(-1967015, 4440501, 6681396, -4120479));
            restrictionLayer.RestrictionMode = RestrictionMode.ShowZones;
            restrictionLayer.UpperScale = 250000000;
            restrictionLayer.LowerScale = double.MinValue;

            LayerOverlay restrictionOverlay = new LayerOverlay();
            restrictionOverlay.Layers.Add("RestrictionLayer", restrictionLayer);
            mapView.Overlays.Add("RestrictionOverlay", restrictionOverlay);

            mapView.Refresh();
        }

        private void cmbRestrictionStyle_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay dynamicOverlay = (LayerOverlay)mapView.Overlays[1];
                RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
                if (currentRestrictionLayer.CustomStyles.Count > 0)
                {
                    currentRestrictionLayer.CustomStyles.Clear();
                }

                switch (cmbRestrictionStyle.SelectedItem.ToString().Split(':')[1].Trim())
                {
                    case "HatchPattern":
                        currentRestrictionLayer.RestrictionStyle = RestrictionStyle.HatchPattern;
                        break;

                    case "CircleWithSlashImage":
                        currentRestrictionLayer.RestrictionStyle = RestrictionStyle.CircleWithSlashImage;
                        break;

                    case "UseCustomStyles":
                        currentRestrictionLayer.RestrictionStyle = RestrictionStyle.UseCustomStyles;
                        AreaStyle customStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(150, GeoColors.Gray)));
                        currentRestrictionLayer.CustomStyles.Add(customStyle);
                        break;

                    default:
                        break;
                }

                mapView.Overlays["RestrictionOverlay"].Refresh();
            }
        }

        private void rbtnShowMode_Click(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay dynamicOverlay = (LayerOverlay)mapView.Overlays[1];
                RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
                currentRestrictionLayer.RestrictionMode = RestrictionMode.ShowZones;
                mapView.Overlays["RestrictionOverlay"].Refresh();
            }
        }

        private void rbtnHideMode_Click(object sender, RoutedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay dynamicOverlay = (LayerOverlay)mapView.Overlays[1];
                RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
                currentRestrictionLayer.RestrictionMode = RestrictionMode.HideZones;
                mapView.Overlays["RestrictionOverlay"].Refresh();
            }
        }
    }
}