/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace PopupOverlay
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private Collection<Feature> selectedFeatures;
        private ShapeFileFeatureLayer statesLayer;

        public TestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Sets the unit of the map to Decimal Degrees because it is in Geodetic.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret") { MapType = ThinkGeoCloudRasterMapsMapType.Hybrid };
            wpfMap1.Overlays.Add(baseOverlay);

            //Adds the PopupOverlay to the overlay collection of the wpf map.
            ThinkGeo.MapSuite.Wpf.PopupOverlay popupOverlay = new ThinkGeo.MapSuite.Wpf.PopupOverlay();
            wpfMap1.Overlays.Add("PopupOverlay", popupOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            statesLayer = new ShapeFileFeatureLayer(@"..\..\Data\USStates.shp", GeoFileReadWriteMode.ReadWrite);
            statesLayer.Open();
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new ThinkGeo.MapSuite.Styles.AreaStyle(new GeoPen(new GeoSolidBrush(GeoColor.SimpleColors.Green), 2));
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(statesLayer);
            wpfMap1.Overlays.Add(layerOverlay);

            wpfMap1.CurrentExtent = new RectangleShape(-12801741.4412265, 5621521.48619207, -7792364.35552915, 4163881.14406429);
            wpfMap1.Refresh();
        }

        private void wpfMap1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            //Gets the features where the user clicked on.
            selectedFeatures = statesLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.AllColumns);

            //Shows on the Popup the population of the state clicked on.
            if (selectedFeatures.Count > 0)
            {
                ThinkGeo.MapSuite.Wpf.PopupOverlay popupOverlay = (ThinkGeo.MapSuite.Wpf.PopupOverlay)wpfMap1.Overlays["PopupOverlay"];
                //Sets the location of the Popup where the user clicked on the map.
                Popup popup = new Popup(e.WorldLocation);

                //Adds a TextBox  to the Popup.
                TextBox displayer = new TextBox();
                //Event for getting the text typed in the TextBox.
                displayer.KeyDown += new KeyEventHandler(displayer_KeyDown);
                displayer.Text = "POP1990: " + selectedFeatures[0].ColumnValues["POP1990"];
                popup.Content = displayer;
                popup.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                //Clears existing Popups and add the new one to the PopupOverlay.
                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);
                popupOverlay.Refresh();
            }
        }

        //Updates the "Pop1990" column of the selected feature based on the data entered in the Popup.
        void displayer_KeyDown(object sender, KeyEventArgs e)
        {
            //When pressing Enter key, updates the selected feature with the new value for Population.
            if (e.Key == Key.Return)
            {
                //Updates the column "Pop1990" with the value added in the Popup for the selected feature.
                ThinkGeo.MapSuite.Wpf.PopupOverlay popupOverlay = (ThinkGeo.MapSuite.Wpf.PopupOverlay)wpfMap1.Overlays["PopupOverlay"];
                TextBox textBox = (TextBox)popupOverlay.Popups[0].Content;
                Feature updatedFeature = selectedFeatures[0];
                updatedFeature.ColumnValues["POP1990"] = textBox.Text.Replace("POP1990: ", "");

                //Edits the shapefile. Make sure to open the shapefile as Read and Write mode.
                statesLayer.EditTools.BeginTransaction();
                statesLayer.EditTools.Update(selectedFeatures[0]);
                statesLayer.EditTools.CommitTransaction();
            }
        }

        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.ActualWidth, (float)wpfMap1.ActualHeight);

            textBox1.Text = "X: " + Math.Round(pointShape.X) +
                          "  Y: " + Math.Round(pointShape.Y);

        }
    }
}
