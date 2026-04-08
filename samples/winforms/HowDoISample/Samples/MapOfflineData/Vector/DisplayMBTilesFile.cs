using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMBTilesFile : UserControl
    {
        private bool _initialized;
        private VectorMbTilesAsyncLayer _mvtLayer;

        public DisplayMBTilesFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            _initialized = true;

            mapView.MapUnit = GeographyUnit.Meter;
            _mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            _mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(_mvtLayer);

            mapView.Overlays.Clear();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add(layerOverlay);

            await _mvtLayer.OpenAsync();
            mapView.CurrentExtent = _mvtLayer.GetBoundingBox();

            _initialized = true;
            await mapView.RefreshAsync();
        }

        private async void SwitchStyleJson_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized || _mvtLayer == null)
                return;

            var radioButton = sender as RadioButton;
            if (radioButton == null || radioButton.Tag == null)
                return;

            if (!radioButton.Checked)
                return;

            var selectedType = ((RadioButton)sender).Tag.ToString();

            _mvtLayer.StyleJsonUri = selectedType == "withStyleJson"
                ? @".\Data\Mbtiles\style.json"
                : null;

            await _mvtLayer.CloseAsync();
            await _mvtLayer.OpenAsync();

            _ = mapView.RefreshAsync();
        }

        private async void Projection_Checked(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            try
            {
                if (_mvtLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton == null || radioButton.Tag == null) return;

                var centerPoint = mapView.CenterPoint;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        mapView.MapUnit = GeographyUnit.Meter;
                        _mvtLayer.ProjectionConverter = null;
                        centerPoint = ProjectionConverter.Convert(4326, 3857, centerPoint);
                        break;

                    case "4326":
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        _mvtLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        centerPoint = ProjectionConverter.Convert(3857, 4326, centerPoint);
                        break;

                    default:
                        return;
                }

                await _mvtLayer.CloseAsync();
                await _mvtLayer.OpenAsync();

                mapView.CenterPoint = centerPoint;
                await mapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private GroupBox projectionGroupBox;
        private GroupBox styleGroupBox;
        private Label projectionLabel;
        private Label styleLabel;
        private RadioButton epsg3857RadioButton;
        private RadioButton epsg4326RadioButton;
        private RadioButton withoutStyleJsonRadioButton;
        private RadioButton withStyleJsonRadioButton;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            projectionLabel = new Label();
            projectionGroupBox = new GroupBox();
            styleGroupBox = new GroupBox();
            styleLabel = new Label();
            epsg3857RadioButton = new RadioButton();
            epsg4326RadioButton = new RadioButton();
            withoutStyleJsonRadioButton = new RadioButton();
            withStyleJsonRadioButton = new RadioButton();
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
            consolePanel.Controls.Add(projectionGroupBox);
            consolePanel.Controls.Add(styleGroupBox);
            consolePanel.Controls.Add(projectionLabel);
            consolePanel.Controls.Add(styleLabel);
            consolePanel.Location = new System.Drawing.Point(1050, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(285, 611);
            consolePanel.TabIndex = 1;
            // 
            // projectionLabel
            // 
            projectionLabel.AutoSize = true;
            projectionLabel.Font = new System.Drawing.Font(new FontFamily("Microsoft Sans Serif"), 10F, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
            projectionLabel.ForeColor = Color.White;
            projectionLabel.Location = new System.Drawing.Point(14, 17);
            projectionLabel.Name = "projectionLabel";
            projectionLabel.Size = new System.Drawing.Size(89, 20);
            projectionLabel.Text = "Projection";
            projectionLabel.TabIndex = 2;
            // 
            // projectionGroupBox
            // 
            projectionGroupBox.ForeColor = Color.White;
            projectionGroupBox.Location = new System.Drawing.Point(10, 40);
            projectionGroupBox.Size = new System.Drawing.Size(250, 80);
            projectionGroupBox.Controls.Add(epsg3857RadioButton);
            projectionGroupBox.Controls.Add(epsg4326RadioButton);
            // 
            // epsg3857RadioButton
            // 
            epsg3857RadioButton.AutoSize = true;
            epsg3857RadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            epsg3857RadioButton.ForeColor = System.Drawing.Color.White;
            epsg3857RadioButton.Location = new System.Drawing.Point(10, 25);
            epsg3857RadioButton.Name = "epsg3857RadioButton";
            epsg3857RadioButton.Size = new System.Drawing.Size(161, 24);
            epsg3857RadioButton.TabStop = true;
            epsg3857RadioButton.Text = "EPSG 3857";
            epsg3857RadioButton.Tag = "3857";
            epsg3857RadioButton.UseVisualStyleBackColor = true;
            epsg3857RadioButton.Checked = true;
            epsg3857RadioButton.CheckedChanged += Projection_Checked;
            epsg3857RadioButton.TabIndex = 3;
            // 
            // epsg4326RadioButton
            // 
            epsg4326RadioButton.AutoSize = true;
            epsg4326RadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            epsg4326RadioButton.ForeColor = System.Drawing.Color.White;
            epsg4326RadioButton.Location = new System.Drawing.Point(10, 50);
            epsg4326RadioButton.Name = "epsg4326RadioButton";
            epsg4326RadioButton.Size = new System.Drawing.Size(161, 24);
            epsg4326RadioButton.TabStop = true;
            epsg4326RadioButton.Text = "EPSG 4326";
            epsg4326RadioButton.Tag = "4326";
            epsg4326RadioButton.UseVisualStyleBackColor = true;
            epsg4326RadioButton.CheckedChanged += Projection_Checked;
            epsg4326RadioButton.TabIndex = 4;
            // 
            // styleLabel
            // 
            styleLabel.AutoSize = true;
            styleLabel.Font = new System.Drawing.Font(new FontFamily("Microsoft Sans Serif"), 10F, System.Drawing.FontStyle.Bold, GraphicsUnit.Point, 0);
            styleLabel.ForeColor = Color.White;
            styleLabel.Location = new System.Drawing.Point(14, projectionGroupBox.Bottom + 20);
            styleLabel.Name = "styleLabel";
            styleLabel.Size = new System.Drawing.Size(89, 20);
            styleLabel.Text = "Style";
            styleLabel.TabIndex = 5;
            // 
            // styleGroupBox
            // 
            styleGroupBox.ForeColor = Color.White;
            styleGroupBox.Location = new System.Drawing.Point(10, styleLabel.Bottom + 5);
            styleGroupBox.Size = new System.Drawing.Size(250, 100);
            styleGroupBox.Controls.Add(withoutStyleJsonRadioButton);
            styleGroupBox.Controls.Add(withStyleJsonRadioButton);
            // 
            // withoutStyleJsonRadioButton
            // 
            withoutStyleJsonRadioButton.AutoSize = true;
            withoutStyleJsonRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            withoutStyleJsonRadioButton.ForeColor = System.Drawing.Color.White;
            withoutStyleJsonRadioButton.Location = new System.Drawing.Point(10, 25);
            withoutStyleJsonRadioButton.Name = "withoutStyleJsonRadioButton";
            withoutStyleJsonRadioButton.Size = new System.Drawing.Size(161, 24);
            withoutStyleJsonRadioButton.TabStop = true;
            withoutStyleJsonRadioButton.Text = "Without StyleJson";
            withoutStyleJsonRadioButton.Tag = "withoutStyleJson";
            withoutStyleJsonRadioButton.UseVisualStyleBackColor = true;
            withoutStyleJsonRadioButton.CheckedChanged += SwitchStyleJson_OnCheckedChanged;
            withoutStyleJsonRadioButton.TabIndex = 6;
            // 
            // withStyleJsonRadioButton
            // 
            withStyleJsonRadioButton.AutoSize = true;
            withStyleJsonRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            withStyleJsonRadioButton.ForeColor = System.Drawing.Color.White;
            withStyleJsonRadioButton.Location = new System.Drawing.Point(10, 55);
            withStyleJsonRadioButton.Name = "withStyleJsonRadioButton";
            withStyleJsonRadioButton.Size = new System.Drawing.Size(196, 24);
            withStyleJsonRadioButton.Text = "With StyleJson";
            withStyleJsonRadioButton.Tag = "withStyleJson";
            withStyleJsonRadioButton.UseVisualStyleBackColor = true;
            withStyleJsonRadioButton.Checked = true;
            withStyleJsonRadioButton.CheckedChanged += SwitchStyleJson_OnCheckedChanged;
            withStyleJsonRadioButton.TabIndex = 7;
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
