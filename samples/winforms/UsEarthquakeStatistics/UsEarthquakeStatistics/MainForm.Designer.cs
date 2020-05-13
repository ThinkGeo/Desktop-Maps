using System.Drawing;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblBigTitle = new System.Windows.Forms.Label();
            this.lblSmallTitle = new System.Windows.Forms.Label();
            this.stpFooter = new System.Windows.Forms.StatusStrip();
            this.lblFooterLocationY = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFooterLocationX = new System.Windows.Forms.ToolStripStatusLabel();
            this.map = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.dataAndMapSpliter = new System.Windows.Forms.SplitContainer();
            this.QueryResultItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.cluZoomto = new System.Windows.Forms.DataGridViewImageColumn();
            this.cluYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluLongitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluLatitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluDepth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluMagnitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluEmpty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblQueryResult = new System.Windows.Forms.Label();
            this.leftSideBarPanel = new ThinkGeo.MapSuite.EarthquakeStatistics.CollapsiblePanel();
            this.pnlConfigurationTools = new System.Windows.Forms.Panel();
            this.DateLowerLabel = new System.Windows.Forms.Label();
            this.DateUpperLabel = new System.Windows.Forms.Label();
            this.DateSelectionRangeSlider = new ThinkGeo.MapSuite.EarthquakeStatistics.SelectionRangeSlider();
            this.lblDate = new System.Windows.Forms.Label();
            this.DepthLowerLabel = new System.Windows.Forms.Label();
            this.DepthUpperLabel = new System.Windows.Forms.Label();
            this.DepthSelectionRangeSlider = new ThinkGeo.MapSuite.EarthquakeStatistics.SelectionRangeSlider();
            this.lblDepth = new System.Windows.Forms.Label();
            this.MagnitudeLowerLabel = new System.Windows.Forms.Label();
            this.MagnitudeUpperLabel = new System.Windows.Forms.Label();
            this.MagnitudeSelectionRangeSlider = new ThinkGeo.MapSuite.EarthquakeStatistics.SelectionRangeSlider();
            this.lblMagnitude = new System.Windows.Forms.Label();
            this.lblConfiguration = new System.Windows.Forms.Label();
            this.lblQueryTool = new System.Windows.Forms.Label();
            this.pnlQueryTool = new System.Windows.Forms.Panel();
            this.rbtnClearAll = new System.Windows.Forms.RadioButton();
            this.rbtnDrawCircle = new System.Windows.Forms.RadioButton();
            this.rbtnDrawRectangle = new System.Windows.Forms.RadioButton();
            this.rbtnDrawPolygon = new System.Windows.Forms.RadioButton();
            this.PanRadioButton = new System.Windows.Forms.RadioButton();
            this.lblExplorerTools = new System.Windows.Forms.Label();
            this.rbtnPointMap = new System.Windows.Forms.RadioButton();
            this.rbtnIsoMap = new System.Windows.Forms.RadioButton();
            this.HeatMapRadioButton = new System.Windows.Forms.RadioButton();
            this.lblDisplayType = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.stpFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAndMapSpliter)).BeginInit();
            this.dataAndMapSpliter.Panel1.SuspendLayout();
            this.dataAndMapSpliter.Panel2.SuspendLayout();
            this.dataAndMapSpliter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResultItemsDataGridView)).BeginInit();
            this.leftSideBarPanel.SuspendLayout();
            this.pnlConfigurationTools.SuspendLayout();
            this.pnlQueryTool.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(983, 50);
            this.pnlHeader.TabIndex = 3;
            // 
            // lblBigTitle
            // 
            this.lblBigTitle.AutoSize = true;
            this.lblBigTitle.Font = new System.Drawing.Font("Segoe UI", 14.35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBigTitle.Location = new System.Drawing.Point(87, 9);
            this.lblBigTitle.Name = "lblBigTitle";
            this.lblBigTitle.Size = new System.Drawing.Size(222, 28);
            this.lblBigTitle.TabIndex = 1;
            this.lblBigTitle.Text = "US Earthquake Statistics";
            // 
            // lblSmallTitle
            // 
            this.lblSmallTitle.AutoSize = true;
            this.lblSmallTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmallTitle.Location = new System.Drawing.Point(13, 15);
            this.lblSmallTitle.Name = "lblSmallTitle";
            this.lblSmallTitle.Size = new System.Drawing.Size(80, 21);
            this.lblSmallTitle.TabIndex = 0;
            this.lblSmallTitle.Text = "Map Suite";
            // 
            // stpFooter
            // 
            this.stpFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblFooterLocationY,
            this.lblFooterLocationX});
            this.stpFooter.Location = new System.Drawing.Point(0, 601);
            this.stpFooter.Name = "stpFooter";
            this.stpFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stpFooter.Size = new System.Drawing.Size(983, 22);
            this.stpFooter.TabIndex = 4;
            this.stpFooter.Text = "statusStrip1";
            // 
            // lblFooterLocationY
            // 
            this.lblFooterLocationY.AutoSize = false;
            this.lblFooterLocationY.BackColor = System.Drawing.Color.Transparent;
            this.lblFooterLocationY.Name = "lblFooterLocationY";
            this.lblFooterLocationY.Size = new System.Drawing.Size(90, 17);
            this.lblFooterLocationY.Text = "Y:000000";
            this.lblFooterLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFooterLocationX
            // 
            this.lblFooterLocationX.AutoSize = false;
            this.lblFooterLocationX.BackColor = System.Drawing.Color.Transparent;
            this.lblFooterLocationX.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFooterLocationX.Name = "lblFooterLocationX";
            this.lblFooterLocationX.Size = new System.Drawing.Size(90, 17);
            this.lblFooterLocationX.Text = "X:000000";
            this.lblFooterLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.map.Location = new System.Drawing.Point(0, 0);
            this.map.MapFocusMode = WinForms.MapFocusMode.Default;
            this.map.MapResizeMode = MapResizeMode.PreserveScale;
            this.map.MapUnit = GeographyUnit.DecimalDegree;
            this.map.Margin = new System.Windows.Forms.Padding(0);
            this.map.MaximumScale = 80000000000000D;
            this.map.MinimumScale = 200D;
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(663, 408);
            this.map.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.map.TabIndex = 6;
            this.map.Text = "winformsMap1";
            this.map.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.map.ThreadingMode = WinForms.MapThreadingMode.Default;
            this.map.ZoomLevelSnapping = WinForms.ZoomLevelSnappingMode.Default;
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapControl_MouseMove);
            // 
            // dataAndMapSpliter
            // 
            this.dataAndMapSpliter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataAndMapSpliter.BackColor = System.Drawing.Color.DarkGray;
            this.dataAndMapSpliter.Location = new System.Drawing.Point(320, 50);
            this.dataAndMapSpliter.Margin = new System.Windows.Forms.Padding(0);
            this.dataAndMapSpliter.Name = "dataAndMapSpliter";
            this.dataAndMapSpliter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // dataAndMapSpliter.Panel1
            // 
            this.dataAndMapSpliter.Panel1.Controls.Add(this.map);
            this.dataAndMapSpliter.Panel1MinSize = 0;
            // 
            // dataAndMapSpliter.Panel2
            // 
            this.dataAndMapSpliter.Panel2.BackColor = System.Drawing.Color.White;
            this.dataAndMapSpliter.Panel2.Controls.Add(this.QueryResultItemsDataGridView);
            this.dataAndMapSpliter.Panel2.Controls.Add(this.lblQueryResult);
            this.dataAndMapSpliter.Panel2MinSize = 0;
            this.dataAndMapSpliter.Size = new System.Drawing.Size(663, 551);
            this.dataAndMapSpliter.SplitterDistance = 408;
            this.dataAndMapSpliter.TabIndex = 7;
            // 
            // QueryResultItemsDataGridView
            // 
            this.QueryResultItemsDataGridView.AllowUserToAddRows = false;
            this.QueryResultItemsDataGridView.AllowUserToDeleteRows = false;
            this.QueryResultItemsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryResultItemsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.QueryResultItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QueryResultItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cluZoomto,
            this.cluYear,
            this.cluLongitude,
            this.cluLatitude,
            this.cluDepth,
            this.cluMagnitude,
            this.cluLocation,
            this.cluEmpty});
            this.QueryResultItemsDataGridView.GridColor = System.Drawing.Color.White;
            this.QueryResultItemsDataGridView.Location = new System.Drawing.Point(3, 34);
            this.QueryResultItemsDataGridView.Name = "QueryResultItemsDataGridView";
            this.QueryResultItemsDataGridView.ReadOnly = true;
            this.QueryResultItemsDataGridView.RowHeadersVisible = false;
            this.QueryResultItemsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.QueryResultItemsDataGridView.Size = new System.Drawing.Size(657, 102);
            this.QueryResultItemsDataGridView.TabIndex = 16;
            this.QueryResultItemsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellClick);
            this.QueryResultItemsDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellMouseEnter);
            this.QueryResultItemsDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellMouseLeave);
            this.QueryResultItemsDataGridView.SizeChanged += new System.EventHandler(this.QueryResultItemsDataGridView_SizeChanged);
            // 
            // cluZoomto
            // 
            this.cluZoomto.HeaderText = "  ";
            this.cluZoomto.Name = "cluZoomto";
            this.cluZoomto.ReadOnly = true;
            this.cluZoomto.Width = 35;
            // 
            // cluYear
            // 
            this.cluYear.HeaderText = "Year";
            this.cluYear.Name = "cluYear";
            this.cluYear.ReadOnly = true;
            this.cluYear.Width = 150;
            // 
            // cluLongitude
            // 
            this.cluLongitude.HeaderText = "Longitude";
            this.cluLongitude.Name = "cluLongitude";
            this.cluLongitude.ReadOnly = true;
            this.cluLongitude.Width = 150;
            // 
            // cluLatitude
            // 
            this.cluLatitude.HeaderText = "Latitude";
            this.cluLatitude.Name = "cluLatitude";
            this.cluLatitude.ReadOnly = true;
            this.cluLatitude.Width = 150;
            // 
            // cluDepth
            // 
            this.cluDepth.HeaderText = "Depth(KM)";
            this.cluDepth.Name = "cluDepth";
            this.cluDepth.ReadOnly = true;
            this.cluDepth.Width = 150;
            // 
            // cluMagnitude
            // 
            this.cluMagnitude.HeaderText = "Magnitude";
            this.cluMagnitude.Name = "cluMagnitude";
            this.cluMagnitude.ReadOnly = true;
            this.cluMagnitude.Width = 200;
            // 
            // cluLocation
            // 
            this.cluLocation.HeaderText = "Location";
            this.cluLocation.Name = "cluLocation";
            this.cluLocation.ReadOnly = true;
            this.cluLocation.Visible = false;
            // 
            // cluEmpty
            // 
            this.cluEmpty.HeaderText = "";
            this.cluEmpty.Name = "cluEmpty";
            this.cluEmpty.ReadOnly = true;
            // 
            // lblQueryResult
            // 
            this.lblQueryResult.AutoSize = true;
            this.lblQueryResult.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueryResult.Location = new System.Drawing.Point(7, 11);
            this.lblQueryResult.Name = "lblQueryResult";
            this.lblQueryResult.Size = new System.Drawing.Size(92, 20);
            this.lblQueryResult.TabIndex = 0;
            this.lblQueryResult.Text = "Query Result";
            // 
            // leftSideBarPanel
            // 
            this.leftSideBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftSideBarPanel.Controls.Add(this.pnlConfigurationTools);
            this.leftSideBarPanel.Controls.Add(this.lblConfiguration);
            this.leftSideBarPanel.Controls.Add(this.lblQueryTool);
            this.leftSideBarPanel.Controls.Add(this.pnlQueryTool);
            this.leftSideBarPanel.Controls.Add(this.lblExplorerTools);
            this.leftSideBarPanel.Controls.Add(this.rbtnPointMap);
            this.leftSideBarPanel.Controls.Add(this.rbtnIsoMap);
            this.leftSideBarPanel.Controls.Add(this.HeatMapRadioButton);
            this.leftSideBarPanel.Controls.Add(this.lblDisplayType);
            this.leftSideBarPanel.LineWidth = 5;
            this.leftSideBarPanel.Location = new System.Drawing.Point(0, 50);
            this.leftSideBarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftSideBarPanel.Name = "leftSideBarPanel";
            this.leftSideBarPanel.PanelWidth = 300;
            this.leftSideBarPanel.Size = new System.Drawing.Size(320, 551);
            this.leftSideBarPanel.TabIndex = 5;
            this.leftSideBarPanel.PanelCollapseButtonClick += new System.EventHandler(this.LeftSideBarPanel_PanelCollapseButtonClick);
            // 
            // pnlConfigurationTools
            // 
            this.pnlConfigurationTools.Controls.Add(this.DateLowerLabel);
            this.pnlConfigurationTools.Controls.Add(this.DateUpperLabel);
            this.pnlConfigurationTools.Controls.Add(this.DateSelectionRangeSlider);
            this.pnlConfigurationTools.Controls.Add(this.lblDate);
            this.pnlConfigurationTools.Controls.Add(this.DepthLowerLabel);
            this.pnlConfigurationTools.Controls.Add(this.DepthUpperLabel);
            this.pnlConfigurationTools.Controls.Add(this.DepthSelectionRangeSlider);
            this.pnlConfigurationTools.Controls.Add(this.lblDepth);
            this.pnlConfigurationTools.Controls.Add(this.MagnitudeLowerLabel);
            this.pnlConfigurationTools.Controls.Add(this.MagnitudeUpperLabel);
            this.pnlConfigurationTools.Controls.Add(this.MagnitudeSelectionRangeSlider);
            this.pnlConfigurationTools.Controls.Add(this.lblMagnitude);
            this.pnlConfigurationTools.Location = new System.Drawing.Point(9, 254);
            this.pnlConfigurationTools.Name = "pnlConfigurationTools";
            this.pnlConfigurationTools.Size = new System.Drawing.Size(293, 213);
            this.pnlConfigurationTools.TabIndex = 25;
            // 
            // DateLowerLabel
            // 
            this.DateLowerLabel.AutoSize = true;
            this.DateLowerLabel.Location = new System.Drawing.Point(10, 169);
            this.DateLowerLabel.Name = "DateLowerLabel";
            this.DateLowerLabel.Size = new System.Drawing.Size(31, 13);
            this.DateLowerLabel.TabIndex = 37;
            this.DateLowerLabel.Text = "1568";
            // 
            // DateUpperLabel
            // 
            this.DateUpperLabel.AutoSize = true;
            this.DateUpperLabel.Location = new System.Drawing.Point(250, 169);
            this.DateUpperLabel.Name = "DateUpperLabel";
            this.DateUpperLabel.Size = new System.Drawing.Size(31, 13);
            this.DateUpperLabel.TabIndex = 36;
            this.DateUpperLabel.Text = "2010";
            // 
            // DateSelectionRangeSlider
            // 
            this.DateSelectionRangeSlider.Location = new System.Drawing.Point(48, 166);
            this.DateSelectionRangeSlider.Maximum = 2010;
            this.DateSelectionRangeSlider.Minimum = 1568;
            this.DateSelectionRangeSlider.Name = "DateSelectionRangeSlider";
            this.DateSelectionRangeSlider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.DateSelectionRangeSlider.Size = new System.Drawing.Size(201, 30);
            this.DateSelectionRangeSlider.SmallChange = 1;
            this.DateSelectionRangeSlider.TabIndex = 35;
            this.DateSelectionRangeSlider.Text = "selectionRangeSlider1";
            this.DateSelectionRangeSlider.TickNum = 28;
            this.DateSelectionRangeSlider.ValueLeft = 1568;
            this.DateSelectionRangeSlider.ValueRight = 2010;
            this.DateSelectionRangeSlider.LeftValueChanged += new System.EventHandler(this.DateSelectionRangeSlider_ValueChanged);
            this.DateSelectionRangeSlider.RightValueChanged += new System.EventHandler(this.DateSelectionRangeSlider_ValueChanged);
            this.DateSelectionRangeSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ConfigurationSlider_MouseUp);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(9, 143);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(83, 20);
            this.lblDate.TabIndex = 34;
            this.lblDate.Text = "Date(Year):";
            // 
            // DepthLowerLabel
            // 
            this.DepthLowerLabel.AutoSize = true;
            this.DepthLowerLabel.Location = new System.Drawing.Point(10, 104);
            this.DepthLowerLabel.Name = "DepthLowerLabel";
            this.DepthLowerLabel.Size = new System.Drawing.Size(13, 13);
            this.DepthLowerLabel.TabIndex = 33;
            this.DepthLowerLabel.Text = "0";
            // 
            // DepthUpperLabel
            // 
            this.DepthUpperLabel.AutoSize = true;
            this.DepthUpperLabel.Location = new System.Drawing.Point(250, 104);
            this.DepthUpperLabel.Name = "DepthUpperLabel";
            this.DepthUpperLabel.Size = new System.Drawing.Size(25, 13);
            this.DepthUpperLabel.TabIndex = 32;
            this.DepthUpperLabel.Text = "300";
            // 
            // DepthSelectionRangeSlider
            // 
            this.DepthSelectionRangeSlider.Location = new System.Drawing.Point(48, 101);
            this.DepthSelectionRangeSlider.Maximum = 300;
            this.DepthSelectionRangeSlider.Minimum = 0;
            this.DepthSelectionRangeSlider.Name = "DepthSelectionRangeSlider";
            this.DepthSelectionRangeSlider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.DepthSelectionRangeSlider.Size = new System.Drawing.Size(201, 30);
            this.DepthSelectionRangeSlider.SmallChange = 1;
            this.DepthSelectionRangeSlider.TabIndex = 31;
            this.DepthSelectionRangeSlider.Text = "selectionRangeSlider1";
            this.DepthSelectionRangeSlider.TickNum = 20;
            this.DepthSelectionRangeSlider.ValueLeft = 0;
            this.DepthSelectionRangeSlider.ValueRight = 300;
            this.DepthSelectionRangeSlider.LeftValueChanged += new System.EventHandler(this.DepthSelectionRangeSlider_ValueChanged);
            this.DepthSelectionRangeSlider.RightValueChanged += new System.EventHandler(this.DepthSelectionRangeSlider_ValueChanged);
            this.DepthSelectionRangeSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ConfigurationSlider_MouseUp);
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepth.Location = new System.Drawing.Point(9, 78);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(184, 20);
            this.lblDepth.TabIndex = 30;
            this.lblDepth.Text = "Depth of Hypocenter(Km):";
            // 
            // MagnitudeLowerLabel
            // 
            this.MagnitudeLowerLabel.AutoSize = true;
            this.MagnitudeLowerLabel.Location = new System.Drawing.Point(10, 36);
            this.MagnitudeLowerLabel.Name = "MagnitudeLowerLabel";
            this.MagnitudeLowerLabel.Size = new System.Drawing.Size(13, 13);
            this.MagnitudeLowerLabel.TabIndex = 29;
            this.MagnitudeLowerLabel.Text = "0";
            // 
            // MagnitudeUpperLabel
            // 
            this.MagnitudeUpperLabel.AutoSize = true;
            this.MagnitudeUpperLabel.Location = new System.Drawing.Point(250, 36);
            this.MagnitudeUpperLabel.Name = "MagnitudeUpperLabel";
            this.MagnitudeUpperLabel.Size = new System.Drawing.Size(19, 13);
            this.MagnitudeUpperLabel.TabIndex = 28;
            this.MagnitudeUpperLabel.Text = "12";
            // 
            // MagnitudeSelectionRangeSlider
            // 
            this.MagnitudeSelectionRangeSlider.Location = new System.Drawing.Point(48, 33);
            this.MagnitudeSelectionRangeSlider.Maximum = 12;
            this.MagnitudeSelectionRangeSlider.Minimum = 0;
            this.MagnitudeSelectionRangeSlider.Name = "MagnitudeSelectionRangeSlider";
            this.MagnitudeSelectionRangeSlider.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.MagnitudeSelectionRangeSlider.Size = new System.Drawing.Size(201, 30);
            this.MagnitudeSelectionRangeSlider.SmallChange = 1;
            this.MagnitudeSelectionRangeSlider.TabIndex = 27;
            this.MagnitudeSelectionRangeSlider.Text = "selectionRangeSlider1";
            this.MagnitudeSelectionRangeSlider.TickNum = 1;
            this.MagnitudeSelectionRangeSlider.ValueLeft = 0;
            this.MagnitudeSelectionRangeSlider.ValueRight = 12;
            this.MagnitudeSelectionRangeSlider.LeftValueChanged += new System.EventHandler(this.MagnitudeSelectionRangeSlider_ValueChanged);
            this.MagnitudeSelectionRangeSlider.RightValueChanged += new System.EventHandler(this.MagnitudeSelectionRangeSlider_ValueChanged);
            this.MagnitudeSelectionRangeSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ConfigurationSlider_MouseUp);
            // 
            // lblMagnitude
            // 
            this.lblMagnitude.AutoSize = true;
            this.lblMagnitude.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMagnitude.Location = new System.Drawing.Point(9, 10);
            this.lblMagnitude.Name = "lblMagnitude";
            this.lblMagnitude.Size = new System.Drawing.Size(84, 20);
            this.lblMagnitude.TabIndex = 26;
            this.lblMagnitude.Text = "Magnitude:";
            // 
            // lblConfiguration
            // 
            this.lblConfiguration.AutoSize = true;
            this.lblConfiguration.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfiguration.Location = new System.Drawing.Point(5, 225);
            this.lblConfiguration.Name = "lblConfiguration";
            this.lblConfiguration.Size = new System.Drawing.Size(146, 20);
            this.lblConfiguration.TabIndex = 24;
            this.lblConfiguration.Text = "Query Configuration:";
            // 
            // lblQueryTool
            // 
            this.lblQueryTool.AutoSize = true;
            this.lblQueryTool.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQueryTool.Location = new System.Drawing.Point(5, 138);
            this.lblQueryTool.Name = "lblQueryTool";
            this.lblQueryTool.Size = new System.Drawing.Size(85, 20);
            this.lblQueryTool.TabIndex = 23;
            this.lblQueryTool.Text = "Query Tool:";
            // 
            // pnlQueryTool
            // 
            this.pnlQueryTool.Controls.Add(this.rbtnClearAll);
            this.pnlQueryTool.Controls.Add(this.rbtnDrawCircle);
            this.pnlQueryTool.Controls.Add(this.rbtnDrawRectangle);
            this.pnlQueryTool.Controls.Add(this.rbtnDrawPolygon);
            this.pnlQueryTool.Controls.Add(this.PanRadioButton);
            this.pnlQueryTool.Location = new System.Drawing.Point(9, 165);
            this.pnlQueryTool.Name = "pnlQueryTool";
            this.pnlQueryTool.Size = new System.Drawing.Size(293, 51);
            this.pnlQueryTool.TabIndex = 22;
            // 
            // rbtnClearAll
            // 
            this.rbtnClearAll.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnClearAll.AutoSize = true;
            this.rbtnClearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnClearAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnClearAll.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnClearAll.Image = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.clear;
            this.rbtnClearAll.Location = new System.Drawing.Point(176, 6);
            this.rbtnClearAll.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnClearAll.Name = "rbtnClearAll";
            this.rbtnClearAll.Size = new System.Drawing.Size(40, 40);
            this.rbtnClearAll.TabIndex = 23;
            this.rbtnClearAll.UseVisualStyleBackColor = true;
            this.rbtnClearAll.CheckedChanged += new System.EventHandler(this.MapModeRadioButton_CheckedChanged);
            this.rbtnClearAll.Click += new System.EventHandler(this.ClearTrackResult_Click);
            // 
            // rbtnDrawCircle
            // 
            this.rbtnDrawCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrawCircle.AutoSize = true;
            this.rbtnDrawCircle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnDrawCircle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnDrawCircle.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnDrawCircle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrawCircle.Image = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.draw_circle;
            this.rbtnDrawCircle.Location = new System.Drawing.Point(133, 6);
            this.rbtnDrawCircle.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnDrawCircle.Name = "rbtnDrawCircle";
            this.rbtnDrawCircle.Size = new System.Drawing.Size(40, 40);
            this.rbtnDrawCircle.TabIndex = 22;
            this.rbtnDrawCircle.UseVisualStyleBackColor = true;
            this.rbtnDrawCircle.CheckedChanged += new System.EventHandler(this.MapModeRadioButton_CheckedChanged);
            // 
            // rbtnDrawRectangle
            // 
            this.rbtnDrawRectangle.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrawRectangle.AutoSize = true;
            this.rbtnDrawRectangle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnDrawRectangle.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnDrawRectangle.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnDrawRectangle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrawRectangle.Image = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.Draw_Rectangle;
            this.rbtnDrawRectangle.Location = new System.Drawing.Point(90, 6);
            this.rbtnDrawRectangle.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnDrawRectangle.Name = "rbtnDrawRectangle";
            this.rbtnDrawRectangle.Size = new System.Drawing.Size(40, 40);
            this.rbtnDrawRectangle.TabIndex = 21;
            this.rbtnDrawRectangle.UseVisualStyleBackColor = true;
            this.rbtnDrawRectangle.CheckedChanged += new System.EventHandler(this.MapModeRadioButton_CheckedChanged);
            // 
            // rbtnDrawPolygon
            // 
            this.rbtnDrawPolygon.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrawPolygon.AutoSize = true;
            this.rbtnDrawPolygon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnDrawPolygon.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnDrawPolygon.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnDrawPolygon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrawPolygon.Image = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.draw_polygon;
            this.rbtnDrawPolygon.Location = new System.Drawing.Point(48, 6);
            this.rbtnDrawPolygon.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnDrawPolygon.Name = "rbtnDrawPolygon";
            this.rbtnDrawPolygon.Size = new System.Drawing.Size(40, 40);
            this.rbtnDrawPolygon.TabIndex = 20;
            this.rbtnDrawPolygon.UseVisualStyleBackColor = true;
            this.rbtnDrawPolygon.CheckedChanged += new System.EventHandler(this.MapModeRadioButton_CheckedChanged);
            // 
            // PanRadioButton
            // 
            this.PanRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.PanRadioButton.AutoSize = true;
            this.PanRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PanRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.PanRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PanRadioButton.Image = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.Pan;
            this.PanRadioButton.Location = new System.Drawing.Point(6, 6);
            this.PanRadioButton.Margin = new System.Windows.Forms.Padding(5);
            this.PanRadioButton.Name = "PanRadioButton";
            this.PanRadioButton.Size = new System.Drawing.Size(40, 40);
            this.PanRadioButton.TabIndex = 19;
            this.PanRadioButton.UseVisualStyleBackColor = true;
            this.PanRadioButton.CheckedChanged += new System.EventHandler(this.MapModeRadioButton_CheckedChanged);
            // 
            // lblExplorerTools
            // 
            this.lblExplorerTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(218)))));
            this.lblExplorerTools.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExplorerTools.ForeColor = System.Drawing.Color.White;
            this.lblExplorerTools.Location = new System.Drawing.Point(5, 103);
            this.lblExplorerTools.Name = "lblExplorerTools";
            this.lblExplorerTools.Size = new System.Drawing.Size(297, 25);
            this.lblExplorerTools.TabIndex = 21;
            this.lblExplorerTools.Text = "Earthquake Information Explorer";
            this.lblExplorerTools.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rbtnPointMap
            // 
            this.rbtnPointMap.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPointMap.BackgroundImage = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.pointMap;
            this.rbtnPointMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnPointMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnPointMap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.rbtnPointMap.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnPointMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnPointMap.Location = new System.Drawing.Point(75, 38);
            this.rbtnPointMap.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnPointMap.Name = "rbtnPointMap";
            this.rbtnPointMap.Size = new System.Drawing.Size(56, 56);
            this.rbtnPointMap.TabIndex = 20;
            this.rbtnPointMap.Tag = "Regular Point Map";
            this.rbtnPointMap.UseVisualStyleBackColor = true;
            this.rbtnPointMap.CheckedChanged += new System.EventHandler(this.MapType_CheckedChanged);
            // 
            // rbtnIsoMap
            // 
            this.rbtnIsoMap.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnIsoMap.BackgroundImage = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.IsoLine;
            this.rbtnIsoMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rbtnIsoMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnIsoMap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.rbtnIsoMap.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnIsoMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnIsoMap.Location = new System.Drawing.Point(141, 38);
            this.rbtnIsoMap.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnIsoMap.Name = "rbtnIsoMap";
            this.rbtnIsoMap.Size = new System.Drawing.Size(56, 56);
            this.rbtnIsoMap.TabIndex = 19;
            this.rbtnIsoMap.Tag = "IsoLines Map";
            this.rbtnIsoMap.UseVisualStyleBackColor = true;
            this.rbtnIsoMap.CheckedChanged += new System.EventHandler(this.MapType_CheckedChanged);
            // 
            // HeatMapRadioButton
            // 
            this.HeatMapRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.HeatMapRadioButton.BackgroundImage = global::ThinkGeo.MapSuite.EarthquakeStatistics.Properties.Resources.heatMap;
            this.HeatMapRadioButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HeatMapRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.HeatMapRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.HeatMapRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.HeatMapRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HeatMapRadioButton.Location = new System.Drawing.Point(9, 38);
            this.HeatMapRadioButton.Margin = new System.Windows.Forms.Padding(5);
            this.HeatMapRadioButton.Name = "HeatMapRadioButton";
            this.HeatMapRadioButton.Size = new System.Drawing.Size(56, 56);
            this.HeatMapRadioButton.TabIndex = 18;
            this.HeatMapRadioButton.Tag = "Heat Map";
            this.HeatMapRadioButton.UseVisualStyleBackColor = true;
            this.HeatMapRadioButton.CheckedChanged += new System.EventHandler(this.MapType_CheckedChanged);
            // 
            // lblDisplayType
            // 
            this.lblDisplayType.AutoSize = true;
            this.lblDisplayType.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayType.Location = new System.Drawing.Point(5, 10);
            this.lblDisplayType.Name = "lblDisplayType";
            this.lblDisplayType.Size = new System.Drawing.Size(97, 20);
            this.lblDisplayType.TabIndex = 17;
            this.lblDisplayType.Text = "Display Type:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(983, 623);
            this.Controls.Add(this.dataAndMapSpliter);
            this.Controls.Add(this.leftSideBarPanel);
            this.Controls.Add(this.stpFooter);
            this.Controls.Add(this.pnlHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "US Earthquake Statistics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.stpFooter.ResumeLayout(false);
            this.stpFooter.PerformLayout();
            this.dataAndMapSpliter.Panel1.ResumeLayout(false);
            this.dataAndMapSpliter.Panel2.ResumeLayout(false);
            this.dataAndMapSpliter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataAndMapSpliter)).EndInit();
            this.dataAndMapSpliter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.QueryResultItemsDataGridView)).EndInit();
            this.leftSideBarPanel.ResumeLayout(false);
            this.leftSideBarPanel.PerformLayout();
            this.pnlConfigurationTools.ResumeLayout(false);
            this.pnlConfigurationTools.PerformLayout();
            this.pnlQueryTool.ResumeLayout(false);
            this.pnlQueryTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblBigTitle;
        private System.Windows.Forms.Label lblSmallTitle;
        private System.Windows.Forms.StatusStrip stpFooter;
        private System.Windows.Forms.ToolStripStatusLabel lblFooterLocationY;
        private System.Windows.Forms.ToolStripStatusLabel lblFooterLocationX;
        private CollapsiblePanel leftSideBarPanel;
        private WinForms.WinformsMap map;
        private System.Windows.Forms.SplitContainer dataAndMapSpliter;
        private System.Windows.Forms.DataGridView QueryResultItemsDataGridView;
        private System.Windows.Forms.Label lblQueryResult;
        private System.Windows.Forms.Label lblExplorerTools;
        private System.Windows.Forms.RadioButton rbtnPointMap;
        private System.Windows.Forms.RadioButton rbtnIsoMap;
        private System.Windows.Forms.RadioButton HeatMapRadioButton;
        private System.Windows.Forms.Label lblDisplayType;
        private System.Windows.Forms.Label lblQueryTool;
        private System.Windows.Forms.Panel pnlQueryTool;
        private System.Windows.Forms.RadioButton PanRadioButton;
        private System.Windows.Forms.RadioButton rbtnClearAll;
        private System.Windows.Forms.RadioButton rbtnDrawCircle;
        private System.Windows.Forms.RadioButton rbtnDrawRectangle;
        private System.Windows.Forms.RadioButton rbtnDrawPolygon;
        private System.Windows.Forms.Panel pnlConfigurationTools;
        private System.Windows.Forms.Label lblMagnitude;
        private System.Windows.Forms.Label lblConfiguration;
        private System.Windows.Forms.Label MagnitudeLowerLabel;
        private System.Windows.Forms.Label MagnitudeUpperLabel;
        private SelectionRangeSlider MagnitudeSelectionRangeSlider;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label DepthLowerLabel;
        private System.Windows.Forms.Label DepthUpperLabel;
        private SelectionRangeSlider DepthSelectionRangeSlider;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.Label DateLowerLabel;
        private System.Windows.Forms.Label DateUpperLabel;
        private SelectionRangeSlider DateSelectionRangeSlider;
        private System.Windows.Forms.DataGridViewImageColumn cluZoomto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluLongitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluLatitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluDepth;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluMagnitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluEmpty;
    }
}

