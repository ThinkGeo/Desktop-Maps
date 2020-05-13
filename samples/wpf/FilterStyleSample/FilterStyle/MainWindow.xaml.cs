/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace FilterStyleSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilterStyle filterStyle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Initialization the wpf map
            wpfMap1.MapUnit = GeographyUnit.Meter;
            wpfMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            wpfMap1.CurrentExtent = new RectangleShape(-14534042.3062566, 9147820.86114499, -4906645.71996033, 1270446.816817);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            wpfMap1.Overlays.Add(baseOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            wpfMap1.Overlays.Add(layerOverlay);

            //create a shapefilefeaturelayer from usstate
            ShapeFileFeatureLayer shpLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\USStates.shp");
            layerOverlay.Layers.Add(shpLayer);
            //add shape file columns to combobox 
            AddColumnItemsFormShapeFile(shpLayer);

            //add the filter style to wpfmap
            filterStyle = new FilterStyle();
            filterStyle.Conditions.Add(CreateNewFilterCondition());
            filterStyle.Styles.Add(AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(120, GeoColor.StandardColors.Red), GeoColor.SimpleColors.White));

            //reset shapefile display style
            shpLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(filterStyle);
            shpLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            wpfMap1.Refresh();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            //change filterstyle and refresh wpfmap.
            filterStyle.Conditions.Clear();
            filterStyle.Conditions.Add(CreateNewFilterCondition());

            wpfMap1.Refresh();
        }

        private FilterCondition CreateNewFilterCondition()
        {
            FilterCondition filterCondition = new FilterCondition();
            filterCondition.ColumnName = cmbFilterColumnName.SelectedItem.ToString();
            filterCondition.Expression = string.Format("{0} {1}", ((ComboBoxItem)cmbFilterCondition.SelectedItem).Content.ToString(), txtFilterValue.Text);
            return filterCondition;
        }

        private void AddColumnItemsFormShapeFile(ShapeFileFeatureLayer shpLayer)
        {
            shpLayer.Open();

            Collection<FeatureSourceColumn> columns = shpLayer.FeatureSource.GetColumns();
            Feature feature = shpLayer.FeatureSource.GetFeatureById("2", ReturningColumnsType.AllColumns);

            foreach (var column in columns)
            {
                double value = 0;
                if (double.TryParse(feature.ColumnValues[column.ColumnName], out value))
                {
                    cmbFilterColumnName.Items.Add(column.ColumnName);
                }
            }

            cmbFilterColumnName.SelectedItem = "POP1990";
        }
    }
}