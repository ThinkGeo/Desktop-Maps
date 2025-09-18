using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTSAsXYZLayer : UserControl
    {
        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();
        private WmtsAsyncLayer wmtsAsyncLayer;
        private int _logIndex = 0;
        private bool _initialized;

        public WMTSAsXYZLayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var layerOverlay = new LayerOverlay();
                mapView.Overlays.Add(layerOverlay);
                wmtsAsyncLayer = new WmtsAsyncLayer(new Uri("https://wmts.geo.admin.ch/1.0.0"));
                wmtsAsyncLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
                wmtsAsyncLayer.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
                wmtsAsyncLayer.ActiveStyleName = "default";
                wmtsAsyncLayer.OutputFormat = "image/png";
                wmtsAsyncLayer.TileMatrixSetName = "21781_26";

                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(wmtsAsyncLayer);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache", "wmts_async_layer");

                if (!System.IO.Directory.Exists(cachePath))
                {
                    System.IO.Directory.CreateDirectory(cachePath);
                }

                ThinkGeoDebugger.DisplayTileId = true;

                wmtsAsyncLayer.TileCache = new FileRasterTileCache(cachePath, "raw");
                wmtsAsyncLayer.ProjectedTileCache = new FileRasterTileCache(cachePath, "projected");

                wmtsAsyncLayer.TileCache.GottenTile += TileCache_GottenCacheTile;
                wmtsAsyncLayer.ProjectedTileCache.GottenTile += ProjectedTileCache_GottenCacheTile;

                await wmtsAsyncLayer.OpenAsync();
                // Create a zoomlevelSet from the WMTS server
                mapView.ZoomScales = GetZoomScalesFromWmtsServer();

                var wmtsAsyncLayerBBox = wmtsAsyncLayer.GetBoundingBox();
                mapView.CenterPoint = wmtsAsyncLayerBBox.GetCenterPoint();
                mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, wmtsAsyncLayerBBox, mapView.MapWidth, mapView.MapHeight);

                _initialized = true;
                await mapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private Collection<double> GetZoomScalesFromWmtsServer()
        {
            var matrices = wmtsAsyncLayer.GetTileMatrixSets()[wmtsAsyncLayer.TileMatrixSetName].TileMatrices;

            var scales = new Collection<double>();
            foreach (var matrix in matrices)
            {
                scales.Add(matrix.Scale);
            }

            return scales;
        }

        private void ProjectedTileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                var message = e.Tile.Content == null ? "Projected Tle Not Exist: " : "Projected Tile From Cache: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            }));
        }

        private void TileCache_GottenCacheTile(object sender, GottenTileTileCacheEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                var message = e.Tile.Content == null ? "Tile From Source: " : "Tile From Cache: ";
                message += $"{e.Tile.ZoomIndex}-{e.Tile.X}-{e.Tile.Y}";

                AppendLog(message);
            }));
        }

        private async void Projection_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (wmtsAsyncLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "21781":
                        mapView.MapUnit = GeographyUnit.Meter;
                        wmtsAsyncLayer.ProjectionConverter = null;
                        break;

                    case "4326":
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        var currentCrs = wmtsAsyncLayer.GetTileMatrixSets()[wmtsAsyncLayer.TileMatrixSetName].SupportedCrs;
                        wmtsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(currentCrs, 4326);
                        break;

                    default:
                        return;
                }

                await wmtsAsyncLayer.CloseAsync();
                await wmtsAsyncLayer.OpenAsync();
                var wmtsAsyncLayerBBox = wmtsAsyncLayer.GetBoundingBox();
                mapView.CenterPoint = wmtsAsyncLayerBBox.GetCenterPoint();
                mapView.CurrentScale = MapUtil.GetScale(mapView.MapUnit, wmtsAsyncLayerBBox, mapView.MapWidth, mapView.MapHeight);
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

            wmtsAsyncLayer.RenderBeyondMaxZoom = checkBox.Checked;

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
        private RadioButton epsg21781RadioButton;
        private RadioButton epsg4326RadioButton;
        private CheckBox showDebugInfoCheckBox;
        private CheckBox renderBeyondMaxZoomCheckBox;
        private ListBox logMessagesListBox;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            projectionLabel = new Label();
            epsg21781RadioButton = new RadioButton();
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
            consolePanel.Controls.Add(epsg21781RadioButton);
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
            // epsg21781RadioButton
            // 
            epsg21781RadioButton.AutoSize = true;
            epsg21781RadioButton.Font = new Font("Microsoft Sans Serif", 8F);
            epsg21781RadioButton.ForeColor = Color.White;
            epsg21781RadioButton.Location = new Point(30, 49);
            epsg21781RadioButton.Name = "epsg21781RadioButton";
            epsg21781RadioButton.Size = new Size(94, 19);
            epsg21781RadioButton.TabStop = true;
            epsg21781RadioButton.Text = "EPSG 21781";
            epsg21781RadioButton.UseVisualStyleBackColor = true;
            epsg21781RadioButton.Checked = true;
            epsg21781RadioButton.Tag = "21781";
            epsg21781RadioButton.CheckedChanged += Projection_CheckedChanged;
            epsg21781RadioButton.TabIndex = 3;
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
            // WMTSAsXYZLayer
            // 
            AutoSize = true;
            Controls.Add(mapView);
            Controls.Add(consolePanel);
            Name = "WMTSAsXYZLayer";
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
}
