namespace ThinkGeo.MapSuite.VehicleTracking
{
    sealed partial class MeasurementPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeasurementPopup));
            this.ContentLabel = new System.Windows.Forms.Label();
            this.ClosePictureBox = new System.Windows.Forms.PictureBox();
            this.TrianglePictureBox = new System.Windows.Forms.PictureBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrianglePictureBox)).BeginInit();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.ContentLabel.AutoSize = true;
            this.ContentLabel.Location = new System.Drawing.Point(66, 16);
            this.ContentLabel.Name = "lblContent";
            this.ContentLabel.Size = new System.Drawing.Size(48, 13);
            this.ContentLabel.TabIndex = 0;
            // 
            // picClose
            // 
            this.ClosePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.ClosePictureBox.Location = new System.Drawing.Point(163, 3);
            this.ClosePictureBox.Name = "picClose";
            this.ClosePictureBox.Size = new System.Drawing.Size(12, 11);
            this.ClosePictureBox.TabIndex = 1;
            this.ClosePictureBox.TabStop = false;
            this.ClosePictureBox.Click += new System.EventHandler(this.ClosingPopup);
            // 
            // picTriangle
            // 
            this.TrianglePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("picTriangle.Image")));
            this.TrianglePictureBox.Location = new System.Drawing.Point(72, 47);
            this.TrianglePictureBox.Name = "picTriangle";
            this.TrianglePictureBox.Size = new System.Drawing.Size(22, 11);
            this.TrianglePictureBox.TabIndex = 2;
            this.TrianglePictureBox.TabStop = false;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.ContentLabel);
            this.contentPanel.Controls.Add(this.ClosePictureBox);
            this.contentPanel.Location = new System.Drawing.Point(3, 3);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(178, 47);
            this.contentPanel.TabIndex = 3;
            // 
            // MeasurementPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.TrianglePictureBox);
            this.Name = "MeasurementPopup";
            this.Size = new System.Drawing.Size(181, 61);
            ((System.ComponentModel.ISupportInitialize)(this.ClosePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrianglePictureBox)).EndInit();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ContentLabel;
        private System.Windows.Forms.PictureBox ClosePictureBox;
        private System.Windows.Forms.PictureBox TrianglePictureBox;
        private System.Windows.Forms.Panel contentPanel;
    }
}
