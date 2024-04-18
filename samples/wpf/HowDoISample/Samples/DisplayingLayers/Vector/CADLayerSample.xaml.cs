using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.DisplayingLayers.Vector
{
    /// <summary>
    /// Learn how to display a Shapefile Layer on the map
    /// </summary>
    public partial class CADLayerSample : IDisposable
    {
        private CadFeatureLayer _cadLayer;

        public CADLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the shapefile layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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
            MapView.CurrentExtent = _cadLayer.GetBoundingBox();

            await MapView.RefreshAsync();

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private async void rbtnEmbeddedStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            await MapView.RefreshAsync();
        }

        private async void rbtnProgrammaticStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.StandardStyling;
            _cadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.Blue, 2));
            _cadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            await MapView.RefreshAsync();
        }
    }
}