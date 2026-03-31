using Esri.FileGDB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to draw, edit, or delete shapes using the map's TrackOverlay and EditOverlay,
    /// and how to listen to related interactive and editing events.
    /// </summary>
    public partial class EditMapEvents : IDisposable
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

        public ObservableCollection<LogEntry> LogMessages { get; } = new ObservableCollection<LogEntry>();
        public ObservableCollection<LogEntryView> FilteredLogMessages { get; } = new ObservableCollection<LogEntryView>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _initialized;
        LayerOverlay _layerOverlay = null;

        private bool _showTrackOverlayLogs = true;
        public bool ShowTrackOverlayLogs
        {
            get => _showTrackOverlayLogs;
            set
            {
                if (_showTrackOverlayLogs != value)
                {
                    _showTrackOverlayLogs = value;
                    OnPropertyChanged(nameof(ShowTrackOverlayLogs));
                    FilterLogMessages();
                }
            }
        }

        private bool _showTrackOverlayMouseMoveLogs = false;
        public bool ShowTrackOverlayMouseMoveLogs
        {
            get => _showTrackOverlayMouseMoveLogs;
            set
            {
                _showTrackOverlayMouseMoveLogs = value;
                OnPropertyChanged(nameof(ShowTrackOverlayMouseMoveLogs));
                FilterLogMessages();
            }
        }

        private bool _showEditOverlayLogs = true;
        public bool ShowEditOverlayLogs
        {
            get => _showEditOverlayLogs;
            set
            {
                _showEditOverlayLogs = value;
                OnPropertyChanged(nameof(ShowEditOverlayLogs));
                FilterLogMessages();
            }
        }

        private bool _showEditOverlayMouseMoveLogs = false;
        public bool ShowEditOverlayMouseMoveLogs
        {
            get => _showEditOverlayMouseMoveLogs;
            set
            {
                _showEditOverlayMouseMoveLogs = value;
                OnPropertyChanged(nameof(ShowEditOverlayMouseMoveLogs));
                FilterLogMessages();
            }
        }

        private bool _eventsExpanded;
        private void ToggleEditEvents_Click(object sender, RoutedEventArgs e)
        {
            _eventsExpanded = !_eventsExpanded;

            RightPanelColumn.Width = _eventsExpanded
                ? new GridLength(500)
                : new GridLength(300);

            EventsColumn.Width = _eventsExpanded
                ? new GridLength(200)
                : new GridLength(0);

            EditEventsPanel.Visibility =
            LogListBox.Visibility =
                _eventsExpanded ? Visibility.Visible : Visibility.Collapsed;

            if (sender is Button btn)
            {
                btn.Content = _eventsExpanded
                    ? "Hide Edit Events ◀"
                    : "Show Edit Events ▶";
            }
        }

        public EditMapEvents()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;
            Map.MapClick += Map_MapClick;

            var demoPolygon = new Feature("POLYGON((-10778500 3915600,-10778500 3910000,-10774040 3910000,-10774040 3915600,-10778500 3915600))");
            var demoPoint = new Feature("POINT(-10773220 3913230)");
            var demoLine = new Feature("LINESTRING(-10780700 3916500, -10780700 3910040)");
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPolygon);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPoint);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoLine);
            Map.EditOverlay.CalculateAllControlPoints();

            // Create the layer that will store the drawn shapes
            var featureLayer = new InMemoryFeatureLayer();

            // Add styles for the layer
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to a LayerOverlay
            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add("featureLayer", featureLayer);

            // Add the LayerOverlay to the map
            Map.Overlays.Add("layerOverlay", _layerOverlay);

            // ============================================================
            // [1] TrackOverlay Event Bindings
            // ============================================================

            Map.TrackOverlay.Drawing += TrackOverlay_Drawing;
            Map.TrackOverlay.Drawn += TrackOverlay_Drawn;
            Map.TrackOverlay.MapKeyDown += TrackOverlay_MapKeyDown;
            Map.TrackOverlay.MapKeyUp += TrackOverlay_MapKeyUp;
            Map.TrackOverlay.MapMouseClick += TrackOverlay_MapMouseClick;
            Map.TrackOverlay.MapMouseDoubleClick += TrackOverlay_MapMouseDoubleClick;
            Map.TrackOverlay.MapMouseDown += TrackOverlay_MapMouseDown;
            Map.TrackOverlay.MapMouseEnter += TrackOverlay_MapMouseEnter;
            Map.TrackOverlay.MapMouseLeave += TrackOverlay_MapMouseLeave;
            Map.TrackOverlay.MapMouseUp += TrackOverlay_MapMouseUp;
            Map.TrackOverlay.MapMouseMove += TrackOverlay_MapMouseMove;
            Map.TrackOverlay.MapMouseWheel += TrackOverlay_MapMouseWheel;
            Map.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
            Map.TrackOverlay.ThrowingException += TrackOverlay_ThrowingException;
            Map.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
            Map.TrackOverlay.TrackEnding += TrackOverlay_TrackEnding;
            Map.TrackOverlay.TrackStarted += TrackOverlay_TrackStarted;
            Map.TrackOverlay.TrackStarting += TrackOverlay_TrackStarting;
            Map.TrackOverlay.VertexAdded += TrackOverlay_VertexAdded;
            Map.TrackOverlay.VertexAdding += TrackOverlay_VertexAdding;

            // ============================================================
            // [2] EditOverlay Event Bindings
            // ============================================================

            Map.EditOverlay.ControlPointSelected += EditOverlay_ControlPointSelected;
            Map.EditOverlay.ControlPointSelecting += EditOverlay_ControlPointSelecting;
            Map.EditOverlay.Drawing += EditOverlay_Drawing;
            Map.EditOverlay.Drawn += EditOverlay_Drawn;
            Map.EditOverlay.FeatureDragged += EditOverlay_FeatureDragged;
            Map.EditOverlay.FeatureDragging += EditOverlay_FeatureDragging;
            Map.EditOverlay.FeatureDropped += EditOverlay_FeatureDropped;
            Map.EditOverlay.FeatureEdited += EditOverlay_FeatureEdited;
            Map.EditOverlay.FeatureEditing += EditOverlay_FeatureEditing;
            Map.EditOverlay.FeatureResized += EditOverlay_FeatureResized;
            Map.EditOverlay.FeatureResizing += EditOverlay_FeatureResizing;
            Map.EditOverlay.FeatureRotated += EditOverlay_FeatureRotated;
            Map.EditOverlay.FeatureRotating += EditOverlay_FeatureRotating;
            Map.EditOverlay.MapKeyDown += EditOverlay_MapKeyDown;
            Map.EditOverlay.MapKeyUp += EditOverlay_MapKeyUp; ;
            Map.EditOverlay.MapMouseClick += EditOverlay_MapMouseClick;
            Map.EditOverlay.MapMouseDoubleClick += EditOverlay_MapMouseDoubleClick;
            Map.EditOverlay.MapMouseDown += EditOverlay_MapMouseDown;
            Map.EditOverlay.MapMouseEnter += EditOverlay_MapMouseEnter;
            Map.EditOverlay.MapMouseLeave += EditOverlay_MapMouseLeave;
            Map.EditOverlay.MapMouseMove += EditOverlay_MapMouseMove;
            Map.EditOverlay.MapMouseUp += EditOverlay_MapMouseUp;
            Map.EditOverlay.MapMouseWheel += EditOverlay_MapMouseWheel;
            Map.EditOverlay.ThrowingException += EditOverlay_ThrowingException;
            Map.EditOverlay.VertexAdded += EditOverlay_VertexAdded;
            Map.EditOverlay.VertexAdding += EditOverlay_VertexAdding;
            Map.EditOverlay.VertexMoved += EditOverlay_VertexMoved;
            Map.EditOverlay.VertexMoving += EditOverlay_VertexMoving;
            Map.EditOverlay.VertexRemoved += EditOverlay_VertexRemoved;
            Map.EditOverlay.VertexRemoving += EditOverlay_VertexRemoving;

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";

            _ = Map.RefreshAsync();
            _initialized = true;
        }


        // TrackOverlay Events Triggered Methods
        private void TrackOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"Drawing: " +
                $"Extent=[({e?.WorldExtent?.MinX:0}, {e?.WorldExtent?.MinY:0}) - ({e?.WorldExtent?.MaxX:0}, {e?.WorldExtent?.MaxY:0})]");
        }

        private void TrackOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"Drawn: " +
                $"Extent=[({e?.WorldExtent?.MinX:0}, {e?.WorldExtent?.MinY:0}) - ({e?.WorldExtent?.MaxX:0}, {e?.WorldExtent?.MaxY:0})]");
        }

        private void TrackOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", "MapKeyDown");
        }

        private void TrackOverlay_MapKeyUp(object sender, MapKeyUpInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", "MapKeyUp");
        }

        private void TrackOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseDoubleClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseDown, Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseEnter");
        }

        private void TrackOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", "MapMouseLeave");
        }

        private void TrackOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseUp, Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseMove at world ({args.WorldX:0},{args.WorldY:0})");
        }

        private void TrackOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            var delta = args.MouseWheelDelta;
            string zoomIntent = delta > 0 ? "ZoomIn" : "ZoomOut";
            var message = $"MapMouseWheel {zoomIntent} at world ({args.WorldX:0},{args.WorldY:0}), WheelDelta={delta}";
            AppendLog("TrackOverlay", message);
        }

        private void TrackOverlay_MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            var shape = e.AffectedFeature.GetShape();
            MeasureResult.Text = GetMeasureResult(shape);
            AppendLog("TrackOverlay", $"MouseMoved - ({e.MovedVertex.X:0}, {e.MovedVertex.Y:0})");
        }

        private void TrackOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"ThrowingException in overlay: {e.Exception.Message}");
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"TrackEnded - {e.TrackShape.GetType().Name}");
        }

        private void TrackOverlay_TrackEnding(object sender, TrackEndingTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"TrackEnding - {e.TrackShape.GetType().Name}");
        }

        private void TrackOverlay_TrackStarted(object sender, TrackStartedTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"TrackStarted - StartVertex: ({e.StartedVertex.X:0},{e.StartedVertex.Y:0})");
        }

        private void TrackOverlay_TrackStarting(object sender, TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"TrackStarting - StartVertex: ({e.StartingVertex.X:0},{e.StartingVertex.Y:0})");
        }

        private void TrackOverlay_VertexAdded(object sender, VertexAddedTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"VertexAdded - StartVertex: ({e.AddedVertex.X:0},{e.AddedVertex.Y:0})");
        }

        private void TrackOverlay_VertexAdding(object sender, VertexAddingTrackInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", $"VertexAdding - StartVertex: ({e.AddingVertex.X:0},{e.AddingVertex.Y:0})");
        }


        // EditOverlay Events Triggered Methods
        private void EditOverlay_ControlPointSelected(object sender, ControlPointSelectedEditInteractiveOverlayEventArgs e)
        {
            if (e.SelectedFeature?.GetShape() is PointShape p)
                AppendLog("EditOverlay", $"ControlPointSelected - Control Point ({p.X:0}, {p.Y:0})");
        }

        private void EditOverlay_ControlPointSelecting(object sender, ControlPointSelectingEditInteractiveOverlayEventArgs e)
        {
            if (e.TargetPointShape != null)
                AppendLog("EditOverlay", $"ControlPointSelected - Target Point ({e.TargetPointShape.X:0}, {e.TargetPointShape.Y:0})");
        }

        private void EditOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"Drawing: " +
                $"Extent=[({e?.WorldExtent?.MinX:0}, {e?.WorldExtent?.MinY:0}) - ({e?.WorldExtent?.MaxX:0}, {e?.WorldExtent?.MaxY:0})]");
        }

        private void EditOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"Drawn: " +
               $"Extent=[({e?.WorldExtent?.MinX:0}, {e?.WorldExtent?.MinY:0}) - ({e?.WorldExtent?.MaxX:0}, {e?.WorldExtent?.MaxY:0})]");
        }

        private void EditOverlay_FeatureDragged(object sender, FeatureDraggedEditInteractiveOverlayEventArgs e)
        {
            if (e.DraggedFeature?.GetShape() != null)
                AppendLog("EditOverlay", $"FeatureDragged - {e.DraggedFeature.GetShape().GetType().Name}");
        }

        private void EditOverlay_FeatureDragging(object sender, FeatureDraggingEditInteractiveOverlayEventArgs e)
        {
            if (e.DraggingFeature?.GetShape() != null)
                AppendLog("EditOverlay", $"FeatureDragging - {e.DraggingFeature.GetShape().GetType().Name}");
        }

        private void EditOverlay_FeatureDropped(object sender, FeatureDroppedEditInteractiveOverlayEventArgs e)
        {
            if (e.DroppedFeature?.GetShape() != null)
                AppendLog("EditOverlay", $"FeatureDropped - {e.DroppedFeature.GetShape().GetType().Name}");
        }

        private void EditOverlay_FeatureEdited(object sender, FeatureEditedEditInteractiveOverlayEventArgs e)
        {
            if (e.EditedFeature?.GetShape() != null)
                AppendLog("EditOverlay", $"FeatureEdited - {e.EditedFeature.GetShape().GetType().Name}");
        }

        private void EditOverlay_FeatureEditing(object sender, FeatureEditingEditInteractiveOverlayEventArgs e)
        {
            if (e.EditingFeature?.GetShape() != null)
                AppendLog("EditOverlay", $"FeatureEditing - {e.EditingFeature.GetShape().GetType().Name}");
        }

        private void EditOverlay_FeatureResized(object sender, FeatureResizedEditInteractiveOverlayEventArgs e)
        {
            var shape = e.ResizedFeature?.GetShape();
            if (shape == null) return;
            var center = shape.GetCenterPoint();
            AppendLog("EditOverlay", $"FeatureResized - {shape.GetType().Name}, Center=({center.X:0}, {center.Y:0})");
        }

        private void EditOverlay_FeatureResizing(object sender, FeatureResizingEditInteractiveOverlayEventArgs e)
        {
            var shape = e.ResizingFeature?.GetShape();
            if (shape == null) return;
            var center = shape.GetCenterPoint();
            AppendLog("EditOverlay", $"FeatureResizing - {shape.GetType().Name}, Center=({center.X:0}, {center.Y:0})");
        }

        private void EditOverlay_FeatureRotated(object sender, FeatureRotatedEditInteractiveOverlayEventArgs e)
        {
            var shape = e.RotatedFeature?.GetShape();
            if (shape == null) return;
            var center = shape.GetCenterPoint();
            AppendLog("EditOverlay", $"FeatureRotated - {shape.GetType().Name}, Center=({center.X:0}, {center.Y:0})");
        }

        private void EditOverlay_FeatureRotating(object sender, FeatureRotatingEditInteractiveOverlayEventArgs e)
        {
            var shape = e.RotatingFeature?.GetShape();
            if (shape == null) return;
            var center = shape.GetCenterPoint();
            AppendLog("EditOverlay", $"FeatureRotating - {shape.GetType().Name}, Center=({center.X:0}, {center.Y:0})");
        }

        private void EditOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"MapKeyDown - Key={e.InteractionArguments.Key}");
        }

        private void EditOverlay_MapKeyUp(object sender, MapKeyUpInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"MapKeyUp - Key={e.InteractionArguments.Key}");
        }

        private void EditOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void EditOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseDoubleClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void EditOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseDown, Button={args.MouseButton}");
        }

        private void EditOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseEnter");
        }

        private void EditOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", "MapMouseLeave");
        }

        private void EditOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseMove at world ({args.WorldX:0},{args.WorldY:0})");
        }

        private void EditOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseUp, Button={args.MouseButton}");
        }

        private void EditOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            var delta = args.MouseWheelDelta;
            string zoomIntent = delta > 0 ? "ZoomIn" : "ZoomOut";
            var message = $"MapMouseWheel {zoomIntent} at world ({args.WorldX:0},{args.WorldY:0}), WheelDelta={delta}";
            AppendLog("EditOverlay", message);
        }

        private void EditOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"ThrowingException occurred in overlay: {e.Exception.Message}");
        }

        private void EditOverlay_VertexAdded(object sender, VertexAddedEditInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"VertexAdded - ({e.AddedVertex.X:0}, {e.AddedVertex.Y:0})");
        }

        private void EditOverlay_VertexAdding(object sender, VertexAddingEditInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"VertexAdding - ({e.AddingVertex.X:0}, {e.AddingVertex.Y:0})");
        }

        private void EditOverlay_VertexMoved(object sender, VertexMovedEditInteractiveOverlayEventArgs e)
        {
            var shape = e.AffectedFeature.GetShape();
            MeasureResult.Text = GetMeasureResult(shape);
            AppendLog("EditOverlay", $"VertexMoved - ({e.MovedVertex.X:0}, {e.MovedVertex.Y:0})");
        }

        private void EditOverlay_VertexMoving(object sender, VertexMovingEditInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"VertexMoving - ({e.MovingVertex.X:0}, {e.MovingVertex.Y:0})");
        }

        private void EditOverlay_VertexRemoved(object sender, VertexRemovedEditInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"VertexRemoved - ({e.RemovedVertex.X:0}, {e.RemovedVertex.Y:0})");
        }

        private void EditOverlay_VertexRemoving(object sender, VertexRemovingEditInteractiveOverlayEventArgs e)
        {
            AppendLog("EditOverlay", $"VertexRemoving - ({e.RemovingVertex.X:0}, {e.RemovingVertex.Y:0})");
        }

        private void FilterLogMessages()
        {
            FilteredLogMessages.Clear();

            var filtered = LogMessages.Where(log =>
                (ShowTrackOverlayLogs && log.Category == "TrackOverlay" &&
                (ShowTrackOverlayMouseMoveLogs || !log.Message.Contains("MapMouseMove"))) ||
                (ShowEditOverlayLogs && log.Category == "EditOverlay" &&
                (ShowEditOverlayMouseMoveLogs || !log.Message.Contains("MapMouseMove"))))
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
            if (ShowTrackOverlayLogs && log.Category == "TrackOverlay")
                return ShowTrackOverlayMouseMoveLogs ||
                       !log.Message.Contains("MapMouseMove");
            if (ShowEditOverlayLogs && log.Category == "EditOverlay")
                return ShowEditOverlayMouseMoveLogs ||
                       !log.Message.Contains("MapMouseMove");
            return false;
        }

        private static string GetMeasureResult(BaseShape shape)
        {
            // the background map is in SphericalMercator
            var currentProjection = new Projection(Projection.GetSphericalMercatorProjString());
            var measureResult = string.Empty;
            switch (shape)
            {
                case AreaBaseShape polygon:
                    // pass in the projection to get the accurate area
                    measureResult = polygon.GetArea(currentProjection, AreaUnit.SquareMiles).ToString("N2");
                    measureResult += " square miles";
                    break;
                case LineBaseShape line:
                    // pass in the projection to get the accurate length
                    measureResult = line.GetLength(currentProjection, DistanceUnit.Mile).ToString("N2");
                    measureResult += " miles";
                    break;
            }
            return measureResult;
        }

        /// <summary>
        /// Set the mode to normal navigation. This is the default.
        /// </summary>
        private void NavMode_Click(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all TrackOverlay features to LayerOverlay
            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all EditOverlay features to LayerOverlay and clear EditOverlay
            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to navigate the map normally
            Map.TrackOverlay.TrackMode = TrackMode.None;

            // Update instructions
            Instructions.Text = "Navigation Mode — Default map state. Use mouse to pan and zoom the map.";

            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Set the mode to draw points on the map
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Point, which draws a new point on the map on mouse click
            Map.TrackOverlay.TrackMode = TrackMode.Point;

            // Update instructions
            Instructions.Text = "Draw Point Mode — Click anywhere on the map to create a point. Hold the middle mouse button to pan.";
        }

        /// <summary>
        /// Set the mode to draw lines on the map
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            Map.TrackOverlay.TrackMode = TrackMode.Line;

            // Update instructions
            Instructions.Text = "Draw Line Mode — Click to add vertices; double-click to finish the line. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Update instructions
            Instructions.Text =
                "Draw Polygon Mode — Click to add vertices; double-click to finish the polygon. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        private void EditShape_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            // Move all TrackOverlay features to EditOverlay
            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all layerOverlay features to EditOverlay
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in featureLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            featureLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes
            Map.TrackOverlay.TrackMode = TrackMode.None;

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            Map.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";
        }

        private void EditShape_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all EditOverlay features to LayerOverlay
            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (DeleteShape.IsChecked != null && !DeleteShape.IsChecked.Value)
                return;

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Query the layer for the closest feature within 100 meters
            var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

            // If a feature was found, remove it from the layer
            if (closestFeatures.Count <= 0) return;
            featureLayer.InternalFeatures.Remove(closestFeatures[0]);

            // Refresh the layerOverlay to show the results
            _ = Map.RefreshAsync(_layerOverlay);
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
