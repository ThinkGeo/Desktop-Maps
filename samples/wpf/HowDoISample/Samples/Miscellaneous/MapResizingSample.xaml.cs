using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This samples shows how to resize map while keeping width of world extend unchanged.
    /// </summary>
    public partial class MapResizingSample
    {
        private bool _requireExtentUpdateBySizeChange;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public MapResizingSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with open street map overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            MapView.ZoomLevelSnappingMode = ZoomLevelSnappingMode.None;

            InitializeMapViewInternal(MapView);

            var overlay = new OpenStreetMapOverlay
            {
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.OsmMaps
            };

            MapView.Overlays.Add("base", overlay);

            MapView.MapResizeMode = MapResizeMode.PreserveExtent;

            MapView.CurrentExtent = GetDrawingExtent(MaxExtents.OsmMaps, MapView.ActualWidth, MapView.ActualHeight);

            await MapView.RefreshAsync();
        }

        private async void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Extent changing from {e.OldExtent} to {e.NewExtent}.");

            if (!_requireExtentUpdateBySizeChange) return;
            var newExtent = MapUtil.GetDrawingExtent(e.OldExtent, MapView.ActualWidth, MapView.ActualHeight);
            _requireExtentUpdateBySizeChange = false;
            if (!(newExtent.Width > MaxExtents.OsmMaps.Width)) return; //Duplicated continents will be displayed
            e.Cancel = true;

            await Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new Action(Action));
            return;

            async void Action() => await UpdateExtent(e);
        }

        private async Task UpdateExtent(CurrentExtentChangingMapViewEventArgs e)
        {
            MapView.CurrentExtent = GetDrawingExtent(e.OldExtent, MapView.ActualWidth, MapView.ActualHeight);
            await MapView.RefreshAsync();
        }

        private void MapView_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Scaling changing from {e.OldScale} to {e.NewScale}.");
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Extent changed from {e.OldExtent} to {e.NewExtent}.");
        }

        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Scaling changed from {e.OldScale} to {e.NewScale}.");
        }

        private async void MapView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine($"Size is changed from {e.PreviousSize} to {e.NewSize}");
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            var customZoomLevelSet = new ZoomLevelSet();
            var newExtent = GetExtent(MapView.CurrentExtent, e.NewSize.Width, e.NewSize.Height);
            var baseScale = MapUtil.GetScale(MapView.MapUnit, newExtent, e.NewSize.Width, e.NewSize.Height);
            baseScale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);

            var scale = baseScale;

            while (scale > customZoomLevelSet.ZoomLevel20.Scale)
            {
                customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(scale));
                scale /= 2;
            }

            MapView.ZoomLevelSet = customZoomLevelSet;
            MapView.CurrentExtent = newExtent;

            await MapView.RefreshAsync(OverlayRefreshType.Redraw, _cancellationTokenSource.Token);
        }

        private static RectangleShape GetExtent(RectangleShape maxExtent, double width, double height)
        {
            var center = maxExtent.GetCenterPoint();
            var resolution = maxExtent.Width / width;
            var halfWidth = resolution * width / 2;
            var halfHeight = resolution * height / 2;
            return new RectangleShape(center.X - halfWidth, center.Y + halfHeight, center.X + halfWidth, center.Y - halfHeight);
        }

        private static RectangleShape GetDrawingExtent(RectangleShape worldExtent, double screenWidth, double screenHeight)
        {
            var screenRatio = screenHeight / screenWidth;
            var worldRatio = worldExtent.Height / worldExtent.Width;

            if (Math.Abs(worldRatio - screenRatio) <= double.Epsilon)
            {
                return (RectangleShape)worldExtent.CloneDeep();
            }

            var worldWidth = worldExtent.Width;
            var worldHeight = worldExtent.Height * screenRatio / worldRatio;

            var centerPoint = worldExtent.GetCenterPoint();
            var pointShape = new PointShape(centerPoint.X - worldWidth * 0.5, centerPoint.Y + worldHeight * 0.5);
            var lowerRightPoint = new PointShape(pointShape.X + worldWidth, pointShape.Y - worldHeight);
            return new RectangleShape(pointShape, lowerRightPoint);
        }

        private static void InitializeMapViewInternal(MapViewBase mapView)
        {
            if (!mapView.IsLoaded)
                return;

            mapView.ZoomLevelSet = new ZoomLevelSet();

            mapView.ZoomLevelSnappingMode = ZoomLevelSnappingMode.None;
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel02);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel03);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel04);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel05);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel06);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel07);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel08);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel09);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel10);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel11);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel12);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel13);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel14);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel15);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel16);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel17);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel18);
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel19);
        }
    }
}