using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render OpenStreetMap using the OpenStreetMapOverlay.
    /// </summary>
    public partial class DisplayOpenStreetMapOverlaySample : IDisposable
    {
        public DisplayOpenStreetMapOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Add a simple background overlay
                MapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

                var osmMapsOverlay = new OpenStreetMapOverlay("DefaultAgent");
                MapView.Overlays.Add(osmMapsOverlay);

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Create an OpenStreetMaps overlay and add it to the map view.
        /// </summary>
        private async void DisplayOsmMaps_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var osmMapsOverlay = new OpenStreetMapOverlay(OsmUserAgent.Text);
                MapView.Overlays.Add(osmMapsOverlay);
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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