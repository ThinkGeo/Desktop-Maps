using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.DisplayingLayers.Vector
{
    /// <summary>
    /// Interaction logic for ShapefileLayer.xaml
    /// </summary>
    public partial class ShapefileLayer : UserControl
    {
        public ShapefileLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoPOI/Parks.shp");
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Purple, GeoColor.FromArgb(100, GeoColors.Green));

            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = projectionConverter;

            LayerOverlay parksOverlay = new LayerOverlay();
            parksOverlay.Layers.Add("Frisco Parks", parksLayer);
            parksOverlay.TileType = TileType.MultiTile;
            Map.Overlays.Add(parksOverlay);

            parksLayer.Open();
            RectangleShape parksBoundingBox = parksLayer.GetBoundingBox();
            parksLayer.Close();

            Map.CurrentExtent = parksBoundingBox;

            Map.Refresh();
        }
    }
}
