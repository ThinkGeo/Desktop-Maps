using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.Sample
{
    public class AnnotationEditInteractiveOverlay : EditInteractiveOverlay
    {
        public AnnotationEditInteractiveOverlay()
            : base()
        {
            EditShapesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new AnnotationStyle());
            EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = null;
            EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = null;
            EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = null;
            EditShapesLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = null;

            CanAddVertex = false;
        }
    }
}
