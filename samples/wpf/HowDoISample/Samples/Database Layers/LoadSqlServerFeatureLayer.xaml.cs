using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadSqlServerFeatureLayer : UserControl
    {
        public LoadSqlServerFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);

            string connectionString = "Server=54.177.171.109;Initial Catalog=spatialdatabase;User ID=sa;Password=akeypmon1!";
            SqlServerFeatureLayer layer = new SqlServerFeatureLayer(connectionString, "COUNTRY02", "RECID");
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Green,GeoColors.Red);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay sql2008Overlay = new LayerOverlay();
            sql2008Overlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            sql2008Overlay.Layers.Add(layer);
            mapView.Overlays.Add(sql2008Overlay);

            mapView.Refresh();
        }
    }
}