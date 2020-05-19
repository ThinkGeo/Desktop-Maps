using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class PanZoomBar : UserControl
    {
        public PanZoomBar()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);
            mapView.MapTools.PanZoomBar.IsEnabled = true;
            mapView.MapTools.PanZoomBar.DisplayZoomBarText = ZoomBarTextDisplayMode.Display;

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColors.DeepOcean)));
            worldOverlay.Layers.Add(worldLayer);
            mapView.Overlays.Add(worldOverlay);
        }

        private void chbVisibility_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (mapView != null)
            {
                mapView.MapTools.PanZoomBar.IsEnabled = (bool)checkBox.Checked;
            }
        }

        private void chbTrackLevel_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (mapView != null)
            {
                mapView.MapTools.PanZoomBar.DisplayZoomBarText =
                    (bool)checkBox.Checked ? ZoomBarTextDisplayMode.Display : ZoomBarTextDisplayMode.None;
            }
        }

       

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private CheckBox chbTrackLevel;
        private CheckBox chbVisibility;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbTrackLevel = new System.Windows.Forms.CheckBox();
            this.chbVisibility = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.UI.WinForms.MapResizeMode.PreserveScale;
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.DecimalDegree;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(798, 486);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chbTrackLevel);
            this.panel1.Controls.Add(this.chbVisibility);
            this.panel1.Location = new System.Drawing.Point(645, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 65);
            this.panel1.TabIndex = 1;
            // 
            // chbTrackLevel
            // 
            this.chbTrackLevel.AutoSize = true;
            this.chbTrackLevel.Checked = true;
            this.chbTrackLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbTrackLevel.Location = new System.Drawing.Point(12, 35);
            this.chbTrackLevel.Name = "chbTrackLevel";
            this.chbTrackLevel.Size = new System.Drawing.Size(130, 17);
            this.chbTrackLevel.TabIndex = 1;
            this.chbTrackLevel.Text = "Enable tracking levels";
            this.chbTrackLevel.UseVisualStyleBackColor = true;
            this.chbTrackLevel.CheckedChanged += new System.EventHandler(this.chbTrackLevel_CheckedChanged);
            // 
            // chbVisibility
            // 
            this.chbVisibility.AutoSize = true;
            this.chbVisibility.Checked = true;
            this.chbVisibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbVisibility.Location = new System.Drawing.Point(12, 12);
            this.chbVisibility.Name = "chbVisibility";
            this.chbVisibility.Size = new System.Drawing.Size(129, 17);
            this.chbVisibility.TabIndex = 0;
            this.chbVisibility.Text = "Pan zoom bar visibility";
            this.chbVisibility.UseVisualStyleBackColor = true;
            this.chbVisibility.CheckedChanged += new System.EventHandler(this.chbVisibility_CheckedChanged);
            // 
            // PanZoomBar
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "PanZoomBar";
            this.Size = new System.Drawing.Size(798, 486);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        
    }
}