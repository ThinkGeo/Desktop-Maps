using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class WorldMapsQuery : UserControl
    {
        private MapsQueryCloudClient mapsQueryCloudClient;

        public WorldMapsQuery()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the query shape used to perform the query
            var queryShapeFeatureLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the query shape will be drawn
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));

            // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
            queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create a new feature layer to display the shapes returned by the query.
            var queriedFeaturesLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the returned shapes will be drawn
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.OrangeRed);
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.OrangeRed);
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.OrangeRed, new GeoSolidBrush(new GeoColor(10, GeoColors.OrangeRed)));
            queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the feature layers to an overlay, then add that overlay to the map
            var queriedFeaturesOverlay = new LayerOverlay();
            queriedFeaturesOverlay.Layers.Add("Queried Features Layer", queriedFeaturesLayer);
            queriedFeaturesOverlay.Layers.Add("Query Shape Layer", queryShapeFeatureLayer);
            mapView.Overlays.Add("Queried Features Overlay", queriedFeaturesOverlay);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Add an event to handle new shapes that are drawn on the map
            mapView.TrackOverlay.TrackEnded += OnShapeDrawn;

            // Initialize the MapsQueryCloudClient with our ThinkGeo Cloud credentials
            mapsQueryCloudClient = new MapsQueryCloudClient(SampleKeys.ClientId2, SampleKeys.ClientSecret2);

            // Create a sample shape and add it to the query shape layer
            var sampleShape = new RectangleShape(-10779877.70, 3915441.00, -10779248.97, 3915119.63);
            queryShapeFeatureLayer.InternalFeatures.Add(new Feature(sampleShape));

            // Run the world maps query
            await PerformWorldMapsQueryAsync();

            txtMaximumResults.DataBindings.Add("Text", maxResults, "Value");
        }

        private async Task PerformWorldMapsQueryAsync()
        {
            //Get the feature layers from the MapView

            var queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            var queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Show an error if trying to query with no query shape
            if (queryShapeFeatureLayer.InternalFeatures.Count == 0)
            {
                MessageBox.Show("Please draw a shape to use for the query", "Error");
                return;
            }

            //Set the MapsQuery parameters based on the drawn query shape and the UI
            var queryShape = queryShapeFeatureLayer.InternalFeatures[0].GetShape();
            int projectionInSrid = 3857;
            string queryLayer = cboQueryLayer.SelectedItem.ToString().ToLower();

            var result = new CloudMapsQueryResult();

            //Show a loading graphic to let users know the request is running
            //loadingImage.Visibility = Visibility.Visible;

            //Perform the world maps query
            try
            {
                switch (cboQueryType.SelectedItem.ToString())
                {
                    case "Containing":
                        result = await mapsQueryCloudClient.GetFeaturesContainingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Nearest":
                        result = await mapsQueryCloudClient.GetFeaturesNearestAsync(queryLayer, queryShape, projectionInSrid, (int)maxResults.Value);
                        break;
                    case "Intersecting":
                        result = await mapsQueryCloudClient.GetFeaturesIntersectingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Overlapping":
                        result = await mapsQueryCloudClient.GetFeaturesOverlappingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                    case "Within":
                        result = await mapsQueryCloudClient.GetFeaturesWithinAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)maxResults.Value });
                        break;
                }
            }
            catch (Exception ex)
            {
                //Handle any errors returned from the maps query service
                if (ex is ArgumentException)
                {
                    MessageBox.Show(string.Format("{0} {1}", ex.InnerException.Message, ex.Message), "Invalid Request");
                    await mapView.RefreshAsync();
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Unexpected Error");
                    await mapView.RefreshAsync();
                    return;
                }
            }
            finally
            {
                // Hide the loading graphic
                //loadingImage.Visibility = Visibility.Hidden;
            }

            if (result.Features.Count > 0)
            {
                //Add any features found by the query to the map
                foreach (var feature in result.Features)
                {
                    queriedFeaturesLayer.InternalFeatures.Add(feature);
                }

                // Set the map extent to the extent of the query results
                queriedFeaturesLayer.Open();
                mapView.CurrentExtent = queriedFeaturesLayer.GetBoundingBox();
                queriedFeaturesLayer.Close();
            }
            else
            {
                MessageBox.Show(@"No features found in the selected area");
            }

            //Refresh and redraw the map
            await mapView.RefreshAsync();
        }

        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Get the query shape layer from the MapView
            var queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];

            // Add the newly drawn shape, then redraw the overlay
            queryShapeFeatureLayer.InternalFeatures.Add(new Feature(e.TrackShape));
            await queriedFeaturesOverlay.RefreshAsync();

            await PerformWorldMapsQueryAsync();
        }

        private async void btnDrawNewPointAndQuery_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Point'
            mapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Clear the old shapes from the map
            await ClearQueryShapesAsync();
        }

        private async void btnDrawNewLineAndQuery_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Line'
            mapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Clear the old shapes from the map
            await ClearQueryShapesAsync();
        }

        private async void btnDrawNewPolygonAndQuery_Click(object sender, EventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Clear the old shapes from the map
            await ClearQueryShapesAsync();
        }
        private async Task ClearQueryShapesAsync()
        {
            // Get the query shape layer from the MapView
            var queriedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Queried Features Overlay"];
            var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            var queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Clear the old query result and query shape from the map
            queriedFeaturesLayer.InternalFeatures.Clear();
            queryShapeFeatureLayer.InternalFeatures.Clear();
            await queriedFeaturesOverlay.RefreshAsync();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button btnDrawNewPolygonAndQuery;
        private Button btnDrawNewLineAndQuery;
        private Button btnDrawNewPointAndQuery;
        private TextBox txtMaximumResults;
        private TrackBar maxResults;
        private ComboBox cboQueryType;
        private ComboBox cboQueryLayer;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDrawNewPolygonAndQuery = new System.Windows.Forms.Button();
            this.btnDrawNewLineAndQuery = new System.Windows.Forms.Button();
            this.btnDrawNewPointAndQuery = new System.Windows.Forms.Button();
            this.txtMaximumResults = new System.Windows.Forms.TextBox();
            this.maxResults = new System.Windows.Forms.TrackBar();
            this.cboQueryType = new System.Windows.Forms.ComboBox();
            this.cboQueryLayer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxResults)).BeginInit();
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
            this.mapView.Size = new System.Drawing.Size(813, 673);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btnDrawNewPolygonAndQuery);
            this.panel1.Controls.Add(this.btnDrawNewLineAndQuery);
            this.panel1.Controls.Add(this.btnDrawNewPointAndQuery);
            this.panel1.Controls.Add(this.txtMaximumResults);
            this.panel1.Controls.Add(this.maxResults);
            this.panel1.Controls.Add(this.cboQueryType);
            this.panel1.Controls.Add(this.cboQueryLayer);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(816, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 673);
            this.panel1.TabIndex = 1;
            // 
            // btnDrawNewPolygonAndQuery
            // 
            this.btnDrawNewPolygonAndQuery.Location = new System.Drawing.Point(3, 268);
            this.btnDrawNewPolygonAndQuery.Name = "btnDrawNewPolygonAndQuery";
            this.btnDrawNewPolygonAndQuery.Size = new System.Drawing.Size(296, 33);
            this.btnDrawNewPolygonAndQuery.TabIndex = 10;
            this.btnDrawNewPolygonAndQuery.Text = "Draw New Polygon And Query";
            this.btnDrawNewPolygonAndQuery.UseVisualStyleBackColor = true;
            this.btnDrawNewPolygonAndQuery.Click += new System.EventHandler(this.btnDrawNewPolygonAndQuery_Click);
            // 
            // btnDrawNewLineAndQuery
            // 
            this.btnDrawNewLineAndQuery.Location = new System.Drawing.Point(3, 229);
            this.btnDrawNewLineAndQuery.Name = "btnDrawNewLineAndQuery";
            this.btnDrawNewLineAndQuery.Size = new System.Drawing.Size(296, 33);
            this.btnDrawNewLineAndQuery.TabIndex = 9;
            this.btnDrawNewLineAndQuery.Text = "Draw New Line And Query";
            this.btnDrawNewLineAndQuery.UseVisualStyleBackColor = true;
            this.btnDrawNewLineAndQuery.Click += new System.EventHandler(this.btnDrawNewLineAndQuery_Click);
            // 
            // btnDrawNewPointAndQuery
            // 
            this.btnDrawNewPointAndQuery.Location = new System.Drawing.Point(3, 190);
            this.btnDrawNewPointAndQuery.Name = "btnDrawNewPointAndQuery";
            this.btnDrawNewPointAndQuery.Size = new System.Drawing.Size(296, 33);
            this.btnDrawNewPointAndQuery.TabIndex = 8;
            this.btnDrawNewPointAndQuery.Text = "Draw New Point And Query";
            this.btnDrawNewPointAndQuery.UseVisualStyleBackColor = true;
            this.btnDrawNewPointAndQuery.Click += new System.EventHandler(this.btnDrawNewPointAndQuery_Click);
            // 
            // txtMaximumResults
            // 
            this.txtMaximumResults.Location = new System.Drawing.Point(252, 157);
            this.txtMaximumResults.Name = "txtMaximumResults";
            this.txtMaximumResults.Size = new System.Drawing.Size(47, 22);
            this.txtMaximumResults.TabIndex = 7;
            // 
            // maxResults
            // 
            this.maxResults.Location = new System.Drawing.Point(22, 157);
            this.maxResults.Maximum = 1000;
            this.maxResults.Minimum = 10;
            this.maxResults.Name = "maxResults";
            this.maxResults.Size = new System.Drawing.Size(227, 56);
            this.maxResults.TabIndex = 6;
            this.maxResults.TickStyle = System.Windows.Forms.TickStyle.None;
            this.maxResults.Value = 100;
            // 
            // cboQueryType
            // 
            this.cboQueryType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboQueryType.FormattingEnabled = true;
            this.cboQueryType.Items.AddRange(new object[] {
            "Intersecting",
            "Nearest",
            "Overlapping",
            "Within"});
            this.cboQueryType.Location = new System.Drawing.Point(164, 94);
            this.cboQueryType.Name = "cboQueryType";
            this.cboQueryType.Size = new System.Drawing.Size(135, 28);
            this.cboQueryType.TabIndex = 5;
            this.cboQueryType.Text = "Intersecting";
            // 
            // cboQueryLayer
            // 
            this.cboQueryLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cboQueryLayer.FormattingEnabled = true;
            this.cboQueryLayer.Items.AddRange(new object[] {
            "Buildings",
            "Countries",
            "States",
            "Roads",
            "Rail",
            "Addresses",
            "Cities",
            "Land-Use",
            "Places",
            "POIs",
            "Transport",
            "Water",
            "Waterways"});
            this.cboQueryLayer.Location = new System.Drawing.Point(164, 52);
            this.cboQueryLayer.Name = "cboQueryLayer";
            this.cboQueryLayer.Size = new System.Drawing.Size(135, 28);
            this.cboQueryLayer.TabIndex = 4;
            this.cboQueryLayer.Text = "Buildings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Maximum Results:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(18, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Query Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(18, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Query Layer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Query World Maps Features";
            // 
            // WorldMapsQuery
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "WorldMapsQuery";
            this.Size = new System.Drawing.Size(1118, 673);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}