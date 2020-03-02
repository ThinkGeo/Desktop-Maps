using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class AddPopups : UserControl
    {
        public AddPopups()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            ThinkGeoCloudRasterMapsOverlay worldOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            Map1.Overlays.Add(worldOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            Map1.Overlays.Add(popupOverlay);

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities_3857.shp"));
            majorCitiesShapeLayer.Open();
            Collection<Feature> features = majorCitiesShapeLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            foreach (Feature feature in features)
            {
                Popup popup = new Popup(feature.GetShape().GetCenterPoint());
                popup.Content = feature.ColumnValues["AREANAME"];
                popupOverlay.Popups.Add(popup);
            }
            majorCitiesShapeLayer.Close();

            Map1.CurrentExtent = popupOverlay.GetBoundingBox();
            Map1.Refresh();
        }
    }
}