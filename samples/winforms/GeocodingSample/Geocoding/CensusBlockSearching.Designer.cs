﻿using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;

namespace Geocoding
{
    partial class CensusBlockSearching
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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.dataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboSourceText = new System.Windows.Forms.ComboBox();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(3, 4);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(513, 522);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 23;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
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
            this.dataGridViewDetail.Location = new System.Drawing.Point(522, 142);
            this.dataGridViewDetail.Name = "dataGridViewDetail";
            this.dataGridViewDetail.ReadOnly = true;
            this.dataGridViewDetail.RowHeadersVisible = false;
            this.dataGridViewDetail.Size = new System.Drawing.Size(215, 384);
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
            // cboSourceText
            // 
            this.cboSourceText.FormattingEnabled = true;
            this.cboSourceText.Items.AddRange(new object[] {
            "CensusBlock:1000:010900",
            "CensusBlock:1010:827805",
            "CensusBlock:2110:824104",
            "CensusBlock:3009:824503",
            "CensusBlock:4015:830004",
            "CensusBlock:5004:100700",
            "CensusBlock:6013:804607",
            "CensusBlock:7002:530200",
            "CensusBlock:8005:650300"});
            this.cboSourceText.Location = new System.Drawing.Point(522, 3);
            this.cboSourceText.Name = "cboSourceText";
            this.cboSourceText.Size = new System.Drawing.Size(134, 21);
            this.cboSourceText.TabIndex = 21;
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.HorizontalScrollbar = true;
            this.lstResult.Location = new System.Drawing.Point(522, 29);
            this.lstResult.Name = "lstResult";
            this.lstResult.ScrollAlwaysVisible = true;
            this.lstResult.Size = new System.Drawing.Size(215, 108);
            this.lstResult.TabIndex = 20;
            this.lstResult.SelectedIndexChanged += new System.EventHandler(this.lstResult_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(662, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CensusBlockSearching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.dataGridViewDetail);
            this.Controls.Add(this.cboSourceText);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.btnSearch);
            this.Name = "CensusBlockSearching";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.CensusBlockSearching_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.DataGridView dataGridViewDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
        private System.Windows.Forms.ComboBox cboSourceText;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnSearch;
    }
}