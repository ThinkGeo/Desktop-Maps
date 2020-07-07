using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UsingMarkersSample: UserControl
    {
        public UsingMarkersSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            AddHotelMarkers();

            AddSimpleMarkers();
        }

        private void AddSimpleMarkers()
        {
            SimpleMarkerOverlay simpleMarkerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("simpleMarkerOverlay", simpleMarkerOverlay);
        }

        private void AddHotelMarkers()
        {
            // Create the FeatureSourceMarkerOverlay and load in Frisco Hotels as the feature source
            FeatureSourceMarkerOverlay hotelMarkers = new FeatureSourceMarkerOverlay()
            {
                FeatureSource = new ShapeFileFeatureSource(@"../../../Data/Shapefile/Hotels.shp")
            };

            // Project the Hotel POI data to match the projection on the map
            hotelMarkers.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Create a style for the hotels
            var pointMarkerStyle = new PointMarkerStyle()
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17,
                ToolTip = "[#NAME#]."
            };
            hotelMarkers.ZoomLevelSet.ZoomLevel01.CustomMarkerStyle = pointMarkerStyle;
            hotelMarkers.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the hotelMarkers overlay to the map
            mapView.Overlays.Add("hotelMarkers", hotelMarkers);
        }

        private Panel panel1;
        private RadioButton copyMode;
        private RadioButton dragMode;
        private RadioButton staticMode;
        private Label label1;
        private Label label2;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.copyMode = new System.Windows.Forms.RadioButton();
            this.dragMode = new System.Windows.Forms.RadioButton();
            this.staticMode = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.mapView.Size = new System.Drawing.Size(692, 621);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.copyMode);
            this.panel1.Controls.Add(this.dragMode);
            this.panel1.Controls.Add(this.staticMode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(698, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 621);
            this.panel1.TabIndex = 1;
            // 
            // copyMode
            // 
            this.copyMode.AutoSize = true;
            this.copyMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.copyMode.ForeColor = System.Drawing.Color.White;
            this.copyMode.Location = new System.Drawing.Point(25, 100);
            this.copyMode.Name = "copyMode";
            this.copyMode.Size = new System.Drawing.Size(114, 24);
            this.copyMode.TabIndex = 3;
            this.copyMode.TabStop = true;
            this.copyMode.Text = "Copy Mode";
            this.copyMode.UseVisualStyleBackColor = true;
            this.copyMode.CheckedChanged += new System.EventHandler(this.copyMode_CheckedChanged);
            // 
            // dragMode
            // 
            this.dragMode.AutoSize = true;
            this.dragMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.dragMode.ForeColor = System.Drawing.Color.White;
            this.dragMode.Location = new System.Drawing.Point(25, 73);
            this.dragMode.Name = "dragMode";
            this.dragMode.Size = new System.Drawing.Size(113, 24);
            this.dragMode.TabIndex = 2;
            this.dragMode.TabStop = true;
            this.dragMode.Text = "Drag Mode";
            this.dragMode.UseVisualStyleBackColor = true;
            this.dragMode.CheckedChanged += new System.EventHandler(this.dragMode_CheckedChanged);
            // 
            // staticMode
            // 
            this.staticMode.AutoSize = true;
            this.staticMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.staticMode.ForeColor = System.Drawing.Color.White;
            this.staticMode.Location = new System.Drawing.Point(25, 46);
            this.staticMode.Name = "staticMode";
            this.staticMode.Size = new System.Drawing.Size(119, 24);
            this.staticMode.TabIndex = 1;
            this.staticMode.TabStop = true;
            this.staticMode.Text = "Static Mode";
            this.staticMode.UseVisualStyleBackColor = true;
            this.staticMode.CheckedChanged += new System.EventHandler(this.staticMode_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "MarkerOverlay Controls:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(145, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Using the Shift Key";
            // 
            // UsingMarkersSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "UsingMarkersSample";
            this.Size = new System.Drawing.Size(998, 621);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void staticMode_CheckedChanged(object sender, EventArgs e)
        {
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.None;
        }

        private void dragMode_CheckedChanged(object sender, EventArgs e)
        {
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.Drag;
        }

        private void copyMode_CheckedChanged(object sender, EventArgs e)
        {
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["simpleMarkerOverlay"];
            simpleMarkerOverlay.DragMode = MarkerDragMode.CopyWithShiftKey;
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            SimpleMarkerOverlay simpleMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["simpleMarkerOverlay"];

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
            simpleMarkerOverlay.Refresh();

        }



    }
}