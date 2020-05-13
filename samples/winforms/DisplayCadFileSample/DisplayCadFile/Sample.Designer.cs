using System;
using System.IO;
using System.Management;
using DisplayCadFile.Properties;
using System.Windows.Forms;
using System.Drawing;

partial class Sample
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private readonly string mainFolder = ((new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)).Parent.Parent).FullName + "\\";

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sample));
        this.toolbarImageList = new System.Windows.Forms.ImageList(this.components);
        this.backgroundPanel = new System.Windows.Forms.Panel();
        this.label1 = new System.Windows.Forms.Label();
        this.sampleFileListBox = new System.Windows.Forms.ListBox();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.radioButtonEmbeddedStyle = new System.Windows.Forms.RadioButton();
        this.radioButtonStandardStyle = new System.Windows.Forms.RadioButton();
        this.ToolBar = new System.Windows.Forms.ToolBar();
        this.tbtnZoomIn = new System.Windows.Forms.ToolBarButton();
        this.tbtnZoomOut = new System.Windows.Forms.ToolBarButton();
        this.tbtnFullExtent = new System.Windows.Forms.ToolBarButton();
        this.toolBarSeparator = new System.Windows.Forms.ToolBarButton();
        this.tbtnPanLeft = new System.Windows.Forms.ToolBarButton();
        this.tbtnPanRight = new System.Windows.Forms.ToolBarButton();
        this.tbtnPanUp = new System.Windows.Forms.ToolBarButton();
        this.tbtnPanDown = new System.Windows.Forms.ToolBarButton();
        this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
        this.sampleFooter1 = new Footer();
        this.sampleBanner1 = new Banner();
        this.backgroundPanel.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.SuspendLayout();
        // 
        // toolbarImageList
        // 
        this.toolbarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolbarImageList.ImageStream")));
        this.toolbarImageList.TransparentColor = System.Drawing.Color.Transparent;
        this.toolbarImageList.Images.SetKeyName(0, "");
        this.toolbarImageList.Images.SetKeyName(1, "");
        this.toolbarImageList.Images.SetKeyName(2, "");
        this.toolbarImageList.Images.SetKeyName(3, "");
        this.toolbarImageList.Images.SetKeyName(4, "");
        this.toolbarImageList.Images.SetKeyName(5, "");
        this.toolbarImageList.Images.SetKeyName(6, "");
        // 
        // backgroundPanel
        // 
        this.backgroundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.backgroundPanel.BackColor = System.Drawing.Color.White;
        this.backgroundPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.backgroundPanel.Controls.Add(this.winformsMap1);
        this.backgroundPanel.Controls.Add(this.label1);
        this.backgroundPanel.Controls.Add(this.sampleFileListBox);
        this.backgroundPanel.Controls.Add(this.groupBox2);
        this.backgroundPanel.Controls.Add(this.ToolBar);
        this.backgroundPanel.Location = new System.Drawing.Point(12, 83);
        this.backgroundPanel.Name = "backgroundPanel";
        this.backgroundPanel.Size = new System.Drawing.Size(1082, 627);
        this.backgroundPanel.TabIndex = 47;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(3, 127);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(84, 16);
        this.label1.TabIndex = 54;
        this.label1.Text = "Sample Files";
        // 
        // sampleFileListBox
        // 
        this.sampleFileListBox.FormattingEnabled = true;
        this.sampleFileListBox.Location = new System.Drawing.Point(5, 153);
        this.sampleFileListBox.Name = "sampleFileListBox";
        this.sampleFileListBox.Size = new System.Drawing.Size(176, 420);
        this.sampleFileListBox.TabIndex = 48;
        this.sampleFileListBox.SelectedIndexChanged += new System.EventHandler(this.sampleFileListBox_SelectedIndexChanged);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.radioButtonEmbeddedStyle);
        this.groupBox2.Controls.Add(this.radioButtonStandardStyle);
        this.groupBox2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.groupBox2.Location = new System.Drawing.Point(5, 32);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(179, 79);
        this.groupBox2.TabIndex = 53;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Render Style";
        // 
        // radioButtonEmbeddedStyle
        // 
        this.radioButtonEmbeddedStyle.AutoSize = true;
        this.radioButtonEmbeddedStyle.Checked = true;
        this.radioButtonEmbeddedStyle.Location = new System.Drawing.Point(29, 26);
        this.radioButtonEmbeddedStyle.Name = "radioButtonEmbeddedStyle";
        this.radioButtonEmbeddedStyle.Size = new System.Drawing.Size(122, 20);
        this.radioButtonEmbeddedStyle.TabIndex = 49;
        this.radioButtonEmbeddedStyle.TabStop = true;
        this.radioButtonEmbeddedStyle.Text = "Embedded Style";
        this.radioButtonEmbeddedStyle.UseVisualStyleBackColor = true;
        // 
        // radioButtonStandardStyle
        // 
        this.radioButtonStandardStyle.AutoSize = true;
        this.radioButtonStandardStyle.Location = new System.Drawing.Point(29, 49);
        this.radioButtonStandardStyle.Name = "radioButtonStandardStyle";
        this.radioButtonStandardStyle.Size = new System.Drawing.Size(112, 20);
        this.radioButtonStandardStyle.TabIndex = 50;
        this.radioButtonStandardStyle.Text = "Standard Style";
        this.radioButtonStandardStyle.UseVisualStyleBackColor = true;
        this.radioButtonStandardStyle.CheckedChanged += new System.EventHandler(this.radioButtonStandardStyle_CheckedChanged);
        // 
        // ToolBar
        // 
        this.ToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbtnZoomIn,
            this.tbtnZoomOut,
            this.tbtnFullExtent,
            this.toolBarSeparator,
            this.tbtnPanLeft,
            this.tbtnPanRight,
            this.tbtnPanUp,
            this.tbtnPanDown});
        this.ToolBar.ButtonSize = new System.Drawing.Size(22, 14);
        this.ToolBar.Divider = false;
        this.ToolBar.Dock = System.Windows.Forms.DockStyle.None;
        this.ToolBar.DropDownArrows = true;
        this.ToolBar.ImageList = this.toolbarImageList;
        this.ToolBar.Location = new System.Drawing.Point(-1, 0);
        this.ToolBar.Name = "ToolBar";
        this.ToolBar.ShowToolTips = true;
        this.ToolBar.Size = new System.Drawing.Size(1081, 26);
        this.ToolBar.TabIndex = 47;
        this.ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.ToolBar_ButtonClick);
        // 
        // tbtnZoomIn
        // 
        this.tbtnZoomIn.ImageIndex = 0;
        this.tbtnZoomIn.Name = "tbtnZoomIn";
        this.tbtnZoomIn.Tag = "Zoom In";
        this.tbtnZoomIn.ToolTipText = "Zoom In";
        // 
        // tbtnZoomOut
        // 
        this.tbtnZoomOut.ImageIndex = 1;
        this.tbtnZoomOut.Name = "tbtnZoomOut";
        this.tbtnZoomOut.Tag = "Zoom Out";
        this.tbtnZoomOut.ToolTipText = "Zoom Out";
        // 
        // tbtnFullExtent
        // 
        this.tbtnFullExtent.ImageIndex = 2;
        this.tbtnFullExtent.Name = "tbtnFullExtent";
        this.tbtnFullExtent.Tag = "Full Extent";
        this.tbtnFullExtent.ToolTipText = "Full Extent";
        // 
        // toolBarSeparator
        // 
        this.toolBarSeparator.Name = "toolBarSeparator";
        this.toolBarSeparator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
        // 
        // tbtnPanLeft
        // 
        this.tbtnPanLeft.ImageIndex = 3;
        this.tbtnPanLeft.Name = "tbtnPanLeft";
        this.tbtnPanLeft.Tag = "Pan Left";
        this.tbtnPanLeft.ToolTipText = "Pan Left";
        // 
        // tbtnPanRight
        // 
        this.tbtnPanRight.ImageIndex = 4;
        this.tbtnPanRight.Name = "tbtnPanRight";
        this.tbtnPanRight.Tag = "Pan Right";
        this.tbtnPanRight.ToolTipText = "Pan Right";
        // 
        // tbtnPanUp
        // 
        this.tbtnPanUp.ImageIndex = 5;
        this.tbtnPanUp.Name = "tbtnPanUp";
        this.tbtnPanUp.Tag = "Pan Up";
        this.tbtnPanUp.ToolTipText = "Pan Up";
        // 
        // tbtnPanDown
        // 
        this.tbtnPanDown.ImageIndex = 6;
        this.tbtnPanDown.Name = "tbtnPanDown";
        this.tbtnPanDown.Tag = "Pan Down";
        this.tbtnPanDown.ToolTipText = "Pan Down";
        // 
        // sampleFooter1
        // 
        this.sampleFooter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.sampleFooter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
        this.sampleFooter1.Location = new System.Drawing.Point(-2, 716);
        this.sampleFooter1.Name = "sampleFooter1";
        this.sampleFooter1.Size = new System.Drawing.Size(1106, 26);
        this.sampleFooter1.TabIndex = 21;
        // 
        // sampleBanner1
        // 
        this.sampleBanner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.sampleBanner1.Location = new System.Drawing.Point(4, 0);
        this.sampleBanner1.Name = "sampleBanner1";
        this.sampleBanner1.Size = new System.Drawing.Size(1100, 81);
        this.sampleBanner1.TabIndex = 20;
        // 
        // winformsMap1
        // 
        this.winformsMap1.BackColor = System.Drawing.Color.White;
        this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
        this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
        this.winformsMap1.Location = new System.Drawing.Point(190, 3);
        this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
        this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
        this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
        this.winformsMap1.MaximumScale = 80000000000000D;
        this.winformsMap1.MinimumScale = 200D;
        this.winformsMap1.Name = "winformsMap1";
        this.winformsMap1.Size = new System.Drawing.Size(887, 619);
        this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        this.winformsMap1.TabIndex = 55;
        this.winformsMap1.Text = "winformsMap1";
        this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
        this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
        // 
        // Sample
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
        this.ClientSize = new System.Drawing.Size(1106, 750);
        this.Controls.Add(this.backgroundPanel);
        this.Controls.Add(this.sampleBanner1);
        this.Controls.Add(this.sampleFooter1);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Sample";
        this.Text = "Display CAD files - Map Suite Services Sample Application";
        this.Load += new System.EventHandler(this.DisplayASimpleMap_Load);
        this.backgroundPanel.ResumeLayout(false);
        this.backgroundPanel.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.ResumeLayout(false);

    }

    private void functionPanel_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.DrawRectangle(new Pen(Color.FromArgb(124, 171, 197)),
        e.ClipRectangle.Left,
        e.ClipRectangle.Top,
        e.ClipRectangle.Width - 1,
        e.ClipRectangle.Height - 1);
        base.OnPaint(e);
    }
    #endregion
    internal System.Windows.Forms.ImageList toolbarImageList;
    private Banner sampleBanner1;
    private Footer sampleFooter1;
    private System.Windows.Forms.Panel backgroundPanel;
    internal ToolBar ToolBar;
    internal ToolBarButton tbtnZoomIn;
    internal ToolBarButton tbtnZoomOut;
    internal ToolBarButton tbtnFullExtent;
    private ToolBarButton toolBarSeparator;
    internal ToolBarButton tbtnPanLeft;
    internal ToolBarButton tbtnPanRight;
    internal ToolBarButton tbtnPanUp;
    internal ToolBarButton tbtnPanDown;
    private RadioButton radioButtonStandardStyle;
    private RadioButton radioButtonEmbeddedStyle;
    private GroupBox groupBox2;
    private Label label1;
    private ListBox sampleFileListBox;
    private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
}