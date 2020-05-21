using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ZoomInAndOutOfTheMap : UserControl
    {
        public ZoomInAndOutOfTheMap()
        {
            InitializeComponent();
        }

        private void ZoomInAndZoomOut_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);

            winformsMap1.Overlays.Add("WorldMapKitWmsOverlay", thinkGeoCloudRasterMapsOverlay);
            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);

            winformsMap1.Refresh();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            winformsMap1.ZoomIn(60);
            winformsMap1.Refresh();
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            winformsMap1.ZoomOut(20);
            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private Button btnZoomOut;
        private Button btnZoomIn;
        private MapView winformsMap1;
        private Label lblDescription;
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
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnZoomOut);
            this.groupBox1.Controls.Add(this.btnZoomIn);
            this.groupBox1.Location = new System.Drawing.Point(660, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(80, 60);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnZoomOut
            //
            this.btnZoomOut.BackColor = System.Drawing.SystemColors.Control;
            this.btnZoomOut.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.ZoomOut;
            this.btnZoomOut.Location = new System.Drawing.Point(43, 20);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(30, 30);
            this.btnZoomOut.TabIndex = 1;
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            //
            // btnZoomIn
            //
            this.btnZoomIn.BackColor = System.Drawing.SystemColors.Control;
            this.btnZoomIn.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.ZoomIn;
            this.btnZoomIn.Location = new System.Drawing.Point(7, 20);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(30, 30);
            this.btnZoomIn.TabIndex = 0;
            this.btnZoomIn.UseVisualStyleBackColor = false;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 6;
            this.winformsMap1.Text = "winformsMap1";
            //
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(3, 3);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(184, 60);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "You can also double right click to zoom out and double left click to zoom in.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ZoomInAndOutOfTheMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ZoomInAndOutOfTheMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ZoomInAndZoomOut_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}