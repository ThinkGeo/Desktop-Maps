using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetShortestLine : UserControl
    {
        public GetShortestLine()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var friscoParks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
            var stadiumLayer = new InMemoryFeatureLayer();
            var shortestLineLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();

            // Project friscoParks layer to Spherical Mercator to match the map projection
            friscoParks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoParks layer
            friscoParks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            friscoParks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style stadiumLayer
            stadiumLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 16, GeoColors.White, 4);
            stadiumLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style shortestLineLayer
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 2, false);
            shortestLineLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add friscoParks layer to a LayerOverlay
            layerOverlay.Layers.Add("friscoParks", friscoParks);

            // Add stadiumLayer layer to a LayerOverlay
            layerOverlay.Layers.Add("stadiumLayer", stadiumLayer);

            // Add shortestLineLayer to the layerOverlay
            layerOverlay.Layers.Add("shortestLineLayer", shortestLineLayer);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10782307.6877106, 3918904.87378907, -10774377.3460701, 3912073.31442403);

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);

            // Add Toyota Stadium feature to stadiumLayer
            var stadium = new Feature(new PointShape(-10779651.500992451, 3915933.0023557912));
            stadiumLayer.InternalFeatures.Add(stadium);

            await mapView.RefreshAsync();
        }

        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            var friscoParks = (ShapeFileFeatureLayer)layerOverlay.Layers["friscoParks"];
            var stadiumLayer = (InMemoryFeatureLayer)layerOverlay.Layers["stadiumLayer"];
            var shortestLineLayer = (InMemoryFeatureLayer)layerOverlay.Layers["shortestLineLayer"];

            // Query the friscoParks layer to get the first feature closest to the map click event
            var park = friscoParks.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            // Get the stadium feature from the stadiumLayer
            var stadium = stadiumLayer.InternalFeatures[0];

            // Get the shortest line from the selected park to the stadium
            var shortestLine = park.GetShape().GetShortestLineTo(stadium, GeographyUnit.Meter);

            // Show the shortestLine on the map
            shortestLineLayer.InternalFeatures.Clear();
            shortestLineLayer.InternalFeatures.Add(new Feature(shortestLine));
            await layerOverlay.RefreshAsync();

            // Get the area of the first feature
            var length = shortestLine.GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer);

            // Display the shortestLine's length in the distanceResult TextBox
            distanceResult.Text = $@"{length:f3} km";
        }

        #region Component Designer generated code
        private Panel panel1;
        private TextBox distanceResult;
        private Label label3;
        private Label label2;
        private Label label1;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.distanceResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(813, 606);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.distanceResult);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(819, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 606);
            this.panel1.TabIndex = 1;
            // 
            // distanceResult
            // 
            this.distanceResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.distanceResult.Location = new System.Drawing.Point(99, 126);
            this.distanceResult.Name = "distanceResult";
            this.distanceResult.Size = new System.Drawing.Size(195, 27);
            this.distanceResult.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Distance: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 60);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click on a Park (green) on the map to\r\nget the shortest line to the Frisco\r\nStadi" +
    "um (blue).";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shortest Line Controls:";
            // 
            // GetShortestLine
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GetShortestLine";
            this.Size = new System.Drawing.Size(1119, 606);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}