using System;
using System.Text;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class TimezoneCloudServicesSample: UserControl
    {
        private TimeZoneCloudClient timeZoneCloudClient;

        public TimezoneCloudServicesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a PopupOverlay to display time zone information based on locations input by the user
            PopupOverlay timezoneInfoPopupOverlay = new PopupOverlay();

            // Add the overlay to the map
            mapView.Overlays.Add("Timezone Info Popup Overlay", timezoneInfoPopupOverlay);

            // Add a new InMemoryFeatureLayer to hold the timezone shapes
            InMemoryFeatureLayer timezonesFeatureLayer = new InMemoryFeatureLayer();

            // Add a style to use to draw the timezone polygons
            timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            timezonesFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Add the layer to an overlay, and add it to the map
            LayerOverlay timezonesLayerOverlay = new LayerOverlay();
            timezonesLayerOverlay.Layers.Add("Timezone Feature Layer", timezonesFeatureLayer);
            mapView.Overlays.Add("Timezone Layer Overlay", timezonesLayerOverlay);

            // Initialize the TimezoneCloudClient with our ThinkGeo Cloud credentials
            timeZoneCloudClient = new TimeZoneCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Set the Map Extent
            mapView.CurrentExtent = new RectangleShape(-14269933.09, 6354969.40, -6966221.89, 2759371.58);

            // Get Timezone info for Frisco, TX
            GetTimeZoneInfo(-10779572.80, 3915268.68);
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Left)
            {
                // Run the timezone info query
                GetTimeZoneInfo(e.WorldX, e.WorldY);
            }
        }

        private async void GetTimeZoneInfo(double lon, double lat)
        {
            CloudTimeZoneResult result;
            try
            {
                // Show a loading graphic to let users know the request is running
                //loadingImage.Visibility = Visibility.Visible;

                // Get timezone info based on the lon, lat, and input projection (Spherical Mercator in this case)
                result = await timeZoneCloudClient.GetTimeZoneByCoordinateAsync(lon, lat, 3857);

                // Hide the loading graphic
                //loadingImage.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                // Hide the loading graphic
                //loadingImage.Visibility = Visibility.Hidden;

                MessageBox.Show(ex.Message, "Error");
                return;
            }

            // Get the timezone info popup overlay from the mapview
            PopupOverlay timezoneInfoPopupOverlay = (PopupOverlay)mapView.Overlays["Timezone Info Popup Overlay"];

            // Clear the existing info popups from the map
            timezoneInfoPopupOverlay.Popups.Clear();

            // Generate a new info popup and add it to the map
            StringBuilder timezoneInfoString = new StringBuilder();
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Time Zone", result.TimeZone));
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Current Local Time", result.CurrentLocalTime));
            timezoneInfoString.AppendLine(String.Format("{0}: {1}", "Daylight Savings Active", result.DaylightSavingsActive));
            Popup popup = new Popup(new PointShape(lon, lat));
            popup.Content = timezoneInfoString.ToString();
            popup.FontSize = 10d;
            popup.FontFamily = new System.Windows.Media.FontFamily("Verdana");
            timezoneInfoPopupOverlay.Popups.Add(popup);

            // Clear the timezone feature layer of previous features
            InMemoryFeatureLayer timezonesFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Timezone Feature Layer");
            timezonesFeatureLayer.Open();
            timezonesFeatureLayer.InternalFeatures.Clear();

            // Use a ProjectionConverter to convert the shape to Spherical Mercator
            ProjectionConverter converter = new ProjectionConverter(3857, 4326);
            converter.Open();

            // Add the new timezone polygon to the map
            timezonesFeatureLayer.InternalFeatures.Add(new Feature(converter.ConvertToInternalProjection(result.Shape)));
            converter.Close();
            timezonesFeatureLayer.Close();

            // Refresh and redraw the map
            mapView.Refresh();
        }

        private Panel panel1;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(906, 713);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(912, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 713);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click on the Map to Get\r\nTimeZone Information";
            // 
            // TimezoneCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "TimezoneCloudServicesSample";
            this.Size = new System.Drawing.Size(1212, 713);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #region Component Designer generated code
        #endregion Component Designer generated code

    }
}