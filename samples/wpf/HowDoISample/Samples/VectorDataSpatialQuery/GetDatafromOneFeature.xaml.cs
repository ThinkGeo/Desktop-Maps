using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get data from a feature in a ShapeFile
    /// </summary>
    public partial class GetDatafromOneFeature : IDisposable
    {
        public GetDatafromOneFeature()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco parks data
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the Map Unit to meters (used in Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create a feature layer to hold the Frisco parks data
                var parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

                // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                var projectionConverter = new ProjectionConverter(2276, 3857);
                parksLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Add a style to use to draw the Frisco parks polygons
                parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Add the feature layer to an overlay, and add the overlay to the map
                var parksOverlay = new LayerOverlay();
                parksOverlay.Layers.Add("Frisco Parks", parksLayer);
                MapView.Overlays.Add(parksOverlay);

                // Add a PopupOverlay to the map, to display feature information
                var popupOverlay = new PopupOverlay();
                MapView.Overlays.Add("Info Popup Overlay", popupOverlay);

                // Set the map extent to the bounding box of the parks
                parksLayer.Open();
                var parksLayerBBox = parksLayer.GetBoundingBox();
                MapView.CenterPoint = parksLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(parksLayerBBox, MapView.ActualWidth, MapView.MapUnit);
                await MapView.ZoomInAsync();
                parksLayer.Close();

                // Refresh and redraw the map
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Get a feature based on a location
        /// </summary>
        private Feature GetFeatureFromLocation(BaseShape location)
        {
            // Get the parks layer from the MapView
            var parksLayer = MapView.FindFeatureLayer("Frisco Parks");

            // Find the feature that was clicked on by querying the layer for features containing the clicked coordinates
            parksLayer.Open();
            var selectedFeature = parksLayer.QueryTools.GetFeaturesContaining(location, ReturningColumnsType.AllColumns).FirstOrDefault();
            parksLayer.Close();

            return selectedFeature;
        }

        /// <summary>
        /// Display a popup containing a feature's info
        /// </summary>
        private async Task DisplayFeatureInfoAsync(Feature feature)
        {
            var parkInfoString = new StringBuilder();

            // Each column in a feature is a data attribute
            // Add all attribute pairs to the info string
            foreach (var column in feature.ColumnValues)
            {
                parkInfoString.AppendLine($"{column.Key}: {column.Value}");
            }

            // Create a new popup with the park info string
            var popupOverlay = (PopupOverlay)MapView.Overlays["Info Popup Overlay"];
            var popup = new Popup(feature.GetShape().GetCenterPoint())
            {
                Content = parkInfoString.ToString(),
                FontSize = 10d,
                FontFamily = new System.Windows.Media.FontFamily("Verdana")
            };

            // Clear the popup overlay and add the new popup to it
            popupOverlay.Popups.Clear();
            popupOverlay.Popups.Add(popup);

            // Refresh the overlay to redraw the popups
            await popupOverlay.RefreshAsync();
        }

        /// <summary>
        /// Pull data from the selected feature and display it when clicked
        /// </summary>
        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Get the selected feature based on the map click location
            var selectedFeature = GetFeatureFromLocation(e.WorldLocation);

            // If a feature was selected, get the data from it and display it
            if (selectedFeature != null)
            {
                _ = DisplayFeatureInfoAsync(selectedFeature);
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}