using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class Logo : UserControl
    {
        public Logo()
        {
            InitializeComponent();
        }
        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent           
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            mapView.MapTools.Logo.Source = new BitmapImage(new Uri(@"..\..\..\Resources\ThinkGeoLogo.png", UriKind.RelativeOrAbsolute));

            await mapView.RefreshAsync();
        }

        private void thinkGeoLogo_CheckedChanged(object sender, EventArgs e)
        {
            mapView.MapTools.Logo.Source = new BitmapImage(new Uri(@"..\..\..\Resources\ThinkGeoLogo.png", UriKind.RelativeOrAbsolute));
        }

        private void genericLogo_CheckedChanged(object sender, EventArgs e)
        {
            mapView.MapTools.Logo.Source = new BitmapImage(new Uri(@"..\..\..\Resources\generic-logo.png", UriKind.RelativeOrAbsolute));
        }

        #region Component Designer generated code
        private Panel panel1;
        private RadioButton genericLogo;
        private RadioButton thinkGeoLogo;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.genericLogo = new System.Windows.Forms.RadioButton();
            this.thinkGeoLogo = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(980, 618);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.genericLogo);
            this.panel1.Controls.Add(this.thinkGeoLogo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(986, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 618);
            this.panel1.TabIndex = 1;
            // 
            // genericLogo
            // 
            this.genericLogo.AutoSize = true;
            this.genericLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.genericLogo.ForeColor = System.Drawing.Color.White;
            this.genericLogo.Location = new System.Drawing.Point(18, 100);
            this.genericLogo.Name = "genericLogo";
            this.genericLogo.Size = new System.Drawing.Size(131, 24);
            this.genericLogo.TabIndex = 2;
            this.genericLogo.Text = "Generic Logo";
            this.genericLogo.UseVisualStyleBackColor = true;
            this.genericLogo.CheckedChanged += new System.EventHandler(this.genericLogo_CheckedChanged);
            // 
            // thinkGeoLogo
            // 
            this.thinkGeoLogo.AutoSize = true;
            this.thinkGeoLogo.Checked = true;
            this.thinkGeoLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.thinkGeoLogo.ForeColor = System.Drawing.Color.White;
            this.thinkGeoLogo.Location = new System.Drawing.Point(18, 70);
            this.thinkGeoLogo.Name = "thinkGeoLogo";
            this.thinkGeoLogo.Size = new System.Drawing.Size(143, 24);
            this.thinkGeoLogo.TabIndex = 1;
            this.thinkGeoLogo.TabStop = true;
            this.thinkGeoLogo.Text = "ThinkGeo Logo";
            this.thinkGeoLogo.UseVisualStyleBackColor = true;
            this.thinkGeoLogo.CheckedChanged += new System.EventHandler(this.thinkGeoLogo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Map Logo Controls:";
            // 
            // Logo
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "Logo";
            this.Size = new System.Drawing.Size(1286, 618);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}