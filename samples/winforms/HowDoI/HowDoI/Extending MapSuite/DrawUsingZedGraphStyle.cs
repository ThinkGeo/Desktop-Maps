/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;
using ZedGraph;

namespace CSHowDoISamples
{
    public class DrawUsingZedGraphStyle : UserControl
    {
        private delegate Bitmap ToUIThreadDelegate(ZedGraphDrawingEventArgs e);

        public DrawUsingZedGraphStyle()
        {
            InitializeComponent();
        }

        private void DrawUsingZedGraphStyle_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("States", statesLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            ShapeFileFeatureLayer citiesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\MajorCities.shp");

            //Create our Zedgraph Sytle and wire up the event.
            ZedGraphStyle zedGraphStyle = new ZedGraphStyle();
            zedGraphStyle.ZedGraphDrawing += new EventHandler<ZedGraphDrawingEventArgs>(zedGraphStyle_ZedGraphDrawing);

            zedGraphStyle.RequiredColumnNames.Add("WHITE");
            zedGraphStyle.RequiredColumnNames.Add("ASIAN");
            zedGraphStyle.RequiredColumnNames.Add("AREANAME");

            TextStyle textStyle = TextStyles.CreateSimpleTextStyle("AREANAME", "Arial", 12, DrawingFontStyles.Regular, GeoColor.StandardColors.Black, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green), 2, 0, -8);
            citiesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(zedGraphStyle);
            citiesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            citiesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay citiesOverlay = new LayerOverlay();
            citiesOverlay.Layers.Add("Cities", citiesLayer);
            citiesOverlay.TileCache = null;
            winformsMap1.Overlays.Add("CitiesOverlay", citiesOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-13738912, 5155583, -11928884, 3550219);
            winformsMap1.Refresh();
        }

        private void zedGraphStyle_ZedGraphDrawing(object sender, ZedGraphDrawingEventArgs e)
        {
            e.GeoImage = new GeoImage(this.Invoke(new ToUIThreadDelegate(ZedGraphDrawing), new object[] { e }));
        }

        private Bitmap ZedGraphDrawing(ZedGraphDrawingEventArgs e)
        {
            double scale = ExtentHelper.GetScale(winformsMap1.CurrentExtent, winformsMap1.Width, GeographyUnit.Meter);

            // Change the size of the graph based on the scale.  It will get bigger as you zoom in.
            int graphHeight = Convert.ToInt32(1400000000 / scale);
            LayerOverlay staticOverlay = (LayerOverlay)winformsMap1.Overlays["CitiesOverlay"];
            ChangeLabelPosition(((ShapeFileFeatureLayer)staticOverlay.Layers["Cities"]), graphHeight);

            ZedGraphControl zedGraph = new ZedGraphControl();
            zedGraph.Size = new Size(graphHeight, graphHeight);

            zedGraph.GraphPane.Fill.Type = FillType.None;
            zedGraph.GraphPane.Chart.Fill.Type = FillType.None;

            zedGraph.GraphPane.Border.IsVisible = false;
            zedGraph.GraphPane.Chart.Border.IsVisible = false;
            zedGraph.GraphPane.XAxis.IsVisible = false;
            zedGraph.GraphPane.YAxis.IsVisible = false;
            zedGraph.GraphPane.Legend.IsVisible = false;
            zedGraph.GraphPane.Title.IsVisible = false;

            PieItem pieItem1 = zedGraph.GraphPane.AddPieSlice(Convert.ToDouble(e.Feature.ColumnValues["WHITE"], CultureInfo.InvariantCulture), GetColorFromGeoColor(GeoColor.StandardColors.LightBlue), 0, "White");
            pieItem1.LabelDetail.IsVisible = false;

            PieItem pieItem2 = zedGraph.GraphPane.AddPieSlice(Convert.ToDouble(e.Feature.ColumnValues["ASIAN"], CultureInfo.InvariantCulture), GetColorFromGeoColor(GeoColor.StandardColors.LightGreen), 0, "Asian");
            pieItem2.LabelDetail.IsVisible = false;
            pieItem2.Displacement = 0.2;

            zedGraph.AxisChange();

            return zedGraph.GraphPane.GetImage();
        }

        private static void ChangeLabelPosition(ShapeFileFeatureLayer shapeFileLayer, int graphHeight)
        {
            ((TextStyle)shapeFileLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[1]).XOffsetInPixel = -20;
            ((TextStyle)shapeFileLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[1]).YOffsetInPixel = Convert.ToSingle(graphHeight * 0.4);
        }

        private static Color GetColorFromGeoColor(GeoColor geoColor)
        {
            return Color.FromArgb(geoColor.AlphaComponent, geoColor.RedComponent, geoColor.GreenComponent, geoColor.BlueComponent);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Panel panel2;
        private System.Windows.Forms.Label lbAsian;
        private Panel panel1;
        private System.Windows.Forms.Label lbWhite;
        private System.Windows.Forms.Label lblDescription;
        private WinformsMap winformsMap1;
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbAsian = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbWhite = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(5, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(350, 50);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "To use the ZedGraphStyle functions, you have to reference [Install-Path]\\Develope" +
                "r Reference\\Spatial Extensions\\ZedGraph Style Extension\\ZedGraphStyleExtension.d" +
                "ll.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(634, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 51);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // panel2
            //
            this.panel2.BackColor = System.Drawing.Color.LightGreen;
            this.panel2.Controls.Add(this.lbAsian);
            this.panel2.Location = new System.Drawing.Point(53, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(41, 19);
            this.panel2.TabIndex = 1;
            //
            // lbAsian
            //
            this.lbAsian.AutoSize = true;
            this.lbAsian.BackColor = System.Drawing.Color.Transparent;
            this.lbAsian.Location = new System.Drawing.Point(3, 3);
            this.lbAsian.Name = "lbAsian";
            this.lbAsian.Size = new System.Drawing.Size(33, 13);
            this.lbAsian.TabIndex = 0;
            this.lbAsian.Text = "Asian";
            //
            // panel1
            //
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.lbWhite);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(41, 19);
            this.panel1.TabIndex = 8;
            //
            // lbWhite
            //
            this.lbWhite.AutoSize = true;
            this.lbWhite.BackColor = System.Drawing.Color.Transparent;
            this.lbWhite.Location = new System.Drawing.Point(2, 3);
            this.lbWhite.Name = "lbWhite";
            this.lbWhite.Size = new System.Drawing.Size(35, 13);
            this.lbWhite.TabIndex = 7;
            this.lbWhite.Text = "White";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 100;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 7;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.ExtentOverlay.ZoomPercentage = 40;
            //
            // DrawUsingZedGraphStyle
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawUsingZedGraphStyle";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawUsingZedGraphStyle_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}