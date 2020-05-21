using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawUsingFleeBooleanStyle : UserControl
    {
        public DrawUsingFleeBooleanStyle()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Highlight the countries that are land locked and have a population greater than 10 million
            string expression = "(POP_CNTRY>10000000) && (LANDLOCKED=='Y')";
            FleeBooleanStyle landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this
            // to access the Convert.Toxxx methods to convert variable types
            landLockedCountryStyle.StaticTypes.Add(typeof(System.Convert));
            // The math class might be handy to include but in this sample we do not use it
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColors.Green, 2), new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green))));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green)));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"), FileAccess.Read);
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.TileType = TileType.SingleTile;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);
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