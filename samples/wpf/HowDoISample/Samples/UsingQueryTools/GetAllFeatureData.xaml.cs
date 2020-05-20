using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingQueryTools
{
    /// <summary>
    /// Interaction logic for GetAllFeatureData.xaml
    /// </summary>
    public partial class GetAllFeatureData : UserControl
    {
        public GetAllFeatureData()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoPOI/Hotels.shp");
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            hotelsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.BrightBlue, 10);

            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            hotelsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            LayerOverlay hotealsOverlay = new LayerOverlay();
            hotealsOverlay.Layers.Add("Frisco Hotels", hotelsLayer);
            hotealsOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add(hotealsOverlay);

            hotelsLayer.Open();
            RectangleShape hotelsBoundingBox = hotelsLayer.GetBoundingBox();
            hotelsLayer.Close();

            mapView.CurrentExtent = hotelsBoundingBox;

            mapView.Refresh();
        }
    }
}
