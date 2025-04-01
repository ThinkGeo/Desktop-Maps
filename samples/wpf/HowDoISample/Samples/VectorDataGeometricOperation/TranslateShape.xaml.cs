using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to translate a shape
    /// </summary>
    public partial class TranslateShape : IDisposable
    {
        public TranslateShape()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and translatedLayer layers
        /// into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;
                ShapeFileFeatureLayer.BuildIndexFile(@"./Data/Shapefile/FriscoCityLimits.shp");

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
                var translatedLayer = new InMemoryFeatureLayer();
                var layerOverlay = new LayerOverlay();

                // Project cityLimits layer to Spherical Mercator to match the map projection
                cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // Style cityLimits layer
                cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
                cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Style translatedLayer
                translatedLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
                translatedLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add cityLimits layer to a LayerOverlay
                layerOverlay.Layers.Add("cityLimits", cityLimits);

                // Add translatedLayer to the layerOverlay
                layerOverlay.Layers.Add("translatedLayer", translatedLayer);

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
        /// Translates the first feature in the cityLimits layer and displays the result on the map.
        /// </summary>
        private async void OffsetTranslateShape_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

                var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
                var translatedLayer = (InMemoryFeatureLayer)layerOverlay.Layers["translatedLayer"];

                // Query the cityLimits layer to get all the features
                cityLimits.Open();
                var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
                cityLimits.Close();

                // Translate the first feature's shape by the X and Y values on the UI in meters
                var translate = BaseShape.TranslateByOffset(features[0].GetShape(), Convert.ToDouble(TranslateX.Text), Convert.ToDouble(TranslateY.Text), GeographyUnit.Meter, DistanceUnit.Meter);

                // Add the translated shape into translatedLayer to display the result.
                // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the
                // underlying data using BeginTransaction and CommitTransaction instead.
                translatedLayer.InternalFeatures.Clear();
                translatedLayer.InternalFeatures.Add(new Feature(translate));

                // Redraw the layerOverlay to see the translated feature on the map
                await layerOverlay.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void DegreeTranslateShape_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

                var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
                var translatedLayer = (InMemoryFeatureLayer)layerOverlay.Layers["translatedLayer"];

                // Query the cityLimits layer to get all the features
                cityLimits.Open();
                var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
                cityLimits.Close();

                // Translate the first feature's shape by the X and Y values on the UI in meters
                var translate = BaseShape.TranslateByDegree(features[0].GetShape(), Convert.ToDouble(TranslateDistance.Text), Convert.ToDouble(TranslateAngle.Text), GeographyUnit.Meter, DistanceUnit.Meter);

                // Add the translated shape into translatedLayer to display the result.
                // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the
                // underlying data using BeginTransaction and CommitTransaction instead.
                translatedLayer.InternalFeatures.Clear();
                translatedLayer.InternalFeatures.Add(new Feature(translate));

                // Redraw the layerOverlay to see the translated feature on the map
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