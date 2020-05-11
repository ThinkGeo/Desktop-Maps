using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAPostgreSqlFeatureLayer : UserControl
    {
        public LoadAPostgreSqlFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            mapView.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);

            string connectString = "Server=54.177.171.109;User Id=postgres;Password=akeypmon1;DataBase=spatialdatabase;";
            PostgreSqlFeatureLayer postgreLayer = new PostgreSqlFeatureLayer(connectString, "COUNTRY02", "'RECID'");
            postgreLayer.SchemaName = "public";
            postgreLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            postgreLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay postgreOverlay = new LayerOverlay();
            postgreOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            postgreOverlay.Layers.Add("PostgreLayer", postgreLayer);
            mapView.Overlays.Add("PostgreOverlay", postgreOverlay);

            mapView.Refresh();
        }
    }
}