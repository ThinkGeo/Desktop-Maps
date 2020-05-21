using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAnInMemoryFeatureLayer : UserControl
    {
        public CreateAnInMemoryFeatureLayer()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            mapView.CurrentExtent = new RectangleShape(0, 100, 100, 0);

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.InternalFeatures.Add("Polygon", new Feature("POLYGON((10 60,40 70,30 85, 10 60))"));
            inMemoryLayer.InternalFeatures.Add("Multipoint", new Feature("MULTIPOINT(10 20, 30 20,40 20, 10 30, 30 30, 40 30)"));
            inMemoryLayer.InternalFeatures.Add("Line", new Feature("LINESTRING(60 60, 70 70,75 60, 80 70, 85 60,95 80)"));
            inMemoryLayer.InternalFeatures.Add("Rectangle", new Feature(new RectangleShape(65, 30, 95, 15)));

            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.RoyalBlue));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Blue;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(200, GeoColors.Red), 5);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, GeoColors.Green), 8);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay inmemoryOverlay = new LayerOverlay();
            inmemoryOverlay.TileType = TileType.SingleTile;
            inmemoryOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.White)));
            inmemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            mapView.Overlays.Add("InmemoryOverlay", inmemoryOverlay);
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