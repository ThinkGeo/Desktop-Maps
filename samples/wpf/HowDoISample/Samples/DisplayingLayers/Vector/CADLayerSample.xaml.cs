using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.DisplayingLayers.Vector
{
    /// <summary>
    /// Learn how to display a Shapefile Layer on the map
    /// </summary>
    public partial class CADLayerSample : UserControl, IDisposable
    {
        private CadFeatureLayer cadLayer;

        public CADLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the shapefile layer to the map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay cadOverlay = new LayerOverlay();
            mapView.Overlays.Add("CAD overlay", cadOverlay);

            // Create the new cad layer
            cadLayer = new CadFeatureLayer(@"../../../Data/CAD/Zipcodes.DWG");

            // Create a new ProjectionConverter to convert between Lambert Conformal Conic and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(103376, 3857);
            cadLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            // Add the layer to the overlay we created earlier.
            cadOverlay.Layers.Add("CAD Drawing", cadLayer);

            // Set the current extent of the map to the extent of the CAD data
            cadLayer.Open();
            mapView.CurrentExtent = cadLayer.GetBoundingBox();
            // Refresh the map.
            mapView.Refresh();            

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private void rbtnEmbeddedStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (cadLayer != null)
            {
                // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
                cadLayer.StylingType = CadStylingType.EmbeddedStyling;
                mapView.Refresh();
            }
        }

        private void rbtnProgrammaticStyling_Checked(object sender, RoutedEventArgs e)
        {
            if (cadLayer != null)
            {
                // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
                cadLayer.StylingType = CadStylingType.StandardStyling;
                cadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.Blue, 2));
                cadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                mapView.Refresh();
            }
        }
    }
}
