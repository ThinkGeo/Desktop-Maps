using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an In-Memory Feature Layer on the map
    /// </summary>
    public partial class DisplayInMemoryFile : IDisposable
    {
        public DisplayInMemoryFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the feature layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;

                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Create a new overlay that will hold our new layer and add it to the map.
                var inMemoryOverlay = new LayerOverlay();
                MapView.Overlays.Add(inMemoryOverlay);

                // Create a new layer that we will pull features from to populate the in memory layer.
                var shapeFileLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Mosquitos.shp")
                {
                    FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
                };
                shapeFileLayer.Open();

                // Get all the features from the above layer.
                var features = shapeFileLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
                shapeFileLayer.Close();

                // Create the in memory layer and add it to the map
                var inMemoryFeatureLayer = new InMemoryFeatureLayer();
                inMemoryOverlay.Layers.Add("Frisco Mosquitos", inMemoryFeatureLayer);

                // Loop through all the features in the first layer and add them to the in memory layer.  We use a shortcut called internal 
                // features that is supported in the in memory layer instead of going through the edit tools
                foreach (var feature in features)
                {
                    inMemoryFeatureLayer.InternalFeatures.Add(feature);
                }

                // Create a text style for the label and give it a mask for use below.
                var textStyle = new TextStyle("Trap: [TrapID]", new GeoFont("ariel", 14), GeoBrushes.Black)
                {
                    Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                    MaskMargin = new DrawingMargin(2, 2, 2, 2),
                    YOffsetInPixel = -10
                };

                // Create a point style and add the text style from above on zoom level 1 and then apply it to all zoom levels up to 20.            
                inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Red, GeoPens.White);
                inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
                inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Open the layer and set the map view current extent to the bounding box of the layer.  
                inMemoryFeatureLayer.Open();
                var inMemoryFeatureLayerBBox = inMemoryFeatureLayer.GetBoundingBox();
                MapView.CenterPoint = inMemoryFeatureLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(inMemoryFeatureLayerBBox,MapView.ActualWidth,MapView.MapUnit);

                await MapView.RefreshAsync();
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