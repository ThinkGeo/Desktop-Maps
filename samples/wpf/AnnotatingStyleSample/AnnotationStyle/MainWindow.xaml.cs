using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.Sample
{
    public partial class MainWindow : Window
    {
        private InMemoryFeatureLayer layer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.RenderMode = RenderMode.DrawingVisual;
            layer = new InMemoryFeatureLayer();
            LineShape lineShape = new LineShape();
            lineShape.Vertices.Add(new Vertex(0, 0));
            lineShape.Vertices.Add(new Vertex(100, 100));
            var feature = new Feature(lineShape);
            feature.ColumnValues.Add("Annotation", "Annotation 1");
            layer.InternalFeatures.Add(feature);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AnnotationStyle("Annotation"));
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add(layer);
            mapView.Overlays.Add(layerOverlay);

            var annotationEditInteractiveOverlay = new AnnotationEditInteractiveOverlay();
            mapView.EditOverlay = annotationEditInteractiveOverlay;

            layer.Open();
            mapView.CurrentExtent = layer.GetBoundingBox();
            mapView.Refresh();
        }

        private void EditAnnotationClicked(object sender, RoutedEventArgs e)
        {
            foreach (Feature feature in layer.InternalFeatures)
            {
                mapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature);
            }

            layer.InternalFeatures.Clear();
            mapView.EditOverlay.CalculateAllControlPoints();
            mapView.Refresh();
        }
    }
}
