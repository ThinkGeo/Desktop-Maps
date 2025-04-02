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
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            MapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            var osmMapsOverlay = new OpenStreetMapOverlay("DefaultAgent");
            MapView.Overlays.Add(osmMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Create an OpenStreetMaps overlay and add it to the map view.
        /// </summary>
        private void DisplayOsmMaps_Click(object sender, RoutedEventArgs e)
        {
            var osmMapsOverlay = new OpenStreetMapOverlay(OsmUserAgent.Text);
            MapView.Overlays.Add(osmMapsOverlay);
            _ = MapView.RefreshAsync();
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