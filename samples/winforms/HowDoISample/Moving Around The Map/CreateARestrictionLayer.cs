using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateARestrictionLayer : UserControl
    {
        public CreateARestrictionLayer()
        {
            InitializeComponent();
        }

        private void CreateARestrictionLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-44.056640625, 49.224609375, 86.021484375, -43.587890625);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            RestrictionLayer restrictionLayer = new RestrictionLayer();
            restrictionLayer.Zones.Add(new RectangleShape(-17.67, 37.01, 60.02, -34.68));
            restrictionLayer.RestrictionMode = RestrictionMode.ShowZones;
            restrictionLayer.UpperScale = 250000000;
            restrictionLayer.LowerScale = double.MinValue;

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);

            winformsMap1.Overlays.Add("WorldOverlay", thinkGeoCloudRasterMapsOverlay);

            LayerOverlay restrictionOverlay = new LayerOverlay();
            restrictionOverlay.Layers.Add("RestrictionLayer", restrictionLayer);
            winformsMap1.Overlays.Add("RestrictionOverlay", restrictionOverlay);

            winformsMap1.Refresh();
        }

        private void cmbRestrictionStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            LayerOverlay dynamicOverlay = (LayerOverlay)winformsMap1.Overlays[1];
            RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
            if (currentRestrictionLayer.CustomStyles.Count > 0)
            {
                currentRestrictionLayer.CustomStyles.Clear();
            }

            switch (cmbRestrictionStyle.SelectedItem.ToString())
            {
                case "HatchPattern":
                    currentRestrictionLayer.RestrictionStyle = RestrictionStyle.HatchPattern;
                    break;

                case "CircleWithSlashImage":
                    currentRestrictionLayer.RestrictionStyle = RestrictionStyle.CircleWithSlashImage;
                    break;

                case "UseCustomStyles":
                    currentRestrictionLayer.RestrictionStyle = RestrictionStyle.UseCustomStyles;
                    Style customStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(150, GeoColors.Gray)));
                    currentRestrictionLayer.CustomStyles.Add(customStyle);
                    break;

                default:
                    break;
            }

            winformsMap1.Refresh(winformsMap1.Overlays["RestrictionOverlay"]);
        }

        private void rbtnShowMode_Click(object sender, EventArgs e)
        {
            LayerOverlay dynamicOverlay = (LayerOverlay)winformsMap1.Overlays[1];
            RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
            currentRestrictionLayer.RestrictionMode = RestrictionMode.ShowZones;
            tbxModeInstruction.Text = "You can see only Africa because we have added a RestrictionLayer and its mode is ShowZones.";

            winformsMap1.Refresh(winformsMap1.Overlays["RestrictionOverlay"]);
        }

        private void rbtnHideMode_Click(object sender, EventArgs e)
        {
            LayerOverlay dynamicOverlay = (LayerOverlay)winformsMap1.Overlays[1];
            RestrictionLayer currentRestrictionLayer = (RestrictionLayer)dynamicOverlay.Layers["RestrictionLayer"];
            currentRestrictionLayer.RestrictionMode = RestrictionMode.HideZones;
            tbxModeInstruction.Text = "You can not see Africa because we have added a RestrictionLayer and its mode is HideZones.";

            winformsMap1.Refresh(winformsMap1.Overlays["RestrictionOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private ComboBox cmbRestrictionStyle;
        private RadioButton rbtnHideMode;
        private RadioButton rbtnShowMode;
        private Label lblRestrictionStyle;
        private TextBox tbxModeInstruction;
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
            this.tbxModeInstruction = new System.Windows.Forms.TextBox();
            this.lblRestrictionStyle = new System.Windows.Forms.Label();
            this.rbtnHideMode = new System.Windows.Forms.RadioButton();
            this.rbtnShowMode = new System.Windows.Forms.RadioButton();
            this.cmbRestrictionStyle = new System.Windows.Forms.ComboBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.tbxModeInstruction);
            this.groupBox1.Controls.Add(this.lblRestrictionStyle);
            this.groupBox1.Controls.Add(this.rbtnHideMode);
            this.groupBox1.Controls.Add(this.rbtnShowMode);
            this.groupBox1.Controls.Add(this.cmbRestrictionStyle);
            this.groupBox1.Location = new System.Drawing.Point(556, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 146);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // tbxModeInstruction
            //
            this.tbxModeInstruction.BackColor = System.Drawing.SystemColors.Control;
            this.tbxModeInstruction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbxModeInstruction.Location = new System.Drawing.Point(6, 100);
            this.tbxModeInstruction.Multiline = true;
            this.tbxModeInstruction.Name = "tbxModeInstruction";
            this.tbxModeInstruction.ReadOnly = true;
            this.tbxModeInstruction.Size = new System.Drawing.Size(169, 40);
            this.tbxModeInstruction.TabIndex = 8;
            this.tbxModeInstruction.Text = "You can see only Africa because we have added a RestrictionLayer and its mode is " +
                "ShowZones.";
            //
            // lblRestrictionStyle
            //
            this.lblRestrictionStyle.AutoSize = true;
            this.lblRestrictionStyle.Location = new System.Drawing.Point(6, 22);
            this.lblRestrictionStyle.Name = "lblRestrictionStyle";
            this.lblRestrictionStyle.Size = new System.Drawing.Size(125, 13);
            this.lblRestrictionStyle.TabIndex = 7;
            this.lblRestrictionStyle.Text = "Select a RestrictionStyle:";
            //
            // rbtnHideMode
            //
            this.rbtnHideMode.AutoSize = true;
            this.rbtnHideMode.Location = new System.Drawing.Point(97, 77);
            this.rbtnHideMode.Name = "rbtnHideMode";
            this.rbtnHideMode.Size = new System.Drawing.Size(80, 17);
            this.rbtnHideMode.TabIndex = 3;
            this.rbtnHideMode.TabStop = true;
            this.rbtnHideMode.Text = "Hide Zones";
            this.rbtnHideMode.UseVisualStyleBackColor = true;
            this.rbtnHideMode.Click += new System.EventHandler(this.rbtnHideMode_Click);
            //
            // rbtnShowMode
            //
            this.rbtnShowMode.AutoSize = true;
            this.rbtnShowMode.Checked = true;
            this.rbtnShowMode.Location = new System.Drawing.Point(6, 77);
            this.rbtnShowMode.Name = "rbtnShowMode";
            this.rbtnShowMode.Size = new System.Drawing.Size(85, 17);
            this.rbtnShowMode.TabIndex = 4;
            this.rbtnShowMode.TabStop = true;
            this.rbtnShowMode.Text = "Show Zones";
            this.rbtnShowMode.UseVisualStyleBackColor = true;
            this.rbtnShowMode.Click += new System.EventHandler(this.rbtnShowMode_Click);
            //
            // cmbRestrictionStyle
            //
            this.cmbRestrictionStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRestrictionStyle.FormattingEnabled = true;
            this.cmbRestrictionStyle.Items.AddRange(new object[] {
            "HatchPattern",
            "UseCustomStyles",
            "CircleWithSlashImage"});
            this.cmbRestrictionStyle.Location = new System.Drawing.Point(6, 41);
            this.cmbRestrictionStyle.Text = "HatchPattern";
            this.cmbRestrictionStyle.Name = "cmbRestrictionStyle";
            this.cmbRestrictionStyle.Size = new System.Drawing.Size(169, 21);
            this.cmbRestrictionStyle.TabIndex = 0;
            this.cmbRestrictionStyle.SelectedIndexChanged += new System.EventHandler(this.cmbRestrictionStyle_SelectedIndexChanged);
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
            // CreateARestrictionLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "CreateARestrictionLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.CreateARestrictionLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}