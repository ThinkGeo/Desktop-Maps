using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class MouseCoordinate : UserControl
    {
        public MouseCoordinate()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-17336118, 20037508, 11623981, -16888303);
            mapView.MapTools.MouseCoordinate.IsEnabled = true;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            mapView.Refresh();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            mapView.MapTools.MouseCoordinate.CustomFormatted -= new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
            RadioButton button = (RadioButton)sender;
            switch (button.Tag.ToString())
            {
                case "0":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;

                case "1":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;
                    break;

                case "2":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;

                case "3":
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    mapView.MapTools.MouseCoordinate.CustomFormatted += new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
                    break;
            }
        }

        private void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
            ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Red);
            e.Result = string.Format(CultureInfo.InvariantCulture, "{0},{1}", e.WorldCoordinate.X.ToString("N0"), e.WorldCoordinate.Y.ToString("N0"));
        }

        private Panel panel1;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(682, 478);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(485, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 108);
            this.panel1.TabIndex = 1;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(13, 78);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(60, 17);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.Tag = "3";
            this.radioButton4.Text = "Custom";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(13, 58);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(168, 17);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.Tag = "2";
            this.radioButton3.Text = "Degrees && Minutes && Seconds";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(13, 35);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(122, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Tag = "1";
            this.radioButton2.Text = "Latitude && Longitude";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(122, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "0";
            this.radioButton1.Text = "Longitude && Latitude";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // MouseCoordinate
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "MouseCoordinate";
            this.Size = new System.Drawing.Size(682, 478);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        
    }
}