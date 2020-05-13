/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using DynamicMarkerOverlay.NaFacilityService;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;
using ZedGraph;

namespace DynamicMarkerOverlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Create a timer which is used to retrive the data from a webservice to update the warehouses.
            Timer dataRetriveTimer = new Timer()
            {
                Enabled = true,
                Interval = 5000   // By default, it will refresh data per hour
            };
            dataRetriveTimer.Tick += (sender, e) =>
            {
                using (var client = new DataServiceClient("defaultDataService"))
                {
                    // Retrive the data from service
                    FacilityModel[] facilities = client.GetFacilities();

                    // Apply the retrived data to InMemoryFeatureSource which is for warehouses.
                    foreach (FacilityModel model in facilities)
                    {
                        FeatureSourceMarkerOverlay facilitiesOverlay = Map1.Overlays["FacilitiesOverlay"] as FeatureSourceMarkerOverlay;
                        InMemoryFeatureSource factilityFeatureSource = facilitiesOverlay.FeatureSource as InMemoryFeatureSource;

                        Feature feature = factilityFeatureSource.InternalFeatures.Where(v => v.ColumnValues["Airport Code"] == model.AirportCode).FirstOrDefault();
                        feature.ColumnValues["Packages"] = model.Packages.ToString(CultureInfo.InvariantCulture);
                        feature.ColumnValues["AverageP"] = model.PackageAverage.ToString(CultureInfo.InvariantCulture);
                    }
                }

                Dispatcher.BeginInvoke(new Action(() => Map1.Refresh()));
            };
        }

        private void Map1_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            Map1.Overlays.Add("WMK", baseOverlay);

            // Import the data into a InMemoryFeatureLayer with limited data, becuase we need to update its column value on the fly.
            ShapeFileFeatureSource shapeFileFeatureSource = new ShapeFileFeatureSource(@"..\..\App_Data\Facilities.shp");
            shapeFileFeatureSource.Open();

            InMemoryFeatureSource facilitiesFeatureSource = new InMemoryFeatureSource(shapeFileFeatureSource.GetColumns(), shapeFileFeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns));
            shapeFileFeatureSource.Close();

            // Create a FeatureSourceMarkerOverlay to display Na-Factilities from InMemoryFeatureSource, which 
            // is created from a shape file "Facilities.shp", it is converted from the "NA-Facitlities.xlsx".
            // The data convert can be managed by following steps:
            // 1. Save the *.xlsx as *.csv with Microsoft Office.
            // 2. Load *.csv into Map Suite GisEditor, and then export it to *.shp.
            ClickableFeatureSourceMarkerOverlay facilitiesOverlay = new ClickableFeatureSourceMarkerOverlay();
            facilitiesOverlay.MarkerMouseClick += FacilitiesOverlay_MarkerMouseClick;
            facilitiesOverlay.FeatureSource = facilitiesFeatureSource;

            // Create the classbreak style with different colors based on the packages.
            ClassBreakMarkerStyle facilitiesStyle = new ClassBreakMarkerStyle("Packages");
            MarkerClassBreak lessBreak = new MarkerClassBreak(0, new PointMarkerStyle(new BitmapImage(new Uri(@"pack://application:,,,/image/Green_warehouse.png"))));
            facilitiesStyle.ClassBreaks.Add(lessBreak);
            MarkerClassBreak greaterBreak = new MarkerClassBreak(10000, new PointMarkerStyle(new BitmapImage(new Uri(@"pack://application:,,,/image/Red_warehouse.png"))));
            facilitiesStyle.ClassBreaks.Add(greaterBreak);

            facilitiesOverlay.ZoomLevelSet.ZoomLevel01.CustomMarkerStyle = facilitiesStyle;
            facilitiesOverlay.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            Map1.Overlays.Add("FacilitiesOverlay", facilitiesOverlay);

            // Create a popup for showing message.
            PopupOverlay popupOverlay = new PopupOverlay();
            Map1.Overlays.Add("MessagePopup", popupOverlay);

            // Set the map zoom to the extent of all the facilities at the startup.
            facilitiesOverlay.FeatureSource.Open();
            Map1.CurrentExtent = facilitiesOverlay.FeatureSource.GetBoundingBox();
            facilitiesOverlay.FeatureSource.Close();

            Map1.Refresh();
        }

        private void FacilitiesOverlay_MarkerMouseClick(object sender, MouseButtonEventArgs e)
        {
            Marker marker = sender as Marker;

            FeatureSourceMarkerOverlay facilitiesOverlay = Map1.Overlays["FacilitiesOverlay"] as FeatureSourceMarkerOverlay;
            facilitiesOverlay.FeatureSource.Open();
            Collection<Feature> selectedFeatures = facilitiesOverlay.FeatureSource.GetFeaturesNearestTo(new PointShape(marker.Position.X, marker.Position.Y), Map1.MapUnit, 1, ReturningColumnsType.AllColumns);
            facilitiesOverlay.FeatureSource.Close();

            if (selectedFeatures.Count > 0)
            {
                PopupOverlay popupOverlay = (PopupOverlay)Map1.Overlays["MessagePopup"];
                Popup popup = new Popup(marker.Position);
                Feature feature = selectedFeatures[0];
                popup.Content = CreatePopup(selectedFeatures[0]);
                popup.FontSize = 12d;

                popupOverlay.Popups.Clear();
                popupOverlay.Popups.Add(popup);
                popupOverlay.Refresh();
            }
        }

        private ContentControl CreatePopup(Feature feature)
        {
            ContentControl content = new ContentControl();
            Grid panel = new Grid();
            panel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(120) });
            panel.ColumnDefinitions.Add(new ColumnDefinition());
            content.Content = panel;
            System.Windows.Controls.Image image = new System.Windows.Controls.Image() { Width = 120, Height = 100 };
            panel.Children.Add(image);
            Grid.SetColumn(image, 0);
            using (Stream ms = CreateImageStream(feature))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();
                image.Source = bitmap;
            }

            TextBlock desc = new TextBlock() { TextWrapping = TextWrapping.WrapWithOverflow };
            panel.Children.Add(desc);
            Grid.SetColumn(desc, 1);
            StringBuilder info = new StringBuilder();
            info.AppendLine(String.Format(CultureInfo.InvariantCulture, "Airport Code:\t{0}", feature.ColumnValues["Airport Code"].Trim()));
            info.AppendLine(String.Format(CultureInfo.InvariantCulture, "Address:\t{0}, {1}\t", feature.ColumnValues["Address1"].Trim(), feature.ColumnValues["Description"].Trim()));
            info.AppendLine(String.Format(CultureInfo.InvariantCulture, "Packages:\t{0}", feature.ColumnValues["Packages"].Trim()));
            info.AppendLine(String.Format(CultureInfo.InvariantCulture, "AveragePackages:\t{0}", feature.ColumnValues["AverageP"].Trim()));
            desc.Text = info.ToString();

            return content;
        }

        /// <summary>
        /// Create the Chart using ZedGraph based on feature's columns.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        private Stream CreateImageStream(Feature feature)
        {
            MemoryStream ms = new MemoryStream();

            ZedGraphControl grapControl = new ZedGraphControl();

            GraphPane myPane = grapControl.GraphPane;

            myPane.YAxis.Title.Text = "Account";

            string[] labels = { "Packages", "AveragePackages" };
            double[] y = { Convert.ToInt32(feature.ColumnValues["Packages"], CultureInfo.InvariantCulture), Convert.ToInt32(feature.ColumnValues["AverageP"], CultureInfo.InvariantCulture) };

            BarItem myBar = myPane.AddBar("", null, y, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.Green, Color.Green);

            myPane.XAxis.Scale.TextLabels = labels;
            myPane.XAxis.Type = AxisType.Text;
            myPane.AxisChange();
            System.Drawing.Image image = grapControl.GetImage();
            image.Save(ms, ImageFormat.Png);

            return ms;
        }
    }
}
