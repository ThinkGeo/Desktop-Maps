using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class AddFeaturesFromAFeatureLayer : UserControl
    {
        public AddFeaturesFromAFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(0, 100, 100, 0);
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.InternalFeatures.Add("Polygon", new Feature("POLYGON((10 60,40 70,30 85, 10 60))"));
            inMemoryLayer.InternalFeatures.Add("Multipoint", new Feature("MULTIPOINT(10 20, 30 20,40 20, 10 30, 30 30, 40 30)"));
            inMemoryLayer.InternalFeatures.Add("Line", new Feature("LINESTRING(60 60, 70 70,75 60, 80 70, 85 60,95 80)"));
            inMemoryLayer.InternalFeatures.Add("Rectangle", new Feature(new RectangleShape(65, 30, 95, 15)));

            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.RoyalBlue));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Blue;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(200, GeoColors.Red), 5);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, GeoColors.Green), 8);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay inMemoryFeatureOverlay = new LayerOverlay();
            inMemoryFeatureOverlay.TileType = TileType.SingleTile;
            inMemoryFeatureOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            mapView.Overlays.Add("InMemoryOverlay", inMemoryFeatureOverlay);

            mapView.Refresh();
        }

        private void btnAddAFeature_Click(object sender, RoutedEventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("InMemoryFeatureLayer");

            BaseShape shape = new EllipseShape(new PointShape(50, 50), 10, 10);
            shape.Id = "Ellipse";

            inMemoryLayer.Open();
            inMemoryLayer.EditTools.BeginTransaction();
            inMemoryLayer.EditTools.Add(new Feature(shape));
            inMemoryLayer.EditTools.CommitTransaction();
            inMemoryLayer.Close();

            mapView.Refresh(mapView.Overlays["InMemoryOverlay"]);
        }
    }
}