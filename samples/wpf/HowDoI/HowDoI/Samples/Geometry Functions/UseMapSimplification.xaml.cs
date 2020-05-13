using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class UseMapSimplification : UserControl
    {
        private AreaBaseShape areaBaseShape;

        public UseMapSimplification()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-177.39584350585937, 83.113876342773437, -52.617362976074219, 14.550546646118164);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));

            worldLayer.Open();
            ProjectionConverter project = new ProjectionConverter(3857, 4326);
            project.Open();
            Feature feature = project.ConvertToExternalProjection(worldLayer.QueryTools.GetFeatureById("135", new string[0]));
            project.Close();
            areaBaseShape = (AreaBaseShape)feature.GetShape();
            worldLayer.Close();

            InMemoryFeatureLayer simplificationLayer = new InMemoryFeatureLayer();
            simplificationLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            simplificationLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            simplificationLayer.InternalFeatures.Add(feature);

            LayerOverlay simplificationOverlay = new LayerOverlay();
            simplificationOverlay.TileType = TileType.SingleTile;
            simplificationOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            simplificationOverlay.Layers.Add("SimplificationLayer", simplificationLayer);
            mapView.Overlays.Add("SimplificationOverlay", simplificationOverlay);

            cmbSimplificationType.SelectedIndex = 0;
            cmbTolerance.SelectedIndex = 0;
            mapView.Refresh();
        }

        private void btnSimplify_Click(object sender, RoutedEventArgs e)
        {
            InMemoryFeatureLayer simplificationLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("SimplificationLayer");

            double tolerance = Convert.ToDouble(cmbTolerance.SelectedItem.ToString().Split(':')[1], CultureInfo.InvariantCulture);
            SimplificationType simplificationType = (SimplificationType)cmbSimplificationType.SelectedIndex;

            MultipolygonShape multipolygonShape = areaBaseShape.Simplify(tolerance, simplificationType);
            simplificationLayer.InternalFeatures.Clear();
            simplificationLayer.InternalFeatures.Add(new Feature(multipolygonShape));

            mapView.Overlays["SimplificationOverlay"].Refresh();
        }
    }
}