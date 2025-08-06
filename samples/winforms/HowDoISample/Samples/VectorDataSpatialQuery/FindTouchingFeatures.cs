using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindTouchingFeatures : UserControl
    {
        public FindTouchingFeatures()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light
                };
                mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the Map Unit to meters (used in Spherical Mercator)
                mapView.MapUnit = GeographyUnit.Meter;

                // Create a feature layer to hold and display the zoning data
                var zoningLayer = new InMemoryFeatureLayer();

                // Add a style to use to draw the Frisco zoning polygons
                zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Import the features from the Frisco zoning data shapefile
                var zoningDataFeatureSource = new ShapeFileFeatureSource(@"./Data/Shapefile/Zoning.shp");

                // Create a ProjectionConverter to convert the shapefile data from North Central Texas (2276) to Spherical Mercator (3857)
                var projectionConverter = new ProjectionConverter(3857, 2276);

                // For this sample, we have to reproject the features before adding them to the feature layer
                // This is because the topological equality query often does not work when used on a feature layer with a ProjectionConverter, due to rounding issues between projections
                zoningDataFeatureSource.Open();
                projectionConverter.Open();
                foreach (var zoningFeature in zoningDataFeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns))
                {
                    var reprojectedFeature = projectionConverter.ConvertToInternalProjection(zoningFeature);
                    zoningLayer.InternalFeatures.Add(reprojectedFeature);
                }
                zoningDataFeatureSource.Close();
                projectionConverter.Close();

                // Set the map extent to Frisco, TX
                mapView.CurrentExtent = new RectangleShape(-10781137.28, 3917162.59, -10774579.34, 3911241.35);

                // Create a layer to hold the feature we will perform the spatial query against
                var queryFeatureLayer = new InMemoryFeatureLayer();
                queryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(75, GeoColors.LightRed), GeoColors.LightRed);
                queryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a layer to hold features found by the spatial query
                var highlightedFeaturesLayer = new InMemoryFeatureLayer();
                highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
                highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add each feature layer to its own overlay
                // We do this, so we can control and refresh/redraw each layer individually
                var zoningOverlay = new LayerOverlay();
                zoningOverlay.TileType = TileType.SingleTile;
                zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
                mapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

                var queryFeaturesOverlay = new LayerOverlay();
                queryFeaturesOverlay.TileType = TileType.SingleTile;
                queryFeaturesOverlay.Layers.Add("Query Feature", queryFeatureLayer);
                mapView.Overlays.Add("Query Features Overlay", queryFeaturesOverlay);

                var highlightedFeaturesOverlay = new LayerOverlay();
                highlightedFeaturesOverlay.TileType = TileType.SingleTile;
                highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
                mapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

                // Create a sample shape using vertices from an existing feature, to ensure that it is touching other features
                zoningLayer.Open();
                var firstFeatureShape = (MultipolygonShape)zoningLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).First().GetShape();

                // Get vertices from an existing feature
                var vertices = firstFeatureShape.Polygons.First().OuterRing.Vertices;

                // Create a new feature using a subset of those vertices
                var sampleShape = new MultipolygonShape(new Collection<PolygonShape> { new PolygonShape(new RingShape(new Collection<Vertex> { vertices[0], vertices[1], vertices[2] })) });
                queryFeatureLayer.InternalFeatures.Add(new Feature(sampleShape));
                zoningLayer.Close();
                await GetFeaturesTouchingAsync(sampleShape);

                // Set the map extent to the sample shape
                mapView.CenterPoint = new PointShape(-10776520, 3919250);
                mapView.CurrentScale = 18060;

                await mapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }
        private Collection<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            // Perform the spatial query on features in the specified layer
            layer.Open();
            var features = layer.QueryTools.GetFeaturesTouching(shape, ReturningColumnsType.NoColumns);
            layer.Close();

            return features;
        }

        /// <summary>
        /// Highlight the features that were found by the spatial query
        /// </summary>
        private async Task HighlightQueriedFeaturesAsync(IEnumerable<Feature> features)
        {
            // Find the layers we will be modifying in the MapView dictionary
            var highlightedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Highlighted Features Overlay"];
            var highlightedFeaturesLayer = (InMemoryFeatureLayer)highlightedFeaturesOverlay.Layers["Highlighted Features"];

            // Clear the currently highlighted features
            highlightedFeaturesLayer.Open();
            highlightedFeaturesLayer.InternalFeatures.Clear();

            // Add new features to the layer
            foreach (var feature in features)
            {
                highlightedFeaturesLayer.InternalFeatures.Add(feature);
            }
            highlightedFeaturesLayer.Close();

            // Refresh the overlay so the layer is redrawn
            await highlightedFeaturesOverlay.RefreshAsync();

            // Update the number of matching features found in the UI
            numberOfFeaturesFoundTextBox.Text = $@"Number of features touching the drawn shape: {features.Count()}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesTouchingAsync(BaseShape shape)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeaturesOverlay = (LayerOverlay)mapView.Overlays["Query Features Overlay"];
            var queryFeatureLayer = (InMemoryFeatureLayer)queryFeaturesOverlay.Layers["Query Feature"];
            var zoningLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query shape layer and add the newly drawn shape
            queryFeatureLayer.InternalFeatures.Clear();
            queryFeatureLayer.InternalFeatures.Add(new Feature(shape));
            await queryFeaturesOverlay.RefreshAsync();

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(shape, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Disable map drawing and clear the drawn shape
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private Label queryLabel;
        private Label panLabel;
        private Label zoomLabel;
        private TextBox numberOfFeaturesFoundTextBox;
        private TextBox panTextBox;
        private TextBox zoomTextBox;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            queryLabel = new Label();
            panLabel = new Label();
            zoomLabel = new Label();
            numberOfFeaturesFoundTextBox = new TextBox();
            panTextBox = new TextBox();
            zoomTextBox = new TextBox();
            consolePanel.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(818, 630);
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            consolePanel.BackColor = System.Drawing.Color.Gray;
            consolePanel.Controls.Add(numberOfFeaturesFoundTextBox);
            consolePanel.Controls.Add(queryLabel);
            consolePanel.Controls.Add(panLabel);
            consolePanel.Controls.Add(panTextBox);
            consolePanel.Controls.Add(zoomLabel);
            consolePanel.Controls.Add(zoomTextBox);
            consolePanel.Location = new System.Drawing.Point(818, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(301, 630);
            consolePanel.TabIndex = 1;
            // 
            // queryLabel
            // 
            queryLabel.AutoSize = true;
            queryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            queryLabel.ForeColor = System.Drawing.Color.White;
            queryLabel.Location = new System.Drawing.Point(20, 18);
            queryLabel.Name = "queryLabel";
            queryLabel.Size = new System.Drawing.Size(192, 20);
            queryLabel.Text = "Perform a \'Touches\' Query";
            queryLabel.TabIndex = 2;
            // 
            // numberOfFeaturesFoundTextBox
            // 
            numberOfFeaturesFoundTextBox.BackColor = System.Drawing.Color.Gray;
            numberOfFeaturesFoundTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            numberOfFeaturesFoundTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            numberOfFeaturesFoundTextBox.ForeColor = System.Drawing.Color.White;
            numberOfFeaturesFoundTextBox.Location = new System.Drawing.Point(25, 41);
            numberOfFeaturesFoundTextBox.Multiline = true;
            numberOfFeaturesFoundTextBox.Name = "numberOfFeaturesFoundTextBox";
            numberOfFeaturesFoundTextBox.Size = new System.Drawing.Size(260, 48);
            numberOfFeaturesFoundTextBox.TabIndex = 3;
            // 
            // panLabel
            // 
            panLabel.AutoSize = true;
            panLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            panLabel.ForeColor = System.Drawing.Color.White;
            panLabel.Location = new System.Drawing.Point(20, 92);
            panLabel.Name = "panLabel";
            panLabel.Size = new System.Drawing.Size(192, 20);
            panLabel.Text = "Pan the Map";
            panLabel.TabIndex = 4;
            // 
            // panTextBox
            // 
            panTextBox.BackColor = System.Drawing.Color.Gray;
            panTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            panTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            panTextBox.ForeColor = System.Drawing.Color.White;
            panTextBox.Location = new System.Drawing.Point(25, 115);
            panTextBox.Multiline = true;
            panTextBox.Name = "panTextBox";
            panTextBox.Size = new System.Drawing.Size(260, 56);
            panTextBox.Text = "Press and hold the middle mouse button (scroll wheel), then drag the mouse to move around the map.";
            panTextBox.TabIndex = 5;
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = System.Drawing.Color.White;
            zoomLabel.Location = new System.Drawing.Point(20, 174);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new System.Drawing.Size(192, 20);
            zoomLabel.Text = "Zoom In/Out";
            zoomLabel.TabIndex = 6;
            // 
            // zoomTextBox
            // 
            zoomTextBox.BackColor = System.Drawing.Color.Gray;
            zoomTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            zoomTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            zoomTextBox.ForeColor = System.Drawing.Color.White;
            zoomTextBox.Location = new System.Drawing.Point(25, 197);
            zoomTextBox.Multiline = true;
            zoomTextBox.Name = "zoomTextBox";
            zoomTextBox.Size = new System.Drawing.Size(260, 56);
            zoomTextBox.Text = "Scroll the mouse wheel forward to zoom in, and backward to zoom out.";
            zoomTextBox.TabIndex = 7;
            // 
            // FindTouchingFeatures
            // 
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "FindTouchingFeatures";
            Size = new System.Drawing.Size(1119, 630);
            Load += new System.EventHandler(Form_Load);
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}