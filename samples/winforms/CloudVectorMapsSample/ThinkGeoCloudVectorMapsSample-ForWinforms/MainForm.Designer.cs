namespace ThinkGeoCloudVectorMapsOverlayOnlineSample_winform
{
    partial class MainForm
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
            this.winformsMap = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.radioButtonLight = new System.Windows.Forms.RadioButton();
            this.radioButtonDark = new System.Windows.Forms.RadioButton();
            this.radioButtonHybrid = new System.Windows.Forms.RadioButton();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // winformsMap
            // 
            this.winformsMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winformsMap.BackColor = System.Drawing.Color.White;
            this.winformsMap.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap.Location = new System.Drawing.Point(0, 0);
            this.winformsMap.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap.Margin = new System.Windows.Forms.Padding(0);
            this.winformsMap.MaximumScale = 80000000000000D;
            this.winformsMap.MinimumScale = 200D;
            this.winformsMap.Name = "winformsMap";
            this.winformsMap.Size = new System.Drawing.Size(1007, 728);
            this.winformsMap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap.TabIndex = 0;
            this.winformsMap.Text = "winformsMap1";
            this.winformsMap.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // radioButtonLight
            // 
            this.radioButtonLight.AutoSize = true;
            this.radioButtonLight.Checked = true;
            this.radioButtonLight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonLight.Location = new System.Drawing.Point(40, 38);
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.Size = new System.Drawing.Size(230, 25);
            this.radioButtonLight.TabIndex = 1;
            this.radioButtonLight.TabStop = true;
            this.radioButtonLight.Text = "ThinkGeo World Streets Light";
            this.radioButtonLight.UseVisualStyleBackColor = true;
            this.radioButtonLight.CheckedChanged += new System.EventHandler(this.radioButtonLight_CheckedChanged);
            // 
            // radioButtonDark
            // 
            this.radioButtonDark.AutoSize = true;
            this.radioButtonDark.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDark.Location = new System.Drawing.Point(40, 69);
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.Size = new System.Drawing.Size(228, 25);
            this.radioButtonDark.TabIndex = 2;
            this.radioButtonDark.Text = "ThinkGeo World Streets Dark";
            this.radioButtonDark.UseVisualStyleBackColor = true;
            this.radioButtonDark.CheckedChanged += new System.EventHandler(this.radioButtonDark_CheckedChanged);
            // 
            // radioButtonHybrid
            // 
            this.radioButtonHybrid.AutoSize = true;
            this.radioButtonHybrid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonHybrid.Location = new System.Drawing.Point(40, 100);
            this.radioButtonHybrid.Name = "radioButtonHybrid";
            this.radioButtonHybrid.Size = new System.Drawing.Size(242, 25);
            this.radioButtonHybrid.TabIndex = 3;
            this.radioButtonHybrid.Text = "ThinkGeo World Streets Hybrid";
            this.radioButtonHybrid.UseVisualStyleBackColor = true;
            this.radioButtonHybrid.CheckedChanged += new System.EventHandler(this.radioButtonHybrid_CheckedChanged);
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AutoSize = true;
            this.radioButtonCustom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCustom.Location = new System.Drawing.Point(40, 131);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(150, 25);
            this.radioButtonCustom.TabIndex = 4;
            this.radioButtonCustom.Text = "Custom StyleJson";
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.CheckedChanged += new System.EventHandler(this.radioButtonCustom_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radioButtonLight);
            this.panel1.Controls.Add(this.radioButtonDark);
            this.panel1.Controls.Add(this.radioButtonHybrid);
            this.panel1.Controls.Add(this.radioButtonCustom);
            this.panel1.Location = new System.Drawing.Point(673, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 209);
            this.panel1.TabIndex = 5;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(55, 184);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(244, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://github.com/ThinkGeo/WorldStreets-Styles";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "You can download all WorldStreets-Styles from";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Select map Type";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.winformsMap);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "ThinkGeoCloudVectorMapsOverlaySample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap;
        private System.Windows.Forms.RadioButton radioButtonLight;
        private System.Windows.Forms.RadioButton radioButtonDark;
        private System.Windows.Forms.RadioButton radioButtonHybrid;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
    }
}

