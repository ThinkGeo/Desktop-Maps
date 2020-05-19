using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UseCustomFontSymbolWithPointStyle : UserControl
    {
        public UseCustomFontSymbolWithPointStyle()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-11523306.58264589, 4293803.999343073, -9676587.989535574, 2828659.049312506);

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            statesLayer.Name = "USStates";
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel04.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel04.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer citiesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities_3857.shp"));
            citiesLayer.Name = "MajorCities";
            citiesLayer.ZoomLevelSet.ZoomLevel04.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 20, DrawingFontStyles.Regular), "\ue0b5", new GeoSolidBrush(GeoColors.Red));
            citiesLayer.ZoomLevelSet.ZoomLevel05.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 25, DrawingFontStyles.Regular), "\ue05c", new GeoSolidBrush(GeoColors.Orange));
            citiesLayer.ZoomLevelSet.ZoomLevel06.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 28, DrawingFontStyles.Regular), "\ue078", new GeoSolidBrush(GeoColors.Green));
            citiesLayer.ZoomLevelSet.ZoomLevel07.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 30, DrawingFontStyles.Regular), "\ue035", new GeoSolidBrush(GeoColors.DarkBlue));
            citiesLayer.ZoomLevelSet.ZoomLevel08.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 34, DrawingFontStyles.Regular), "\ue02e", new GeoSolidBrush(GeoColors.Blue));
            citiesLayer.ZoomLevelSet.ZoomLevel09.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 36, DrawingFontStyles.Regular), "\ue06f", new GeoSolidBrush(GeoColors.Purple));
            citiesLayer.ZoomLevelSet.ZoomLevel10.DefaultPointStyle = new PointStyle(new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 40, DrawingFontStyles.Regular), "\ue084", new GeoSolidBrush(GeoColors.Red));

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.ShallowOcean)));
            worldOverlay.Layers.Add("StatesLayer", statesLayer);
            worldOverlay.Layers.Add("CitiesLayer", citiesLayer);
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