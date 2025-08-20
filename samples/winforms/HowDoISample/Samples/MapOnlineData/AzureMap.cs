using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class AzureMap : UserControl
    {
        public AzureMap()
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
            Process.Start(new ProcessStartInfo("https://portal.azure.com/#browse/Microsoft.Maps%2Faccounts") { UseShellExecute = true });
        }

        private async void btnActivate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationID.Text) || mapView.Overlays.Contains("Azure Map")) return;
            btnActivate.Enabled = false;

            // Create the layer overlay with some additional settings and add to the map.
            var layerOverlay = new LayerOverlay()
            {
                TileHeight = 256,
                TileWidth = 256,
            };
            mapView.Overlays.Add("Azure Map", layerOverlay);

            // Create the bing map layer and add it to the map.
            var azureMapsLayer = new AzureMapsRasterAsyncLayer(txtApplicationID.Text, AzureMapsRasterTileSet.Imagery)
            {
                TileCache = new FileRasterTileCache(@".\cache", "azureMapsImagery")
            };
            layerOverlay.Layers.Add(azureMapsLayer);

            // Refresh the map.
            await mapView.RefreshAsync();
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
            mapView = new MapView();
            panel1 = new Panel();
            linkLabel1 = new LinkLabel();
            btnActivate = new Button();
            txtApplicationID = new TextBox();
            label3 = new Label();
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
            mapView.Size = new System.Drawing.Size(990, 599);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(btnActivate);
            panel1.Controls.Add(txtApplicationID);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(993, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(303, 599);
            panel1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            linkLabel1.ForeColor = System.Drawing.Color.White;
            linkLabel1.LinkColor = System.Drawing.Color.White;
            linkLabel1.Location = new System.Drawing.Point(14, 58);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(251, 20);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Sign up for an Azure Maps Api Key";
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            // 
            // btnActivate
            // 
            btnActivate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnActivate.Location = new System.Drawing.Point(3, 139);
            btnActivate.Name = "btnActivate";
            btnActivate.Enabled = false;
            btnActivate.Size = new System.Drawing.Size(297, 34);
            btnActivate.TabIndex = 4;
            btnActivate.Text = "Activate";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += new EventHandler(btnActivate_Click);
            // 
            // txtApplicationID
            // 
            txtApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtApplicationID.Location = new System.Drawing.Point(3, 110);
            txtApplicationID.Name = "txtApplicationID";
            txtApplicationID.Size = new System.Drawing.Size(297, 27);
            txtApplicationID.TextChanged += TxtApplicationID_TextChanged;
            txtApplicationID.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(20, 89);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(113, 20);
            label3.TabIndex = 2;
            label3.Text = "Application ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(14, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(112, 20);
            label1.TabIndex = 0;
            label1.Text = "Azure API Key:";
            // 
            // AzureMap
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "AzureMap";
            Size = new System.Drawing.Size(1296, 599);
            Load += Form_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code


    }
}