using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Interaction logic for GetDataFromFeature.xaml
    /// </summary>
    public partial class GetDataFromFeature : UserControl
    {
        public GetDataFromFeature()
        {
            InitializeComponent();
        }
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoPOI/Parks.shp");
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.CadetBlue), GeoColors.DarkBlue, 5);

            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = projectionConverter;

            LayerOverlay hotelsOverlay = new LayerOverlay();
            hotelsOverlay.Layers.Add("Frisco Parks", parksLayer);
            hotelsOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add(hotelsOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add("Info Popup Overlay", popupOverlay);

            parksLayer.Open();
            RectangleShape parksBoundingBox = parksLayer.GetBoundingBox();
            parksLayer.Close();

            mapView.CurrentExtent = parksBoundingBox;

            mapView.Refresh();
        }

        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            FeatureLayer parksLayer = mapView.FindFeatureLayer("Frisco Parks");

            parksLayer.Open();
            var selectedFeature = parksLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.AllColumns).FirstOrDefault();
            parksLayer.Close();

            if (selectedFeature != null)
            {
                StringBuilder parkInfoString = new StringBuilder();

                foreach(var column in selectedFeature.ColumnValues)
                {
                    parkInfoString.AppendLine(String.Format("{0}: {1}", column.Key, column.Value));
                }

                PopupOverlay popupOverlay = (PopupOverlay)mapView.Overlays["Info Popup Overlay"];
                Popup popup = new Popup(e.WorldLocation);
                popup.Content = parkInfoString.ToString();
                popup.FontSize = 10d;
                popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");

                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);
                popupOverlay.Refresh();
            }
        }
    }
}
