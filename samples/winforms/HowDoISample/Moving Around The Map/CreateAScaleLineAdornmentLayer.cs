using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class CreateAScaleLineAdornmentLayer : UserControl
    {
        public CreateAScaleLineAdornmentLayer()
        {
            InitializeComponent();
        }

        private void CreateAScaleLineAdormentLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add("WorldOverlay", thinkGeoCloudRasterMapsOverlay);

            ScaleLineAdornmentLayer scaleLineAdornmentLayer = new ScaleLineAdornmentLayer();
            winformsMap1.AdornmentOverlay.Layers.Add("ScaleLineAdornmentLayer", scaleLineAdornmentLayer);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void cbxScaleLineLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScaleLineAdornmentLayer currentScaleLineAdornmentLayer = (ScaleLineAdornmentLayer)winformsMap1.AdornmentOverlay.Layers["ScaleLineAdornmentLayer"];
            currentScaleLineAdornmentLayer.Location = (AdornmentLocation)cbxScaleLineLocation.SelectedIndex;

            winformsMap1.Refresh(winformsMap1.AdornmentOverlay);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private ComboBox cbxScaleLineLocation;
        private Label lblLocation;
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
            this.cbxScaleLineLocation = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbxScaleLineLocation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Location = new System.Drawing.Point(576, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 64);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // cbxScaleLineLocation
            //
            this.cbxScaleLineLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxScaleLineLocation.FormattingEnabled = true;
            this.cbxScaleLineLocation.Items.AddRange(new object[] {
            "UseOffsets",
            "UpperLeft",
            "UpperCenter",
            "UpperRight",
            "CenterLeft",
            "Center",
            "CenterRight",
            "LowerLeft",
            "LowerCenter",
            "LowerRight"});
            this.cbxScaleLineLocation.Location = new System.Drawing.Point(12, 32);
            this.cbxScaleLineLocation.Name = "cbxScaleLineLocation";
            this.cbxScaleLineLocation.Size = new System.Drawing.Size(140, 21);
            this.cbxScaleLineLocation.Text = "LowerLeft";
            this.cbxScaleLineLocation.TabIndex = 3;
            this.cbxScaleLineLocation.SelectedIndexChanged += new System.EventHandler(this.cbxScaleLineLocation_SelectedIndexChanged);
            //
            // lblLocation
            //
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(9, 16);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(145, 13);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Select location for ScaleLine:";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 6;
            this.winformsMap1.Text = "winformsMap1";
            //
            // CreateAScaleLineAdornmentLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "CreateAScaleLineAdornmentLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.CreateAScaleLineAdormentLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}