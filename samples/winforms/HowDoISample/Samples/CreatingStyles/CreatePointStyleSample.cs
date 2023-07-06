using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreatePointStyleSample : UserControl
    {
        public CreatePointStyleSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10778329.017082, 3909598.36751101, -10776250.8853871, 3907890.47766975);

            ShapeFileFeatureLayer hotelsLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Hotels.shp");
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project the layer's data to match the projection of the map
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to a layer overlay
            layerOverlay.Layers.Add("hotels", hotelsLayer);

            // Add the overlay to the map
            mapView.Overlays.Add("hotels", layerOverlay);

            pointSymbol.Checked = true;
        }



        private void pointSymbol_CheckedChanged(object sender, EventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
                ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

                // Create a point style
                var pointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Blue, new GeoPen(GeoBrushes.White, 2));

                // Add the point style to the collection of custom styles for ZoomLevel 1.
                hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
                hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);

                // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
                hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Refresh the layerOverlay to show the new style
                layerOverlay.Refresh();
            }
        }

        private void icon_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
            ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

            // Create a point style
            var pointStyle = new PointStyle(new GeoImage(@"../../../Resources/hotel_icon.png"))
            {
                ImageScale = .25
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            layerOverlay.Refresh();
        }

        private void symbol_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["hotels"];
            ShapeFileFeatureLayer hotelsLayer = (ShapeFileFeatureLayer)layerOverlay.Layers["hotels"];

            // Create a point style
            var symbolPointStyle = new PointStyle(new GeoFont("Verdana", 16, DrawingFontStyles.Bold), "@", GeoBrushes.Black)
            {
                Mask = new AreaStyle(GeoBrushes.White),
                MaskType = MaskType.Circle
            };

            // Add the point style to the collection of custom styles for ZoomLevel 1.
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            hotelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(symbolPointStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the point style on every zoom level on the map. 
            hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Refresh the layerOverlay to show the new style
            layerOverlay.Refresh();
        }

        #region Component Designer generated code
        private Panel panel1;
        private RadioButton symbol;
        private RadioButton icon;
        private RadioButton pointSymbol;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pointSymbol = new System.Windows.Forms.RadioButton();
            this.icon = new System.Windows.Forms.RadioButton();
            this.symbol = new System.Windows.Forms.RadioButton();
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
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.Margin = new System.Windows.Forms.Padding(0);
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(843, 659);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.symbol);
            this.panel1.Controls.Add(this.icon);
            this.panel1.Controls.Add(this.pointSymbol);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(843, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 659);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "PointStyle Controls:";
            // 
            // pointSymbol
            // 
            this.pointSymbol.AutoSize = true;
            this.pointSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointSymbol.ForeColor = System.Drawing.Color.White;
            this.pointSymbol.Location = new System.Drawing.Point(17, 54);
            this.pointSymbol.Name = "pointSymbol";
            this.pointSymbol.Size = new System.Drawing.Size(170, 24);
            this.pointSymbol.TabIndex = 1;
            this.pointSymbol.TabStop = true;
            this.pointSymbol.Text = "Predefined Style";
            this.pointSymbol.UseVisualStyleBackColor = true;
            this.pointSymbol.CheckedChanged += new System.EventHandler(this.pointSymbol_CheckedChanged);
            // 
            // icon
            // 
            this.icon.AutoSize = true;
            this.icon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.icon.ForeColor = System.Drawing.Color.White;
            this.icon.Location = new System.Drawing.Point(17, 81);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(146, 24);
            this.icon.TabIndex = 2;
            this.icon.TabStop = true;
            this.icon.Text = "Image Style";
            this.icon.UseVisualStyleBackColor = true;
            this.icon.CheckedChanged += new System.EventHandler(this.icon_CheckedChanged);
            // 
            // symbol
            // 
            this.symbol.AutoSize = true;
            this.symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.symbol.ForeColor = System.Drawing.Color.White;
            this.symbol.Location = new System.Drawing.Point(17, 108);
            this.symbol.Name = "symbol";
            this.symbol.Size = new System.Drawing.Size(170, 24);
            this.symbol.TabIndex = 3;
            this.symbol.TabStop = true;
            this.symbol.Text = "Font Style";
            this.symbol.UseVisualStyleBackColor = true;
            this.symbol.CheckedChanged += new System.EventHandler(this.symbol_CheckedChanged);
            // 
            // CreatePointStyleSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CreatePointStyleSample";
            this.Size = new System.Drawing.Size(1142, 659);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion Component Designer generated code

    }
}