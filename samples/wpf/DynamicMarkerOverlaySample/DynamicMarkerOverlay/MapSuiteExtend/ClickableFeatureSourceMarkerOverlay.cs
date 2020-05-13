using System.Windows.Input;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace DynamicMarkerOverlay
{
    public class ClickableFeatureSourceMarkerOverlay : FeatureSourceMarkerOverlay
    {
        public event MouseButtonEventHandler MarkerMouseDown;

        public event MouseButtonEventHandler MarkerMouseUp;

        public event MouseButtonEventHandler MarkerMouseClick;

        public event MouseEventHandler MarkerMouseMove;

        protected override GeoCollection<Marker> GetMarkersForDrawingCore(RectangleShape boundingBox)
        {
            GeoCollection<Marker> markers = base.GetMarkersForDrawingCore(boundingBox);
            foreach(Marker marker in markers)
            {
                marker.MarkerMouseClick -= MarkerMouseClick;
                marker.MarkerMouseClick += MarkerMouseClick;
                marker.MarkerMouseDown -= MarkerMouseDown;
                marker.MarkerMouseDown += MarkerMouseDown;
                marker.MarkerMouseUp -= MarkerMouseUp;
                marker.MarkerMouseUp += MarkerMouseUp;
                marker.MarkerMouseMove -= MarkerMouseMove;
                marker.MarkerMouseMove += MarkerMouseMove;
            }

            return markers;
        }
        protected virtual void OnMarkerMouseDown(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = MarkerMouseDown;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMarkerMouseUp(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = MarkerMouseUp;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMarkerMouseClick(MouseButtonEventArgs e)
        {
            MouseButtonEventHandler handler = MarkerMouseClick;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnMarkerMouseMove(MouseEventArgs e)
        {
            MouseEventHandler handler = MarkerMouseMove;
            if (handler != null) handler(this, e);
        }
    }
}
