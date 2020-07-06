using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayGoogleMapsOverlaySample: UserControl
    {
        public DisplayGoogleMapsOverlaySample()
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

        private Panel panel1;
        private Button displayGoogleMaps;
        private TextBox googleSigningSecret;
        private TextBox googleApiKey;
        private Label label3;
        private Label label2;
        private LinkLabel lblGoogleMapsLink;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.displayGoogleMaps = new System.Windows.Forms.Button();
            this.googleSigningSecret = new System.Windows.Forms.TextBox();
            this.googleApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGoogleMapsLink = new System.Windows.Forms.LinkLabel();
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
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(858, 556);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.displayGoogleMaps);
            this.panel1.Controls.Add(this.googleSigningSecret);
            this.panel1.Controls.Add(this.googleApiKey);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblGoogleMapsLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(861, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 556);
            this.panel1.TabIndex = 1;
            // 
            // displayGoogleMaps
            // 
            this.displayGoogleMaps.Location = new System.Drawing.Point(3, 165);
            this.displayGoogleMaps.Name = "displayGoogleMaps";
            this.displayGoogleMaps.Size = new System.Drawing.Size(295, 36);
            this.displayGoogleMaps.TabIndex = 6;
            this.displayGoogleMaps.Text = "Update";
            this.displayGoogleMaps.UseVisualStyleBackColor = true;
            this.displayGoogleMaps.Click += new System.EventHandler(this.displayGoogleMaps_Click);
            // 
            // googleSigningSecret
            // 
            this.googleSigningSecret.Location = new System.Drawing.Point(135, 122);
            this.googleSigningSecret.Name = "googleSigningSecret";
            this.googleSigningSecret.Size = new System.Drawing.Size(163, 22);
            this.googleSigningSecret.TabIndex = 5;
            // 
            // googleApiKey
            // 
            this.googleApiKey.Location = new System.Drawing.Point(135, 85);
            this.googleApiKey.Name = "googleApiKey";
            this.googleApiKey.Size = new System.Drawing.Size(163, 22);
            this.googleApiKey.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(24, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Secrete Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Api Key";
            // 
            // lblGoogleMapsLink
            // 
            this.lblGoogleMapsLink.AutoSize = true;
            this.lblGoogleMapsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblGoogleMapsLink.ForeColor = System.Drawing.Color.White;
            this.lblGoogleMapsLink.LinkColor = System.Drawing.Color.White;
            this.lblGoogleMapsLink.Location = new System.Drawing.Point(21, 47);
            this.lblGoogleMapsLink.Name = "lblGoogleMapsLink";
            this.lblGoogleMapsLink.Size = new System.Drawing.Size(272, 20);
            this.lblGoogleMapsLink.TabIndex = 1;
            this.lblGoogleMapsLink.TabStop = true;
            this.lblGoogleMapsLink.Text = "Sign up for a Google Maps account";
            this.lblGoogleMapsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblGoogleMapsLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Google Maps:";
            // 
            // DisplayGoogleMapsOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayGoogleMapsOverlaySample";
            this.Size = new System.Drawing.Size(1162, 556);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion Component Designer generated code

        private void lblGoogleMapsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://cloud.thinkgeo.com/"));
        }

        private void displayGoogleMaps_Click(object sender, EventArgs e)
        {
            GoogleMapsOverlay googleMapsOverlay = new GoogleMapsOverlay(googleApiKey.Text, googleSigningSecret.Text);
            mapView.Overlays.Add(googleMapsOverlay);
            mapView.Refresh();
        }
    }
}