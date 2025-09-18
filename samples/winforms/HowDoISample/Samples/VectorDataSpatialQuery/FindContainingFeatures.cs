using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindContainingFeatures : UserControl
    {
        public FindContainingFeatures()
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

                // Set the map extent to the sample shape
                mapView.CenterPoint = new PointShape(-10779430, 3914970);
                mapView.CurrentScale = 18060;

                await mapView.RefreshAsync();

                // Add a sample point to the map for the initial query
                var sampleShape = new PointShape(-10779425, 3914970);
                await GetFeaturesContainingAsync(sampleShape);
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
            var features = layer.QueryTools.GetFeaturesContaining(shape, ReturningColumnsType.AllColumns);
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
            numberOfFeaturesFoundTextBox.Text = $@"Number of features containing the drawn shape: {features.Count()}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesContainingAsync(PointShape point)
        {
            // Find the layers we will be modifying in the MapView
            var queryFeatureMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["Query Feature Marker Overlay"];
            var zoningLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query point marker overlay layer and add a marker on the newly drawn point
            queryFeatureMarkerOverlay.Markers.Clear();

            // Create a marker with a static marker image and add it to the map
            var marker = CreateNewMarker(point);
            queryFeatureMarkerOverlay.Markers.Add(marker);
            await queryFeatureMarkerOverlay.RefreshAsync();

            // Perform the spatial query using the drawn point and highlight features that were found
            var queriedFeatures = PerformSpatialQuery(point, zoningLayer);
            await HighlightQueriedFeaturesAsync(queriedFeatures);

            // Clear the drawn point
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
            await GetFeaturesContainingAsync(e.WorldLocation);
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel consolePanel;
        private FlowLayoutPanel controlsGroupPanel;
        private Label queryLabel;
        private Label clickLabel;
        private Label panLabel;
        private Label zoomLabel;
        private TextBox numberOfFeaturesFoundTextBox;
        private TextBox panTextBox;
        private TextBox zoomTextBox;

        private void InitializeComponent()
        {
            mapView = new MapView();
            consolePanel = new Panel();
            controlsGroupPanel = new FlowLayoutPanel();
            queryLabel = new Label();
            clickLabel = new Label();
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
            mapView.Size = new System.Drawing.Size(858, 622);
            mapView.MapClick += new System.EventHandler<MapClickMapViewEventArgs>(this.mapView_MapClick);
            mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            consolePanel.BackColor = System.Drawing.Color.Gray;
            consolePanel.Controls.Add(controlsGroupPanel);
            consolePanel.Location = new System.Drawing.Point(858, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(289, 622);
            consolePanel.TabIndex = 1;
            // 
            // controlsGroupPanel
            // 
            controlsGroupPanel.AutoSize = true;
            controlsGroupPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlsGroupPanel.FlowDirection = FlowDirection.TopDown;
            controlsGroupPanel.WrapContents = false;
            controlsGroupPanel.Location = new System.Drawing.Point(18, 20);
            controlsGroupPanel.Size = new System.Drawing.Size(260, 120);
            controlsGroupPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            controlsGroupPanel.BackColor = System.Drawing.Color.Transparent;
            controlsGroupPanel.Controls.Add(this.queryLabel);
            controlsGroupPanel.Controls.Add(this.clickLabel);
            controlsGroupPanel.Controls.Add(this.numberOfFeaturesFoundTextBox);
            controlsGroupPanel.Controls.Add(this.panLabel);
            controlsGroupPanel.Controls.Add(this.panTextBox);
            controlsGroupPanel.Controls.Add(this.zoomLabel);
            controlsGroupPanel.Controls.Add(this.zoomTextBox);
            // 
            // queryLabel
            // 
            queryLabel.AutoSize = true;
            queryLabel.MaximumSize = new Size(0, 0);
            queryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            queryLabel.ForeColor = System.Drawing.Color.White;
            queryLabel.Margin = new Padding(0, 0, 0, 6);
            queryLabel.Name = "queryLabel";
            queryLabel.Size = new System.Drawing.Size(192, 20);
            queryLabel.TabIndex = 0;
            queryLabel.Text = "Perfom a 'Contains' Query";
            // 
            // clickLabel
            // 
            clickLabel.AutoSize = true;
            clickLabel.MaximumSize = new Size(0, 0);
            clickLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            clickLabel.ForeColor = System.Drawing.Color.White;
            clickLabel.Margin = new Padding(0, 0, 0, 6);
            clickLabel.Name = "clickLabel";
            clickLabel.Size = new System.Drawing.Size(192, 20);
            clickLabel.TabIndex = 1;
            clickLabel.Text = "Click On The Map To Draw A New Point";
            // 
            // numberOfFeaturesFoundTextBox
            // 
            numberOfFeaturesFoundTextBox.BackColor = System.Drawing.Color.Gray;
            numberOfFeaturesFoundTextBox.BorderStyle = BorderStyle.None;
            numberOfFeaturesFoundTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            numberOfFeaturesFoundTextBox.ForeColor = System.Drawing.Color.White;
            numberOfFeaturesFoundTextBox.Margin = new Padding(0, 0, 0, 6);
            numberOfFeaturesFoundTextBox.Multiline = true;
            numberOfFeaturesFoundTextBox.Name = "numberOfFeaturesFoundTextBox";
            numberOfFeaturesFoundTextBox.Size = new System.Drawing.Size(260, 48);
            numberOfFeaturesFoundTextBox.TabIndex = 2;
            // 
            // panLabel
            // 
            panLabel.AutoSize = true;
            panLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            panLabel.ForeColor = System.Drawing.Color.White;
            panLabel.Margin = new Padding(0, 0, 0, 6);
            panLabel.Name = "panLabel";
            panLabel.Size = new System.Drawing.Size(192, 20);
            panLabel.TabIndex = 3;
            panLabel.Text = "Pan the Map";
            // 
            // panTextBox
            // 
            panTextBox.BackColor = System.Drawing.Color.Gray;
            panTextBox.BorderStyle = BorderStyle.None;
            panTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            panTextBox.ForeColor = System.Drawing.Color.White;
            panTextBox.Margin = new Padding(0, 0, 0, 6);
            panTextBox.Multiline = true;
            panTextBox.Name = "panTextBox";
            panTextBox.Width = 260;
            panTextBox.TabIndex = 4;
            panTextBox.Text = "Press and hold the middle mouse button (scroll wheel), then drag the mouse to move around the map.";
            TextBoxHelper.AdjustTextBoxHeight(panTextBox);
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = System.Drawing.Color.White;
            zoomLabel.Margin = new Padding(0, 0, 0, 6);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new System.Drawing.Size(192, 20);
            zoomLabel.TabIndex = 5;
            zoomLabel.Text = "Zoom In/Out";
            // 
            // zoomTextBox
            // 
            zoomTextBox.BackColor = System.Drawing.Color.Gray;
            zoomTextBox.BorderStyle = BorderStyle.None;
            zoomTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            zoomTextBox.ForeColor = System.Drawing.Color.White;
            zoomTextBox.Margin = new Padding(0, 0, 0, 6);
            zoomTextBox.Multiline = true;
            zoomTextBox.Name = "zoomTextBox";
            zoomTextBox.Width = 260;
            zoomTextBox.TabIndex = 6;
            zoomTextBox.Text = "Scroll the mouse wheel forward to zoom in, and backward to zoom out.";
            TextBoxHelper.AdjustTextBoxHeight(zoomTextBox);
            // 
            // FindContainingFeatures
            // 
            this.AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "FindContainingFeatures";
            Size = new System.Drawing.Size(1147, 621);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

    }
}