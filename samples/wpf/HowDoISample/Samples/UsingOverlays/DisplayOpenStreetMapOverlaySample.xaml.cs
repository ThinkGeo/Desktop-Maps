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

        private bool _initialized;
        public DisplayOpenStreetMapOverlaySample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with a background overlay and set the map's extent to Frisco, Tx.
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            Map.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            var osmMapsOverlay = new OpenStreetMapOverlay("DefaultAgent");
            Map.Overlays.Add(osmMapsOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Create an OpenStreetMaps overlay and add it to the map view.
        /// </summary>
        private void DisplayOsmMaps_Click(object sender, RoutedEventArgs e)
        {
            var osmMapsOverlay = new OpenStreetMapOverlay(OsmUserAgent.Text);
            Map.Overlays.Add(osmMapsOverlay);
            _ = Map.RefreshAsync();
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