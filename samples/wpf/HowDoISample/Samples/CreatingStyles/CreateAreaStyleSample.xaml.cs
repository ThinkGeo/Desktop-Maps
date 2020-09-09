using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to style polygon data using an AreaStyle
    /// </summary>
    public partial class CreateAreaStyleSample : UserControl, IDisposable
    {                
        public CreateAreaStyleSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            ShapeFileFeatureLayer friscoSubdivisions;

            // Create a layer with polygon data
            friscoSubdivisions = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Parks.shp");
            
            // Project the layer's data to match the projection of the map
            friscoSubdivisions.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            
            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoSubdivisions);
            
            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);
            
            // Add the area style to the historicSites layer
            AddAreaStyle(friscoSubdivisions);
        }

        /// <summary>
        /// Create a areaStyle and add it to the Historic Sites layer
        /// </summary>
        private void AddAreaStyle(ShapeFileFeatureLayer layer)
        {
            // Create a area style
            var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(new GeoColor(128, GeoColors.ForestGreen)));

            // Add the area style to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
