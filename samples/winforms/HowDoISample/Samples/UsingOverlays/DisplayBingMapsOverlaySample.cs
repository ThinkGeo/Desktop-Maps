using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayBingMapsOverlaySample: UserControl
    {
        public DisplayBingMapsOverlaySample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            mapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        private void BingMapsAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.bingmapsportal.com/"));
        }

        private void displayBingMaps_Click(object sender, EventArgs e)
        {
            BingMapsOverlay bingMapsOverlay = new BingMapsOverlay(bingApplicationId.Text, BingMapsMapType.Road);
            mapView.Overlays.Add(bingMapsOverlay);
            mapView.Refresh();
        }

        #region Component Designer generated code
        private MapView mapView;
        private Panel panel1;
        private Label label2;
        private LinkLabel BingMapsAccountLink;
        private Label label1;
        private TextBox bingApplicationId;
        private Button displayBingMaps;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bingApplicationId = new System.Windows.Forms.TextBox();
            this.displayBingMaps = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BingMapsAccountLink = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(2, -2);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(899, 474);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.bingApplicationId);
            this.panel1.Controls.Add(this.displayBingMaps);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.BingMapsAccountLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(902, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 474);
            this.panel1.TabIndex = 1;
            // 
            // bingApplicationId
            // 
            this.bingApplicationId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.bingApplicationId.Location = new System.Drawing.Point(89, 80);
            this.bingApplicationId.Name = "bingApplicationId";
            this.bingApplicationId.Size = new System.Drawing.Size(209, 26);
            this.bingApplicationId.TabIndex = 4;
            // 
            // displayBingMaps
            // 
            this.displayBingMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.displayBingMaps.Location = new System.Drawing.Point(3, 113);
            this.displayBingMaps.Name = "displayBingMaps";
            this.displayBingMaps.Size = new System.Drawing.Size(295, 32);
            this.displayBingMaps.TabIndex = 3;
            this.displayBingMaps.Text = "Update";
            this.displayBingMaps.UseVisualStyleBackColor = true;
            this.displayBingMaps.Click += new System.EventHandler(this.displayBingMaps_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "API Key:";
            // 
            // BingMapsAccountLink
            // 
            this.BingMapsAccountLink.AutoSize = true;
            this.BingMapsAccountLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.BingMapsAccountLink.ForeColor = System.Drawing.Color.White;
            this.BingMapsAccountLink.LinkColor = System.Drawing.Color.White;
            this.BingMapsAccountLink.Location = new System.Drawing.Point(15, 48);
            this.BingMapsAccountLink.Name = "BingMapsAccountLink";
            this.BingMapsAccountLink.Size = new System.Drawing.Size(253, 20);
            this.BingMapsAccountLink.TabIndex = 1;
            this.BingMapsAccountLink.TabStop = true;
            this.BingMapsAccountLink.Text = "Sign up for a Bing Maps account";
            this.BingMapsAccountLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BingMapsAccountLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bing Maps Controls:";
            // 
            // DisplayBingMapsOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayBingMapsOverlaySample";
            this.Size = new System.Drawing.Size(1203, 474);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}