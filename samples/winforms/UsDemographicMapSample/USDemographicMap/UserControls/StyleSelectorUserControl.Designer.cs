namespace ThinkGeo.MapSuite.USDemographicMap
{
    partial class StyleSelectorUserControl
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
            this.picDotDensity = new System.Windows.Forms.PictureBox();
            this.picThematic = new System.Windows.Forms.PictureBox();
            this.picValueCircle = new System.Windows.Forms.PictureBox();
            this.chkIsSelected = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDotDensity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThematic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picValueCircle)).BeginInit();
            this.SuspendLayout();
            // 
            // pctDotDensity
            // 
            this.picDotDensity.BackColor = System.Drawing.Color.Transparent;
            this.picDotDensity.Location = new System.Drawing.Point(187, 5);
            this.picDotDensity.Margin = new System.Windows.Forms.Padding(5);
            this.picDotDensity.Name = "pctDotDensity";
            this.picDotDensity.Size = new System.Drawing.Size(20, 20);
            this.picDotDensity.TabIndex = 0;
            this.picDotDensity.TabStop = false;
            this.picDotDensity.Click += new System.EventHandler(this.pictureBox_Click);
            this.picDotDensity.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.picDotDensity.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // pctThematic
            // 
            this.picThematic.BackColor = System.Drawing.Color.Transparent;
            this.picThematic.Location = new System.Drawing.Point(157, 5);
            this.picThematic.Margin = new System.Windows.Forms.Padding(5);
            this.picThematic.Name = "pctThematic";
            this.picThematic.Size = new System.Drawing.Size(20, 20);
            this.picThematic.TabIndex = 1;
            this.picThematic.TabStop = false;
            this.picThematic.Click += new System.EventHandler(this.pictureBox_Click);
            this.picThematic.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.picThematic.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // pctValueCircle
            // 
            this.picValueCircle.BackColor = System.Drawing.Color.Transparent;
            this.picValueCircle.Location = new System.Drawing.Point(127, 5);
            this.picValueCircle.Margin = new System.Windows.Forms.Padding(5);
            this.picValueCircle.Name = "pctValueCircle";
            this.picValueCircle.Size = new System.Drawing.Size(20, 20);
            this.picValueCircle.TabIndex = 2;
            this.picValueCircle.TabStop = false;
            this.picValueCircle.Click += new System.EventHandler(this.pictureBox_Click);
            this.picValueCircle.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.picValueCircle.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // chbIsSelected
            // 
            this.chkIsSelected.AutoSize = true;
            this.chkIsSelected.Location = new System.Drawing.Point(215, 8);
            this.chkIsSelected.Name = "chbIsSelected";
            this.chkIsSelected.Size = new System.Drawing.Size(15, 14);
            this.chkIsSelected.TabIndex = 3;
            this.chkIsSelected.UseVisualStyleBackColor = true;
            this.chkIsSelected.CheckedChanged += new System.EventHandler(this.SelectedCheckBox_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(-3, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(41, 15);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "label1";
            // 
            // StyleSelectorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.chkIsSelected);
            this.Controls.Add(this.picValueCircle);
            this.Controls.Add(this.picThematic);
            this.Controls.Add(this.picDotDensity);
            this.Margin = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.Name = "StyleSelectorUserControl";
            this.Size = new System.Drawing.Size(230, 30);
            ((System.ComponentModel.ISupportInitialize)(this.picDotDensity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThematic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picValueCircle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picDotDensity;
        private System.Windows.Forms.PictureBox picThematic;
        private System.Windows.Forms.PictureBox picValueCircle;
        private System.Windows.Forms.CheckBox chkIsSelected;
        private System.Windows.Forms.Label lblTitle;
    }
}
