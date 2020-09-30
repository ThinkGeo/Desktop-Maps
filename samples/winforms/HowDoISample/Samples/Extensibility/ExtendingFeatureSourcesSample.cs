using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ExtendingFeatureSourcesSample: UserControl 
    {
        public ExtendingFeatureSourcesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(-155.733, 95.60, 104.42, -81.9);

            BackgroundLayer backgroundLayer = new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add(backgroundLayer);
            worldOverlay.Layers.Add(worldLayer);
            mapView.Overlays.Add(worldOverlay);

            mapView.Refresh();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
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
            this.mapView.ForeColor = System.Drawing.Color.Black;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapFocusMode = ThinkGeo.Core.MapFocusMode.Default;
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1296, 599);
            this.mapView.TabIndex = 0;
            // 
            // ExtendingFeatureSourcesSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "ExtendingFeatureSourcesSample";
            this.Size = new System.Drawing.Size(1296, 599);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code


    }
}