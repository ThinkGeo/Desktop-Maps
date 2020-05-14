using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ConvertAGeoColorToAndFromOleWin32HtmlArgbColors : UserControl
    {
        public ConvertAGeoColorToAndFromOleWin32HtmlArgbColors()
        {
            InitializeComponent();
        }

        private void ConvertAGeoColorToAndFromOleWin32HtmlArgbColors_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            //winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            winformsMap1.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);

            winformsMap1.Refresh();

            cmbGeoColors.SelectedIndex = 0;
        }

        private void cmbGeoColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string geoColorName = cmbGeoColors.SelectedItem.ToString();
            GeoColor geoColor = GeoColors.ShallowOcean;
            switch (geoColorName)
            {
                case "ShallowOcean":
                    geoColor = GeoColors.ShallowOcean;
                    break;

                case "Sand":
                    geoColor = GeoColors.Sand;
                    break;

                case "Swamp":
                    geoColor = GeoColors.Swamp;
                    break;

                case "Silver":
                    geoColor = GeoColors.Silver;
                    break;

                case "Gold":
                    geoColor = GeoColors.Gold;
                    break;

                case "Transparent":
                    geoColor = GeoColors.Transparent;
                    break;

                default:
                    break;
            }
            txtArgbColor.Text = string.Format(CultureInfo.InvariantCulture, "A:{0}  R:{1}  G:{2}  B:{3}", geoColor.AlphaComponent, geoColor.RedComponent, geoColor.GreenComponent, geoColor.BlueComponent);
            txtHtmlColor.Text = GeoColor.ToHtml(geoColor);
            txtOleColor.Text = GeoColor.ToOle(geoColor).ToString(CultureInfo.InvariantCulture);
            txtWin32Color.Text = GeoColor.ToWin32(geoColor).ToString(CultureInfo.InvariantCulture);

            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(geoColor);

            winformsMap1.Refresh(winformsMap1.BackgroundOverlay);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private TextBox txtHtmlColor;
        private Label label3;
        private TextBox txtWin32Color;
        private TextBox txtOleColor;
        private Label label2;
        private Label label1;
        private TextBox txtArgbColor;
        private Label label5;
        private ComboBox cmbGeoColors;
        private Label label4;
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtArgbColor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGeoColors = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtHtmlColor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWin32Color = new System.Windows.Forms.TextBox();
            this.txtOleColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.txtArgbColor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbGeoColors);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtHtmlColor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtWin32Color);
            this.groupBox1.Controls.Add(this.txtOleColor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(436, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 175);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background Color";
            //
            // txtArgbColor
            //
            this.txtArgbColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArgbColor.Location = new System.Drawing.Point(132, 53);
            this.txtArgbColor.Name = "txtArgbColor";
            this.txtArgbColor.ReadOnly = true;
            this.txtArgbColor.Size = new System.Drawing.Size(163, 20);
            this.txtArgbColor.TabIndex = 25;
            //
            // label5
            //
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Background ARGB Color:";
            //
            // cmbGeoColors
            //
            this.cmbGeoColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeoColors.FormattingEnabled = true;
            this.cmbGeoColors.Items.AddRange(new object[] {
            "ShallowOcean",
            "Sand",
            "Swamp",
            "Silver",
            "Gold",
            "Transparent"});
            this.cmbGeoColors.Location = new System.Drawing.Point(132, 23);
            this.cmbGeoColors.Name = "cmbGeoColors";
            this.cmbGeoColors.Size = new System.Drawing.Size(163, 21);
            this.cmbGeoColors.TabIndex = 23;
            cmbGeoColors.SelectedIndex = 1;
            this.cmbGeoColors.SelectedIndexChanged += new System.EventHandler(this.cmbGeoColors_SelectedIndexChanged);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Background GeoColor:";
            //
            // txtHtmlColor
            //
            this.txtHtmlColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHtmlColor.Location = new System.Drawing.Point(132, 140);
            this.txtHtmlColor.Name = "txtHtmlColor";
            this.txtHtmlColor.ReadOnly = true;
            this.txtHtmlColor.Size = new System.Drawing.Size(163, 20);
            this.txtHtmlColor.TabIndex = 17;
            //
            // label3
            //
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Background Html Color:";
            //
            // txtWin32Color
            //
            this.txtWin32Color.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWin32Color.Location = new System.Drawing.Point(132, 82);
            this.txtWin32Color.Name = "txtWin32Color";
            this.txtWin32Color.ReadOnly = true;
            this.txtWin32Color.Size = new System.Drawing.Size(163, 20);
            this.txtWin32Color.TabIndex = 12;
            //
            // txtOleColor
            //
            this.txtOleColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOleColor.Location = new System.Drawing.Point(132, 111);
            this.txtOleColor.Name = "txtOleColor";
            this.txtOleColor.ReadOnly = true;
            this.txtOleColor.Size = new System.Drawing.Size(163, 20);
            this.txtOleColor.TabIndex = 11;
            //
            // label2
            //
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Background Win32 Color:";
            //
            // label1
            //
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Background Ole Color:";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 13;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ConvertAGeoColorToAndFromOleWin32HtmlArgbColors
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ConvertAGeoColorToAndFromOleWin32HtmlArgbColors";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ConvertAGeoColorToAndFromOleWin32HtmlArgbColors_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}