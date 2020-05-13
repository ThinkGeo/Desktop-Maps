using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class ChangeTheWidthOrColorOfALine : UserControl
    {
        public ChangeTheWidthOrColorOfALine()
        {
            InitializeComponent();
        }

        private void ChangeTheWidthOrColorOfALine_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 242, 239, 233));

            ShapeFileFeatureLayer streetsLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\austinstreets.shp");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.StandardColors.Gray, 5);
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay overlay = new LayerOverlay();
            overlay.Layers.Add("StreetsLayer", streetsLayer);

            winformsMap1.Overlays.Add("StreetsOverlay", overlay);

            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;
            winformsMap1.CurrentExtent = new RectangleShape(-97.745827547760484, 30.297694742808115, -97.728208518132988, 30.285123327073894);

            winformsMap1.Refresh();
        }

        private void btnWidth_Click(object sender, EventArgs e)
        {
            FeatureLayer streetsLayer = winformsMap1.FindFeatureLayer("StreetsLayer");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Width += 2;

            winformsMap1.Refresh(winformsMap1.Overlays["StreetsOverlay"]);
        }

        private void btnNarrow_Click(object sender, EventArgs e)
        {
            FeatureLayer streetsLayer = winformsMap1.FindFeatureLayer("StreetsLayer");
            if (streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Width > 2)
            {
                streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Width -= 2;

                winformsMap1.Refresh(winformsMap1.Overlays["StreetsOverlay"]);
            }
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            FeatureLayer streetsLayer = winformsMap1.FindFeatureLayer("StreetsLayer");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Color = GeoColor.StandardColors.SteelBlue;

            winformsMap1.Refresh(winformsMap1.Overlays["StreetsOverlay"]);
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            FeatureLayer streetsLayer = winformsMap1.FindFeatureLayer("StreetsLayer");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Color = GeoColor.StandardColors.YellowGreen;

            winformsMap1.Refresh(winformsMap1.Overlays["StreetsOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxFunctions;
        private Button btnYellow;
        private Button btnBlue;
        private Button btnNarrow;
        private Button btnWidth;
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
            this.gbxFunctions = new System.Windows.Forms.GroupBox();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnNarrow = new System.Windows.Forms.Button();
            this.btnWidth = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.gbxFunctions.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxFunctions
            //
            this.gbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFunctions.Controls.Add(this.btnYellow);
            this.gbxFunctions.Controls.Add(this.btnBlue);
            this.gbxFunctions.Controls.Add(this.btnNarrow);
            this.gbxFunctions.Controls.Add(this.btnWidth);
            this.gbxFunctions.Location = new System.Drawing.Point(551, 3);
            this.gbxFunctions.Name = "gbxFunctions";
            this.gbxFunctions.Size = new System.Drawing.Size(186, 89);
            this.gbxFunctions.TabIndex = 1;
            this.gbxFunctions.TabStop = false;
            this.gbxFunctions.Text = "Functions";
            //
            // btnYellow
            //
            this.btnYellow.Location = new System.Drawing.Point(95, 49);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(75, 23);
            this.btnYellow.TabIndex = 3;
            this.btnYellow.Text = "Color yellow";
            this.btnYellow.UseVisualStyleBackColor = true;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            //
            // btnBlue
            //
            this.btnBlue.Location = new System.Drawing.Point(95, 19);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(75, 23);
            this.btnBlue.TabIndex = 2;
            this.btnBlue.Text = "Color blue";
            this.btnBlue.UseVisualStyleBackColor = true;
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            //
            // btnNarrow
            //
            this.btnNarrow.Location = new System.Drawing.Point(14, 49);
            this.btnNarrow.Name = "btnNarrow";
            this.btnNarrow.Size = new System.Drawing.Size(75, 23);
            this.btnNarrow.TabIndex = 1;
            this.btnNarrow.Text = "Narrower";
            this.btnNarrow.UseVisualStyleBackColor = true;
            this.btnNarrow.Click += new System.EventHandler(this.btnNarrow_Click);
            //
            // btnWidth
            //
            this.btnWidth.Location = new System.Drawing.Point(14, 19);
            this.btnWidth.Name = "btnWidth";
            this.btnWidth.Size = new System.Drawing.Size(75, 23);
            this.btnWidth.TabIndex = 0;
            this.btnWidth.Text = "Wider";
            this.btnWidth.UseVisualStyleBackColor = true;
            this.btnWidth.Click += new System.EventHandler(this.btnWidth_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 2;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ChangeTheWidthOrColorOfALine
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ChangeTheWidthOrColorOfALine";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ChangeTheWidthOrColorOfALine_Load);
            this.gbxFunctions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}