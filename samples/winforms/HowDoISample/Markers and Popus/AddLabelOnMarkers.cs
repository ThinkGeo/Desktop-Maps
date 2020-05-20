using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class AddLabelOnMarkers : System.Windows.Forms.UserControl
    {
        private int index = 1;

        public AddLabelOnMarkers()
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
            mapView.Overlays.Add("MarkerOverlay", markerOverlay);

            mapView.MapClick += mapView_MapClick;
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            SimpleMarkerOverlay markerOverlay = (SimpleMarkerOverlay)mapView.Overlays["MarkerOverlay"];
            Marker marker = new Marker(e.WorldLocation);
            marker.ImageSource = new BitmapImage(new Uri(@"\Resources\AQUABLANK.png", UriKind.RelativeOrAbsolute));
            marker.Width = 20;
            marker.Height = 34;
            marker.YOffset = -17;

            TextBlock content = new TextBlock();
            content.Text = (index++).ToString();
            content.FontSize = 14;
            content.FontWeight = FontWeights.Bold;
            content.Foreground = new SolidColorBrush(Colors.Black);
            content.Margin = new Thickness(0, -10, 0, 0);
            marker.Content = content;

            markerOverlay.Markers.Add(marker);
            markerOverlay.Refresh();
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