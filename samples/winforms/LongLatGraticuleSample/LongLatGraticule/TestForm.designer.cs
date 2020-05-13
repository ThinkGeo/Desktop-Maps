namespace LatLongGraticule
{
    partial class TestForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.btnClose = new System.Windows.Forms.Button();
            this.ImageListToolbar = new System.Windows.Forms.ImageList(this.components);
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBoxDMS = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLatitudeDMS = new System.Windows.Forms.Label();
            this.lblLongitudeDMS = new System.Windows.Forms.Label();
            this.groupBoxDecimalDegrees = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLatitude = new System.Windows.Forms.Label();
            this.lblLongitude = new System.Windows.Forms.Label();
            this.groupBoxDMS.SuspendLayout();
            this.groupBoxDecimalDegrees.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(603, 436);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ImageListToolbar
            // 
            this.ImageListToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListToolbar.ImageStream")));
            this.ImageListToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListToolbar.Images.SetKeyName(0, "");
            this.ImageListToolbar.Images.SetKeyName(1, "");
            this.ImageListToolbar.Images.SetKeyName(2, "");
            this.ImageListToolbar.Images.SetKeyName(3, "");
            this.ImageListToolbar.Images.SetKeyName(4, "");
            this.ImageListToolbar.Images.SetKeyName(5, "");
            this.ImageListToolbar.Images.SetKeyName(6, "");
            this.ImageListToolbar.Images.SetKeyName(7, "");
            this.ImageListToolbar.Images.SetKeyName(8, "");
            this.ImageListToolbar.Images.SetKeyName(9, "");
            this.ImageListToolbar.Images.SetKeyName(10, "");
            this.ImageListToolbar.Images.SetKeyName(11, "");
            this.ImageListToolbar.Images.SetKeyName(12, "");
            this.ImageListToolbar.Images.SetKeyName(13, "");
            this.ImageListToolbar.Images.SetKeyName(14, "");
            this.ImageListToolbar.Images.SetKeyName(15, "");
            this.ImageListToolbar.Images.SetKeyName(16, "");
            this.ImageListToolbar.Images.SetKeyName(17, "");
            this.ImageListToolbar.Images.SetKeyName(18, "");
            this.ImageListToolbar.Images.SetKeyName(19, "");
            this.ImageListToolbar.Images.SetKeyName(20, "");
            this.ImageListToolbar.Images.SetKeyName(21, "");
            this.ImageListToolbar.Images.SetKeyName(22, "");
            this.ImageListToolbar.Images.SetKeyName(23, "");
            this.ImageListToolbar.Images.SetKeyName(24, "");
            this.ImageListToolbar.Images.SetKeyName(25, "");
            this.ImageListToolbar.Images.SetKeyName(26, "");
            this.ImageListToolbar.Images.SetKeyName(27, "");
            this.ImageListToolbar.Images.SetKeyName(28, "");
            this.ImageListToolbar.Images.SetKeyName(29, "");
            this.ImageListToolbar.Images.SetKeyName(30, "");
            this.ImageListToolbar.Images.SetKeyName(31, "tool_icon_color_picker.png");
            this.ImageListToolbar.Images.SetKeyName(32, "btn_layer_up.png");
            this.ImageListToolbar.Images.SetKeyName(33, "btn_layer_down.png");
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(12, 12);
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(666, 341);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 2;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseMove);
            // 
            // groupBoxDMS
            // 
            this.groupBoxDMS.Controls.Add(this.label6);
            this.groupBoxDMS.Controls.Add(this.label5);
            this.groupBoxDMS.Controls.Add(this.lblLatitudeDMS);
            this.groupBoxDMS.Controls.Add(this.lblLongitudeDMS);
            this.groupBoxDMS.Location = new System.Drawing.Point(292, 367);
            this.groupBoxDMS.Name = "groupBoxDMS";
            this.groupBoxDMS.Size = new System.Drawing.Size(255, 92);
            this.groupBoxDMS.TabIndex = 4;
            this.groupBoxDMS.TabStop = false;
            this.groupBoxDMS.Text = "Degrees Minutes Seconds";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 14);
            this.label6.TabIndex = 4;
            this.label6.Text = "Latitude:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(20, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "Longitude:";
            // 
            // lblLatitudeDMS
            // 
            this.lblLatitudeDMS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLatitudeDMS.Location = new System.Drawing.Point(92, 60);
            this.lblLatitudeDMS.Name = "lblLatitudeDMS";
            this.lblLatitudeDMS.Size = new System.Drawing.Size(140, 16);
            this.lblLatitudeDMS.TabIndex = 2;
            // 
            // lblLongitudeDMS
            // 
            this.lblLongitudeDMS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLongitudeDMS.Location = new System.Drawing.Point(92, 27);
            this.lblLongitudeDMS.Name = "lblLongitudeDMS";
            this.lblLongitudeDMS.Size = new System.Drawing.Size(140, 16);
            this.lblLongitudeDMS.TabIndex = 1;
            // 
            // groupBoxDecimalDegrees
            // 
            this.groupBoxDecimalDegrees.Controls.Add(this.label2);
            this.groupBoxDecimalDegrees.Controls.Add(this.label1);
            this.groupBoxDecimalDegrees.Controls.Add(this.lblLatitude);
            this.groupBoxDecimalDegrees.Controls.Add(this.lblLongitude);
            this.groupBoxDecimalDegrees.Location = new System.Drawing.Point(12, 367);
            this.groupBoxDecimalDegrees.Name = "groupBoxDecimalDegrees";
            this.groupBoxDecimalDegrees.Size = new System.Drawing.Size(255, 92);
            this.groupBoxDecimalDegrees.TabIndex = 3;
            this.groupBoxDecimalDegrees.TabStop = false;
            this.groupBoxDecimalDegrees.Text = "Decimal Degrees";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Latitude:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Longitude:";
            // 
            // lblLatitude
            // 
            this.lblLatitude.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLatitude.Location = new System.Drawing.Point(94, 60);
            this.lblLatitude.Name = "lblLatitude";
            this.lblLatitude.Size = new System.Drawing.Size(140, 16);
            this.lblLatitude.TabIndex = 1;
            // 
            // lblLongitude
            // 
            this.lblLongitude.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLongitude.Location = new System.Drawing.Point(94, 26);
            this.lblLongitude.Name = "lblLongitude";
            this.lblLongitude.Size = new System.Drawing.Size(140, 16);
            this.lblLongitude.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 471);
            this.Controls.Add(this.groupBoxDMS);
            this.Controls.Add(this.groupBoxDecimalDegrees);
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LatLongGraticule";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.groupBoxDMS.ResumeLayout(false);
            this.groupBoxDecimalDegrees.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.ImageList ImageListToolbar;
        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.GroupBox groupBoxDMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLatitudeDMS;
        private System.Windows.Forms.Label lblLongitudeDMS;
        private System.Windows.Forms.GroupBox groupBoxDecimalDegrees;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLatitude;
        private System.Windows.Forms.Label lblLongitude;
    }
}

