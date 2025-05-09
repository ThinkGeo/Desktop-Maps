using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render polygons with dot densities based on their values.
    /// </summary>
    public partial class DisplayDotDensity : IDisposable
    {
        public DisplayDotDensity()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
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

            // Project the layer's data to match the projection of the map
            var housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Here we use a dot density type based on the number of housing units in the area.
            // It draws 1 sized blue circles with a ratio of one dot per 10 housing units in the data
            var housingUnitsStyle = new DotDensityStyle("H_UNITS", .1, 1, GeoColors.Blue);

            // Add and apply the ClassBreakStyle to the housingUnitsLayer
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(housingUnitsStyle);
            housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add housingUnitsLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);

            // Add layerOverlay to the mapView
            MapView.Overlays.Add(layerOverlay);

            // Set the map extent
            housingUnitsLayer.Open();
            var housingUnitsLayerBBox = housingUnitsLayer.GetBoundingBox();
            MapView.CenterPoint = housingUnitsLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, housingUnitsLayerBBox, MapView.MapWidth, MapView.MapHeight);
            housingUnitsLayer.Close();

            _ = MapView.RefreshAsync();
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