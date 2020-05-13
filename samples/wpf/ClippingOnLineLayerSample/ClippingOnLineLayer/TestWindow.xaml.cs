/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ClippingOnLineLayer
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private ShapeFileFeatureLayer balticCountriesLayer = null;
        private ShapeFileFeatureLayer balticRailsRoadsLayer = null;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit of the map.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Polygon based shapefile used for clipping
            balticCountriesLayer = new ShapeFileFeatureLayer(@"..\..\Data\baltic_countries.shp", GeoFileReadWriteMode.Read);
            balticCountriesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColor.StandardColors.Red), GeoColor.StandardColors.DarkGray, 2);
            balticCountriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Line based shapefile used for creating the clipped shapefile.
            balticRailsRoadsLayer = new ShapeFileFeatureLayer(@"..\..\Data\baltic_rail_roads.shp", GeoFileReadWriteMode.Read);
            balticRailsRoadsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle
                (GeoColor.StandardColors.Red, 2, LineDashStyle.Dash, true);
            balticRailsRoadsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("BalticCountries", balticCountriesLayer);
            layerOverlay.Layers.Add("BalticRailRoads", balticRailsRoadsLayer);
            wpfMap1.Overlays.Add("StaticOverlay", layerOverlay);

            //Adds the InMemoryFeatureLayer with of the polygon of Latvia used for clipping.
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle
                             (GeoColor.StandardColors.Transparent, GeoColor.FromArgb(150, GeoColor.StandardColors.Red), 2);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            balticCountriesLayer.Open();
            Collection<Feature> features = balticCountriesLayer.FeatureSource.GetFeaturesByColumnValue("cntry_name", "Latvia");
            balticCountriesLayer.Close();

            inMemoryFeatureLayer.InternalFeatures.Add(features[0]);

            LayerOverlay dynamicOverlay = new LayerOverlay();
            dynamicOverlay.Layers.Add("Latvia", inMemoryFeatureLayer);
            wpfMap1.Overlays.Add("DynamicOverlay", dynamicOverlay);

            balticRailsRoadsLayer.Open();
            wpfMap1.CurrentExtent = balticRailsRoadsLayer.GetBoundingBox();
            balticRailsRoadsLayer.Close();

            wpfMap1.Refresh();
        }

        private void btnClip_Click(object sender, RoutedEventArgs e)
        {
            //deletes the files for the clipped shapefile if they already exist.
            if (File.Exists(@"..\..\Data\baltic_rail_roads_clip.shp"))
            {
                DeleteClipFiles();
            }

            //Gets the MultipolygonShape of Latvia used for clipping.
            balticCountriesLayer.Open();
            Collection<Feature> features = balticCountriesLayer.FeatureSource.GetFeaturesByColumnValue("cntry_name", "Latvia");
            balticCountriesLayer.Close();
            MultipolygonShape multiPolygonShape = (MultipolygonShape)features[0].GetShape();

            //Calls the ClipLineBasedShapeFile function to get the clipped layer.
            ShapeFileFeatureLayer clipLayer = ClipLineBasedShapeFile(balticRailsRoadsLayer, @"..\..\Data\baltic_rail_roads_clip.shp", multiPolygonShape);

            LayerOverlay layerOverlay = (LayerOverlay)wpfMap1.Overlays["StaticOverlay"];
            layerOverlay.Layers.Remove("BalticRailRoads");

            //Adds the new clipped layer of baltic_rail_roads_clip.
            ShapeFileFeatureLayer.BuildIndexFile(@"..\..\Data\baltic_rail_roads_clip.shp", BuildIndexMode.DoNotRebuild);
            clipLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle
                (GeoColor.StandardColors.Red, 2, LineDashStyle.Dash, true);
            clipLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            layerOverlay.Layers.Add("ClippedBalticRailRoads", clipLayer);

            wpfMap1.Refresh(layerOverlay);
        }

        private ShapeFileFeatureLayer ClipLineBasedShapeFile(ShapeFileFeatureLayer shapeFileFeatureLayer, string outputShapeFileFeatureLayerPath, AreaBaseShape ClippingShape)
        {
            //Gets the columns from the line based shapefile.
            shapeFileFeatureLayer.Open();
            Collection<DbfColumn> dbfColumns = ((ShapeFileFeatureSource)shapeFileFeatureLayer.FeatureSource).GetDbfColumns();

            ShapeFileFeatureLayer.CreateShapeFile(ShapeFileType.Polyline, outputShapeFileFeatureLayerPath, dbfColumns);

            ShapeFileFeatureSource clippedShapeFileFeatureSource = new ShapeFileFeatureSource(outputShapeFileFeatureLayerPath, GeoFileReadWriteMode.ReadWrite);

            //Gets the features with Spatial Query that will be used for the clipping.
            Collection<Feature> features = shapeFileFeatureLayer.QueryTools.GetFeaturesIntersecting(ClippingShape, ReturningColumnsType.AllColumns);
            shapeFileFeatureLayer.Close();

            //Loops thru all the features from the Spatial Query.
            clippedShapeFileFeatureSource.Open();
            clippedShapeFileFeatureSource.BeginTransaction();
            foreach (Feature feature in features)
            {
                //Gets the MultilineShape and clip it using GetIntersection geometric function.
                MultilineShape currentMultilineShape = (MultilineShape)(feature.GetShape());
                MultilineShape clippedMultilineShape = currentMultilineShape.GetIntersection(ClippingShape);

                //Adds the feature to the clipped layer with the clipped MultilineShape with its ColumnValues.
                Dictionary<string, string> Columns = feature.ColumnValues;
                Feature clippedFeature = new Feature(clippedMultilineShape, Columns);
                clippedShapeFileFeatureSource.AddFeature(clippedFeature);
            }

            clippedShapeFileFeatureSource.CommitTransaction();
            clippedShapeFileFeatureSource.Close();

            //Returns the clipped ShapeFileFeatureLayer.
            ShapeFileFeatureLayer clippedShapeFileFeatureLayer = new ShapeFileFeatureLayer(outputShapeFileFeatureLayerPath);
            return clippedShapeFileFeatureLayer;

        }

        private void DeleteClipFiles()
        {
            File.Delete(@"..\..\Data\baltic_rail_roads_clip.shp");
            File.Delete(@"..\..\Data\baltic_rail_roads_clip.ids");
            File.Delete(@"..\..\Data\baltic_rail_roads_clip.idx");
            File.Delete(@"..\..\Data\baltic_rail_roads_clip.dbf");
            File.Delete(@"..\..\Data\baltic_rail_roads_clip.shx");
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";
        }


    }
}
