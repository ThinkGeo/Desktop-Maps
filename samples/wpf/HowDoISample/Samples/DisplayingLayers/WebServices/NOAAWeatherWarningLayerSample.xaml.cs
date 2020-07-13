using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System.Windows.Threading;
using System.Threading;
using System.Collections.ObjectModel;
using System.Text;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class NOAAWeatherWarningLayerSample : UserControl
    {
        public NOAAWeatherWarningLayerSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay noaaWeatherWarningsOverlay = new LayerOverlay();
            mapView.Overlays.Add("Noaa Weather Warning", noaaWeatherWarningsOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            NoaaWeatherWarningsFeatureLayer noaaWeatherWarningsFeatureLayer = new NoaaWeatherWarningsFeatureLayer();
            noaaWeatherWarningsFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Add the new layer to the overlay we created earlier
            noaaWeatherWarningsOverlay.Layers.Add("Noaa Weather Warning",noaaWeatherWarningsFeatureLayer);

            // Get the layers feature source and setup an event that will refresh the map when the data refreshes
            var featureSource = (NoaaWeatherWarningsFeatureSource)noaaWeatherWarningsFeatureLayer.FeatureSource;
            featureSource.WarningsUpdated -= FeatureSource_WarningsUpdated;
            featureSource.WarningsUpdated += FeatureSource_WarningsUpdated;

            featureSource.WarningsUpdating -= FeatureSource_WarningsUpdating;
            featureSource.WarningsUpdating += FeatureSource_WarningsUpdating;
            
            // Create the weather warnings style and add it on zoom level 1 and then apply it to all zoom levels up to 20.
            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            noaaWeatherWarningsFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the extent to a view of the US
            mapView.CurrentExtent = new RectangleShape(-14927495.374917, 8262593.0543992, -6686622.84891633, 1827556.23117885);

            // Add a PopupOverlay to the map, to display feature information
            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add("Info Popup Overlay", popupOverlay);

            // Refresh the map.
            mapView.Refresh();
        }

        private void FeatureSource_WarningsUpdating(object sender, WarningsUpdatingNoaaWeatherWarningsFeatureSourceEventArgs e)
        {
            loadingImage.Dispatcher.Invoke(() => loadingImage.Visibility = Visibility.Visible);            
        }

        private void FeatureSource_WarningsUpdated(object sender, WarningsUpdatedNoaaWeatherWarningsFeatureSourceEventArgs e)
        {
            // This event fires when the the feature source has new data.  We need to make sure we refresh the map
            // on the UI threat so we use the Invoke method on the map using the delegate we created at the top.                        
            loadingImage.Dispatcher.Invoke(() => loadingImage.Visibility = Visibility.Hidden);
            mapView.Dispatcher.Invoke(() => mapView.Refresh(mapView.Overlays["Noaa Weather Warning"]));

        }

        public void InvokeMethod()
        {
            mapView.Refresh(mapView.Overlays["Noaa Weather Warning"]);
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Get the selected feature based on the map click location
            Collection<Feature> selectedFeatures = GetFeaturesFromLocation(e.WorldLocation);

            // If a feature was selected, get the data from it and display it
            if (selectedFeatures != null)
            {
                DisplayFeatureInfo(selectedFeatures);
            }
        }
        private Collection<Feature> GetFeaturesFromLocation(PointShape location)
        {
            // Get the parks layer from the MapView
            FeatureLayer weatherWarnings = mapView.FindFeatureLayer("Noaa Weather Warning");

            // Find the feature that was clicked on by querying the layer for features containing the clicked coordinates            
            Collection<Feature> selectedFeatures = weatherWarnings.QueryTools.GetFeaturesContaining(location, ReturningColumnsType.AllColumns);

            return selectedFeatures;
        }
        private void DisplayFeatureInfo(Collection<Feature> features)
        {
            StringBuilder weatherWarningString = new StringBuilder();

            // Each column in a feature is a data attribute
            // Add all attribute pairs to the info string


            foreach (Feature feature in features)
            {
                weatherWarningString.AppendLine($"{feature.ColumnValues["TITLE"]}");
            }

            // Create a new popup with the park info string
            PopupOverlay popupOverlay = (PopupOverlay)mapView.Overlays["Info Popup Overlay"];
            Popup popup = new Popup(features[0].GetShape().GetCenterPoint());
            popup.Content = weatherWarningString.ToString();
            popup.FontSize = 10d;
            popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");

            // Clear the popup overlay and add the new popup to it
            popupOverlay.Popups.Clear();
            popupOverlay.Popups.Add(popup);

            // Refresh the overlay to redraw the popups
            popupOverlay.Refresh();
        }

    }
}
