namespace HowDoISamples
{
    partial class BuildDbfMatchingPlugin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbInstructions = new System.Windows.Forms.GroupBox();
            this.btnBuildIndex = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSourceText = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbInstructions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // gbInstructions
            // 
            this.gbInstructions.Controls.Add(this.btnBuildIndex);
            this.gbInstructions.Controls.Add(this.label4);
            this.gbInstructions.Location = new System.Drawing.Point(546, 5);
            this.gbInstructions.Name = "gbInstructions";
            this.gbInstructions.Size = new System.Drawing.Size(191, 99);
            this.gbInstructions.TabIndex = 22;
            this.gbInstructions.TabStop = false;
            this.gbInstructions.Text = "Instructions";
            // 
            // btnBuildIndex
            // 
            this.btnBuildIndex.Location = new System.Drawing.Point(46, 65);
            this.btnBuildIndex.Name = "btnBuildIndex";
            this.btnBuildIndex.Size = new System.Drawing.Size(117, 23);
            this.btnBuildIndex.TabIndex = 0;
            this.btnBuildIndex.Text = "Build Cntry02 Index";
            this.btnBuildIndex.UseVisualStyleBackColor = true;
            this.btnBuildIndex.Click += new System.EventHandler(this.btnBuildIndex_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 39);
            this.label4.TabIndex = 18;
            this.label4.Text = "Select or type in a country. We\r\nwill match that county name in the\r\ncreated inde" +
                "x file.";
            // 
            // cboSourceText
            // 
            this.cboSourceText.FormattingEnabled = true;
            this.cboSourceText.Items.AddRange(new object[] {
            "China",
            "Australia",
            "Mexico",
            "France",
            "Germany",
            "Italy",
            "Poland",
            "Argentina"});
            this.cboSourceText.Location = new System.Drawing.Point(6, 19);
            this.cboSourceText.Name = "cboSourceText";
            this.cboSourceText.Size = new System.Drawing.Size(96, 21);
            this.cboSourceText.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(108, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.HorizontalScrollbar = true;
            this.lstResult.Location = new System.Drawing.Point(6, 47);
            this.lstResult.Name = "lstResult";
            this.lstResult.ScrollAlwaysVisible = true;
            this.lstResult.Size = new System.Drawing.Size(178, 108);
            this.lstResult.TabIndex = 21;
            this.lstResult.SelectedIndexChanged += new System.EventHandler(this.lstResult_SelectedIndexChanged);
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(3, 3);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.DesktopEdition.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(537, 522);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 20;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewDetail);
            this.groupBox1.Controls.Add(this.cboSourceText);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.lstResult);
            this.groupBox1.Location = new System.Drawing.Point(547, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 415);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geocoder";
            // 
            // dataGridViewDetail
            // 
            this.dataGridViewDetail.AllowUserToAddRows = false;
            this.dataGridViewDetail.AllowUserToDeleteRows = false;
            this.dataGridViewDetail.AllowUserToResizeRows = false;
            this.dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnName,
            this.columnValue});
            this.dataGridViewDetail.Location = new System.Drawing.Point(6, 161);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.ReadOnly = true;
            this.dataGridViewDetail.RowHeadersVisible = false;
            this.dataGridViewDetail.Size = new System.Drawing.Size(178, 248);
            this.dataGridViewDetail.TabIndex = 22;
            // 
            // columnName
            // 
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            // 
            // columnValue
            // 
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.ReadOnly = true;
            // 
            // BuildDbfMatchingPlugin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbInstructions);
            this.Controls.Add(this.winformsMap1);
            this.Name = "BuildDbfMatchingPlugin";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.BuildIndexFileFromCntry02_Load);
            this.gbInstructions.ResumeLayout(false);
            this.gbInstructions.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInstructions;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboSourceText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstResult;
        private ThinkGeo.MapSuite.DesktopEdition.WinformsMap winformsMap1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuildIndex;
        private System.Windows.Forms.DataGridView dataGridViewDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
    }
}
