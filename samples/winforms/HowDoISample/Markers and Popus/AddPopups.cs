using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class AddPopups : UserControl
    {
        public AddPopups()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);

            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);

            PopupOverlay popupOverlay = new PopupOverlay();
            mapView.Overlays.Add(popupOverlay);

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer("SampleData/MajorCities_3857.shp");
            majorCitiesShapeLayer.Open();
            Collection<Feature> features = majorCitiesShapeLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            foreach (Feature feature in features)
            {
                Popup popup = new Popup(feature.GetShape().GetCenterPoint());
                popup.Content = feature.ColumnValues["AREANAME"];
                popupOverlay.Popups.Add(popup);
            }
            majorCitiesShapeLayer.Close();
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