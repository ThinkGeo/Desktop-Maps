using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class OpenStreetMap : UserControl
    {
        public OpenStreetMap()
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
            var openStreetMapLayer = new Core.OpenStreetMapAsyncLayer("ThinkGeo Samples/12.0 (http://thinkgeo.com/; system@thinkgeo.com)");
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
            mapView = new MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(1077, 610);
            mapView.TabIndex = 0;
            // 
            // OpenStreetMap
            // 
            Controls.Add(mapView);
            Name = "OpenStreetMap";
            Size = new System.Drawing.Size(1077, 610);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);

        }
        #endregion Component Designer generated code
    }
}