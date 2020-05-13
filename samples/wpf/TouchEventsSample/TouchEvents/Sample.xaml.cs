using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace TouchEventsInWpfEdition
{
    public partial class Sample : Window
    {
        private const double radius = 60d;

        private bool isRotated;
        private double currentAngle = 0;
        private double value = 0;
        private DispatcherTimer refreshTimer;

        public Sample()
        {
            InitializeComponent();

            Map1.ManipulationDelta += Map1_ManipulationDelta;
            Map1.MapClick += Map1_MapClick;

            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromSeconds(3);
            refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["LayerOverlay"];
            Map1.Refresh(layerOverlay);
        }

        private void Menuitem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Menu item 1 clicked.");
        }

        private void Map1_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.CurrentExtent = new RectangleShape(-16965308.8699768, 10390517.8895596, 15908646.0310775, -8922930.61480984);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultipleTile;
            layerOverlay.TileBuffer = 2;

            BackgroundLayer backgroundLayer = new BackgroundLayer(new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean));
            layerOverlay.Layers.Add(backgroundLayer);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"..\..\SampleData\Countries02.shp");
            worldLayer.DrawingMarginInPixel = 60;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 1);
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = WorldStreetsTextStyles.GeneralPurpose("CNTRY_NAME", 10);
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.Font = new GeoFont("Arial", 16, DrawingFontStyles.Bold);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add("SampleLayer", worldLayer);
            Map1.Overlays.Add("LayerOverlay", layerOverlay);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            Map1.Overlays.Add("MarkerOverlay", markerOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            Map1.Overlays.Add("PopupOverlay", popupOverlay);
            Popup popup = new Popup(new PointShape(-10777662.2854073, 3912165.79621789));
            popup.Content = new TextBlock() { Text = "ThinkGeo", FontSize = 20 };
            popupOverlay.Popups.Add(popup);

            Map1.TrackOverlay.DrawingMarginPercentage = 80;
            Map1.EditOverlay.DrawingMarginPercentage = 80;

            Map1.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                switch (button.Name)
                {
                    case "btnTrackNormal":
                        Map1.TrackOverlay.TrackMode = TrackMode.None;
                        break;
                    case "btnTrackPoint":
                        Map1.TrackOverlay.TrackMode = TrackMode.Point;
                        break;
                    case "btnTrackLine":
                        Map1.TrackOverlay.TrackMode = TrackMode.Line;
                        break;
                    case "btnTrackRectangle":
                        Map1.TrackOverlay.TrackMode = TrackMode.Rectangle;
                        break;
                    case "btnTrackSquare":
                        Map1.TrackOverlay.TrackMode = TrackMode.Square;
                        break;
                    case "btnTrackPolygon":
                        Map1.TrackOverlay.TrackMode = TrackMode.Polygon;
                        break;
                    case "btnTrackCircle":
                        Map1.TrackOverlay.TrackMode = TrackMode.Circle;
                        break;
                    case "btnTrackEllipse":
                        Map1.TrackOverlay.TrackMode = TrackMode.Ellipse;
                        break;
                    case "btnTrackEdit":
                        Map1.TrackOverlay.TrackMode = TrackMode.None;
                        foreach (Feature feature in Map1.TrackOverlay.TrackShapeLayer.InternalFeatures)
                        {
                            Map1.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature);
                        }

                        Map1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
                        Map1.EditOverlay.CalculateAllControlPoints();
                        Map1.Refresh(new Overlay[] { Map1.EditOverlay, Map1.TrackOverlay });
                        break;
                    case "btnTrackDelete":
                        // should use TrackMode.EditShape select a shape, and then call DeleteTrackShape();
                        int lastIndex = Map1.EditOverlay.EditShapesLayer.InternalFeatures.Count - 1;

                        if (lastIndex >= 0)
                        {
                            Map1.EditOverlay.EditShapesLayer.InternalFeatures.RemoveAt(lastIndex);
                            Map1.EditOverlay.CalculateAllControlPoints();
                        }

                        Map1.Refresh(Map1.EditOverlay);
                        break;
                    default:
                        Map1.TrackOverlay.TrackMode = TrackMode.None;
                        break;
                }
            }
        }

        private void Map1_MapTap(object sender, MapTapWpfMapEventArgs e)
        {
            if (tapRadioButton.IsChecked.GetValueOrDefault())
            {
                AddMarkerToMap(new PointShape(e.ScreenX, e.ScreenY));
            }
        }

        private void AddMarkerToMap(PointShape location)
        {
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)Map1.Overlays["MarkerOverlay"];
            Marker marker = new Marker(location);
            marker.ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute));
            marker.Width = 20;
            marker.Height = 34;
            marker.YOffset = -17;
            marker.RenderTransform = new RotateTransform(-currentAngle, marker.Width / 2, marker.Height);
            markerOverlay.Markers.Add(marker);
            markerOverlay.Refresh();
        }

        void Map1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            if (tapRadioButton.IsChecked.GetValueOrDefault())
            {
                AddMarkerToMap(e.WorldLocation);
            }
        }

        private void Map1_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            RotateTransform compassRotateTransform = Compass.RenderTransform as RotateTransform;
            if (compassRotateTransform != null)
            {
                compassRotateTransform.Angle += e.DeltaManipulation.Rotation;
                compassRotateTransform.CenterX = Compass.ActualWidth / 2 + 2;
                compassRotateTransform.CenterY = Compass.ActualHeight / 2;

                SetCanvasRotate(compassRotateTransform.Angle);
            }
            else
            {
                Compass.RenderTransform = new RotateTransform(e.DeltaManipulation.Rotation, Compass.ActualWidth / 2 + 2, Compass.ActualHeight / 2);

                SetCanvasRotate(e.DeltaManipulation.Rotation);
            }
        }

        private void TrackRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (trackPanel != null)
            {
                trackPanel.Visibility = Visibility.Visible;
            }
        }

        private void TapRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (trackPanel != null)
            {
                trackPanel.Visibility = Visibility.Collapsed;
            }
            CancelEdit();
        }

        private void CancelEdit()
        {
            foreach (var feature in Map1.EditOverlay.EditShapesLayer.InternalFeatures)
            {
                Map1.TrackOverlay.TrackShapeLayer.InternalFeatures.Add(feature);
            }
            Map1.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
            Map1.Refresh(new Overlay[] { Map1.EditOverlay, Map1.TrackOverlay });
            Map1.TrackOverlay.TrackMode = TrackMode.None;
        }

        private void StopAnglePanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!isRotated)
            {
                Compass.RenderTransform = new RotateTransform(0, Compass.ActualWidth / 2 + 2, Compass.ActualHeight / 2);
                SetCanvasRotate(0);
                currentAngle = 0;
            }
            Map1.Refresh();
        }

        private void StopAnglePanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isRotated = false;
            Point mousePosition = e.GetPosition((UIElement)sender);
            double x = mousePosition.X - radius;
            double y = radius - mousePosition.Y;
            double angle = Math.Atan2(x, y) * 180 / Math.PI;
            value = angle - currentAngle;
        }

        private void StopAnglePanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                isRotated = true;
                SetAngle(e.GetPosition((UIElement)sender));
            }
        }

        private void SetAngle(Point mousePosition)
        {
            double x = mousePosition.X - radius;
            double y = radius - mousePosition.Y;
            double angle = Math.Atan2(x, y) * 180 / Math.PI;
            angle = angle - value;
            Compass.RenderTransform = new RotateTransform(angle, Compass.ActualWidth / 2 + 2, Compass.ActualHeight / 2);
            SetCanvasRotate(angle);

            refreshTimer.Start();
        }

        private void SetCanvasRotate(double angle)
        {
            currentAngle = angle;
            UpdateElementsRotation(angle);
            FrameworkElement element = GetElement(Map1, "EventCanvas");
            FrameworkElement allEventCanvas = GetElement(Map1, "AllEventCanvas");
            if (element != null && allEventCanvas != null)
            {
                if (allEventCanvas.ActualHeight < ActualHeight * 2)
                {
                    allEventCanvas.Height = ActualHeight * 2;
                    allEventCanvas.Width = ActualWidth * 2;
                    allEventCanvas.Margin = new Thickness(-ActualWidth / 2, -ActualHeight / 2, 0, 0);
                }
                element.RenderTransform = new RotateTransform(angle, Map1.ActualWidth / 2, Map1.ActualHeight / 2);
            }
        }

        private void UpdateElementsRotation(double angle)
        {
            LayerOverlay layerOverlay = (LayerOverlay)Map1.Overlays["LayerOverlay"];
            ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["SampleLayer"];
            worldLayer.Open();

            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)Map1.Overlays["MarkerOverlay"];
            foreach (var marker in markerOverlay.Markers)
            {
                marker.RenderTransform = new RotateTransform(-angle, marker.ActualWidth / 2, marker.ActualHeight);
            }

            PopupOverlay popupOverlay = (PopupOverlay)Map1.Overlays["PopupOverlay"];
            foreach (var popup in popupOverlay.Popups)
            {
                popup.RenderTransform = new RotateTransform(-angle, popup.ActualWidth, popup.ActualHeight);
            }

            if (worldLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle != null)
            {
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.RotationAngle = angle;
            }
        }


        private Point GetRotatedCenterPoint(double rotateAngle, double width, double height, double xOffset, double yOffset)
        {
            double newPointX = width / 2;
            double newPointY = height / 2;
            return new Point(newPointX, newPointY);
        }

        private FrameworkElement GetElement(FrameworkElement element, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < count; i++)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(element, i) as FrameworkElement;
                if (child != null)
                {
                    if (child.Name == name)
                    {
                        return child;
                    }
                    else
                    {
                        var temp = GetElement(child, name);
                        if (temp != null && temp.Name == name)
                        {
                            return temp;
                        }
                    }
                }
            }
            return null;
        }
    }
}
