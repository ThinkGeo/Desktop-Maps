using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class OpenStreetMapLayerSample : UserControl
    {
        public OpenStreetMapLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the zoom level set on the map to make sure its compatible with the OSM zoom levels.
            mapView.ZoomLevelSet = new OpenStreetMapsZoomLevelSet();

            // Create a new overlay that will hold our new layer and add it to the map and set the tile size to match up with the OSM til size.
            var layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);
            layerOverlay.TileWidth = 256;
            layerOverlay.TileHeight = 256;

            // Create the new layer and add it to the overlay.  We set the user agent to specify the requests are coming from our samples.
            // You need to change this to your application, so they can identify you for usage.
            var openStreetMapLayer = new Core.Async.OpenStreetMapLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
            layerOverlay.Layers.Add(openStreetMapLayer);

            // Set the current extent to a local area.
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
            this.mapView.Size = new System.Drawing.Size(1077, 610);
            this.mapView.TabIndex = 0;
            // 
            // OpenStreetMapLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "OpenStreetMapLayerSample";
            this.Size = new System.Drawing.Size(1077, 610);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code
    }
}