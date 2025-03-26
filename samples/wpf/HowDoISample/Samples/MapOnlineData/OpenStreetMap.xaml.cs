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
        public OpenStreetMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the OpenStreetMaps layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;

                // Set the zoom level set on the map to make sure its compatible with the OSM zoom levels.
                MapView.ZoomLevelSet = new OpenStreetMapsZoomLevelSet();

                // Create a new overlay that will hold our new layer and add it to the map and set the tile size to match up with the OSM til size.
                var layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(layerOverlay);
                layerOverlay.TileWidth = 256;
                layerOverlay.TileHeight = 256;

                // Create the new layer and add it to the overlay.  We set the user agent to specify the requests are coming from our samples.
                // You need to change this to your application, so they can identify you for usage.
                var openStreetMapLayer = new Core.OpenStreetMapAsyncLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
                layerOverlay.Layers.Add(openStreetMapLayer);

                // Set the current extent to a local area.
                MapView.CenterPoint = new PointShape(-10778800,3915300);
                MapView.CurrentScale = 91000;

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