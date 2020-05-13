using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.USDemographicMap
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.stpFooter = new System.Windows.Forms.StatusStrip();
            this.lblLocationY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLocationX = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBigTitle = new System.Windows.Forms.Label();
            this.lblSmallTitle = new System.Windows.Forms.Label();
            this.map = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.LeftSideBarPanel = new ThinkGeo.MapSuite.USDemographicMap.CollapsiblePanel();
            this.pnlStyleControls = new System.Windows.Forms.Panel();
            this.lblColorWheelDirection = new System.Windows.Forms.Label();
            this.cbxSelectColorWheelDirection = new System.Windows.Forms.ComboBox();
            this.cbxActiveEndColor = new ThinkGeo.MapSuite.USDemographicMap.ColorComboBox();
            this.lblDisplayEndColor = new System.Windows.Forms.Label();
            this.cbxActiveColor = new ThinkGeo.MapSuite.USDemographicMap.ColorComboBox();
            this.lblDisplayColor = new System.Windows.Forms.Label();
            this.trackBarDots = new System.Windows.Forms.TrackBar();
            this.lblDotDensityUnit = new System.Windows.Forms.Label();
            this.trackBarValueCirleRadio = new System.Windows.Forms.TrackBar();
            this.lblMore = new System.Windows.Forms.Label();
            this.lblValueCircle = new System.Windows.Forms.Label();
            this.lblFewer = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.stpFooter.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.LeftSideBarPanel.SuspendLayout();
            this.pnlStyleControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValueCirleRadio)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // stpFooter
            // 
            this.stpFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLocationY,
            this.lblLocationX});
            this.stpFooter.Location = new System.Drawing.Point(0, 741);
            this.stpFooter.Name = "stpFooter";
            this.stpFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stpFooter.Size = new System.Drawing.Size(1084, 22);
            this.stpFooter.TabIndex = 0;
            this.stpFooter.Text = "statusStrip1";
            // 
            // lblLocationY
            // 
            this.lblLocationY.AutoSize = false;
            this.lblLocationY.BackColor = System.Drawing.Color.Transparent;
            this.lblLocationY.Name = "lblLocationY";
            this.lblLocationY.Size = new System.Drawing.Size(80, 17);
            this.lblLocationY.Text = "Y:000000";
            this.lblLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLocationX
            // 
            this.lblLocationX.AutoSize = false;
            this.lblLocationX.BackColor = System.Drawing.Color.Transparent;
            this.lblLocationX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLocationX.Name = "lblLocationX";
            this.lblLocationX.Size = new System.Drawing.Size(80, 17);
            this.lblLocationX.Text = "X:000000";
            this.lblLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlHeader.BackgroundImage")));
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.lblBigTitle);
            this.pnlHeader.Controls.Add(this.lblSmallTitle);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1084, 50);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblBigTitle
            // 
            this.lblBigTitle.AutoSize = true;
            this.lblBigTitle.Font = new System.Drawing.Font("Segoe UI", 14.35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBigTitle.Location = new System.Drawing.Point(92, 9);
            this.lblBigTitle.Name = "lblBigTitle";
            this.lblBigTitle.Size = new System.Drawing.Size(208, 28);
            this.lblBigTitle.TabIndex = 1;
            this.lblBigTitle.Text = "US DemoGraphic Map";
            // 
            // lblSmallTitle
            // 
            this.lblSmallTitle.AutoSize = true;
            this.lblSmallTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallTitle.Location = new System.Drawing.Point(18, 15);
            this.lblSmallTitle.Name = "lblSmallTitle";
            this.lblSmallTitle.Size = new System.Drawing.Size(80, 21);
            this.lblSmallTitle.TabIndex = 0;
            this.lblSmallTitle.Text = "Map Suite";
            // 
            // map
            // 
            this.map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map.BackColor = System.Drawing.Color.Gray;
            this.map.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.map.CurrentScale = 590591790D;
            this.map.DrawingQuality = DrawingQuality.Default;
            this.map.Location = new System.Drawing.Point(320, 50);
            this.map.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.map.MapResizeMode = MapResizeMode.PreserveScale;
            this.map.MapUnit = GeographyUnit.DecimalDegree;
            this.map.Margin = new System.Windows.Forms.Padding(0);
            this.map.MaximumScale = 80000000000000D;
            this.map.MinimumScale = 200D;
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(764, 691);
            this.map.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.map.TabIndex = 3;
            this.map.Text = "map";
            this.map.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.map.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.map.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
            // 
            // LeftSideBarPanel
            // 
            this.LeftSideBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LeftSideBarPanel.Controls.Add(this.pnlStyleControls);
            this.LeftSideBarPanel.Controls.Add(this.pnlControl);
            this.LeftSideBarPanel.Controls.Add(this.label1);
            this.LeftSideBarPanel.LineWidth = 5;
            this.LeftSideBarPanel.Location = new System.Drawing.Point(0, 50);
            this.LeftSideBarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LeftSideBarPanel.Name = "LeftSideBarPanel";
            this.LeftSideBarPanel.PanelWidth = 300;
            this.LeftSideBarPanel.Size = new System.Drawing.Size(320, 691);
            this.LeftSideBarPanel.TabIndex = 2;
            this.LeftSideBarPanel.PanelCollapseButtonClick += new System.EventHandler<System.EventArgs>(this.LeftSideBarPanel_PanelCollapseButtonClick);
            // 
            // pnlStyleControls
            // 
            this.pnlStyleControls.Controls.Add(this.lblColorWheelDirection);
            this.pnlStyleControls.Controls.Add(this.cbxSelectColorWheelDirection);
            this.pnlStyleControls.Controls.Add(this.cbxActiveEndColor);
            this.pnlStyleControls.Controls.Add(this.lblDisplayEndColor);
            this.pnlStyleControls.Controls.Add(this.cbxActiveColor);
            this.pnlStyleControls.Controls.Add(this.lblDisplayColor);
            this.pnlStyleControls.Controls.Add(this.trackBarDots);
            this.pnlStyleControls.Controls.Add(this.lblDotDensityUnit);
            this.pnlStyleControls.Controls.Add(this.trackBarValueCirleRadio);
            this.pnlStyleControls.Controls.Add(this.lblMore);
            this.pnlStyleControls.Controls.Add(this.lblValueCircle);
            this.pnlStyleControls.Controls.Add(this.lblFewer);
            this.pnlStyleControls.Location = new System.Drawing.Point(3, 501);
            this.pnlStyleControls.Name = "pnlStyleControls";
            this.pnlStyleControls.Size = new System.Drawing.Size(296, 129);
            this.pnlStyleControls.TabIndex = 0;
            // 
            // lblColorWheelDirection
            // 
            this.lblColorWheelDirection.AutoSize = true;
            this.lblColorWheelDirection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorWheelDirection.Location = new System.Drawing.Point(6, 81);
            this.lblColorWheelDirection.Name = "lblColorWheelDirection";
            this.lblColorWheelDirection.Size = new System.Drawing.Size(120, 15);
            this.lblColorWheelDirection.TabIndex = 28;
            this.lblColorWheelDirection.Text = "ColorWheelDirection:";
            // 
            // cbxSelectColorWheelDirection
            // 
            this.cbxSelectColorWheelDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectColorWheelDirection.FormattingEnabled = true;
            this.cbxSelectColorWheelDirection.Items.AddRange(new object[] {
            "Clockwise",
            "CounterClockwise"});
            this.cbxSelectColorWheelDirection.Location = new System.Drawing.Point(145, 78);
            this.cbxSelectColorWheelDirection.Name = "cbxSelectColorWheelDirection";
            this.cbxSelectColorWheelDirection.Size = new System.Drawing.Size(148, 21);
            this.cbxSelectColorWheelDirection.TabIndex = 27;
            this.cbxSelectColorWheelDirection.SelectedIndexChanged += new System.EventHandler(this.SelectColorWheelDirectionComboBox_SelectedIndexChanged);
            // 
            // cbxActiveEndColor
            // 
            this.cbxActiveEndColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxActiveEndColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActiveEndColor.FormattingEnabled = true;
            this.cbxActiveEndColor.ItemHeight = 25;
            this.cbxActiveEndColor.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White"});
            this.cbxActiveEndColor.Location = new System.Drawing.Point(145, 40);
            this.cbxActiveEndColor.Name = "cbxActiveEndColor";
            this.cbxActiveEndColor.SimpleColors = ((System.Collections.Generic.Dictionary<string, System.Drawing.Color>)(resources.GetObject("cbxActiveEndColor.SimpleColors")));
            this.cbxActiveEndColor.Size = new System.Drawing.Size(148, 31);
            this.cbxActiveEndColor.TabIndex = 26;
            this.cbxActiveEndColor.SelectedIndexChanged += new System.EventHandler(this.ActiveEndColorComboBox_SelectedIndexChanged);
            // 
            // lblDisplayEndColor
            // 
            this.lblDisplayEndColor.AutoSize = true;
            this.lblDisplayEndColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayEndColor.Location = new System.Drawing.Point(6, 49);
            this.lblDisplayEndColor.Name = "lblDisplayEndColor";
            this.lblDisplayEndColor.Size = new System.Drawing.Size(103, 15);
            this.lblDisplayEndColor.TabIndex = 25;
            this.lblDisplayEndColor.Text = "Display End Color:";
            // 
            // cbxActiveColor
            // 
            this.cbxActiveColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxActiveColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActiveColor.FormattingEnabled = true;
            this.cbxActiveColor.ItemHeight = 25;
            this.cbxActiveColor.Items.AddRange(new object[] {
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White",
            "Black",
            "Red",
            "Blue",
            "Yellow",
            "Green",
            "Orange",
            "DarkRed",
            "DarkBlue",
            "DarkYellow",
            "DarkGreen",
            "DarkOrange",
            "LightRed",
            "LightBlue",
            "LightYellow",
            "LightGreen",
            "LightOrange",
            "PaleRed",
            "PaleBlue",
            "PaleYellow",
            "PaleGreen",
            "PaleOrange",
            "BrightRed",
            "BrightBlue",
            "BrightYellow",
            "BrightGreen",
            "BrightOrange",
            "PastelRed",
            "PastelBlue",
            "PastelYellow",
            "PastelGreen",
            "PastelOrange",
            "Silver",
            "Gold",
            "Copper",
            "White"});
            this.cbxActiveColor.Location = new System.Drawing.Point(145, 4);
            this.cbxActiveColor.Name = "cbxActiveColor";
            this.cbxActiveColor.SimpleColors = ((System.Collections.Generic.Dictionary<string, System.Drawing.Color>)(resources.GetObject("cbxActiveColor.SimpleColors")));
            this.cbxActiveColor.Size = new System.Drawing.Size(148, 31);
            this.cbxActiveColor.TabIndex = 24;
            this.cbxActiveColor.SelectedIndexChanged += new System.EventHandler(this.ActiveColorComboBox_SelectedIndexChanged);
            // 
            // lblDisplayColor
            // 
            this.lblDisplayColor.AutoSize = true;
            this.lblDisplayColor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayColor.Location = new System.Drawing.Point(6, 12);
            this.lblDisplayColor.Name = "lblDisplayColor";
            this.lblDisplayColor.Size = new System.Drawing.Size(107, 15);
            this.lblDisplayColor.TabIndex = 17;
            this.lblDisplayColor.Text = "Display Start Color:";
            // 
            // trackBarDots
            // 
            this.trackBarDots.AutoSize = false;
            this.trackBarDots.LargeChange = 1;
            this.trackBarDots.Location = new System.Drawing.Point(145, 42);
            this.trackBarDots.Maximum = 5;
            this.trackBarDots.Minimum = 1;
            this.trackBarDots.Name = "trackBarDots";
            this.trackBarDots.Size = new System.Drawing.Size(148, 29);
            this.trackBarDots.TabIndex = 18;
            this.trackBarDots.Value = 3;
            this.trackBarDots.Visible = false;
            this.trackBarDots.ValueChanged += new System.EventHandler(this.DotsTrackBar_ValueChanged);
            // 
            // lblDotDensityUnit
            // 
            this.lblDotDensityUnit.AutoSize = true;
            this.lblDotDensityUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDotDensityUnit.Location = new System.Drawing.Point(6, 49);
            this.lblDotDensityUnit.Name = "lblDotDensityUnit";
            this.lblDotDensityUnit.Size = new System.Drawing.Size(94, 15);
            this.lblDotDensityUnit.TabIndex = 21;
            this.lblDotDensityUnit.Text = "DotDensity Unit:";
            this.lblDotDensityUnit.Visible = false;
            // 
            // trackBarValueCirleRadio
            // 
            this.trackBarValueCirleRadio.AutoSize = false;
            this.trackBarValueCirleRadio.LargeChange = 1;
            this.trackBarValueCirleRadio.Location = new System.Drawing.Point(145, 42);
            this.trackBarValueCirleRadio.Maximum = 5;
            this.trackBarValueCirleRadio.Minimum = 1;
            this.trackBarValueCirleRadio.Name = "trackBarValueCirleRadio";
            this.trackBarValueCirleRadio.Size = new System.Drawing.Size(148, 29);
            this.trackBarValueCirleRadio.TabIndex = 23;
            this.trackBarValueCirleRadio.Value = 3;
            this.trackBarValueCirleRadio.Visible = false;
            this.trackBarValueCirleRadio.ValueChanged += new System.EventHandler(this.ValueCirleRadioTrackBar_ValueChanged);
            // 
            // lblMore
            // 
            this.lblMore.AutoSize = true;
            this.lblMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMore.Location = new System.Drawing.Point(249, 68);
            this.lblMore.Name = "lblMore";
            this.lblMore.Size = new System.Drawing.Size(47, 12);
            this.lblMore.TabIndex = 20;
            this.lblMore.Text = "More dots";
            this.lblMore.Visible = false;
            // 
            // lblValueCircle
            // 
            this.lblValueCircle.AutoSize = true;
            this.lblValueCircle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCircle.Location = new System.Drawing.Point(7, 46);
            this.lblValueCircle.Name = "lblValueCircle";
            this.lblValueCircle.Size = new System.Drawing.Size(102, 15);
            this.lblValueCircle.TabIndex = 22;
            this.lblValueCircle.Text = "MAGNIFICATION:";
            this.lblValueCircle.Visible = false;
            // 
            // lblFewer
            // 
            this.lblFewer.AutoSize = true;
            this.lblFewer.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFewer.Location = new System.Drawing.Point(143, 68);
            this.lblFewer.Name = "lblFewer";
            this.lblFewer.Size = new System.Drawing.Size(52, 12);
            this.lblFewer.TabIndex = 19;
            this.lblFewer.Text = "Fewer dots";
            this.lblFewer.Visible = false;
            // 
            // pnlControl
            // 
            this.pnlControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlControl.BackgroundImage")));
            this.pnlControl.Controls.Add(this.flowLayoutPanel1);
            this.pnlControl.Location = new System.Drawing.Point(9, 43);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControl.MaximumSize = new System.Drawing.Size(290, 446);
            this.pnlControl.MinimumSize = new System.Drawing.Size(290, 200);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(290, 446);
            this.pnlControl.TabIndex = 1;
            this.pnlControl.Resize += new System.EventHandler(this.CategoryBorder_Resize);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 436);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 10, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select data for display:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 763);
            this.Controls.Add(this.map);
            this.Controls.Add(this.LeftSideBarPanel);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.stpFooter);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 570);
            this.Name = "MainForm";
            this.Text = "US Demographic Map";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.stpFooter.ResumeLayout(false);
            this.stpFooter.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.LeftSideBarPanel.ResumeLayout(false);
            this.LeftSideBarPanel.PerformLayout();
            this.pnlStyleControls.ResumeLayout(false);
            this.pnlStyleControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarValueCirleRadio)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stpFooter;
        private System.Windows.Forms.ToolStripStatusLabel lblLocationY;
        private System.Windows.Forms.ToolStripStatusLabel lblLocationX;
        private System.Windows.Forms.Panel pnlHeader;
        private CollapsiblePanel LeftSideBarPanel;
        private System.Windows.Forms.Label lblBigTitle;
        private System.Windows.Forms.Label lblSmallTitle;
        private WinformsMap map;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlStyleControls;
        private System.Windows.Forms.Label lblColorWheelDirection;
        private System.Windows.Forms.ComboBox cbxSelectColorWheelDirection;
        private ColorComboBox cbxActiveEndColor;
        private System.Windows.Forms.Label lblDisplayEndColor;
        private ColorComboBox cbxActiveColor;
        private System.Windows.Forms.Label lblDisplayColor;
        private System.Windows.Forms.TrackBar trackBarDots;
        private System.Windows.Forms.Label lblDotDensityUnit;
        private System.Windows.Forms.TrackBar trackBarValueCirleRadio;
        private System.Windows.Forms.Label lblMore;
        private System.Windows.Forms.Label lblValueCircle;
        private System.Windows.Forms.Label lblFewer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

