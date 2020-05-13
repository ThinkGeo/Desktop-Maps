/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace StylesWithInmemoryFeatureLayer
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
            //Sets correct map unit.
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            //Adds the hotel features to map from the text file.
            InMemoryFeatureLayer inMemoryFeatureLayer = CreateInMemoryFeatureLayerFromTextFile(@"..\..\data\FriscoHotels.txt");
            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(inMemoryFeatureLayer);
            wpfMap1.Overlays.Add(layerOverlay);

            inMemoryFeatureLayer.Open();
            wpfMap1.CurrentExtent = inMemoryFeatureLayer.GetBoundingBox();
            inMemoryFeatureLayer.Close();

            wpfMap1.Refresh();
        }

        private InMemoryFeatureLayer CreateInMemoryFeatureLayerFromTextFile(string textFile)
        {
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();

            StreamReader hotelReader = new StreamReader(textFile);
            //Reads the header of the text file to add the columns to the InMemoryFeatureLayer
            string nameColumn = ""; string addressColumn = ""; string roomsColumn = "";

            string header = hotelReader.ReadLine();
            if (header.Trim() != "/")
            {
                string[] strSplit = header.Split(',');
                nameColumn = strSplit[0]; addressColumn = strSplit[1]; roomsColumn = strSplit[2];
            }

            inMemoryFeatureLayer.Open();
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn(nameColumn, DbfColumnType.Character.ToString(), 30));
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn(addressColumn, DbfColumnType.Character.ToString(), 40));
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn(roomsColumn, DbfColumnType.Numeric.ToString(), 30));

            //Read every line of the text file to add the point based features with the column values.
            inMemoryFeatureLayer.EditTools.BeginTransaction();
            string name = ""; string address = ""; string rooms = ""; string longitude = ""; string latitude = "";
            using (hotelReader)
            {
                String line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = hotelReader.ReadLine()) != null)
                {
                    string[] strSplit = line.Split(',');
                    name = strSplit[0]; address = strSplit[1]; rooms = strSplit[2]; longitude = strSplit[3]; latitude = strSplit[4];

                    Feature feature = new Feature(new PointShape(Convert.ToDouble(longitude), Convert.ToDouble(latitude)));
                    feature.ColumnValues.Add(nameColumn, name);
                    feature.ColumnValues.Add(addressColumn, address);
                    feature.ColumnValues.Add(roomsColumn, rooms);

                    inMemoryFeatureLayer.EditTools.Add(feature);
                }
            }
            inMemoryFeatureLayer.EditTools.CommitTransaction();
            inMemoryFeatureLayer.Close();

            //Sets the class break styles and text styles of the InMemoryFeatureLayer.
            ClassBreakStyle cbs = new ClassBreakStyle(roomsColumn);
            cbs.BreakValueInclusion = BreakValueInclusion.IncludeValue;
            cbs.ClassBreaks.Add(new ClassBreak(double.MinValue, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                            GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 8)));

            cbs.ClassBreaks.Add(new ClassBreak(100, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                            GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 12)));

            cbs.ClassBreaks.Add(new ClassBreak(200, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                            GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 16)));

            cbs.ClassBreaks.Add(new ClassBreak(300, PointStyles.CreateSimplePointStyle(PointSymbolType.Circle,
                                            GeoColor.FromArgb(150, GeoColor.StandardColors.Blue), GeoColor.StandardColors.Black, 20)));

            TextStyle textStyle = new TextStyle(nameColumn, new GeoFont("Arial", 10, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.StandardColors.Black));
            textStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 1);
            textStyle.XOffsetInPixel = 10;

            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(cbs);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return inMemoryFeatureLayer;
        }


        private void wpfMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Gets the PointShape in world coordinates from screen coordinates.
            Point point = e.MouseDevice.GetPosition(null);

            ScreenPointF screenPointF = new ScreenPointF((float)point.X, (float)point.Y);
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(wpfMap1.CurrentExtent, screenPointF, (float)wpfMap1.Width, (float)wpfMap1.Height);

            textBox1.Text = $"X: {pointShape.X}  Y: {pointShape.Y}";

        }
    }
}
