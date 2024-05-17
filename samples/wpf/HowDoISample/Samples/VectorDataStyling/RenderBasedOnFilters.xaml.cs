using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render certain features using a FilterStyle.
    /// </summary>
    public partial class RenderBasedOnFilters : IDisposable
    {
        public RenderBasedOnFilters()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and style the Frisco Crime layer
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10780196.9469504, 3916119.49665258, -10776231.7761301, 3912703.71697007);

            // Project the layer's data to match the projection of the map
            var friscoCrimeLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Crime.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add friscoCrimeLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrimeLayer);

            AddFilterStyle(friscoCrimeLayer);

            // Add layerOverlay to the mapView
            MapView.Overlays.Add(layerOverlay);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Adds a filter style to various categories of the Frisco Crime layer
        /// </summary>
        private static void AddFilterStyle(FeatureLayer layer)
        {
            // Create a filter style based on the "Drugs" Offense Group 
            var drugFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Drugs") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"./Resources/drugs_icon.png")) { ImageScale = .60 }
                }
            };

            // Create a filter style based on the "Weapons" Offense Group 
            var weaponFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Weapons") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"./Resources/weapon_icon.png")) { ImageScale = .25 }
                }
            };

            // Create a filter style based on the "Vandalism" Offense Group 
            var vandalismFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Vandalism") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"./Resources/vandalism_icon.png")) { ImageScale = .25 }
                }
            };

            // Add the filter styles to the CustomStyles collection
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(drugFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(weaponFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(vandalismFilterStyle);
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