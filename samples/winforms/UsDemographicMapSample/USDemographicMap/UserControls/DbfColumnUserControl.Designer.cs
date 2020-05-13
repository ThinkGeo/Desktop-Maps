namespace ThinkGeo.MapSuite.USDemographicMap
{
    partial class DbfColumnUserControl
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
            this.DotDensityPictureBox = new System.Windows.Forms.PictureBox();
            this.ThematicPictureBox = new System.Windows.Forms.PictureBox();
            this.ValueCirclePictureBox = new System.Windows.Forms.PictureBox();
            this.IsSelectedCheckBox = new System.Windows.Forms.CheckBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DotDensityPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThematicPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueCirclePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pctDotDensity
            // 
            this.DotDensityPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.DotDensityPictureBox.Location = new System.Drawing.Point(187, 5);
            this.DotDensityPictureBox.Margin = new System.Windows.Forms.Padding(5);
            this.DotDensityPictureBox.Name = "pctDotDensity";
            this.DotDensityPictureBox.Size = new System.Drawing.Size(20, 20);
            this.DotDensityPictureBox.TabIndex = 0;
            this.DotDensityPictureBox.TabStop = false;
            this.DotDensityPictureBox.EnabledChanged += new System.EventHandler(this.DbfColumnLegendItem_EnabledChanged);
            this.DotDensityPictureBox.Click += new System.EventHandler(this.DbfColumnLegendItem_Click);
            this.DotDensityPictureBox.MouseEnter += new System.EventHandler(this.DbfColumnLegendItem_MouseEnter);
            this.DotDensityPictureBox.MouseLeave += new System.EventHandler(this.DbfColumnLegendItem_MouseLeave);
            // 
            // pctThematic
            // 
            this.ThematicPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ThematicPictureBox.Location = new System.Drawing.Point(157, 5);
            this.ThematicPictureBox.Margin = new System.Windows.Forms.Padding(5);
            this.ThematicPictureBox.Name = "pctThematic";
            this.ThematicPictureBox.Size = new System.Drawing.Size(20, 20);
            this.ThematicPictureBox.TabIndex = 1;
            this.ThematicPictureBox.TabStop = false;
            this.ThematicPictureBox.EnabledChanged += new System.EventHandler(this.DbfColumnLegendItem_EnabledChanged);
            this.ThematicPictureBox.Click += new System.EventHandler(this.DbfColumnLegendItem_Click);
            this.ThematicPictureBox.MouseEnter += new System.EventHandler(this.DbfColumnLegendItem_MouseEnter);
            this.ThematicPictureBox.MouseLeave += new System.EventHandler(this.DbfColumnLegendItem_MouseLeave);
            // 
            // pctValueCircle
            // 
            this.ValueCirclePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.ValueCirclePictureBox.Location = new System.Drawing.Point(127, 5);
            this.ValueCirclePictureBox.Margin = new System.Windows.Forms.Padding(5);
            this.ValueCirclePictureBox.Name = "pctValueCircle";
            this.ValueCirclePictureBox.Size = new System.Drawing.Size(20, 20);
            this.ValueCirclePictureBox.TabIndex = 2;
            this.ValueCirclePictureBox.TabStop = false;
            this.ValueCirclePictureBox.EnabledChanged += new System.EventHandler(this.DbfColumnLegendItem_EnabledChanged);
            this.ValueCirclePictureBox.Click += new System.EventHandler(this.DbfColumnLegendItem_Click);
            this.ValueCirclePictureBox.MouseEnter += new System.EventHandler(this.DbfColumnLegendItem_MouseEnter);
            this.ValueCirclePictureBox.MouseLeave += new System.EventHandler(this.DbfColumnLegendItem_MouseLeave);
            // 
            // chbIsSelected
            // 
            this.IsSelectedCheckBox.AutoSize = true;
            this.IsSelectedCheckBox.Location = new System.Drawing.Point(215, 8);
            this.IsSelectedCheckBox.Name = "chbIsSelected";
            this.IsSelectedCheckBox.Size = new System.Drawing.Size(15, 14);
            this.IsSelectedCheckBox.TabIndex = 3;
            this.IsSelectedCheckBox.UseVisualStyleBackColor = true;
            this.IsSelectedCheckBox.CheckedChanged += new System.EventHandler(this.SelectedCheckBox_CheckedChanged);
            this.IsSelectedCheckBox.Click += new System.EventHandler(this.IsSelectedCheckBox_Click);
            // 
            // lblTitle
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(-3, 8);
            this.TitleLabel.Name = "lblTitle";
            this.TitleLabel.Size = new System.Drawing.Size(41, 15);
            this.TitleLabel.TabIndex = 4;
            this.TitleLabel.Text = "label1";
            // 
            // DbfColumnUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.IsSelectedCheckBox);
            this.Controls.Add(this.ValueCirclePictureBox);
            this.Controls.Add(this.ThematicPictureBox);
            this.Controls.Add(this.DotDensityPictureBox);
            this.Margin = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.Name = "DbfColumnUserControl";
            this.Size = new System.Drawing.Size(230, 30);
            ((System.ComponentModel.ISupportInitialize)(this.DotDensityPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThematicPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValueCirclePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DotDensityPictureBox;
        private System.Windows.Forms.PictureBox ThematicPictureBox;
        private System.Windows.Forms.PictureBox ValueCirclePictureBox;
        private System.Windows.Forms.CheckBox IsSelectedCheckBox;
        private System.Windows.Forms.Label TitleLabel;
    }
}
