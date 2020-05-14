using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindUnionBetweenTwoFeatures : UserControl
    {
        public FindUnionBetweenTwoFeatures()
        {
            InitializeComponent();
        }

        private void FindUnionBetweenTwoFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates.shp"));
            worldLayer.Open();
            Collection<Feature> allFeatures = worldLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            worldLayer.Close();

            // Setup the inMemoryLayer.
            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green)));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Green;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            foreach (Feature feature in allFeatures)
            {
                inMemoryLayer.InternalFeatures.Add(feature);
            }

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-165.946875, 86.4359375, -35.86875, -6.3765625);
            winformsMap1.Refresh();
        }

        private void btnUnion_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");

            if (inMemoryLayer.InternalFeatures.Count > 1)
            {
                GeoCollection<AreaBaseShape> areaBaseShapes = new GeoCollection<AreaBaseShape>();

                GeoCollection<Feature> features = inMemoryLayer.InternalFeatures;
                foreach (Feature feature in features)
                {
                    AreaBaseShape targetAreaBaseShape = feature.GetShape() as AreaBaseShape;
                    if (targetAreaBaseShape != null)
                    {
                        areaBaseShapes.Add(targetAreaBaseShape);
                    }
                }

                AreaBaseShape unionedAreaBaseShape = AreaBaseShape.Union(areaBaseShapes);
                unionedAreaBaseShape.Id = "UnionedFeature";
                inMemoryLayer.InternalFeatures.Clear();
                inMemoryLayer.InternalFeatures.Add("UnionedFeature", new Feature(unionedAreaBaseShape));
                inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green));

                winformsMap1.Refresh(winformsMap1.Overlays["InMemoryOverlay"]);
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private MapView winformsMap1;
        private Button btnUnion;

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
            this.btnUnion = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnUnion);
            this.groupBox1.Location = new System.Drawing.Point(598, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 60);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnUnion
            //
            this.btnUnion.Location = new System.Drawing.Point(13, 20);
            this.btnUnion.Name = "btnUnion";
            this.btnUnion.Size = new System.Drawing.Size(118, 23);
            this.btnUnion.TabIndex = 0;
            this.btnUnion.Text = "Union";
            this.btnUnion.UseVisualStyleBackColor = true;
            this.btnUnion.Click += new System.EventHandler(this.btnUnion_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 8;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindUnionBetweenTwoFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindUnionBetweenTwoFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindUnionBetweenTwoFeatures_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}