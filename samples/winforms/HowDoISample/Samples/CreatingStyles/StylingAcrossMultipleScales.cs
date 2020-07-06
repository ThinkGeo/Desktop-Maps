using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class StylingAcrossMultipleScales : UserControl
    {
        private readonly ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Hotels.shp");
        private readonly ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Streets.shp");
        private readonly ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Parks.shp");

        public StylingAcrossMultipleScales()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the map background color
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 232, 232, 232));

            // Project the layer's data to match the projection of the map
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Styles to the layers
            StyleHotelsLayer();
            StyleStreetsLayer();
            StyleParksLayer();

            // Add layers to a layerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(parksLayer);
            layerOverlay.Layers.Add(streetsLayer);
            layerOverlay.Layers.Add(hotelsLayer);

            // Add overlay to map
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            mapView.Refresh();
        }

        /// <summary>
        /// Adds a PointStyle and TextStyle to the Hotels Layer
        /// </summary>
        private void StyleHotelsLayer()
        {
            var pointStyle = new PointStyle(PointSymbolType.Circle, 4, GeoBrushes.Brown, new GeoPen(GeoBrushes.DarkRed, 2));
            var textStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed)
            {
                TextPlacement = TextPlacement.Lower,
                YOffsetInPixel = 2,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds a LineStyle and TextStyle to the Streets Layer
        /// </summary>
        private void StyleStreetsLayer()
        {
            var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 6), new GeoPen(GeoBrushes.WhiteSmoke, 5));
            var textStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.MidnightBlue)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel
            };
            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);
            streetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds an AreaStyle and TextStyle to the Parks Layer
        /// </summary>
        private void StyleParksLayer()
        {
            var areaStyle = new AreaStyle(GeoPens.DimGray, GeoBrushes.PastelGreen);
            var textStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                FittingPolygon = false,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };
            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(areaStyle);
            parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);

            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1175, 629);
            this.mapView.TabIndex = 0;
            // 
            // StylingAcrossMultipleScales
            // 
            this.Controls.Add(this.mapView);
            this.Name = "StylingAcrossMultipleScales";
            this.Size = new System.Drawing.Size(1175, 632);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}