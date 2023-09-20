using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateClusterPointStyleSample : UserControl
    {
        private readonly ShapeFileFeatureLayer coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp");

        public CreateClusterPointStyleSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp");

            // Project the layer's data to match the projection of the map
            coyoteSightings.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(coyoteSightings);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Apply HeatStyle
            AddClusterPointStyle(coyoteSightings);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10812042.5236828, 3942445.36497713, -10748599.7905585, 3887792.89005685);

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Create and add a cluster style to the coyote layer
        /// </summary>
        private void AddClusterPointStyle(ShapeFileFeatureLayer layer)
        {
            // Create the point style that will serve as the basis of the cluster style
            var pointStyle = new PointStyle(new GeoImage(@"../../../Resources/coyote_paw.png"))
            {
                ImageScale = .25,
                Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                MaskType = MaskType.RoundedCorners
            };

            // Create a text style that will display the number of features within a clustered point
            var textStyle = new TextStyle("FeatureCount", new GeoFont("Segoe UI", 14, DrawingFontStyles.Bold), GeoBrushes.DimGray)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 1),
                YOffsetInPixel = 20
            };

            // Create the cluster point style
            var clusterPointStyle = new ClusterPointStyle(pointStyle, textStyle)
            {
                MinimumFeaturesPerCellToCluster = 2
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map.
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
            this.mapView.Size = new System.Drawing.Size(1182, 553);
            this.mapView.TabIndex = 0;
            // 
            // CreateClusterPointStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateClusterPointStyleSample";
            this.Size = new System.Drawing.Size(1182, 553);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}