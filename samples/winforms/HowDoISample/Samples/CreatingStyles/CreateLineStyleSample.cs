using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateLineStyleSample : UserControl
    {
        public CreateLineStyleSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10779675.1746605, 3914631.77546835, -10779173.5566652, 3914204.80300804);

            // Create a layer with line data
            ShapeFileFeatureLayer friscoRailroad = new ShapeFileFeatureLayer(@"./Data/Railroad/Railroad.shp");
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project the layer's data to match the projection of the map
            friscoRailroad.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            layerOverlay.Layers.Add("Railroad", friscoRailroad);

            // Add the overlay to the map
            mapView.Overlays.Add("overlay", layerOverlay);

            rbLineStyle.Checked = true;

            await mapView.RefreshAsync();
        }

        private async void rbLineStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["overlay"];
                ShapeFileFeatureLayer friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                // Create a line style
                var lineStyle = new LineStyle(new GeoPen(GeoBrushes.DimGray, 10), new GeoPen(GeoBrushes.WhiteSmoke, 6));

                // Add the line style to the collection of custom styles for ZoomLevel 1.
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
                friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                await layerOverlay.RefreshAsync();
            }
        }

        private async void rbDashedLineStyle_CheckedChanged(object sender, EventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["overlay"];
                ShapeFileFeatureLayer friscoRailroad = (ShapeFileFeatureLayer)layerOverlay.Layers["Railroad"];

                var lineStyle = new LineStyle(
                    outerPen: new GeoPen(GeoColors.Black, 12),
                    innerPen: new GeoPen(GeoColors.White, 6)
                    {
                        DashStyle = LineDashStyle.Custom,
                        DashPattern = { 3f, 3f },
                        StartCap = DrawingLineCap.Flat,
                        EndCap = DrawingLineCap.Flat
                    }
                );

                // Add the line style to the collection of custom styles for ZoomLevel 1.
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                friscoRailroad.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(lineStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the line style on every zoom level on the map. 
                friscoRailroad.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                await layerOverlay.RefreshAsync();
            }
        }

        private Panel panel1;
        private RadioButton rbDashedLineStyle;
        private Label label1;
        private RadioButton rbLineStyle;

        #region Component Designer generated code

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
            this.mapView.MapFocusMode = ThinkGeo.Core.MapFocusMode.Default;
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1162, 624);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.rbDashedLineStyle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rbLineStyle);
            this.panel1.Location = new System.Drawing.Point(863, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 624);
            this.panel1.TabIndex = 1;
            // 
            // rbLineStyle
            // 
            this.rbLineStyle.AutoSize = true;
            this.rbLineStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLineStyle.ForeColor = System.Drawing.Color.White;
            this.rbLineStyle.Location = new System.Drawing.Point(45, 68);
            this.rbLineStyle.Name = "rbLineStyle";
            this.rbLineStyle.Size = new System.Drawing.Size(85, 20);
            this.rbLineStyle.TabIndex = 0;
            this.rbLineStyle.TabStop = true;
            this.rbLineStyle.Text = "Solid Line";
            this.rbLineStyle.UseVisualStyleBackColor = true;
            this.rbLineStyle.CheckedChanged += new System.EventHandler(this.rbLineStyle_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Line Type";
            // 
            // rbDashedLineStyle
            // 
            this.rbDashedLineStyle.AutoSize = true;
            this.rbDashedLineStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDashedLineStyle.ForeColor = System.Drawing.Color.White;
            this.rbDashedLineStyle.Location = new System.Drawing.Point(45, 94);
            this.rbDashedLineStyle.Name = "rbDashedLineStyle";
            this.rbDashedLineStyle.Size = new System.Drawing.Size(102, 20);
            this.rbDashedLineStyle.TabIndex = 3;
            this.rbDashedLineStyle.TabStop = true;
            this.rbDashedLineStyle.Text = "Dashed Line";
            this.rbDashedLineStyle.UseVisualStyleBackColor = true;
            this.rbDashedLineStyle.CheckedChanged += new System.EventHandler(this.rbDashedLineStyle_CheckedChanged);
            // 
            // CreateLineStyleSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CreateLineStyleSample";
            this.Size = new System.Drawing.Size(1162, 624);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        
    }
}