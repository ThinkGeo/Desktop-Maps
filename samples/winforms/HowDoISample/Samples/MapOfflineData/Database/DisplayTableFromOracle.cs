using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayTableFromOracle : UserControl
    {
        public DisplayTableFromOracle()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay schoolOverlay = new LayerOverlay();
            mapView.Overlays.Add(schoolOverlay);

            #region Initialize Oracle feature layer

            //// Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            ////OracleFeatureLayer schoolLayer = new OracleFeatureLayer(@"OCI:system/ThinkGeodatabasepassword!@sampledatabases.thinkgeo.com/xe", "SCHOOLS", "OGR_FID");
            //OracleFeatureLayer schoolLayer = new OracleFeatureLayer(@"OCI:ThinkGeoSampleUser/ThinkGeoSamplePassword@sampledatabases.thinkgeo.com/xe", "SYSTEM.SCHOOLS", "OGR_FID");
            //schoolLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            //// Add the layer to the overlay we created earlier.
            //schoolOverlay.Layers.Add("Coyote Sightings", schoolLayer);

            //// Set a point style to zoom level 1 and then apply it to all zoom levels up to 20.
            //schoolLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Blue, new GeoPen(GeoColors.White, 2));
            //schoolLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            #endregion

            // Set the map view current extent to a bounding box that shows just a few sightings.  
            mapView.CurrentExtent = new RectangleShape(-10789388.4602951, 3923878.18083465, -10768258.7082788, 3906668.46719412);

            // Refresh the map.
            await mapView.RefreshAsync();
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
            this.mapView.Size = new System.Drawing.Size(894, 481);
            this.mapView.TabIndex = 0;
            // 
            // DisplayTableFromOracle
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayTableFromOracle";
            this.Size = new System.Drawing.Size(894, 481);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}