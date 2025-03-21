using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to handle basic map events.
    /// </summary>
    public partial class BasicMapEvents : IDisposable
    {
        private ShapeFileFeatureLayer _friscoCityBoundary;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> FilteredLogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _showOverlaysLogs = true;
        public bool ShowOverlaysLogs
        {
            get => _showOverlaysLogs;
            set
            {
                _showOverlaysLogs = value;
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

        private bool _showMouseMoveLogs = true;
        public bool ShowMouseMoveLogs
        {
            get => _showMouseMoveLogs;
            set
            {
                _showMouseMoveLogs = value;
                OnPropertyChanged(nameof(ShowMouseMoveLogs));
                FilterLogMessages();
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

        private void FilterLogMessages()
        {
            FilteredLogMessages.Clear();

            foreach (var log in LogMessages)
            {
                // Example: Filter by category 
                if (ShowMapViewLogs && log.Contains("MapView"))
                    FilteredLogMessages.Add(log);
                else if (ShowOverlaysLogs && log.Contains("Overlays"))
                    FilteredLogMessages.Add(log);
                else if (ShowExtentOverlayLogs && log.Contains("ExtentOverlay"))
                {
                    // Filter Mouse Move events correctly
                    if (ShowMouseMoveLogs || !log.Contains("Mouse Move"))
                        FilteredLogMessages.Add(log);
                }
                else if (ShowLayerOverlayLogs && log.Contains("LayerOverlay"))
                    FilteredLogMessages.Add(log);
                else if (ShowShapeFileLogs && log.Contains("FeatureLayer"))
                    FilteredLogMessages.Add(log);
            }

            // Ensure the UI updates
            OnPropertyChanged(nameof(FilteredLogMessages));
        }

        public BasicMapEvents()
        {
            InitializeComponent();
            DataContext = this;
            ThinkGeoDebugger.DisplayTileId = true;
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Load MapView Events
                MapView.CollectedMapArguments += MapView_CollectedMapArguments;
                MapView.ContextMenuClosing += MapView_ContextMenuClosing;
                MapView.ContextMenuOpening += MapView_ContextMenuOpening;
                MapView.CurrentExtentChanged += MapView_CurrentExtentChanged;
                MapView.CurrentExtentChanging += MapView_CurrentExtentChanging;
                MapView.CurrentScaleChanged += MapView_CurrentScaleChanged;
                MapView.CurrentScaleChanging += MapView_CurrentScaleChanging;

                // Load Overlays Events
                MapView.Overlays.Adding += Overlays_Adding;
                MapView.Overlays.Added += Overlays_Added;
                MapView.Overlays.ClearedItems += Overlays_ClearedItems;
                MapView.Overlays.ClearingItems += Overlays_ClearingItems;
                MapView.Overlays.CollectionChanged += Overlays_CollectionChanged;
                MapView.Overlays.Inserted += Overlays_Inserted;
                MapView.Overlays.Inserting += Overlays_Inserting;
                MapView.Overlays.MovedItem += Overlays_MovedItem;
                MapView.Overlays.PropertyChanged += Overlays_PropertyChanged;
                MapView.Overlays.Removed += Overlays_Removed;
                MapView.Overlays.Removing += Overlays_Removing;

                // Load ExtentOverlay Events
                MapView.ExtentOverlay.Drawing += ExtentOverlay_Drawing;
                MapView.ExtentOverlay.DrawingAttribution += ExtentOverlay_DrawingAttribution;
                MapView.ExtentOverlay.Drawn += ExtentOverlay_Drawn;
                MapView.ExtentOverlay.DrawnAttribution += ExtentOverlay_DrawnAttribution;
                MapView.ExtentOverlay.MapKeyDown += ExtentOverlay_MapKeyDown;
                MapView.ExtentOverlay.MapMouseClick += ExtentOverlay_MapMouseClick;
                MapView.ExtentOverlay.MapMouseDoubleClick += ExtentOverlay_MapMouseDoubleClick;
                MapView.ExtentOverlay.MapMouseDown += ExtentOverlay_MapMouseDown;
                MapView.ExtentOverlay.MapMouseEnter += ExtentOverlay_MapMouseEnter;
                MapView.ExtentOverlay.MapMouseLeave += ExtentOverlay_MapMouseLeave;
                MapView.ExtentOverlay.MapMouseMove += ExtentOverlay_MapMouseMove;
                MapView.ExtentOverlay.MapMouseUp += ExtentOverlay_MapMouseUp;
                MapView.ExtentOverlay.MapMouseWheel += ExtentOverlay_MapMouseWheel;
                MapView.ExtentOverlay.ThrowingException += ExtentOverlay_ThrowingException;


                // Load the Frisco data to a layer
                _friscoCityBoundary = new ShapeFileFeatureLayer(@"./Data/Shapefile/City_ETJ.shp")
                {
                    FeatureSource =
                    {
                        // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
                };

                // Style the data so that we can see it on the map
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(100, GeoColors.Blue), GeoColors.DimGray, 2);
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add Frisco data to a LayerOverlay and add it to the map
                var layerOverlay = new LayerOverlay();

                // Load LayerOverlay Events
                layerOverlay.Drawing += LayerOverlay_Drawing;
                layerOverlay.DrawingAttribution += LayerOverlay_DrawingAttribution;
                layerOverlay.DrawingException += LayerOverlay_DrawingException;
                layerOverlay.DrawingTile += LayerOverlay_DrawingTile;
                layerOverlay.Drawn += LayerOverlay_Drawn;
                layerOverlay.DrawnAttribution += LayerOverlay_DrawnAttribution;
                layerOverlay.DrawnException += LayerOverlay_DrawnException;
                layerOverlay.DrawnTile += LayerOverlay_DrawnTile;
                layerOverlay.DrawTilesProgressChanged += LayerOverlay_DrawTilesProgressChanged;
                layerOverlay.ThrowingException += LayerOverlay_ThrowingException;
                layerOverlay.TileTypeChanged += LayerOverlay_TileTypeChanged;
                layerOverlay.TileTypeChanging += LayerOverlay_TileTypeChanging;

                layerOverlay.Layers.Add(_friscoCityBoundary);
                MapView.Overlays.Add(layerOverlay);

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3909518, -10769429, 3908502);

                // Load ShapeFileFeatureLayer Events
                _friscoCityBoundary.DrawingFeatures += _friscoCityBoundary_DrawingFeatures;
                _friscoCityBoundary.DrawingException += _friscoCityBoundary_DrawingException;
                _friscoCityBoundary.DrawingProgressChanged += _friscoCityBoundary_DrawingProgressChanged;
                _friscoCityBoundary.DrawingWrappingFeatures += _friscoCityBoundary_DrawingWrappingFeatures;
                _friscoCityBoundary.DrawnException += _friscoCityBoundary_DrawnException;
                _friscoCityBoundary.RequestedDrawing += _friscoCityBoundary_RequestedDrawing;
                _friscoCityBoundary.RequestingDrawing += _friscoCityBoundary_RequestingDrawing;
                _friscoCityBoundary.StreamLoading += _friscoCityBoundary_StreamLoading;

                await MapView.PanByDirectionAsync(PanDirection.Up, 20);
                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        // MapView Events Triggered Methods
        private void MapView_CollectedMapArguments(object sender, CollectedMapArgumentsMapViewEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Collected Map Arguments" + e.MapArguments.ToString () ;
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Context Menu Closing";
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Context Menu Opening";
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Current Extent Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_CurrentExtentChanging(object sender, CurrentExtentChangingMapViewEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Current Extent Changing";
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_CurrentScaleChanged(object sender, CurrentScaleChangedMapViewEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Current Scale Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void MapView_CurrentScaleChanging(object sender, CurrentScaleChangingMapViewEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "MapView";
                    var message = "Current Scale Changing";
                    AppendLog(category, message);
                }
            });
        }

        // Overlays Events Triggered Methods
        private void Overlays_Adding(object sender, AddingGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Adding";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_Added(object sender, AddedGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Added";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_ClearedItems(object sender, ClearedItemsGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Cleared Items";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_ClearingItems(object sender, ClearingItemsGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Clearing Items";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Collection Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_Inserted(object sender, InsertedGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Inserted";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_Inserting(object sender, InsertingGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Inserting";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_MovedItem(object sender, MovedItemGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Moved Item";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Property Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_Removed(object sender, RemovedGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Removed";
                    AppendLog(category, message);
                }
            });
        }

        private void Overlays_Removing(object sender, RemovingGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "Overlays";
                    var message = "Removing";
                    AppendLog(category, message);
                }
            });
        }

        // ExtentOverlay Events Triggered Methods
        private void ExtentOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Drawing";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_DrawingAttribution(object sender, DrawingAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Drawing Attribution";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Drawn";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_DrawnAttribution(object sender, DrawnAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Drawn Attribution";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapKeyDown(object sender, MapKeyDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Key Down";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseClick(object sender, MapMouseClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Click";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseDoubleClick(object sender, MapMouseDoubleClickInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Double Click";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseDown(object sender, MapMouseDownInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Down";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseEnter(object sender, MapMouseEnterInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Enter";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseLeave(object sender, MapMouseLeaveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Leave";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseMove(object sender, MapMouseMoveInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Move";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseUp(object sender, MapMouseUpInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Up";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_MapMouseWheel(object sender, MapMouseWheelInteractiveOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Map Mouse Wheel";
                    AppendLog(category, message);
                }
            });
        }

        private void ExtentOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "ExtentOverlay";
                    var message = "Throwing Exception";
                    AppendLog(category, message);
                }
            });
        }

        // LayerOverlay Events Triggered Methods
        private void LayerOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawing";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawnAttribution(object sender, DrawnAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawn Attribution";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawingException(object sender, DrawingExceptionTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawing Exception";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawingTile(object sender, DrawingTileTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawing Tile";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_Drawn(object sender, DrawnOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawn";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawingAttribution(object sender, DrawingAttributionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawing Attribution";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawnException(object sender, DrawnExceptionTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawn Exception";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawnTile(object sender, DrawnTileTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Drawn Tile" + e.DrawnTile.Id;
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_DrawTilesProgressChanged(object sender, DrawTilesProgressChangedTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Draw Tiles Progress Changed" + e.ProgressPercentage;
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_ThrowingException(object sender, ThrowingExceptionOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Throwing Exception";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_TileTypeChanged(object sender, TileTypeChangedTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Tile Type Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void LayerOverlay_TileTypeChanging(object sender, TileTypeChangingTileOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "LayerOverlay";
                    var message = "Tile Type Changing";
                    AppendLog(category, message);
                }
            });
        }

        // ShapeFileFeatureLayer Events Triggered Methods
        private void _friscoCityBoundary_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Drawing Features";
                    AppendLog(category, message);
                }                
            });
        }

        private void _friscoCityBoundary_DrawingException(object sender, DrawingExceptionLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Drawing Exception";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_DrawingProgressChanged(object sender, DrawingProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Drawing Progress Changed";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_DrawingWrappingFeatures(object sender, DrawingWrappingFeaturesFeatureLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Drawing Wrapping Features";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_DrawnException(object sender, DrawnExceptionLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Drawn Exception";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_RequestedDrawing(object sender, RequestedDrawingLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Requested Drawing";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_RequestingDrawing(object sender, RequestingDrawingLayerEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Requesting Drawing";
                    AppendLog(category, message);
                }
            });
        }

        private void _friscoCityBoundary_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.ToString();
                if (checkInfo != null)
                {
                    var category = "FeatureLayer";
                    var message = "Stream Loading";
                    AppendLog(category, message);
                }
            });
        }

        public void AppendLog(string category, string message)
        {
            var logEntry = $"{_logIndex++}. {category}: {message}";
            LogMessages.Add(logEntry);

            // Check if the new message should be shown
            if ((ShowMapViewLogs && category == "MapView") ||
                (ShowOverlaysLogs && category == "Overlays") ||
                (ShowExtentOverlayLogs && category == "ExtentOverlay" &&
                 (ShowMouseMoveLogs || !message.Contains("Mouse Move"))) ||
                (ShowLayerOverlayLogs && category == "LayerOverlay") ||
                (ShowShapeFileLogs && category == "FeatureLayer"))
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