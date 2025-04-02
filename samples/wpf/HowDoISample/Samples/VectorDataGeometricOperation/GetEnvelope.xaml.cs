using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to get the envelope of a shape
    /// </summary>
    public partial class GetEnvelope : IDisposable
    {
        public GetEnvelope()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and envelopeLayer layers
        /// into a grouped LayerOverlay and display them on the map.
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

            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
            var envelopeLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the envelopeLayer
            envelopeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            envelopeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add envelopeLayer to the layerOverlay
            layerOverlay.Layers.Add("envelopeLayer", envelopeLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            var cityLimitsBBox = cityLimits.GetBoundingBox();
            MapView.CenterPoint = cityLimitsBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(cityLimitsBBox, MapView.ActualWidth, MapView.MapUnit);
            cityLimits.Close();

            // Add LayerOverlay to Map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Gets the Envelope of the first feature in the cityLimits layer and adds them to the envelopeLayer to display on the map
        /// </summary>
        private void ShapeEnvelope_OnClick(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];

            var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            var envelopeLayer = (InMemoryFeatureLayer)layerOverlay.Layers["envelopeLayer"];

            // Query the cityLimits layer to get the first feature
            cityLimits.Open();
            var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            cityLimits.Close();

            // Get the bounding box (or envelope) of the feature
            var envelope = feature.GetBoundingBox();

            // Add the envelope shape into an InMemoryFeatureLayer to display the result.
            envelopeLayer.InternalFeatures.Clear();
            envelopeLayer.InternalFeatures.Add(new Feature(envelope));

            // Redraw the layerOverlay to see the envelope feature on the map
            _ = layerOverlay.RefreshAsync();
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