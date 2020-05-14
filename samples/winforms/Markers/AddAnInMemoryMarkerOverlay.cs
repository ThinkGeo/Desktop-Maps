using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class AddAnInMemoryMarkerOverlay : UserControl
    {
        public AddAnInMemoryMarkerOverlay()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);


            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);

            FeatureSourceMarkerOverlay markerOverlay = new FeatureSourceMarkerOverlay(new InMemoryFeatureSource());
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.ImageSource = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Resources\AQUA.png", UriKind.Absolute));
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.Width = 20;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.Height = 34;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.YOffset = -17;
            markerOverlay.ZoomLevelSet.ZoomLevel01.DefaultPointMarkerStyle.ToolTip = "This is [#Name#].";
            markerOverlay.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            mapView.Overlays.Add("MarkerOverlay", markerOverlay);

            Feature newFeature = new Feature(-10606588, 4715285);
            newFeature.ColumnValues.Add("Name", "Lawrence");

            markerOverlay.FeatureSource.Open();
            markerOverlay.FeatureSource.BeginTransaction();
            markerOverlay.FeatureSource.AddFeature(newFeature);
            markerOverlay.FeatureSource.CommitTransaction();
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