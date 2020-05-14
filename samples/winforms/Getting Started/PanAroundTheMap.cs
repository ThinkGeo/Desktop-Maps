using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class PanAroundTheMap : UserControl
    {
        public PanAroundTheMap()
        {
            InitializeComponent();
        }

        private void Pan_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);

            winformsMap1.Refresh();
        }

        private void btnPanUp_Click(object sender, EventArgs e)
        {
            winformsMap1.Pan(PanDirection.Up, 20);
            winformsMap1.Refresh();
        }

        private void btnPanDown_Click(object sender, EventArgs e)
        {
            winformsMap1.Pan(PanDirection.Down, 20);
            winformsMap1.Refresh();
        }

        private void btnPanLeft_Click(object sender, EventArgs e)
        {
            winformsMap1.Pan(PanDirection.Left, 20);
            winformsMap1.Refresh();
        }

        private void btnPanRight_Click(object sender, EventArgs e)
        {
            winformsMap1.Pan(PanDirection.Right, 20);
            winformsMap1.Refresh();
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            float degree = Convert.ToSingle(cmbDegree.SelectedItem.ToString(), CultureInfo.InvariantCulture);
            int percentage = Convert.ToInt32(cmbPercent.SelectedItem.ToString(), CultureInfo.InvariantCulture);

            winformsMap1.Pan(degree, percentage);
            winformsMap1.Refresh();
        }

        private MapView winformsMap1;
        private Label lblDescription;

        #region Component Designer generated code

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
            this.cmbPercent = new System.Windows.Forms.ComboBox();
            this.btnPan = new System.Windows.Forms.Button();
            this.cmbDegree = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPanDown = new System.Windows.Forms.Button();
            this.btnPanUp = new System.Windows.Forms.Button();
            this.btnPanRight = new System.Windows.Forms.Button();
            this.btnPanLeft = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.lblDescription = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbPercent);
            this.groupBox1.Controls.Add(this.btnPan);
            this.groupBox1.Controls.Add(this.cmbDegree);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPanDown);
            this.groupBox1.Controls.Add(this.btnPanUp);
            this.groupBox1.Controls.Add(this.btnPanRight);
            this.groupBox1.Controls.Add(this.btnPanLeft);
            this.groupBox1.Location = new System.Drawing.Point(533, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // cmbPercent
            //
            this.cmbPercent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPercent.FormattingEnabled = true;
            this.cmbPercent.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "80",
            "100"});
            this.cmbPercent.Location = new System.Drawing.Point(146, 47);
            this.cmbPercent.Name = "cmbPercent";
            this.cmbPercent.Size = new System.Drawing.Size(50, 21);
            this.cmbPercent.TabIndex = 4;
            this.cmbPercent.Text = "20";
            //
            // btnPan
            //
            this.btnPan.Location = new System.Drawing.Point(146, 77);
            this.btnPan.Name = "btnPan";
            this.btnPan.Size = new System.Drawing.Size(50, 20);
            this.btnPan.TabIndex = 24;
            this.btnPan.Text = "Pan";
            this.btnPan.UseVisualStyleBackColor = true;
            this.btnPan.Click += new System.EventHandler(this.btnPan_Click);
            //
            // cmbDegree
            //
            this.cmbDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDegree.FormattingEnabled = true;
            this.cmbDegree.Items.AddRange(new object[] {
            "0",
            "30",
            "60",
            "90",
            "120",
            "180",
            "270"});
            this.cmbDegree.Location = new System.Drawing.Point(146, 15);
            this.cmbDegree.Name = "cmbDegree";
            this.cmbDegree.Size = new System.Drawing.Size(50, 21);
            this.cmbDegree.TabIndex = 3;
            this.cmbDegree.Text = "30";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Percent:";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Degree:";
            //
            // btnPanDown
            //
            this.btnPanDown.BackColor = System.Drawing.Color.Transparent;
            this.btnPanDown.FlatAppearance.BorderSize = 0;
            this.btnPanDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanDown.ForeColor = System.Drawing.Color.Transparent;
            this.btnPanDown.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.South;
            this.btnPanDown.Location = new System.Drawing.Point(41, 77);
            this.btnPanDown.Name = "btnPanDown";
            this.btnPanDown.Size = new System.Drawing.Size(22, 22);
            this.btnPanDown.TabIndex = 19;
            this.btnPanDown.UseVisualStyleBackColor = true;
            this.btnPanDown.Click += new System.EventHandler(this.btnPanDown_Click);
            //
            // btnPanUp
            //
            this.btnPanUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanUp.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPanUp.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.North;
            this.btnPanUp.Location = new System.Drawing.Point(41, 19);
            this.btnPanUp.Name = "btnPanUp";
            this.btnPanUp.Size = new System.Drawing.Size(22, 22);
            this.btnPanUp.TabIndex = 18;
            this.btnPanUp.UseVisualStyleBackColor = true;
            this.btnPanUp.Click += new System.EventHandler(this.btnPanUp_Click);
            //
            // btnPanRight
            //
            this.btnPanRight.FlatAppearance.BorderSize = 0;
            this.btnPanRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanRight.ForeColor = System.Drawing.Color.Transparent;
            this.btnPanRight.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.East;
            this.btnPanRight.Location = new System.Drawing.Point(71, 48);
            this.btnPanRight.Name = "btnPanRight";
            this.btnPanRight.Size = new System.Drawing.Size(22, 22);
            this.btnPanRight.TabIndex = 17;
            this.btnPanRight.UseVisualStyleBackColor = true;
            this.btnPanRight.Click += new System.EventHandler(this.btnPanRight_Click);
            //
            // btnPanLeft
            //
            this.btnPanLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPanLeft.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPanLeft.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.West;
            this.btnPanLeft.Location = new System.Drawing.Point(13, 48);
            this.btnPanLeft.Name = "btnPanLeft";
            this.btnPanLeft.Size = new System.Drawing.Size(22, 22);
            this.btnPanLeft.TabIndex = 16;
            this.btnPanLeft.UseVisualStyleBackColor = true;
            this.btnPanLeft.Click += new System.EventHandler(this.btnPanLeft_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CurrentScale = 755957491.2;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            //
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(3, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(184, 60);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "You can click, hold and drag the mouse to pan as well.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // PanAroundTheMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "PanAroundTheMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Pan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private Button btnPan;
        private Label label2;
        private Label label1;
        private Button btnPanDown;
        private Button btnPanUp;
        private Button btnPanRight;
        private Button btnPanLeft;
        private ComboBox cmbDegree;
        private ComboBox cmbPercent;

        #endregion Component Designer generated code
    }
}