using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class GetAColumnDataInAFeatureLayer : UserControl
    {
        public GetAColumnDataInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-12933325, 14195325, 16027137, -8510143);

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.TileType = TileType.SingleTile;
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }

        private void btnColumns_Click(object sender, RoutedEventArgs e)
        {
            FeatureLayer worldLayer = mapView.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            Collection<FeatureSourceColumn> allColumns = worldLayer.QueryTools.GetColumns();
            worldLayer.Close();

            dgridFeatures.DataContext = GetDataTableFromFeatureSourceColumns(allColumns);
            dgridFeatures.SetBinding(ListView.ItemsSourceProperty, new Binding());
        }

        private static DataTable GetDataTableFromFeatureSourceColumns(Collection<FeatureSourceColumn> featureSourceColumns)
        {
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.InvariantCulture;

            // Setup the column.
            dataTable.Columns.Add("ColumnName");
            dataTable.Columns.Add("MaxLength");
            dataTable.Columns.Add("TypeName");

            foreach (FeatureSourceColumn featureSourceColumn in featureSourceColumns)
            {
                // Add a record.
                DataRow dataRow = dataTable.NewRow();

                dataRow[0] = featureSourceColumn.ColumnName;
                dataRow[1] = featureSourceColumn.MaxLength;
                dataRow[2] = featureSourceColumn.TypeName;

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }
    }
}