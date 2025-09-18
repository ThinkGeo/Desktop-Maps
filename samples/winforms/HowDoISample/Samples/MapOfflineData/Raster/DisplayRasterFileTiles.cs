using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayRasterFileTiles : UserControl
    {
        private XyzFileTilesAsyncLayer fileTilesAsyncLayer;
        private int _logIndex = 0;
        private bool _initialized;

        public DisplayRasterFileTiles()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            if (!Directory.Exists(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS"))
                ZipFile.ExtractToDirectory(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS.zip", @".\Data\OSM_Tiles_z0-z5_Created_By_QGIS");

            var layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);
            fileTilesAsyncLayer = new XyzFileTilesAsyncLayer(@".\Data\OSM_Tiles_z0-z5_Created_By_QGIS");
            fileTilesAsyncLayer.MaxZoomOfTheData = 5; // The MaxZoom with data

            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(fileTilesAsyncLayer);

            string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "raster_file_tiles_layer");

            if (!System.IO.Directory.Exists(cachePath))
            {
                System.IO.Directory.CreateDirectory(cachePath);
            }

            fileTilesAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
            fileTilesAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

            fileTilesAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
            fileTilesAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

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
                if (fileTilesAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        mapView.MapUnit = GeographyUnit.Meter;
                        fileTilesAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        fileTilesAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        break;

                    default:
                        return;
                }

                await fileTilesAsyncLayer.CloseAsync();
                await fileTilesAsyncLayer.OpenAsync();
                var fileTilesAsyncLayerBBox = fileTilesAsyncLayer.GetBoundingBox();
                mapView.CenterPoint = fileTilesAsyncLayerBBox.GetCenterPoint();
                mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, fileTilesAsyncLayerBBox, mapView.MapWidth, mapView.MapHeight);
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

            fileTilesAsyncLayer.RenderBeyondMaxZoom = checkBox.Checked;
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

        private void CacheXyzTiles_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                ThinkGeoDebugger.DisplayTileId = false;
            }
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
            projectionLabel.Text = "Projection";
            projectionLabel.TabIndex = 2;
            // 
            // epsg3857RadioButton
            // 
            epsg3857RadioButton.AutoSize = true;
            epsg3857RadioButton.Font = new Font("Microsoft Sans Serif", 8F);
            epsg3857RadioButton.ForeColor = Color.White;
            epsg3857RadioButton.Location = new Point(30, 49);
            epsg3857RadioButton.Name = "epsg3857RadioButton";
            epsg3857RadioButton.Size = new Size(94, 19);
            epsg3857RadioButton.TabStop = true;
            epsg3857RadioButton.Text = "EPSG 3857";
            epsg3857RadioButton.UseVisualStyleBackColor = true;
            epsg3857RadioButton.Checked = true;
            epsg3857RadioButton.Tag = "3857";
            epsg3857RadioButton.CheckedChanged += Projection_CheckedChanged;
            epsg3857RadioButton.TabIndex = 3;
            // 
            // epsg4326RadioButton
            // 
            epsg4326RadioButton.AutoSize = true;
            epsg4326RadioButton.Font = new Font("Microsoft Sans Serif", 8F);
            epsg4326RadioButton.ForeColor = Color.White;
            epsg4326RadioButton.Location = new Point(30, 74);
            epsg4326RadioButton.Name = "epsg4326RadioButton";
            epsg4326RadioButton.Size = new Size(94, 19);
            epsg4326RadioButton.TabStop = true;
            epsg4326RadioButton.Text = "EPSG 4326";
            epsg4326RadioButton.UseVisualStyleBackColor = true;
            epsg4326RadioButton.Tag = "4326";
            epsg4326RadioButton.CheckedChanged += Projection_CheckedChanged;
            epsg4326RadioButton.TabIndex = 4;
            // 
            // showDebugInfoCheckBox
            // 
            showDebugInfoCheckBox.AutoSize = true;
            showDebugInfoCheckBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            showDebugInfoCheckBox.ForeColor = Color.White;
            showDebugInfoCheckBox.Location = new Point(25, 105);
            showDebugInfoCheckBox.Name = "showDebugInfoCheckBox";
            showDebugInfoCheckBox.Size = new Size(83, 19);
            showDebugInfoCheckBox.Text = "Show Debug Info";
            showDebugInfoCheckBox.UseVisualStyleBackColor = true;
            showDebugInfoCheckBox.Checked = true;
            showDebugInfoCheckBox.CheckedChanged += DisplayTileIdCheckBox_CheckedChanged;
            showDebugInfoCheckBox.TabIndex = 5;
            // 
            // renderBeyondMaxZoomCheckBox
            // 
            renderBeyondMaxZoomCheckBox.AutoSize = true;
            renderBeyondMaxZoomCheckBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            renderBeyondMaxZoomCheckBox.ForeColor = Color.White;
            renderBeyondMaxZoomCheckBox.Location = new Point(25, 130);
            renderBeyondMaxZoomCheckBox.Name = "renderBeyondMaxZoomCheckBox";
            renderBeyondMaxZoomCheckBox.Size = new Size(83, 19);
            renderBeyondMaxZoomCheckBox.Text = "Render Beyond Max Zoom";
            renderBeyondMaxZoomCheckBox.UseVisualStyleBackColor = true;
            renderBeyondMaxZoomCheckBox.CheckedChanged += RenderBeyondMaxZoomCheckBox_CheckedChanged;
            renderBeyondMaxZoomCheckBox.TabIndex = 6;
            // 
            // logMessagesListBox
            // 
            logMessagesListBox.FormattingEnabled = true;
            logMessagesListBox.ItemHeight = 15;
            logMessagesListBox.Location = new Point(3, 159);
            logMessagesListBox.Name = "logMessagesListBox";
            logMessagesListBox.Size = new Size(296, 469);
            logMessagesListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            logMessagesListBox.TabIndex = 7;
            // 
            // RasterFileTiles
            // 
            AutoSize = true;
            Controls.Add(mapView);
            Controls.Add(consolePanel);
            Name = "RasterFileTiles";
            Size = new Size(1255, 634);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
            //
            // Attach VisibleChanged event
            //
            VisibleChanged += CacheXyzTiles_VisibleChanged;
        }

        #endregion Component Designer generated code
    }

    class XyzFileTilesAsyncLayer : RasterXyzTileAsyncLayer
    {
        private string _root;

        public XyzFileTilesAsyncLayer(string tilesFolder)
        {
            _root = tilesFolder;
        }

        protected override Task<RasterTile> GetTileAsyncCore(int zoomLevel, long x, long y, float resolutionFactor, CancellationToken cancellationToken)
        {
            var path = $"{_root}\\{zoomLevel}\\{x}\\{y}.jpg";
            if (!File.Exists(path))
                return Task.FromResult(new RasterTile(null, zoomLevel, x, y));

            var bytes = File.ReadAllBytes(path);
            return Task.FromResult(new RasterTile(bytes, zoomLevel, x, y));
        }
    }

}
