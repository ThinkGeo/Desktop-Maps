using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UseGeographicAndStandardColors : UserControl
    {
        public UseGeographicAndStandardColors()
        {
            InitializeComponent();
        }

        private void UseGeographicAndStandardColors_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void standardColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeoColor borderColor = GetStandardColor();
            winformsMap1.FindFeatureLayer("WorldLayer").ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = borderColor;

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        private void geographyColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeoColor areaColor = GetGeographyColor();
            winformsMap1.FindFeatureLayer("WorldLayer").ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(new GeoColor(100, areaColor));

            winformsMap1.Refresh(winformsMap1.Overlays["WorldOverlay"]);
        }

        private GeoColor GetGeographyColor()
        {
            string color = geographyColorComboBox.SelectedItem != null ? geographyColorComboBox.SelectedItem.ToString() : geographyColorComboBox.Text;

            switch (color)
            {
                case "Dirt": return GeoColors.Dirt;
                case "Sand": return GeoColors.Sand;
                case "Swamp": return GeoColors.Swamp;
                default: return GeoColors.Sand;
            }
        }

        private GeoColor GetStandardColor()
        {
            string color = standardColorComboBox.SelectedItem != null ? standardColorComboBox.SelectedItem.ToString() : color = standardColorComboBox.Text;

            switch (color)
            {
                case "Blue": return GeoColors.Blue;
                case "Pink": return GeoColors.Pink;
                case "Teal": return GeoColors.Teal;
                case "Plum": return GeoColors.Plum;
                default: return GeoColors.Gray;
            }
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox standardColorComboBox;
        private Label label2;
        private ComboBox geographyColorComboBox;
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
            this.label1 = new System.Windows.Forms.Label();
            this.standardColorComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.geographyColorComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Change border color by using standard color:";
            //
            // standardColorComboBox
            //
            this.standardColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.standardColorComboBox.FormattingEnabled = true;
            this.standardColorComboBox.Items.AddRange(new object[] {
            "Blue",
            "Pink",
            "Teal",
            "Plum"});
            this.standardColorComboBox.Location = new System.Drawing.Point(237, 19);
            this.standardColorComboBox.Name = "standardColorComboBox";
            this.standardColorComboBox.Size = new System.Drawing.Size(100, 21);
            this.standardColorComboBox.TabIndex = 2;
            standardColorComboBox.SelectedItem = 1;
            this.standardColorComboBox.SelectedIndexChanged += new System.EventHandler(this.standardColorComboBox_SelectedIndexChanged);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(218, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Change area color by using geography color:";
            //
            // geographyColorComboBox
            //
            this.geographyColorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.geographyColorComboBox.FormattingEnabled = true;
            this.geographyColorComboBox.Items.AddRange(new object[] {
            "Dirt",
            "Sand",
            "Swamp"});
            this.geographyColorComboBox.Location = new System.Drawing.Point(237, 50);
            this.geographyColorComboBox.Name = "geographyColorComboBox";
            this.geographyColorComboBox.Size = new System.Drawing.Size(100, 21);
            geographyColorComboBox.SelectedItem = 1;
            this.geographyColorComboBox.TabIndex = 2;
            this.geographyColorComboBox.SelectedIndexChanged += new System.EventHandler(this.geographyColorComboBox_SelectedIndexChanged);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.standardColorComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.geographyColorComboBox);
            this.groupBox1.Location = new System.Drawing.Point(388, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 82);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 5;
            this.winformsMap1.Text = "winformsMap1";
            //
            // UseGeographicAndStandardColors
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "UseGeographicAndStandardColors";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UseGeographicAndStandardColors_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}