using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DragToCopyAMarker : UserControl
    {
        public DragToCopyAMarker()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);

            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);

            SimpleMarkerOverlay markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.DragMode = MarkerDragMode.CopyWithShiftKey;
            mapView.Overlays.Add("MarkerOverlay", markerOverlay);

            Marker marker = new Marker(-10606588, 4715285);
            marker.ImageSource = new BitmapImage(new Uri(@"\Resources\AQUA.png", UriKind.RelativeOrAbsolute));
            marker.Width = 20;
            marker.Height = 34;
            marker.YOffset = -17;
            markerOverlay.Markers.Add(marker);
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