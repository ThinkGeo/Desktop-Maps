using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to set the map extent using a variety of different methods.
    /// </summary>
    public partial class BasicMapEvents : IDisposable
    {
        private ShapeFileFeatureLayer _friscoCityBoundary;
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private int _logIndex = 0;

        public BasicMapEvents()
        {
            InitializeComponent();
            Debug.WriteLine($"DataContext: {DataContext}");
            DataContext = this;
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

                // Load Overlays Events
                MapView.Overlays.Adding += Overlays_Adding;
                MapView.Overlays.Added += Overlays_Added;
                MapView.Overlays.CollectionChanged += Overlays_CollectionChanged;

                //var extentInteractiveOverlay = new ExtentInteractiveOverlay();

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
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add Frisco data to a LayerOverlay and add it to the map
                var layerOverlay = new LayerOverlay();

                // Load LayerOverlay Events
                layerOverlay.Drawing += LayerOverlay_Drawing;

                layerOverlay.Layers.Add(_friscoCityBoundary);
                MapView.Overlays.Add(layerOverlay);

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

                // Load ShapeFileFeatureLayer Events
                _friscoCityBoundary.DrawingFeatures += _friscoCityBoundary_DrawingFeatures;

                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void LayerOverlay_Drawing(object sender, DrawingOverlayEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.WorldExtent.ToString();
                if (checkInfo != null)
                {
                    var message = "LayerOverlay: Feature Layer Drawing";
                    AppendLog(message);
                }
            });
        }

        private void Overlays_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.NewItems.ToString();
                if (checkInfo != null)
                {
                    var message = "Overlays: Feature Layer CollectionChanged";
                    AppendLog(message);
                }
            });
        }

        private void Overlays_Adding(object sender, AddingGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.Item.ToString();
                if (checkInfo != null)
                {
                    var message = "Overlays: Feature Layer Adding";
                    AppendLog(message);
                }
            });
        }

        private void Overlays_Added(object sender, AddedGeoCollectionEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.Item.ToString();
                if (checkInfo != null)
                {
                    var message = "Overlays: Feature Layer Added";
                    AppendLog(message);
                }
            });
        }

        private void _friscoCityBoundary_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                var checkInfo = e.FeaturesToDraw.ToString();
                if (checkInfo != null)
                {
                    var message = "ShapeFileFeatureLayer: DrawingFeatures";
                    AppendLog(message);
                }                
            });
        }

        public void AppendLog(string message)
        {
            // Add log message to the observable collection
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public class LogMessage
    {
        public string Category { get; set; }  // Overlays, ShapeFileFeatureLayer, LayerOverlay
        public string Message { get; set; }   // Event message
    }

    public class MapEventsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<LogMessage> AllLogMessages { get; set; }
        public ObservableCollection<LogMessage> FilteredLogMessages { get; set; }

        public bool ShowOverlays { get; set; }
        public bool ShowShapeFileFeatureLayer { get; set; }
        public bool ShowLayerOverlay { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MapEventsViewModel()
        {
            AllLogMessages = new ObservableCollection<LogMessage>();
            FilteredLogMessages = new ObservableCollection<LogMessage>();

            ShowOverlays = true;  // Default checked
            ShowShapeFileFeatureLayer = true;
            ShowLayerOverlay = true;
        }

        public void AddLog(string category, string message)
        {
            var log = new LogMessage { Category = category, Message = message };
            AllLogMessages.Add(log);
            UpdateFilteredLogs();
        }

        public void UpdateFilteredLogs()
        {
            FilteredLogMessages.Clear();

            foreach (var log in AllLogMessages)
            {
                if ((ShowOverlays && log.Category == "Overlays") ||
                    (ShowShapeFileFeatureLayer && log.Category == "ShapeFileFeatureLayer") ||
                    (ShowLayerOverlay && log.Category == "LayerOverlay"))
                {
                    FilteredLogMessages.Add(log);
                }
            }
        }
    }

}