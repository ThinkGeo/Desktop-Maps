using System.Windows.Forms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    partial class Samples
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Samples));
            this.lblInformation = new System.Windows.Forms.Label();
            this.miViewCode = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabelSupport = new System.Windows.Forms.LinkLabel();
            this.linkLabelDocumentation = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewLeft = new System.Windows.Forms.TreeView();
            this.splitContainerSampleSource = new System.Windows.Forms.SplitContainer();
            this.cSharpBrowser = new System.Windows.Forms.WebBrowser();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.btnSource = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSampleDescription = new System.Windows.Forms.Label();
            this.labelSampleName = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSampleSource)).BeginInit();
            this.splitContainerSampleSource.Panel1.SuspendLayout();
            this.splitContainerSampleSource.Panel2.SuspendLayout();
            this.splitContainerSampleSource.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.BackColor = System.Drawing.Color.Maroon;
            this.lblInformation.Location = new System.Drawing.Point(224, 29);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(0, 13);
            this.lblInformation.TabIndex = 4;
            // 
            // miViewCode
            // 
            this.miViewCode.Name = "miViewCode";
            this.miViewCode.Size = new System.Drawing.Size(135, 22);
            this.miViewCode.Text = "View Code";
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(40)))), ((int)(((byte)(44)))));
            this.pnlTop.Controls.Add(this.label3);
            this.pnlTop.Controls.Add(this.label2);
            this.pnlTop.Controls.Add(this.linkLabelSupport);
            this.pnlTop.Controls.Add(this.linkLabelDocumentation);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1332, 70);
            this.pnlTop.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(1253, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "|";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(70, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Explore samples of ThinkGeo UI Controls for Desktop.";
            // 
            // linkLabelSupport
            // 
            this.linkLabelSupport.AutoSize = true;
            this.linkLabelSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelSupport.LinkColor = System.Drawing.Color.White;
            this.linkLabelSupport.Location = new System.Drawing.Point(1269, 20);
            this.linkLabelSupport.Name = "linkLabelSupport";
            this.linkLabelSupport.Size = new System.Drawing.Size(60, 18);
            this.linkLabelSupport.TabIndex = 10;
            this.linkLabelSupport.TabStop = true;
            this.linkLabelSupport.Text = "Support";
            this.linkLabelSupport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSupport_LinkClicked);
            // 
            // linkLabelDocumentation
            // 
            this.linkLabelDocumentation.AutoSize = true;
            this.linkLabelDocumentation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelDocumentation.LinkColor = System.Drawing.Color.White;
            this.linkLabelDocumentation.Location = new System.Drawing.Point(1144, 20);
            this.linkLabelDocumentation.Name = "linkLabelDocumentation";
            this.linkLabelDocumentation.Size = new System.Drawing.Size(109, 18);
            this.linkLabelDocumentation.TabIndex = 9;
            this.linkLabelDocumentation.TabStop = true;
            this.linkLabelDocumentation.Text = "Documentation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(69, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "ThinkGeo UI Control Samples for Desktop(WinForms)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(2, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 70);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 68);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(62)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainerSampleSource);
            this.splitContainer1.Panel2.Controls.Add(this.btnSource);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1332, 715);
            this.splitContainer1.SplitterDistance = 327;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewLeft
            // 
            this.treeViewLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(48)))), ((int)(((byte)(53)))));
            this.treeViewLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLeft.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(185)))), ((int)(((byte)(226)))));
            this.treeViewLeft.Location = new System.Drawing.Point(0, 0);
            this.treeViewLeft.Name = "treeViewLeft";
            this.treeViewLeft.ShowNodeToolTips = true;
            this.treeViewLeft.Size = new System.Drawing.Size(327, 715);
            this.treeViewLeft.TabIndex = 6;
            this.treeViewLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLeft_AfterSelect);
            // 
            // splitContainerSampleSource
            // 
            this.splitContainerSampleSource.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainerSampleSource.Location = new System.Drawing.Point(0, 109);
            this.splitContainerSampleSource.Name = "splitContainerSampleSource";
            // 
            // splitContainerSampleSource.Panel1
            // 
            this.splitContainerSampleSource.Panel1.Controls.Add(this.cSharpBrowser);
            this.splitContainerSampleSource.Panel1Collapsed = true;
            this.splitContainerSampleSource.Panel1MinSize = 20;
            // 
            // splitContainerSampleSource.Panel2
            // 
            this.splitContainerSampleSource.Panel2.Controls.Add(this.pnlOption);
            this.splitContainerSampleSource.Size = new System.Drawing.Size(1001, 606);
            this.splitContainerSampleSource.SplitterDistance = 512;
            this.splitContainerSampleSource.TabIndex = 11;
            // 
            // cSharpBrowser
            // 
            this.cSharpBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cSharpBrowser.Location = new System.Drawing.Point(0, 0);
            this.cSharpBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.cSharpBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.cSharpBrowser.Name = "cSharpBrowser";
            this.cSharpBrowser.ScrollBarsEnabled = false;
            this.cSharpBrowser.Size = new System.Drawing.Size(512, 100);
            this.cSharpBrowser.TabIndex = 0;
            // 
            // pnlOption
            // 
            this.pnlOption.AutoSize = true;
            this.pnlOption.BackColor = System.Drawing.Color.LightBlue;
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.Location = new System.Drawing.Point(0, 0);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(1001, 606);
            this.pnlOption.TabIndex = 8;
            // 
            // btnSource
            // 
            this.btnSource.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnSource.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSource.ForeColor = System.Drawing.Color.White;
            this.btnSource.Location = new System.Drawing.Point(4, 74);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(56, 29);
            this.btnSource.TabIndex = 10;
            this.btnSource.Text = "Source";
            this.btnSource.UseVisualStyleBackColor = false;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelSampleDescription);
            this.panel1.Controls.Add(this.labelSampleName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 68);
            this.panel1.TabIndex = 9;
            // 
            // labelSampleDescription
            // 
            this.labelSampleDescription.AutoSize = true;
            this.labelSampleDescription.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSampleDescription.ForeColor = System.Drawing.Color.Transparent;
            this.labelSampleDescription.Location = new System.Drawing.Point(6, 37);
            this.labelSampleDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSampleDescription.Name = "labelSampleDescription";
            this.labelSampleDescription.Size = new System.Drawing.Size(144, 16);
            this.labelSampleDescription.TabIndex = 8;
            this.labelSampleDescription.Text = "labelSampleDescription";
            // 
            // labelSampleName
            // 
            this.labelSampleName.AutoSize = true;
            this.labelSampleName.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSampleName.ForeColor = System.Drawing.Color.Transparent;
            this.labelSampleName.Location = new System.Drawing.Point(5, 9);
            this.labelSampleName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSampleName.Name = "labelSampleName";
            this.labelSampleName.Size = new System.Drawing.Size(138, 19);
            this.labelSampleName.TabIndex = 8;
            this.labelSampleName.Text = "labelSampleName";
            // 
            // Samples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 783);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Samples";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Sample App";
            this.Load += new System.EventHandler(this.Samples_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerSampleSource.Panel1.ResumeLayout(false);
            this.splitContainerSampleSource.Panel2.ResumeLayout(false);
            this.splitContainerSampleSource.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSampleSource)).EndInit();
            this.splitContainerSampleSource.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewLeft;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ToolStripMenuItem miViewCode;
        private System.Windows.Forms.WebBrowser cSharpBrowser;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Panel panel1;
        private Panel pnlOption;
        private Label labelSampleDescription;
        private Label labelSampleName;
        private Button btnSource;
        private SplitContainer splitContainerSampleSource;
        private Label label3;
        private LinkLabel linkLabelSupport;
        private LinkLabel linkLabelDocumentation;
    }
}

