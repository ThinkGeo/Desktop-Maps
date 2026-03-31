using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to render Bing Maps using the BingMapsOverlay.
    /// A valid Bing Maps ApplicationID is required.
    /// </summary>
    public partial class DisplayAzureMapsOverlaySample : IDisposable
    {

        private bool _initialized;
        public DisplayAzureMapsOverlaySample()
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

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;
        }

        /// <summary>
        /// Create a Bing Maps overlay and add it to the map view.
        /// </summary>
        private void DisplayAzureMaps_Click(object sender, RoutedEventArgs e)
        {
            var AzureMapsOverlay = new AzureMapsRasterOverlay(AzureApplicationId.Text, AzureMapsRasterTileSet.Imagery);
            Map.Overlays.Add(AzureMapsOverlay);
            _ = Map.RefreshAsync();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
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