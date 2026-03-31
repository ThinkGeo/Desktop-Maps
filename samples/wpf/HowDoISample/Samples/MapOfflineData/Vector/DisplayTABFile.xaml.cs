using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a TAB Layer on the map
    /// </summary>
    public partial class DisplayTABFile : IDisposable
    {

        private bool _initialized;
        public DisplayTABFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the TAB layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var cityBoundaryOverlay = new LayerOverlay();
            cityBoundaryOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add(cityBoundaryOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            var cityBoundaryLayer = new TabFeatureLayer(@"./Data/Tab/City_ETJ.tab")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Add the layer to the overlay we created earlier.
            cityBoundaryOverlay.Layers.Add("City Boundary", cityBoundaryLayer);

            // Set this so we can use our own styles as opposed to the styles in the file.
            cityBoundaryLayer.StylingType = TabStylingType.StandardStyling;

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            cityBoundaryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Green), GeoColors.Green);
            cityBoundaryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            cityBoundaryLayer.Open();
            var cityBoundaryLayerBBox = cityBoundaryLayer.GetBoundingBox();
            Map.CenterPoint = cityBoundaryLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(Map.MapUnit, cityBoundaryLayerBBox, Map.MapWidth, Map.MapHeight);
            Map.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            _ = Map.RefreshAsync();
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