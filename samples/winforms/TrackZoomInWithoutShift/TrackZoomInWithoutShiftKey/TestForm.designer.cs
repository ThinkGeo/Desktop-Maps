namespace TrackZoomInWithoutShiftKey
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelScreen = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWorld = new System.Windows.Forms.ToolStripStatusLabel();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolBarButtonZIT = new System.Windows.Forms.ToolBarButton();
            this.spc = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonPan = new System.Windows.Forms.ToolBarButton();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(603, 434);
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
            this.winformsMap1.Location = new System.Drawing.Point(12, 34);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(666, 394);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 2;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.winformsMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelScreen,
            this.toolStripStatusLabelWorld});
            this.statusStrip1.Location = new System.Drawing.Point(0, 468);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 22);
            this.statusStrip1.TabIndex = 46;
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
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            this.ImageList1.Images.SetKeyName(3, "");
            this.ImageList1.Images.SetKeyName(4, "");
            this.ImageList1.Images.SetKeyName(5, "");
            this.ImageList1.Images.SetKeyName(6, "");
            this.ImageList1.Images.SetKeyName(7, "");
            this.ImageList1.Images.SetKeyName(8, "");
            this.ImageList1.Images.SetKeyName(9, "");
            this.ImageList1.Images.SetKeyName(10, "");
            this.ImageList1.Images.SetKeyName(11, "");
            this.ImageList1.Images.SetKeyName(12, "");
            this.ImageList1.Images.SetKeyName(13, "");
            this.ImageList1.Images.SetKeyName(14, "");
            this.ImageList1.Images.SetKeyName(15, "");
            this.ImageList1.Images.SetKeyName(16, "");
            this.ImageList1.Images.SetKeyName(17, "");
            this.ImageList1.Images.SetKeyName(18, "");
            this.ImageList1.Images.SetKeyName(19, "");
            // 
            // ToolBar1
            // 
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonZIT,
            this.spc,
            this.ToolBarButtonPan});
            this.ToolBar1.ButtonSize = new System.Drawing.Size(16, 16);
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.ImageList = this.ImageList1;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(690, 28);
            this.ToolBar1.TabIndex = 85;
            this.ToolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar1_ButtonClick);
            // 
            // ToolBarButtonZIT
            // 
            this.ToolBarButtonZIT.ImageIndex = 8;
            this.ToolBarButtonZIT.Name = "ToolBarButtonZIT";
            this.ToolBarButtonZIT.Pushed = true;
            this.ToolBarButtonZIT.ToolTipText = "Track Zoom In";
            // 
            // spc
            // 
            this.spc.Name = "spc";
            this.spc.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // ToolBarButtonPan
            // 
            this.ToolBarButtonPan.ImageIndex = 2;
            this.ToolBarButtonPan.Name = "ToolBarButtonPan";
            this.ToolBarButtonPan.ToolTipText = "Pan";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 490);
            this.Controls.Add(this.ToolBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Track Zoom In Without Shift Key";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.ImageList ImageListToolbar;
        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScreen;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWorld;
        internal System.Windows.Forms.ImageList ImageList1;
        internal System.Windows.Forms.ToolBar ToolBar1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonZIT;
        private System.Windows.Forms.ToolBarButton spc;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonPan;
    }
}

