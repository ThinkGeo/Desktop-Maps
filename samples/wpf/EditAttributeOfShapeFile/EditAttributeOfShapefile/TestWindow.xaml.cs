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

namespace EditAttributeOfShapefile
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        ShapeFileFeatureLayer editShapefileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\countries02.shp", GeoFileReadWriteMode.ReadWrite);
        String editID = null;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the correct map unit and the extent of the map.
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;
            wpfMap1.CurrentExtent = new RectangleShape(-102, 38, 32, -59);
            wpfMap1.Background = new SolidColorBrush(Color.FromRgb(148, 196, 243));

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\countries02.shp");
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle
                                                 (GeoColor.FromArgb(80, GeoColor.StandardColors.Red), GeoColor.StandardColors.Black);
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Countries ShapefileFeatureLayer for labeling
            ShapeFileFeatureLayer labelShapefileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\Data\countries02.shp");
            //TextStyle for the first line, country name.
            labelShapefileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(TextStyles.CreateSimpleTextStyle("MergedColumn", "Arial", 8,
                DrawingFontStyles.Regular, GeoColor.StandardColors.Black, 10, 12));

            //TextStyle for the second line, country population.
            labelShapefileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(TextStyles.CreateSimpleTextStyle("MergedColumn2", "Arial", 8,
               DrawingFontStyles.Italic, GeoColor.StandardColors.Black, 10, 0));

            labelShapefileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Hooks up the event for CustomColumnFetch used to label the different columns.
            ((ShapeFileFeatureSource)labelShapefileFeatureLayer.FeatureSource).CustomColumnFetch
            += new EventHandler<CustomColumnFetchEventArgs>(labelShapefileFeatureLayer_CustomColumnFetch);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("CountriesLayer", shapeFileFeatureLayer);
            layerOverlay.Layers.Add("LabelCountriesLayer", labelShapefileFeatureLayer);

            wpfMap1.Overlays.Add("StaticOverlay", layerOverlay);

            //InMemoryFeature to show the selected feature (the feature clicked on).
            InMemoryFeatureLayer selectLayer = new InMemoryFeatureLayer();
            selectLayer.Open();
            selectLayer.Columns.Add(new FeatureSourceColumn("POP_CNTRY"));
            selectLayer.Close();
            selectLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.Transparent, GeoColor.StandardColors.Yellow, 2);
            selectLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay selectOverlay = new LayerOverlay();
            selectOverlay.Layers.Add("SelectLayer", selectLayer);
            wpfMap1.Overlays.Add("SelectOverlay", selectOverlay);

            wpfMap1.Refresh();
        }


        private void textBoxPopulation_KeyDown(object sender, KeyEventArgs e)
        {
            //Updates the population attribute for the selected feature based on the value entered in the textbox.
            //Notice how this updated value is automatically reflected in the labeling by refreshing the drawing of the countries shapefile.
            if (e.Key == Key.Enter)
            {
                if (editID != null)
                {
                    editShapefileFeatureLayer.Open();
                    Feature editFeature = editShapefileFeatureLayer.FeatureSource.GetFeatureById(editID, ReturningColumnsType.AllColumns);
                    editFeature.ColumnValues["POP_CNTRY"] = textBoxPopulation.Text;

                    editShapefileFeatureLayer.EditTools.BeginTransaction();
                    editShapefileFeatureLayer.EditTools.Update(editFeature);
                    editShapefileFeatureLayer.EditTools.CommitTransaction();
                    editShapefileFeatureLayer.Close();

                    wpfMap1.Refresh(wpfMap1.Overlays["StaticOverlay"]);
                }
            }
        }

        private void wpfMap1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            //Here we use a buffer of 15 in screen coordinate. This means that regardless of the zoom level, we will always find the nearest feature
            //within 15 pixels to where we clicked.
            int screenBuffer = 15;

            //Logic for converting screen coordinate values to world coordinate for the spatial query. Notice that the distance buffer for the spatial query
            //will change according to the zoom level while it remains the same for the screen buffer distance.
            ScreenPointF clickedPointF = new ScreenPointF(e.ScreenX, e.ScreenY);
            ScreenPointF bufferPointF = new ScreenPointF(clickedPointF.X + screenBuffer, clickedPointF.Y);

            double distanceBuffer = ExtentHelper.GetWorldDistanceBetweenTwoScreenPoints(wpfMap1.CurrentExtent, clickedPointF, bufferPointF,
                                                                (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight, wpfMap1.MapUnit, DistanceUnit.Meter);

            ShapeFileFeatureLayer layerShapefileFeatureLayer = (ShapeFileFeatureLayer)wpfMap1.FindFeatureLayer("LabelCountriesLayer");
            Collection<string> columnNames = new Collection<string>();
            columnNames.Add("POP_CNTRY");

            layerShapefileFeatureLayer.Open();
            Collection<Feature> features = layerShapefileFeatureLayer.FeatureSource.GetFeaturesNearestTo(new PointShape(e.WorldX, e.WorldY), wpfMap1.MapUnit, 1, columnNames, distanceBuffer, DistanceUnit.Meter);
            layerShapefileFeatureLayer.Close();

            //Adds the feature clicked on to the selected layer to be displayed as highlighed and with the name labeled.
            InMemoryFeatureLayer selectLayer = (InMemoryFeatureLayer)wpfMap1.FindFeatureLayer("SelectLayer");

            selectLayer.InternalFeatures.Clear();

            if (features.Count > 0)
            {
                selectLayer.InternalFeatures.Add(features[0]);
                editID = features[0].Id;
            }

            //Refreshes only the select layer.
            wpfMap1.Refresh(wpfMap1.Overlays["SelectOverlay"]);

            textBoxPopulation.Text = features[0].ColumnValues["POP_CNTRY"];
        }

        //Event used for labeling based on the different columns.
        void labelShapefileFeatureLayer_CustomColumnFetch(object sender, CustomColumnFetchEventArgs e)
        {
            //For the first TextStyle.
            if (e.ColumnName == "MergedColumn")
            {
                ShapeFileFeatureLayer labelShapeFileFeatureLayer = (ShapeFileFeatureLayer)wpfMap1.FindFeatureLayer("LabelCountriesLayer");
                Feature currentFeature = labelShapeFileFeatureLayer.QueryTools.GetFeatureById(e.Id, ReturningColumnsType.AllColumns);
                e.ColumnValue = currentFeature.ColumnValues["CNTRY_NAME"];
            }
            //For the second TextStyle.
            else if (e.ColumnName == "MergedColumn2")
            {
                ShapeFileFeatureLayer labelShapeFileFeatureLayer = (ShapeFileFeatureLayer)wpfMap1.FindFeatureLayer("LabelCountriesLayer");
                Feature currentFeature = labelShapeFileFeatureLayer.QueryTools.GetFeatureById(e.Id, ReturningColumnsType.AllColumns);
                e.ColumnValue = "Pop: " + String.Format("{0:0,0}", Convert.ToDouble(currentFeature.ColumnValues["POP_CNTRY"]));
            }
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = "X: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.X) +
                          "  Y: " + DecimalDegreesHelper.GetDegreesMinutesSecondsStringFromDecimalDegree(pointShape.Y);

        }
    }
}
