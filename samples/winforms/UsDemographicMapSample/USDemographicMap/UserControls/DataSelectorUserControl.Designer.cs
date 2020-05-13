namespace ThinkGeo.MapSuite.USDemographicMap
{
    partial class DataSelectorUserControl
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
            this.picDataImage = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picPie = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDataImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pctCategoryImage
            // 
            this.picDataImage.BackColor = System.Drawing.Color.Transparent;
            this.picDataImage.Location = new System.Drawing.Point(2, 11);
            this.picDataImage.Name = "pctCategoryImage";
            this.picDataImage.Size = new System.Drawing.Size(32, 32);
            this.picDataImage.TabIndex = 0;
            this.picDataImage.TabStop = false;
            this.picDataImage.Click += new System.EventHandler(this.SubControls_Click);
            this.picDataImage.MouseEnter += new System.EventHandler(this.DataCategoryUserControl_MouseEnter);
            this.picDataImage.MouseLeave += new System.EventHandler(this.DataCategoryUserControl_MouseLeave);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(34, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(50, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "label1";
            this.lblTitle.Click += new System.EventHandler(this.SubControls_Click);
            this.lblTitle.MouseEnter += new System.EventHandler(this.DataCategoryUserControl_MouseEnter);
            this.lblTitle.MouseLeave += new System.EventHandler(this.DataCategoryUserControl_MouseLeave);
            // 
            // pctPie
            // 
            this.picPie.BackColor = System.Drawing.Color.Transparent;
            this.picPie.Location = new System.Drawing.Point(230, 15);
            this.picPie.Name = "pctPie";
            this.picPie.Size = new System.Drawing.Size(20, 20);
            this.picPie.TabIndex = 2;
            this.picPie.TabStop = false;
            this.picPie.EnabledChanged += new System.EventHandler(this.PictureBoxPie_EnabledChanged);
            this.picPie.Click += new System.EventHandler(this.PieChartLegendItem_Click);
            this.picPie.MouseEnter += new System.EventHandler(this.PieChartLegendItem_MouseEnter);
            this.picPie.MouseLeave += new System.EventHandler(this.PieChartLegendItem_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Location = new System.Drawing.Point(0, 44);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 1);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // DataCategoryUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.picPie);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picDataImage);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.Name = "DataCategoryUserControl";
            this.Size = new System.Drawing.Size(255, 45);
            this.MouseEnter += new System.EventHandler(this.DataCategoryUserControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DataCategoryUserControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.picDataImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDataImage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picPie;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
