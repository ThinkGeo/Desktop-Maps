using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to draw, edit, or delete shapes using the map's TrackOverlay and EditOverlay.
    /// </summary>
    public partial class EditMapEvents : IDisposable
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> FilteredLogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        private bool _showTrackOverlayMouseMoveLogs = true;
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

        private bool _showEditOverlayMouseMoveLogs = true;
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

        private void FilterLogMessages()
        {
            FilteredLogMessages.Clear();

            foreach (var log in LogMessages)
            {
                if (ShowTrackOverlayLogs && log.Contains("TrackOverlay"))
                {
                    // Filter TrackOverlay Mouse Move events 
                    if (ShowTrackOverlayMouseMoveLogs || !log.Contains("MapMouseMove"))
                        FilteredLogMessages.Add(log);
                }
                else if (ShowEditOverlayLogs && log.Contains("EditOverlay"))
                {
                    // Filter EditOverlay Mouse Move events 
                    if (ShowEditOverlayMouseMoveLogs || !log.Contains("MapMouseMove"))
                        FilteredLogMessages.Add(log);
                }
            }

            // Ensure the UI updates
            OnPropertyChanged(nameof(FilteredLogMessages));
        }

        public EditMapEvents()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
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
            MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            // Create the layer that will store the drawn shapes
            var featureLayer = new InMemoryFeatureLayer();

            // Add styles for the layer
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("featureLayer", featureLayer);

            // Add the LayerOverlay to the map
            MapView.Overlays.Add("layerOverlay", layerOverlay);

            // Load TrackOverlay Events
            MapView.TrackOverlay.Drawing += TrackOverlay_Drawing;
            MapView.TrackOverlay.DrawingAttribution += TrackOverlay_DrawingAttribution;
            MapView.TrackOverlay.Drawn += TrackOverlay_Drawn;
            MapView.TrackOverlay.DrawnAttribution += TrackOverlay_DrawnAttribution;
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

            // Load EditOverlay Events
            MapView.EditOverlay.ControlPointSelected += EditOverlay_ControlPointSelected;
            MapView.EditOverlay.ControlPointSelecting += EditOverlay_ControlPointSelecting;
            MapView.EditOverlay.Drawing += EditOverlay_Drawing;
            MapView.EditOverlay.DrawingAttribution += EditOverlay_DrawingAttribution;
            MapView.EditOverlay.Drawn += EditOverlay_Drawn;
            MapView.EditOverlay.DrawnAttribution += EditOverlay_DrawnAttribution;
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
            MapView.EditOverlay.MapKeyUp += EditOverlay_MapKeyUp;
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

            // Simulate the checked RadioButton's logic
            DrawPolygon_Click(DrawPolygon, new RoutedEventArgs());

            _ = MapView.RefreshAsync();
        }

        // TrackOverlay Events Triggered Methods
        private void TrackOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"Drawing [WorldExtent.Id: {e.WorldExtent.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_DrawingAttribution(object sender, DrawingAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"DrawingAttribution [Canvas.CurrentScale: {e.Canvas.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"Drawn [WorldExtent.Id: {e.WorldExtent.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_DrawnAttribution(object sender, DrawnAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"DrawnAttribution [Attribution.Length: {e.Attribution.Length.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapKeyDown [InteractionArguments.CurrentScale: {e.InteractionArguments.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapKeyUp(object sender, MapKeyUpInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapKeyUp [InteractionArguments.CurrentScale: {e.InteractionArguments.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseClick [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseDoubleClick [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseDown [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseEnter [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseLeave [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseUp [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseMove [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MapMouseWheel [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"MouseMoved [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"ThrowingException [Exception.Message: {e.Exception.Message.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"TrackEnded [TrackShape.Id: {e.TrackShape.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_TrackEnding(object sender, TrackEndingTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"TrackEnding [TrackShape.Id: {e.TrackShape.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_TrackStarted(object sender, TrackStartedTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"TrackStarted [StartedVertex.X: {e.StartedVertex.X.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_TrackStarting(object sender, TrackStartingTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"TrackStarting [StartingVertex.X: {e.StartingVertex.X.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_VertexAdded(object sender, VertexAddedTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"VertexAdded [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void TrackOverlay_VertexAdding(object sender, VertexAddingTrackInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "TrackOverlay";
                    var message = $"VertexAdding [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }


        // EditOverlay Events Triggered Methods
        private void EditOverlay_ControlPointSelected(object sender, ControlPointSelectedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"ControlPointSelected( [SelectedFeature.Id: {e.SelectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_ControlPointSelecting(object sender, ControlPointSelectingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"ControlPointSelecting [{e.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"Drawing [WorldExtent.Id: {e.WorldExtent.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_DrawingAttribution(object sender, DrawingAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"DrawingAttribution [Canvas.CurrentScale: {e.Canvas.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"Drawn [Overlay.MapArguments.CurrentScale: {e.Overlay.MapArguments.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_DrawnAttribution(object sender, DrawnAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"DrawnAttribution [Canvas.CurrentScale: {e.Canvas.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureDragged(object sender, FeatureDraggedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureDragged [DraggedFeature.Id: {e.DraggedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureDragging(object sender, FeatureDraggingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureDragging [SourceControlPoint.Id: {e.SourceControlPoint.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureDropped(object sender, FeatureDroppedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureDropped [DroppedFeature.Id: {e.DroppedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureEdited(object sender, FeatureEditedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureEdited [EditedFeature.Id: {e.EditedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureEditing(object sender, FeatureEditingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureEditing [EditingFeature.Id: {e.EditingFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureResized(object sender, FeatureResizedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureResized [ResizedFeature.Id: {e.ResizedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureResizing(object sender, FeatureResizingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureResizing [TargetControlPoint.Id: {e.TargetControlPoint.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureRotated(object sender, FeatureRotatedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureRotated [RotatedFeature.Id: {e.RotatedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_FeatureRotating(object sender, FeatureRotatingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"FeatureRotating [RotatingFeature.Id: {e.RotatingFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapKeyDown [InteractionArguments.CurrentScale: {e.InteractionArguments.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapKeyUp(object sender, MapKeyUpInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapKeyUp [InteractionArguments.CurrentScale: {e.InteractionArguments.CurrentScale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseClick [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseDoubleClick [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseDown [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseEnter [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseLeave [AffectedFeature.Id: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseMove [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseUp [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"MapMouseWheel [InteractionArguments.Scale: {e.InteractionArguments.Scale.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"ThrowingException [Exception.Message: {e.Exception.Message.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexAdded(object sender, VertexAddedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexAdded [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexAdding(object sender, VertexAddingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexAdding [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexMoved(object sender, VertexMovedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexMoved [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexMoving(object sender, VertexMovingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexMoving [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexRemoved(object sender, VertexRemovedEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexRemoved [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }

        private void EditOverlay_VertexRemoving(object sender, VertexRemovingEditInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "EditOverlay";
                    var message = $"VertexRemoving [AffectedFeature.Id: {e.AffectedFeature.Id.ToString()}]";
                    AppendLog(category, message);
                }
            });
        }


        /// <summary>
        /// Update the layer whenever the user switches modes
        /// </summary>
        private void UpdateLayerFeaturesAsync(InMemoryFeatureLayer featureLayer, LayerOverlay layerOverlay)
        {
            // If the user switched away from a Drawing Mode, add all the newly drawn shapes in the TrackOverlay into the featureLayer
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the TrackOverlay's features
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // If the user switched away from Edit Mode, add all the shapes that were in the EditOverlay back into the featureLayer
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the EditOverlay's features
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, layerOverlay });

            // In case the user was in Delete Mode, remove the event handler to avoid deleting features unintentionally
            MapView.MapClick -= MapView_MapClick;
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            MapView.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        /// <summary>
        /// Set the mode to edit drawn shapes
        /// </summary>
        private void EditShape_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // Put all features in the featureLayer into the EditOverlay
            foreach (var feature in featureLayer.InternalFeatures)
            {
                MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear all the features in the featureLayer so that the editing features don't overlap with the original shapes
            // In UpdateLayerFeatures, we will add them all back to the featureLayer once the user switches modes
            featureLayer.InternalFeatures.Clear();

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            MapView.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            _ = MapView.RefreshAsync(new Overlay[] { MapView.EditOverlay, layerOverlay });
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Query the layer for the closest feature within 100 meters
            var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

            // If a feature was found, remove it from the layer
            if (closestFeatures.Count <= 0) return;
            featureLayer.InternalFeatures.Remove(closestFeatures[0]);

            // Refresh the layerOverlay to show the results
            _ = MapView.RefreshAsync(layerOverlay);
        }

        public void AppendLog(string category, string message)
        {
            var logEntry = $"{_logIndex++}. {category} -> {message}";
            LogMessages.Add(logEntry);

            // Check if the new message should be shown
            if (((ShowTrackOverlayLogs && category == "TrackOverlay") &&
                (ShowTrackOverlayMouseMoveLogs || !message.Contains("MapMouseMove"))) ||
                ((ShowEditOverlayLogs && category == "EditOverlay") &&
                 (ShowEditOverlayMouseMoveLogs || !message.Contains("MapMouseMove"))))
            {
                FilteredLogMessages.Add(logEntry);
                OnPropertyChanged(nameof(FilteredLogMessages)); // Notify UI
            }

            if (FilteredLogMessages?.Count > 0)
                LogListBox.ScrollIntoView(FilteredLogMessages[FilteredLogMessages.Count - 1]);
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