using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ChangeTheFillColorAndOutlineOfAArea : UserControl
    {
        public ChangeTheFillColorAndOutlineOfAArea()
        {
            InitializeComponent();
        }

        private void ChangeTheFillColorAndOutLineOfAArea_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates.shp"));
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColors.Gray);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Black;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay overlay = new LayerOverlay();
            overlay.Layers.Add("StatesLayer", statesLayer);

            winformsMap1.Overlays.Add("StatesOverlay", overlay);

            winformsMap1.CurrentExtent = new RectangleShape(-170.0, 68.5, -59.0, 16.0);
            winformsMap1.Refresh();
        }

        private void btnFillBlue_Click(object sender, EventArgs e)
        {
            FeatureLayer statesLayer = winformsMap1.FindFeatureLayer("StatesLayer");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColors.SteelBlue);

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        private void btnFillYellow_Click(object sender, EventArgs e)
        {
            FeatureLayer statesLayer = winformsMap1.FindFeatureLayer("StatesLayer");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColors.LightYellow);

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            FeatureLayer statesLayer = winformsMap1.FindFeatureLayer("StatesLayer");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Blue;

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            FeatureLayer statesLayer = winformsMap1.FindFeatureLayer("StatesLayer");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Yellow;

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        #region Component Designer generated code

        private Button btnBlue;
        private GroupBox gbxFunctions;
        private Button btnYellow;
        private Button btnFillYellow;
        private Button btnFillBlue;
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
            this.btnBlue = new System.Windows.Forms.Button();
            this.gbxFunctions = new System.Windows.Forms.GroupBox();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnFillYellow = new System.Windows.Forms.Button();
            this.btnFillBlue = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.gbxFunctions.SuspendLayout();
            this.SuspendLayout();
            //
            // btnBlue
            //
            this.btnBlue.Location = new System.Drawing.Point(101, 19);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(106, 23);
            this.btnBlue.TabIndex = 2;
            this.btnBlue.Text = "Outline color blue";
            this.btnBlue.UseVisualStyleBackColor = true;
            this.btnBlue.Click += new System.EventHandler(this.btnBlue_Click);
            //
            // gbxFunctions
            //
            this.gbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFunctions.Controls.Add(this.btnYellow);
            this.gbxFunctions.Controls.Add(this.btnBlue);
            this.gbxFunctions.Controls.Add(this.btnFillYellow);
            this.gbxFunctions.Controls.Add(this.btnFillBlue);
            this.gbxFunctions.Location = new System.Drawing.Point(526, 3);
            this.gbxFunctions.Name = "gbxFunctions";
            this.gbxFunctions.Size = new System.Drawing.Size(211, 89);
            this.gbxFunctions.TabIndex = 2;
            this.gbxFunctions.TabStop = false;
            this.gbxFunctions.Text = "Functions";
            //
            // btnYellow
            //
            this.btnYellow.Location = new System.Drawing.Point(101, 49);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(106, 23);
            this.btnYellow.TabIndex = 3;
            this.btnYellow.Text = "Outline color yellow";
            this.btnYellow.UseVisualStyleBackColor = true;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            //
            // btnFillYellow
            //
            this.btnFillYellow.Location = new System.Drawing.Point(11, 49);
            this.btnFillYellow.Name = "btnFillYellow";
            this.btnFillYellow.Size = new System.Drawing.Size(85, 23);
            this.btnFillYellow.TabIndex = 1;
            this.btnFillYellow.Text = "Fill color yellow";
            this.btnFillYellow.UseVisualStyleBackColor = true;
            this.btnFillYellow.Click += new System.EventHandler(this.btnFillYellow_Click);
            //
            // btnFillBlue
            //
            this.btnFillBlue.Location = new System.Drawing.Point(11, 19);
            this.btnFillBlue.Name = "btnFillBlue";
            this.btnFillBlue.Size = new System.Drawing.Size(85, 23);
            this.btnFillBlue.TabIndex = 0;
            this.btnFillBlue.Text = "Fill color Blue";
            this.btnFillBlue.UseVisualStyleBackColor = true;
            this.btnFillBlue.Click += new System.EventHandler(this.btnFillBlue_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ChangeTheFillColorAndOutlineOfAArea
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ChangeTheFillColorAndOutlineOfAArea";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ChangeTheFillColorAndOutLineOfAArea_Load);
            this.gbxFunctions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}