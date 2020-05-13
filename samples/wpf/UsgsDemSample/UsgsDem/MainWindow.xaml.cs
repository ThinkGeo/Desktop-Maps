using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace UsgsDem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;

            LayerOverlay layerOverlay = new LayerOverlay();
            UsgsDemFeatureLayer layer = new UsgsDemFeatureLayer("../../AppData/mobile-e");
            layer.DrawingQuality = DrawingQuality.HighSpeed;

            layer.Open();
            DataTable informations = CreateInformationTable((UsgsDemFeatureSource)layer.FeatureSource);
            informationDataGrid.ItemsSource = informations.DefaultView;

            ClassBreakStyle classBreakStyle = new ClassBreakStyle();
            classBreakStyle.ColumnName = layer.DataValueColumnName;
            Collection<GeoColor> colors = GeoColor.GetColorsInQualityFamily(GeoColors.Blue, 100);
            double cellValue = (layer.MaxElevation - layer.MinElevation) / 100;
            for (int i = 0; i < colors.Count; i++)
                classBreakStyle.ClassBreaks.Add(new ClassBreak(layer.MinElevation + cellValue * i, new AreaStyle(new GeoSolidBrush(colors[i]))));

            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(layer);
            map.Overlays.Add(layerOverlay);

            map.CurrentExtent = layer.GetBoundingBox();
            map.Refresh();
        }

        private DataTable CreateInformationTable(UsgsDemFeatureSource featureSource)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Key", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            AddRecordToDataTable(dataTable, "Origin", featureSource.OriginCode);
            AddRecordToDataTable(dataTable, "Level", featureSource.QualityLevel.ToString());
            AddRecordToDataTable(dataTable, "ResolutionX", featureSource.ResolutionX.ToString());
            AddRecordToDataTable(dataTable, "ResolutionY", featureSource.ResolutionY.ToString());
            AddRecordToDataTable(dataTable, "ResolutionZ", featureSource.ResolutionZ.ToString());
            AddRecordToDataTable(dataTable, "MinElevation", featureSource.MinElevation.ToString());
            AddRecordToDataTable(dataTable, "MaxElevation", featureSource.MaxElevation.ToString());
            AddRecordToDataTable(dataTable, "ElevationUnit", featureSource.ElevationUnit.ToString());
            AddRecordToDataTable(dataTable, "BoundingBox", featureSource.GetBoundingBox().ToString());

            return dataTable;
        }

        private void AddRecordToDataTable(DataTable dataTable, string key, string value)
        {
            var newRow = dataTable.NewRow();
            newRow[0] = key;
            newRow[1] = value;

            dataTable.Rows.Add(newRow);
        }
    }
}
