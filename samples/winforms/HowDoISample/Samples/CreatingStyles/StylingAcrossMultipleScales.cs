using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Windows.Media;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class StylingAcrossMultipleScales : UserControl
    {
        private readonly ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");
        private readonly ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
        private readonly ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

        public StylingAcrossMultipleScales()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the map background color
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 234, 232, 226));

            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");
            ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Project the layer's data to match the projection of the map
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Styles to the layers
            StyleHotelsLayer(hotelsLayer);
            StyleStreetsLayer(streetsLayer);
            StyleParksLayer(parksLayer);

            // Add layers to a layerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(parksLayer);
            layerOverlay.Layers.Add(streetsLayer);
            layerOverlay.Layers.Add(hotelsLayer);

            // Add overlay to map
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Adds a PointStyle and TextStyle to the Hotels Layer
        /// </summary>
        private void StyleHotelsLayer(ShapeFileFeatureLayer hotelsLayer)
        {
            /********************
             * Zoom Level 12-13 *
             ********************/
            hotelsLayer.ZoomLevelSet.ZoomLevel12.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 4, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));

            hotelsLayer.ZoomLevelSet.ZoomLevel12.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level13;

            /********************
             * Zoom Level 14-15 *
             ********************/
            hotelsLayer.ZoomLevelSet.ZoomLevel14.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 8, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));

            hotelsLayer.ZoomLevelSet.ZoomLevel14.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level15;

            /********************
             * Zoom Level 16-20 *
             ********************/
            hotelsLayer.ZoomLevelSet.ZoomLevel16.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.DarkRed, new GeoPen(GeoBrushes.White, 2));
            hotelsLayer.ZoomLevelSet.ZoomLevel16.DefaultTextStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkRed)
            {
                TextPlacement = TextPlacement.Lower,
                YOffsetInPixel = 4,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true
            };

            hotelsLayer.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds a LineStyle and TextStyle to the Streets Layer
        /// </summary>
        private void StyleStreetsLayer(ShapeFileFeatureLayer streetsLayer)
        {
            /********************
             * Zoom Level 10-11 *
             ********************/
            streetsLayer.ZoomLevelSet.ZoomLevel10.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 1));

            streetsLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level11;

            /*****************
             * Zoom Level 12 *
             *****************/
            streetsLayer.ZoomLevelSet.ZoomLevel12.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 2), new GeoPen(GeoBrushes.White, 1));

            /*****************
             * Zoom Level 13 *
             *****************/
            streetsLayer.ZoomLevelSet.ZoomLevel13.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.LightGray, 4), new GeoPen(GeoBrushes.White, 2));
            streetsLayer.ZoomLevelSet.ZoomLevel13.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 6, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            /*****************
             * Zoom Level 14 *
             *****************/
            streetsLayer.ZoomLevelSet.ZoomLevel14.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 5), new GeoPen(GeoBrushes.White, 4));
            streetsLayer.ZoomLevelSet.ZoomLevel14.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 8, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            /*****************
             * Zoom Level 15 *
             *****************/
            streetsLayer.ZoomLevelSet.ZoomLevel15.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 7), new GeoPen(GeoBrushes.White, 6));
            streetsLayer.ZoomLevelSet.ZoomLevel15.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 9, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            /*****************
             * Zoom Level 16 *
             *****************/
            streetsLayer.ZoomLevelSet.ZoomLevel16.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 9), new GeoPen(GeoBrushes.White, 8));
            streetsLayer.ZoomLevelSet.ZoomLevel16.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 9, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            /********************
             * Zoom Level 17-18 *
             ********************/
            streetsLayer.ZoomLevelSet.ZoomLevel17.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 13), new GeoPen(GeoBrushes.White, 12));
            streetsLayer.ZoomLevelSet.ZoomLevel17.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            streetsLayer.ZoomLevelSet.ZoomLevel17.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level18;

            /********************
             * Zoom Level 19-20 *
             ********************/
            streetsLayer.ZoomLevelSet.ZoomLevel19.DefaultLineStyle = new LineStyle(new GeoPen(GeoBrushes.Gray, 15), new GeoPen(GeoBrushes.White, 14));
            streetsLayer.ZoomLevelSet.ZoomLevel19.DefaultTextStyle = new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.Black)
            {
                SplineType = SplineType.StandardSplining,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                GridSize = 20
            };

            streetsLayer.ZoomLevelSet.ZoomLevel19.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }

        /// <summary>
        /// Adds an AreaStyle and TextStyle to the Parks Layer
        /// </summary>
        private void StyleParksLayer(ShapeFileFeatureLayer parksLayer)
        {
            /********************
             * Zoom Level 10-14 *
             ********************/
            parksLayer.ZoomLevelSet.ZoomLevel10.DefaultAreaStyle = new AreaStyle(GeoBrushes.PastelGreen);

            parksLayer.ZoomLevelSet.ZoomLevel10.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level14;

            /********************
             * Zoom Level 15-20 *
             ********************/
            parksLayer.ZoomLevelSet.ZoomLevel15.DefaultAreaStyle = new AreaStyle(GeoBrushes.PastelGreen);
            parksLayer.ZoomLevelSet.ZoomLevel15.DefaultTextStyle = new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold), GeoBrushes.DarkGreen)
            {
                FittingPolygon = false,
                HaloPen = new GeoPen(GeoBrushes.White, 2),
                DrawingLevel = DrawingLevel.LabelLevel,
                AllowLineCarriage = true,
                FittingPolygonFactor = 1
            };

            parksLayer.ZoomLevelSet.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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