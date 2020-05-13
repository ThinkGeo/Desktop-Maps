namespace ThinkGeoCloudMapsSample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoHybrid = new System.Windows.Forms.RadioButton();
            this.rdoAerial = new System.Windows.Forms.RadioButton();
            this.rdoDark = new System.Windows.Forms.RadioButton();
            this.rdoLight = new System.Windows.Forms.RadioButton();
            this.rdoTransparentBackground = new System.Windows.Forms.RadioButton();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.rdoTransparentBackground);
            this.panel1.Controls.Add(this.rdoHybrid);
            this.panel1.Controls.Add(this.rdoAerial);
            this.panel1.Controls.Add(this.rdoDark);
            this.panel1.Controls.Add(this.rdoLight);
            this.panel1.Location = new System.Drawing.Point(940, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 134);
            this.panel1.TabIndex = 1;
            // 
            // rdoHybrid
            // 
            this.rdoHybrid.AutoSize = true;
            this.rdoHybrid.Location = new System.Drawing.Point(17, 82);
            this.rdoHybrid.Name = "rdoHybrid";
            this.rdoHybrid.Size = new System.Drawing.Size(55, 17);
            this.rdoHybrid.TabIndex = 3;
            this.rdoHybrid.Text = "Hybrid";
            this.rdoHybrid.UseVisualStyleBackColor = true;
            this.rdoHybrid.CheckedChanged += new System.EventHandler(this.MapTypeChanged);
            // 
            // rdoAerial
            // 
            this.rdoAerial.AutoSize = true;
            this.rdoAerial.Location = new System.Drawing.Point(17, 59);
            this.rdoAerial.Name = "rdoAerial";
            this.rdoAerial.Size = new System.Drawing.Size(51, 17);
            this.rdoAerial.TabIndex = 2;
            this.rdoAerial.Text = "Aerial";
            this.rdoAerial.UseVisualStyleBackColor = true;
            this.rdoAerial.CheckedChanged += new System.EventHandler(this.MapTypeChanged);
            // 
            // rdoDark
            // 
            this.rdoDark.AutoSize = true;
            this.rdoDark.Location = new System.Drawing.Point(17, 36);
            this.rdoDark.Name = "rdoDark";
            this.rdoDark.Size = new System.Drawing.Size(48, 17);
            this.rdoDark.TabIndex = 1;
            this.rdoDark.Text = "Dark";
            this.rdoDark.UseVisualStyleBackColor = true;
            this.rdoDark.CheckedChanged += new System.EventHandler(this.MapTypeChanged);
            // 
            // rdoLight
            // 
            this.rdoLight.AutoSize = true;
            this.rdoLight.Checked = true;
            this.rdoLight.Location = new System.Drawing.Point(17, 13);
            this.rdoLight.Name = "rdoLight";
            this.rdoLight.Size = new System.Drawing.Size(48, 17);
            this.rdoLight.TabIndex = 0;
            this.rdoLight.TabStop = true;
            this.rdoLight.Text = "Light";
            this.rdoLight.UseVisualStyleBackColor = true;
            this.rdoLight.CheckedChanged += new System.EventHandler(this.MapTypeChanged);
            // 
            // rdoTransparentBackground
            // 
            this.rdoTransparentBackground.AutoSize = true;
            this.rdoTransparentBackground.Location = new System.Drawing.Point(17, 105);
            this.rdoTransparentBackground.Name = "rdoTransparentBackground";
            this.rdoTransparentBackground.Size = new System.Drawing.Size(140, 17);
            this.rdoTransparentBackground.TabIndex = 4;
            this.rdoTransparentBackground.Text = "TransparentBackground";
            this.rdoTransparentBackground.UseVisualStyleBackColor = true;
            this.rdoTransparentBackground.CheckedChanged += new System.EventHandler(this.MapTypeChanged);
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(1119, 744);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 744);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoHybrid;
        private System.Windows.Forms.RadioButton rdoAerial;
        private System.Windows.Forms.RadioButton rdoDark;
        private System.Windows.Forms.RadioButton rdoLight;
        private System.Windows.Forms.RadioButton rdoTransparentBackground;
    }
}

