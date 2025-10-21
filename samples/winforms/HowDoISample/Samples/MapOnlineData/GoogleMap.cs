using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GoogleMap : UserControl
    {
        public GoogleMap()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Add a simple background overlay
            mapView.BackgroundOverlay.BackgroundBrush = GeoBrushes.AliceBlue;

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10778000, 3912000);
            mapView.CurrentScale = 77000;
        }

        private void TxtPrivateKey_TextChanged(object sender, EventArgs e)
        {
            btnActivate.Enabled = txtApiKey.Text.Length > 0;
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (txtApiKey.Text != null && !mapView.Overlays.Contains("WorldOverlay"))
            {
                // Clear the current overlay
                mapView.Overlays.Clear();

                // Create a new overlay that will hold our new layer and add it to the map.
                var worldOverlay = new GoogleMapsOverlay(txtApiKey.Text, string.Empty);
                mapView.Overlays.Add("WorldOverlay", worldOverlay);

                // Set the current extent to the whole world.
                mapView.CenterPoint = new PointShape(0, 0);
                mapView.CurrentScale = 105721100;

                // Refresh the map.
                _ = mapView.RefreshAsync();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://developers.google.com/maps/documentation/maps-static/get-api-key") { UseShellExecute = true });
        }

        #region Component Designer generated code

        private Panel panel1;
        private LinkLabel linkLabel1;
        private Button btnActivate;
        private TextBox txtApiKey;
        private Label label4;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            linkLabel1 = new LinkLabel();
            btnActivate = new Button();
            txtApiKey = new TextBox();
            label4 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(868, 568);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(btnActivate);
            panel1.Controls.Add(txtApiKey);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(871, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(301, 568);
            panel1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            linkLabel1.ForeColor = System.Drawing.Color.White;
            linkLabel1.LinkColor = System.Drawing.Color.White;
            linkLabel1.Location = new System.Drawing.Point(14, 53);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(272, 20);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Sign up for a Google Maps API Key";
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            // 
            // btnActivate
            // 
            btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnActivate.Location = new System.Drawing.Point(3, 154);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new System.Drawing.Size(294, 35);
            btnActivate.TabIndex = 6;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += new EventHandler(btnActivate_Click);
            // 
            // txtPrivateKey
            // 
            txtApiKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtApiKey.Location = new System.Drawing.Point(3, 121);
            txtApiKey.Name = "txtPrivateKey";
            txtApiKey.Size = new System.Drawing.Size(294, 27);
            txtApiKey.TabIndex = 5;
            txtApiKey.TextChanged += TxtPrivateKey_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.ForeColor = System.Drawing.Color.White;
            label4.Location = new System.Drawing.Point(17, 101);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(94, 20);
            label4.TabIndex = 4;
            label4.Text = "Api Key";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(14, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(158, 25);
            label1.TabIndex = 0;
            label1.Text = "Google API Key:";
            // 
            // GoogleMap
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "GoogleMap";
            Size = new System.Drawing.Size(1172, 568);
            Load += Form_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}