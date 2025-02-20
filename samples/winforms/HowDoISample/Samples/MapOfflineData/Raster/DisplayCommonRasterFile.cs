using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to display common raster layers on the map
    /// </summary>
    public class DisplayCommonRasterFile : UserControl
    {
        private LayerOverlay rasterOverlay;
        private SkiaRasterLayer skiaRasterLayer;
        private WpfRasterLayer wpfRasterLayer;

        public DisplayCommonRasterFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the common raster layer to the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create an overlay for the raster layers
            rasterOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            mapView.Overlays.Add(rasterOverlay);

            // Path to the raster file
            string skiaRasterFilePath = "./Data/Jpg/m_3309650_sw_14_1_20160911_20161121.jpg";
            string wpfRasterFilePath = "./Data/GeoTiff/m_3309650_sw_14_1_20160911_20161121.tif";

            // Initialize SkiaRasterLayer and WpfRasterLayer
            skiaRasterLayer = new SkiaRasterLayer(skiaRasterFilePath);
            wpfRasterLayer = new WpfRasterLayer(wpfRasterFilePath);

            // Set default layer to SkiaRasterLayer
            rasterOverlay.Layers.Add(skiaRasterLayer);

            // Set the map view current extent to a slightly zoomed in area of the image.
            mapView.CurrentExtent = new RectangleShape(-10782910.2966461, 3918274.29233111, -10776309.4670677, 3913119.9131963);

            await mapView.RefreshAsync();
        }

        private async void SwitchRasterLayer_OnCheckedChanged(object sender, EventArgs e)
        {
            if (rasterOverlay == null) return;
            var selectedRadioButton = sender as RadioButton;

            rasterOverlay.Layers.Clear();

            if (selectedRadioButton != null)
            {
                switch (selectedRadioButton.Text)
                {
                    case "SkiaRasterLayer":
                        rasterOverlay.Layers.Add(skiaRasterLayer);
                        break;
                    case "WpfRasterLayer":
                        rasterOverlay.Layers.Add(wpfRasterLayer);
                        break;
                }
            }

            await mapView.RefreshAsync();
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            label1 = new Label();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(1165, 646);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(965, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 3;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(20, 48);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(161, 24);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "SkiaRasterLayer";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += SwitchRasterLayer_OnCheckedChanged; 
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(20, 85);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(196, 24);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "WpfRasterLayer";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += SwitchRasterLayer_OnCheckedChanged; 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(15, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(162, 25);
            label1.TabIndex = 0;
            label1.Text = "Use Raster Layer";
            // 
            // DisplayCommonRasterFile
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "DisplayCommonRasterFile";
            Size = new System.Drawing.Size(1165, 646);
            Load += new System.EventHandler(Form_Load);
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}