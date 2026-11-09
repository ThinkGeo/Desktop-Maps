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
            var thinkGeoVectorTileOverlay = new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey);
            mapView.Overlays.Add(thinkGeoVectorTileOverlay);

            // Create the new layer. It generates its lines in srid 4326, so set the projection our background is in, srid 3857 (spherical mercator).
            var graticuleLayer = new GraticuleAdornmentLayer
            {
                Projection = new Projection(3857)
            };

            // We set the pen color to the graticule layer.
            graticuleLayer.GraticuleLineStyle.OuterPen.Color = GeoColor.FromArgb(125, GeoColors.Navy);

            // Add the layer to the map's adornment overlay, so its labels stay pinned to the edges of the map while panning.
            mapView.AdornmentOverlay.Layers.Add("graticule", graticuleLayer);

            // Set the current extent of the map to start in Frisco TX
            mapView.CenterPoint = new PointShape(-10777200, 3911500);
            mapView.CurrentScale = 36200;

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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
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