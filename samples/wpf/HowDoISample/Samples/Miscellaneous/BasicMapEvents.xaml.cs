using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to handle basic map events.
    /// </summary>
    public partial class BasicMapEvents : IDisposable
    {
        public class LogEntry
        {
            public string Category { get; set; }
            public string Message { get; set; }
        }

        public class LogEntryView
        {
            public int Index { get; }
            public string Category { get; }
            public string Message { get; }

            public LogEntryView(int index, LogEntry log)
            {
                Index = index;
                Category = log.Category;
                Message = log.Message;
            }
        }

        private ShapeFileFeatureLayer _friscoCityBoundary;
        public ObservableCollection<LogEntry> LogMessages { get; } = new ObservableCollection<LogEntry>();
        public ObservableCollection<LogEntryView> FilteredLogMessages { get; } = new ObservableCollection<LogEntryView>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _showMapViewLogs = true;
        public bool ShowMapViewLogs
        {
            get => _showMapViewLogs;
            set
            {
                _showMapViewLogs = value;
                FilterLogMessages();
            }
        }

        private bool _showExtentOverlayLogs = true;
        public bool ShowExtentOverlayLogs
        {
            get => _showExtentOverlayLogs;
            set
            {
                _showExtentOverlayLogs = value;
                FilterLogMessages();
            }
        }

        private bool _showExtentOverlayMouseMoveLogs = false;
        public bool ShowExtentOverlayMouseMoveLogs
        {
            get => _showExtentOverlayMouseMoveLogs;
            set
            {
                if (_showExtentOverlayMouseMoveLogs != value)
                {
                    _showExtentOverlayMouseMoveLogs = value;
                    OnPropertyChanged(nameof(ShowExtentOverlayMouseMoveLogs));
                    FilterLogMessages();
                }
            }
        }

        private bool _showLayerOverlayLogs = true;
        public bool ShowLayerOverlayLogs
        {
            get => _showLayerOverlayLogs;
            set
            {
                _showLayerOverlayLogs = value;
                FilterLogMessages();
            }
        }

        private bool _showShapeFileLogs = true;
        public bool ShowShapeFileLogs
        {
            get => _showShapeFileLogs;
            set
            {
                _showShapeFileLogs = value;
                FilterLogMessages();
            }
        }

        public BasicMapEvents()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            // ============================================================
            // [1] MapView Event Bindings
            // ============================================================

            MapView.CurrentExtentChanging += MapView_CurrentExtentChanging;
            MapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
            MapView.CurrentExtentChangedInAnimation += MapView_CurrentExtentChangedInAnimation;
            MapView.CurrentScaleChanging += MapView_CurrentScaleChanging;
            MapView.CurrentScaleChanged += MapView_CurrentScaleChanged;
            MapView.MapClick += MapView_MapClick;
            MapView.MapDoubleClick += MapView_MapDoubleClick;
            MapView.OverlayDrawing += MapView_OverlayDrawing;
            MapView.OverlayDrawn += MapView_OverlayDrawn;
            MapView.OverlaysDrawing += MapView_OverlaysDrawing;
            MapView.OverlaysDrawn += MapView_OverlaysDrawn;
            MapView.PropertyChanged += MapView_PropertyChanged;
            MapView.RotationAngleChanging += MapView_RotationAngleChanging;

            // ============================================================
            // [2] ExtentOverlay Event Bindings
            // ============================================================

            MapView.ExtentOverlay.Drawing += ExtentOverlay_Drawing;
            MapView.ExtentOverlay.Drawn += ExtentOverlay_Drawn;
            MapView.ExtentOverlay.MapKeyDown += ExtentOverlay_MapKeyDown;
            MapView.ExtentOverlay.MapKeyUp += ExtentOverlay_MapKeyUp;
            MapView.ExtentOverlay.MapMouseClick += ExtentOverlay_MapMouseClick;
            MapView.ExtentOverlay.MapMouseDoubleClick += ExtentOverlay_MapMouseDoubleClick;
            MapView.ExtentOverlay.MapMouseDown += ExtentOverlay_MapMouseDown;
            MapView.ExtentOverlay.MapMouseEnter += ExtentOverlay_MapMouseEnter;
            MapView.ExtentOverlay.MapMouseLeave += ExtentOverlay_MapMouseLeave;
            MapView.ExtentOverlay.MapMouseMove += ExtentOverlay_MapMouseMove;
            MapView.ExtentOverlay.MapMouseUp += ExtentOverlay_MapMouseUp;
            MapView.ExtentOverlay.MapMouseWheel += ExtentOverlay_MapMouseWheel;
            MapView.ExtentOverlay.ThrowingException += ExtentOverlay_ThrowingException;

            // ============================================================
            // [3] Data & Layer Initialization (Non-event code)
            // ============================================================

            _friscoCityBoundary = new ShapeFileFeatureLayer(@"./Data/Shapefile/City_ETJ.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(100, GeoColors.Blue), GeoColors.DimGray, 2);
            _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();

            // ============================================================
            // [4] LayerOverlay Event Bindings
            // ============================================================

            layerOverlay.CreatingSKTypefacesForCharacter += LayerOverlay_CreatingSKTypefacesForCharacter;
            layerOverlay.Drawing += LayerOverlay_Drawing;
            layerOverlay.Drawn += LayerOverlay_Drawn;
            layerOverlay.DrawTilesProgressChanged += LayerOverlay_DrawTilesProgressChanged;
            layerOverlay.ThrowingException += LayerOverlay_ThrowingException;
            layerOverlay.DrawingTile += LayerOverlay_DrawingTile;
            layerOverlay.DrawnTile += LayerOverlay_DrawnTile;
            layerOverlay.TileCacheGenerated += LayerOverlay_TileCacheGenerated;
            layerOverlay.TileTypeChanged += LayerOverlay_TileTypeChanged;
            layerOverlay.TileTypeChanging += LayerOverlay_TileTypeChanging;

            layerOverlay.Layers.Add(_friscoCityBoundary);
            MapView.Overlays.Add(layerOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778500, 3916460);
            MapView.CurrentScale = 144450;

            _ = MapView.RefreshAsync();

            // ============================================================
            // [5] ShapeFileFeatureLayer Event Bindings
            // ============================================================

            _friscoCityBoundary.DrawingProgressChanged += _friscoCityBoundary_DrawingProgressChanged;
            _friscoCityBoundary.DrawingFeatures += _friscoCityBoundary_DrawingFeatures;
            _friscoCityBoundary.DrawingException += _friscoCityBoundary_DrawingException;
            _friscoCityBoundary.DrawnException += _friscoCityBoundary_DrawnException;
            _friscoCityBoundary.StreamLoading += _friscoCityBoundary_StreamLoading;
        }


        // MapView Events Triggered Methods
        private void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            var center = e.NewExtent.GetCenterPoint();
            AppendLog("MapView", $"CurrentExtentChanging to CenterPoint " + $"X: {center.X:0}, " + $"Y: {center.Y:0}");
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = e.NewExtent.GetCenterPoint();
            AppendLog("MapView", $"CurrentExtentChanged to CenterPoint " + $"X: {center.X:0}, " + $"Y: {center.Y:0}");
        }

        private void MapView_CurrentExtentChangedInAnimation(object sender, CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
            var progressPercent = e.Progress * 100;
            var from = e.FromCenterPoint;
            var to = e.ToCenterPoint;

            var zoomIntent =
                e.ToResolution < e.FromResolution ? "Zoom In" :
                e.ToResolution > e.FromResolution ? "Zoom Out" :
                "Pan";

            var message =
                $"CurrentExtentChangedInAnimation: {progressPercent:0}% | " +
                $"Center ({from.X:0},{from.Y:0} → {to.X:0},{to.Y:0}) | " +
                $"{zoomIntent}";

            AppendLog("MapView", message);
        }

        private void MapView_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {
            AppendLog("MapView", $"CurrentScaleChanging to: {(int)e.NewScale::0}");
        }

        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            AppendLog("MapView", $"CurrentScaleChanged to: {(int)e.NewScale:0}");
        }

        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog("MapView", $"MapClick: Button={e.MouseButton}, Location=({e.WorldLocation.X:0},{e.WorldLocation.Y:0})");
        }

        private void MapView_MapDoubleClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog("MapView", $"MapDoubleClick: Button={e.MouseButton}, Location=({e.WorldLocation.X:0},{e.WorldLocation.Y:0})");
        }

        private void MapView_OverlayDrawing(object sender, OverlayDrawingMapViewEventArgs e)
        {
            AppendLog("MapView", $"OverlayDrawing: {(e.Overlay == null ? "null" : e.Overlay.GetType().Name)}");
        }

        private void MapView_OverlayDrawn(object sender, OverlayDrawnMapViewEventArgs e)
        {
            AppendLog("MapView", $"OverlayDrawn: {(e.Overlay == null ? "null" : e.Overlay.GetType().Name)}");
        }

        private void MapView_OverlaysDrawing(object sender, OverlaysDrawingMapViewEventArgs e)
        {
            var names = string.Join(", ", e.Overlays.Select(o => o?.GetType().Name ?? "null"));
            AppendLog("MapView", $"OverlaysDrawing: {names}");
        }

        private void MapView_OverlaysDrawn(object sender, OverlaysDrawnMapViewEventArgs e)
        {
            var names = string.Join(", ", e.Overlays.Select(o => o?.GetType().Name ?? "null"));
            AppendLog("MapView", $"OverlaysDrawn: {names}");
        }

        private void MapView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AppendLog("MapView", "PropertyChanged");
        }

        private void MapView_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            AppendLog("MapView", $"RotationAngleChanging: Angle={e.NewRotationAngle:0.0}°");
        }


        // ExtentOverlay Events Triggered Methods

        private void ExtentOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            var c = e.WorldExtent.GetCenterPoint();
            AppendLog("ExtentOverlay", $"Drawing overlay at map center: ({c.X:0},{c.Y:0})");
        }

        private void ExtentOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            var c = e.WorldExtent.GetCenterPoint();
            AppendLog("ExtentOverlay", $"Drawn overlay at map center: ({c.X:0},{c.Y:0})");
        }

        private void ExtentOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            AppendLog("ExtentOverlay", "MapKeyDown");
        }

        private void ExtentOverlay_MapKeyUp(object sender, MapKeyUpInteractiveOverlayEventArgs e)
        {
            AppendLog("ExtentOverlay", "MapKeyUp");
        }

        private void ExtentOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MouseClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void ExtentOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MouseDoubleClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void ExtentOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MapMouseDown at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void ExtentOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MouseEnter at world ({args.WorldX:0},{args.WorldY:0})");
        }

        private void ExtentOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            AppendLog("ExtentOverlay", "MapMouseLeave");
        }

        private void ExtentOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MapMouseMove at world ({args.WorldX:0},{args.WorldY:0})");
        }

        private void ExtentOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("ExtentOverlay", $"MapMouseUp at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void ExtentOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            var delta = args.MouseWheelDelta;
            string zoomIntent = delta > 0 ? "ZoomIn" : "ZoomOut";
            var message = $"MouseWheel {zoomIntent} at world ({args.WorldX:0},{args.WorldY:0}), WheelDelta={delta}";
            AppendLog("ExtentOverlay", message);
        }

        private void ExtentOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            AppendLog("ExtentOverlay", $"Exception occurred in overlay: {e.Exception.Message}");
        }

        // LayerOverlay Events Triggered Methods

        // Raised when Skia creates or selects a typeface for rendering a specific character.
        private void LayerOverlay_CreatingSKTypefacesForCharacter(object sender, CreatingSKTypefaceForCharacterEventArgs e)
        {
            AppendLog("LayerOverlay", "CreatingSKTypefacesForCharacter");
        }

        private void LayerOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            var c = e.WorldExtent.GetCenterPoint();
            AppendLog("LayerOverlay", $"Drawing started at map center ({c.X:0},{c.Y:0})");
        }

        private void LayerOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            var c = e.WorldExtent.GetCenterPoint();
            AppendLog("LayerOverlay", $"Drawing completed at map center ({c.X:0},{c.Y:0})");
        }

        private void LayerOverlay_DrawTilesProgressChanged(object sender, DrawTilesProgressChangedTileOverlayEventArgs e)
        {
            AppendLog("LayerOverlay", $"DrawTilesProgressChanged: {e.ProgressPercentage}%");
        }

        private void LayerOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            AppendLog("LayerOverlay", "ThrowingException");
        }

        private void LayerOverlay_DrawingTile(object sender, DrawingTileTileOverlayEventArgs e)
        {
            var t = e.DrawingTile;
            AppendLog("LayerOverlay", $"DrawingTile ({t.Zoom}-{t.X}-{t.Y})");
        }

        private void LayerOverlay_DrawnTile(object sender, DrawnTileTileOverlayEventArgs e)
        {
            var t = e.DrawnTile;
            AppendLog("LayerOverlay", $"DrawnTile ({t.Zoom}-{t.X}-{t.Y})");
        }

        private void LayerOverlay_TileCacheGenerated(object sender, TileCacheGeneratedLayerOverlayEventArgs e)
        {
            var t = e.Tile;
            AppendLog("LayerOverlay", $"TileCacheGenerated ({t.ZoomIndex}-{t.X}-{t.Y})");
        }

        private void LayerOverlay_TileTypeChanged(object sender, TileTypeChangedTileOverlayEventArgs e)
        {
            AppendLog("LayerOverlay", $"TileTypeChanged: {e.CurrentTileType}");
        }

        private void LayerOverlay_TileTypeChanging(object sender, TileTypeChangingTileOverlayEventArgs e)
        {
            AppendLog("LayerOverlay", $"TileTypeChanging: {e.CurrentTileType}");
        }


        // FeatureLayer Events Triggered Methods
        private void _friscoCityBoundary_DrawingProgressChanged(object sender, DrawingProgressChangedEventArgs e)
        {
            AppendLog("FeatureLayer", $"DrawingProgressChanged [Progress Percentage: {e.ProgressPercentage}%]");
        }

        private void _friscoCityBoundary_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            AppendLog("FeatureLayer", $"DrawingFeatures [Zoom Level Scale: {e.DrawingZoomLevel.Scale}]");
        }

        private void _friscoCityBoundary_DrawingException(object sender, DrawingExceptionLayerEventArgs e)
        {
            AppendLog("FeatureLayer", $"DrawingException [Canvas: {e.Canvas}, Exception: {e.Exception.Message}]");
        }

        private void _friscoCityBoundary_DrawnException(object sender, DrawnExceptionLayerEventArgs e)
        {
            AppendLog("FeatureLayer", $"DrawnException [Canvas: {e.Canvas}, Exception: {e.Exception.Message}]");
        }

        private void _friscoCityBoundary_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            AppendLog("FeatureLayer", $"StreamLoading [Stream: {e.AlternateStreamName}, StreamType: {e.StreamType}%]");
        }

        private void FilterLogMessages()
        {
            FilteredLogMessages.Clear();

            var filtered = LogMessages.Where(log =>
                (ShowMapViewLogs && log.Category == "MapView") ||
                (ShowExtentOverlayLogs && log.Category == "ExtentOverlay" &&
                (ShowExtentOverlayMouseMoveLogs || !log.Message.Contains("MapMouseMove"))) ||
                (ShowLayerOverlayLogs && log.Category == "LayerOverlay") ||
                (ShowShapeFileLogs && log.Category == "FeatureLayer"))
                .ToList();

            int idx = 1;
            foreach (var log in filtered)
            {
                FilteredLogMessages.Add(new LogEntryView(idx++, log));
            }
        }

        public void AppendLog(string category, string message)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => AppendLog(category, message));
                return;
            }

            var log = new LogEntry
            {
                Category = category,
                Message = message
            };

            LogMessages.Add(log);

            if (IsLogVisible(log))
            {
                FilteredLogMessages.Add(
                    new LogEntryView(FilteredLogMessages.Count + 1, log));

                LogListBox.ScrollIntoView(
                    FilteredLogMessages[FilteredLogMessages.Count - 1]);
            }
        }

        private bool IsLogVisible(LogEntry log)
        {
            if (ShowMapViewLogs && log.Category == "MapView")
                return true;
            if (ShowExtentOverlayLogs && log.Category == "ExtentOverlay")
                return ShowExtentOverlayMouseMoveLogs ||
                       !log.Message.Contains("MapMouseMove");
            if (ShowLayerOverlayLogs && log.Category == "LayerOverlay")
                return true;
            if (ShowShapeFileLogs && log.Category == "FeatureLayer")
                return true;
            return false;
        }

        private void ClearLogs_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Clear();
            FilteredLogMessages.Clear();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}