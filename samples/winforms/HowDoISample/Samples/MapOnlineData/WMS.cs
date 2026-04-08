using System;
using System.Windows.Forms;
using ThinkGeo.Core;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class WMS : UserControl
    {
        private bool _initialized;

        public WMS()
        {
            InitializeComponent();
        }

        private WmsAsyncLayer wms;
        private ThinkGeoRasterMapsAsyncLayer _thinkGeoRasterMapsAsyncLayer;
        private PointShape australiaCenterPoint = new PointShape(15253160, 4318830);
        private double australiaCurrentScale = 9266220;

        private async void Form_Load(object sender, EventArgs e)
        {
            _initialized = true;
            mapView.MapUnit = GeographyUnit.Meter;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;

            // Add Cloud Maps as a background overlay
            _thinkGeoRasterMapsAsyncLayer = new ThinkGeoRasterMapsAsyncLayer
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X1,
            };

            layerOverlay.Layers.Add(_thinkGeoRasterMapsAsyncLayer);
            mapView.Overlays.Add(layerOverlay);

            wms = new WmsAsyncLayer(new Uri("http://geo.vliz.be/geoserver/Dataportal/ows?service=WMS&"));
            wms.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wms.Parameters.Add("LAYERS", "eurobis_grid_15m-obisenv");
            wms.Parameters.Add("STYLES", "generic");
            wms.OutputFormat = "image/png";
            wms.Crs = "EPSG:3857";  // Coordinate system, typically EPSG:3857 for WMS with Spherical Mercator
            //wms.Transparency = 100;

            // Extent of Australia 
            mapView.CenterPoint = australiaCenterPoint;
            mapView.CurrentScale = australiaCurrentScale;

            var layerOverlay2 = new LayerOverlay();
            layerOverlay2.Opacity = 0.5;
            layerOverlay2.TileType = TileType.SingleTile;
            layerOverlay2.Layers.Add(wms);
            mapView.Overlays.Add(layerOverlay2);

            _initialized = true;
            _ = mapView.RefreshAsync();
        }

        private async void Projection_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            try
            {
                if (wms == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        wms.ProjectionConverter = null;
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = null;
                        mapView.CenterPoint = australiaCenterPoint;
                        mapView.CurrentScale = australiaCurrentScale;
                        break;

                    case "3112":
                        wms.ProjectionConverter = new GdalProjectionConverter(3857, 6669);
                        _thinkGeoRasterMapsAsyncLayer.ProjectionConverter = new GdalProjectionConverter(3857, 6669);
                        var projectedCenter = ProjectionConverter.Convert(3857, 6669, australiaCenterPoint);
                        mapView.CenterPoint = projectedCenter;
                        mapView.CurrentScale = australiaCurrentScale;
                        break;

                    default:
                        return;
                }

                await wms.CloseAsync();
                await _thinkGeoRasterMapsAsyncLayer.CloseAsync();
                await wms.OpenAsync();
                await _thinkGeoRasterMapsAsyncLayer.OpenAsync();

                await mapView.RefreshAsync();
            }
            catch (Exception ex)
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label label1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
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
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(962, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(965, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 3;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(20, 48);
            radioButton1.Name = "radioButton1";
            radioButton1.Tag = "3857";
            radioButton1.Size = new System.Drawing.Size(161, 24);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "EPSG 3857";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Checked = true;
            radioButton1.CheckedChanged += new EventHandler(Projection_CheckedChanged);
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(20, 85);
            radioButton2.Name = "radioButton2";
            radioButton2.Tag = "3112";
            radioButton2.Size = new System.Drawing.Size(196, 24);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "EPSG 3112";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(Projection_CheckedChanged);
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
            label1.Text = "Projection";
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