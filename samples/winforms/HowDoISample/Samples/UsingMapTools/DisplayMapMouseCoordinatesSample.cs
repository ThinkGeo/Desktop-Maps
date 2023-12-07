using System;
using System.Windows.Forms;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayMapMouseCoordinatesSample: UserControl
    {
        public DisplayMapMouseCoordinatesSample()
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
            mapView.MapTools.MouseCoordinate.IsEnabled = true;

            await mapView.RefreshAsync();
        }

        private void displayMouseCoordinates_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Checked == true)
            {
                mapView.MapTools.MouseCoordinate.IsEnabled = true;

            }
            else
            {
                mapView.MapTools.MouseCoordinate.IsEnabled = false;

            }
        }

        /// <summary>
        /// Event handler that formats the MouseCoordinates to use WorldCoordinates and changes the Foreground color to red.
        /// Other modifications to the display of the MouseCoordinates can be safely done here.
        /// </summary>
        private void MouseCoordinate_CustomMouseCoordinateFormat(object sender, CustomFormattedMouseCoordinateMapToolEventArgs e)
        {
           ((MouseCoordinateMapTool)sender).Foreground = new SolidColorBrush(Colors.Red);
            e.Result = $"X: {e.WorldCoordinate.X.ToString("N0")}, Y: {e.WorldCoordinate.Y.ToString("N0")}";
        }

        private void coordinateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (coordinateType.Text)
            {
                case "(lat), (lon)":
                    // Set to Lat, Lon format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LatitudeLongitude;
                    break;
                case "(lon), (lat)":
                    // Set to Lon, Lat format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.LongitudeLatitude;
                    break;
                case "(degrees), (minutes), (seconds)":
                    // Set to Degrees, Minutes, Seconds format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.DegreesMinutesSeconds;
                    break;
                case "(custom)":
                    // Set to a custom format
                    mapView.MapTools.MouseCoordinate.MouseCoordinateType = MouseCoordinateType.Custom;
                    // Add an EventHandler to handle what the formatted output should look like
                    mapView.MapTools.MouseCoordinate.CustomFormatted += new System.EventHandler<CustomFormattedMouseCoordinateMapToolEventArgs>(MouseCoordinate_CustomMouseCoordinateFormat);
                    break;
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private ComboBox coordinateType;
        private CheckBox displayMouseCoordinates;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.coordinateType = new System.Windows.Forms.ComboBox();
            this.displayMouseCoordinates = new System.Windows.Forms.CheckBox();
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
            this.mapView.Size = new System.Drawing.Size(993, 534);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.coordinateType);
            this.panel1.Controls.Add(this.displayMouseCoordinates);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.panel1.Location = new System.Drawing.Point(996, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 546);
            this.panel1.TabIndex = 1;
            // 
            // coordinateType
            // 
            this.coordinateType.FormattingEnabled = true;
            this.coordinateType.Items.AddRange(new object[] {
            "(lat), (lon)",
            "(lon), (lat)",
            "(degrees), (minutes), (seconds)",
            "(custom)"});
            this.coordinateType.Location = new System.Drawing.Point(3, 107);
            this.coordinateType.Name = "coordinateType";
            this.coordinateType.Size = new System.Drawing.Size(294, 24);
            this.coordinateType.TabIndex = 2;
            this.coordinateType.Text = "(lat), (lon)";
            this.coordinateType.SelectedIndexChanged += new System.EventHandler(this.coordinateType_SelectedIndexChanged);
            // 
            // displayMouseCoordinates
            // 
            this.displayMouseCoordinates.AutoSize = true;
            this.displayMouseCoordinates.Checked = true;
            this.displayMouseCoordinates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.displayMouseCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.displayMouseCoordinates.ForeColor = System.Drawing.Color.White;
            this.displayMouseCoordinates.Location = new System.Drawing.Point(24, 65);
            this.displayMouseCoordinates.Name = "displayMouseCoordinates";
            this.displayMouseCoordinates.Size = new System.Drawing.Size(199, 21);
            this.displayMouseCoordinates.TabIndex = 1;
            this.displayMouseCoordinates.Text = "Display Mouse Coordinates";
            this.displayMouseCoordinates.UseVisualStyleBackColor = true;
            this.displayMouseCoordinates.CheckedChanged += new System.EventHandler(this.displayMouseCoordinates_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mouse Coordinates Controls:";
            // 
            // DisplayMapMouseCoordinatesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayMapMouseCoordinatesSample";
            this.Size = new System.Drawing.Size(1296, 546);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}