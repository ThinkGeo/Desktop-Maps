using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class CacheTileOverlay : UserControl
    {
        private LayerOverlay _layerOverlay;
        private RectangleShape _bbox;
        private int _finishedTileCount = 0;
        private string _cachePath;

        public CacheTileOverlay()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            mapView.MapUnit = GeographyUnit.Meter;

            var streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            streetsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 2, true);
            streetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(streetsLayer);
            mapView.Overlays.Add(_layerOverlay);

            _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

            if (!Directory.Exists(_cachePath))
                Directory.CreateDirectory(_cachePath);

            _layerOverlay.TileCache = new FileRasterTileCache(_cachePath, "overlayCacheTest");
            _layerOverlay.IsCacheOnly = true; // so it will not render but only get the tiles from the cache.

            streetsLayer.Open();
            _bbox = streetsLayer.GetBoundingBox();
            mapView.CurrentExtent = _bbox;

            _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, _bbox, GeographyUnit.Meter, 10);
            mapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();
            _layerOverlay.TileCacheGenerated += _layerOverlay_TileCacheGenerated;

            _ = mapView.RefreshAsync();
        }

        private void _layerOverlay_TileCacheGenerated(object sender, TileCacheGeneratedLayerOverlayEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                _finishedTileCount++;
                progressBar.Maximum = e.TotalTileCount;
                progressBar.Value = _finishedTileCount;
                progressBar.DisplayText = $"{_finishedTileCount} / {e.TotalTileCount}";
            }));
        }

        private void CacheOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_layerOverlay == null)
                return;

            var checkBox = (CheckBox)sender;
            _layerOverlay.IsCacheOnly = checkBox.Checked;
            _ = mapView.RefreshAsync();
        }

        private async void GenerateCacheButton_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.DisplayText = "";
                _finishedTileCount = 0;

                // Optional: clear old cache files before regenerating
                _layerOverlay.TileCache.ClearCache();

                // get the ScaleFactor
                float scaleFactor;
                using (var graphics = this.CreateGraphics())
                {
                    scaleFactor = graphics.DpiX / 96f;
                }

                // generate the cache from minZoomLevel to maxZoomLevel. 
                int minZoomLevel = int.Parse(minZoomTextBox.Text);
                int maxZoomLevel = int.Parse(maxZoomTextBox.Text);

                await Layer.GenerateTileCacheAsync(_layerOverlay.Layers, (FileRasterTileCache)_layerOverlay.TileCache, _layerOverlay.TileMatrixSet,
                    _bbox, GeographyUnit.Meter, minZoomLevel, maxZoomLevel,
                    (e1) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            _finishedTileCount++;
                            progressBar.Maximum = e1.TotalTileCount;
                            progressBar.Value = e1.TilesCompleted;
                            progressBar.DisplayText = $"{e1.TilesCompleted} / {e1.TotalTileCount}";
                        }));
                    }, scaleFactor, OverwriteMode.Overwrite);

                progressBar.Visible = false;

                int targetZoomLevel = minZoomLevel;
                double newScale = _layerOverlay.TileMatrixSet.GetScales()[targetZoomLevel];

                mapView.CurrentScale = newScale;
                mapView.CenterPoint = _bbox.GetCenterPoint();

                await mapView.RefreshAsync();
            }
            catch
            {
                // deal with the exception if needed.
            }
        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            _layerOverlay.TileCache.ClearCache();
            _ = mapView.RefreshAsync();
        }

        private void OpenCacheFolderButton_Click(object sender, EventArgs e)
        {
            var fileRasterTileCache = _layerOverlay.TileCache as FileRasterTileCache;
            if (fileRasterTileCache == null)
                return;

            var cacheFolder = Path.Combine(fileRasterTileCache.CacheDirectory, fileRasterTileCache.CacheId);
            if (Directory.Exists(cacheFolder))
                Process.Start("explorer.exe", cacheFolder);
            else
                MessageBox.Show("Cache Tiles have not been generated");
        }

        private void ToggleMatrixButton_Click(object sender, EventArgs e)
        {
            _layerOverlay.TileCache.ClearCache();

            var button = sender as Button;
            if (button == null)
                return;

            if ((string)button.Text == "Switch to Local Tile Matrix")
            {
                button.Text = "Switch to Global Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, _bbox, GeographyUnit.Meter, 10);
                mapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();

                var zoom = _layerOverlay.TileMatrixSet.GetSnappedZoomIndex(mapView.CurrentScale);
                minZoomTextBox.Text = zoom.ToString();
                maxZoomTextBox.Text = (int.Parse(zoom.ToString()) + 3).ToString();
                zoomRangeGroupBox.Text = "Valid Range: 0–9";
            }
            else
            {
                button.Text = "Switch to Local Tile Matrix";
                _layerOverlay.TileMatrixSet = TileMatrixSet.CreateTileMatrixSet(512, MaxExtents.SphericalMercator, GeographyUnit.Meter);
                mapView.ZoomScales = _layerOverlay.TileMatrixSet.GetScales();

                var zoom = _layerOverlay.TileMatrixSet.GetSnappedZoomIndex(mapView.CurrentScale);
                minZoomTextBox.Text = zoom.ToString();
                maxZoomTextBox.Text = (int.Parse(zoom.ToString()) + 3).ToString();
                zoomRangeGroupBox.Text = "Valid Range: 0–19";
            }

            _ = mapView.RefreshAsync();
        }

        private void CacheTileOverlay_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible)
            {
                ThinkGeoDebugger.DisplayTileId = false;
            }
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private CheckBox cacheOnlyCheckBox;
        private GroupBox zoomRangeGroupBox;
        private Label minZoomLabel;
        private Label maxZoomLabel;
        private TextBox minZoomTextBox;
        private TextBox maxZoomTextBox;
        private Button generateCacheButton;
        private Button clearCacheButton;
        private Button openCacheFolderButton;
        private Button toggleMatrixButton;
        private ProgressBarWithText progressBar;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            cacheOnlyCheckBox = new CheckBox();
            zoomRangeGroupBox = new GroupBox();
            minZoomLabel = new Label();
            maxZoomLabel = new Label();
            minZoomTextBox = new TextBox();
            maxZoomTextBox = new TextBox();
            generateCacheButton = new Button();
            clearCacheButton = new Button();
            openCacheFolderButton = new Button();
            toggleMatrixButton = new Button();
            progressBar = new ProgressBarWithText();
            consolePanel.SuspendLayout();
            zoomRangeGroupBox.SuspendLayout();
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
            // consolePanel
            // 
            consolePanel.BackColor = Color.Gray;
            consolePanel.Location = new Point(1050, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new Size(300, 611);
            consolePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            consolePanel.Controls.Add(cacheOnlyCheckBox);
            consolePanel.Controls.Add(zoomRangeGroupBox);
            consolePanel.Controls.Add(generateCacheButton);
            consolePanel.Controls.Add(clearCacheButton);
            consolePanel.Controls.Add(openCacheFolderButton);
            consolePanel.Controls.Add(toggleMatrixButton);
            consolePanel.TabIndex = 1;
            // 
            // cacheOnlyCheckBox
            // 
            cacheOnlyCheckBox.BackColor = Color.Transparent;
            cacheOnlyCheckBox.Checked = true;
            cacheOnlyCheckBox.Font = new Font("Microsoft Sans Serif", 9F);
            cacheOnlyCheckBox.ForeColor = Color.White;
            cacheOnlyCheckBox.Location = new Point(20, 20);
            cacheOnlyCheckBox.Name = "cacheOnlyCheckBox";
            cacheOnlyCheckBox.Size = new Size(170, 25);
            cacheOnlyCheckBox.Text = "Only Get Tiles from Cache";
            cacheOnlyCheckBox.UseVisualStyleBackColor = false;
            cacheOnlyCheckBox.CheckedChanged += CacheOnlyCheckBox_CheckedChanged;
            cacheOnlyCheckBox.TabIndex = 2;
            // 
            // zoomRangeGroupBox
            // 
            zoomRangeGroupBox.Font = new Font("Microsoft Sans Serif", 9F);
            zoomRangeGroupBox.ForeColor = Color.White;
            zoomRangeGroupBox.Location = new Point(20, 55);
            zoomRangeGroupBox.Name = "zoomRangeGroupBox";
            zoomRangeGroupBox.Padding = new Padding(5);
            zoomRangeGroupBox.Size = new Size(170, 60);
            zoomRangeGroupBox.TabStop = false;
            zoomRangeGroupBox.Text = "Valid Range: 0–9";
            zoomRangeGroupBox.Controls.Add(minZoomLabel);
            zoomRangeGroupBox.Controls.Add(maxZoomLabel);
            zoomRangeGroupBox.Controls.Add(minZoomTextBox);
            zoomRangeGroupBox.Controls.Add(maxZoomTextBox);
            zoomRangeGroupBox.TabIndex = 3;
            // 
            // minZoomLabel
            // 
            minZoomLabel.Font = new Font("Microsoft Sans Serif", 9F);
            minZoomLabel.ForeColor = Color.White;
            minZoomLabel.Location = new Point(10, 30);
            minZoomLabel.Name = "minZoomLabel";
            minZoomLabel.Size = new Size(34, 15);
            minZoomLabel.Text = "Min:";
            minZoomLabel.TabIndex = 4;
            // 
            // maxZoomLabel
            // 
            maxZoomLabel.Font = new Font("Microsoft Sans Serif", 9F);
            maxZoomLabel.ForeColor = Color.White;
            maxZoomLabel.Location = new Point(80, 30);
            maxZoomLabel.Name = "maxZoomLabel";
            maxZoomLabel.Size = new Size(34, 15);
            maxZoomLabel.Text = "Max:";
            maxZoomLabel.TabIndex = 6;
            // 
            // minZoomTextBox
            // 
            minZoomTextBox.Font = new Font("Microsoft Sans Serif", 8F);
            minZoomTextBox.Location = new Point(45, 28);
            minZoomTextBox.Name = "minZoomTextBox";
            minZoomTextBox.Size = new Size(30, 20);
            minZoomTextBox.Text = "0";
            minZoomTextBox.TabIndex = 5;
            // 
            // maxZoomTextBox
            // 
            maxZoomTextBox.Font = new Font("Microsoft Sans Serif", 8F);
            maxZoomTextBox.Location = new Point(120, 28);
            maxZoomTextBox.Name = "maxZoomTextBox";
            maxZoomTextBox.Size = new Size(30, 20);
            maxZoomTextBox.Text = "4";
            maxZoomTextBox.TabIndex = 7;
            // 
            // generateCacheButton
            // 
            generateCacheButton.Font = new Font("Microsoft Sans Serif", 8F);
            generateCacheButton.Location = new Point(20, 125);
            generateCacheButton.Name = "generateCacheButton";
            generateCacheButton.Size = new Size(170, 30);
            generateCacheButton.Text = "Generate Cache";
            generateCacheButton.UseVisualStyleBackColor = true;
            generateCacheButton.Click += GenerateCacheButton_Click;
            generateCacheButton.TabIndex = 8;
            // 
            // clearCacheButton
            // 
            clearCacheButton.Font = new Font("Microsoft Sans Serif", 8F);
            clearCacheButton.Location = new Point(20, 165);
            clearCacheButton.Name = "clearCacheButton";
            clearCacheButton.Size = new Size(170, 30);
            clearCacheButton.Text = "Clear Cache";
            clearCacheButton.UseVisualStyleBackColor = true;
            clearCacheButton.Click += ClearCacheButton_Click;
            clearCacheButton.TabIndex = 9;
            // 
            // openCacheFolderButton
            // 
            openCacheFolderButton.Font = new Font("Microsoft Sans Serif", 8F);
            openCacheFolderButton.Location = new Point(20, 205);
            openCacheFolderButton.Name = "openCacheFolderButton";
            openCacheFolderButton.Size = new Size(170, 30);
            openCacheFolderButton.Text = "Open Cache Folder In Explorer";
            openCacheFolderButton.UseVisualStyleBackColor = true;
            openCacheFolderButton.Click += OpenCacheFolderButton_Click;
            openCacheFolderButton.TabIndex = 10;
            // 
            // toggleMatrixButton
            // 
            toggleMatrixButton.Font = new Font("Microsoft Sans Serif", 8F);
            toggleMatrixButton.Location = new Point(20, 245);
            toggleMatrixButton.Name = "toggleMatrixButton";
            toggleMatrixButton.Size = new Size(170, 30);
            toggleMatrixButton.Text = "Switch to Global Tile Matrix";
            toggleMatrixButton.UseVisualStyleBackColor = true;
            toggleMatrixButton.Click += ToggleMatrixButton_Click;
            toggleMatrixButton.TabIndex = 11;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            progressBar.Location = new Point(20, 570);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1000, 20);
            progressBar.Visible = false;
            // 
            // CacheTileOverlay
            // 
            Controls.Add(mapView);
            Controls.Add(progressBar);
            Controls.Add(consolePanel);
            Name = "CacheTileOverlay";
            Size = new Size(1250, 611);
            Load += new EventHandler(Form_Load);
            consolePanel.ResumeLayout(false);
            zoomRangeGroupBox.ResumeLayout(false);
            zoomRangeGroupBox.PerformLayout();
            ResumeLayout(false);
            progressBar.BringToFront();
            //
            // Attach VisibleChanged event
            //
            VisibleChanged += CacheTileOverlay_VisibleChanged;
        }

        #endregion Component Designer generated code
    }
}
