using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CustomFeatureLayer : UserControl
    {
        public CustomFeatureLayer()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add(layerOverlay);

            RadiusLayer radiusLayer = new RadiusLayer();
            radiusLayer.RingDistanceUnit = DistanceUnit.Mile;
            radiusLayer.RingGeography = GeographyUnit.Meter;
            radiusLayer.RingDistance = 5;

            layerOverlay.Layers.Add(radiusLayer);
            mapView.CurrentExtent = new RectangleShape(-10812042.5236828, 3942445.36497713, -10748599.7905585, 3887792.89005685);

            await mapView.RefreshAsync();
        }

        protected override void Dispose(bool disposing)
        {
            mapView.Dispose();
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
            // CustomFeatureLayer
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CustomFeatureLayer";
            this.Size = new System.Drawing.Size(1296, 599);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code
    }

    // This layer overrides the DrawCore and draws circles every x miles based on the center point
    // of the screen.  You notice in the DrawCore we can draw directly on the canvas which gives us
    // allot of power.  This is similar to custom styles where we can also draw directly on the canvas
    // from the style.
    public class RadiusLayer : Layer
    {
        public RadiusLayer()
        {
            RingDistanceUnit = DistanceUnit.Mile;
            RingGeography = GeographyUnit.Meter;
            RingDistance = 1;
            RingAreaStyle = new AreaStyle(new GeoPen(new GeoSolidBrush(GeoColor.FromArgb(50, GeoColors.Blue)), 4));
        }

        public double RingDistance { get; set; }

        public DistanceUnit RingDistanceUnit { get; set; }

        public GeographyUnit RingGeography { get; set; }

        public AreaStyle RingAreaStyle { get; set; }
        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            PointShape centerPoint = canvas.CurrentWorldExtent.GetCenterPoint();

            double currentRingDistance = RingDistance;
            MultipolygonShape circle = null;

            // Keep drawing rings until the only barley fit inside the current extent.
            do
            {
                circle = centerPoint.Buffer(currentRingDistance, RingGeography, RingDistanceUnit);

                canvas.DrawArea(circle, RingAreaStyle.OutlinePen, RingAreaStyle.FillBrush, DrawingLevel.LevelOne);
                currentRingDistance += RingDistance;
            } while (canvas.CurrentWorldExtent.Contains(circle));
        }
    }

}