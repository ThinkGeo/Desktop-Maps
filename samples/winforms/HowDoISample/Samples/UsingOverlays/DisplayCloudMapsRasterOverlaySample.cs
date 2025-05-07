using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayCloudMapsRasterOverlaySample : UserControl
    {
        public DisplayCloudMapsRasterOverlaySample()
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
            mapView.CurrentExtent = new RectangleShape(-10782598.9806675, 3915669.09132595, -10772234.1196896, 3906343.77392696);
        }

        private void lblCloudMapsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://cloud.thinkgeo.com/") { UseShellExecute = true });
        }

        private async void displayRasterCloudMaps_Click(object sender, EventArgs e)
        {
            if (mapView.Overlays.Count == 0)
            {
                var thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1);
                mapView.Overlays.Add(thinkGeoCloudRasterMapsOverlay);
                await mapView.RefreshAsync();
            }
        }

        #region Component Designer generated code

        private Panel panel1;
        private Button displayRasterCloudMaps;
        private TextBox cloudMapsSecretKey;
        private TextBox cloudMapsApiKey;
        private Label label3;
        private Label label2;
        private LinkLabel lblCloudMapsLink;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCloudMapsLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cloudMapsApiKey = new System.Windows.Forms.TextBox();
            this.cloudMapsSecretKey = new System.Windows.Forms.TextBox();
            this.displayRasterCloudMaps = new System.Windows.Forms.Button();
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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(958, 578);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.displayRasterCloudMaps);
            this.panel1.Controls.Add(this.cloudMapsSecretKey);
            this.panel1.Controls.Add(this.cloudMapsApiKey);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCloudMapsLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(961, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 578);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ThinkGeo Raster Cloud Maps:";
            // 
            // lblCloudMapsLink
            // 
            this.lblCloudMapsLink.AutoSize = true;
            this.lblCloudMapsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCloudMapsLink.ForeColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.LinkColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.Location = new System.Drawing.Point(18, 65);
            this.lblCloudMapsLink.Name = "lblCloudMapsLink";
            this.lblCloudMapsLink.Size = new System.Drawing.Size(262, 20);
            this.lblCloudMapsLink.TabIndex = 1;
            this.lblCloudMapsLink.TabStop = true;
            this.lblCloudMapsLink.Text = "Sign up for a Cloud Maps account";
            this.lblCloudMapsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCloudMapsLink_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "API Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Secrete Key:";
            // 
            // cloudMapsApiKey
            // 
            this.cloudMapsApiKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cloudMapsApiKey.Location = new System.Drawing.Point(123, 100);
            this.cloudMapsApiKey.Name = "cloudMapsApiKey";
            this.cloudMapsApiKey.Size = new System.Drawing.Size(176, 26);
            this.cloudMapsApiKey.TabIndex = 4;
            this.cloudMapsApiKey.Text = "AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~";
            // 
            // cloudMapsSecretKey
            // 
            this.cloudMapsSecretKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cloudMapsSecretKey.Location = new System.Drawing.Point(123, 137);
            this.cloudMapsSecretKey.Name = "cloudMapsSecretKey";
            this.cloudMapsSecretKey.Size = new System.Drawing.Size(176, 26);
            this.cloudMapsSecretKey.TabIndex = 5;
            this.cloudMapsSecretKey.Text = "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~";
            // 
            // displayRasterCloudMaps
            // 
            this.displayRasterCloudMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.displayRasterCloudMaps.Location = new System.Drawing.Point(3, 177);
            this.displayRasterCloudMaps.Name = "displayRasterCloudMaps";
            this.displayRasterCloudMaps.Size = new System.Drawing.Size(296, 36);
            this.displayRasterCloudMaps.TabIndex = 6;
            this.displayRasterCloudMaps.Text = "Update";
            this.displayRasterCloudMaps.UseVisualStyleBackColor = true;
            this.displayRasterCloudMaps.Click += new System.EventHandler(this.displayRasterCloudMaps_Click);
            // 
            // DisplayCloudMapsRasterOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayCloudMapsRasterOverlaySample";
            this.Size = new System.Drawing.Size(1263, 578);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}