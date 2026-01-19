using Esri.FileGDB;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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

        public EditMapEvents()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;
            MapView.MapClick += MapView_MapClick;

            var demoPolygon = new Feature("POLYGON((-10778500 3915600,-10778500 3910000,-10774040 3910000,-10774040 3915600,-10778500 3915600))");
            var demoPoint = new Feature("POINT(-10773220 3913230)");
            var demoLine = new Feature("LINESTRING(-10780700 3916500, -10780700 3910040)");
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPolygon);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPoint);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoLine);
            MapView.EditOverlay.CalculateAllControlPoints();

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
            MapView.Overlays.Add("layerOverlay", _layerOverlay);

            // ============================================================
            // [1] TrackOverlay Event Bindings
            // ============================================================

            MapView.TrackOverlay.Drawing += TrackOverlay_Drawing;
            MapView.TrackOverlay.Drawn += TrackOverlay_Drawn;
            MapView.TrackOverlay.MapKeyDown += TrackOverlay_MapKeyDown;
            MapView.TrackOverlay.MapKeyUp += TrackOverlay_MapKeyUp;
            MapView.TrackOverlay.MapMouseClick += TrackOverlay_MapMouseClick;
            MapView.TrackOverlay.MapMouseDoubleClick += TrackOverlay_MapMouseDoubleClick;
            MapView.TrackOverlay.MapMouseDown += TrackOverlay_MapMouseDown;
            MapView.TrackOverlay.MapMouseEnter += TrackOverlay_MapMouseEnter;
            MapView.TrackOverlay.MapMouseLeave += TrackOverlay_MapMouseLeave;
            MapView.TrackOverlay.MapMouseUp += TrackOverlay_MapMouseUp;
            MapView.TrackOverlay.MapMouseMove += TrackOverlay_MapMouseMove;
            MapView.TrackOverlay.MapMouseWheel += TrackOverlay_MapMouseWheel;
            MapView.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
            MapView.TrackOverlay.ThrowingException += TrackOverlay_ThrowingException;
            MapView.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
            MapView.TrackOverlay.TrackEnding += TrackOverlay_TrackEnding;
            MapView.TrackOverlay.TrackStarted += TrackOverlay_TrackStarted;
            MapView.TrackOverlay.TrackStarting += TrackOverlay_TrackStarting;
            MapView.TrackOverlay.VertexAdded += TrackOverlay_VertexAdded;
            MapView.TrackOverlay.VertexAdding += TrackOverlay_VertexAdding;

            // ============================================================
            // [2] EditOverlay Event Bindings
            // ============================================================

            MapView.EditOverlay.ControlPointSelected += EditOverlay_ControlPointSelected;
            MapView.EditOverlay.ControlPointSelecting += EditOverlay_ControlPointSelecting;
            MapView.EditOverlay.Drawing += EditOverlay_Drawing;
            MapView.EditOverlay.Drawn += EditOverlay_Drawn;
            MapView.EditOverlay.FeatureDragged += EditOverlay_FeatureDragged;
            MapView.EditOverlay.FeatureDragging += EditOverlay_FeatureDragging;
            MapView.EditOverlay.FeatureDropped += EditOverlay_FeatureDropped;
            MapView.EditOverlay.FeatureEdited += EditOverlay_FeatureEdited;
            MapView.EditOverlay.FeatureEditing += EditOverlay_FeatureEditing;
            MapView.EditOverlay.FeatureResized += EditOverlay_FeatureResized;
            MapView.EditOverlay.FeatureResizing += EditOverlay_FeatureResizing;
            MapView.EditOverlay.FeatureRotated += EditOverlay_FeatureRotated;
            MapView.EditOverlay.FeatureRotating += EditOverlay_FeatureRotating;
            MapView.EditOverlay.MapKeyDown += EditOverlay_MapKeyDown;
            MapView.EditOverlay.MapKeyUp += EditOverlay_MapKeyUp; ;
            MapView.EditOverlay.MapMouseClick += EditOverlay_MapMouseClick;
            MapView.EditOverlay.MapMouseDoubleClick += EditOverlay_MapMouseDoubleClick;
            MapView.EditOverlay.MapMouseDown += EditOverlay_MapMouseDown;
            MapView.EditOverlay.MapMouseEnter += EditOverlay_MapMouseEnter;
            MapView.EditOverlay.MapMouseLeave += EditOverlay_MapMouseLeave;
            MapView.EditOverlay.MapMouseMove += EditOverlay_MapMouseMove;
            MapView.EditOverlay.MapMouseUp += EditOverlay_MapMouseUp;
            MapView.EditOverlay.MapMouseWheel += EditOverlay_MapMouseWheel;
            MapView.EditOverlay.ThrowingException += EditOverlay_ThrowingException;
            MapView.EditOverlay.VertexAdded += EditOverlay_VertexAdded;
            MapView.EditOverlay.VertexAdding += EditOverlay_VertexAdding;
            MapView.EditOverlay.VertexMoved += EditOverlay_VertexMoved;
            MapView.EditOverlay.VertexMoving += EditOverlay_VertexMoving;
            MapView.EditOverlay.VertexRemoved += EditOverlay_VertexRemoved;
            MapView.EditOverlay.VertexRemoving += EditOverlay_VertexRemoving;

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";

            _ = MapView.RefreshAsync();
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
            AppendLog("TrackOverlay", $"MouseClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MouseDoubleClick at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseDown at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void TrackOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseEnter at world ({args.WorldX:0},{args.WorldY:0})");
        }

        private void TrackOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            AppendLog("TrackOverlay", "MapMouseLeave");
        }

        private void TrackOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("TrackOverlay", $"MapMouseUp at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
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
            AppendLog("EditOverlay", $"MapMouseDown at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
        }

        private void EditOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            var args = e.InteractionArguments;
            AppendLog("EditOverlay", $"MapMouseEnter at world ({args.WorldX:0},{args.WorldY:0})");
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
            AppendLog("EditOverlay", $"MapMouseUp at world ({args.WorldX:0},{args.WorldY:0}), Button={args.MouseButton}");
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
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all EditOverlay features to LayerOverlay and clear EditOverlay
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to navigate the map normally
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // Update instructions
            Instructions.Text = "Navigation Mode — Default map state. Use mouse to pan and zoom the map.";

            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Set the mode to draw points on the map
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Point, which draws a new point on the map on mouse click
            MapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Update instructions
            Instructions.Text = "Draw Point Mode — Click anywhere on the map to create a point. Hold the middle mouse button to pan.";
        }

        /// <summary>
        /// Set the mode to draw lines on the map
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            MapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Update instructions
            Instructions.Text = "Draw Line Mode — Click to add vertices; double-click to finish the line. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            MapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Update instructions
            Instructions.Text =
                "Draw Polygon Mode — Click to add vertices; double-click to finish the polygon. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        private void EditShape_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            // Move all TrackOverlay features to EditOverlay
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
                MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all layerOverlay features to EditOverlay
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in featureLayer.InternalFeatures)
                MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            featureLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            MapView.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";
        }

        private void EditShape_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all EditOverlay features to LayerOverlay
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
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
            _ = MapView.RefreshAsync(_layerOverlay);
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