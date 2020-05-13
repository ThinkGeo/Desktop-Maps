/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace GetFeatureClickedOnWithProjection
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
            Proj4Projection proj4 = new Proj4Projection(4326, 3857);

            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            //Sets the extent of the map converting from 4326 to 3857 projection
            RectangleShape currentExtentWGS84 = new RectangleShape(-97.0015, 32.8315, -96.9796, 32.8179);
            proj4.Open();
            wpfMap1.CurrentExtent = proj4.ConvertToExternalProjection(currentExtentWGS84);
            proj4.Close();

            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Aerial };
            wpfMap1.Overlays.Add(baseOverlay);


            ShapeFileFeatureLayer buildingsLayer = new ShapeFileFeatureLayer(@"..\..\data\buildings.shp");
            buildingsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Silver, GeoColor.StandardColors.Black);
            buildingsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            //Sets projection so that natively WGS84 layer will be displayed in Google Map projection.
            buildingsLayer.FeatureSource.Projection = proj4;

            ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(@"..\..\data\streets.shp");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.LocalRoad1;
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            //Sets projection so that natively WGS84 layer will be displayed in Google Map projection.
            streetsLayer.FeatureSource.Projection = proj4;

            ShapeFileFeatureLayer poisLayer = new ShapeFileFeatureLayer(@"..\..\data\pois.shp");
            poisLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.StandardColors.LightGreen, 12, GeoColor.StandardColors.Black, 1);
            poisLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            //Sets projection so that natively WGS84 layer will be displayed in Google Map projection.
            poisLayer.FeatureSource.Projection = proj4;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("Buildings", buildingsLayer);
            layerOverlay.Layers.Add("Streets", streetsLayer);
            layerOverlay.Layers.Add("Pois", poisLayer);

            wpfMap1.Overlays.Add("Static Overlay", layerOverlay);

            //InMemoryFeature to show the selected feature (the feature clicked on).
            InMemoryFeatureLayer selectLayer = new InMemoryFeatureLayer();
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromArgb(150, GeoColor.StandardColors.Red), 12,
                                                                     GeoColor.StandardColors.Brown, 2);
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(150, GeoColor.StandardColors.Red), 10, true);
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(150, GeoColor.StandardColors.Red));
            selectLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay selectOverlay = new LayerOverlay();
            selectOverlay.Layers.Add("SelectLayer", selectLayer);
            wpfMap1.Overlays.Add("SelectOverlay", selectOverlay);

        }

        private void wpfMap1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            //Notice that in this function to get the features the user clicked on, we do not have to worry about the native projection of the layers.
            //All the logic is done without 

            //Here we use a buffer of 15 in screen coordinate. This means that regardless of the zoom level, we will always find the nearest feature
            //within 15 pixels to where we click.
            int screenBuffer = 15;

            //Logic for converting screen coordinate values to world coordinate for the spatial query. Notice that the distance buffer for the spatial query
            //will change according to the zoom level while the screen buffer distance is constant.
            ScreenPointF clickedPointF = new ScreenPointF(e.ScreenX, e.ScreenY);
            ScreenPointF bufferPointF = new ScreenPointF(clickedPointF.X + screenBuffer, clickedPointF.Y);

            double distanceBuffer = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(wpfMap1.CurrentExtent, clickedPointF, bufferPointF,
                                (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight, wpfMap1.MapUnit, DistanceUnit.Meter);

            //We Add the features clicked on for each layer to the selected layer to be displayed as highlighed.
            InMemoryFeatureLayer selectLayer = (InMemoryFeatureLayer)wpfMap1.FindFeatureLayer("SelectLayer");
            selectLayer.InternalFeatures.Clear();

            //We loop thru all the layers
            LayerOverlay layerOverlay = (LayerOverlay)wpfMap1.Overlays["Static Overlay"];
            foreach (FeatureLayer featureLayer in layerOverlay.Layers)
            {
                if (featureLayer.GetType() == typeof(ShapeFileFeatureLayer))
                {
                    //We get the features that are within the tolerance distance in world coordinate.
                    Collection<Feature> features = featureLayer.FeatureSource.GetFeaturesWithinDistanceOf(new PointShape(e.WorldX, e.WorldY), wpfMap1.MapUnit,
                                                                                                   DistanceUnit.Meter, distanceBuffer, ReturningColumnsType.NoColumns);

                    //We check to see which is the closest feature to the clicked point among the features within the tolerance.
                    if (features.Count > 0)
                    {
                        double[] dists;
                        dists = new double[features.Count];
                        int currentIndex = 0;
                        BaseShape baseShape = (BaseShape)features[0].GetShape();
                        double minDist = baseShape.GetDistanceTo(new PointShape(e.WorldX, e.WorldY), wpfMap1.MapUnit, DistanceUnit.Meter);

                        for (int i = 1; i <= features.Count - 1; i += 1)
                        {
                            baseShape = (BaseShape)features[i].GetShape();
                            double currentDist = baseShape.GetDistanceTo(new PointShape(e.WorldX, e.WorldY), wpfMap1.MapUnit, DistanceUnit.Meter);
                            if (currentDist < minDist)
                            {
                                currentIndex = i; minDist = currentDist;
                            }
                        }
                        selectLayer.InternalFeatures.Add(features[currentIndex]);
                    }
                }
            }
            //Refreshes only the select layer.
            wpfMap1.Refresh(wpfMap1.Overlays["SelectOverlay"]);
            //---------------------------------------------------------------------------------------------------------------------------------------
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.MouseDevice.GetPosition(null);
            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = "X: " + Math.Round(pointShape.X) + "  Y: " + Math.Round(pointShape.Y);
        }
    }
}
