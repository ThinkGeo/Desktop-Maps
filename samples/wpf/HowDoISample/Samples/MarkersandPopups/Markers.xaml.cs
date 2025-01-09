﻿using System;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to add, edit, or remove markers on the map using the MarkerOverlay.
    /// </summary>
    public partial class Markers : IDisposable
    {
        public Markers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

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

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

                AddSimpleMarkers();

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Add a SimpleMarkerOverlay to the map
        /// </summary>
        private void AddSimpleMarkers()
        {
            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            MapView.Overlays.Add("simpleMarkerOverlay", simpleMarkerOverlay);
        }

        /// <summary>
        /// Adds a marker to the simpleMarkerOverlay where the map click event occurred.
        /// </summary>
        private async void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {
            try
            { 
                var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];

                // Create a marker at the position the mouse was clicked
                var marker = new Marker(e.WorldLocation)
                {
                    ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                    Width = 20,
                    Height = 34,
                    YOffset = -17
                };

                // Add the marker to the simpleMarkerOverlay and refresh the map
                simpleMarkerOverlay.Markers.Add(marker);
                await simpleMarkerOverlay.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to none, meaning that the markers cannot be moved or manipulated.
        /// </summary>
        private void StaticMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.None;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to drag, which allows the user to click and drag on an icon to move it.
        /// </summary>
        private void DragMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
        }

        /// <summary>
        /// Sets the simpleMarkerOverlay's drag mode to copy, which allows the user to copy an existing marker by shift-clicking and dragging it.
        /// </summary>
        private void CopyMode_OnClick(object sender, RoutedEventArgs e)
        {
            var simpleMarkerOverlay = (SimpleMarkerOverlay)MapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.CopyWithShiftKey;
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