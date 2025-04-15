using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// TODO: This sample is a Work In Progress and is disabled in the app!
    /// Learn how to snap a shape to a nearby shape for precise placement.
    /// </summary>
    public partial class SnapToShape : IDisposable
    {
        public SnapToShape()
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

            var friscoParks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
            var snapLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;

            // Project friscoParks layer to Spherical Mercator to match the map projection
            friscoParks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoParks layer
            friscoParks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            friscoParks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the splitLayer
            snapLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            snapLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add friscoParks to a LayerOverlay
            layerOverlay.Layers.Add("friscoParks", friscoParks);

            // Add splitLayer to the layerOverlay
            layerOverlay.Layers.Add("snapLayer", snapLayer);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778340, 3915490);
            MapView.CurrentScale = 36110;

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            // Add Toyota Stadium feature to stadiumLayer
            var stadium = new Feature(new PointShape(-10779651.500992451, 3915933.0023557912));
            snapLayer.InternalFeatures.Add(stadium);
        }

        /// <summary>
        /// WIP
        /// </summary>
        private void SnapToShape_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

            var friscoParks = (ShapeFileFeatureLayer)layerOverlay.Layers["friscoParks"];
            var snapLayer = (InMemoryFeatureLayer)layerOverlay.Layers["snapLayer"];
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