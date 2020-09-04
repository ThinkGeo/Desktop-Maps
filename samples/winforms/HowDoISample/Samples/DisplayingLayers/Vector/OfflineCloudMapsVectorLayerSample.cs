using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class OfflineCloudMapsVectorLayerSample : UserControl
    {
        public OfflineCloudMapsVectorLayerSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new overlay that will hold our new layer
            LayerOverlay layerOverlay = new LayerOverlay();

            // Create the background world maps using vector tiles stored locally in our MBTiles file and also set the styling though a json file
            ThinkGeoMBTilesLayer mbTilesLayer = new ThinkGeoMBTilesLayer(@"../../../Data/Mbtiles/Frisco.mbtiles", new Uri(@"../../../Data/Json/thinkgeo-world-streets-dark.json", UriKind.Relative));
            layerOverlay.Layers.Add(mbTilesLayer);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Set the current extent of the map to an area in the data
            mapView.CurrentExtent = new RectangleShape(-10785086.173498387, 3913489.693302595, -10779919.030415015, 3910065.3144544438);

            // Refresh the map.
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
            this.mapView.Size = new System.Drawing.Size(1142, 633);
            this.mapView.TabIndex = 0;
            // 
            // ShapefileLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "ShapefileLayerSample";
            this.Size = new System.Drawing.Size(1142, 633);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}