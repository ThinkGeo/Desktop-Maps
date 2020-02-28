using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class RefreshPointsRandomly : UserControl
    {
        private System.Threading.Timer movePointsTimer;
        private System.Threading.Timer changeColorTimer;
        private static Random random = new Random();
        private RectangleShape pointsBoundary = new RectangleShape(-20037508, 20037508, 20037508, -20037508);

        public RefreshPointsRandomly()
        {
            InitializeComponent();

            movePointsTimer = new System.Threading.Timer(new System.Threading.TimerCallback(MovePoints));
            changeColorTimer = new System.Threading.Timer(new System.Threading.TimerCallback(ChangeColor));
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-10018754, 20037508, 10018754, -20037508);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            InMemoryFeatureLayer pointsLayer = new InMemoryFeatureLayer();
            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 10, new GeoSolidBrush(GeoColor.GetRandomGeoColor(RandomColorType.All)));
            pointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay pointsOverlay = new LayerOverlay();
            pointsOverlay.TileType = TileType.SingleTile;
            pointsOverlay.Layers.Add("PointsLayer", pointsLayer);
            mapView.Overlays.Add("PointsOverlay", pointsOverlay);

            mapView.Refresh();
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            int pointsCount = Convert.ToInt32(((ComboBoxItem)cbxPointCount.SelectedItem).Content);
            LayerOverlay pointsOverlay = (LayerOverlay)mapView.Overlays["PointsOverlay"];
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)pointsOverlay.Layers["PointsLayer"];

            pointsLayer.InternalFeatures.Clear();
            for (int i = 0; i < pointsCount; i++)
            {
                PointShape pointShape = GetRandomPoint(pointsBoundary);
                string id = GetRandomDirection().ToString();
                Feature feature = new Feature(pointShape.X, pointShape.Y, id);
                pointsLayer.InternalFeatures.Add(feature);
            }

            pointsOverlay.Refresh();

            // Starts the move points timer
            movePointsTimer.Change(0, Convert.ToInt32(((ComboBoxItem)cbxRedrawInterval.SelectedItem).Content));

            // Starts the color changing timer
            changeColorTimer.Change(0, Convert.ToInt32(((ComboBoxItem)cbxChangeColorInterval.SelectedItem).Content));

            btnBegin.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            movePointsTimer.Change(0, System.Threading.Timeout.Infinite);
            changeColorTimer.Change(0, System.Threading.Timeout.Infinite);

            btnBegin.IsEnabled = true;
            btnStop.IsEnabled = false;
        }

        private void ChangeColor(object obj)
        {
            Dispatcher.BeginInvoke(new System.Action(ChangeColor));
        }

        private void ChangeColor()
        {
            LayerOverlay pointsOverlay = (LayerOverlay)mapView.Overlays["PointsOverlay"];
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)pointsOverlay.Layers["PointsLayer"];

            pointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 10, new GeoSolidBrush(GeoColor.GetRandomGeoColor(RandomColorType.All)));
            pointsOverlay.Refresh();
        }

        private void MovePoints(object obj)
        {
            Dispatcher.BeginInvoke(new System.Action(MovePoints), DispatcherPriority.Background);
        }

        private void MovePoints()
        {
            LayerOverlay pointsOverlay = (LayerOverlay)mapView.Overlays["PointsOverlay"];
            InMemoryFeatureLayer pointsLayer = (InMemoryFeatureLayer)pointsOverlay.Layers["PointsLayer"];

            Collection<Feature> features = new Collection<Feature>();
            for (int i = 0; i < pointsLayer.InternalFeatures.Count; i++)
            {
                PointShape shape = pointsLayer.InternalFeatures[i].GetShape() as PointShape;
                string id = pointsLayer.InternalFeatures[i].Id;
                PanDirection direction = (PanDirection)PanDirection.Parse(typeof(PanDirection), id, true);
                if (shape.X > pointsBoundary.UpperLeftPoint.X && shape.X < pointsBoundary.LowerRightPoint.X && shape.Y < pointsBoundary.UpperLeftPoint.Y && shape.Y > pointsBoundary.LowerRightPoint.Y)
                {
                    UpdatePointShape(shape, direction);
                }
                else
                {
                    id = ReverseDirection(shape, direction).ToString();
                }
                features.Add(new Feature(shape.X, shape.Y, id));
            }

            pointsLayer.InternalFeatures.Clear();
            foreach (Feature item in features)
            {
                pointsLayer.InternalFeatures.Add(item);
            }

            pointsOverlay.Refresh();
        }

        private object ReverseDirection(PointShape shape, PanDirection direction)
        {
            PanDirection newDirection = PanDirection.Down;

            switch (direction)
            {
                case PanDirection.Up:
                    newDirection = PanDirection.Down;
                    break;

                case PanDirection.UpperRight:
                    newDirection = PanDirection.LowerLeft;
                    break;

                case PanDirection.Right:
                    newDirection = PanDirection.Left;
                    break;

                case PanDirection.LowerRight:
                    newDirection = PanDirection.UpperLeft;
                    break;

                case PanDirection.Down:
                    newDirection = PanDirection.Up;
                    break;

                case PanDirection.LowerLeft:
                    newDirection = PanDirection.UpperRight;
                    break;

                case PanDirection.Left:
                    newDirection = PanDirection.Right;
                    break;

                case PanDirection.UpperLeft:
                    newDirection = PanDirection.LowerRight;
                    break;

                default:
                    break;
            }

            UpdatePointShape(shape, newDirection);

            return newDirection;
        }

        private void UpdatePointShape(PointShape shape, PanDirection direction)
        {
            switch (direction)
            {
                case PanDirection.Up:
                    shape.Y++;
                    break;

                case PanDirection.UpperRight:
                    shape.X++;
                    shape.Y++;
                    break;

                case PanDirection.Right:
                    shape.X++;
                    break;

                case PanDirection.LowerRight:
                    shape.X++;
                    shape.Y--;
                    break;

                case PanDirection.Down:
                    shape.Y--;
                    break;

                case PanDirection.LowerLeft:
                    shape.X--;
                    shape.Y--;
                    break;

                case PanDirection.Left:
                    shape.X--;
                    break;

                case PanDirection.UpperLeft:
                    shape.X--;
                    shape.Y++;
                    break;

                default:
                    break;
            }
        }

        private object GetRandomDirection()
        {
            int i = random.Next(8);

            PanDirection panDirection = (PanDirection)i;

            return panDirection;
        }

        private PointShape GetRandomPoint(RectangleShape currentExtent)
        {
            double x = random.Next((int)currentExtent.UpperLeftPoint.X, (int)currentExtent.LowerRightPoint.X);
            double y = random.Next((int)currentExtent.LowerRightPoint.Y, (int)currentExtent.UpperLeftPoint.Y);

            PointShape pointShape = new PointShape(x, y);

            return pointShape;
        }
    }
}