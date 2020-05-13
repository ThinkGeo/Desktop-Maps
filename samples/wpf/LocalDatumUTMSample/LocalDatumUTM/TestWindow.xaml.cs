/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace LocalDatumUTM
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        Proj4Projection proj4Projection;
        public TestWindow()
        {
            InitializeComponent();

            proj4Projection = new Proj4Projection(3857, 4326);
            proj4Projection.Open();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(12245143, -1574216, 16030006, -4579425);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //For the zone 50 Australia
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle
                                         (GeoColor.StandardColors.Transparent, GeoColor.StandardColors.Black);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(new RectangleShape(12690421, -1874311, 13358338, -4191093)));

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(inMemoryFeatureLayer);
            wpfMap1.Overlays.Add(layerOverlay);

            wpfMap1.Refresh();

        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);
            pointShape = (PointShape)proj4Projection.ConvertToExternalProjection(pointShape);

            //---------------------------------------------------------------------------------------------------------------------------------------
            //The first transformation is to go from WSG84, the coordinates of the map to local australian AGD84 datum in geographic coordinates
            //Here we are only dealing with Longitude and Latitude. The purpose of this transformation is to set the coordinate in AGD84 datum from 
            //the coordinate of map (World Map Kit) in WGS84.
            Proj4Projection WGS84toAGD84proj4 = new Proj4Projection();
            WGS84toAGD84proj4.InternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326); //WGS84
            //http://www.spatial-reference.org/ref/epsg/62036405/
            WGS84toAGD84proj4.ExternalProjectionParametersString = "+proj=longlat +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +no_defs";

            WGS84toAGD84proj4.Open();
            Vertex AGD84vertex = WGS84toAGD84proj4.ConvertToExternalProjection(pointShape.X, pointShape.Y);
            WGS84toAGD84proj4.Close();

            string longAGD84 = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(AGD84vertex.X, 2);
            string latAGD84 = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(AGD84vertex.Y, 2);

            lblLongAGD84.Content = "Long.  " + longAGD84;
            lblLatAGD84.Content = "Lat.     " + latAGD84;

            //-------------------------------------------------------------------------------------------------------------------------------------------
            //The second transformation is projection transformation to go from geographic AGD84 to AMG94 australian UTM, both in AGD84 datum.
            //Here we go from Longitude and Latitude to Northings and Eastings.
            Proj4Projection AGD84toAMGproj4 = new Proj4Projection();
            //http://www.spatial-reference.org/ref/epsg/62036405/
            AGD84toAMGproj4.InternalProjectionParametersString = "+proj=longlat +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +no_defs";
            //http://www.spatial-reference.org/ref/epsg/20350/
            AGD84toAMGproj4.ExternalProjectionParametersString = "+proj=utm +zone=50 +south +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +units=m +no_defs";

            AGD84toAMGproj4.Open();
            Vertex AMGvertex = AGD84toAMGproj4.ConvertToExternalProjection(AGD84vertex.X, AGD84vertex.Y);
            AGD84toAMGproj4.Close();

            lblEastingAMG.Content = "Easting " + Math.Round(AMGvertex.X);
            lblNorthingAMG.Content = "Northing " + Math.Round(AMGvertex.Y);

            //---------------------------------------------------------------------------------------------------------------------------------------
            //The third transformation is a datum transformation to go from AGD84 to GDA94 both in geographic coordinate.
            //Here we are dealing only with Longitude and Latitude.
            Proj4Projection AGD84toGDA94proj4 = new Proj4Projection();
            //http://www.spatial-reference.org/ref/epsg/62036405/
            AGD84toGDA94proj4.InternalProjectionParametersString = "+proj=longlat +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +no_defs";
            //http://www.spatial-reference.org/ref/epsg/4939/
            AGD84toGDA94proj4.ExternalProjectionParametersString = "+proj=longlat +ellps=GRS80 +towgs84=0.0,0.0,0.0,0.0,0.0,0.0,0.0 +no_defs";


            AGD84toGDA94proj4.Open();
            Vertex GDA94vertex = AGD84toGDA94proj4.ConvertToExternalProjection(AGD84vertex.X, AGD84vertex.Y);
            AGD84toGDA94proj4.Close();

            string GDA94long = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(GDA94vertex.X, 2);
            string GDA94lat = DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(GDA94vertex.Y, 2);

            lblLongGDA94.Content = GDA94long;
            lblLatGDA94.Content = GDA94lat;

            //Shows the corrections in Degrees from AGD84 to GDA94 for longitude and latitude.
            double longCorrection = GDA94vertex.X - AGD84vertex.X;
            lblLongCorrection.Content = "+  " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(longCorrection, 2) + "   -->";
            double latCorrection = GDA94vertex.Y - AGD84vertex.Y;
            lblLatCorrection.Content = "+  " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(latCorrection, 2) + "   -->";

            //---------------------------------------------------------------------------------------------------------------------------------------
            //The fourth transformation is a datum transformation to go from the old AMG84 (using AGD84 datum) to the new MGA94 (using GDA94 datum). 
            //We stay in the UTM projection but we change datum from old AGD84 to new GDA94.
            //Here we are dealing only with Northings and Eastings.
            Proj4Projection AMG84toMGA94proj4 = new Proj4Projection();
            //http://www.spatial-reference.org/ref/epsg/20350/
            AMG84toMGA94proj4.InternalProjectionParametersString = "+proj=utm +zone=50 +south +ellps=aust_SA +towgs84=-117.763,-51.51,139.061,0.292,-0.443,-0.277,-0.03939657799319541 +units=m +no_defs";
            //http://www.spatial-reference.org/ref/epsg/28350/
            AMG84toMGA94proj4.ExternalProjectionParametersString = "+proj=utm +zone=50 +south +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs";

            AMG84toMGA94proj4.Open();
            Vertex MGA94vertex = AMG84toMGA94proj4.ConvertToExternalProjection(AMGvertex.X, AMGvertex.Y);
            AMG84toMGA94proj4.Close();

            lblEastingMGA.Content = Math.Round(MGA94vertex.X);
            lblNorthingMGA.Content = Math.Round(MGA94vertex.Y);

            //Shows the corrections in meters from AMG to MGA for eastings and northings
            double eastingCorrection = Math.Round(MGA94vertex.X - AMGvertex.X);
            lblEastingCorrection.Content = "+  " + eastingCorrection + "m   -->";
            double northingCorrection = Math.Round(MGA94vertex.Y - AMGvertex.Y);
            lblNorthingCorrection.Content = "+  " + northingCorrection + "m   -->";


        }
    }
}
