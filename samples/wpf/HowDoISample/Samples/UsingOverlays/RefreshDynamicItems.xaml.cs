using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This samples shows how to refresh points on the map based on some outside event
    /// </summary>
    public partial class RefreshDynamicItems : UserControl
    {        
        bool cancelFeed;
        bool pauseFeed;

        public RefreshDynamicItems()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Setup the overlay that we will refresh often
            LayerOverlay vehicleOverlay = new LayerOverlay();
            
            // This in memory layer will hold the active point, we will be adding and removing from it frequently
            InMemoryFeatureLayer vehicleLayer = new InMemoryFeatureLayer();

            // Set the points image to an car icon and then apply it to all zoomlevels
            vehicleLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(@"../../../Resources/vehicle.png"));
            vehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the in memory layer to the overlay
            vehicleOverlay.Layers.Add("Vehicle Layer", vehicleLayer);
            
            // Add the overlay to the map
            mapView.Overlays.Add("Vehicle Overlay", vehicleOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10779430.188014803, 3912668.1732483786, -10778438.895309737, 3911814.2283277493);

            // We hookup this even so when you leave this sample we stop the background data feed task
            this.Unloaded -= RefreshDynamicItems_Unloaded;
            this.Unloaded += RefreshDynamicItems_Unloaded;

            //  Here we call the method below to start the background data feed
            StartDataFeed();

            // Refresh the map
            mapView.Refresh();
        }

        private async void StartDataFeed()
        {
            // Create a task that runs until we set the cacnelFeed variable

            var task = Task.Run(() =>
            {
                // Create a queue and load it up with coordinated from the CSV file
                Queue<Feature> vehicleLocationQueue = new Queue<Feature>();

                string[] locations = File.ReadAllLines(@"../../../Data/Csv/vehicle-route.csv");

                foreach (var location in locations)
                {
                    vehicleLocationQueue.Enqueue(new Feature(double.Parse(location.Split(',')[0]), double.Parse(location.Split(',')[1])));
                }

                // Keep looping as long as it's not canceled
                do
                {
                    // If the feed is not paused then update the vehicle location
                    if (!pauseFeed)
                    {
                        // Get the latest point from the queue and then re-add it so the points
                        // will loop forever
                        Feature currentFeature = vehicleLocationQueue.Dequeue();
                        vehicleLocationQueue.Enqueue(currentFeature);

                        // Call the invoke on the mapview so we pop over to the main UI thread
                        // to update the map control
                        mapView.Dispatcher.Invoke(() =>
                        {
                            UpdateMap(currentFeature);
                        });
                    }

                    // Sleep for two second
                    Debug.WriteLine($"Sleeping Vehicle Location Data Feed: {DateTime.Now.ToString()}");
                    Thread.Sleep(1000);

                } while (cancelFeed == false);
            });
        }

        private void UpdateMap(Feature currentFeature)
        {
            // We need to first find our vehicle overlay and in memory layer in the map
            LayerOverlay vehicleOverlay = (LayerOverlay)mapView.Overlays["Vehicle Overlay"];
            InMemoryFeatureLayer vehicleLayer = (InMemoryFeatureLayer)vehicleOverlay.Layers["Vehicle Layer"];

            // Let's clear the old location and add the new one
            vehicleLayer.InternalFeatures.Clear();
            vehicleLayer.InternalFeatures.Add(currentFeature);

            // If we have the center on vehicle check box checked then we center the map on the new location
            if (centerOnVehicle.IsChecked ?? false)
            {
                mapView.CenterAt(currentFeature);
            }
            
            // Refresh the vehicle overlay
            mapView.Refresh(mapView.Overlays["Vehicle Overlay"]);
        }

        private void RefreshDynamicItems_Unloaded(object sender, RoutedEventArgs e)
        {
            cancelFeed = true;
        }

        /// <summary>
        /// Pause the data feed
        /// </summary>
        private void PauseDataFeed_Checked(object sender, RoutedEventArgs e)
        {
            pauseFeed = true;
        }

        /// <summary>
        /// Un-pause the data feed
        /// </summary>
        private void PauseDataFeed_Unchecked(object sender, RoutedEventArgs e)
        {
            pauseFeed = false;
        }
    }
}
