/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
using System;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ArcGISServerRestLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(backgroundOverlay);

            // Creates a new ArcGISServerRestLayer.  This will be used to show counties based on population.
            // documentation for ArcGIS Server REST API can be found at:  http://resources.arcgis.com/en/help/arcgis-rest-api/ , V 9.3 : http://resources.esri.com/help/9.3/arcgisserver/apis/rest/index-9-3.html
            ThinkGeo.MapSuite.Layers.ArcGISServerRestLayer arcGisLayer = new ThinkGeo.MapSuite.Layers.ArcGISServerRestLayer();

            // Specifies the uri of the web service.
            arcGisLayer.ServerUri = new Uri("http://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Specialty/ESRI_StateCityHighway_USA/MapServer/export");

            // Sets our parameters for bounding box spatial renference.
            arcGisLayer.Parameters.Add("bboxSR", "3857");

            // Sets our parameters for image spatial renference.
            arcGisLayer.Parameters.Add("imageSR", "3857");

            // Sets our parameters for image format.
            arcGisLayer.Parameters.Add("format", "png");
            //arcGisLayer.ImageFormat = ArcGISServerRestLayerImageFormat.Png; // we can use the property 'ImageFormat' instead of the 'format' in parameters. When they coexist, the property is perferred.

            // Sets our parameters for transparency.
            arcGisLayer.Parameters.Add("transparent", "true");

            // Specifies the layerId(s) you wish to display.  LayerId 2 is the county layer.
            arcGisLayer.Parameters.Add("layers", "show:2");

            // Specifies the layer and field you wish to query(layerid: field).  in this example, the layerid is 2 and the data field to query is POP1999.
            arcGisLayer.Parameters.Add("layerdefs", string.Format("2:POP1999{0}", txtPopulationFilter.Text));

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("ArcGISServerRestLayer", arcGisLayer);

            wpfMap1.Overlays.Add("LayerOverlay", layerOverlay);

            wpfMap1.CurrentExtent = new RectangleShape(-13086298.60, 7339062.72, -8111177.75, 2853137.62);
            wpfMap1.Refresh();
        }

        private void btnSubmitQuery_Click(object sender, RoutedEventArgs e)
        {
            // Gets our ArcGIS Layer and update the layerdefs parameter.
            var layerOverlay = (LayerOverlay)wpfMap1.Overlays["LayerOverlay"];
            var arcGISLayer = (ThinkGeo.MapSuite.Layers.ArcGISServerRestLayer)layerOverlay.Layers["ArcGISServerRestLayer"];
            arcGISLayer.Parameters["layerdefs"] = string.Format("2:POP1999{0}", txtPopulationFilter.Text);
            wpfMap1.Refresh();
        }
    }
}
