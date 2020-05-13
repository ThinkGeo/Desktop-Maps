using DisplayCadFile.Properties;

partial class Banner
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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Banner));
        this.sampleNameLabel = new System.Windows.Forms.Label();
        this.textPictureBox = new System.Windows.Forms.PictureBox();
        this.adsRotatorTimer = new System.Windows.Forms.Timer(this.components);
        this.backgroundPictureBox = new System.Windows.Forms.PictureBox();
        this.adRotator = new System.Windows.Forms.WebBrowser();
        ((System.ComponentModel.ISupportInitialize)(this.textPictureBox)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox)).BeginInit();
        this.SuspendLayout();
        // 
        // sampleNameLabel
        // 
        this.sampleNameLabel.AutoSize = true;
        this.sampleNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
        this.sampleNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.sampleNameLabel.Location = new System.Drawing.Point(9, 57);
        this.sampleNameLabel.Name = "sampleNameLabel";
        this.sampleNameLabel.Size = new System.Drawing.Size(164, 13);
        this.sampleNameLabel.TabIndex = 23;
        this.sampleNameLabel.Text = "How do I display CAD files?";
        // 
        // textPictureBox
        // 
        this.textPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("textPictureBox.Image")));
        this.textPictureBox.Location = new System.Drawing.Point(0, 0);
        this.textPictureBox.Name = "textPictureBox";
        this.textPictureBox.Size = new System.Drawing.Size(415, 81);
        this.textPictureBox.TabIndex = 21;
        this.textPictureBox.TabStop = false;
        // 
        // adsRotatorTimer
        // 
        this.adsRotatorTimer.Interval = 20000;
        this.adsRotatorTimer.Tick += new System.EventHandler(this.adsRotatorTimer_Tick);
        // 
        // backgroundPictureBox
        // 
        this.backgroundPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.backgroundPictureBox.BackgroundImage = global::DisplayCadFile.Properties.Resources.masthead_top_blue_1px;
        this.backgroundPictureBox.Location = new System.Drawing.Point(413, 0);
        this.backgroundPictureBox.Name = "backgroundPictureBox";
        this.backgroundPictureBox.Size = new System.Drawing.Size(595, 81);
        this.backgroundPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.backgroundPictureBox.TabIndex = 24;
        this.backgroundPictureBox.TabStop = false;
        // 
        // adRotator
        // 
        this.adRotator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.adRotator.IsWebBrowserContextMenuEnabled = false;
        this.adRotator.Location = new System.Drawing.Point(479, 10);
        this.adRotator.Margin = new System.Windows.Forms.Padding(0);
        this.adRotator.Name = "adRotator";
        this.adRotator.ScrollBarsEnabled = false;
        this.adRotator.Size = new System.Drawing.Size(468, 60);
        this.adRotator.TabIndex = 25;
        // 
        // Banner
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.adRotator);
        this.Controls.Add(this.backgroundPictureBox);
        this.Controls.Add(this.sampleNameLabel);
        this.Controls.Add(this.textPictureBox);
        this.Name = "Banner";
        this.Size = new System.Drawing.Size(1008, 81);
        ((System.ComponentModel.ISupportInitialize)(this.textPictureBox)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.backgroundPictureBox)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label sampleNameLabel;
    private System.Windows.Forms.PictureBox textPictureBox;
    private System.Windows.Forms.Timer adsRotatorTimer;
    private System.Windows.Forms.PictureBox backgroundPictureBox;
    private System.Windows.Forms.WebBrowser adRotator;
}
