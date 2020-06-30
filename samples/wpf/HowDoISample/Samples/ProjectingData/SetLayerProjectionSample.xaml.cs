using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Learn how to automatically reproject a layer using the ProjectionConverter class
    /// </summary>
    public partial class SetLayerProjectionSample : UserControl
    {
        public SetLayerProjectionSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we can add feature layers to, and add it to the MapView
            LayerOverlay subdivisionsOverlay = new LayerOverlay();
            mapView.Overlays.Add("Frisco Subdivisions Overlay", subdivisionsOverlay);

            // Reproject a shapefile and set the extent
            ReprojectFeaturesFromShapefile();
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject features in a ShapeFileFeatureLayer
        /// </summary>
        private void ReprojectFeaturesFromShapefile()
        {
            // Create a feature layer to hold the Frisco subdivisions data
            ShapeFileFeatureLayer subdivisionsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Subdivisions.shp");

            // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            subdivisionsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco subdivions polygons
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply the styles across all zoom levels
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Get the overlay we prepared from the MapView, and add the subdivisions ShapeFileFeatureLayer to it
            LayerOverlay subdivisionsOverlay = (LayerOverlay)mapView.Overlays["Frisco Subdivisions Overlay"];
            subdivisionsOverlay.Layers.Clear();
            subdivisionsOverlay.Layers.Add("Frisco Subdivisions", subdivisionsLayer);

            // Set the map to the extent of the subdivisions features and refresh the map
            subdivisionsLayer.Open();
            mapView.CurrentExtent = subdivisionsLayer.GetBoundingBox();
            subdivisionsLayer.Close();
            mapView.Refresh();
        }
    }
}
