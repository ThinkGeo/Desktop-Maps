using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    /// <summary>
    /// This samples shows how to resize map while keeping width of world extend unchanged.
    /// </summary>
    public partial class MapResizingSample : UserControl
    {
        private bool _requireExtentUpdateBySizeChange = false;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public MapResizingSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with open street map overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSnappingMode = ZoomLevelSnappingMode.None;

            InitializeMapViewInternal(mapView);

            var overlay = new OpenStreetMapOverlay();
            overlay.WrappingMode = WrappingMode.WrapDateline;
            overlay.WrappingExtent = MaxExtents.OsmMaps;

            mapView.Overlays.Add("base", overlay);

            mapView.MapResizeMode = MapResizeMode.PreserveExtent;

            mapView.CurrentExtent = GetDrawingExtent(MaxExtents.OsmMaps, mapView.ActualWidth, mapView.ActualHeight);

            await mapView.RefreshAsync();
        }

        private async void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            Debug.WriteLine($"Current Extent changing from {e.OldExtent} to {e.NewExtent}.");

            if (_requireExtentUpdateBySizeChange)
            {
                var newExtent = MapUtil.GetDrawingExtent(e.OldExtent, mapView.ActualWidth, mapView.ActualHeight);
                _requireExtentUpdateBySizeChange = false;
                if (newExtent.Width > MaxExtents.OsmMaps.Width) //Duplicated continents will be displayed
                {
                    e.Cancel = true;
                    await Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new Action(async () => await UpdateExtent(e)));
                }
            }
        }

        private async Task UpdateExtent(CurrentExtentChangingMapViewEventArgs e)
        {
            mapView.CurrentExtent = GetDrawingExtent(e.OldExtent, mapView.ActualWidth, mapView.ActualHeight);
            await mapView.RefreshAsync();
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
            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            var customZoomLevelSet = new ZoomLevelSet();
            var newExtent = GetExtent(mapView.CurrentExtent, e.NewSize.Width, e.NewSize.Height);
            var baseScale = MapUtil.GetScale(mapView.MapUnit, newExtent, e.NewSize.Width, e.NewSize.Height);
            baseScale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);

            var scale = baseScale;

            while (scale > customZoomLevelSet.ZoomLevel20.Scale)
            {
                customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(scale));
                scale /= 2;
            }

            mapView.ZoomLevelSet = customZoomLevelSet;
            mapView.CurrentExtent = newExtent;

            await mapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);
        }

        private RectangleShape GetExtent(RectangleShape maxExtent, double width, double height)
        {
            var center = maxExtent.GetCenterPoint();
            var resolution = maxExtent.Width / width;
            var halfWidth = resolution * width / 2;
            var halfHeight = resolution * height / 2;
            return new RectangleShape(center.X - halfWidth, center.Y + halfHeight, center.X + halfWidth, center.Y - halfHeight);
        }

        private RectangleShape GetDrawingExtent(RectangleShape worldExtent, double screenWidth, double screenHeight)
        {
            double screenRatio = screenHeight / screenWidth;
            double worldRatio = worldExtent.Height / worldExtent.Width;

            if (Math.Abs(worldRatio - screenRatio) <= double.Epsilon)
            {
                return (RectangleShape)worldExtent.CloneDeep();
            }

            double worldWidth = worldExtent.Width;
            double worldHeight = worldExtent.Height * screenRatio / worldRatio;

            PointShape centerPoint = worldExtent.GetCenterPoint();
            PointShape pointShape = new PointShape(centerPoint.X - worldWidth * 0.5, centerPoint.Y + worldHeight * 0.5);
            PointShape lowerRightPoint = new PointShape(pointShape.X + worldWidth, pointShape.Y - worldHeight);
            return new RectangleShape(pointShape, lowerRightPoint);
        }

        private void InitializeMapViewInternal(MapView mapView)
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