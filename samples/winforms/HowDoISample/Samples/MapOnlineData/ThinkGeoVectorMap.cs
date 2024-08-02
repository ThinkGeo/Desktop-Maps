using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ThinkGeoVectorMap : UserControl
    {
        public ThinkGeoVectorMap()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Set the map zoom level set to the Cloud Maps zoom level set.
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Create the layer overlay with some additional settings and add to the map.
            var cloudOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            mapView.CurrentExtent = new RectangleShape(-10781708.9749424, 3913502.90429046, -10777685.1114043, 3910360.79646662);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private void lblCloudMapsLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://cloud.thinkgeo.com/") { UseShellExecute = true });
        }

        private async void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                var cloudOverlay = (ThinkGeoCloudVectorMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text)
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                        break;
                    case "TransparentBackground":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private async void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                var cloudOverlay = (ThinkGeoCloudVectorMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text)
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                        break;
                    case "TransparentBackground":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private async void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                var cloudOverlay = (ThinkGeoCloudVectorMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text)
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
                        break;
                    case "TransparentBackground":
                        cloudOverlay.MapType = ThinkGeoCloudVectorMapsMapType.TransparentBackground;
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        #region Component Designer generated code

        private Panel panel1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private LinkLabel lblCloudMapsLink;
        private Label label2;
        private Label label1;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lblCloudMapsLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(908, 630);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                             | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton3);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(lblCloudMapsLink);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(907, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(301, 630);
            panel1.TabIndex = 1;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton3.ForeColor = System.Drawing.Color.White;
            radioButton3.Location = new System.Drawing.Point(23, 170);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new System.Drawing.Size(73, 24);
            radioButton3.TabIndex = 3;
            radioButton3.Text = "TransparentBackground";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new EventHandler(radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(23, 150);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(66, 24);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "Dark";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(23, 130);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(67, 24);
            radioButton1.TabIndex = 1;
            radioButton1.Text = "Light";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(radioButton1_CheckedChanged);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.ForeColor = System.Drawing.Color.White;
            label2.Location = new System.Drawing.Point(18, 100);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(107, 25);
            label2.TabIndex = 0;
            label2.Text = "Map Type:";
            // 
            // lblCloudMapsLink
            // 
            this.lblCloudMapsLink.AutoSize = true;
            this.lblCloudMapsLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCloudMapsLink.ForeColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.LinkColor = System.Drawing.Color.White;
            this.lblCloudMapsLink.Location = new System.Drawing.Point(18, 50);
            this.lblCloudMapsLink.Name = "lblCloudMapsLink";
            this.lblCloudMapsLink.Size = new System.Drawing.Size(262, 20);
            this.lblCloudMapsLink.TabIndex = 1;
            this.lblCloudMapsLink.TabStop = true;
            this.lblCloudMapsLink.Text = "Sign up for a Cloud Maps account";
            this.lblCloudMapsLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblCloudMapsLink_LinkClicked);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(18, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(107, 25);
            label1.TabIndex = 0;
            label1.Text = "ThinkGeo Vector Cloud Maps:";
            // 
            // ThinkGeoVectorMap
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "ThinkGeoVectorMap";
            Size = new System.Drawing.Size(1208, 630);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}