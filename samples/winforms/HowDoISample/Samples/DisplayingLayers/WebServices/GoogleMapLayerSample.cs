using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GoogleMapLayerSample : UserControl
    {
        public GoogleMapLayerSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;
            // Set the current extent to the whole world.
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
        }

        private void TxtPrivateKey_TextChanged(object sender, EventArgs e)
        {
            btnActivate.Enabled = txtApiKey.Text.Length > 0;
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (txtApiKey.Text != null && !mapView.Overlays.Contains("WorldOverlay"))
            {
                // Sets the map zoom level set to the Google maps zoom level set.
                mapView.ZoomLevelSet = new GoogleMapsZoomLevelSet();

                // Clear the current overlay
                mapView.Overlays.Clear();

                // Create a new overlay that will hold our new layer and add it to the map.
                LayerOverlay worldOverlay = new LayerOverlay();
                mapView.Overlays.Add("WorldOverlay", worldOverlay);

                // Create the new layer.
                GoogleMapsLayer worldLayer = new GoogleMapsLayer(txtApiKey.Text);

                // Add the layer to the overlay we created earlier.
                worldOverlay.Layers.Add("WorldLayer", worldLayer);

                // Set the current extent to the whole world.
                mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);

                // Refresh the map.
                await mapView.RefreshAsync();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://developers.google.com/maps/documentation/maps-static/get-api-key"));
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
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnActivate = new System.Windows.Forms.Button();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.mapView.Size = new System.Drawing.Size(868, 568);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.btnActivate);
            this.panel1.Controls.Add(this.txtApiKey);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(871, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 568);
            this.panel1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(14, 53);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(272, 20);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sign up for a Google Maps API Key";
            // 
            // btnActivate
            // 
            this.btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.Location = new System.Drawing.Point(3, 154);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(294, 35);
            this.btnActivate.TabIndex = 6;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // txtPrivateKey
            // 
            this.txtApiKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApiKey.Location = new System.Drawing.Point(3, 121);
            this.txtApiKey.Name = "txtPrivateKey";
            this.txtApiKey.Size = new System.Drawing.Size(294, 27);
            this.txtApiKey.TabIndex = 5;
            this.txtApiKey.TextChanged += TxtPrivateKey_TextChanged;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Api Key";
            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Google API Key:";
            // 
            // GoogleMapLayerSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GoogleMapLayerSample";
            this.Size = new System.Drawing.Size(1172, 568);
            this.Load += Form_Load;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}