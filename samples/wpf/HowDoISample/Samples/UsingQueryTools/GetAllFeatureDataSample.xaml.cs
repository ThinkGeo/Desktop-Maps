using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Learn how to get data from all features in a ShapeFile
    /// </summary>
    public partial class GetAllFeatureDataSample : UserControl, IDisposable
    {
        public GetAllFeatureDataSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco hotels data
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold the Frisco hotels data
            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            hotelsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco hotel points
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            hotelsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);

            InMemoryFeatureLayer highlightedHotelLayer = new InMemoryFeatureLayer();
            highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 30, GeoBrushes.BrightYellow, GeoPens.Black);

            // Add the feature layer to an overlay, and add the overlay to the map
            LayerOverlay hotelsOverlay = new LayerOverlay();
            hotelsOverlay.Layers.Add("Frisco Hotels", hotelsLayer);
            hotelsOverlay.Layers.Add("Highlighted Hotel", highlightedHotelLayer);
            mapView.Overlays.Add(hotelsOverlay);

            // Open the hotels layer so we can read the data from it
            hotelsLayer.Open();

            // Get all features from the hotels layer
            // ReturningColumnsType.AllColumns will return all attributes for the features
            var features = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

            // Create a collection of Hotel objects to use as the data source for our list box
            Collection<Hotel> hotels = new Collection<Hotel>();

            // Create a hotel object based on the data from each hotel feature, and add them to the collection
            foreach(Feature feature in features)
            {
                string name = feature.ColumnValues["NAME"];
                string address = feature.ColumnValues["ADDRESS"];
                int rooms = int.Parse(feature.ColumnValues["ROOMS"]);
                PointShape location = (PointShape)feature.GetShape();

                hotels.Add(new Hotel(name, address, rooms, location));
            }

            // Set the hotel collection as the data source of the list box
            lsbHotels.ItemsSource = hotels;

            // Set the map extent to the extent of the hotel features
            mapView.CurrentExtent = hotelsLayer.GetBoundingBox();
            hotelsLayer.Close();

            // Refresh and redraw the map
            mapView.Refresh();
        }

        /// <summary>
        /// When a hotel is selected in the UI, center the map on it
        /// </summary>
        private void lsbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InMemoryFeatureLayer highlightedHotelLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Highlighted Hotel");
            highlightedHotelLayer.Open();
            highlightedHotelLayer.InternalFeatures.Clear();

            // Get the selected location
            Hotel hotel = lsbHotels.SelectedItem as Hotel;
            if (hotel != null)
            {
                highlightedHotelLayer.InternalFeatures.Add(new Feature(hotel.Location));

                // Center the map on the chosen location
                mapView.CurrentExtent = hotel.Location.GetBoundingBox();
                ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
                mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel18.Scale);
                mapView.Refresh();
            }

            highlightedHotelLayer.Close();
        }

        /// <summary>
        /// Create a custom 'Hotel' class to use as the data source for our list box
        /// </summary>
        public class Hotel
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public int Rooms { get; set; }
            public PointShape Location { get; set; }

            public Hotel(string name, string address, int rooms, PointShape location)
            {
                Name = name;
                Address = address;
                Rooms = rooms;
                Location = location;
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
