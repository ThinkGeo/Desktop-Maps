/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace GraticuleWithGoogleProjection
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the unit of the map to meters because it is in the Google projection (Spherical Mercator)
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Creates the propection object with the internal and external properties.
            //We need to have Geodetic as internal projection and Google map  as external projection
            //because GraticuleAdornmentLayer is natively in Geodetic and we need to have it displayed
            //with WorldMapKit Spherical Mercartor (Google Map projection).
            Proj4Projection proj4 = new Proj4Projection();
            proj4.InternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326);
            proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
            proj4.Open();

            //Creates the GraticuleAdornmentLayer with the Projection property.
            //(Other AdormentLayer that would be added to GraticuleAdornmentLayer are:
            //LogoAdormentLayer, ScaleBarAdornmentLayer and ScaleLineAdornmentLayer)
            GraticuleFeatureLayer graticuleAdornmentLayer = new GraticuleFeatureLayer();
            graticuleAdornmentLayer.Projection = proj4;
            //Set the styles of the GraticuleAdornmentLayer.
            LineStyle graticuleLineStyle = new LineStyle(new GeoPen(GeoColor.FromArgb(150, GeoColor.StandardColors.Navy), 1));
            graticuleAdornmentLayer.GraticuleLineStyle = graticuleLineStyle;
            graticuleAdornmentLayer.GraticuleTextFont = new GeoFont("Times", 12, DrawingFontStyles.Bold);
            //Adds the GraticuleAdormentLayer to the Layers collection of the AdornmentOverlay.
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("graticule", graticuleAdornmentLayer);
            wpfMap1.Overlays.Add(layerOverlay);

            wpfMap1.CurrentExtent = new RectangleShape(-13939426.6371, 6701997.4056, -7812401.86, 2626987.386962);
            wpfMap1.Refresh();
        }

        //Function for getting the extent based on a collection of layers.
        //It gets the overall extent of all the layers.
        private RectangleShape GetFullExtent(GeoCollection<Layer> Layers)
        {
            Collection<BaseShape> rectangleShapes = new Collection<BaseShape>();

            foreach (Layer layer in Layers)
            {
                layer.Open();
                if (layer.HasBoundingBox == true) rectangleShapes.Add(layer.GetBoundingBox());
            }
            return ExtentHelper.GetBoundingBoxOfItems(rectangleShapes);
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = "X: " + Math.Round(pointShape.X) +
                          "  Y: " + Math.Round(pointShape.Y);

        }
    }
}
