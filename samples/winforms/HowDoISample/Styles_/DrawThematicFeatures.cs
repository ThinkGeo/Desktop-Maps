using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawThematicFeatures : UserControl
    {
        public DrawThematicFeatures()
        {
            InitializeComponent();
        }

        private void DrawThematicFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Draw thematic features
            ClassBreakStyle classBreakStyle = new ClassBreakStyle("POP_CNTRY");
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(150, 216, 221, 188))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(1000000, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(150, 144, 238, 144))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(10000000, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(150, 154, 205, 50))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(50000000, AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGreen)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(100000000, AreaStyle.CreateSimpleAreaStyle(GeoColors.DarkGreen)));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69)));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            //winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            winformsMap1.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);
            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private GroupBox gbxDescrition;
        private Label label1;
        private MapView winformsMap1;

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
            this.gbxDescrition.Location = new System.Drawing.Point(555, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(182, 57);
            this.gbxDescrition.TabIndex = 3;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "The population of each country ";
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
            // DrawThematicFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawThematicFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawThematicFeatures_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.gbxDescrition.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}