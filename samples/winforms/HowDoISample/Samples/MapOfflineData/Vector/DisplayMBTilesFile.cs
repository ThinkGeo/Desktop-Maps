using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMBTilesFile : UserControl
    {
        public DisplayMBTilesFile()
        {
            InitializeComponent();
        }

        LayerOverlay _layerOverlay;

        private async void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            _layerOverlay = new LayerOverlay();
            _layerOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add(_layerOverlay);

            var openstackMbtiles = new VectorMbTilesAsyncLayer(@"../../../Data\Mbtiles\maplibre.mbtiles", @"../../../Data\Mbtiles\style.json");
            _layerOverlay.Layers.Add(openstackMbtiles);

            mapView.CurrentExtent = MaxExtents.SphericalMercator;
            await openstackMbtiles.OpenAsync();

            checkBox1.Checked = true; // Assuming you want to show Tile ID by default
            ThinkGeoDebugger.DisplayTileId = checkBox1.Checked;

            await mapView.RefreshAsync();
        }

        private async void ShowTileID_CheckedChanged(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = (sender as CheckBox).Checked == true;
            if (mapView != null)
                await mapView.RefreshAsync();
        }

        private async void SwitchTileSize_OnCheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton == null)
                return;

            if (!radioButton.Checked)
                return;

            if (mapView.Overlays.Count <= 0) return;

            if (!(_layerOverlay.Layers[0] is VectorMbTilesAsyncLayer mbTilesLayer))
                return;

            var content = ((RadioButton)sender).Tag.ToString();
            {
                if (content == "256")
                {
                    var zoomLevelSet = new SphericalMercatorZoomLevelSet(256);
                    mapView.ZoomScales = zoomLevelSet.GetScales();
                    _layerOverlay.TileType = TileType.MultiTile;
                    _layerOverlay.TileWidth = 256;
                    _layerOverlay.TileHeight = 256;
                    await mbTilesLayer.CloseAsync();
                    mbTilesLayer.TileWidth = 256;
                    mbTilesLayer.TileHeight = 256;
                    await mbTilesLayer.OpenAsync();

                }
                else if (content == "512")
                {
                    var zoomLevelSet = new SphericalMercatorZoomLevelSet(512);
                    mapView.ZoomScales = zoomLevelSet.GetScales();
                    _layerOverlay.TileType = TileType.MultiTile;
                    _layerOverlay.TileWidth = 512;
                    _layerOverlay.TileHeight = 512;
                    await mbTilesLayer.CloseAsync();
                    mbTilesLayer.TileWidth = 512;
                    mbTilesLayer.TileHeight = 512;
                    await mbTilesLayer.OpenAsync();
                }
            }
            await mapView.RefreshAsync();
        }

        private void DisplayMBTilesFile_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                ThinkGeoDebugger.DisplayTileId = false;
            }
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private CheckBox checkBox1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            checkBox1 = new CheckBox();
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
            mapView.Location = new System.Drawing.Point(4, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(1050, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(checkBox1);
            panel1.Location = new System.Drawing.Point(1050, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 1;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Checked = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(20, 85);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(196, 24);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "512 * 512";
            radioButton2.Tag = "512";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += SwitchTileSize_OnCheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(20, 48);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(161, 24);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "256 * 256";
            radioButton1.Tag = "256";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += SwitchTileSize_OnCheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBox1.ForeColor = System.Drawing.Color.White;
            checkBox1.Location = new System.Drawing.Point(15, 10);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(162, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Show Tile ID";
            checkBox1.CheckedChanged += ShowTileID_CheckedChanged;
            // 
            // DisplayMBTilesFile
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "DisplayMBTilesFile";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            //
            // Attach VisibleChanged event
            //
            VisibleChanged += DisplayMBTilesFile_VisibleChanged;
        }

        #endregion Component Designer generated code
    }
}
