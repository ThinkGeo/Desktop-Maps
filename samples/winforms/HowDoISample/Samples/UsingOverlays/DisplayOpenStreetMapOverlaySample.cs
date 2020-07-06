using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayOpenStreetMapOverlaySample: UserControl
    {
        public DisplayOpenStreetMapOverlaySample()
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
        private Button displayOsmMaps;
        private TextBox osmUserAgent;
        private Label label2;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.osmUserAgent = new System.Windows.Forms.TextBox();
            this.displayOsmMaps = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1269, 576);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.displayOsmMaps);
            this.panel1.Controls.Add(this.osmUserAgent);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(968, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 576);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "OpenStreetMaps:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "User Agent:";
            // 
            // osmUserAgent
            // 
            this.osmUserAgent.Location = new System.Drawing.Point(110, 69);
            this.osmUserAgent.Name = "osmUserAgent";
            this.osmUserAgent.Size = new System.Drawing.Size(188, 22);
            this.osmUserAgent.TabIndex = 2;
            // 
            // displayOsmMaps
            // 
            this.displayOsmMaps.Location = new System.Drawing.Point(3, 108);
            this.displayOsmMaps.Name = "displayOsmMaps";
            this.displayOsmMaps.Size = new System.Drawing.Size(295, 36);
            this.displayOsmMaps.TabIndex = 3;
            this.displayOsmMaps.Text = "Update";
            this.displayOsmMaps.UseVisualStyleBackColor = true;
            this.displayOsmMaps.Click += new System.EventHandler(this.displayOsmMaps_Click);
            // 
            // DisplayOpenStreetMapOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayOpenStreetMapOverlaySample";
            this.Size = new System.Drawing.Size(1269, 576);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void displayOsmMaps_Click(object sender, EventArgs e)
        {
            OpenStreetMapOverlay osmMapsOverlay = new OpenStreetMapOverlay(osmUserAgent.Text);
            mapView.Overlays.Add(osmMapsOverlay);
            mapView.Refresh();
        }
    }
}