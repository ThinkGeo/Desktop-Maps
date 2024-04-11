using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get part of a line from an existing line
    /// </summary>
    public partial class GetLineOnALineSample : IDisposable
    {
        public GetLineOnALineSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the railway and subLineLayer layers into a
        /// grouped LayerOverlay and display them on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            InMemoryFeatureLayer railway = new InMemoryFeatureLayer();
            InMemoryFeatureLayer subLineLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Add the rail line feature to the railway layer
            railway.InternalFeatures.Add(new Feature("LineString (-10776730.91861553490161896 3925750.69222266925498843, -10778989.31895966082811356 3915278.00731692276895046, -10781766.12723691388964653 3909228.15506267035380006, -10782065.98029803484678268 3907458.59967381786555052, -10781867.48601813986897469 3905465.21030976390466094)"));

            // Style railway layer
            railway.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 2, false);
            railway.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the subLineLayer
            subLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 2, false);
            subLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add railway to the layerOverlay
            layerOverlay.Layers.Add("railway", railway);

            // Add subLineLayer to the layerOverlay
            layerOverlay.Layers.Add("subLineLayer", subLineLayer);

            // Set the map extent to the railway layer bounding box
            railway.Open();
            MapView.CurrentExtent = railway.GetBoundingBox();
            railway.Close();

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Get a subLine of a line and displays it on the map
        /// </summary>
        private async void GetSubLine_OnClick(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

            InMemoryFeatureLayer railway = (InMemoryFeatureLayer)layerOverlay.Layers["railway"];
            InMemoryFeatureLayer subLineLayer = (InMemoryFeatureLayer)layerOverlay.Layers["subLineLayer"];

            // Query the railway layer to get all the features
            railway.Open();
            var feature = railway.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            railway.Close();

            // Get the subLine from the railway line shape
            var subLine = ((LineShape)feature.GetShape()).GetLineOnALine(StartingPoint.FirstPoint, Convert.ToDouble(startingOffset.Text), Convert.ToDouble(distance.Text), GeographyUnit.Meter,
                DistanceUnit.Meter);

            // Add the subLine into an InMemoryFeatureLayer to display the result.
            subLineLayer.InternalFeatures.Clear();
            subLineLayer.InternalFeatures.Add(new Feature(subLine));

            // Redraw the layerOverlay to see the subLine on the map
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