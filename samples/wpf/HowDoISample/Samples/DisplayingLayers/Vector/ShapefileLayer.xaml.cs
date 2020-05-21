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

            LayerOverlay parksOverlay = new LayerOverlay();
            Map.Overlays.Add(parksOverlay);

            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoPOI/Parks.shp");
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.Open();

            var dashedPen = new GeoPen(GeoColors.Green, 5);
            dashedPen.DashPattern.Add(1);
            dashedPen.DashPattern.Add(1);

            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(dashedPen, new GeoSolidBrush(GeoColors.Transparent));

            parksOverlay.Layers.Add("Frisco Parks", parksLayer);

            Map.CurrentExtent = parksLayer.GetBoundingBox();

            Map.Refresh();
        }
    }
}
