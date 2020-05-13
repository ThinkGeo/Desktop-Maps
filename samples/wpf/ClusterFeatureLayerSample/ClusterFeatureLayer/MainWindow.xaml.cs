/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ClusterFeatureLayerSample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            map.CurrentExtent = new RectangleShape(-13568844, 7538931, -8637835, 2737088);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            map.Overlays.Add(baseOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\AppData\USStates.shp");
            shapeFileFeatureLayer.Open();

            var allFeatures = shapeFileFeatureLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            var clusterColumns = new FeatureSourceColumn[] { new FeatureSourceColumn (){ColumnName="AREA",TypeName="Number" },
                new FeatureSourceColumn() { ColumnName = "SUB_REGION", TypeName = "String" },
                new FeatureSourceColumn() { ColumnName = "POP1990", TypeName = "Number" } ,
                new FeatureSourceColumn() { ColumnName = "MALES", TypeName = "Number" } ,
                new FeatureSourceColumn() { ColumnName = "FEMALES", TypeName = "Number" } };
            ClusterFeatureLayer clusterFeatureLayer = new ClusterFeatureLayer(clusterColumns, allFeatures);
            layerOverlay.Layers.Add(clusterFeatureLayer);
            map.Overlays.Add(layerOverlay);

            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Width = 210;
            legendAdornmentLayer.Height = 120;
            GeoImage legendImage = clusterFeatureLayer.GetLegendImage();
            LegendItem legendItem = new LegendItem { ImageStyle = new PointStyle(legendImage) };
            legendItem.ImageLeftPadding = legendImage.Width / 2;
            legendItem.ImageTopPadding = legendImage.Height / 2;

            legendAdornmentLayer.LegendItems.Add(legendItem);
            map.AdornmentOverlay.Layers.Add(legendAdornmentLayer);

            map.Refresh();
        }
    }
}
