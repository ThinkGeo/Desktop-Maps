using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style polygon data using an FleeBooleanStyle
    /// </summary>
    public partial class CreateAFleeBooleanStyle : IDisposable
    {
        public CreateAFleeBooleanStyle()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a layer with polygon data
            var countries02Layer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp")
            {
                FeatureSource =
                    {
                        // Project the layer's data to match the projection of the map
                        ProjectionConverter = new ProjectionConverter(4326, 3857)
                    }
            };

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countries02Layer);

            // Add the overlay to the map
            MapView.Overlays.Add(layerOverlay);

            // Add the fleeBooleanStyle to the countries02 layer
            AddFleeBooleanStyle(countries02Layer);

            // Set the map extent
            MapView.CenterPoint = MaxExtents.SphericalMercator.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, MaxExtents.SphericalMercator, MapView.MapWidth, MapView.MapHeight);

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Create a fleeBooleanStyle and add it to the countries02 layer
        /// </summary>
        private static void AddFleeBooleanStyle(FeatureLayer layer)
        {
            // Highlight the countries that are land locked and have a population greater than 10 million  
            const string expression = "(POP_CNTRY>10000000 && LANDLOCKED=='Y')";
            var landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this  
            // to access the Convert.Toxxx methods to convert variable types  
            landLockedCountryStyle.StaticTypes.Add(typeof(Convert));

            // The math class might be handy to include but in this sample we do not use it  
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));  

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1), new GeoSolidBrush(GeoColor.FromArgb(205,255,255,0))));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(205, 233, 232, 214), GeoColor.FromArgb(205, 118, 138, 69)));

            // Add the landLockedCountryStyle to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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