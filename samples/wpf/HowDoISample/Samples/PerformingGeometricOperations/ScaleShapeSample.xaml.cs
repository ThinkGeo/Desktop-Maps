using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to scale a shape
    /// </summary>
    public partial class ScaleShapeSample : IDisposable
    {
        public ScaleShapeSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and scaledLayer layers
        /// into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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
            var scaledLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style scaledLayer
            scaledLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            scaledLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits layer to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add scaledLayer to the layerOverlay
            layerOverlay.Layers.Add("scaledLayer", scaledLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            MapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Scales the first feature in the cityLimits layer and displays the result on the map.
        /// </summary>
        private async void ScaleShape_OnClick(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

            var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            var scaledLayer = (InMemoryFeatureLayer)layerOverlay.Layers["scaledLayer"];

            // Query the cityLimits layer to get all the features
            cityLimits.Open();
            var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            cityLimits.Close();

            // Scale the first feature by the scaleFactor TextBox on the UI
            var scale = BaseShape.ScaleTo(features[0].GetShape(), Convert.ToSingle(ScaleFactor.Text));

            // Add the scaled shape into scaledLayer to display the result.
            // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the
            // underlying data using BeginTransaction and CommitTransaction instead.
            scaledLayer.InternalFeatures.Clear();
            scaledLayer.InternalFeatures.Add(new Feature(scale));

            // Redraw the layerOverlay to see the scaled feature on the map
            await layerOverlay.RefreshAsync();
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