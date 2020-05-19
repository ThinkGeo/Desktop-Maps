using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawUsingPredefinedCommonStyle : UserControl
    {
        public DrawUsingPredefinedCommonStyle()
        {
            InitializeComponent();
        }

        private void DrawUingPredefinedCommonStyle_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            // Set the areastyle with predefined area styles.
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);

            cbxStyle.SelectedIndex = 0;
        }

        private void cbxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            AreaStyle areaStyle = null;
            switch (cbxStyle.Text)
            {
                case "Country1":
                    areaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
                    break;

                case "Country2":
                    areaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(255, 118, 138, 69));
                    break;

                default:
                    break;
            }
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxDescrition;
        private ComboBox cbxStyle;
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
            this.cbxStyle = new System.Windows.Forms.ComboBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.gbxDescrition.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxDescrition
            //
            this.gbxDescrition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxDescrition.Controls.Add(this.cbxStyle);
            this.gbxDescrition.Location = new System.Drawing.Point(587, 3);
            this.gbxDescrition.Name = "gbxDescrition";
            this.gbxDescrition.Size = new System.Drawing.Size(150, 55);
            this.gbxDescrition.TabIndex = 1;
            this.gbxDescrition.TabStop = false;
            this.gbxDescrition.Text = "Predefined Common Styles";
            //
            // cbxStyle
            //
            this.cbxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStyle.FormattingEnabled = true;
            this.cbxStyle.Items.AddRange(new object[] {
            "Country1",
            "Country2"});
            this.cbxStyle.Location = new System.Drawing.Point(23, 19);
            this.cbxStyle.Name = "cbxStyle";
            this.cbxStyle.Size = new System.Drawing.Size(108, 21);
            this.cbxStyle.TabIndex = 0;
            this.cbxStyle.SelectedIndex = 1;
            this.cbxStyle.SelectedIndexChanged += new System.EventHandler(this.cbxStyle_SelectedIndexChanged);
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
            // DrawUsingPredefinedCommonStyle
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxDescrition);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawUsingPredefinedCommonStyle";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawUingPredefinedCommonStyle_Load);
            this.gbxDescrition.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}