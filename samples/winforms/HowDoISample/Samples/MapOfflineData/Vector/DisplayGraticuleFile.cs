using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayGraticuleFile : UserControl
    {
        public DisplayGraticuleFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            var graticuleFeatureLayer = new GraticuleFeatureLayer
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };

            // We set the pen color to the graticule layer.
            graticuleFeatureLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("graticule", graticuleFeatureLayer);

            // Set the current extent of the map to start in Frisco TX
            mapView.CurrentExtent = new RectangleShape(-10782364.041857453, 3914916.6811720245, -10772029.75569071, 3908067.923475721);

            //Refresh the map.
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
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1023, 552);
            this.mapView.TabIndex = 0;
            // 
            // DisplayGraticuleFile
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayGraticuleFile";
            this.Size = new System.Drawing.Size(1023, 552);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}