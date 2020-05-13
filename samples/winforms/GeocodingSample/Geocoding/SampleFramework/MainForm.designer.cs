namespace Geocoding
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Geocoding in Texas");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Reverse Geocoding in Texas");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Batch Geocoding in Texas");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Basic Samples", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Build an index file");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Create a Text File MatchingPlugIn");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Create a Database MatchingPlugin");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Fuzzy Logic Searching");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Census Tract Searching");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Census Block Searching");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Census BlockGroup Searching");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("IpAddressMatchingPlugin");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("GetClosestStreetNumberInTexas");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Post Code Searching");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Advanced Samples", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("How Do I Samples", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode15});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblInformation = new System.Windows.Forms.Label();
            this.miViewCode = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pbxBanner = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewLeft = new System.Windows.Forms.TreeView();
            this.pnlSample = new System.Windows.Forms.Panel();
            this.pbxContactUs = new System.Windows.Forms.PictureBox();
            this.pbxFAQ = new System.Windows.Forms.PictureBox();
            this.pbxTestimonials = new System.Windows.Forms.PictureBox();
            this.pbxOnlineStore = new System.Windows.Forms.PictureBox();
            this.pbxDiscussionForums = new System.Windows.Forms.PictureBox();
            this.pbxProductInformation = new System.Windows.Forms.PictureBox();
            this.lnkContactUs = new System.Windows.Forms.LinkLabel();
            this.lnkTestimonials = new System.Windows.Forms.LinkLabel();
            this.lnkFAQ = new System.Windows.Forms.LinkLabel();
            this.lnkOnlineStore = new System.Windows.Forms.LinkLabel();
            this.lnkDiscussionForums = new System.Windows.Forms.LinkLabel();
            this.lnkProductInformation = new System.Windows.Forms.LinkLabel();
            this.pbxLowerRightLogo = new System.Windows.Forms.PictureBox();
            this.tabctrlShow = new System.Windows.Forms.TabControl();
            this.tabpageSample = new System.Windows.Forms.TabPage();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.tabpageCSCode = new System.Windows.Forms.TabPage();
            this.cSharpBrowser = new System.Windows.Forms.WebBrowser();
            this.tmrRotator = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlSample.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxContactUs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFAQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTestimonials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOnlineStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDiscussionForums)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProductInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLowerRightLogo)).BeginInit();
            this.tabctrlShow.SuspendLayout();
            this.tabpageSample.SuspendLayout();
            this.tabpageCSCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
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
            this.miViewCode.Click += new System.EventHandler(this.miViewCode_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTop.Controls.Add(this.pbxBanner);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1014, 100);
            this.pnlTop.TabIndex = 8;
            // 
            // pbxBanner
            // 
            this.pbxBanner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbxBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxBanner.Image = Properties.Resources.Geocoder_Sample_Apps_Masthead;
            this.pbxBanner.ImageLocation = "";
            this.pbxBanner.Location = new System.Drawing.Point(0, 0);
            this.pbxBanner.Name = "pbxBanner";
            this.pbxBanner.Size = new System.Drawing.Size(1014, 100);
            this.pbxBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxBanner.TabIndex = 0;
            this.pbxBanner.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 106);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlSample);
            this.splitContainer1.Panel2.Controls.Add(this.tabctrlShow);
            this.splitContainer1.Size = new System.Drawing.Size(1014, 633);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewLeft
            // 
            this.treeViewLeft.BackColor = System.Drawing.Color.White;
            this.treeViewLeft.Location = new System.Drawing.Point(0, 3);
            this.treeViewLeft.Name = "treeViewLeft";
            treeNode1.Name = "GeocodingInTexas";
            treeNode1.Text = "Geocoding in Texas";
            treeNode2.Name = "ReverseGeocodingInTexas";
            treeNode2.Text = "Reverse Geocoding in Texas";
            treeNode3.Name = "BatchGeocodingInTexas";
            treeNode3.Text = "Batch Geocoding in Texas";
            treeNode4.Name = "BasicSamples";
            treeNode4.Text = "Basic Samples";
            treeNode5.Name = "BuildIndexFile";
            treeNode5.Text = "Build an index file";
            treeNode6.Name = "CreateTextFileMatchingPlugIn";
            treeNode6.Text = "Create a Text File MatchingPlugIn";
            treeNode7.Name = "CreateDatabaseMatchingPlugin";
            treeNode7.Text = "Create a Database MatchingPlugin";
            treeNode8.Name = "FuzzyLogicSearching";
            treeNode8.Text = "Fuzzy Logic Searching";
            treeNode9.Name = "CensusTractSearching";
            treeNode9.Text = "Census Tract Searching";
            treeNode10.Name = "CensusBlockSearching";
            treeNode10.Text = "Census Block Searching";
            treeNode11.Name = "CensusBlockGroupSearching";
            treeNode11.Text = "Census BlockGroup Searching";
            treeNode12.Name = "IpAddressMatchingPlugin";
            treeNode12.Text = "IpAddressMatchingPlugin";
            treeNode13.Name = "GetClosestStreetNumberInTexas";
            treeNode13.Text = "GetClosestStreetNumberInTexas";
            treeNode14.Name = "PostCodeSearching";
            treeNode14.Text = "Post Code Searching";
            treeNode15.Name = "AdvancedSamples";
            treeNode15.Text = "Advanced Samples";
            treeNode16.ForeColor = System.Drawing.Color.Black;
            treeNode16.ImageIndex = 0;
            treeNode16.Name = "RootNode";
            treeNode16.NodeFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode16.Text = "How Do I Samples";
            this.treeViewLeft.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16});
            this.treeViewLeft.ShowNodeToolTips = true;
            this.treeViewLeft.Size = new System.Drawing.Size(213, 630);
            this.treeViewLeft.TabIndex = 6;
            this.treeViewLeft.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewLeft_BeforeExpand);
            this.treeViewLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLeft_AfterSelect);
            // 
            // pnlSample
            // 
            this.pnlSample.Controls.Add(this.pbxContactUs);
            this.pnlSample.Controls.Add(this.pbxFAQ);
            this.pnlSample.Controls.Add(this.pbxTestimonials);
            this.pnlSample.Controls.Add(this.pbxOnlineStore);
            this.pnlSample.Controls.Add(this.pbxDiscussionForums);
            this.pnlSample.Controls.Add(this.pbxProductInformation);
            this.pnlSample.Controls.Add(this.lnkContactUs);
            this.pnlSample.Controls.Add(this.lnkTestimonials);
            this.pnlSample.Controls.Add(this.lnkFAQ);
            this.pnlSample.Controls.Add(this.lnkOnlineStore);
            this.pnlSample.Controls.Add(this.lnkDiscussionForums);
            this.pnlSample.Controls.Add(this.lnkProductInformation);
            this.pnlSample.Controls.Add(this.pbxLowerRightLogo);
            this.pnlSample.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSample.Location = new System.Drawing.Point(0, 567);
            this.pnlSample.Name = "pnlSample";
            this.pnlSample.Size = new System.Drawing.Size(790, 66);
            this.pnlSample.TabIndex = 10;
            // 
            // pbxContactUs
            // 
            this.pbxContactUs.Image = Properties.Resources.LinkButtonIconContactUs;
            this.pbxContactUs.Location = new System.Drawing.Point(331, 36);
            this.pbxContactUs.Name = "pbxContactUs";
            this.pbxContactUs.Size = new System.Drawing.Size(26, 26);
            this.pbxContactUs.TabIndex = 82;
            this.pbxContactUs.TabStop = false;
            // 
            // pbxFAQ
            // 
            this.pbxFAQ.Image = Properties.Resources.LinkButtonIconFAQ;
            this.pbxFAQ.Location = new System.Drawing.Point(191, 36);
            this.pbxFAQ.Name = "pbxFAQ";
            this.pbxFAQ.Size = new System.Drawing.Size(26, 26);
            this.pbxFAQ.TabIndex = 82;
            this.pbxFAQ.TabStop = false;
            // 
            // pbxTestimonials
            // 
            this.pbxTestimonials.Image = Properties.Resources.LinkButtonIconTestimonials;
            this.pbxTestimonials.Location = new System.Drawing.Point(331, 4);
            this.pbxTestimonials.Name = "pbxTestimonials";
            this.pbxTestimonials.Size = new System.Drawing.Size(26, 26);
            this.pbxTestimonials.TabIndex = 82;
            this.pbxTestimonials.TabStop = false;
            // 
            // pbxOnlineStore
            // 
            this.pbxOnlineStore.Image = Properties.Resources.LinkButtonIconOnlineStore;
            this.pbxOnlineStore.Location = new System.Drawing.Point(191, 4);
            this.pbxOnlineStore.Name = "pbxOnlineStore";
            this.pbxOnlineStore.Size = new System.Drawing.Size(26, 26);
            this.pbxOnlineStore.TabIndex = 82;
            this.pbxOnlineStore.TabStop = false;
            // 
            // pbxDiscussionForums
            // 
            this.pbxDiscussionForums.Image = Properties.Resources.LinkButtonIconDiscussionForums;
            this.pbxDiscussionForums.Location = new System.Drawing.Point(10, 36);
            this.pbxDiscussionForums.Name = "pbxDiscussionForums";
            this.pbxDiscussionForums.Size = new System.Drawing.Size(26, 26);
            this.pbxDiscussionForums.TabIndex = 82;
            this.pbxDiscussionForums.TabStop = false;
            // 
            // pbxProductInformation
            // 
            this.pbxProductInformation.Image = Properties.Resources.LinkButtonIconProductInfo;
            this.pbxProductInformation.Location = new System.Drawing.Point(10, 4);
            this.pbxProductInformation.Name = "pbxProductInformation";
            this.pbxProductInformation.Size = new System.Drawing.Size(26, 26);
            this.pbxProductInformation.TabIndex = 82;
            this.pbxProductInformation.TabStop = false;
            // 
            // lnkContactUs
            // 
            this.lnkContactUs.AutoSize = true;
            this.lnkContactUs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkContactUs.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkContactUs.Location = new System.Drawing.Point(365, 43);
            this.lnkContactUs.Name = "lnkContactUs";
            this.lnkContactUs.Size = new System.Drawing.Size(70, 13);
            this.lnkContactUs.TabIndex = 81;
            this.lnkContactUs.TabStop = true;
            this.lnkContactUs.Text = "Contact Us";
            this.lnkContactUs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkContactUs_LinkClicked);
            // 
            // lnkTestimonials
            // 
            this.lnkTestimonials.AutoSize = true;
            this.lnkTestimonials.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkTestimonials.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkTestimonials.Location = new System.Drawing.Point(365, 11);
            this.lnkTestimonials.Name = "lnkTestimonials";
            this.lnkTestimonials.Size = new System.Drawing.Size(77, 13);
            this.lnkTestimonials.TabIndex = 80;
            this.lnkTestimonials.TabStop = true;
            this.lnkTestimonials.Text = "Testimonials";
            this.lnkTestimonials.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTestimonials_LinkClicked);
            // 
            // lnkFAQ
            // 
            this.lnkFAQ.AutoSize = true;
            this.lnkFAQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkFAQ.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkFAQ.Location = new System.Drawing.Point(225, 43);
            this.lnkFAQ.Name = "lnkFAQ";
            this.lnkFAQ.Size = new System.Drawing.Size(31, 13);
            this.lnkFAQ.TabIndex = 79;
            this.lnkFAQ.TabStop = true;
            this.lnkFAQ.Text = "FAQ";
            this.lnkFAQ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkFAQ_LinkClicked);
            // 
            // lnkOnlineStore
            // 
            this.lnkOnlineStore.AutoSize = true;
            this.lnkOnlineStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkOnlineStore.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkOnlineStore.Location = new System.Drawing.Point(225, 11);
            this.lnkOnlineStore.Name = "lnkOnlineStore";
            this.lnkOnlineStore.Size = new System.Drawing.Size(77, 13);
            this.lnkOnlineStore.TabIndex = 78;
            this.lnkOnlineStore.TabStop = true;
            this.lnkOnlineStore.Text = "Online Store";
            this.lnkOnlineStore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOnlineStore_LinkClicked);
            // 
            // lnkDiscussionForums
            // 
            this.lnkDiscussionForums.AutoSize = true;
            this.lnkDiscussionForums.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDiscussionForums.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkDiscussionForums.Location = new System.Drawing.Point(44, 43);
            this.lnkDiscussionForums.Name = "lnkDiscussionForums";
            this.lnkDiscussionForums.Size = new System.Drawing.Size(112, 13);
            this.lnkDiscussionForums.TabIndex = 77;
            this.lnkDiscussionForums.TabStop = true;
            this.lnkDiscussionForums.Text = "Discussion Forums";
            this.lnkDiscussionForums.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDiscussionForums_LinkClicked);
            // 
            // lnkProductInformation
            // 
            this.lnkProductInformation.AutoSize = true;
            this.lnkProductInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkProductInformation.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(84)))), ((int)(((byte)(120)))));
            this.lnkProductInformation.Location = new System.Drawing.Point(44, 11);
            this.lnkProductInformation.Name = "lnkProductInformation";
            this.lnkProductInformation.Size = new System.Drawing.Size(118, 13);
            this.lnkProductInformation.TabIndex = 76;
            this.lnkProductInformation.TabStop = true;
            this.lnkProductInformation.Text = "Product Information";
            this.lnkProductInformation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProductInformation_LinkClicked);
            // 
            // pbxLowerRightLogo
            // 
            this.pbxLowerRightLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLowerRightLogo.Image = Properties.Resources.ThinkGeoLogo;
            this.pbxLowerRightLogo.Location = new System.Drawing.Point(666, 19);
            this.pbxLowerRightLogo.Name = "pbxLowerRightLogo";
            this.pbxLowerRightLogo.Size = new System.Drawing.Size(117, 37);
            this.pbxLowerRightLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxLowerRightLogo.TabIndex = 10;
            this.pbxLowerRightLogo.TabStop = false;
            // 
            // tabctrlShow
            // 
            this.tabctrlShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabctrlShow.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabctrlShow.Controls.Add(this.tabpageSample);
            this.tabctrlShow.Controls.Add(this.tabpageCSCode);
            this.tabctrlShow.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabctrlShow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabctrlShow.Location = new System.Drawing.Point(3, 3);
            this.tabctrlShow.Name = "tabctrlShow";
            this.tabctrlShow.SelectedIndex = 0;
            this.tabctrlShow.Size = new System.Drawing.Size(784, 565);
            this.tabctrlShow.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabctrlShow.TabIndex = 8;
            this.tabctrlShow.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabctrlShow_DrawItem);
            // 
            // tabpageSample
            // 
            this.tabpageSample.Controls.Add(this.pnlOption);
            this.tabpageSample.Location = new System.Drawing.Point(4, 25);
            this.tabpageSample.Name = "tabpageSample";
            this.tabpageSample.Padding = new System.Windows.Forms.Padding(3);
            this.tabpageSample.Size = new System.Drawing.Size(776, 536);
            this.tabpageSample.TabIndex = 0;
            this.tabpageSample.Text = "Sample";
            this.tabpageSample.UseVisualStyleBackColor = true;
            // 
            // pnlOption
            // 
            this.pnlOption.AutoSize = true;
            this.pnlOption.BackColor = System.Drawing.SystemColors.Control;
            this.pnlOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOption.Location = new System.Drawing.Point(3, 3);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(770, 530);
            this.pnlOption.TabIndex = 8;
            // 
            // tabpageCSCode
            // 
            this.tabpageCSCode.Controls.Add(this.cSharpBrowser);
            this.tabpageCSCode.Location = new System.Drawing.Point(4, 25);
            this.tabpageCSCode.Name = "tabpageCSCode";
            this.tabpageCSCode.Padding = new System.Windows.Forms.Padding(3);
            this.tabpageCSCode.Size = new System.Drawing.Size(776, 536);
            this.tabpageCSCode.TabIndex = 1;
            this.tabpageCSCode.Text = "C# Source";
            this.tabpageCSCode.UseVisualStyleBackColor = true;
            this.tabpageCSCode.Enter += new System.EventHandler(this.tabpageCSCode_Enter);
            // 
            // cSharpBrowser
            // 
            this.cSharpBrowser.Location = new System.Drawing.Point(3, 3);
            this.cSharpBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.cSharpBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.cSharpBrowser.Name = "cSharpBrowser";
            this.cSharpBrowser.ScrollBarsEnabled = false;
            this.cSharpBrowser.Size = new System.Drawing.Size(740, 528);
            this.cSharpBrowser.TabIndex = 0;
            // 
            // tmrRotator
            // 
            this.tmrRotator.Interval = 20000;
            this.tmrRotator.Tick += new System.EventHandler(this.tmrRotator_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 739);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Sample App";
            this.Load += new System.EventHandler(this.Samples_Load);
            this.pnlTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBanner)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.pnlSample.ResumeLayout(false);
            this.pnlSample.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxContactUs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFAQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxTestimonials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOnlineStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDiscussionForums)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxProductInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLowerRightLogo)).EndInit();
            this.tabctrlShow.ResumeLayout(false);
            this.tabpageSample.ResumeLayout(false);
            this.tabpageSample.PerformLayout();
            this.tabpageCSCode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewLeft;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlSample;
        private System.Windows.Forms.PictureBox pbxLowerRightLogo;
        private System.Windows.Forms.TabControl tabctrlShow;
        private System.Windows.Forms.TabPage tabpageSample;
        private System.Windows.Forms.Panel pnlOption;
        private System.Windows.Forms.TabPage tabpageCSCode;
        private System.Windows.Forms.PictureBox pbxBanner;
        internal System.Windows.Forms.LinkLabel lnkContactUs;
        internal System.Windows.Forms.LinkLabel lnkTestimonials;
        internal System.Windows.Forms.LinkLabel lnkFAQ;
        internal System.Windows.Forms.LinkLabel lnkOnlineStore;
        internal System.Windows.Forms.LinkLabel lnkDiscussionForums;
        internal System.Windows.Forms.LinkLabel lnkProductInformation;
        private System.Windows.Forms.ToolStripMenuItem miViewCode;
        private System.Windows.Forms.PictureBox pbxContactUs;
        private System.Windows.Forms.PictureBox pbxFAQ;
        private System.Windows.Forms.PictureBox pbxTestimonials;
        private System.Windows.Forms.PictureBox pbxOnlineStore;
        private System.Windows.Forms.PictureBox pbxDiscussionForums;
        private System.Windows.Forms.PictureBox pbxProductInformation;
        private System.Windows.Forms.WebBrowser cSharpBrowser;
        private System.Windows.Forms.Timer tmrRotator;
    }
}

