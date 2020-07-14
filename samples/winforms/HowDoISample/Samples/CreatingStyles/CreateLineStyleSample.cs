using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateLineStyleSample : UserControl
    {
        private ShapeFileFeatureLayer friscoStreets;

        public CreateLineStyleSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudRasterMapsMapType.Aerial);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10782598.9806675, 3915669.09132595, -10772234.1196896, 3906343.77392696);

            // Create a layer with line data
            ShapeFileFeatureLayer friscoStreets = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Streets.shp");

            // Project the layer's data to match the projection of the map
            friscoStreets.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoStreets);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Add the line style to the historicSites layer
            AddLineStyle(friscoStreets);
        }

        /// <summary>
        /// Create a lineStyle and add it to the Frisco Streets layer
        /// </summary>
        private void AddLineStyle(ShapeFileFeatureLayer layer)
        {
            // Create a line style
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 4), new GeoPen(GeoBrushes.WhiteSmoke, 2));

            // Add the line style to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
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
            this.mapView.Size = new System.Drawing.Size(1162, 624);
            this.mapView.TabIndex = 0;
            // 
            // CreateLineStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateLineStyleSample";
            this.Size = new System.Drawing.Size(1162, 624);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}