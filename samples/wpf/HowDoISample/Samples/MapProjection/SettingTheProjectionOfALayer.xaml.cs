using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to automatically reproject a layer using the ProjectionConverter class
    /// </summary>
    public partial class SettingTheProjectionOfALayer : IDisposable
    {

        private bool _initialized;
        public SettingTheProjectionOfALayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we can add feature layers to, and add it to the Map
            var subdivisionsOverlay = new LayerOverlay();
            subdivisionsOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add("Frisco Subdivisions Overlay", subdivisionsOverlay);

            // Reproject a shapefile and set the extent
            _ = ReprojectFeaturesFromShapefileAsync();
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject features in a ShapeFileFeatureLayer
        /// </summary>
        private async Task ReprojectFeaturesFromShapefileAsync()
        {
            // Create a feature layer to hold the Frisco subdivisions data
            var subdivisionsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Subdivisions.shp");

            // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(2276, 3857);
            subdivisionsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style for drawing Frisco subdivisions polygons
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply the styles across all zoom levels
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Get the overlay we prepared from the Map, and add the subdivisions ShapeFileFeatureLayer to it
            var subdivisionsOverlay = (LayerOverlay)Map.Overlays["Frisco Subdivisions Overlay"];
            subdivisionsOverlay.Layers.Clear();
            subdivisionsOverlay.Layers.Add("Frisco Subdivisions", subdivisionsLayer);

            // Set the map to the extent of the subdivisions features and refresh the map
            subdivisionsLayer.Open();
            var subdivisionsLayerBBox = subdivisionsLayer.GetBoundingBox();
            Map.CenterPoint = subdivisionsLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, subdivisionsLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.
            subdivisionsLayer.Close();

            await Map.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}