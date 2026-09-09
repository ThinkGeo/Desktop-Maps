using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to get data from a feature in a ShapeFile
    /// </summary>
    public partial class GetDataFromOneFeature
    {
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private readonly FeatureSourceVectorTileSource _tiles = new FeatureSourceVectorTileSource();
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        // The parks read a second time, for asking rather than drawing: the tile source
        // below queries its own copy, and a second consumer asking that one questions
        // mid-cut is a race.
        private ShapeFileFeatureLayer _parks;

        public GetDataFromOneFeature()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layer containing Frisco parks data
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            try
            {
                // Set the Map Unit to meters (used in Spherical Mercator)
                Map.MapUnit = GeographyUnit.Meter;

                // The parks are static polygons, so they are cut into tiles and drawn
                // with the basemap in one pass rather than by an overlay above it.
                _tiles.FeatureSources.Add("parks", new ShapeFileFeatureSource(@"./Data/Shapefile/Parks.shp")
                {
                    // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                    ProjectionConverter = new ProjectionConverter(2276, 3857),
                });
                Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, _cloud).AddStyle(Editor.Text, _tiles));

                _parks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp")
                {
                    FeatureSource = { ProjectionConverter = new ProjectionConverter(2276, 3857) },
                };

                // Add a PopupOverlay to the map, to display feature information
                var popupOverlay = new PopupOverlay();
                Map.Overlays.Add("Info Popup Overlay", popupOverlay);

                _parks.Open();
                var parksLayerBBox = _parks.GetBoundingBox();
                Map.CenterPoint = parksLayerBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, parksLayerBBox, Map.MapWidth, Map.MapHeight);
                await Map.ZoomInAsync();
                _parks.Close();

                // Refresh and redraw the map
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(new MapStyle(ThinkGeoVectorStyles.Light, _cloud).AddStyle(Editor.Text, _tiles));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        /// <summary>
        /// Get a feature based on a location
        /// </summary>
        private Feature GetFeatureFromLocation(BaseShape location)
        {
            // Find the feature that was clicked on by querying the layer for features containing the clicked coordinates
            _parks.Open();
            var selectedFeature = _parks.QueryTools.GetFeaturesContaining(location, ReturningColumnsType.AllColumns).FirstOrDefault();
            _parks.Close();

            return selectedFeature;
        }

        /// <summary>
        /// Display a popup containing a feature's info
        /// </summary>
        private async Task DisplayFeatureInfoAsync(Feature feature)
        {
            var parkInfoString = new StringBuilder();

            // Each column in a feature is a data attribute
            // Add all attribute pairs to the info string
            foreach (var column in feature.ColumnValues)
            {
                parkInfoString.AppendLine($"{column.Key}: {column.Value}");
            }

            // Create a new popup with the park info string
            var popupOverlay = (PopupOverlay)Map.Overlays["Info Popup Overlay"];
            var popup = new Popup(feature.GetShape().GetCenterPoint())
            {
                Content = parkInfoString.ToString(),
                FontSize = 10d,
                FontFamily = new System.Windows.Media.FontFamily("Verdana")
            };

            // Clear the popup overlay and add the new popup to it
            popupOverlay.Popups.Clear();
            popupOverlay.Popups.Add(popup);

            // Refresh the overlay to redraw the popups
            await popupOverlay.RefreshAsync();
        }

        /// <summary>
        /// Pull data from the selected feature and display it when clicked
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Get the selected feature based on the map click location
            var selectedFeature = GetFeatureFromLocation(e.WorldLocation);

            // If a feature was selected, get the data from it and display it
            if (selectedFeature != null)
            {
                _ = DisplayFeatureInfoAsync(selectedFeature);
            }
        }
    }
}
