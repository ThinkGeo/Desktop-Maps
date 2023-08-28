using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Learn how to get data from a feature in a ShapeFile
    /// </summary>
    public partial class GetDataFromFeatureSample : UserControl, IDisposable
    {
        public GetDataFromFeatureSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco parks data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold the Frisco parks data
            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco parks polygons
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Add the feature layer to an overlay, and add the overlay to the map
            LayerOverlay parksOverlay = new LayerOverlay();
            parksOverlay.Layers.Add("Frisco Parks", parksLayer);
            mapView.Overlays.Add(parksOverlay);

            // Add a PopupOverlay to the map, to display feature information
            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add("Info Popup Overlay", popupOverlay);

            // Set the map extent to the bounding box of the parks
            parksLayer.Open();
            mapView.CurrentExtent = parksLayer.GetBoundingBox();
            await mapView.ZoomInAsync();
            parksLayer.Close();

            // Refresh and redraw the map
            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Get a feature based on a location
        /// </summary>
        private Feature GetFeatureFromLocation(PointShape location)
        {
            // Get the parks layer from the MapView
            FeatureLayer parksLayer = mapView.FindFeatureLayer("Frisco Parks");

            // Find the feature that was clicked on by querying the layer for features containing the clicked coordinates
            parksLayer.Open();
            Feature selectedFeature = parksLayer.QueryTools.GetFeaturesContaining(location, ReturningColumnsType.AllColumns).FirstOrDefault();
            parksLayer.Close();

            return selectedFeature;
        }

        /// <summary>
        /// Display a popup containing a feature's info
        /// </summary>
        private async Task DisplayFeatureInfoAsync(Feature feature)
        {
            StringBuilder parkInfoString = new StringBuilder();

            // Each column in a feature is a data attribute
            // Add all attribute pairs to the info string
            foreach (var column in feature.ColumnValues)
            {
                parkInfoString.AppendLine(String.Format("{0}: {1}", column.Key, column.Value));
            }

            // Create a new popup with the park info string
            PopupOverlay popupOverlay = (PopupOverlay)mapView.Overlays["Info Popup Overlay"];
            Popup popup = new Popup(feature.GetShape().GetCenterPoint());
            popup.Content = parkInfoString.ToString();
            popup.FontSize = 10d;
            popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");

            // Clear the popup overlay and add the new popup to it
            popupOverlay.Popups.Clear();
            popupOverlay.Popups.Add(popup);

            // Refresh the overlay to redraw the popups
            await popupOverlay.RefreshAsync();
        }

        /// <summary>
        /// Pull data from the selected feature and display it when clicked
        /// </summary>
        private async void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Get the selected feature based on the map click location
            Feature selectedFeature = GetFeatureFromLocation(e.WorldLocation);

            // If a feature was selected, get the data from it and display it
            if (selectedFeature != null)
            {
                await DisplayFeatureInfoAsync(selectedFeature);
            }
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
