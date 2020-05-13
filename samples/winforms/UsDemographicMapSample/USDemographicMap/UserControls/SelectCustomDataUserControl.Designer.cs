namespace ThinkGeo.MapSuite.USDemographicMap
{
    partial class SelectCustomDataUserControl
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
            this.txtPathFilename = new System.Windows.Forms.TextBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PathFilenameTextBox
            // 
            this.txtPathFilename.Enabled = false;
            this.txtPathFilename.Location = new System.Drawing.Point(10, 5);
            this.txtPathFilename.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txtPathFilename.Name = "PathFilenameTextBox";
            this.txtPathFilename.Size = new System.Drawing.Size(142, 20);
            this.txtPathFilename.TabIndex = 0;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(162, 3);
            this.btnBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 23);
            this.btnBrowser.TabIndex = 1;
            this.btnBrowser.Text = "Browse";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // SelectCustomDataUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.txtPathFilename);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SelectCustomDataUserControl";
            this.Size = new System.Drawing.Size(240, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathFilename;
        private System.Windows.Forms.Button btnBrowser;
    }
}
