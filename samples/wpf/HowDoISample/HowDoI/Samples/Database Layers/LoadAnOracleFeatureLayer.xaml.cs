using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAnOracleFeatureLayer : UserControl
    {
        public LoadAnOracleFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-180, 90, 180, -90);

            string connectString = "OCI:system/akeypmon1@54.177.171.109/orcl";
            OracleFeatureLayer oracleLayer = new OracleFeatureLayer(connectString);
            oracleLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
            oracleLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(255, 169, 195, 221), 2F, GeoColors.Black, 4F, false);
            oracleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            oracleLayer.FeatureIdFieldName = "ID";
            oracleLayer.ActiveLayer = "COUNTRY02";
            oracleLayer.EnableEmbeddedStyle = false;

            LayerOverlay oracleOverlay = new LayerOverlay();
            oracleOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            oracleOverlay.Layers.Add("OracleLayer", oracleLayer);
            mapView.Overlays.Add("OracleOverlay", oracleOverlay);

            mapView.Refresh();
        }
    }
}