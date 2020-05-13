using System.Drawing;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
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
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.lblBigTitle = new System.Windows.Forms.Label();
            this.lblSmallTitle = new System.Windows.Forms.Label();
            this.stpFooter = new System.Windows.Forms.StatusStrip();
            this.FooterLocationYToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FooterLocationXToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.map = new WinformsMap();
            this.leftSideBarPanel = new ThinkGeo.MapSuite.SiteSelection.CollapsiblePanel();
            this.QueryResultItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.cluZoomto = new System.Windows.Forms.DataGridViewImageColumn();
            this.cluName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResultTitle = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.AreaTypePanel = new System.Windows.Forms.Panel();
            this.BufferPanel = new System.Windows.Forms.Panel();
            this.DistanceValueTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DistanceUnitComboBox = new System.Windows.Forms.ComboBox();
            this.BufferRadioButton = new System.Windows.Forms.RadioButton();
            this.RoutePanel = new System.Windows.Forms.Panel();
            this.RouteTimeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RouteRadioButton = new System.Windows.Forms.RadioButton();
            this.lblAreaType = new System.Windows.Forms.Label();
            this.pnlControlMode = new System.Windows.Forms.Panel();
            this.RadioButtonClear = new System.Windows.Forms.RadioButton();
            this.rbtnDrawPoint = new System.Windows.Forms.RadioButton();
            this.PanRadioButton = new System.Windows.Forms.RadioButton();
            this.lblControlMode = new System.Windows.Forms.Label();
            this.pnlBuildingType = new System.Windows.Forms.Panel();
            this.CandidatePoiTypesComboBox = new System.Windows.Forms.ComboBox();
            this.CandidatePoiLayersComboBox = new System.Windows.Forms.ComboBox();
            this.lblLeftSideBarTitle = new System.Windows.Forms.Label();
            this.HeaderPanel.SuspendLayout();
            this.stpFooter.SuspendLayout();
            this.leftSideBarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResultItemsDataGridView)).BeginInit();
            this.AreaTypePanel.SuspendLayout();
            this.BufferPanel.SuspendLayout();
            this.RoutePanel.SuspendLayout();
            this.pnlControlMode.SuspendLayout();
            this.pnlBuildingType.SuspendLayout();
            this.SuspendLayout();
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HeaderPanel.BackColor = System.Drawing.Color.Transparent;
            this.HeaderPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("HeaderPanel.BackgroundImage")));
            this.HeaderPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HeaderPanel.Controls.Add(this.lblBigTitle);
            this.HeaderPanel.Controls.Add(this.lblSmallTitle);
            this.HeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.HeaderPanel.Margin = new System.Windows.Forms.Padding(0);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(674, 50);
            this.HeaderPanel.TabIndex = 2;
            // 
            // lblBigTitle
            // 
            this.lblBigTitle.AutoSize = true;
            this.lblBigTitle.Font = new System.Drawing.Font("Segoe UI", 14.35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBigTitle.Location = new System.Drawing.Point(92, 9);
            this.lblBigTitle.Name = "lblBigTitle";
            this.lblBigTitle.Size = new System.Drawing.Size(130, 28);
            this.lblBigTitle.TabIndex = 1;
            this.lblBigTitle.Text = "Site Selection";
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
            // stpFooter
            // 
            this.stpFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FooterLocationYToolStripStatusLabel,
            this.FooterLocationXToolStripStatusLabel});
            this.stpFooter.Location = new System.Drawing.Point(0, 627);
            this.stpFooter.Name = "stpFooter";
            this.stpFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.stpFooter.Size = new System.Drawing.Size(674, 22);
            this.stpFooter.TabIndex = 3;
            this.stpFooter.Text = "statusStrip1";
            // 
            // FooterLocationYToolStripStatusLabel
            // 
            this.FooterLocationYToolStripStatusLabel.AutoSize = false;
            this.FooterLocationYToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.FooterLocationYToolStripStatusLabel.Name = "FooterLocationYToolStripStatusLabel";
            this.FooterLocationYToolStripStatusLabel.Size = new System.Drawing.Size(90, 17);
            this.FooterLocationYToolStripStatusLabel.Text = "Y:000000";
            this.FooterLocationYToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FooterLocationXToolStripStatusLabel
            // 
            this.FooterLocationXToolStripStatusLabel.AutoSize = false;
            this.FooterLocationXToolStripStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.FooterLocationXToolStripStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FooterLocationXToolStripStatusLabel.Name = "FooterLocationXToolStripStatusLabel";
            this.FooterLocationXToolStripStatusLabel.Size = new System.Drawing.Size(90, 17);
            this.FooterLocationXToolStripStatusLabel.Text = "X:000000";
            this.FooterLocationXToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.map.MapFocusMode = MapFocusMode.Default;
            this.map.MapResizeMode = MapResizeMode.PreserveScale;
            this.map.MapUnit = GeographyUnit.DecimalDegree;
            this.map.Margin = new System.Windows.Forms.Padding(0);
            this.map.MaximumScale = 80000000000000D;
            this.map.MinimumScale = 200D;
            this.map.Name = "map";
            this.map.Size = new System.Drawing.Size(354, 577);
            this.map.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.map.TabIndex = 5;
            this.map.Text = "winformsMap1";
            this.map.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.map.ThreadingMode = MapThreadingMode.Default;
            this.map.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            this.map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
            // 
            // leftSideBarPanel
            // 
            this.leftSideBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftSideBarPanel.Controls.Add(this.QueryResultItemsDataGridView);
            this.leftSideBarPanel.Controls.Add(this.lblResultTitle);
            this.leftSideBarPanel.Controls.Add(this.btnApply);
            this.leftSideBarPanel.Controls.Add(this.AreaTypePanel);
            this.leftSideBarPanel.Controls.Add(this.lblAreaType);
            this.leftSideBarPanel.Controls.Add(this.pnlControlMode);
            this.leftSideBarPanel.Controls.Add(this.lblControlMode);
            this.leftSideBarPanel.Controls.Add(this.pnlBuildingType);
            this.leftSideBarPanel.Controls.Add(this.lblLeftSideBarTitle);
            this.leftSideBarPanel.LineWidth = 5;
            this.leftSideBarPanel.Location = new System.Drawing.Point(0, 50);
            this.leftSideBarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftSideBarPanel.Name = "leftSideBarPanel";
            this.leftSideBarPanel.PanelWidth = 300;
            this.leftSideBarPanel.Size = new System.Drawing.Size(320, 577);
            this.leftSideBarPanel.TabIndex = 4;
            this.leftSideBarPanel.ToggleExpandedButtonClick += new System.EventHandler(this.LeftSideBarPanel_PanelCollapseButtonClick);
            // 
            // QueryResultItemsDataGridView
            // 
            this.QueryResultItemsDataGridView.AllowUserToAddRows = false;
            this.QueryResultItemsDataGridView.AllowUserToDeleteRows = false;
            this.QueryResultItemsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.QueryResultItemsDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.QueryResultItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QueryResultItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cluZoomto,
            this.cluName,
            this.Location});
            this.QueryResultItemsDataGridView.GridColor = System.Drawing.Color.White;
            this.QueryResultItemsDataGridView.Location = new System.Drawing.Point(11, 493);
            this.QueryResultItemsDataGridView.Name = "QueryResultItemsDataGridView";
            this.QueryResultItemsDataGridView.ReadOnly = true;
            this.QueryResultItemsDataGridView.RowHeadersVisible = false;
            this.QueryResultItemsDataGridView.Size = new System.Drawing.Size(288, 64);
            this.QueryResultItemsDataGridView.TabIndex = 15;
            this.QueryResultItemsDataGridView.Visible = false;
            this.QueryResultItemsDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellClick);
            this.QueryResultItemsDataGridView.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellMouseEnter);
            this.QueryResultItemsDataGridView.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.QueryResultItemsDataGridView_CellMouseLeave);
            // 
            // cluZoomto
            // 
            this.cluZoomto.HeaderText = "  ";
            this.cluZoomto.Name = "cluZoomto";
            this.cluZoomto.ReadOnly = true;
            this.cluZoomto.Width = 35;
            // 
            // cluName
            // 
            this.cluName.HeaderText = "Name";
            this.cluName.Name = "cluName";
            this.cluName.ReadOnly = true;
            this.cluName.Width = 220;
            // 
            // Location
            // 
            this.Location.HeaderText = "Location";
            this.Location.Name = "Location";
            this.Location.ReadOnly = true;
            this.Location.Visible = false;
            // 
            // lblResultTitle
            // 
            this.lblResultTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(218)))));
            this.lblResultTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultTitle.ForeColor = System.Drawing.Color.White;
            this.lblResultTitle.Location = new System.Drawing.Point(11, 462);
            this.lblResultTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(288, 28);
            this.lblResultTitle.TabIndex = 14;
            this.lblResultTitle.Text = "Potential Similar Sites";
            this.lblResultTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(203, 400);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(87, 26);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.ApplyQueryButton_Click);
            // 
            // AreaTypePanel
            // 
            this.AreaTypePanel.Controls.Add(this.BufferPanel);
            this.AreaTypePanel.Controls.Add(this.BufferRadioButton);
            this.AreaTypePanel.Controls.Add(this.RoutePanel);
            this.AreaTypePanel.Controls.Add(this.RouteRadioButton);
            this.AreaTypePanel.Location = new System.Drawing.Point(11, 293);
            this.AreaTypePanel.Name = "AreaTypePanel";
            this.AreaTypePanel.Size = new System.Drawing.Size(288, 148);
            this.AreaTypePanel.TabIndex = 9;
            // 
            // BufferPanel
            // 
            this.BufferPanel.Controls.Add(this.DistanceValueTextBox);
            this.BufferPanel.Controls.Add(this.label4);
            this.BufferPanel.Controls.Add(this.DistanceUnitComboBox);
            this.BufferPanel.Location = new System.Drawing.Point(20, 104);
            this.BufferPanel.Name = "BufferPanel";
            this.BufferPanel.Size = new System.Drawing.Size(261, 33);
            this.BufferPanel.TabIndex = 15;
            // 
            // DistanceValueTextBox
            // 
            this.DistanceValueTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceValueTextBox.Location = new System.Drawing.Point(108, 3);
            this.DistanceValueTextBox.Name = "DistanceValueTextBox";
            this.DistanceValueTextBox.Size = new System.Drawing.Size(58, 26);
            this.DistanceValueTextBox.TabIndex = 9;
            this.DistanceValueTextBox.TextChanged += new System.EventHandler(this.DistanceValueTextBox_TextChanged);
            this.DistanceValueTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DistanceValueTextBox_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Buffer Distance: ";
            // 
            // DistanceUnitComboBox
            // 
            this.DistanceUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DistanceUnitComboBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceUnitComboBox.FormattingEnabled = true;
            this.DistanceUnitComboBox.Items.AddRange(new object[] {
            "Kilometers",
            "Miles"});
            this.DistanceUnitComboBox.Location = new System.Drawing.Point(172, 4);
            this.DistanceUnitComboBox.Name = "DistanceUnitComboBox";
            this.DistanceUnitComboBox.Size = new System.Drawing.Size(86, 25);
            this.DistanceUnitComboBox.TabIndex = 10;
            // 
            // BufferRadioButton
            // 
            this.BufferRadioButton.AutoSize = true;
            this.BufferRadioButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BufferRadioButton.Location = new System.Drawing.Point(4, 77);
            this.BufferRadioButton.Name = "BufferRadioButton";
            this.BufferRadioButton.Size = new System.Drawing.Size(91, 21);
            this.BufferRadioButton.TabIndex = 14;
            this.BufferRadioButton.Text = "Buffer Area";
            this.BufferRadioButton.UseVisualStyleBackColor = true;
            this.BufferRadioButton.CheckedChanged += new System.EventHandler(this.AreaTypeRadioButton_CheckedChanged);
            // 
            // RoutePanel
            // 
            this.RoutePanel.Controls.Add(this.RouteTimeTextBox);
            this.RoutePanel.Controls.Add(this.label6);
            this.RoutePanel.Controls.Add(this.label5);
            this.RoutePanel.Location = new System.Drawing.Point(20, 38);
            this.RoutePanel.Name = "RoutePanel";
            this.RoutePanel.Size = new System.Drawing.Size(265, 33);
            this.RoutePanel.TabIndex = 12;
            // 
            // RouteTimeTextBox
            // 
            this.RouteTimeTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RouteTimeTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RouteTimeTextBox.Location = new System.Drawing.Point(99, 4);
            this.RouteTimeTextBox.Name = "RouteTimeTextBox";
            this.RouteTimeTextBox.Size = new System.Drawing.Size(58, 26);
            this.RouteTimeTextBox.TabIndex = 9;
            this.RouteTimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RouteTimeTextBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(158, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Minutes Driving";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Service Area in ";
            // 
            // RouteRadioButton
            // 
            this.RouteRadioButton.AutoSize = true;
            this.RouteRadioButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RouteRadioButton.Location = new System.Drawing.Point(5, 15);
            this.RouteRadioButton.Name = "RouteRadioButton";
            this.RouteRadioButton.Size = new System.Drawing.Size(98, 21);
            this.RouteRadioButton.TabIndex = 7;
            this.RouteRadioButton.Text = "Service Area";
            this.RouteRadioButton.UseVisualStyleBackColor = true;
            this.RouteRadioButton.CheckedChanged += new System.EventHandler(this.AreaTypeRadioButton_CheckedChanged);
            // 
            // lblAreaType
            // 
            this.lblAreaType.AutoSize = true;
            this.lblAreaType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaType.Location = new System.Drawing.Point(7, 269);
            this.lblAreaType.Name = "lblAreaType";
            this.lblAreaType.Size = new System.Drawing.Size(82, 21);
            this.lblAreaType.TabIndex = 5;
            this.lblAreaType.Text = "Area Type:";
            // 
            // pnlControlMode
            // 
            this.pnlControlMode.Controls.Add(this.RadioButtonClear);
            this.pnlControlMode.Controls.Add(this.rbtnDrawPoint);
            this.pnlControlMode.Controls.Add(this.PanRadioButton);
            this.pnlControlMode.Location = new System.Drawing.Point(11, 194);
            this.pnlControlMode.Name = "pnlControlMode";
            this.pnlControlMode.Size = new System.Drawing.Size(288, 51);
            this.pnlControlMode.TabIndex = 4;
            // 
            // RadioButtonClear
            // 
            this.RadioButtonClear.Appearance = System.Windows.Forms.Appearance.Button;
            this.RadioButtonClear.AutoSize = true;
            this.RadioButtonClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RadioButtonClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.RadioButtonClear.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.RadioButtonClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RadioButtonClear.Image = global::ThinkGeo.MapSuite.SiteSelection.Properties.Resources.clear;
            this.RadioButtonClear.Location = new System.Drawing.Point(100, 6);
            this.RadioButtonClear.Margin = new System.Windows.Forms.Padding(5);
            this.RadioButtonClear.Name = "RadioButtonClear";
            this.RadioButtonClear.Size = new System.Drawing.Size(40, 40);
            this.RadioButtonClear.TabIndex = 2;
            this.RadioButtonClear.UseVisualStyleBackColor = true;
            this.RadioButtonClear.Click += new System.EventHandler(this.ClearRadioButton_Click);
            // 
            // rbtnDrawPoint
            // 
            this.rbtnDrawPoint.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrawPoint.AutoSize = true;
            this.rbtnDrawPoint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbtnDrawPoint.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbtnDrawPoint.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.rbtnDrawPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrawPoint.Image = global::ThinkGeo.MapSuite.SiteSelection.Properties.Resources.drawPoint;
            this.rbtnDrawPoint.Location = new System.Drawing.Point(54, 6);
            this.rbtnDrawPoint.Margin = new System.Windows.Forms.Padding(5);
            this.rbtnDrawPoint.Name = "rbtnDrawPoint";
            this.rbtnDrawPoint.Size = new System.Drawing.Size(40, 40);
            this.rbtnDrawPoint.TabIndex = 1;
            this.rbtnDrawPoint.UseVisualStyleBackColor = true;
            this.rbtnDrawPoint.CheckedChanged += new System.EventHandler(this.ControlMapMode_CheckedChanged);
            // 
            // PanRadioButton
            // 
            this.PanRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.PanRadioButton.AutoSize = true;
            this.PanRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanRadioButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.PanRadioButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(222)))), ((int)(((byte)(81)))), ((int)(((byte)(11)))));
            this.PanRadioButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PanRadioButton.Image = global::ThinkGeo.MapSuite.SiteSelection.Properties.Resources.pan;
            this.PanRadioButton.Location = new System.Drawing.Point(9, 6);
            this.PanRadioButton.Margin = new System.Windows.Forms.Padding(5);
            this.PanRadioButton.Name = "PanRadioButton";
            this.PanRadioButton.Size = new System.Drawing.Size(40, 40);
            this.PanRadioButton.TabIndex = 0;
            this.PanRadioButton.UseVisualStyleBackColor = true;
            this.PanRadioButton.CheckedChanged += new System.EventHandler(this.ControlMapMode_CheckedChanged);
            // 
            // lblControlMode
            // 
            this.lblControlMode.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlMode.Location = new System.Drawing.Point(7, 122);
            this.lblControlMode.Name = "lblControlMode";
            this.lblControlMode.Size = new System.Drawing.Size(292, 69);
            this.lblControlMode.TabIndex = 3;
            this.lblControlMode.Text = "Place the center pin on the map to highlight points of interest within a specifie" +
    "d area around where you clicked:";
            // 
            // pnlBuildingType
            // 
            this.pnlBuildingType.Controls.Add(this.CandidatePoiTypesComboBox);
            this.pnlBuildingType.Controls.Add(this.CandidatePoiLayersComboBox);
            this.pnlBuildingType.Location = new System.Drawing.Point(11, 33);
            this.pnlBuildingType.Name = "pnlBuildingType";
            this.pnlBuildingType.Size = new System.Drawing.Size(288, 68);
            this.pnlBuildingType.TabIndex = 2;
            // 
            // CandidatePoiTypesComboBox
            // 
            this.CandidatePoiTypesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CandidatePoiTypesComboBox.FormattingEnabled = true;
            this.CandidatePoiTypesComboBox.Location = new System.Drawing.Point(9, 37);
            this.CandidatePoiTypesComboBox.Name = "CandidatePoiTypesComboBox";
            this.CandidatePoiTypesComboBox.Size = new System.Drawing.Size(272, 21);
            this.CandidatePoiTypesComboBox.TabIndex = 1;
            this.CandidatePoiTypesComboBox.SelectedIndexChanged += new System.EventHandler(this.ApplyQueryButton_Click);
            // 
            // CandidatePoiLayersComboBox
            // 
            this.CandidatePoiLayersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CandidatePoiLayersComboBox.FormattingEnabled = true;
            this.CandidatePoiLayersComboBox.Location = new System.Drawing.Point(9, 10);
            this.CandidatePoiLayersComboBox.Name = "CandidatePoiLayersComboBox";
            this.CandidatePoiLayersComboBox.Size = new System.Drawing.Size(272, 21);
            this.CandidatePoiLayersComboBox.TabIndex = 0;
            this.CandidatePoiLayersComboBox.SelectedIndexChanged += new System.EventHandler(this.CandidatePoiLayersComboBox_SelectedIndexChanged);
            // 
            // lblLeftSideBarTitle
            // 
            this.lblLeftSideBarTitle.AutoSize = true;
            this.lblLeftSideBarTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeftSideBarTitle.Location = new System.Drawing.Point(7, 9);
            this.lblLeftSideBarTitle.Name = "lblLeftSideBarTitle";
            this.lblLeftSideBarTitle.Size = new System.Drawing.Size(205, 21);
            this.lblLeftSideBarTitle.TabIndex = 1;
            this.lblLeftSideBarTitle.Text = "Highlight points of this type:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(674, 649);
            this.Controls.Add(this.map);
            this.Controls.Add(this.leftSideBarPanel);
            this.Controls.Add(this.stpFooter);
            this.Controls.Add(this.HeaderPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(690, 500);
            this.Name = "MainForm";
            this.Text = "Site Selection";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.HeaderPanel.ResumeLayout(false);
            this.HeaderPanel.PerformLayout();
            this.stpFooter.ResumeLayout(false);
            this.stpFooter.PerformLayout();
            this.leftSideBarPanel.ResumeLayout(false);
            this.leftSideBarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryResultItemsDataGridView)).EndInit();
            this.AreaTypePanel.ResumeLayout(false);
            this.AreaTypePanel.PerformLayout();
            this.BufferPanel.ResumeLayout(false);
            this.BufferPanel.PerformLayout();
            this.RoutePanel.ResumeLayout(false);
            this.RoutePanel.PerformLayout();
            this.pnlControlMode.ResumeLayout(false);
            this.pnlControlMode.PerformLayout();
            this.pnlBuildingType.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Label lblBigTitle;
        private System.Windows.Forms.Label lblSmallTitle;
        private System.Windows.Forms.StatusStrip stpFooter;
        private System.Windows.Forms.ToolStripStatusLabel FooterLocationYToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel FooterLocationXToolStripStatusLabel;
        private CollapsiblePanel leftSideBarPanel;
        private System.Windows.Forms.Label lblLeftSideBarTitle;
        private System.Windows.Forms.Panel pnlBuildingType;
        private System.Windows.Forms.ComboBox CandidatePoiTypesComboBox;
        private System.Windows.Forms.ComboBox CandidatePoiLayersComboBox;
        private System.Windows.Forms.Panel pnlControlMode;
        private System.Windows.Forms.RadioButton rbtnDrawPoint;
        private System.Windows.Forms.RadioButton PanRadioButton;
        private System.Windows.Forms.Label lblControlMode;
        private System.Windows.Forms.Panel AreaTypePanel;
        private System.Windows.Forms.Panel RoutePanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox RouteTimeTextBox;
        private System.Windows.Forms.RadioButton RouteRadioButton;
        private System.Windows.Forms.Label lblAreaType;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel BufferPanel;
        private System.Windows.Forms.TextBox DistanceValueTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DistanceUnitComboBox;
        private System.Windows.Forms.RadioButton BufferRadioButton;
        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.DataGridView QueryResultItemsDataGridView;
        private WinformsMap map;
        private System.Windows.Forms.RadioButton RadioButtonClear;
        private System.Windows.Forms.DataGridViewImageColumn cluZoomto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Location;
    }
}

