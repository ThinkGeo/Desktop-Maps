using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UsingPopupsSample: UserControl
    {
        public UsingPopupsSample()
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

            AddHotelPopups();
        }

        /// <summary>
        /// Adds hotel popups to the map
        /// </summary>
        private void AddHotelPopups()
        {
            // Create a PopupOverlay
            var popupOverlay = new PopupOverlay();

            // Create a layer in order to query the data
            var hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

            // Project the data to match the map's projection
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Open the layer so that we can begin querying
            hotelsLayer.Open();

            // Query all the hotel features
            var hotelFeatures = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

            // Add each hotel feature to the popupOverlay
            foreach (var feature in hotelFeatures)
            {
                var popup = new Popup(feature.GetShape().GetCenterPoint())
                {
                    Content = feature.ColumnValues["NAME"]
                };
                popupOverlay.Popups.Add(popup);
            }

            // Close the hotel layer
            hotelsLayer.Close();

            // Add the popupOverlay to the map and refresh
            mapView.Overlays.Add(popupOverlay);
            mapView.Refresh();
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
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
            this.mapView.Size = new System.Drawing.Size(950, 585);
            this.mapView.TabIndex = 0;
            // 
            // UsingPopupsSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "UsingPopupsSample";
            this.Size = new System.Drawing.Size(950, 585);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}