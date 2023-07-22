using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class UnionShapesSample: UserControl 
    {
        public UnionShapesSample()
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

            ShapeFileFeatureLayer dividedCityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimitsDivided.shp");
            InMemoryFeatureLayer unionLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project dividedCityLimits layer to Spherical Mercator to match the map projection
            dividedCityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style dividedCityLimits layer
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.LightOrange), GeoColors.DimGray, 2);
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("NAME", "Segoe UI", 12, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2);
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style unionLayer
            unionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray, 2);
            unionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add dividedCityLimits to a LayerOverlay
            layerOverlay.Layers.Add("dividedCityLimits", dividedCityLimits);

            // Add unionLayer to the layerOverlay
            layerOverlay.Layers.Add("unionLayer", unionLayer);

            // Set the map extent to the dividedCityLimits layer bounding box
            dividedCityLimits.Open();
            mapView.CurrentExtent = dividedCityLimits.GetBoundingBox();
            dividedCityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);

            mapView.Refresh();
        }

        private void unionShapes_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer dividedCityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["dividedCityLimits"];
            InMemoryFeatureLayer unionLayer = (InMemoryFeatureLayer)layerOverlay.Layers["unionLayer"];

            // Query the dividedCityLimits layer to get all the features
            dividedCityLimits.Open();
            var features = dividedCityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            dividedCityLimits.Close();

            // Union all the features into a single Multipolygon Shape
            var union = AreaBaseShape.Union(features);

            // Add the union shape into unionLayer to display the result.
            // If this were to be a permanent change to the dividedCityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
            unionLayer.InternalFeatures.Clear();
            unionLayer.InternalFeatures.Add(new Feature(union));

            // Hide the dividedCityLimits layer
            dividedCityLimits.IsVisible = false;

            // Redraw the layerOverlay to see the unioned features on the map
            layerOverlay.Refresh();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button unionShapes;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.unionShapes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(878, 606);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.unionShapes);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(881, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 606);
            this.panel1.TabIndex = 1;
            // 
            // unionShapes
            // 
            this.unionShapes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unionShapes.Location = new System.Drawing.Point(3, 70);
            this.unionShapes.Name = "unionShapes";
            this.unionShapes.Size = new System.Drawing.Size(296, 35);
            this.unionShapes.TabIndex = 1;
            this.unionShapes.Text = "Union";
            this.unionShapes.UseVisualStyleBackColor = true;
            this.unionShapes.Click += new System.EventHandler(this.unionShapes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Union Controls:";
            // 
            // UnionShapesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "UnionShapesSample";
            this.Size = new System.Drawing.Size(1183, 606);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}