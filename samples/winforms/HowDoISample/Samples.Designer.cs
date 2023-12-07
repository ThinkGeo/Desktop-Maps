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
            lblInformation = new Label();
            miViewCode = new ToolStripMenuItem();
            pnlTop = new Panel();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            splitContainerMain = new SplitContainer();
            splitContainerLeft = new SplitContainer();
            txtSearch = new TextBox();
            lblSearch = new Label();
            treeViewLeft = new TreeView();
            SplitContainerRight = new SplitContainer();
            panel1 = new Panel();
            btnSource = new Button();
            labelSampleDescription = new Label();
            labelSampleName = new Label();
            splitContainerSampleSource = new SplitContainer();
            cSharpBrowser = new WebBrowser();
            pnlOption = new Panel();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerLeft).BeginInit();
            splitContainerLeft.Panel1.SuspendLayout();
            splitContainerLeft.Panel2.SuspendLayout();
            splitContainerLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainerRight).BeginInit();
            SplitContainerRight.Panel1.SuspendLayout();
            SplitContainerRight.Panel2.SuspendLayout();
            SplitContainerRight.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerSampleSource).BeginInit();
            splitContainerSampleSource.Panel1.SuspendLayout();
            splitContainerSampleSource.Panel2.SuspendLayout();
            splitContainerSampleSource.SuspendLayout();
            SuspendLayout();
            // 
            // lblInformation
            // 
            lblInformation.AutoSize = true;
            lblInformation.BackColor = System.Drawing.Color.Maroon;
            lblInformation.Location = new System.Drawing.Point(224, 29);
            lblInformation.Name = "lblInformation";
            lblInformation.Size = new System.Drawing.Size(0, 13);
            lblInformation.TabIndex = 4;
            // 
            // miViewCode
            // 
            miViewCode.Name = "miViewCode";
            miViewCode.Size = new System.Drawing.Size(135, 22);
            miViewCode.Text = "View Code";
            // 
            // pnlTop
            // 
            pnlTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlTop.BackColor = System.Drawing.Color.FromArgb(38, 40, 44);
            pnlTop.Controls.Add(label2);
            pnlTop.Controls.Add(label1);
            pnlTop.Controls.Add(pictureBox1);
            pnlTop.Location = new System.Drawing.Point(0, 0);
            pnlTop.Margin = new Padding(0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new System.Drawing.Size(1554, 88);
            pnlTop.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = (System.Drawing.Font)resources.GetObject("label2.Font");
            label2.ForeColor = System.Drawing.Color.Transparent;
            label2.Location = new System.Drawing.Point(81, 46);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(264, 14);
            label2.TabIndex = 8;
            label2.Text = "Explore samples of ThinkGeo UI Controls for Desktop.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = (System.Drawing.Font)resources.GetObject("label1.Font");
            label1.ForeColor = System.Drawing.Color.Transparent;
            label1.Location = new System.Drawing.Point(80, 23);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(380, 18);
            label1.TabIndex = 8;
            label1.Text = "ThinkGeo UI Control Samples for Desktop(WinForms)";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new System.Drawing.Point(3, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(85, 81);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.IsSplitterFixed = true;
            splitContainerMain.Location = new System.Drawing.Point(0, 88);
            splitContainerMain.Margin = new Padding(0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(splitContainerLeft);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = System.Drawing.Color.FromArgb(61, 61, 62);
            splitContainerMain.Panel2.Controls.Add(SplitContainerRight);
            splitContainerMain.Size = new System.Drawing.Size(1554, 825);
            splitContainerMain.SplitterDistance = 302;
            splitContainerMain.TabIndex = 0;
            // 
            // splitContainerLeft
            // 
            splitContainerLeft.Dock = DockStyle.Fill;
            splitContainerLeft.FixedPanel = FixedPanel.Panel1;
            splitContainerLeft.IsSplitterFixed = true;
            splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            splitContainerLeft.Margin = new Padding(4);
            splitContainerLeft.Name = "splitContainerLeft";
            splitContainerLeft.Orientation = Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            splitContainerLeft.Panel1.Controls.Add(txtSearch);
            splitContainerLeft.Panel1.Controls.Add(lblSearch);
            // 
            // splitContainerLeft.Panel2
            // 
            splitContainerLeft.Panel2.Controls.Add(treeViewLeft);
            splitContainerLeft.Size = new System.Drawing.Size(302, 825);
            splitContainerLeft.SplitterDistance = 47;
            splitContainerLeft.SplitterWidth = 5;
            splitContainerLeft.TabIndex = 7;
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(94, 17);
            txtSearch.Margin = new Padding(4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(294, 23);
            txtSearch.TabIndex = 1;
            txtSearch.KeyUp += txtSearch_KeyUp;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            lblSearch.Location = new System.Drawing.Point(32, 21);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(48, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search: ";
            // 
            // treeViewLeft
            // 
            treeViewLeft.BackColor = System.Drawing.Color.FromArgb(45, 48, 53);
            treeViewLeft.Dock = DockStyle.Fill;
            treeViewLeft.Font = (System.Drawing.Font)resources.GetObject("treeViewLeft.Font");
            treeViewLeft.ForeColor = System.Drawing.Color.FromArgb(96, 185, 226);
            treeViewLeft.Location = new System.Drawing.Point(0, 0);
            treeViewLeft.Margin = new Padding(4);
            treeViewLeft.Name = "treeViewLeft";
            treeViewLeft.ShowNodeToolTips = true;
            treeViewLeft.Size = new System.Drawing.Size(302, 773);
            treeViewLeft.TabIndex = 6;
            treeViewLeft.BeforeSelect += treeViewLeft_BeforeSelect;
            treeViewLeft.AfterSelect += treeViewLeft_AfterSelect;
            // 
            // SplitContainerRight
            // 
            SplitContainerRight.Dock = DockStyle.Fill;
            SplitContainerRight.FixedPanel = FixedPanel.Panel1;
            SplitContainerRight.IsSplitterFixed = true;
            SplitContainerRight.Location = new System.Drawing.Point(0, 0);
            SplitContainerRight.Margin = new Padding(4);
            SplitContainerRight.Name = "SplitContainerRight";
            SplitContainerRight.Orientation = Orientation.Horizontal;
            // 
            // SplitContainerRight.Panel1
            // 
            SplitContainerRight.Panel1.Controls.Add(panel1);
            // 
            // SplitContainerRight.Panel2
            // 
            SplitContainerRight.Panel2.Controls.Add(splitContainerSampleSource);
            SplitContainerRight.Size = new System.Drawing.Size(1248, 825);
            SplitContainerRight.SplitterDistance = 96;
            SplitContainerRight.SplitterWidth = 5;
            SplitContainerRight.TabIndex = 12;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSource);
            panel1.Controls.Add(labelSampleDescription);
            panel1.Controls.Add(labelSampleName);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1248, 96);
            panel1.TabIndex = 9;
            // 
            // btnSource
            // 
            btnSource.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
            btnSource.FlatStyle = FlatStyle.Popup;
            btnSource.ForeColor = System.Drawing.Color.White;
            btnSource.Location = new System.Drawing.Point(4, 63);
            btnSource.Margin = new Padding(4);
            btnSource.Name = "btnSource";
            btnSource.Size = new System.Drawing.Size(101, 34);
            btnSource.TabIndex = 11;
            btnSource.Text = "Source";
            btnSource.UseVisualStyleBackColor = false;
            btnSource.Click += btnSource_Click;
            // 
            // labelSampleDescription
            // 
            labelSampleDescription.Dock = DockStyle.Bottom;
            labelSampleDescription.Font = (System.Drawing.Font)resources.GetObject("labelSampleDescription.Font");
            labelSampleDescription.ForeColor = System.Drawing.Color.Transparent;
            labelSampleDescription.Location = new System.Drawing.Point(0, 44);
            labelSampleDescription.Name = "labelSampleDescription";
            labelSampleDescription.Size = new System.Drawing.Size(1248, 52);
            labelSampleDescription.TabIndex = 8;
            labelSampleDescription.Text = "labelSampleDescription";
            // 
            // labelSampleName
            // 
            labelSampleName.AutoSize = true;
            labelSampleName.Font = (System.Drawing.Font)resources.GetObject("labelSampleName.Font");
            labelSampleName.ForeColor = System.Drawing.Color.Transparent;
            labelSampleName.Location = new System.Drawing.Point(1, 14);
            labelSampleName.Name = "labelSampleName";
            labelSampleName.Size = new System.Drawing.Size(138, 19);
            labelSampleName.TabIndex = 8;
            labelSampleName.Text = "labelSampleName";
            // 
            // splitContainerSampleSource
            // 
            splitContainerSampleSource.Dock = DockStyle.Fill;
            splitContainerSampleSource.Location = new System.Drawing.Point(0, 0);
            splitContainerSampleSource.Margin = new Padding(0);
            splitContainerSampleSource.Name = "splitContainerSampleSource";
            // 
            // splitContainerSampleSource.Panel1
            // 
            splitContainerSampleSource.Panel1.Controls.Add(cSharpBrowser);
            splitContainerSampleSource.Panel1Collapsed = true;
            splitContainerSampleSource.Panel1MinSize = 20;
            // 
            // splitContainerSampleSource.Panel2
            // 
            splitContainerSampleSource.Panel2.Controls.Add(pnlOption);
            splitContainerSampleSource.Size = new System.Drawing.Size(1248, 724);
            splitContainerSampleSource.SplitterDistance = 448;
            splitContainerSampleSource.TabIndex = 11;
            // 
            // cSharpBrowser
            // 
            cSharpBrowser.Dock = DockStyle.Fill;
            cSharpBrowser.Location = new System.Drawing.Point(0, 0);
            cSharpBrowser.Margin = new Padding(0);
            cSharpBrowser.MinimumSize = new System.Drawing.Size(32, 29);
            cSharpBrowser.Name = "cSharpBrowser";
            cSharpBrowser.Size = new System.Drawing.Size(448, 94);
            cSharpBrowser.TabIndex = 0;
            // 
            // pnlOption
            // 
            pnlOption.AutoSize = true;
            pnlOption.BackColor = System.Drawing.Color.LightBlue;
            pnlOption.Dock = DockStyle.Fill;
            pnlOption.Location = new System.Drawing.Point(0, 0);
            pnlOption.Margin = new Padding(0);
            pnlOption.Name = "pnlOption";
            pnlOption.Size = new System.Drawing.Size(1248, 724);
            pnlOption.TabIndex = 8;
            // 
            // Samples
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(45, 48, 53);
            ClientSize = new System.Drawing.Size(1554, 904);
            Controls.Add(pnlTop);
            Controls.Add(splitContainerMain);
            Margin = new Padding(4);
            Name = "Samples";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "C# Sample App";
            Load += Samples_Load;
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            splitContainerLeft.Panel1.ResumeLayout(false);
            splitContainerLeft.Panel1.PerformLayout();
            splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerLeft).EndInit();
            splitContainerLeft.ResumeLayout(false);
            SplitContainerRight.Panel1.ResumeLayout(false);
            SplitContainerRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainerRight).EndInit();
            SplitContainerRight.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainerSampleSource.Panel1.ResumeLayout(false);
            splitContainerSampleSource.Panel2.ResumeLayout(false);
            splitContainerSampleSource.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerSampleSource).EndInit();
            splitContainerSampleSource.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.SplitContainer splitContainerMain;
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
        private SplitContainer splitContainerSampleSource;
        private SplitContainer splitContainerLeft;
        private TextBox txtSearch;
        private Label lblSearch;
        private SplitContainer SplitContainerRight;
        private Button btnSource;
    }
}

