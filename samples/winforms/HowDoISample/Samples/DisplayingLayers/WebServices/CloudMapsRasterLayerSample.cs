using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Diagnostics;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CloudMapsRasterLayerSample: UserControl
    {
        public CloudMapsRasterLayerSample()
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
            ThinkGeoCloudRasterMapsOverlay cloudOverlay = new ThinkGeoCloudRasterMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~");
            cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
            mapView.Overlays.Add("Cloud Overlay", cloudOverlay);

            // Set the current extent to a neighborhood in Frisco Texas.
            mapView.CurrentExtent = new RectangleShape(-10781708.9749424, 3913502.90429046, -10777685.1114043, 3910360.79646662);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private async void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                ThinkGeoCloudRasterMapsOverlay cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                    default:
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private async void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                ThinkGeoCloudRasterMapsOverlay cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                    default:
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private async void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                ThinkGeoCloudRasterMapsOverlay cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                    default:
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private async void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton button = (RadioButton)sender;
            if (mapView.Overlays.Contains("Cloud Overlay"))
            {
                ThinkGeoCloudRasterMapsOverlay cloudOverlay = (ThinkGeoCloudRasterMapsOverlay)mapView.Overlays["Cloud Overlay"];

                switch (button.Text.ToString())
                {
                    case "Light":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1;
                        break;
                    case "Dark":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Dark_V2_X1;
                        break;
                    case "Aerial":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1;
                        break;
                    case "Hybrid":
                        cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
                        break;
                    default:
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(908, 630);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(907, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 630);
            this.panel1.TabIndex = 1;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.ForeColor = System.Drawing.Color.White;
            this.radioButton4.Location = new System.Drawing.Point(23, 139);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(79, 24);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Hybrid";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.ForeColor = System.Drawing.Color.White;
            this.radioButton3.Location = new System.Drawing.Point(23, 111);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(73, 24);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "Aerial";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(23, 83);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(66, 24);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "Dark";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(23, 56);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(67, 24);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Light";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Type:";
            // 
            // CloudMapsRasterLayerSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CloudMapsRasterLayerSample";
            this.Size = new System.Drawing.Size(1208, 630);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }        
        #endregion Component Designer generated code

    }

}