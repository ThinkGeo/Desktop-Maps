using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAreaStyleSample : UserControl
    {
        private ShapeFileFeatureLayer friscoSubdivisions;

        public CreateAreaStyleSample()
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
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            ShapeFileFeatureLayer friscoSubdivisions;

            // Create a layer with polygon data
            friscoSubdivisions = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Parks.shp");

            // Project the layer's data to match the projection of the map
            friscoSubdivisions.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoSubdivisions);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Add the area style to the historicSites layer
            AddAreaStyle(friscoSubdivisions);
        }

        /// <summary>
        /// Create a areaStyle and add it to the Historic Sites layer
        /// </summary>
        private void AddAreaStyle(ShapeFileFeatureLayer layer)
        {
            // Create a area style
            var areaStyle = new AreaStyle(GeoPens.DimGray, new GeoSolidBrush(new GeoColor(128, GeoColors.ForestGreen)));

            // Add the area style to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
            this.mapView.Size = new System.Drawing.Size(1281, 643);
            this.mapView.TabIndex = 0;
            // 
            // CreateAreaStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateAreaStyleSample";
            this.Size = new System.Drawing.Size(1281, 643);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}