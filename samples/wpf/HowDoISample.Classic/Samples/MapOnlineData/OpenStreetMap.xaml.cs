using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display an OpenStreetMaps Layer on the map
    /// </summary>
    public partial class OpenStreetMap : IDisposable
    {

        private bool _initialized;
        public OpenStreetMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the OpenStreetMaps layer to the map
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            Map.MapUnit = GeographyUnit.Meter;

            // Create a new overlay that will hold our new layer and add it to the map and set the tile size to match up with the OSM til size.
            var layerOverlay = new OpenStreetMapOverlay("ThinkGeo Samples");
            Map.Overlays.Add(layerOverlay);

            // Set the current extent to a local area.
            Map.CenterPoint = new PointShape(-10778800, 3915300);
            Map.CurrentScale = 91000;

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