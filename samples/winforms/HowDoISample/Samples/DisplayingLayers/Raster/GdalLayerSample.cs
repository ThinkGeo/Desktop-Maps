using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GdalLayerSample : UserControl
    {
        public GdalLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Create the new layer and dd the layer to the overlay we created earlier.
            GdalRasterLayer worldLayer = new GdalRasterLayer("./Data/GeoTiff/World.tif");
            worldLayer.LowerThreshold = 0;
            worldLayer.UpperThreshold = double.MaxValue;

            worldLayer.Open();
            mapView.CurrentExtent = worldLayer.GetBoundingBox();
            worldLayer.Close();

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.DrawingExceptionMode = DrawingExceptionMode.DrawException;

            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add(staticOverlay);

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
            this.mapView.Size = new System.Drawing.Size(1227, 723);
            this.mapView.TabIndex = 0;
            // 
            // GeoTiffLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "GeoTiffLayerSample";
            this.Size = new System.Drawing.Size(1227, 723);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}