namespace PresetVertexToTrackedPolygon
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
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(603, 456);
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
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(12, 43);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(666, 407);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 492);
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
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(302, 10);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(66, 20);
            this.txtX.TabIndex = 47;
            this.txtX.Text = "-10776361";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(405, 10);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(66, 20);
            this.txtY.TabIndex = 48;
            this.txtY.Text = "3909807";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(279, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Y:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(169, 26);
            this.button1.TabIndex = 51;
            this.button1.Text = "Add Vertex to Tracked Polygon";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 514);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Community Project-Preset Vertex to Tracked Polygon";
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
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

