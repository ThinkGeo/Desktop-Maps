using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.ProjectingData
{
    /// <summary>
    /// Interaction logic for SetLayerProjection.xaml
    /// </summary>
    public partial class SetLayerProjection : UserControl
    {
        public SetLayerProjection()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map Unit to feet (used in Texas North Central Projection)
            mapView.MapUnit = GeographyUnit.Feet;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a feature layer to hold the Frisco subdivisions data
            ShapeFileFeatureLayer subdivisionsLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoLandRec/Subdivisions.shp");

            // Add a style to use to draw the Frisco subdivions polygons
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.MediumPurple, GeoColors.Black, 3);

            // Set the map to the extent of the subdivisions features
            subdivisionsLayer.Open();
            RectangleShape subdivisionsBoundingBox = subdivisionsLayer.GetBoundingBox();
            subdivisionsLayer.Close();
            mapView.CurrentExtent = subdivisionsBoundingBox;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay subdivisionsOverlay = new LayerOverlay();
            subdivisionsOverlay.Layers.Add("Frisco Subdivisions", subdivisionsLayer);
            subdivisionsOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add("Frisco Subdivisions Overlay", subdivisionsOverlay);

            // Redraw the map
            mapView.Refresh();
        }

        private void rbMapProjection_Checked(object sender, RoutedEventArgs e)
        {
            // Redraw the map
            mapView.Refresh();
        }
    }
}
