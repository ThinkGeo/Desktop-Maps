using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTS : UserControl
    {
        public WMTS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            ThinkGeoDebugger.DisplayTileId = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;
            var layerOverlay = new WmtsOverlay(new Uri("https://wmts.geo.admin.ch/1.0.0"));
            layerOverlay.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            layerOverlay.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
            layerOverlay.ActiveStyleName = "default";
            layerOverlay.OutputFormat = "image/png";
            layerOverlay.TileMatrixSetName = "21781_26";
            mapView.Overlays.Add(layerOverlay);

            await layerOverlay.OpenAsync();
            mapView.ZoomLevelSet = GetZoomLevelSetFromWmtsServer(layerOverlay);
            mapView.CurrentExtent = layerOverlay.GetBoundingBox();
            mapView.StretchMode = MapViewStretchMode.ShowNewTilesOnStart;
            await mapView.RefreshAsync();
        }

        private ZoomLevelSet GetZoomLevelSetFromWmtsServer(WmtsOverlay wmtsOverlay)
        {
            var scales = wmtsOverlay.GetTileMatrixSets()[wmtsOverlay.TileMatrixSetName].TileMatrices
                .Select((matrix, i) => matrix.Scale);
            var zoomLevels = scales.Select((d, i) => new ZoomLevel(d));
            var zoomLevelSet = new ZoomLevelSet();
            foreach (var zoomLevel in zoomLevels)
            {
                zoomLevelSet.CustomZoomLevels.Add(zoomLevel);
            }

            return zoomLevelSet;
        }

        private async void DisplayTileIdCheckBox_Checked(object sender, EventArgs e)
        {
            if (!(sender is CheckBox checkBox))
               return;

            bool isChecked = checkBox.Checked;

            if (ThinkGeoDebugger.DisplayTileId != isChecked)
            {
                ThinkGeoDebugger.DisplayTileId = isChecked;
                await mapView.RefreshAsync();
            }
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private CheckBox checkBox1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
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
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(1050, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(checkBox1);
            panel1.Location = new System.Drawing.Point(1050, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 1;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBox1.ForeColor = System.Drawing.Color.White;
            checkBox1.Location = new System.Drawing.Point(15, 10);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(162, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Show Debug Info";
            checkBox1.CheckedChanged += DisplayTileIdCheckBox_Checked;
            // 
            // WMTS
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "WMTS";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
