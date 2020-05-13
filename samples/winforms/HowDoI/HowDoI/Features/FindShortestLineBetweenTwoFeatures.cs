using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class FindShortestLineBetweenTwoFeatures : UserControl
    {
        public FindShortestLineBetweenTwoFeatures()
        {
            InitializeComponent();
        }

        private void FindShortestLineBetweenTwoFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(0, 100, 100, 0);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.White);

            // Setup the inMemoryLayer.
            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillSolidBrush.Color = GeoColor.FromArgb(125, GeoColor.StandardColors.Gray);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.StandardColors.Black;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryLayer.InternalFeatures.Add("AreaShape1", new Feature("POLYGON((10 20,30 60,40 10,10 20))", "AreaShape1"));
            BaseShape shape = new EllipseShape(new PointShape(70, 70), 10, 20);
            shape.Id = "AreaShape2";
            inMemoryLayer.InternalFeatures.Add("AreaShape2", new Feature(shape));

            InMemoryFeatureLayer shortestLineLayer = new InMemoryFeatureLayer();
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Color = GeoColor.StandardColors.Red;
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            LayerOverlay shortestLineOverlay = new LayerOverlay();
            shortestLineOverlay.Layers.Add("ShortestLineLayer", shortestLineLayer);
            winformsMap1.Overlays.Add("ShortestLineOverlay", shortestLineOverlay);

            winformsMap1.Refresh();
        }

        private void btnGetShortestLine_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");
            InMemoryFeatureLayer shortestLineLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("ShortestLineLayer");

            BaseShape areaShape1 = inMemoryLayer.InternalFeatures["AreaShape1"].GetShape();
            BaseShape areaShape2 = inMemoryLayer.InternalFeatures["AreaShape2"].GetShape();
            MultilineShape multiLineShape = areaShape1.GetShortestLineTo(areaShape2, GeographyUnit.Meter);

            shortestLineLayer.InternalFeatures.Clear();
            shortestLineLayer.InternalFeatures.Add("ShortestLine", new Feature(multiLineShape.GetWellKnownBinary(), "ShortestLine"));

            winformsMap1.Refresh(winformsMap1.Overlays["ShortestLineOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Button btnGetDistance;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetDistance = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnGetDistance);
            this.groupBox1.Location = new System.Drawing.Point(579, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 58);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnGetDistance
            //
            this.btnGetDistance.Location = new System.Drawing.Point(6, 17);
            this.btnGetDistance.Name = "btnGetDistance";
            this.btnGetDistance.Size = new System.Drawing.Size(146, 26);
            this.btnGetDistance.TabIndex = 24;
            this.btnGetDistance.Text = "Find Shortest Line";
            this.btnGetDistance.UseVisualStyleBackColor = true;
            this.btnGetDistance.Click += new System.EventHandler(this.btnGetShortestLine_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 7;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindShortestLineBetweenTwoFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindShortestLineBetweenTwoFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindShortestLineBetweenTwoFeatures_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}