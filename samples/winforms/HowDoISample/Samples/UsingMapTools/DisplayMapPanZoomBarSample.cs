using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayMapPanZoomBarSample: UserControl
    {
        public DisplayMapPanZoomBarSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
        }

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DisplayPanZoomBar = new System.Windows.Forms.CheckBox();
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
            this.mapView.Size = new System.Drawing.Size(994, 730);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.DisplayPanZoomBar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1000, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 730);
            this.panel1.TabIndex = 1;
            // 
            // DisplayPanZoomBar
            // 
            this.DisplayPanZoomBar.AutoSize = true;
            this.DisplayPanZoomBar.Checked = true;
            this.DisplayPanZoomBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DisplayPanZoomBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DisplayPanZoomBar.ForeColor = System.Drawing.Color.White;
            this.DisplayPanZoomBar.Location = new System.Drawing.Point(19, 57);
            this.DisplayPanZoomBar.Name = "DisplayPanZoomBar";
            this.DisplayPanZoomBar.Size = new System.Drawing.Size(189, 24);
            this.DisplayPanZoomBar.TabIndex = 1;
            this.DisplayPanZoomBar.Text = "Display PanZoomBar";
            this.DisplayPanZoomBar.UseVisualStyleBackColor = true;
            this.DisplayPanZoomBar.CheckedChanged += new System.EventHandler(this.DisplayPanZoomBar_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "PanZoomBar Controls:";
            // 
            // DisplayMapPanZoomBarSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayMapPanZoomBarSample";
            this.Size = new System.Drawing.Size(1300, 730);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        private void DisplayPanZoomBar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.Checked == true)
            {
                mapView.MapTools.PanZoomBar.IsEnabled = true;

            }
            else
            {
                mapView.MapTools.PanZoomBar.IsEnabled = false;

            }
        }

        private Panel panel1;
        private CheckBox DisplayPanZoomBar;
        private Label label1;

        #region Component Designer generated code
        #endregion Component Designer generated code

    }
}