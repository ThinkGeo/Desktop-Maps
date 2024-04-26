using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayCloudMapsVectorOverlaySample: UserControl
    {
        public DisplayCloudMapsVectorOverlaySample()
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

        private void lblCloudMapsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://cloud.thinkgeo.com/") { UseShellExecute = true });
        }

        private async void displayVectorCloudMaps_Click(object sender, EventArgs e)
        {
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);
            await mapView.RefreshAsync();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button displayVectorCloudMaps;
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
            this.displayVectorCloudMaps = new System.Windows.Forms.Button();
            this.cloudMapsSecretKey = new System.Windows.Forms.TextBox();
            this.cloudMapsApiKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCloudMapsLink = new System.Windows.Forms.LinkLabel();
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
            this.mapView.Size = new System.Drawing.Size(1025, 553);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.displayVectorCloudMaps);
            this.panel1.Controls.Add(this.cloudMapsSecretKey);
            this.panel1.Controls.Add(this.cloudMapsApiKey);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblCloudMapsLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1028, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 553);
            this.panel1.TabIndex = 1;
            // 
            // displayVectorCloudMaps
            // 
            this.displayVectorCloudMaps.Location = new System.Drawing.Point(3, 168);
            this.displayVectorCloudMaps.Name = "displayVectorCloudMaps";
            this.displayVectorCloudMaps.Size = new System.Drawing.Size(295, 31);
            this.displayVectorCloudMaps.TabIndex = 6;
            this.displayVectorCloudMaps.Text = "Update";
            this.displayVectorCloudMaps.UseVisualStyleBackColor = true;
            this.displayVectorCloudMaps.Click += new System.EventHandler(this.displayVectorCloudMaps_Click);
            // 
            // cloudMapsSecretKey
            // 
            this.cloudMapsSecretKey.Location = new System.Drawing.Point(131, 128);
            this.cloudMapsSecretKey.Name = "cloudMapsSecretKey";
            this.cloudMapsSecretKey.Size = new System.Drawing.Size(167, 22);
            this.cloudMapsSecretKey.TabIndex = 5;
            this.cloudMapsSecretKey.Text = "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~";
            // 
            // cloudMapsApiKey
            // 
            this.cloudMapsApiKey.Location = new System.Drawing.Point(131, 93);
            this.cloudMapsApiKey.Name = "cloudMapsApiKey";
            this.cloudMapsApiKey.Size = new System.Drawing.Size(167, 22);
            this.cloudMapsApiKey.TabIndex = 4;
            this.cloudMapsApiKey.Text = "AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label3.Location = new System.Drawing.Point(25, 128);
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
            this.label2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label2.Location = new System.Drawing.Point(25, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "API Key:";
            // 
            // lblCloudMapsLink
            // 
            this.lblCloudMapsLink.AutoSize = true;
            this.lblCloudMapsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCloudMapsLink.ForeColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblCloudMapsLink.LinkColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.Location = new System.Drawing.Point(25, 49);
            this.lblCloudMapsLink.Name = "lblCloudMapsLink";
            this.lblCloudMapsLink.Size = new System.Drawing.Size(262, 20);
            this.lblCloudMapsLink.TabIndex = 1;
            this.lblCloudMapsLink.TabStop = true;
            this.lblCloudMapsLink.Text = "Sing up for a Cloud Maps account";
            this.lblCloudMapsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCloudMapsLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vector Cloud Maps:";
            // 
            // DisplayCloudMapsVectorOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayCloudMapsVectorOverlaySample";
            this.Size = new System.Drawing.Size(1329, 553);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion Component Designer generated code

    }
}