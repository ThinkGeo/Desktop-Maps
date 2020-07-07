using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetAllFeatureDataSample: UserControl
    {
        public GetAllFeatureDataSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold the Frisco hotels data
            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Hotels.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            hotelsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco hotel points
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            hotelsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);

            InMemoryFeatureLayer highlightedHotelLayer = new InMemoryFeatureLayer();
            highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 30, GeoBrushes.BrightYellow, GeoPens.Black);

            // Add the feature layer to an overlay, and add the overlay to the map
            LayerOverlay hotelsOverlay = new LayerOverlay();
            hotelsOverlay.Layers.Add("Frisco Hotels", hotelsLayer);
            hotelsOverlay.Layers.Add("Highlighted Hotel", highlightedHotelLayer);
            mapView.Overlays.Add(hotelsOverlay);

            // Open the hotels layer so we can read the data from it
            hotelsLayer.Open();

            // Get all features from the hotels layer
            // ReturningColumnsType.AllColumns will return all attributes for the features
            var features = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

            // Create a collection of Hotel objects to use as the data source for our list box
            Collection<Hotel> hotels = new Collection<Hotel>();

            // Create a hotel object based on the data from each hotel feature, and add them to the collection
            foreach (Feature feature in features)
            {
                string name = feature.ColumnValues["NAME"];
                string address = feature.ColumnValues["ADDRESS"];
                int rooms = int.Parse(feature.ColumnValues["ROOMS"]);
                PointShape location = (PointShape)feature.GetShape();

                hotels.Add(new Hotel(name, address, rooms, location));
            }

            // Set the hotel collection as the data source of the list box
            lsbHotels.DataSource = hotels;
            //lsbHotels.ValueMember = "Name";
            lsbHotels.DisplayMember = "Name";
            mapView.CurrentExtent = hotelsLayer.GetBoundingBox();
            hotelsLayer.Close();

            // Refresh and redraw the map
            mapView.Refresh();
        }



        private Panel panel1;
        private ListBox lsbHotels;
        private Label label2;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lsbHotels = new System.Windows.Forms.ListBox();
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
            this.mapView.Size = new System.Drawing.Size(748, 665);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.lsbHotels);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(754, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 665);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Frisco Hotels Feature Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(289, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select a hotel from the list to zoom to\r\nthat location";
            // 
            // lsbHotels
            // 
            this.lsbHotels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lsbHotels.FormattingEnabled = true;
            this.lsbHotels.ItemHeight = 20;
            this.lsbHotels.Location = new System.Drawing.Point(15, 108);
            this.lsbHotels.Name = "lsbHotels";
            this.lsbHotels.Size = new System.Drawing.Size(268, 444);
            this.lsbHotels.TabIndex = 2;
            this.lsbHotels.SelectedIndexChanged += new System.EventHandler(this.lsbHotels_SelectedIndexChanged);
            // 
            // GetAllFeatureDataSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GetAllFeatureDataSample";
            this.Size = new System.Drawing.Size(1056, 665);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void lsbHotels_SelectedIndexChanged(object sender, EventArgs e)
        {
            InMemoryFeatureLayer highlightedHotelLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Highlighted Hotel");
            highlightedHotelLayer.Open();
            highlightedHotelLayer.InternalFeatures.Clear();

            // Get the selected location
            Hotel hotel = lsbHotels.SelectedItem as Hotel;
            if (hotel != null)
            {
                highlightedHotelLayer.InternalFeatures.Add(new Feature(hotel.Location));

                // Center the map on the chosen location
                mapView.CurrentExtent = hotel.Location.GetBoundingBox();
                ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
                mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel18.Scale);
                mapView.Refresh();
            }

            highlightedHotelLayer.Close();

        }
        public class Hotel
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public int Rooms { get; set; }
            public PointShape Location { get; set; }

            public Hotel(string name, string address, int rooms, PointShape location)
            {
                Name = name;
                Address = address;
                Rooms = rooms;
                Location = location;
            }
        }

    }
}