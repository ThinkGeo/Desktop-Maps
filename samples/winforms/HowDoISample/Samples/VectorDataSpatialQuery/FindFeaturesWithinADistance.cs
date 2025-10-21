using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindFeaturesWithinADistance : UserControl
    {
        public FindFeaturesWithinADistance()
        {
            InitializeComponent();
            MapViewHelper.InitializeDefaultZoomScales(mapView);
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

                var highlightedFeaturesOverlay = new LayerOverlay();
                highlightedFeaturesOverlay.TileType = TileType.SingleTile;
                highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
                mapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

                // Add a MarkerOverlay to the map to display the selected point for the query
                var queryFeatureMarkerOverlay = new SimpleMarkerOverlay();
                mapView.Overlays.Add("Query Feature Marker Overlay", queryFeatureMarkerOverlay);

                // Set the map extent to the initial area
                mapView.CenterPoint = new PointShape(-10779430, 3914970);
                mapView.CurrentScale = 18060;

                sliderTextBox.DataBindings.Add("Text", searchRadiusTrackBar, "Value");
                await mapView.RefreshAsync();

                // Add a sample point to the map for the initial query
                var sampleShape = new PointShape(-10779430, 3914970);
                await GetFeaturesWithinDistanceAsync(sampleShape);
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private Collection<Feature> PerformSpatialQuery(BaseShape shape, FeatureLayer layer)
        {
            var features = new Collection<Feature>();

            // Perform the spatial query on features in the specified layer
            layer.Open();
            features = layer.QueryTools.GetFeaturesWithinDistanceOf(shape, GeographyUnit.Meter, DistanceUnit.Meter, (int)searchRadiusTrackBar.Value, ReturningColumnsType.NoColumns);
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
            numberOfFeaturesFoundTextBox.Text = $@"Number of features within distance of the drawn shape: {features.Count()}";
        }

        private async Task GetFeaturesWithinDistanceAsync(PointShape point)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeatureMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["Query Feature Marker Overlay"];
            var zoningLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query point marker overlay layer and add a marker on the newly drawn point
            queryFeatureMarkerOverlay.Markers.Clear();
            queryFeatureMarkerOverlay.Markers.Add(CreateNewMarker(point));
            await queryFeatureMarkerOverlay.RefreshAsync();

            // Perform the spatial query using the drawn point and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(point, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Disable map drawing and clear the drawn point
            mapView.TrackOverlay.TrackMode = TrackMode.None;
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
        }
        private Marker CreateNewMarker(PointShape point)
        {
            return new Marker(point)
            {
                ImageSource = new BitmapImage(new Uri("/Resources/AQUA.png", UriKind.RelativeOrAbsolute)),
                Width = 20,
                Height = 34,
                YOffset = -17
            };
        }

        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            await GetFeaturesWithinDistanceAsync(e.WorldLocation);
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private Label getFeaturesLabel;
        private Label clickLabel;
        private Label searchRadiusLabel;
        private Label panLabel;
        private Label zoomLabel;
        private TrackBar searchRadiusTrackBar;
        private TextBox sliderTextBox;
        private TextBox numberOfFeaturesFoundTextBox;
        private TextBox panTextBox;
        private TextBox zoomTextBox;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            getFeaturesLabel = new Label();
            clickLabel = new Label();
            searchRadiusLabel = new Label();
            panLabel = new Label();
            zoomLabel = new Label();
            searchRadiusTrackBar = new TrackBar();
            sliderTextBox = new TextBox();
            numberOfFeaturesFoundTextBox = new TextBox();
            panTextBox = new TextBox();
            zoomTextBox = new TextBox();
            consolePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchRadiusTrackBar).BeginInit();
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
            mapView.Size = new System.Drawing.Size(680, 626);
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
            consolePanel.Controls.Add(sliderTextBox);
            consolePanel.Controls.Add(searchRadiusTrackBar);
            consolePanel.Controls.Add(searchRadiusLabel);
            consolePanel.Controls.Add(clickLabel);
            consolePanel.Controls.Add(getFeaturesLabel);
            consolePanel.Location = new System.Drawing.Point(680, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(391, 626);
            consolePanel.TabIndex = 1;
            // 
            // getFeaturesLabel
            // 
            getFeaturesLabel.AutoSize = true;
            getFeaturesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            getFeaturesLabel.ForeColor = System.Drawing.Color.White;
            getFeaturesLabel.Location = new System.Drawing.Point(20, 18);
            getFeaturesLabel.Name = "getFeaturesLabel";
            getFeaturesLabel.Size = new System.Drawing.Size(360, 20);
            getFeaturesLabel.Text = "Get Features Within A Set Distance";
            getFeaturesLabel.TabIndex = 2;
            // 
            // clickLabel
            // 
            clickLabel.AutoSize = true;
            clickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            clickLabel.ForeColor = System.Drawing.Color.White;
            clickLabel.Location = new System.Drawing.Point(20, 60);
            clickLabel.Name = "clickLabel";
            clickLabel.Size = new System.Drawing.Size(360, 20);
            clickLabel.Text = "Click On The Map To Draw A New Point";
            clickLabel.TabIndex = 3;
            // 
            // searchRadiusLabel
            // 
            searchRadiusLabel.AutoSize = true;
            searchRadiusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            searchRadiusLabel.ForeColor = System.Drawing.Color.White;
            searchRadiusLabel.Location = new System.Drawing.Point(20, 100);
            searchRadiusLabel.Name = "searchRadiusLabel";
            searchRadiusLabel.Size = new System.Drawing.Size(130, 17);
            searchRadiusLabel.Text = "Search Radius: (m)";
            searchRadiusLabel.TabIndex = 4;
            // 
            // searchRadiusTrackBar
            // 
            searchRadiusTrackBar.Location = new System.Drawing.Point(20, 130);
            searchRadiusTrackBar.Maximum = 1600;
            searchRadiusTrackBar.Minimum = 100;
            searchRadiusTrackBar.Name = "searchRadius";
            searchRadiusTrackBar.Size = new System.Drawing.Size(260, 45);
            searchRadiusTrackBar.TickStyle = TickStyle.None;
            searchRadiusTrackBar.Value = 400;
            searchRadiusTrackBar.TabIndex = 5;
            // 
            // sliderTextBox
            // 
            sliderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            sliderTextBox.Location = new System.Drawing.Point(285, 130);
            sliderTextBox.Multiline = true;
            sliderTextBox.Name = "sliderTextBox";
            sliderTextBox.ReadOnly = true;
            sliderTextBox.Size = new System.Drawing.Size(93, 34);
            sliderTextBox.Text = "400";
            sliderTextBox.TabIndex = 6;
            // 
            // numberOfFeaturesFoundTextBox
            // 
            numberOfFeaturesFoundTextBox.BackColor = System.Drawing.Color.Gray;
            numberOfFeaturesFoundTextBox.BorderStyle = BorderStyle.None;
            numberOfFeaturesFoundTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            numberOfFeaturesFoundTextBox.ForeColor = System.Drawing.Color.White;
            numberOfFeaturesFoundTextBox.Location = new System.Drawing.Point(25, 170);
            numberOfFeaturesFoundTextBox.Multiline = true;
            numberOfFeaturesFoundTextBox.Name = "numberOfFeaturesFoundTextBox";
            numberOfFeaturesFoundTextBox.Size = new System.Drawing.Size(360, 20);
            numberOfFeaturesFoundTextBox.TabIndex = 7;
            // 
            // panLabel
            // 
            panLabel.AutoSize = true;
            panLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            panLabel.ForeColor = System.Drawing.Color.White;
            panLabel.Location = new System.Drawing.Point(20, 210);
            panLabel.Name = "panLabel";
            panLabel.Size = new System.Drawing.Size(192, 20);
            panLabel.Text = "Pan the Map";
            panLabel.TabIndex = 8;
            // 
            // panTextBox
            // 
            panTextBox.BackColor = System.Drawing.Color.Gray;
            panTextBox.BorderStyle = BorderStyle.None;
            panTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            panTextBox.ForeColor = System.Drawing.Color.White;
            panTextBox.Location = new System.Drawing.Point(25, 235);
            panTextBox.Multiline = true;
            panTextBox.Name = "panTextBox";
            panTextBox.Size = new System.Drawing.Size(360, 48);
            panTextBox.Text = "Press and hold the middle mouse button (scroll wheel), then drag the mouse to move around the map.";
            panTextBox.TabIndex = 9;
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = System.Drawing.Color.White;
            zoomLabel.Location = new System.Drawing.Point(20, 290);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new System.Drawing.Size(192, 20);
            zoomLabel.Text = "Zoom In/Out";
            zoomLabel.TabIndex = 10;
            // 
            // zoomTextBox
            // 
            zoomTextBox.BackColor = System.Drawing.Color.Gray;
            zoomTextBox.BorderStyle = BorderStyle.None;
            zoomTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            zoomTextBox.ForeColor = System.Drawing.Color.White;
            zoomTextBox.Location = new System.Drawing.Point(25, 315);
            zoomTextBox.Multiline = true;
            zoomTextBox.Name = "zoomTextBox";
            zoomTextBox.Size = new System.Drawing.Size(260, 48);
            zoomTextBox.Text = "Scroll the mouse wheel forward to zoom in, and backward to zoom out.";
            zoomTextBox.TabIndex = 11;
            // 
            // FindFeaturesWithinADistance
            // 
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "FindFeaturesWithinADistance";
            Size = new System.Drawing.Size(1075, 626);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)searchRadiusTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

    }
}