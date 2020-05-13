namespace CacheGenerator
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
            this.cmbStartZoomLevel = new System.Windows.Forms.ComboBox();
            this.cmbEndZoomLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxWatermark = new System.Windows.Forms.CheckBox();
            this.txtGridSize = new System.Windows.Forms.TextBox();
            this.checkBoxRestrict = new System.Windows.Forms.CheckBox();
            this.txtLowerRightX = new System.Windows.Forms.TextBox();
            this.btnSelectWatermark = new System.Windows.Forms.Button();
            this.btnSelectRestrictLayer = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWatermarkPath = new System.Windows.Forms.TextBox();
            this.txtRestrictionLayerPath = new System.Windows.Forms.TextBox();
            this.txtUpperLeftX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtThreadsCount = new System.Windows.Forms.TextBox();
            this.txtJpegQuality = new System.Windows.Forms.TextBox();
            this.cmbTileImageType = new System.Windows.Forms.ComboBox();
            this.lblJpegQuality = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelectCacheFolder = new System.Windows.Forms.Button();
            this.txtCacheFolder = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTileStatus = new System.Windows.Forms.Label();
            this.lblCurrentImageCount = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbStartZoomLevel
            // 
            this.cmbStartZoomLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartZoomLevel.FormattingEnabled = true;
            this.cmbStartZoomLevel.Items.AddRange(new object[] {
            "ZoomLevel01",
            "ZoomLevel02",
            "ZoomLevel03",
            "ZoomLevel04",
            "ZoomLevel05",
            "ZoomLevel06",
            "ZoomLevel07",
            "ZoomLevel08",
            "ZoomLevel09",
            "ZoomLevel10",
            "ZoomLevel11",
            "ZoomLevel12",
            "ZoomLevel13",
            "ZoomLevel14",
            "ZoomLevel15",
            "ZoomLevel16",
            "ZoomLevel17",
            "ZoomLevel18",
            "ZoomLevel19",
            "ZoomLevel20"});
            this.cmbStartZoomLevel.Location = new System.Drawing.Point(375, 19);
            this.cmbStartZoomLevel.Name = "cmbStartZoomLevel";
            this.cmbStartZoomLevel.Size = new System.Drawing.Size(147, 21);
            this.cmbStartZoomLevel.TabIndex = 1;
            // 
            // cmbEndZoomLevel
            // 
            this.cmbEndZoomLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndZoomLevel.FormattingEnabled = true;
            this.cmbEndZoomLevel.Items.AddRange(new object[] {
            "ZoomLevel01",
            "ZoomLevel02",
            "ZoomLevel03",
            "ZoomLevel04",
            "ZoomLevel05",
            "ZoomLevel06",
            "ZoomLevel07",
            "ZoomLevel08",
            "ZoomLevel09",
            "ZoomLevel10",
            "ZoomLevel11",
            "ZoomLevel12",
            "ZoomLevel13",
            "ZoomLevel14",
            "ZoomLevel15",
            "ZoomLevel16",
            "ZoomLevel17",
            "ZoomLevel18",
            "ZoomLevel19",
            "ZoomLevel20"});
            this.cmbEndZoomLevel.Location = new System.Drawing.Point(375, 42);
            this.cmbEndZoomLevel.Name = "cmbEndZoomLevel";
            this.cmbEndZoomLevel.Size = new System.Drawing.Size(147, 21);
            this.cmbEndZoomLevel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(333, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxWatermark);
            this.groupBox2.Controls.Add(this.txtGridSize);
            this.groupBox2.Controls.Add(this.checkBoxRestrict);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtLowerRightX);
            this.groupBox2.Controls.Add(this.btnSelectWatermark);
            this.groupBox2.Controls.Add(this.btnSelectRestrictLayer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtWatermarkPath);
            this.groupBox2.Controls.Add(this.txtRestrictionLayerPath);
            this.groupBox2.Controls.Add(this.txtUpperLeftX);
            this.groupBox2.Controls.Add(this.cmbStartZoomLevel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbEndZoomLevel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(528, 162);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input";
            // 
            // checkBoxWatermark
            // 
            this.checkBoxWatermark.AutoSize = true;
            this.checkBoxWatermark.Location = new System.Drawing.Point(14, 73);
            this.checkBoxWatermark.Name = "checkBoxWatermark";
            this.checkBoxWatermark.Size = new System.Drawing.Size(114, 17);
            this.checkBoxWatermark.TabIndex = 4;
            this.checkBoxWatermark.Text = "Embed Watermark";
            this.checkBoxWatermark.UseVisualStyleBackColor = true;
            this.checkBoxWatermark.CheckedChanged += new System.EventHandler(this.checkBoxWatermark_CheckedChanged);
            // 
            // txtGridSize
            // 
            this.txtGridSize.Location = new System.Drawing.Point(182, 129);
            this.txtGridSize.Name = "txtGridSize";
            this.txtGridSize.Size = new System.Drawing.Size(28, 20);
            this.txtGridSize.TabIndex = 10;
            this.txtGridSize.Text = "40";
            // 
            // checkBoxRestrict
            // 
            this.checkBoxRestrict.AutoSize = true;
            this.checkBoxRestrict.Location = new System.Drawing.Point(14, 96);
            this.checkBoxRestrict.Name = "checkBoxRestrict";
            this.checkBoxRestrict.Size = new System.Drawing.Size(112, 17);
            this.checkBoxRestrict.TabIndex = 7;
            this.checkBoxRestrict.Text = "Restrict to a Layer";
            this.checkBoxRestrict.UseVisualStyleBackColor = true;
            this.checkBoxRestrict.CheckedChanged += new System.EventHandler(this.checkBoxRestrict_CheckedChanged);
            // 
            // txtLowerRightX
            // 
            this.txtLowerRightX.Location = new System.Drawing.Point(121, 43);
            this.txtLowerRightX.Name = "txtLowerRightX";
            this.txtLowerRightX.Size = new System.Drawing.Size(198, 20);
            this.txtLowerRightX.TabIndex = 2;
            this.txtLowerRightX.Text = "180, -90";
            // 
            // btnSelectWatermark
            // 
            this.btnSelectWatermark.Location = new System.Drawing.Point(498, 70);
            this.btnSelectWatermark.Name = "btnSelectWatermark";
            this.btnSelectWatermark.Size = new System.Drawing.Size(24, 23);
            this.btnSelectWatermark.TabIndex = 6;
            this.btnSelectWatermark.Text = "...";
            this.btnSelectWatermark.UseVisualStyleBackColor = true;
            this.btnSelectWatermark.Click += new System.EventHandler(this.btnSelectWatermark_Click);
            // 
            // btnSelectRestrictLayer
            // 
            this.btnSelectRestrictLayer.Location = new System.Drawing.Point(498, 97);
            this.btnSelectRestrictLayer.Name = "btnSelectRestrictLayer";
            this.btnSelectRestrictLayer.Size = new System.Drawing.Size(24, 23);
            this.btnSelectRestrictLayer.TabIndex = 9;
            this.btnSelectRestrictLayer.Text = "...";
            this.btnSelectRestrictLayer.UseVisualStyleBackColor = true;
            this.btnSelectRestrictLayer.Click += new System.EventHandler(this.btnSelectRestrictionLayer_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Grid Size(0 is for no griding): ";
            // 
            // txtWatermarkPath
            // 
            this.txtWatermarkPath.Location = new System.Drawing.Point(138, 72);
            this.txtWatermarkPath.Name = "txtWatermarkPath";
            this.txtWatermarkPath.ReadOnly = true;
            this.txtWatermarkPath.Size = new System.Drawing.Size(354, 20);
            this.txtWatermarkPath.TabIndex = 5;
            this.txtWatermarkPath.Text = "..\\..\\App_Data\\thinkgeo_demo_watermark_256x256.png";
            // 
            // txtRestrictionLayerPath
            // 
            this.txtRestrictionLayerPath.Location = new System.Drawing.Point(138, 99);
            this.txtRestrictionLayerPath.Name = "txtRestrictionLayerPath";
            this.txtRestrictionLayerPath.ReadOnly = true;
            this.txtRestrictionLayerPath.Size = new System.Drawing.Size(354, 20);
            this.txtRestrictionLayerPath.TabIndex = 8;
            this.txtRestrictionLayerPath.Text = "..\\..\\App_Data\\restrictionLayer.shp";
            // 
            // txtUpperLeftX
            // 
            this.txtUpperLeftX.Location = new System.Drawing.Point(121, 20);
            this.txtUpperLeftX.Name = "txtUpperLeftX";
            this.txtUpperLeftX.Size = new System.Drawing.Size(198, 20);
            this.txtUpperLeftX.TabIndex = 0;
            this.txtUpperLeftX.Text = "-180, 90";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Lower Right Point:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Upper Left Point:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtThreadsCount);
            this.groupBox3.Controls.Add(this.txtJpegQuality);
            this.groupBox3.Controls.Add(this.cmbTileImageType);
            this.groupBox3.Controls.Add(this.lblJpegQuality);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnSelectCacheFolder);
            this.groupBox3.Controls.Add(this.txtCacheFolder);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(12, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 82);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output";
            // 
            // txtThreadsCount
            // 
            this.txtThreadsCount.Location = new System.Drawing.Point(487, 48);
            this.txtThreadsCount.Name = "txtThreadsCount";
            this.txtThreadsCount.Size = new System.Drawing.Size(35, 20);
            this.txtThreadsCount.TabIndex = 16;
            this.txtThreadsCount.Text = "4";
            // 
            // txtJpegQuality
            // 
            this.txtJpegQuality.Enabled = false;
            this.txtJpegQuality.Location = new System.Drawing.Point(293, 49);
            this.txtJpegQuality.Name = "txtJpegQuality";
            this.txtJpegQuality.Size = new System.Drawing.Size(53, 20);
            this.txtJpegQuality.TabIndex = 14;
            this.txtJpegQuality.Text = "80";
            // 
            // cmbTileImageType
            // 
            this.cmbTileImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTileImageType.Items.AddRange(new object[] {
            "JPEG",
            "PNG"});
            this.cmbTileImageType.Location = new System.Drawing.Point(103, 51);
            this.cmbTileImageType.Name = "cmbTileImageType";
            this.cmbTileImageType.Size = new System.Drawing.Size(91, 21);
            this.cmbTileImageType.TabIndex = 13;
            this.cmbTileImageType.SelectedIndexChanged += new System.EventHandler(this.cmbTileImageType_SelectedIndexChanged);
            // 
            // lblJpegQuality
            // 
            this.lblJpegQuality.AutoSize = true;
            this.lblJpegQuality.Location = new System.Drawing.Point(212, 52);
            this.lblJpegQuality.Name = "lblJpegQuality";
            this.lblJpegQuality.Size = new System.Drawing.Size(75, 13);
            this.lblJpegQuality.TabIndex = 6;
            this.lblJpegQuality.Text = "JPEG Quality: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Image Format:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(401, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Threads Count:";
            // 
            // btnSelectCacheFolder
            // 
            this.btnSelectCacheFolder.Location = new System.Drawing.Point(498, 13);
            this.btnSelectCacheFolder.Name = "btnSelectCacheFolder";
            this.btnSelectCacheFolder.Size = new System.Drawing.Size(24, 23);
            this.btnSelectCacheFolder.TabIndex = 12;
            this.btnSelectCacheFolder.Text = "...";
            this.btnSelectCacheFolder.UseVisualStyleBackColor = true;
            this.btnSelectCacheFolder.Click += new System.EventHandler(this.btnSelectCacheFolder_Click);
            // 
            // txtCacheFolder
            // 
            this.txtCacheFolder.Location = new System.Drawing.Point(103, 16);
            this.txtCacheFolder.Name = "txtCacheFolder";
            this.txtCacheFolder.Size = new System.Drawing.Size(389, 20);
            this.txtCacheFolder.TabIndex = 11;
            this.txtCacheFolder.Text = "..\\..\\..\\CacheFolder\\DecimalDegree";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Cache Folder";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(303, 340);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 23);
            this.btnPreview.TabIndex = 19;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(465, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Quit";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(384, 340);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 20;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTileStatus);
            this.groupBox1.Controls.Add(this.lblCurrentImageCount);
            this.groupBox1.Controls.Add(this.lblTotalTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(528, 66);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Progress";
            // 
            // lblTileStatus
            // 
            this.lblTileStatus.AutoSize = true;
            this.lblTileStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTileStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTileStatus.Location = new System.Drawing.Point(145, 45);
            this.lblTileStatus.Name = "lblTileStatus";
            this.lblTileStatus.Size = new System.Drawing.Size(0, 13);
            this.lblTileStatus.TabIndex = 8;
            // 
            // lblCurrentImageCount
            // 
            this.lblCurrentImageCount.AutoSize = true;
            this.lblCurrentImageCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentImageCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentImageCount.Location = new System.Drawing.Point(6, 45);
            this.lblCurrentImageCount.Name = "lblCurrentImageCount";
            this.lblCurrentImageCount.Size = new System.Drawing.Size(43, 13);
            this.lblCurrentImageCount.TabIndex = 8;
            this.lblCurrentImageCount.Text = "Status";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTotalTime.Location = new System.Drawing.Point(7, 22);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(67, 13);
            this.lblTotalTime.TabIndex = 7;
            this.lblTotalTime.Text = "Total Time";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 374);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cache Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.CachedImagesGenerateroForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStartZoomLevel;
        private System.Windows.Forms.ComboBox cmbEndZoomLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLowerRightX;
        private System.Windows.Forms.TextBox txtUpperLeftX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSelectCacheFolder;
        private System.Windows.Forms.TextBox txtCacheFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTileImageType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtJpegQuality;
        private System.Windows.Forms.Label lblJpegQuality;
        private System.Windows.Forms.CheckBox checkBoxRestrict;
        private System.Windows.Forms.Button btnSelectRestrictLayer;
        private System.Windows.Forms.TextBox txtRestrictionLayerPath;
        private System.Windows.Forms.CheckBox checkBoxWatermark;
        private System.Windows.Forms.Button btnSelectWatermark;
        private System.Windows.Forms.TextBox txtWatermarkPath;
        private System.Windows.Forms.TextBox txtGridSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtThreadsCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCurrentImageCount;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Label lblTileStatus;
    }
}

