using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class PerfTestRefreshShapes : UserControl
    {
        private DispatcherTimer timer;

        public PerfTestRefreshShapes()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Creating a rectangle area we will use to generate the polygons and also start the map there.
            var currentExtent = new RectangleShape(-10810995.245624, 3939081.90719325, -10747552.5124997, 3884429.43227297);

            //Do all the things we need to set up the polygon layer and overlay such as creating all the polygons etc.
            AddPolygonOverlay(AreaBaseShape.ScaleDown(currentExtent.GetBoundingBox(), 80).GetBoundingBox());

            //Set the maps current extent so we start there
            mapView.CurrentExtent = currentExtent;

            await mapView.RefreshAsync();
        }

        private void AddPolygonOverlay(BaseShape boundingRectangle)
        {
            //We are going to store all the polygons in an in memory layer
            var polygonLayer = new InMemoryFeatureLayer();

            //Here we generate all of our make believe polygons
            var features = GetGeneratedPolygons(boundingRectangle.GetBoundingBox());

            //Add all the polygons to the layer
            foreach (var feature in features)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            //We are going to style them based on their values we randomly added using the column DataPoint1
            var valueStyle = new ValueStyle
            {
                ColumnName = "DataPoint1"
            };

            //Here we add all the different sub styles so for example "1" is going to be a red semitransparent fill with a black border etc.
            valueStyle.ValueItems.Add(new ValueItem("1", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Red)))));
            valueStyle.ValueItems.Add(new ValueItem("2", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)))));
            valueStyle.ValueItems.Add(new ValueItem("3", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Green)))));
            valueStyle.ValueItems.Add(new ValueItem("4", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.White)))));

            //We add the style we just created to the custom styles of the first zoom level.  Zoom level on is the highest
            // on as such you can see the whole globe.  Zoom level 20 is the lowest street level.
            polygonLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            // We can define a style that applies from zoom level 1 all the way to level 20. However, if you only want this style to be visible at lower zoom levels
            // you can adjust the starting level. For example, by setting the style to start at zoom level 15 and apply it up to level 20, the style will no longer be applied once you zoom out beyond level 15.
            polygonLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create the overlay to house the layer and add it to the map.
            var polygonOverlay = new LayerOverlay
            {
                // Here we set the overlay to draw as a single tile.  Alternatively we could draw it as multiples tiles all threaded but single tile is a bit faster
                //We like to use multi tile for slow data sources or very complex ones that may take time to render as you can start to see data as it comes in.
                TileType = TileType.SingleTile
            };

            polygonOverlay.Layers.Add("PolygonLayer", polygonLayer);
            mapView.Overlays.Add("PolygonOverlay", polygonOverlay);
        }

        private Collection<Feature> GetGeneratedPolygons(RectangleShape boundingRectangle)
        {
            // We just created approximately 20,000 rectangles around the bounding box area and assigned them random data values between 1 and 4.
            var random = new Random();

            boundingRectangle.ScaleTo(10);

            var features = new Collection<Feature>();

            for (var x = 1; x < 150; x++)
            {
                for (var y = 1; y < 150; y++)
                {
                    var upperLeftX = boundingRectangle.UpperLeftPoint.X + x * boundingRectangle.Width / 150;
                    var upperLeftY = boundingRectangle.UpperLeftPoint.Y - y * boundingRectangle.Height / 150;

                    var lowerRightX = upperLeftX + boundingRectangle.Width / 150;
                    var lowerRightY = upperLeftY - boundingRectangle.Height / 150;

                    var feature = new Feature(new RectangleShape(new PointShape(upperLeftX, upperLeftY), new PointShape(lowerRightX, lowerRightY)));
                    feature.ColumnValues.Add("DataPoint1", random.Next(1, 5).ToString());

                    features.Add(feature);
                }
            }

            return features;
        }

        private void btnStartRefresh_Click(object sender, EventArgs e)
        {
            if (timer != null) return;
            //When you click this I start a timer that ticks every second
            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            //I go to find the layer and then loop through all the features and assign them new
            // random colors and refresh just the overlay that we are using to draw the polygons
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            var random = new Random();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                feature.ColumnValues["DataPoint1"] = random.Next(1, 5).ToString();
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.            
            await mapView.RefreshAsync(mapView.Overlays["PolygonOverlay"]);
        }

        private async void btnRotate_Click(object sender, EventArgs e)
        {
            //I go to find the layer and then loop through all the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            var newFeatures = new Collection<Feature>();

            var center = polygonLayer.GetBoundingBox().GetCenterPoint();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                var shape = (PolygonShape)feature.GetShape();

                shape.Rotate(center, 10);
                shape.Id = feature.Id;

                var newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }

            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            await mapView.RefreshAsync(mapView.Overlays["PolygonOverlay"]);
        }

        private async void btnOffset_Click(object sender, EventArgs e)
        {
            //I go to find the layer and then loop through all the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            var newFeatures = new Collection<Feature>();

            polygonLayer.GetBoundingBox().GetCenterPoint();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                var shape = (PolygonShape)feature.GetShape();

                shape.TranslateByOffset(2000, 2000);
                shape.Id = feature.Id;

                var newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }

            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            await mapView.RefreshAsync(mapView.Overlays["PolygonOverlay"]);
        }

        protected override void Dispose(bool disposing)
        {
            mapView.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private Button btnOffset;
        private Button btnRotate;
        private Button btnStartRefresh;
        private Label label1;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOffset = new System.Windows.Forms.Button();
            this.btnRotate = new System.Windows.Forms.Button();
            this.btnStartRefresh = new System.Windows.Forms.Button();
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
            this.mapView.Location = new System.Drawing.Point(5, 0);
            this.mapView.MapFocusMode = MapFocusMode.Default;
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(969, 562);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btnOffset);
            this.panel1.Controls.Add(this.btnRotate);
            this.panel1.Controls.Add(this.btnStartRefresh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(975, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 562);
            this.panel1.TabIndex = 1;
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(0, 128);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(302, 30);
            this.btnOffset.TabIndex = 4;
            this.btnOffset.Text = "Offset";
            this.btnOffset.UseVisualStyleBackColor = true;
            this.btnOffset.Click += new System.EventHandler(this.btnOffset_Click);
            // 
            // btnRotate
            // 
            this.btnRotate.Location = new System.Drawing.Point(0, 92);
            this.btnRotate.Name = "btnRotate";
            this.btnRotate.Size = new System.Drawing.Size(302, 30);
            this.btnRotate.TabIndex = 3;
            this.btnRotate.Text = "Rotate";
            this.btnRotate.UseVisualStyleBackColor = true;
            this.btnRotate.Click += new System.EventHandler(this.btnRotate_Click);
            // 
            // btnStartRefresh
            // 
            this.btnStartRefresh.Location = new System.Drawing.Point(0, 56);
            this.btnStartRefresh.Name = "btnStartRefresh";
            this.btnStartRefresh.Size = new System.Drawing.Size(302, 30);
            this.btnStartRefresh.TabIndex = 2;
            this.btnStartRefresh.Text = "Refresh";
            this.btnStartRefresh.UseVisualStyleBackColor = true;
            this.btnStartRefresh.Click += new System.EventHandler(this.btnStartRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Feed Controls:";
            // 
            // PerfTestRefreshShapes
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "PerfTestRefreshShapes";
            this.Size = new System.Drawing.Size(1277, 562);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}