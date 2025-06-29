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

        private async void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            var mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);

            mapView.Overlays.Clear();
            mapView.Overlays.Add(layerOverlay);

            await mvtLayer.OpenAsync();
            mapView.CurrentExtent = mvtLayer.GetBoundingBox();

            await mapView.RefreshAsync();
        }

        private void SwitchTileSize_OnCheckedChanged(object sender, EventArgs e)
        {
            var selectedType = ((RadioButton)sender).Tag?.ToString();

            if (selectedType == null)
                return;

            var mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            if (selectedType == "applyStyle")
                mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);

            mapView.Overlays.Clear();
            mapView.Overlays.Add(layerOverlay);

            _ = mapView.RefreshAsync();
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private RadioButton applyStyleRadioButton;
        private RadioButton noStyleRadioButton;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            applyStyleRadioButton = new RadioButton();
            noStyleRadioButton = new RadioButton();
            consolePanel.SuspendLayout();
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
            consolePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            consolePanel.BackColor = System.Drawing.Color.Gray;
            consolePanel.Controls.Add(applyStyleRadioButton);
            consolePanel.Controls.Add(noStyleRadioButton);
            consolePanel.Location = new System.Drawing.Point(1050, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(285, 611);
            consolePanel.TabIndex = 1;
            // 
            // noStyleRadioButton
            // 
            noStyleRadioButton.AutoSize = true;
            noStyleRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            noStyleRadioButton.ForeColor = System.Drawing.Color.White;
            noStyleRadioButton.Location = new System.Drawing.Point(20, 10);
            noStyleRadioButton.Name = "noStyleRadioButton";
            noStyleRadioButton.Size = new System.Drawing.Size(161, 24);
            noStyleRadioButton.TabStop = true;
            noStyleRadioButton.Text = "No Style";
            noStyleRadioButton.Tag = "noStyle";
            noStyleRadioButton.UseVisualStyleBackColor = true;
            noStyleRadioButton.CheckedChanged += SwitchTileSize_OnCheckedChanged;
            noStyleRadioButton.TabIndex = 2;
            // 
            // applyStyleRadioButton
            // 
            applyStyleRadioButton.AutoSize = true;
            applyStyleRadioButton.Checked = true;
            applyStyleRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            applyStyleRadioButton.ForeColor = System.Drawing.Color.White;
            applyStyleRadioButton.Location = new System.Drawing.Point(20, 40);
            applyStyleRadioButton.Name = "applyStyleRadioButton";
            applyStyleRadioButton.Size = new System.Drawing.Size(196, 24);
            applyStyleRadioButton.Text = "Apply Style";
            applyStyleRadioButton.Tag = "applyStyle";
            applyStyleRadioButton.UseVisualStyleBackColor = true;
            applyStyleRadioButton.CheckedChanged += SwitchTileSize_OnCheckedChanged;
            applyStyleRadioButton.TabIndex = 3;
            // 
            // DisplayMBTilesFile
            // 
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "DisplayMBTilesFile";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
