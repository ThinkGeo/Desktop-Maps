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
    public partial class ResizeTheMap
    {
        private bool _requireExtentUpdateBySizeChange;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public ResizeTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with open street map overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MapView.MapUnit = GeographyUnit.Meter;

                InitializeMapViewInternal(MapView);

                var overlay = new OpenStreetMapOverlay
                {
                    WrappingMode = WrappingMode.WrapDateline,
                    WrappingExtent = MaxExtents.OsmMaps
                };

                MapView.Overlays.Add("base", overlay);

                MapView.MapResizeMode = MapResizeMode.PreserveExtent;

                // Get the center point of the MaxExtents.OsmMaps
                MapView.CenterPoint = MaxExtents.OsmMaps.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MaxExtents.OsmMaps, MapView.ActualWidth, MapView.MapUnit);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            try
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
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async Task UpdateExtent(CurrentExtentChangingMapViewEventArgs e)
        {
            MapView.CenterPoint = e.OldExtent.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(e.OldExtent, MapView.ActualWidth, MapView.MapUnit);
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
            try
            {
                if (MapView.CurrentExtent == null)
                    return;
                Debug.WriteLine($"Size is changed from {e.PreviousSize} to {e.NewSize}");
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();

                // Capture the current center of the map
                var currentCenter = MapView.CurrentExtent.GetCenterPoint();

                // Recalculate the extent only if the width or height actually changed
                if (e.NewSize.Width != e.PreviousSize.Width || e.NewSize.Height != e.PreviousSize.Height)
                {
                    // Calculate the extent based on the maximum world extent to prevent duplication
                    var newExtent = GetDrawingExtent(currentCenter, MapView.ActualWidth, MapView.ActualHeight);

                    // Set custom zoom levels based on the new extent size
                    var baseScale = MapUtil.GetScale(newExtent, MapView.ActualWidth, MapView.MapUnit);
                    var customZoomLevelSet = new ZoomLevelSet();
                    baseScale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);

                    //var scale = baseScale;
                    var scale = Math.Max(baseScale, customZoomLevelSet.ZoomLevel08.Scale);
                    while (scale > customZoomLevelSet.ZoomLevel20.Scale)
                    {
                        customZoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(scale));
                        scale /= 2;
                    }

                    MapView.ZoomLevelSet = customZoomLevelSet;
                    MapView.CenterPoint = newExtent.GetCenterPoint();
                    MapView.CurrentScale = MapUtil.GetScale(newExtent,MapView.ActualWidth, MapView.MapUnit);
                    await MapView.RefreshAsync(OverlayRefreshType.Redraw, _cancellationTokenSource.Token);
                }
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        // Helper method to calculate the new extent based on the center point and dimensions
        private static RectangleShape GetDrawingExtent(PointShape centerPoint, double width, double height)
        {
            var resolution = MaxExtents.OsmMaps.Width / width;
            var halfWidth = resolution * width / 2;
            var halfHeight = resolution * height / 2;

            return new RectangleShape(
                centerPoint.X - halfWidth,
                centerPoint.Y + halfHeight,
                centerPoint.X + halfWidth,
                centerPoint.Y - halfHeight
            );
        }

        private static void InitializeMapViewInternal(MapViewBase mapView)
        {
            if (!mapView.IsLoaded)
                return;

            mapView.ZoomLevelSet = new ZoomLevelSet();

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