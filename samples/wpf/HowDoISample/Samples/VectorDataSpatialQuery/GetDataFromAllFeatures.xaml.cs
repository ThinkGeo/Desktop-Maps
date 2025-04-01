using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get data from all features in a ShapeFile
    /// </summary>
    public partial class GetDataFromAllFeatures : IDisposable
    {
        public GetDataFromAllFeatures()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco hotels data
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

                // Create a feature layer to hold the Frisco hotels data
                var hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

                // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                var projectionConverter = new ProjectionConverter(2276, 3857);
                hotelsLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Add a style to use to draw the Frisco hotel points
                hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                hotelsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);

                var highlightedHotelLayer = new InMemoryFeatureLayer();
                highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 30, GeoBrushes.BrightYellow, GeoPens.Black);

                // Add the feature layer to an overlay, and add the overlay to the map
                var hotelsOverlay = new LayerOverlay();
                hotelsOverlay.Layers.Add("Frisco Hotels", hotelsLayer);
                hotelsOverlay.Layers.Add("Highlighted Hotel", highlightedHotelLayer);
                MapView.Overlays.Add(hotelsOverlay);

                // Open the hotels layer, so we can read the data from it
                hotelsLayer.Open();

                // Get all features from the hotels layer
                // ReturningColumnsType.AllColumns will return all attributes for the features
                var features = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

                // Create a collection of Hotel objects to use as the data source for our list box
                var hotels = new Collection<Hotel>();

                // Create a hotel object based on the data from each hotel feature, and add them to the collection
                foreach (var feature in features)
                {
                    var name = feature.ColumnValues["NAME"];
                    var address = feature.ColumnValues["ADDRESS"];
                    var rooms = int.Parse(feature.ColumnValues["ROOMS"]);
                    var location = (PointShape)feature.GetShape();

                    hotels.Add(new Hotel(name, address, rooms, location));
                }

                // Set the hotel collection as the data source of the list box
                LsbHotels.ItemsSource = hotels;

                // Set the map extent to the extent of the hotel features
                var hotelsLayerBBox = hotelsLayer.GetBoundingBox();
                MapView.CenterPoint = hotelsLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(hotelsLayerBBox, MapView.ActualWidth, MapView.MapUnit);
                hotelsLayer.Close();

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// When a hotel is selected in the UI, center the map on it
        /// </summary>
        private async void LsbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var highlightedHotelLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("Highlighted Hotel");
                highlightedHotelLayer.Open();
                highlightedHotelLayer.InternalFeatures.Clear();

                // Get the selected location
                if (LsbHotels.SelectedItem is Hotel hotel)
                {
                    highlightedHotelLayer.InternalFeatures.Add(new Feature(hotel.Location));

                    // Center the map on the chosen location
                    var hotelLayerBBox = hotel.Location.GetBoundingBox();
                    MapView.CenterPoint = hotelLayerBBox.GetCenterPoint();
                    MapView.CurrentScale = MapUtil.GetScale(hotelLayerBBox, MapView.ActualWidth, MapView.MapUnit);
                    var standardZoomLevelSet = new ZoomLevelSet();
                    await MapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);
                    await MapView.RefreshAsync();
                }

                highlightedHotelLayer.Close();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}