using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class BingMapLayerSample : UserControl 
    {
        public BingMapLayerSample()
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


        private void TxtApplicationID_TextChanged(object sender, EventArgs e)
        {
            btnActivate.Enabled = txtApplicationID.Text.Length > 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.bingmapsportal.com/"));
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (txtApplicationID.Text != null && !mapView.Overlays.Contains("Bing Map"))
            {
                btnActivate.Enabled = false;
                // Set the map zoom level set to the bing map zoom level set so all the zoom levels line up.
                mapView.ZoomLevelSet = new BingMapsZoomLevelSet();

                // Create the layer overlay with some additional settings and add to the map.
                LayerOverlay layerOverlay = new LayerOverlay() { TileHeight = 256, TileWidth = 256 };
                layerOverlay.TileSizeMode = TileSizeMode.Small;
                layerOverlay.MaxExtent = MaxExtents.BingMaps;
                mapView.Overlays.Add("Bing Map", layerOverlay);

                // Create the bing map layer and add it to the map.
                BingMapsLayer bingMapsLayer = new BingMapsLayer(txtApplicationID.Text, BingMapsMapType.Road);
                bingMapsLayer.TileCache = new FileRasterTileCache("C:\\temp", "bingMapsRoad");
                layerOverlay.Layers.Add(bingMapsLayer);

                // Refresh the map.
                await mapView.RefreshAsync();
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private LinkLabel linkLabel1;
        private Button btnActivate;
        private TextBox txtApplicationID;
        private Label label3;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnActivate = new System.Windows.Forms.Button();
            this.txtApplicationID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.mapView.Size = new System.Drawing.Size(990, 599);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.btnActivate);
            this.panel1.Controls.Add(this.txtApplicationID);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(993, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 599);
            this.panel1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.White;
            this.linkLabel1.LinkColor = System.Drawing.Color.White;
            this.linkLabel1.Location = new System.Drawing.Point(14, 58);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(251, 20);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Sign up for a Bing Maps Api Key";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnActivate
            // 
            this.btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.Location = new System.Drawing.Point(3, 139);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Enabled = false;
            this.btnActivate.Size = new System.Drawing.Size(297, 34);
            this.btnActivate.TabIndex = 4;
            this.btnActivate.Text = "Activate";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // txtApplicationID
            // 
            this.txtApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApplicationID.Location = new System.Drawing.Point(3, 110);
            this.txtApplicationID.Name = "txtApplicationID";
            this.txtApplicationID.Size = new System.Drawing.Size(297, 27);
            this.txtApplicationID.TextChanged += TxtApplicationID_TextChanged;
            this.txtApplicationID.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Application ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bing API Key:";
            // 
            // BingMapLayerSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "BingMapLayerSample";
            this.Size = new System.Drawing.Size(1296, 599);
            this.Load += Form_Load;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code


    }
}