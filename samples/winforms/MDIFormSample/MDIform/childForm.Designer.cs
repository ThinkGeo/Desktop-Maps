namespace MDIform
{
    partial class childForm
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
            this.btnPlot = new System.Windows.Forms.Button();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPlot
            // 
            this.btnPlot.Location = new System.Drawing.Point(164, 55);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(79, 27);
            this.btnPlot.TabIndex = 0;
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(31, 24);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(111, 20);
            this.txtX.TabIndex = 1;
            this.txtX.Text = "-8940235.28";
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(31, 62);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(111, 20);
            this.txtY.TabIndex = 2;
            this.txtY.Text = "2991218.32";
            // 
            // childForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 109);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.btnPlot);
            this.Name = "childForm";
            this.Text = "Plot Point";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
    }
}