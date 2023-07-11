using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateMultiColumnTextStyleSample : UserControl
    {
        
        public CreateMultiColumnTextStyleSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Create a layer with polygon data
            var countries02Layer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");
            countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColors.SandyBrown, GeoColors.Black);

            var textStyle = new TextStyle();
            textStyle.TextContent = "{CNTRY_NAME}: " + Environment.NewLine + " Population:{POP_CNTRY}";
            textStyle.Font = new GeoFont("Arial", 10);
            textStyle.TextBrush = GeoBrushes.Black;

            countries02Layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            countries02Layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add layers to a layerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countries02Layer);

            // Add overlay to map
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-8.70, 62.60, 38.81, 31.11);
            mapView.Refresh();
        }

        #region Component Designer generated code
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
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
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1281, 643);
            this.mapView.TabIndex = 0;
            // 
            // CreateAreaStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateAreaStyleSample";
            this.Size = new System.Drawing.Size(1281, 643);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }
       
        #endregion Component Designer generated code
    }
}