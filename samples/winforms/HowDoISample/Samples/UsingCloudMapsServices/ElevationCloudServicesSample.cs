using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using NetTopologySuite.Geometries;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ElevationCloudServicesSample: UserControl
    {
        private ElevationCloudClient elevationCloudClient;

        public ElevationCloudServicesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new InMemoryFeatureLayer to hold the shape drawn for the elevation query
            InMemoryFeatureLayer drawnShapeLayer = new InMemoryFeatureLayer();

            // Create Point, Line, and Polygon styles to display the drawn shape, and apply them across all zoom levels
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            drawnShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new InMemoryFeatureLayer to display the elevation points returned from the query
            InMemoryFeatureLayer elevationPointsLayer = new InMemoryFeatureLayer();

            // Create a point style for the elevation points
            elevationPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            elevationPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the feature layers to an overlay, and add the overlay to the map
            LayerOverlay elevationFeaturesOverlay = new LayerOverlay();
            elevationFeaturesOverlay.Layers.Add("Elevation Points Layer", elevationPointsLayer);
            elevationFeaturesOverlay.Layers.Add("Drawn Shape Layer", drawnShapeLayer);
            mapView.Overlays.Add("Elevation Features Overlay", elevationFeaturesOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Add an event to trigger the elevation query when a new shape is drawn
            mapView.TrackOverlay.TrackEnded += OnShapeDrawn;

            // Initialize the ElevationCloudClient with our ThinkGeo Cloud credentials
            elevationCloudClient = new ElevationCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");

            // Create a sample line and get elevation along that line
            LineShape sampleShape = new LineShape("LINESTRING(-10776298.0601626 3912306.29684573,-10776496.3187036 3912399.45447343,-10776675.4679876 3912478.28015841,-10776890.4471285 3912516.49867234,-10777189.0292686 3912509.33270098,-10777329.9600387 3912442.4503016,-10777664.3720356 3912174.92070409)");
            PerformElevationQuery(sampleShape);
            txtSliderValue.DataBindings.Add("Text", intervalDistance, "Value");
        }

        private async void PerformElevationQuery(BaseShape queryShape)
        {
            // Get feature layers from the MapView
            LayerOverlay elevationPointsOverlay = (LayerOverlay)mapView.Overlays["Elevation Features Overlay"];
            InMemoryFeatureLayer drawnShapesLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Drawn Shape Layer"];
            InMemoryFeatureLayer elevationPointsLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Elevation Points Layer"];

            // Clear the existing shapes from the map
            elevationPointsLayer.Open();
            elevationPointsLayer.Clear();
            elevationPointsLayer.Close();
            drawnShapesLayer.Open();
            drawnShapesLayer.Clear();
            drawnShapesLayer.Close();

            // Add the drawn shape to the map
            drawnShapesLayer.InternalFeatures.Add(new Feature(queryShape));

            // Set options from the UI and run the query using the ElevationCloudClient
            Collection<CloudElevationPointResult> elevationPoints = new Collection<CloudElevationPointResult>();
            int projectionInSrid = 3857;

            // Show a loading graphic to let users know the request is running
            //loadingImage.Visibility = Visibility.Visible;

            // The point interval distance determines how many elevation points are retrieved for line and area queries
            int pointIntervalDistance = (int)intervalDistance.Value;
            switch (queryShape.GetWellKnownType())
            {
                case WellKnownType.Point:
                    PointShape drawnPoint = (PointShape)queryShape;
                    double elevation = await elevationCloudClient.GetElevationOfPointAsync(drawnPoint.X, drawnPoint.Y, projectionInSrid);

                    // The API for getting the elevation of a single point returns a double, so we manually create a CloudElevationPointResult to use as a data source for the Elevations list
                    elevationPoints.Add(new CloudElevationPointResult(elevation, drawnPoint));

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", elevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", elevation, drawnPoint);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", elevation, drawnPoint);
                    break;
                case WellKnownType.Line:
                    LineShape drawnLine = (LineShape)queryShape;
                    var result = await elevationCloudClient.GetElevationOfLineAsync(drawnLine, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter, DistanceUnit.Feet);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", result.AverageElevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", result.HighestElevationPoint.Elevation, result.HighestElevationPoint.Point);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", result.LowestElevationPoint.Elevation, result.LowestElevationPoint.Point);
                    break;
                case WellKnownType.Polygon:
                    PolygonShape drawnPolygon = (PolygonShape)queryShape;
                    result = await elevationCloudClient.GetElevationOfAreaAsync(drawnPolygon, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter, DistanceUnit.Feet);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    txtAverageElevation.Text = string.Format("Average Elevation: {0:0.00} feet", result.AverageElevation);
                    txtHighestElevation.Text = string.Format("Highest Elevation: {0:0.00} feet", result.HighestElevationPoint.Elevation, result.HighestElevationPoint.Point);
                    txtLowestElevation.Text = string.Format("Lowest Elevation: {0:0.00} feet", result.LowestElevationPoint.Elevation, result.LowestElevationPoint.Point);
                    break;
                default:
                    break;
            }

            // Add the elevation result points to the map and list box
            foreach (CloudElevationPointResult elevationPoint in elevationPoints)
            {
                elevationPointsLayer.InternalFeatures.Add(new Feature(elevationPoint.Point));
            }
            lsbElevations.DataSource = elevationPoints;

            lsbElevations.DisplayMember = "Elevation";
            // Hide the loading graphic
            //loadingImage.Visibility = Visibility.Hidden;

            // Set the map extent to the elevation query feature
            drawnShapesLayer.Open();
            mapView.CurrentExtent = drawnShapesLayer.GetBoundingBox();
            mapView.ZoomToScale(mapView.CurrentScale * 2);
            drawnShapesLayer.Close();
            mapView.Refresh();
        }
        private void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Validate shape size to avoid queries that are too large
            // Maximum length of a line is 10km
            // Maximum area of a polygon is 10km^2
            if (e.TrackShape.GetWellKnownType() == WellKnownType.Polygon)
            {
                if (((PolygonShape)e.TrackShape).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers) > 5)
                {
                    MessageBox.Show("Please draw a smaller polygon (limit: 5km^2)", "Error");
                    return;
                }
            }
            else if (e.TrackShape.GetWellKnownType() == WellKnownType.Line)
            {
                if (((LineShape)e.TrackShape).GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer) > 5)
                {
                    MessageBox.Show("Please draw a shorter line (limit: 5km)", "Error");
                    return;
                }
            }

            // Get elevation data for the drawn shape and update the UI
            PerformElevationQuery(e.TrackShape);
        }


        private void btnDrawANewPoint_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Point'
            mapView.TrackOverlay.TrackMode = TrackMode.Point;
        }

        private void btnDrawANewLine_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Line'
            mapView.TrackOverlay.TrackMode = TrackMode.Line;
        }

        private void btnDrawANewPolygon_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void lsbElevations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbElevations.SelectedItem != null)
            {
                // Set the map extent to the selected point
                CloudElevationPointResult elevationPoint = (CloudElevationPointResult)lsbElevations.SelectedItem;
                mapView.CurrentExtent = elevationPoint.Point.GetBoundingBox();
                mapView.Refresh();
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label2;
        private Label label1;
        private TextBox txtLowestElevation;
        private TextBox txtHighestElevation;
        private ListBox lsbElevations;
        private Button btnDrawANewPolygon;
        private Button btnDrawANewLine;
        private Button btnDrawANewPoint;
        private TextBox txtAverageElevation;
        private TextBox txtSliderValue;
        private TrackBar intervalDistance;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLowestElevation = new System.Windows.Forms.TextBox();
            this.txtHighestElevation = new System.Windows.Forms.TextBox();
            this.txtAverageElevation = new System.Windows.Forms.TextBox();
            this.lsbElevations = new System.Windows.Forms.ListBox();
            this.btnDrawANewPolygon = new System.Windows.Forms.Button();
            this.btnDrawANewLine = new System.Windows.Forms.Button();
            this.btnDrawANewPoint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.intervalDistance = new System.Windows.Forms.TrackBar();
            this.txtSliderValue = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalDistance)).BeginInit();
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
            this.mapView.Size = new System.Drawing.Size(943, 629);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtSliderValue);
            this.panel1.Controls.Add(this.intervalDistance);
            this.panel1.Controls.Add(this.txtLowestElevation);
            this.panel1.Controls.Add(this.txtHighestElevation);
            this.panel1.Controls.Add(this.txtAverageElevation);
            this.panel1.Controls.Add(this.lsbElevations);
            this.panel1.Controls.Add(this.btnDrawANewPolygon);
            this.panel1.Controls.Add(this.btnDrawANewLine);
            this.panel1.Controls.Add(this.btnDrawANewPoint);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(945, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 629);
            this.panel1.TabIndex = 1;
            // 
            // txtLowestElevation
            // 
            this.txtLowestElevation.BackColor = System.Drawing.Color.Gray;
            this.txtLowestElevation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLowestElevation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtLowestElevation.ForeColor = System.Drawing.Color.White;
            this.txtLowestElevation.Location = new System.Drawing.Point(4, 352);
            this.txtLowestElevation.Multiline = true;
            this.txtLowestElevation.Name = "txtLowestElevation";
            this.txtLowestElevation.Size = new System.Drawing.Size(294, 37);
            this.txtLowestElevation.TabIndex = 9;
            // 
            // txtHighestElevation
            // 
            this.txtHighestElevation.BackColor = System.Drawing.Color.Gray;
            this.txtHighestElevation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHighestElevation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtHighestElevation.ForeColor = System.Drawing.Color.White;
            this.txtHighestElevation.Location = new System.Drawing.Point(4, 309);
            this.txtHighestElevation.Multiline = true;
            this.txtHighestElevation.Name = "txtHighestElevation";
            this.txtHighestElevation.Size = new System.Drawing.Size(294, 37);
            this.txtHighestElevation.TabIndex = 8;
            // 
            // txtAverageElevation
            // 
            this.txtAverageElevation.BackColor = System.Drawing.Color.Gray;
            this.txtAverageElevation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAverageElevation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtAverageElevation.ForeColor = System.Drawing.Color.White;
            this.txtAverageElevation.Location = new System.Drawing.Point(4, 266);
            this.txtAverageElevation.Multiline = true;
            this.txtAverageElevation.Name = "txtAverageElevation";
            this.txtAverageElevation.Size = new System.Drawing.Size(294, 37);
            this.txtAverageElevation.TabIndex = 7;
            // 
            // lsbElevations
            // 
            this.lsbElevations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsbElevations.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lsbElevations.FormattingEnabled = true;
            this.lsbElevations.ItemHeight = 20;
            this.lsbElevations.Location = new System.Drawing.Point(3, 405);
            this.lsbElevations.Margin = new System.Windows.Forms.Padding(0);
            this.lsbElevations.Name = "lsbElevations";
            this.lsbElevations.Size = new System.Drawing.Size(295, 224);
            this.lsbElevations.TabIndex = 6;
            this.lsbElevations.SelectedIndexChanged += new System.EventHandler(this.lsbElevations_SelectedIndexChanged);
            // 
            // btnDrawANewPolygon
            // 
            this.btnDrawANewPolygon.Location = new System.Drawing.Point(3, 227);
            this.btnDrawANewPolygon.Name = "btnDrawANewPolygon";
            this.btnDrawANewPolygon.Size = new System.Drawing.Size(295, 32);
            this.btnDrawANewPolygon.TabIndex = 5;
            this.btnDrawANewPolygon.Text = "Draw a New Polygon";
            this.btnDrawANewPolygon.UseVisualStyleBackColor = true;
            this.btnDrawANewPolygon.Click += new System.EventHandler(this.btnDrawANewPolygon_Click);
            // 
            // btnDrawANewLine
            // 
            this.btnDrawANewLine.Location = new System.Drawing.Point(4, 188);
            this.btnDrawANewLine.Name = "btnDrawANewLine";
            this.btnDrawANewLine.Size = new System.Drawing.Size(295, 33);
            this.btnDrawANewLine.TabIndex = 4;
            this.btnDrawANewLine.Text = "Draw a New Line";
            this.btnDrawANewLine.UseVisualStyleBackColor = true;
            this.btnDrawANewLine.Click += new System.EventHandler(this.btnDrawANewLine_Click);
            // 
            // btnDrawANewPoint
            // 
            this.btnDrawANewPoint.Location = new System.Drawing.Point(4, 150);
            this.btnDrawANewPoint.Name = "btnDrawANewPoint";
            this.btnDrawANewPoint.Size = new System.Drawing.Size(295, 32);
            this.btnDrawANewPoint.TabIndex = 3;
            this.btnDrawANewPoint.Text = "Draw a New Point";
            this.btnDrawANewPoint.UseVisualStyleBackColor = true;
            this.btnDrawANewPoint.Click += new System.EventHandler(this.btnDrawANewPoint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Interval Distance (m):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Get Elevation for a shape";
            // 
            // intervalDistance
            // 
            this.intervalDistance.Location = new System.Drawing.Point(21, 88);
            this.intervalDistance.Maximum = 1000;
            this.intervalDistance.Minimum = 50;
            this.intervalDistance.Name = "intervalDistance";
            this.intervalDistance.Size = new System.Drawing.Size(224, 56);
            this.intervalDistance.TabIndex = 10;
            this.intervalDistance.TickStyle = System.Windows.Forms.TickStyle.None;
            this.intervalDistance.Value = 100;
            // 
            // txtSliderValue
            // 
            this.txtSliderValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtSliderValue.Location = new System.Drawing.Point(251, 88);
            this.txtSliderValue.Name = "txtSliderValue";
            this.txtSliderValue.Size = new System.Drawing.Size(48, 26);
            this.txtSliderValue.TabIndex = 11;
            this.txtSliderValue.Text = "100";
            // 
            // ElevationCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ElevationCloudServicesSample";
            this.Size = new System.Drawing.Size(1246, 629);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalDistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}