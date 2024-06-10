using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a CloudMapsVector Layer on the map
    /// </summary>
    public partial class UpdateTheGPSLocation : IDisposable
    {
        private bool _cancelFeed;
        private bool _pauseFeed;

        public UpdateTheGPSLocation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Set up the overlay that we will refresh often
            var vehicleOverlay = new LayerOverlay();

            // This in memory layer will hold the active point, we will be adding and removing from it frequently
            var vehicleLayer = new InMemoryFeatureLayer();

            // Set the points image to a car icon and then apply it to all zoom levels
            var vehiclePointStyle = new PointStyle(new GeoImage(@"./Resources/vehicle-location.png"))
            {
                YOffsetInPixel = -12
            };

            vehicleLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = vehiclePointStyle;
            vehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the in memory layer to the overlay
            vehicleOverlay.Layers.Add("Vehicle Layer", vehicleLayer);

            // Add the overlay to the map
            MapView.Overlays.Add("Vehicle Overlay", vehicleOverlay);

            // Set the map extent
            MapView.CurrentExtent = new RectangleShape(-10779430.188014803, 3912668.1732483786, -10778438.895309737, 3911814.2283277493);

            // We hook up this even so when you leave this sample we stop the background data feed task
            this.Unloaded -= RefreshDynamicItems_Unloaded;
            this.Unloaded += RefreshDynamicItems_Unloaded;

            //  Here we call the method below to start the background data feed
            StartDataFeed();

            // Refresh the map
            await MapView.RefreshAsync();
        }

        private void StartDataFeed()
        {
            // Create a task that runs until we set the cancelFeed variable
            Task.Run(() =>
            {
                // Create a queue and load it up with coordinated from the CSV file
                var vehicleLocationQueue = new Queue<Feature>();

                var locations = File.ReadAllLines(@"./Data/Csv/vehicle-route.csv");

                foreach (var location in locations)
                {
                    vehicleLocationQueue.Enqueue(new Feature(double.Parse(location.Split(',')[0]), double.Parse(location.Split(',')[1])));
                }

                // Keep looping as long as it's not canceled
                do
                {
                    // If the feed is not paused then update the vehicle location
                    if (!_pauseFeed)
                    {
                        // Get the latest point from the queue and then re-add it so the points
                        // will loop forever
                        var currentFeature = vehicleLocationQueue.Dequeue();
                        vehicleLocationQueue.Enqueue(currentFeature);

                        // Call to invoke on the mapView, so we pop over to the main UI thread
                        // to update the map control
                        MapView.Dispatcher.InvokeAsync(async () =>
                        {
                            await UpdateMapAsync(currentFeature);
                        });
                    }

                    // Sleep for two second
                    Debug.WriteLine($"Sleeping Vehicle Location Data Feed: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
                    Thread.Sleep(1000);

                } while (_cancelFeed == false);
            });
        }

        private async Task UpdateMapAsync(Feature currentFeature)
        {
            // We need to first find our vehicle overlay and in memory layer in the map
            var vehicleOverlay = (LayerOverlay)MapView.Overlays["Vehicle Overlay"];
            var vehicleLayer = (InMemoryFeatureLayer)vehicleOverlay.Layers["Vehicle Layer"];

            // Let's clear the old location and add the new one
            vehicleLayer.InternalFeatures.Clear();
            vehicleLayer.InternalFeatures.Add(currentFeature);

            // If we have the center on vehicle check box checked then we center the map on the new location
            if (CenterOnVehicle.IsChecked ?? false)
            {
                await MapView.CenterAtAsync(currentFeature);
            }

            // Refresh the vehicle overlay
            await MapView.RefreshAsync(MapView.Overlays["Vehicle Overlay"]);
        }

        private void RefreshDynamicItems_Unloaded(object sender, RoutedEventArgs e)
        {
            _cancelFeed = true;
        }

        /// <summary>
        /// Pause the data feed
        /// </summary>
        private void PauseDataFeed_Checked(object sender, RoutedEventArgs e)
        {
            _pauseFeed = true;
        }

        /// <summary>
        /// Un-pause the data feed
        /// </summary>
        private void PauseDataFeed_Unchecked(object sender, RoutedEventArgs e)
        {
            _pauseFeed = false;
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