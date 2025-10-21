using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class RenderLines : UserControl
    {
        private ShapeFileFeatureLayer solidLineLayer;
        private ShapeFileFeatureLayer dashedLineLayer;
        private bool _initialized;

        public RenderLines()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create solidLineLayer
            solidLineLayer = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
            solidLineLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Apply solid line style to solidLineLayer
            var solidStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));
            solidLineLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(solidStyle);
            solidLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            
            mapView.Overlays.Add("SolidLineOverlay", new LayerOverlay { Layers = { solidLineLayer } });

            // Create dashedLineLayer
            dashedLineLayer = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
            dashedLineLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Apply dashed line style to dashedLineLayer
            var dashedStyle = new LineStyle(
                new GeoPen(GeoColors.Black, 12),
                new GeoPen(GeoColors.White, 6)
                {
                    DashStyle = LineDashStyle.Custom,
                    DashPattern = { 3f, 3f },
                    StartCap = DrawingLineCap.Flat,
                    EndCap = DrawingLineCap.Flat
                });
            dashedLineLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(dashedStyle);
            dashedLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            mapView.Overlays.Add("DashedLineOverlay", new LayerOverlay { Layers = { dashedLineLayer } });

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10779420, 3914420);
            mapView.CurrentScale = 2260;

            dashedLineLayer.IsVisible = false;

            _initialized = true;
            await mapView.RefreshAsync();
        }

        private void rbLineStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            if (rbLineStyle.Checked)
            {
                solidLineLayer.IsVisible = true;
                dashedLineLayer.IsVisible = false;
            }

            _ = mapView.RefreshAsync();
        }

        private void rbDashedLineStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            if (rbDashedLineStyle.Checked)
            {
                solidLineLayer.IsVisible = false;
                dashedLineLayer.IsVisible = true;
            }

            _ = mapView.RefreshAsync();
        }

        #region Component Designer generated code

        private Panel panel1;
        private Label label1;
        private RadioButton rbDashedLineStyle;
        private RadioButton rbLineStyle;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbLineStyle = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbDashedLineStyle = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapFocusMode = MapFocusMode.Default;
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1162, 624);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.rbDashedLineStyle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbLineStyle);
            this.panel1.Location = new System.Drawing.Point(863, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 624);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 25);
            this.label1.Text = "Line Type";
            this.label1.TabIndex = 2;
            // 
            // rbLineStyle
            // 
            this.rbLineStyle.AutoSize = true;
            this.rbLineStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLineStyle.ForeColor = System.Drawing.Color.White;
            this.rbLineStyle.Location = new System.Drawing.Point(17, 54);
            this.rbLineStyle.Name = "rbLineStyle";
            this.rbLineStyle.Size = new System.Drawing.Size(85, 20);
            this.rbLineStyle.TabStop = true;
            this.rbLineStyle.Text = "Solid Line";
            this.rbLineStyle.UseVisualStyleBackColor = true;
            this.rbLineStyle.Checked = true;
            this.rbLineStyle.CheckedChanged += new System.EventHandler(this.rbLineStyle_CheckedChanged);
            this.rbLineStyle.TabIndex = 3;
            // 
            // rbDashedLineStyle
            // 
            this.rbDashedLineStyle.AutoSize = true;
            this.rbDashedLineStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDashedLineStyle.ForeColor = System.Drawing.Color.White;
            this.rbDashedLineStyle.Location = new System.Drawing.Point(17, 81);
            this.rbDashedLineStyle.Name = "rbDashedLineStyle";
            this.rbDashedLineStyle.Size = new System.Drawing.Size(102, 20);
            this.rbDashedLineStyle.TabStop = true;
            this.rbDashedLineStyle.Text = "Dashed Line";
            this.rbDashedLineStyle.UseVisualStyleBackColor = true;
            this.rbDashedLineStyle.CheckedChanged += new System.EventHandler(this.rbDashedLineStyle_CheckedChanged);
            this.rbDashedLineStyle.TabIndex = 4;
            // 
            // RenderLines
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "RenderLines";
            this.Size = new System.Drawing.Size(1162, 624);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Component Designer generated code
    }
}