using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class TrackAndEditShapesInRotateWorld : UserControl
    {
        public TrackAndEditShapesInRotateWorld()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-13629668, 6044916, -7913604, 2972807);

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add(popupOverlay);

            Popup popup = new Popup(-13177554, 4040376);
            popup.Content = "Los Angeles";
            popupOverlay.Popups.Add(popup);

            mapView.RotatedAngle = 30;

            mapView.Refresh();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnTrackNormal":
                        mapView.TrackOverlay.TrackMode = TrackMode.None;
                        break;

                    case "btnTrackPoint":
                        mapView.TrackOverlay.TrackMode = TrackMode.Point;
                        break;

                    case "btnTrackLine":
                        mapView.TrackOverlay.TrackMode = TrackMode.Line;
                        break;

                    case "btnTrackRectangle":
                        mapView.TrackOverlay.TrackMode = TrackMode.Rectangle;
                        break;

                    case "btnTrackSquare":
                        mapView.TrackOverlay.TrackMode = TrackMode.Square;
                        break;

                    case "btnTrackPolygon":
                        mapView.TrackOverlay.TrackMode = TrackMode.Polygon;
                        break;

                    case "btnTrackCircle":
                        mapView.TrackOverlay.TrackMode = TrackMode.Circle;
                        break;

                    case "btnTrackEllipse":
                        mapView.TrackOverlay.TrackMode = TrackMode.Ellipse;
                        break;

                    case "btnTrackEdit":
                        mapView.TrackOverlay.TrackMode = TrackMode.None;
                        foreach (Feature feature in mapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
                        {
                            mapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature);
                        }

                        mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                        mapView.EditOverlay.CalculateAllControlPoints();
                        mapView.Refresh(new Overlay[] { mapView.EditOverlay, mapView.TrackOverlay });
                        break;

                    case "btnTrackDelete":
                        // should use TrackMode.EditShape select a shape, and then call DeleteTrackShape();
                        int lastIndex = mapView.EditOverlay.EditShapesLayer.InternalFeatures.Count - 1;

                        if (lastIndex >= 0)
                        {
                            mapView.EditOverlay.EditShapesLayer.InternalFeatures.RemoveAt(lastIndex);
                            mapView.EditOverlay.CalculateAllControlPoints();
                        }

                        mapView.Refresh(mapView.EditOverlay);
                        break;

                    default:
                        mapView.TrackOverlay.TrackMode = TrackMode.None;
                        break;
                }
            }
        }
    }
}