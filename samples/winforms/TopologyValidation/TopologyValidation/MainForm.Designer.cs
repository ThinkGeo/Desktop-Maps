namespace TopologyValidation
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTrackNormal = new System.Windows.Forms.Button();
            this.btnTrackPoint = new System.Windows.Forms.Button();
            this.btnTrackCoveredLine = new System.Windows.Forms.Button();
            this.btnTrackLine = new System.Windows.Forms.Button();
            this.btnTrackCoveredPolygon = new System.Windows.Forms.Button();
            this.btnTrackPolygon = new System.Windows.Forms.Button();
            this.btnTrackDelete = new System.Windows.Forms.Button();
            this.currentBoundingBox = new System.Windows.Forms.Label();
            this.listboxNames = new System.Windows.Forms.ListBox();
            this.txtCaseDescription = new System.Windows.Forms.TextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.banner1 = new Banner();
            this.cmbTopologyRules = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnRunTestCase = new System.Windows.Forms.Button();
            this.btnSaveTestCase = new System.Windows.Forms.Button();
            this.btnDeleteTestCase = new System.Windows.Forms.Button();
            this.btnSaveTestCaseInCurrentExtent = new System.Windows.Forms.Button();
            this.btnImportExportWKT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.footer1 = new Footer();
            this.groupBox1.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnTrackNormal);
            this.groupBox1.Controls.Add(this.btnTrackPoint);
            this.groupBox1.Controls.Add(this.btnTrackCoveredLine);
            this.groupBox1.Controls.Add(this.btnTrackLine);
            this.groupBox1.Controls.Add(this.btnTrackCoveredPolygon);
            this.groupBox1.Controls.Add(this.btnTrackPolygon);
            this.groupBox1.Controls.Add(this.btnTrackDelete);
            this.groupBox1.Location = new System.Drawing.Point(518, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 56);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            // 
            // btnTrackNormal
            // 
            this.btnTrackNormal.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackNormal.Image = global::TopologyValidation.Properties.Resources.Normal;
            this.btnTrackNormal.Location = new System.Drawing.Point(6, 19);
            this.btnTrackNormal.Name = "btnTrackNormal";
            this.btnTrackNormal.Size = new System.Drawing.Size(30, 30);
            this.btnTrackNormal.TabIndex = 0;
            this.btnTrackNormal.UseVisualStyleBackColor = false;
            this.btnTrackNormal.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackPoint
            // 
            this.btnTrackPoint.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackPoint.Image = global::TopologyValidation.Properties.Resources.Point;
            this.btnTrackPoint.Location = new System.Drawing.Point(42, 19);
            this.btnTrackPoint.Name = "btnTrackPoint";
            this.btnTrackPoint.Size = new System.Drawing.Size(30, 30);
            this.btnTrackPoint.TabIndex = 1;
            this.btnTrackPoint.UseVisualStyleBackColor = false;
            this.btnTrackPoint.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackCoveredLine
            // 
            this.btnTrackCoveredLine.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackCoveredLine.Image = global::TopologyValidation.Properties.Resources.Line;
            this.btnTrackCoveredLine.Location = new System.Drawing.Point(114, 19);
            this.btnTrackCoveredLine.Name = "btnTrackCoveredLine";
            this.btnTrackCoveredLine.Size = new System.Drawing.Size(30, 30);
            this.btnTrackCoveredLine.TabIndex = 2;
            this.btnTrackCoveredLine.UseVisualStyleBackColor = false;
            this.btnTrackCoveredLine.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackLine
            // 
            this.btnTrackLine.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackLine.Image = global::TopologyValidation.Properties.Resources.Line;
            this.btnTrackLine.Location = new System.Drawing.Point(78, 19);
            this.btnTrackLine.Name = "btnTrackLine";
            this.btnTrackLine.Size = new System.Drawing.Size(30, 30);
            this.btnTrackLine.TabIndex = 2;
            this.btnTrackLine.UseVisualStyleBackColor = false;
            this.btnTrackLine.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackCoveredPolygon
            // 
            this.btnTrackCoveredPolygon.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackCoveredPolygon.Image = global::TopologyValidation.Properties.Resources.Polygon;
            this.btnTrackCoveredPolygon.Location = new System.Drawing.Point(186, 19);
            this.btnTrackCoveredPolygon.Name = "btnTrackCoveredPolygon";
            this.btnTrackCoveredPolygon.Size = new System.Drawing.Size(30, 30);
            this.btnTrackCoveredPolygon.TabIndex = 5;
            this.btnTrackCoveredPolygon.UseVisualStyleBackColor = false;
            this.btnTrackCoveredPolygon.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackPolygon
            // 
            this.btnTrackPolygon.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackPolygon.Image = global::TopologyValidation.Properties.Resources.Polygon;
            this.btnTrackPolygon.Location = new System.Drawing.Point(150, 18);
            this.btnTrackPolygon.Name = "btnTrackPolygon";
            this.btnTrackPolygon.Size = new System.Drawing.Size(30, 30);
            this.btnTrackPolygon.TabIndex = 5;
            this.btnTrackPolygon.UseVisualStyleBackColor = false;
            this.btnTrackPolygon.Click += new System.EventHandler(this.TrackButton_Click);
            // 
            // btnTrackDelete
            // 
            this.btnTrackDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackDelete.Image = global::TopologyValidation.Properties.Resources.Delete;
            this.btnTrackDelete.Location = new System.Drawing.Point(222, 19);
            this.btnTrackDelete.Name = "btnTrackDelete";
            this.btnTrackDelete.Size = new System.Drawing.Size(30, 30);
            this.btnTrackDelete.TabIndex = 9;
            this.btnTrackDelete.UseVisualStyleBackColor = false;
            this.btnTrackDelete.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // currentBoundingBox
            // 
            this.currentBoundingBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.currentBoundingBox.AutoSize = true;
            this.currentBoundingBox.Location = new System.Drawing.Point(12, 722);
            this.currentBoundingBox.Name = "currentBoundingBox";
            this.currentBoundingBox.Size = new System.Drawing.Size(0, 13);
            this.currentBoundingBox.TabIndex = 7;
            // 
            // listboxNames
            // 
            this.listboxNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listboxNames.FormattingEnabled = true;
            this.listboxNames.Location = new System.Drawing.Point(3, 83);
            this.listboxNames.Name = "listboxNames";
            this.listboxNames.Size = new System.Drawing.Size(220, 368);
            this.listboxNames.TabIndex = 13;
            this.listboxNames.SelectedIndexChanged += new System.EventHandler(this.lbTestCases_SelectedIndexChanged);
            // 
            // txtCaseDescription
            // 
            this.txtCaseDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaseDescription.Location = new System.Drawing.Point(3, 16);
            this.txtCaseDescription.Multiline = true;
            this.txtCaseDescription.Name = "txtCaseDescription";
            this.txtCaseDescription.Size = new System.Drawing.Size(220, 146);
            this.txtCaseDescription.TabIndex = 12;
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.Controls.Add(this.banner1);
            this.pnlTop.Location = new System.Drawing.Point(1, 1);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1012, 81);
            this.pnlTop.TabIndex = 16;
            // 
            // banner1
            // 
            this.banner1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.banner1.Location = new System.Drawing.Point(0, 0);
            this.banner1.Name = "banner1";
            this.banner1.Size = new System.Drawing.Size(1013, 84);
            this.banner1.TabIndex = 0;
            // 
            // cmbTopologyRules
            // 
            this.cmbTopologyRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTopologyRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTopologyRules.FormattingEnabled = true;
            this.cmbTopologyRules.Items.AddRange(new object[] {
            "LinesEndPointMustBeCoveredByPoints",
            "LinesMustBeCoveredByBoundaryOfPolygons",
            "LinesMustBeCoveredByFeatureClassOfLines",
            "LinesMustBeLargerThanClusterTolerance",
            "LinesMustBeSinglePart",
            "LinesMustNotHaveDangles",
            "LinesMustNotHavePseudonodes",
            "LinesMustNotIntersectOrTouchInterior",
            "LinesMustNotIntersect",
            "LinesMustNotOverlap",
            "LinesMustNotOverlapWithLines",
            "LinesMustNotSelfIntersect",
            "LinesMustNotSelfOverlap",
            "PointsMustBeCoveredByBoundaryOfPolygons",
            "PointsMustBeCoveredByEndPointOfLines",
            "PointsMustBeProperlyInsidePolygons",
            "PointsMustBeCoveredByLines",
            "PolygonsBoundaryMustBeCoveredByBoundaryOfPolygons",
            "PolygonsBoundaryMustBeCoveredByLines",
            "PolygonsMustBeCoveredByFeatureClassOfPolygons",
            "PolygonsMustBeCoveredByPolygons",
            "PolygonsMustBeLargerThanClusterTolerance",
            "PolygonsMustContainPoint",
            "PolygonsMustCoverEachOther",
            "PolygonsMustNotHaveGaps",
            "PolygonsMustNotOverlap",
            "PolygonsMustNotOverlapWithPolygons",
            "ValidateShapes"});
            this.cmbTopologyRules.Location = new System.Drawing.Point(0, 0);
            this.cmbTopologyRules.Name = "cmbTopologyRules";
            this.cmbTopologyRules.Size = new System.Drawing.Size(226, 21);
            this.cmbTopologyRules.TabIndex = 18;
            this.cmbTopologyRules.SelectedIndexChanged += new System.EventHandler(this.cmbTopologyRules_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(1, 88);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.winformsMap1);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 631);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 20;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listboxNames);
            this.splitContainer2.Panel1.Controls.Add(this.cmbTopologyRules);
            this.splitContainer2.Panel1.Controls.Add(this.btnRunTestCase);
            this.splitContainer2.Panel1.Controls.Add(this.btnSaveTestCase);
            this.splitContainer2.Panel1.Controls.Add(this.btnDeleteTestCase);
            this.splitContainer2.Panel1.Controls.Add(this.btnSaveTestCaseInCurrentExtent);
            this.splitContainer2.Panel1.Controls.Add(this.btnImportExportWKT);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtCaseDescription);
            this.splitContainer2.Panel2.Controls.Add(this.label1);
            this.splitContainer2.Size = new System.Drawing.Size(226, 625);
            this.splitContainer2.SplitterDistance = 459;
            this.splitContainer2.TabIndex = 21;
            // 
            // btnRunTestCase
            // 
            this.btnRunTestCase.Image = global::TopologyValidation.Properties.Resources.btn_run_test_case;
            this.btnRunTestCase.Location = new System.Drawing.Point(2, 27);
            this.btnRunTestCase.Name = "btnRunTestCase";
            this.btnRunTestCase.Size = new System.Drawing.Size(40, 40);
            this.btnRunTestCase.TabIndex = 19;
            this.btnRunTestCase.UseVisualStyleBackColor = true;
            this.btnRunTestCase.Click += new System.EventHandler(this.btnTopologyResult_Click);
            // 
            // btnSaveTestCase
            // 
            this.btnSaveTestCase.Image = global::TopologyValidation.Properties.Resources.btn_save_test_case;
            this.btnSaveTestCase.Location = new System.Drawing.Point(94, 27);
            this.btnSaveTestCase.Name = "btnSaveTestCase";
            this.btnSaveTestCase.Size = new System.Drawing.Size(40, 40);
            this.btnSaveTestCase.TabIndex = 19;
            this.btnSaveTestCase.UseVisualStyleBackColor = true;
            this.btnSaveTestCase.Click += new System.EventHandler(this.btnSaveToXML_Click);
            // 
            // btnDeleteTestCase
            // 
            this.btnDeleteTestCase.Image = global::TopologyValidation.Properties.Resources.btn_delete_test_case;
            this.btnDeleteTestCase.Location = new System.Drawing.Point(186, 27);
            this.btnDeleteTestCase.Name = "btnDeleteTestCase";
            this.btnDeleteTestCase.Size = new System.Drawing.Size(40, 40);
            this.btnDeleteTestCase.TabIndex = 19;
            this.btnDeleteTestCase.UseVisualStyleBackColor = true;
            this.btnDeleteTestCase.Click += new System.EventHandler(this.btnDeleteTestCase_Click);
            // 
            // btnSaveTestCaseInCurrentExtent
            // 
            this.btnSaveTestCaseInCurrentExtent.Image = global::TopologyValidation.Properties.Resources.btn_save_test_case_extent;
            this.btnSaveTestCaseInCurrentExtent.Location = new System.Drawing.Point(140, 27);
            this.btnSaveTestCaseInCurrentExtent.Name = "btnSaveTestCaseInCurrentExtent";
            this.btnSaveTestCaseInCurrentExtent.Size = new System.Drawing.Size(40, 40);
            this.btnSaveTestCaseInCurrentExtent.TabIndex = 19;
            this.btnSaveTestCaseInCurrentExtent.UseVisualStyleBackColor = true;
            this.btnSaveTestCaseInCurrentExtent.Click += new System.EventHandler(this.btnSaveCaseInCurrentExtent_Click);
            // 
            // btnImportExportWKT
            // 
            this.btnImportExportWKT.Image = global::TopologyValidation.Properties.Resources.btn_import_export_wkt;
            this.btnImportExportWKT.Location = new System.Drawing.Point(48, 27);
            this.btnImportExportWKT.Name = "btnImportExportWKT";
            this.btnImportExportWKT.Size = new System.Drawing.Size(40, 40);
            this.btnImportExportWKT.TabIndex = 19;
            this.btnImportExportWKT.UseVisualStyleBackColor = true;
            this.btnImportExportWKT.Click += new System.EventHandler(this.btnImportSingleWKT_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Case Description:";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790D;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(777, 631);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // footer1
            // 
            this.footer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.footer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.footer1.Location = new System.Drawing.Point(1, 716);
            this.footer1.Name = "footer1";
            this.footer1.Size = new System.Drawing.Size(1014, 32);
            this.footer1.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 744);
            this.Controls.Add(this.footer1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.currentBoundingBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Topology Validation";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTrackNormal;
        private System.Windows.Forms.Button btnTrackPoint;
        private System.Windows.Forms.Button btnTrackLine;
        private System.Windows.Forms.Button btnTrackPolygon;
        private System.Windows.Forms.Button btnTrackDelete;
        private System.Windows.Forms.Label currentBoundingBox;
        private System.Windows.Forms.ListBox listboxNames;
        private System.Windows.Forms.TextBox txtCaseDescription;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ComboBox cmbTopologyRules;
        private System.Windows.Forms.Button btnRunTestCase;
        private System.Windows.Forms.Button btnImportExportWKT;
        private System.Windows.Forms.Button btnSaveTestCase;
        private System.Windows.Forms.Button btnSaveTestCaseInCurrentExtent;
        private System.Windows.Forms.Button btnDeleteTestCase;
        private System.Windows.Forms.Button btnTrackCoveredPolygon;
        private System.Windows.Forms.Button btnTrackCoveredLine;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private Banner banner1;
        private Footer footer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}