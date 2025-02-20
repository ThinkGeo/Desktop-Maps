using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class WMTS : UserControl
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private readonly string defaultLayerName = "SwissTopo";
        private readonly LayerOverlay layerOverlay = new LayerOverlay();
        private readonly Dictionary<string, RectangleShape> bBoxDict = new Dictionary<string, RectangleShape>();

        public WMTS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.Overlays.Add(layerOverlay);

            LoadWmtsServer1();
            LoadWmtsServer2();
            LoadWmtsServer3();

            await SwitchToLayer(defaultLayerName);
        }

        private void UpdateCancellationToken()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private async void rbLayerOrOverlay_CheckedChanged(object sender, EventArgs e)
        {
            var button = (RadioButton)sender;
            if (button == null) return;

            var activeLayerName = button.Text;
            await SwitchToLayer(activeLayerName);
        }

        private async Task SwitchToLayer(string activeLayerName)
        {
            foreach (var layer in layerOverlay.Layers)
            {
                layer.IsVisible = false;
            }

            layerOverlay.Layers[activeLayerName].IsVisible = true;
            var activeBBox = bBoxDict[activeLayerName];

            UpdateCancellationToken();
            mapView.CurrentExtent = activeBBox;
            await mapView.RefreshAsync(OverlayRefreshType.Redraw, cancellationTokenSource.Token);
        }

        private void LoadWmtsServer1()
        {
            var wmtsLayer = new Core.WmtsAsyncLayer(new Uri("https://wmts.geo.admin.ch/1.0.0"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "21781_26";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_1"));

            layerOverlay.Layers.Add(defaultLayerName, wmtsLayer);
            bBoxDict.Add(defaultLayerName, new RectangleShape(641202.9893498598, 159695.95554381475, 645651.6243713424, 156646.11813217978));
        }

        private void LoadWmtsServer2()
        {
            var wmtsLayer = new Core.WmtsAsyncLayer(new Uri("https://geo.vliz.be/geoserver/Dataportal/gwc/service/wmts"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.ActiveLayerName = "eurobis_grid_15m-obisenv";
            wmtsLayer.ActiveStyleName = "generic";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "EPSG:900913";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_2"));

            string layerName = "VLIZ";
            layerOverlay.Layers.Add(layerName, wmtsLayer);
            bBoxDict.Add(layerName, new RectangleShape(14702448.140544016, -1074476.17351944, 15302448.801711123, -5574476.985662017));
        }

        private void LoadWmtsServer3()
        {
            var wmtsLayer = new Core.WmtsAsyncLayer(new Uri("https://basemaps.linz.govt.nz/v1/tiles/aerial/NZTM2000Quad/WMTSCapabilities.xml?api=c01j20m6pmjhc81bn55sakayftb"));
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.ActiveLayerName = "aerial";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.TileMatrixSetName = "NZTM2000Quad";
            wmtsLayer.TileCache = new FileRasterTileCache(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "WmtsTmpTileCache_5"));

            string layerName = "LINZ";
            layerOverlay.Layers.Add(layerName, wmtsLayer);
            bBoxDict.Add(layerName, new RectangleShape(14303497.448365476, -7610740.842272079, 16022006.68392926, -9080257.632067444));
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label1 = new Label();
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
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(1050, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton3);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(1050, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 1;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton3.ForeColor = System.Drawing.Color.White;
            radioButton3.Location = new System.Drawing.Point(20, 122);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new System.Drawing.Size(231, 24);
            radioButton3.TabIndex = 3;
            radioButton3.Text = "LINZ";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += new EventHandler(rbLayerOrOverlay_CheckedChanged);
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
            radioButton2.Text = "VLIZ";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(rbLayerOrOverlay_CheckedChanged);
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
            radioButton1.Text = "SwissTopo";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(rbLayerOrOverlay_CheckedChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(15, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(162, 25);
            label1.TabIndex = 0;
            label1.Text = "Layer or Overlay:";
            // 
            // WMS
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "WMS";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
