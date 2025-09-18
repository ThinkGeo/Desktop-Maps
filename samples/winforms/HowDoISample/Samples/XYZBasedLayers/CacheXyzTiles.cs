using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using FontStyle = System.Drawing.FontStyle;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class CacheXyzTiles : UserControl
    {
        private OpenStreetMapAsyncLayer _openStreetMapAsyncLayer;
        private int _finishedTileCount;
        private string _cachePath;

        public CacheXyzTiles()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add(layerOverlay);

            // Add Cloud Maps as a background overlay
            _openStreetMapAsyncLayer = new OpenStreetMapAsyncLayer();
            _openStreetMapAsyncLayer.IsCacheOnly = true;
            layerOverlay.Layers.Add(_openStreetMapAsyncLayer);

            _cachePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cache");

            if (!Directory.Exists(_cachePath))
                Directory.CreateDirectory(_cachePath);

            _openStreetMapAsyncLayer.TileCache = new FileRasterTileCache(_cachePath, "xyzLayerCacheTest");
            _openStreetMapAsyncLayer.TileCacheGenerated += OpenStreetMapAsyncLayerOnTileCacheGenerated;

            mapView.CurrentExtent = MaxExtents.OsmMaps;
            _ = mapView.RefreshAsync();
        }

        private void OpenStreetMapAsyncLayerOnTileCacheGenerated(object sender,
            TileCacheGeneratedXyzTileAsyncLayerEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                _finishedTileCount++;
                progressBar.Maximum = e.TotalTileCount;
                progressBar.Value = _finishedTileCount;
                progressBar.DisplayText = $"{_finishedTileCount} / {e.TotalTileCount}";
            }));
        }

        private async void GenerateCacheButton_Click(object sender, EventArgs e)
        {
            _openStreetMapAsyncLayer.TileCache.ClearCache();

            try
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                progressBar.DisplayText = "";
                _finishedTileCount = 0;

                int minZoom = int.Parse(minZoomTextBox.Text);
                int maxZoom = int.Parse(maxZoomTextBox.Text);

                var northAmericaExtent = new RectangleShape(-20030000, 20030000, 0, 0);
                await _openStreetMapAsyncLayer.GenerateTileCacheAsync(northAmericaExtent, minZoom, maxZoom);

                progressBar.Visible = false;

                await mapView.RefreshAsync();
            }
            catch
            {
                // deal with the exception if needed.
            }
        }

        private void ClearCacheButton_Click(object sender, EventArgs e)
        {
            _openStreetMapAsyncLayer.TileCache.ClearCache();
            _ = mapView.RefreshAsync();
        }

        private void CacheOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_openStreetMapAsyncLayer == null)
                return;
            var checkBox = (CheckBox)sender;
            _openStreetMapAsyncLayer.IsCacheOnly = checkBox.Checked;
            _ = mapView.RefreshAsync();
        }

        private void OpenCacheFolderButton_Click(object sender, EventArgs e)
        {
            var fileRasterTileCache = _openStreetMapAsyncLayer.TileCache as FileRasterTileCache;
            if (fileRasterTileCache == null)
                return;

            var cacheFolder = Path.Combine(fileRasterTileCache.CacheDirectory, fileRasterTileCache.CacheId);
            if (Directory.Exists(cacheFolder))
                Process.Start("explorer.exe", cacheFolder);
            else
                MessageBox.Show("Cache Tiles have not been generated");
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
        private CheckBox cacheOnlyCheckBox;
        private GroupBox zoomRangeGroupBox;
        private Label minZoomLabel;
        private Label maxZoomLabel;
        private TextBox minZoomTextBox;
        private TextBox maxZoomTextBox;
        private Button generateCacheButton;
        private Button clearCacheButton;
        private Button openCacheFolderButton;
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
            zoomRangeGroupBox.Text = "Zoom Ranges";
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
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            progressBar.Location = new Point(20, 570);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1000, 20);
            progressBar.Visible = false;
            // 
            // CacheXyzTiles
            // 
            Controls.Add(mapView);
            Controls.Add(progressBar);
            Controls.Add(consolePanel);
            Name = "CacheXyzTiles";
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
            VisibleChanged += CacheXyzTiles_VisibleChanged;
        }

        #endregion Component Designer generated code
    }


    public class ProgressBarWithText : ProgressBar
    {
        public string DisplayText { get; set; } = "";

        public ProgressBarWithText()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            Graphics g = e.Graphics;

            // Draw background
            ProgressBarRenderer.DrawHorizontalBar(g, rect);

            // Calculate filled area
            rect.Inflate(-1, -1);
            if (Value > 0)
            {
                Rectangle clip = new Rectangle(rect.X, rect.Y,
                    (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                g.FillRectangle(Brushes.Green, clip);
            }

            // Draw centered text
            string text = string.IsNullOrEmpty(DisplayText) ? "" : DisplayText;
            using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString(text, this.Font, Brushes.Black, rect, sf);
            }
        }
    }
}
