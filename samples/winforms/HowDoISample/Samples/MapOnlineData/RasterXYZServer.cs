using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class RasterXYZServer : UserControl
    {
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private int _logIndex;
        private bool _initialized;
        private bool _disposed;

        public RasterXYZServer()
        {
            InitializeComponent();

            // Attach event handler for visibility change
            this.VisibleChanged += (s, e) =>
            {
                if (!this.Visible)
                {
                    Dispose();
                }
            };
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
            };
            layerOverlay.Layers.Add(_thinkGeoRasterMapsAsyncLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "thinkgeo_raster_maps_online_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            _thinkGeoRasterMapsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            _thinkGeoRasterMapsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            _thinkGeoRasterMapsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

            mapView.CenterPoint = MaxExtents.ThinkGeoMaps.GetCenterPoint();
            mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, MaxExtents.ThinkGeoMaps, mapView.MapWidth, mapView.MapHeight);

            _initialized = true;
            _ = mapView.RefreshAsync();
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                var message = e.Tile.Content == null ? "Projection Cache Not Hit: " : "Projection Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            }));
        }

        private void TileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                var message = e.Tile.Content == null ? "Cache Not Hit: " : "Cache Hit: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            }));
        }

        private async void Projection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_thinkGeoRasterMapsAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        mapView.MapUnit = GeographyUnit.Meter;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
                await _thinkGeoRasterMapsAsyncLayer.OpenAsync();
                var thinkGeoRasterMapsAsyncLayerBBox = _thinkGeoRasterMapsAsyncLayer.GetBoundingBox();
                mapView.CenterPoint = thinkGeoRasterMapsAsyncLayerBBox.GetCenterPoint();
                mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, thinkGeoRasterMapsAsyncLayerBBox, mapView.MapWidth, mapView.MapHeight);
                await mapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void RenderBeyondMaxZoomCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is CheckBox checkBox))
                return;

            _thinkGeoRasterMapsAsyncLayer.RenderBeyondMaxZoom = checkBox.Checked;
            _ = mapView.RefreshAsync();
        }

        private void DisplayTileIdCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            if (!(sender is CheckBox checkBox))
                return;

            if (ThinkGeoDebugger.DisplayTileId != checkBox.Checked)
            {
                ThinkGeoDebugger.DisplayTileId = checkBox.Checked;
                _ = mapView.RefreshAsync();
            }
        }

        public void AppendLog(string message)
        {
            logMessagesListBox.Items.Add($"{_logIndex++}: {message}");
            logMessagesListBox.TopIndex = logMessagesListBox.Items.Count - 1;
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ThinkGeoDebugger.DisplayTileId = false;

                    if (mapView != null)
                    {
                        mapView.Overlays.Clear();
                        mapView.Refresh();
                    }
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private Label projectionLabel;
        private RadioButton epsg3857RadioButton;
        private RadioButton epsg4326RadioButton;
        private CheckBox showDebugInfoCheckBox;
        private CheckBox renderBeyondMaxZoomCheckBox;
        private ListBox logMessagesListBox;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            projectionLabel = new Label();
            epsg3857RadioButton = new RadioButton();
            epsg4326RadioButton = new RadioButton();
            showDebugInfoCheckBox = new CheckBox();
            renderBeyondMaxZoomCheckBox = new CheckBox();
            logMessagesListBox = new ListBox();
            consolePanel.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(4, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.Margin = new System.Windows.Forms.Padding(0);
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(946, 634);
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.BackColor = Color.Gray;
            consolePanel.Controls.Add(logMessagesListBox);
            consolePanel.Controls.Add(renderBeyondMaxZoomCheckBox);
            consolePanel.Controls.Add(showDebugInfoCheckBox);
            consolePanel.Controls.Add(epsg4326RadioButton);
            consolePanel.Controls.Add(epsg3857RadioButton);
            consolePanel.Controls.Add(projectionLabel);
            consolePanel.Dock = DockStyle.Right;
            consolePanel.Location = new Point(953, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new Size(302, 634);
            consolePanel.TabIndex = 1;
            // 
            // projectionLabel
            // 
            projectionLabel.AutoSize = true;
            projectionLabel.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            projectionLabel.ForeColor = Color.White;
            projectionLabel.Location = new Point(14, 17);
            projectionLabel.Name = "projectionLabel";
            projectionLabel.Size = new Size(89, 20);
            projectionLabel.TabIndex = 0;
            projectionLabel.Text = "Projection";
            // 
            // epsg3857RadioButton
            // 
            epsg3857RadioButton.AutoSize = true;
            epsg3857RadioButton.Font = new Font("Microsoft Sans Serif", 8F);
            epsg3857RadioButton.ForeColor = Color.White;
            epsg3857RadioButton.Location = new Point(30, 49);
            epsg3857RadioButton.Name = "epsg3857RadioButton";
            epsg3857RadioButton.Size = new Size(94, 19);
            epsg3857RadioButton.TabIndex = 1;
            epsg3857RadioButton.TabStop = true;
            epsg3857RadioButton.Text = "EPSG 3857";
            epsg3857RadioButton.UseVisualStyleBackColor = true;
            epsg3857RadioButton.Checked = true;
            epsg3857RadioButton.Tag = "3857";
            epsg3857RadioButton.CheckedChanged += Projection_CheckedChanged;
            // 
            // epsg4326RadioButton
            // 
            epsg4326RadioButton.AutoSize = true;
            epsg4326RadioButton.Font = new Font("Microsoft Sans Serif", 8F);
            epsg4326RadioButton.ForeColor = Color.White;
            epsg4326RadioButton.Location = new Point(30, 74);
            epsg4326RadioButton.Name = "epsg4326RadioButton";
            epsg4326RadioButton.Size = new Size(94, 19);
            epsg4326RadioButton.TabIndex = 2;
            epsg4326RadioButton.TabStop = true;
            epsg4326RadioButton.Text = "EPSG 4326";
            epsg4326RadioButton.UseVisualStyleBackColor = true;
            epsg4326RadioButton.Tag = "4326";
            epsg4326RadioButton.CheckedChanged += Projection_CheckedChanged;
            // 
            // showDebugInfoCheckBox
            // 
            showDebugInfoCheckBox.AutoSize = true;
            showDebugInfoCheckBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            showDebugInfoCheckBox.ForeColor = Color.White;
            showDebugInfoCheckBox.Location = new Point(25, 105);
            showDebugInfoCheckBox.Name = "showDebugInfoCheckBox";
            showDebugInfoCheckBox.Size = new Size(83, 19);
            showDebugInfoCheckBox.TabIndex = 3;
            showDebugInfoCheckBox.Text = "Show Debug Info";
            showDebugInfoCheckBox.UseVisualStyleBackColor = true;
            showDebugInfoCheckBox.Checked = true;
            showDebugInfoCheckBox.CheckedChanged += DisplayTileIdCheckBox_CheckedChanged;
            // 
            // renderBeyondMaxZoomCheckBox
            // 
            renderBeyondMaxZoomCheckBox.AutoSize = true;
            renderBeyondMaxZoomCheckBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            renderBeyondMaxZoomCheckBox.ForeColor = Color.White;
            renderBeyondMaxZoomCheckBox.Location = new Point(25, 130);
            renderBeyondMaxZoomCheckBox.Name = "renderBeyondMaxZoomCheckBox";
            renderBeyondMaxZoomCheckBox.Size = new Size(83, 19);
            renderBeyondMaxZoomCheckBox.TabIndex = 4;
            renderBeyondMaxZoomCheckBox.Text = "Render Beyond Max Zoom";
            renderBeyondMaxZoomCheckBox.UseVisualStyleBackColor = true;
            renderBeyondMaxZoomCheckBox.CheckedChanged += RenderBeyondMaxZoomCheckBox_CheckedChanged;
            // 
            // logMessagesListBox
            // 
            logMessagesListBox.FormattingEnabled = true;
            logMessagesListBox.ItemHeight = 15;
            logMessagesListBox.Location = new Point(3, 159);
            logMessagesListBox.Name = "logMessagesListBox";
            logMessagesListBox.Size = new Size(296, 469);
            logMessagesListBox.TabIndex = 5;
            logMessagesListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // RasterXYZServer
            // 
            AutoSize = true;
            Controls.Add(mapView);
            Controls.Add(consolePanel);
            Name = "RasterXYZServer";
            Size = new Size(1255, 634);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
