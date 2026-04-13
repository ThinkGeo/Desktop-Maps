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

        private bool _initialized;
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

        private bool _showMapLogs = true;
        public bool ShowMapLogs
        {
            get => _showMapLogs;
            set
            {
                _showMapLogs = value;
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

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // ============================================================
            // [1] Map Event Bindings
            // ============================================================

            Map.CurrentExtentChanging += Map_CurrentExtentChanging;
            Map.CurrentExtentChanged += Map_CurrentExtentChanged;
            Map.CurrentExtentChangedInAnimation += Map_CurrentExtentChangedInAnimation;
            Map.CurrentScaleChanging += Map_CurrentScaleChanging;
            Map.CurrentScaleChanged += Map_CurrentScaleChanged;
            Map.MapClick += Map_MapClick;
            Map.MapDoubleClick += Map_MapDoubleClick;
            Map.OverlayDrawing += Map_OverlayDrawing;
            Map.OverlayDrawn += Map_OverlayDrawn;
            Map.OverlaysDrawing += Map_OverlaysDrawing;
            Map.OverlaysDrawn += Map_OverlaysDrawn;
            Map.PropertyChanged += Map_PropertyChanged;
            Map.RotationAngleChanging += Map_RotationAngleChanging;

            // ============================================================
            // [2] ExtentOverlay Event Bindings
            // ============================================================

            Map.ExtentOverlay.Drawing += ExtentOverlay_Drawing;
            Map.ExtentOverlay.Drawn += ExtentOverlay_Drawn;
            Map.ExtentOverlay.MapKeyDown += ExtentOverlay_MapKeyDown;
            Map.ExtentOverlay.MapKeyUp += ExtentOverlay_MapKeyUp;
            Map.ExtentOverlay.MapMouseClick += ExtentOverlay_MapMouseClick;
            Map.ExtentOverlay.MapMouseDoubleClick += ExtentOverlay_MapMouseDoubleClick;
            Map.ExtentOverlay.MapMouseDown += ExtentOverlay_MapMouseDown;
            Map.ExtentOverlay.MapMouseEnter += ExtentOverlay_MapMouseEnter;
            Map.ExtentOverlay.MapMouseLeave += ExtentOverlay_MapMouseLeave;
            Map.ExtentOverlay.MapMouseMove += ExtentOverlay_MapMouseMove;
            Map.ExtentOverlay.MapMouseUp += ExtentOverlay_MapMouseUp;
            Map.ExtentOverlay.MapMouseWheel += ExtentOverlay_MapMouseWheel;
            Map.ExtentOverlay.ThrowingException += ExtentOverlay_ThrowingException;

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
            Map.Overlays.Add(layerOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778500, 3916460);
            Map.CurrentScale = 144450;

            _ = Map.RefreshAsync();

            // ============================================================
            // [5] ShapeFileFeatureLayer Event Bindings
            // ============================================================

            _friscoCityBoundary.DrawingProgressChanged += _friscoCityBoundary_DrawingProgressChanged;
            _friscoCityBoundary.DrawingFeatures += _friscoCityBoundary_DrawingFeatures;
            _friscoCityBoundary.DrawingException += _friscoCityBoundary_DrawingException;
            _friscoCityBoundary.DrawnException += _friscoCityBoundary_DrawnException;
            _friscoCityBoundary.StreamLoading += _friscoCityBoundary_StreamLoading;
        }


        // Map Events Triggered Methods
        private void Map_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            if (e.NewExtent == null) return;

            var center = e.NewExtent.GetCenterPoint();
            AppendLog("Map", $"CurrentExtentChanging to CenterPoint " + $"X: {center.X:0}, " + $"Y: {center.Y:0}");
        }

        private void Map_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            if (e.NewExtent == null) return;

            var center = e.NewExtent.GetCenterPoint();
            AppendLog("Map", $"CurrentExtentChanged to CenterPoint " + $"X: {center.X:0}, " + $"Y: {center.Y:0}");
        }

        private void Map_CurrentExtentChangedInAnimation(object sender, CurrentExtentChangedInAnimationMapViewEventArgs e)
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

            AppendLog("Map", message);
        }

        private void Map_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {
            AppendLog("Map", $"CurrentScaleChanging to: {(int)e.NewScale::0}");
        }

        private void Map_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            AppendLog("Map", $"CurrentScaleChanged to: {(int)e.NewScale:0}");
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog("Map", $"MapClick: Button={e.MouseButton}, Location=({e.WorldLocation.X:0},{e.WorldLocation.Y:0})");
        }

        private void Map_MapDoubleClick(object sender, MapClickMapViewEventArgs e)
        {
            AppendLog("Map", $"MapDoubleClick: Button={e.MouseButton}, Location=({e.WorldLocation.X:0},{e.WorldLocation.Y:0})");
        }

        private void Map_OverlayDrawing(object sender, OverlayDrawingMapViewEventArgs e)
        {
            AppendLog("Map", $"OverlayDrawing: {(e.Overlay == null ? "null" : e.Overlay.GetType().Name)}");
        }

        private void Map_OverlayDrawn(object sender, OverlayDrawnMapViewEventArgs e)
        {
            AppendLog("Map", $"OverlayDrawn: {(e.Overlay == null ? "null" : e.Overlay.GetType().Name)}");
        }

        private void Map_OverlaysDrawing(object sender, OverlaysDrawingMapViewEventArgs e)
        {
            var names = string.Join(", ", e.Overlays.Select(o => o?.GetType().Name ?? "null"));
            AppendLog("Map", $"OverlaysDrawing: {names}");
        }

        private void Map_OverlaysDrawn(object sender, OverlaysDrawnMapViewEventArgs e)
        {
            var names = string.Join(", ", e.Overlays.Select(o => o?.GetType().Name ?? "null"));
            AppendLog("Map", $"OverlaysDrawn: {names}");
        }

        private void Map_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AppendLog("Map", "PropertyChanged");
        }

        private void Map_RotationAngleChanging(object sender, RotationAngleChangingMapViewEventArgs e)
        {
            AppendLog("Map", $"RotationAngleChanging: Angle={e.NewRotationAngle:0.0}°");
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
                (ShowMapLogs && log.Category == "Map") ||
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
            if (ShowMapLogs && log.Category == "Map")
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
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}

