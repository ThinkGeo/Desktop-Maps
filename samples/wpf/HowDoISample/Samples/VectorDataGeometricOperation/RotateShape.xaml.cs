using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to rotate a shape
    /// </summary>
    public partial class RotateShape : IDisposable
    {
        public RotateShape()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and rotatedLayer layers into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
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

                var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
                var rotatedLayer = new InMemoryFeatureLayer();
                var layerOverlay = new LayerOverlay();

                // Project cityLimits layer to Spherical Mercator to match the map projection
                cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Style cityLimits layer
                cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
                cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Style rotatedLayer
                rotatedLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
                rotatedLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add cityLimits layer to a LayerOverlay
                layerOverlay.Layers.Add("cityLimits", cityLimits);

                // Add rotatedLayer to the layerOverlay
                layerOverlay.Layers.Add("rotatedLayer", rotatedLayer);

                // Set the map extent to the cityLimits layer bounding box
                cityLimits.Open();
                var cityLimitsBBox = cityLimits.GetBoundingBox();
                MapView.CenterPoint = cityLimitsBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(cityLimitsBBox, MapView.ActualWidth, MapView.MapUnit);
                cityLimits.Close();

                // Add LayerOverlay to Map
                MapView.Overlays.Add("layerOverlay", layerOverlay);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Rotates the first feature in the cityLimits layer and displays the result on the map.
        /// </summary>
        private async void RotateShape_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

                var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
                var rotatedLayer = (InMemoryFeatureLayer)layerOverlay.Layers["rotatedLayer"];

                // Query the cityLimits layer to get all the features
                cityLimits.Open();
                var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
                cityLimits.Close();

                // Rotate the first feature using its center point as the anchor
                var rotate = BaseShape.Rotate(features[0], features[0].GetShape().GetCenterPoint(), Convert.ToSingle(RotateDegree.Text));

                // Add the rotated shape into rotatedLayer to display the result.
                // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
                rotatedLayer.InternalFeatures.Clear();
                rotatedLayer.InternalFeatures.Add(new Feature(rotate));

                // Redraw the layerOverlay to see the rotated feature on the map
                await layerOverlay.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
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