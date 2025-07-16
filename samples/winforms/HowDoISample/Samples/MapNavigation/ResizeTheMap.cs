using System;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn to resize the map while preserving the world extent with various map resize modes.
    /// </summary>
    public partial class ResizeTheMap : UserControl
    {
        public ResizeTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10336000, 5260000);
            mapView.CurrentScale = 37000000;

            _ = mapView.RefreshAsync();
        }

        private void ToggleButton_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;
            switch (radioButton.Text)
            {
                case "PreserveScale":
                    mapView.MapResizeMode = MapResizeMode.PreserveScale;
                    break;
                case "PreserveScaleAndCenter":
                    mapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
                    break;
                case "PreserveExtent":
                    mapView.MapResizeMode = MapResizeMode.PreserveExtent;
                    break;
            }
        }

        #region Component Designer generated code

        private MapView mapView;
        private FlowLayoutPanel consolePanel;
        private Label mapResizeModeLabel;
        private RadioButton preserveScaleRadioButton;
        private RadioButton preserveScaleAndCenterRadioButton;
        private RadioButton preserveExtentRadioButton;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            consolePanel = new FlowLayoutPanel();
            mapResizeModeLabel = new Label();
            preserveScaleRadioButton = new RadioButton();
            preserveScaleAndCenterRadioButton = new RadioButton();
            preserveExtentRadioButton = new RadioButton();
            consolePanel.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Dock = DockStyle.Fill;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.AutoSize = true;
            consolePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            consolePanel.BackColor = SystemColors.ControlDark;
            consolePanel.Name = "consolePanel";
            consolePanel.FlowDirection = FlowDirection.TopDown;
            consolePanel.Controls.Add(mapResizeModeLabel);
            consolePanel.Controls.Add(preserveScaleRadioButton);
            consolePanel.Controls.Add(preserveScaleAndCenterRadioButton);
            consolePanel.Controls.Add(preserveExtentRadioButton);// 
            consolePanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            consolePanel.Left = 20;
            consolePanel.Top = mapView.Height - 60;
            // mapResizeModeLabel
            // 
            mapResizeModeLabel.AutoSize = true;
            mapResizeModeLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            mapResizeModeLabel.Location = new Point(14, 13);
            mapResizeModeLabel.Name = "mapResizeModeLabel";
            mapResizeModeLabel.Text = "Map Resize Mode:";
            // 
            // preserveScaleRadioButton
            // 
            preserveScaleRadioButton.AutoSize = true;
            preserveScaleRadioButton.Location = new Point(14, 38);
            preserveScaleRadioButton.Name = "preserveScaleRadioButton";
            preserveScaleRadioButton.TabStop = true;
            preserveScaleRadioButton.Text = "PreserveScale";
            preserveScaleRadioButton.UseVisualStyleBackColor = true;
            preserveScaleRadioButton.CheckedChanged += ToggleButton_CheckedChanged;
            preserveScaleRadioButton.Checked = true;
            preserveScaleRadioButton.TabIndex = 1;
            // 
            // preserveScaleAndCenterRadioButton
            // 
            preserveScaleAndCenterRadioButton.AutoSize = true;
            preserveScaleAndCenterRadioButton.Location = new Point(14, 59);
            preserveScaleAndCenterRadioButton.Name = "preserveScaleAndCenterRadioButton";
            preserveScaleAndCenterRadioButton.TabStop = true;
            preserveScaleAndCenterRadioButton.Text = "PreserveScaleAndCenter";
            preserveScaleAndCenterRadioButton.UseVisualStyleBackColor = true;
            preserveScaleAndCenterRadioButton.CheckedChanged += ToggleButton_CheckedChanged;
            preserveScaleAndCenterRadioButton.TabIndex = 2;
            // 
            // preserveExtentRadioButton
            // 
            preserveExtentRadioButton.AutoSize = true;
            preserveExtentRadioButton.Location = new Point(14, 81);
            preserveExtentRadioButton.Name = "preserveExtentRadioButton";
            preserveExtentRadioButton.TabStop = true;
            preserveExtentRadioButton.Text = "PreserveExtent";
            preserveExtentRadioButton.UseVisualStyleBackColor = true;
            preserveExtentRadioButton.CheckedChanged += ToggleButton_CheckedChanged;
            preserveExtentRadioButton.TabIndex = 3;
            // 
            // ResizeTheMap
            // 
            Controls.Add(mapView);
            Controls.Add(consolePanel);
            Name = "ResizeTheMap";
            Size = new Size(1257, 669);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            ResumeLayout(false);

            consolePanel.BringToFront();
        }

        #endregion Component Designer generated code
    }
}
