using System;
using System.Diagnostics;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Bing Maps Layer on the map
    /// </summary>
    public partial class AzureMap : IDisposable
    {

        private bool _initialized;
        public AzureMap()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Set the current extent to the whole world.
            Map.CenterPoint = new PointShape(0, 0);
            Map.CurrentScale = 105721100;
        }

        /// <summary>
        /// Add the Bing Maps layer to the map
        /// </summary>
        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtApplicationId.Text) || Map.Overlays.Contains("Azure Map")) return;
            BtnActivate.IsEnabled = false;

            // Create the Azure map layer and add it to the map.                
            var azureMapsOverlay = new AzureMapsRasterOverlay(TxtApplicationId.Text, AzureMapsRasterTileSet.Imagery)
            {
                TileCache = new FileRasterTileCache(@".\cache", "azureMapsImagery")
            };
            Map.Overlays.Add(azureMapsOverlay);

            // Refresh the map.
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