using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Shapefile Layer on the map
    /// </summary>
    public partial class DisplayCADFile : IDisposable
    {
        private CadFeatureLayer _cadLayer;

        public DisplayCADFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the shapefile layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var cadOverlay = new LayerOverlay();
            MapView.Overlays.Add("CAD overlay", cadOverlay);

            // Create the new cad layer
            _cadLayer = new CadFeatureLayer(@"./Data/CAD/Zipcodes.DWG");

            // Create a new ProjectionConverter to convert between Lambert Conformal Conic and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(103376, 3857);
            _cadLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            // Add the layer to the overlay we created earlier.
            cadOverlay.Layers.Add("CAD Drawing", _cadLayer);

            // Set the current extent of the map to the extent of the CAD data
            _cadLayer.Open();
            var cadLayerBBox = _cadLayer.GetBoundingBox();
            MapView.CenterPoint = cadLayerBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(MapView.MapUnit, cadLayerBBox, MapView.MapWidth, MapView.MapHeight);
            MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            _ = MapView.RefreshAsync();
        }

        private void EmbeddedStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            _ = MapView.RefreshAsync();
        }

        private void ProgrammaticStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.StandardStyling;
            _cadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.Blue, 2));
            _cadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

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