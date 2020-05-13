/*===========================================
   Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
   a Client ID and Secret. These were sent to you via email when you signed up
   with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace UseWpfMapControl
{
    public partial class Form1 : Form
    {
        // Define a WpfMap object.   
        private WpfMap map;

        public Form1()
        {
            InitializeComponent();

            // Initialize the WpfMap object and add it to the ElementHost. 
            map = new WpfMap();
            elementHost1.Child = map;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkgeoBackgroundOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeoCloudClientId", "ThinkGeoCloudClientSecret");
            // Set up the TileCache for the background overlay. 
            thinkgeoBackgroundOverlay.TileCache = new XyzFileBitmapTileCache(".\\Cache");
            map.Overlays.Add(thinkgeoBackgroundOverlay);

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\Countries.shp");
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black);
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            shapeFileFeatureLayer.FeatureSource.Projection = new Proj4Projection(Proj4Projection.GetDecimalDegreesParametersString(), Proj4Projection.GetSphericalMercatorParametersString());

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(shapeFileFeatureLayer);
            // Set up the TileCache for LayerOverlay.
            layerOverlay.TileCache = new FileBitmapTileCache(".\\Cache", "LayerOverlay");
            map.Overlays.Add(layerOverlay);

            map.CurrentExtent = new RectangleShape(-19489607.6157655, 12836528.7107853, 19293928.8244426, -8022830.44424083);
        }
    }
}
