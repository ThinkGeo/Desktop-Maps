using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawGPSLocationOverlaySample : UserControl
    {
        bool cancelFeed;
        bool pauseFeed;

        private delegate void InvokeDelegate(Feature currentFeature);

        public DrawGPSLocationOverlaySample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Setup the overlay that we will refresh often
            LayerOverlay vehicleOverlay = new LayerOverlay();

            // This in memory layer will hold the active point, we will be adding and removing from it frequently
            InMemoryFeatureLayer vehicleLayer = new InMemoryFeatureLayer();

            // Set the points image to an car icon and then apply it to all zoomlevels
            PointStyle vehiclePointStyle = new PointStyle(new GeoImage(@"../../../Resources/vehicle-location.png"));
            vehiclePointStyle.YOffsetInPixel = -12;

            vehicleLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = vehiclePointStyle;
            vehicleLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the in memory layer to the overlay
            vehicleOverlay.Layers.Add("Vehicle Layer", vehicleLayer);

            // Add the overlay to the map
            mapView.Overlays.Add("Vehicle Overlay", vehicleOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10779430.188014803, 3912668.1732483786, -10778438.895309737, 3911814.2283277493);

            // We hookup this even so when you leave this sample we stop the background data feed task
            //this.Unloaded -= RefreshDynamicItems_Unloaded;
            //this.Unloaded += RefreshDynamicItems_Unloaded;

            //  Here we call the method below to start the background data feed
            await StartDataFeed();

            // Refresh the map
            await mapView.RefreshAsync();

        }

        private async Task StartDataFeed()
        {
            // Create a task that runs until we set the cacnelFeed variable


            // Create a queue and load it up with coordinated from the CSV file
            Queue<Feature> vehicleLocationQueue = new Queue<Feature>();

            string[] locations = File.ReadAllLines(@"./Data/Csv/vehicle-route.csv");

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

                    // This event fires when the feature source has new data.  We need to make sure we refresh the map
                    // on the UI threat so we use the Invoke method on the map using the delegate we created at the top.                                    
                    mapView.BeginInvoke(new InvokeDelegate(UpdateMap), new object[] { currentFeature });
                }

                // Sleep for two second
                Debug.WriteLine($"Sleeping Vehicle Location Data Feed: {DateTime.Now}");
                await Task.Delay(1000);

            } while (cancelFeed == false);
        }

        protected override void Dispose(bool disposing)
        {
            cancelFeed = true;
            base.Dispose(disposing);
        }

        private async void UpdateMap(Feature currentFeature)
        {
            // We need to first find our vehicle overlay and in memory layer in the map
            LayerOverlay vehicleOverlay = (LayerOverlay)mapView.Overlays["Vehicle Overlay"];
            InMemoryFeatureLayer vehicleLayer = (InMemoryFeatureLayer)vehicleOverlay.Layers["Vehicle Layer"];

            // Let's clear the old location and add the new one
            vehicleLayer.InternalFeatures.Clear();
            vehicleLayer.InternalFeatures.Add(currentFeature);

            // If we have the center on vehicle check box checked then we center the map on the new location
            if (centerOnVehicle.Checked)
            {
                await mapView.CenterAtAsync(currentFeature);
            }

            // Refresh the vehicle overlay
            await mapView.RefreshAsync(mapView.Overlays["Vehicle Overlay"]);
        }

        private void pauseDataFeed_Click(object sender, EventArgs e)
        {
            pauseFeed = !pauseFeed;
        }

        #region Component Designer generated code

        private MapView mapView;
        private CheckBox centerOnVehicle;
        private Panel panel1;
        private CheckBox pauseDataFeed;
        private Label label1;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.centerOnVehicle = new System.Windows.Forms.CheckBox();
            this.pauseDataFeed = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(5, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(969, 562);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.centerOnVehicle);
            this.panel1.Controls.Add(this.pauseDataFeed);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(975, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 562);
            this.panel1.TabIndex = 1;
            // 
            // centerOnVehicle
            // 
            this.centerOnVehicle.AutoSize = true;
            this.centerOnVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.centerOnVehicle.ForeColor = System.Drawing.Color.White;
            this.centerOnVehicle.Location = new System.Drawing.Point(20, 77);
            this.centerOnVehicle.Name = "centerOnVehicle";
            this.centerOnVehicle.Size = new System.Drawing.Size(142, 21);
            this.centerOnVehicle.TabIndex = 2;
            this.centerOnVehicle.Text = "Center On Vehicle";
            this.centerOnVehicle.UseVisualStyleBackColor = true;
            // 
            // pauseDataFeed
            // 
            this.pauseDataFeed.AutoSize = true;
            this.pauseDataFeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.pauseDataFeed.ForeColor = System.Drawing.Color.White;
            this.pauseDataFeed.Location = new System.Drawing.Point(20, 50);
            this.pauseDataFeed.Name = "pauseDataFeed";
            this.pauseDataFeed.Size = new System.Drawing.Size(137, 21);
            this.pauseDataFeed.TabIndex = 1;
            this.pauseDataFeed.Text = "Pause Data Feed";
            this.pauseDataFeed.UseVisualStyleBackColor = true;
            this.pauseDataFeed.Click += new System.EventHandler(this.pauseDataFeed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Feed Controls:";
            // 
            // RefreshDynamicItemsSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "RefreshDynamicItemsSample";
            this.Size = new System.Drawing.Size(1277, 562);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion Component Designer generated code

    }
}