using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class GenerateESRIGridFile : UserControl
    {
        public GenerateESRIGridFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.Snow);

            // Create background hybrid satellite map requested from ThinkGeo Cloud Service. 
            var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1
            };
            mapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            //Applies class break style to show sample points of pH level of a field.
            var classBreakStyle = new ClassBreakStyle("PH");
            const byte alpha = 180;
            const int symbolSize = 10;
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColors.Transparent))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 0, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 128, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 245, 210, 10)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 225, 255, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 224, 251, 132)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 128, 255, 128)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 0, 255, 0)))));

            //load the point shapefile containing ph values in different points of a field.
            var samplesLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/sample_ph_2.shp");
            samplesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            samplesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Create an overlay for our points (and later the grid) and add our points layer to it.
            var gridOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile
            };
            gridOverlay.Layers.Add("GridFeatureLayer", samplesLayer);
            mapView.Overlays.Add("GridFeatureOverlay", gridOverlay);

            //set the map's current extent to the point shapefile location.
            samplesLayer.Open();
            mapView.CurrentExtent = samplesLayer.GetBoundingBox();
            samplesLayer.Close();

            await mapView.RefreshAsync();
        }

        private async void generateGridFile_Click(object sender, EventArgs e)
        {
            // call the functions to generate the grid file and render it.
            const string filename = @"../../../Data/GridFile/generated.grd";
            GenerateGrid(filename);
            await LoadGridAsync(filename);
        }

        private static void GenerateGrid(string filename)
        {
            //Point based shapefile used to create the GRID (sample points of pH level of a field)
            var pointLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/sample_ph_2.shp");

            //Sets the extent of the grid based on the extent of the sample points shapefile and slightly larger.
            pointLayer.Open();
            var gridExtent = pointLayer.GetBoundingBox();
            gridExtent.ScaleUp(5);

            //Gets the data (points with their pH value) to build the GRID.
            var features = pointLayer.FeatureSource.GetAllFeatures(new[] { "PH" });
            var dataPoints = new Dictionary<PointShape, double>();
            pointLayer.Close();

            foreach (var feature in features)
            {
                var pointShape = (PointShape)feature.GetShape();
                var value = double.Parse(feature.ColumnValues["PH"]);
                dataPoints.Add(pointShape, value);
            }

            //Cell size based on the width of the extent divided by 100.
            var cellSize = gridExtent.Width / 100;
            //Sets the definition of the GRID with its extent, the cell size, the non value, and the data (point locations with their value)
            //For more information on GRID definition see http://en.wikipedia.org/wiki/ASCII_GRID
            var definition = new GridDefinition(gridExtent, cellSize, -9999, dataPoints);

            //Inverse Distance Weighted (IDW) is the interpolation model used for the GRID for assigning values to unknown points 
            //based on known neighbor points.
            //http://en.wikipedia.org/wiki/Inverse_distance_weighting
            GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue);

            var outputStream = new FileStream(filename, FileMode.Create);
            GridFeatureSource.GenerateGrid(definition, interpolationModel, outputStream);
        }

        private async Task LoadGridAsync(string filename)
        {
            //Shows how to set the class breaks to display grid cell with color according to its value.
            var gridFeatureLayer = new GridFeatureLayer(filename);
            var gridClassBreakStyle = new ClassBreakStyle("CellValue");
            const byte alpha = 150;
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 0, 255, 0)))));
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = mapView.Overlays["GridFeatureOverlay"] as LayerOverlay;
            if (layerOverlay != null && layerOverlay.Layers.Contains("GridFeatureLayer"))
            {
                layerOverlay.Layers.Remove("GridFeatureLayer");
            }

            if (layerOverlay != null)
            {
                layerOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);

                await mapView.RefreshAsync(layerOverlay);
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label1;
        private Button button1;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.mapView.Size = new System.Drawing.Size(891, 560);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(894, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 560);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(3, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(293, 35);
            this.button1.TabIndex = 9;
            this.button1.Text = "Generate Grid File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.generateGridFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Generate Grid File:";
            // 
            // GenerateESRIGridFile
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GenerateESRIGridFile";
            this.Size = new System.Drawing.Size(1194, 560);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
