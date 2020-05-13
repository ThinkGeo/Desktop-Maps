/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace MagneticDeclination
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MagneticDeclinationAdornmentLayer magneticDeclinationAdornmentLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Create a MagneticDeclinationAdornmentLayer and add to AdornmentOverlay.
            magneticDeclinationAdornmentLayer = new MagneticDeclinationAdornmentLayer() { Location = (AdornmentLocation)(cmbLocation.SelectedIndex + 1) };
            var proj4Projection = new Proj4Projection(3857, 4326);
            proj4Projection.Open();
            magneticDeclinationAdornmentLayer.ProjectionToDecimalDegrees = proj4Projection;
            magneticDeclinationAdornmentLayer.TrueNorthPointStyle.SymbolSize = 25;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.TrueNorthLineStyle.OuterPen.Width = 5f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.InnerPen.Width = 2f;
            magneticDeclinationAdornmentLayer.MagneticNorthLineStyle.OuterPen.Width = 5f;
            wpfMap1.AdornmentOverlay.Layers.Add(magneticDeclinationAdornmentLayer);

            //move map to usa.
            wpfMap1.CurrentExtent = new RectangleShape(-16481046.2907366, 8829563.84619887, -6853649.70388374, 1121097.78508191);

            //add mouse move event handler
            wpfMap1.MouseMove += new MouseEventHandler(wpfMap1_MouseMove);
        }

        private void cmbLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //change magneticDeclinationAdornmentLayer location
            if (magneticDeclinationAdornmentLayer != null)
            {
                magneticDeclinationAdornmentLayer.Location = (AdornmentLocation)(cmbLocation.SelectedIndex + 1);
                magneticDeclinationAdornmentLayer.XOffsetInPixel = magneticDeclinationAdornmentLayer.Location == AdornmentLocation.UpperRight ? -220 : 0;
                wpfMap1.Refresh();
            }
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            wpfMap1.AdornmentOverlay.Refresh();
        }
    }
}