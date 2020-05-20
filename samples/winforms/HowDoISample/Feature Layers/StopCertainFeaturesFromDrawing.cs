using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class StopCertainFeaturesFromDrawing : UserControl
    {
        public StopCertainFeaturesFromDrawing()
        {
            InitializeComponent();
        }

        private void StopCertainFeaturesFromDrawing_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.RequiredColumnNames.Add("POP_CNTRY");
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.DrawingFeatures += new EventHandler<DrawingFeaturesEventArgs>(worldLayer_DrawingFeatures);

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        private void worldLayer_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            Collection<Feature> featuresToDrawn = new Collection<Feature>();
            foreach (Feature feature in e.FeaturesToDraw)
            {
                double population = Convert.ToDouble(feature.ColumnValues["POP_CNTRY"], CultureInfo.InvariantCulture);
                if (population > 10000000)
                {
                    featuresToDrawn.Add(feature);
                }
            }

            e.FeaturesToDraw.Clear();
            foreach (Feature feature in featuresToDrawn)
            {
                e.FeaturesToDraw.Add(feature);
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox gbxDescrition;
        private MapView winformsMap1;
        private Label label1;

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
            this.gbxDescrition = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.label1);
            this.gbxDescrition.Location = new System.Drawing.Point(525, 0);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(212, 57);
            this.gbxDescrition.TabIndex = 3;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "The countries with a  population of less \r\n than 10 millions people will not draw" +
                ".";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 4;
            this.winformsMap1.Text = "winformsMap1";
            //
            // StopCertainFeaturesFromDrawing
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "StopCertainFeaturesFromDrawing";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.StopCertainFeaturesFromDrawing_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}