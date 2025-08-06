using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindCrossingFeatures : UserControl
    {
        public FindCrossingFeatures()
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

                // Create a feature layer to hold the Frisco zoning data
                var zoningLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

                // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                var projectionConverter = new ProjectionConverter(2276, 3857);
                zoningLayer.FeatureSource.ProjectionConverter = projectionConverter;

                // Add a style to use to draw the Frisco zoning polygons
                zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

                // Create a layer to hold the feature we will perform the spatial query against
                var queryFeatureLayer = new InMemoryFeatureLayer();
                queryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 6, false);
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

                // Add an event to handle new shapes that are drawn on the map
                mapView.TrackOverlay.TrackEnded += OnLineDrawn;

                // Set the map extent to the sample shapes
                mapView.CenterPoint = new PointShape(-10776670, 3914800);
                mapView.CurrentScale = 27870;

                mapView.TrackOverlay.TrackMode = TrackMode.Line;
                await mapView.RefreshAsync();

                // Add a sample shape to the map for the initial query
                var sampleShape = new LineShape("LINESTRING(-10774628 3914024,-10776902 3915582,-10778030 3914368,-10778708 3914445)");
                await GetFeaturesCrossingAsync(sampleShape);
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
            var features = layer.QueryTools.GetFeaturesCrossing(shape, ReturningColumnsType.AllColumns);
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
            numberOfFeaturesFoundTextBox.Text = $@"Number of features crossing the drawn shape: {features.Count()}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesCrossingAsync(BaseShape shape)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeaturesOverlay = (LayerOverlay)mapView.Overlays["Query Features Overlay"];
            var queryFeatureLayer = (InMemoryFeatureLayer)queryFeaturesOverlay.Layers["Query Feature"];
            var zoningLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query shape layer and add the newly drawn shape
            queryFeatureLayer.InternalFeatures.Clear();
            queryFeatureLayer.InternalFeatures.Add(new Feature(shape));
            await queryFeaturesOverlay.RefreshAsync();

            // Perform the spatial query using the drawn shape and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(shape, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Clear the drawn shape
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }

        /// <summary>
        /// Performs the spatial query when a new line is drawn
        /// </summary>
        private async void OnLineDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            await GetFeaturesCrossingAsync(e.TrackShape);
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            // Set the drawing mode to 'Line'
            mapView.TrackOverlay.TrackMode = TrackMode.Line;
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private Label queryLabel;
        private Label clickLabel;
        private Label doubleClickLabel;
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
            clickLabel = new Label();
            doubleClickLabel = new Label();
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
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0D;
            mapView.Size = new System.Drawing.Size(840, 638);
            mapView.MapClick += new System.EventHandler<MapClickMapViewEventArgs>(this.mapView_MapClick);
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            consolePanel.BackColor = System.Drawing.Color.Gray;
            consolePanel.Controls.Add(panTextBox);
            consolePanel.Controls.Add(zoomLabel);
            consolePanel.Controls.Add(zoomTextBox);
            consolePanel.Controls.Add(panLabel);
            consolePanel.Controls.Add(numberOfFeaturesFoundTextBox);
            consolePanel.Controls.Add(clickLabel);
            consolePanel.Controls.Add(doubleClickLabel);
            consolePanel.Controls.Add(queryLabel);
            consolePanel.Location = new System.Drawing.Point(839, 0);
            consolePanel.Name = "panel1";
            consolePanel.Size = new System.Drawing.Size(300, 638);
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
            queryLabel.Text = "Perform a 'Crosses' Query";
            queryLabel.TabIndex = 2;
            // 
            // clickLabel
            // 
            clickLabel.AutoSize = true;
            clickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            clickLabel.ForeColor = System.Drawing.Color.White;
            clickLabel.Location = new System.Drawing.Point(20, 50);
            clickLabel.Name = "clickLabel";
            clickLabel.Size = new System.Drawing.Size(192, 20);
            clickLabel.Text = "Click On The Map To Start Drawing\r\na New Line";
            clickLabel.TabIndex = 3;
            // 
            // doubleClickLabel
            // 
            doubleClickLabel.AutoSize = true;
            doubleClickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            doubleClickLabel.ForeColor = System.Drawing.Color.White;
            doubleClickLabel.Location = new System.Drawing.Point(20, 90);
            doubleClickLabel.Name = "doubleClickLabel";
            doubleClickLabel.Size = new System.Drawing.Size(192, 20);
            doubleClickLabel.Text = "Double-Click On The Map To\r\nFinish Drawing";
            doubleClickLabel.TabIndex = 4;
            // 
            // txtNumberOfFeaturesFound
            // 
            numberOfFeaturesFoundTextBox.BackColor = System.Drawing.Color.Gray;
            numberOfFeaturesFoundTextBox.BorderStyle = BorderStyle.None;
            numberOfFeaturesFoundTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            numberOfFeaturesFoundTextBox.ForeColor = System.Drawing.Color.White;
            numberOfFeaturesFoundTextBox.Location = new System.Drawing.Point(25, 130);
            numberOfFeaturesFoundTextBox.Multiline = true;
            numberOfFeaturesFoundTextBox.Name = "txtNumberOfFeaturesFound";
            numberOfFeaturesFoundTextBox.Size = new System.Drawing.Size(260, 48);
            numberOfFeaturesFoundTextBox.TabIndex = 5;
            // 
            // panLabel
            // 
            panLabel.AutoSize = true;
            panLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            panLabel.ForeColor = System.Drawing.Color.White;
            panLabel.Location = new System.Drawing.Point(20, 180);
            panLabel.Name = "panLabel";
            panLabel.Size = new System.Drawing.Size(192, 20);
            panLabel.Text = "Pan the Map";
            panLabel.TabIndex = 6;
            // 
            // panTextBox
            // 
            panTextBox.BackColor = System.Drawing.Color.Gray;
            panTextBox.BorderStyle = BorderStyle.None;
            panTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            panTextBox.ForeColor = System.Drawing.Color.White;
            panTextBox.Location = new System.Drawing.Point(25, 205);
            panTextBox.Multiline = true;
            panTextBox.Name = "panTextBox";
            panTextBox.Size = new System.Drawing.Size(260, 90);
            panTextBox.Text = "You can press and hold the middle mouse button (scroll wheel) to move the map, even while drawing a line. This allows you to reposition the view before placing the next point.";
            panTextBox.TabIndex = 7;
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = System.Drawing.Color.White;
            zoomLabel.Location = new System.Drawing.Point(20, 310);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new System.Drawing.Size(192, 20);
            zoomLabel.Text = "Zoom In/Out";
            zoomLabel.TabIndex = 8;
            // 
            // zoomTextBox
            // 
            zoomTextBox.BackColor = System.Drawing.Color.Gray;
            zoomTextBox.BorderStyle = BorderStyle.None;
            zoomTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            zoomTextBox.ForeColor = System.Drawing.Color.White;
            zoomTextBox.Location = new System.Drawing.Point(25, 335);
            zoomTextBox.Multiline = true;
            zoomTextBox.Name = "zoomTextBox";
            zoomTextBox.Size = new System.Drawing.Size(260, 48);
            zoomTextBox.Text = "Scroll the mouse wheel forward to zoom in, and backward to zoom out.";
            zoomTextBox.TabIndex = 9;
            // 
            // FindCrossingFeatures
            // 
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "FindCrossingFeatures";
            Size = new System.Drawing.Size(1139, 638);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

    }
}