﻿using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMS Layer on the map
    /// </summary>
    public partial class WMS : IDisposable
    {
        public WMS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.DecimalDegree;

            // This code sets up the sample to use the overlay versus the layer.
            UseOverlay();

            // Set the current extent to a local area.
            MapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

            // Refresh the map.
            await MapView.RefreshAsync();
        }

        private async void RbLayerOrOverlay_Checked(object sender, RoutedEventArgs e)
        {
            // Based on the radio buttons we switch between using the overlay and layer.
            var button = (RadioButton)sender;
            if (button.Content == null) return;
            switch (button.Content.ToString())
            {
                case "Use WmsOverlay":
                    UseOverlay();
                    break;
                case "Use WmsRasterLayer":
                    UseLayer();
                    break;
            }
            await MapView.RefreshAsync();
        }
        private void UseOverlay()
        {
            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create a WMS overlay using the WMS parameters below.
            // This is a public service and is very slow most of the time.
            var wmsOverlay = new WmsOverlay
            {
                Uri = new Uri("http://ows.mundialis.de/services/service")
            };
            wmsOverlay.Parameters.Add("layers", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");

            // Add the overlay to the map.
            MapView.Overlays.Add(wmsOverlay);
        }

        private void UseLayer()
        {
            // Clear out the overlays so we start fresh
            MapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay();
            MapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            var wmsImageLayer = new Core.WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));
            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
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