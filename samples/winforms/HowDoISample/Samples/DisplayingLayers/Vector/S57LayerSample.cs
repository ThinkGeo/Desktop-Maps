using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class S57LayerSample: UserControl
    {
        public S57LayerSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay chartOverlay = new LayerOverlay();

            // Currently this layer only works in single tile mode at the moment.
            // If you use multi tile not all of the data may load in.
            chartOverlay.TileType = TileType.SingleTile;

            // Add the chart to the overlay for display
            mapView.Overlays.Add(chartOverlay);

            // Create the new layer.
            NauticalChartsFeatureLayer nauticalLayer = new NauticalChartsFeatureLayer(@"../../../Data/S57/US1GC09M/US1GC09M.000");

            // Add the layer to the overlay we created earlier.
            chartOverlay.Layers.Add("Charts", nauticalLayer);

            // Set the current extent to a portion of the data
            mapView.CurrentExtent = new RectangleShape(-83.79534200990409, 25.87521424320395, -80.82463888490409, 23.90646424320395);

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
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}