using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadClusterPointStyle : UserControl
    {
        private readonly string startGlyphContent = GeoFont.GetGlyphContent(9733);
        private ClassBreakStyle classBreakStyle = null;

        public LoadClusterPointStyle()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            GeoFont geoFont = new GeoFont("unicode", 8);
            var random = new Random();

            var inMemoryFeatureLayer1 = new InMemoryFeatureLayer();
            for (var i = 0; i < 100000; i++)
            {
                var feature = new Feature(random.Next(-1000000, 1000000), random.Next(-1000000, 1000000));
                inMemoryFeatureLayer1.InternalFeatures.Add(feature);
            }

            inMemoryFeatureLayer1.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(geoFont, startGlyphContent, GeoBrushes.Black);
            inMemoryFeatureLayer1.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;


            var inMemoryFeatureLayer2 = new InMemoryFeatureLayer();
            foreach (var feature in inMemoryFeatureLayer1.InternalFeatures)
            {
                inMemoryFeatureLayer2.InternalFeatures.Add(feature);
            }

            var clusterStyle = new ClusterPointStyle(new PointStyle(geoFont, startGlyphContent, GeoBrushes.Red), new TextStyle("FeatureCount", geoFont, new GeoSolidBrush(GeoColors.White)));
            clusterStyle.DrawingClusteredFeature += ClusterStyle_DrawingClusteredFeature;
            clusterStyle.CellSize = 100;
            clusterStyle.MinimumFeaturesPerCellToCluster = 10;

            inMemoryFeatureLayer2.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterStyle);
            inMemoryFeatureLayer2.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay1 = new LayerOverlay();
            layerOverlay1.Layers.Add(inMemoryFeatureLayer1);

            var layerOverlay2 = new LayerOverlay() { TileType = TileType.SingleTile };
            layerOverlay2.Layers.Add(inMemoryFeatureLayer2);

            var backgroundOverlay = new BackgroundOverlay() { BackgroundBrush = new GeoSolidBrush(GeoColors.LightGray) };

            mapView.Overlays.Add(backgroundOverlay);
            mapView.Overlays.Add(layerOverlay1);
            mapView.Overlays.Add(layerOverlay2);

            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-100000, 100000, 100000, -100000);
        }

        private void ClusterStyle_DrawingClusteredFeature(object sender, DrawingClusteredFeatureClusterPointStyleEventArgs e)
        {
            classBreakStyle = classBreakStyle ?? GetClassBreakStyle("FeatureCount");
            e.Styles.Add(classBreakStyle);
        }

        private ClassBreakStyle GetClassBreakStyle(string columnName)
        {
            ClassBreakStyle classBreakStyle = new ClassBreakStyle();
            classBreakStyle.ColumnName = columnName;

            PointStyle pointStyle1 = new PointStyle(new GeoFont("unicode", 8), startGlyphContent, GeoBrushes.Red);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(1, pointStyle1));

            PointStyle pointStyle2 = new PointStyle(new GeoFont("unicode", 15), startGlyphContent, GeoBrushes.OrangeRed);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(10, pointStyle2));

            PointStyle pointStyle3 = new PointStyle(new GeoFont("unicode", 22), startGlyphContent, GeoBrushes.Orange);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(20, pointStyle3));

            PointStyle pointStyle4 = new PointStyle(new GeoFont("unicode", 30), startGlyphContent, GeoBrushes.Green);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(50, pointStyle4));

            PointStyle pointStyle5 = new PointStyle(new GeoFont("unicode", 44), startGlyphContent, GeoBrushes.Blue);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(80, pointStyle5));

            PointStyle pointStyle6 = new PointStyle(new GeoFont("unicode", 60), startGlyphContent, GeoBrushes.Purple);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(100, pointStyle6));

            PointStyle pointStyle7 = new PointStyle(new GeoFont("unicode", 80), startGlyphContent, GeoBrushes.Red);
            classBreakStyle.ClassBreaks.Add(new ClassBreak(120, pointStyle7));

            return classBreakStyle;
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