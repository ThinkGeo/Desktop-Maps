using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for LoadSqliteFeatureLayer.xaml
    /// </summary>
    public partial class LoadSqliteFeatureLayer : UserControl
    {
        public LoadSqliteFeatureLayer()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            var connectionString = @"Data Source=\\192.168.0.3\Internal Test Data\Sqlite\Mapping.sqlite";
            var ne_road10m_linestring = new SqliteFeatureLayer(connectionString, "Segments", "geomID", "geom");
            ne_road10m_linestring.Name = ne_road10m_linestring.TableName;
            ne_road10m_linestring.WhereClause = $"WHERE ReplicationState = 1";
            ne_road10m_linestring.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true));
            ne_road10m_linestring.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(ne_road10m_linestring);

            mapView.Overlays.Add(layerOverlay);
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-8336043.617221244, 5212990.090742311, -8328829.872716907, 5207266.868281254);
            mapView.Refresh();
        }
    }
}
