namespace DrawCustomException
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnDrawCustomException = new System.Windows.Forms.RadioButton();
            this.rbnDrawException = new System.Windows.Forms.RadioButton();
            this.rbnThrowException = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbnOffline = new System.Windows.Forms.RadioButton();
            this.rbnOnline = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelScreen = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWorld = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(666, 337);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 45;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseMove);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbnDrawCustomException);
            this.groupBox1.Controls.Add(this.rbnDrawException);
            this.groupBox1.Controls.Add(this.rbnThrowException);
            this.groupBox1.Location = new System.Drawing.Point(318, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 100);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // rbnDrawCustomException
            // 
            this.rbnDrawCustomException.AutoSize = true;
            this.rbnDrawCustomException.Location = new System.Drawing.Point(7, 43);
            this.rbnDrawCustomException.Name = "rbnDrawCustomException";
            this.rbnDrawCustomException.Size = new System.Drawing.Size(138, 17);
            this.rbnDrawCustomException.TabIndex = 0;
            this.rbnDrawCustomException.Text = "Draw Custom Exception";
            this.rbnDrawCustomException.UseVisualStyleBackColor = true;
            this.rbnDrawCustomException.CheckedChanged += new System.EventHandler(this.rbnException_CheckedChanged);
            // 
            // rbnDrawException
            // 
            this.rbnDrawException.AutoSize = true;
            this.rbnDrawException.Checked = true;
            this.rbnDrawException.Location = new System.Drawing.Point(7, 19);
            this.rbnDrawException.Name = "rbnDrawException";
            this.rbnDrawException.Size = new System.Drawing.Size(137, 17);
            this.rbnDrawException.TabIndex = 0;
            this.rbnDrawException.TabStop = true;
            this.rbnDrawException.Text = "Draw Default Exception";
            this.rbnDrawException.UseVisualStyleBackColor = true;
            this.rbnDrawException.CheckedChanged += new System.EventHandler(this.rbnException_CheckedChanged);
            // 
            // rbnThrowException
            // 
            this.rbnThrowException.AutoSize = true;
            this.rbnThrowException.Location = new System.Drawing.Point(6, 66);
            this.rbnThrowException.Name = "rbnThrowException";
            this.rbnThrowException.Size = new System.Drawing.Size(105, 17);
            this.rbnThrowException.TabIndex = 0;
            this.rbnThrowException.Text = "Throw Exception";
            this.rbnThrowException.UseVisualStyleBackColor = true;
            this.rbnThrowException.CheckedChanged += new System.EventHandler(this.rbnException_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbnOffline);
            this.groupBox2.Controls.Add(this.rbnOnline);
            this.groupBox2.Location = new System.Drawing.Point(212, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 100);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            // 
            // rbnOffline
            // 
            this.rbnOffline.AutoSize = true;
            this.rbnOffline.Location = new System.Drawing.Point(21, 43);
            this.rbnOffline.Name = "rbnOffline";
            this.rbnOffline.Size = new System.Drawing.Size(55, 17);
            this.rbnOffline.TabIndex = 0;
            this.rbnOffline.Text = "Offline";
            this.rbnOffline.UseVisualStyleBackColor = true;
            this.rbnOffline.CheckedChanged += new System.EventHandler(this.rbnOnline_CheckedChanged);
            // 
            // rbnOnline
            // 
            this.rbnOnline.AutoSize = true;
            this.rbnOnline.Checked = true;
            this.rbnOnline.Location = new System.Drawing.Point(21, 19);
            this.rbnOnline.Name = "rbnOnline";
            this.rbnOnline.Size = new System.Drawing.Size(55, 17);
            this.rbnOnline.TabIndex = 0;
            this.rbnOnline.TabStop = true;
            this.rbnOnline.Text = "Online";
            this.rbnOnline.UseVisualStyleBackColor = true;
            this.rbnOnline.CheckedChanged += new System.EventHandler(this.rbnOnline_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 359);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 100);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "Note: For this sample to work properly you need to run it without the debugger at" +
                "tached.  You can use Ctrl-F5 or in the debug menu choose Start Without Debugging" +
                ".";
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelScreen,
            this.toolStripStatusLabelWorld});
            this.statusStrip1.Location = new System.Drawing.Point(0, 467);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 22);
            this.statusStrip1.TabIndex = 48;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelScreen
            // 
            this.toolStripStatusLabelScreen.AutoSize = false;
            this.toolStripStatusLabelScreen.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabelScreen.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this.toolStripStatusLabelScreen.Name = "toolStripStatusLabelScreen";
            this.toolStripStatusLabelScreen.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabelScreen.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelWorld
            // 
            this.toolStripStatusLabelWorld.AutoSize = false;
            this.toolStripStatusLabelWorld.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabelWorld.Name = "toolStripStatusLabelWorld";
            this.toolStripStatusLabelWorld.Size = new System.Drawing.Size(188, 17);
            this.toolStripStatusLabelWorld.Text = "toolStripStatusLabel1";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 489);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Community Project-Draw Custom Exception";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.ImageList ImageListToolbar;
        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnDrawCustomException;
        private System.Windows.Forms.RadioButton rbnDrawException;
        private System.Windows.Forms.RadioButton rbnThrowException;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbnOffline;
        private System.Windows.Forms.RadioButton rbnOnline;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScreen;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWorld;
    }
}

