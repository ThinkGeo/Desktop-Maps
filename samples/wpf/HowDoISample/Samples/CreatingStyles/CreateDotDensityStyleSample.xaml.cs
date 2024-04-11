using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Dot Density style on the map
    /// </summary>
    public partial class CreateDotDensityStyleSample : IDisposable
    {
        public CreateDotDensityStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
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
            MapView.CurrentExtent = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();
            await MapView.RefreshAsync();
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