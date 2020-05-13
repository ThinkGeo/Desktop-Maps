namespace DynamicTrackShapes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblPerimeter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTrackPolygon = new System.Windows.Forms.Button();
            this.btnTrackCircle = new System.Windows.Forms.Button();
            this.groupBoxInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Drawing.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(12, 12);
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Shapes.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(803, 494);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Controls.Add(this.lblArea);
            this.groupBoxInfo.Controls.Add(this.lblPerimeter);
            this.groupBoxInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.Location = new System.Drawing.Point(566, 299);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(144, 64);
            this.groupBoxInfo.TabIndex = 2;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Shape Info";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Area:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Perim:";
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(46, 39);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(89, 13);
            this.lblArea.TabIndex = 3;
            this.lblArea.Text = "Area";
            // 
            // lblPerimeter
            // 
            this.lblPerimeter.Location = new System.Drawing.Point(46, 20);
            this.lblPerimeter.Name = "lblPerimeter";
            this.lblPerimeter.Size = new System.Drawing.Size(90, 13);
            this.lblPerimeter.TabIndex = 2;
            this.lblPerimeter.Text = "Perimeter";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnTrackPolygon);
            this.groupBox1.Controls.Add(this.btnTrackCircle);
            this.groupBox1.Location = new System.Drawing.Point(716, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 60);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            // 
            // btnTrackPolygon
            // 
            this.btnTrackPolygon.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackPolygon.Image = ((System.Drawing.Image)(resources.GetObject("btnTrackPolygon.Image")));
            this.btnTrackPolygon.Location = new System.Drawing.Point(16, 19);
            this.btnTrackPolygon.Name = "btnTrackPolygon";
            this.btnTrackPolygon.Size = new System.Drawing.Size(30, 30);
            this.btnTrackPolygon.TabIndex = 5;
            this.btnTrackPolygon.UseVisualStyleBackColor = false;
            this.btnTrackPolygon.Click += new System.EventHandler(this.btnTrackPolygon_Click);
            // 
            // btnTrackCircle
            // 
            this.btnTrackCircle.BackColor = System.Drawing.SystemColors.Control;
            this.btnTrackCircle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTrackCircle.BackgroundImage")));
            this.btnTrackCircle.Location = new System.Drawing.Point(52, 19);
            this.btnTrackCircle.Name = "btnTrackCircle";
            this.btnTrackCircle.Size = new System.Drawing.Size(30, 30);
            this.btnTrackCircle.TabIndex = 6;
            this.btnTrackCircle.UseVisualStyleBackColor = false;
            this.btnTrackCircle.Click += new System.EventHandler(this.btnTrackCircle_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 547);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.winformsMap1);
            this.Name = "Form1";
            this.Text = "Dynamic TrackShapes";
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblPerimeter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTrackPolygon;
        private System.Windows.Forms.Button btnTrackCircle;
    }
}