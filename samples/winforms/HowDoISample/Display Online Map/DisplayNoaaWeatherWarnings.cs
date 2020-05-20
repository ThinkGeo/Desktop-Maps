using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayNoaaWeatherWarnings : UserControl
    {
        private ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay;

        public DisplayNoaaWeatherWarnings()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            //Add LayerOverlay to map.
            LayerOverlay noaaWeatherStationOverlay = new LayerOverlay();
            mapView.Overlays.Add(noaaWeatherStationOverlay);

            //Add NoaaWeatherStationFeatureLayer to LayerOverlay.
            NoaaWeatherWarningsFeatureLayer noaaWeatherStationFeatureLayer = new NoaaWeatherWarningsFeatureLayer();
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            noaaWeatherStationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            noaaWeatherStationFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            noaaWeatherStationOverlay.Layers.Add(noaaWeatherStationFeatureLayer);

            mapView.CurrentExtent = new RectangleShape(-14534042, 9147820, -4906645, 1270446);
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
