using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CheckIfFeaturesAreEqual : UserControl
    {
        public CheckIfFeaturesAreEqual()
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

                // Add a sample shape to the map for the initial query
                // To ensure topological equality for this sample, we create a new shape using the same geometry as an existing feature
                zoningLayer.Open();
                var sampleShape = zoningLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns).First().GetShape();
                zoningLayer.Close();
                await GetFeaturesEqualAsync(sampleShape);

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
            var features = layer.QueryTools.GetFeaturesTopologicalEqual(shape, ReturningColumnsType.AllColumns);
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
            numberOfFeaturesFoundTextBox.Text = $@"Number of features topologically equal to the drawn shape: {features.Count()}";
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesEqualAsync(BaseShape shape)
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
        private FlowLayoutPanel controlsGroupPanel;
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
            controlsGroupPanel = new FlowLayoutPanel();
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
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(960, 622);
            this.mapView.TabIndex = 0;
            // 
            // consolePanel
            // 
            consolePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            consolePanel.BackColor = System.Drawing.Color.Gray;
            consolePanel.Controls.Add(controlsGroupPanel);
            consolePanel.Location = new System.Drawing.Point(960, 0);
            consolePanel.Name = "consolePanel";
            consolePanel.Size = new System.Drawing.Size(300, 622);
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
            controlsGroupPanel.Controls.Add(this.numberOfFeaturesFoundTextBox);
            controlsGroupPanel.Controls.Add(this.panLabel);
            controlsGroupPanel.Controls.Add(this.panTextBox);
            controlsGroupPanel.Controls.Add(this.zoomLabel);
            controlsGroupPanel.Controls.Add(this.zoomTextBox);
            // 
            // queryLabel
            // 
            queryLabel.AutoSize = true;
            queryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            queryLabel.ForeColor = System.Drawing.Color.White;
            queryLabel.Margin = new Padding(0, 0, 0, 6);
            queryLabel.Name = "queryLabel";
            queryLabel.Text = "Perform an 'Equals' Query";
            queryLabel.TabIndex = 2;
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
            numberOfFeaturesFoundTextBox.Width = 260;
            TextBoxHelper.AdjustTextBoxHeightForDpi(numberOfFeaturesFoundTextBox, 48);
            numberOfFeaturesFoundTextBox.TabIndex = 3;
            // 
            // panLabel
            // 
            panLabel.AutoSize = true;
            panLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            panLabel.ForeColor = System.Drawing.Color.White;
            panLabel.Margin = new Padding(0, 0, 0, 6);
            panLabel.Name = "panLabel";
            panLabel.Size = new System.Drawing.Size(260, 20);
            panLabel.Text = "Pan the Map";
            panLabel.TabIndex = 4;
            // 
            // panTextBox
            // 
            panTextBox.BackColor = System.Drawing.Color.Gray;
            panTextBox.BorderStyle = BorderStyle.None;
            panTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            panTextBox.ForeColor = System.Drawing.Color.White;
            panTextBox.Margin = new Padding(0, 0, 0, 6);
            panTextBox.Multiline = true;
            panTextBox.Name = "textBox1";
            panTextBox.Width = 260;
            panTextBox.Text = "Press and hold the middle mouse button (scroll wheel), then drag the mouse to move around the map.";
            TextBoxHelper.AdjustTextBoxHeight(panTextBox);
            panTextBox.TabIndex = 5;
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            zoomLabel.ForeColor = System.Drawing.Color.White;
            zoomLabel.Margin = new Padding(0, 0, 0, 6);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new System.Drawing.Size(260, 20);
            zoomLabel.Text = "Zoom In/Out";
            zoomLabel.TabIndex = 6;
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
            zoomTextBox.Text = "Scroll the mouse wheel forward to zoom in, and backward to zoom out.";
            TextBoxHelper.AdjustTextBoxHeight(zoomTextBox);
            zoomTextBox.TabIndex = 7;
            // 
            // CheckIfFeaturesAreEqual
            // 
            this.AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(consolePanel);
            Controls.Add(mapView);
            Name = "CheckIfFeaturesAreEqual";
            Size = new System.Drawing.Size(1260, 622);
            Load += Form_Load;
            consolePanel.ResumeLayout(false);
            consolePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

    }

    public static class TextBoxHelper
    {
        public static void AdjustTextBoxHeight(TextBox textBox)
        {
            if (!textBox.Multiline) return;

            var size = TextRenderer.MeasureText(
                textBox.Text,
                textBox.Font,
                new Size(textBox.Width, int.MaxValue),
                TextFormatFlags.WordBreak
            );

            textBox.Height = size.Height + textBox.Margin.Vertical;
        }

        /// <summary>
        /// Adjusts the TextBox height based on the current screen DPI scaling.
        /// Example: baseHeight = 48 at 100% (96 DPI).
        /// </summary>
        public static void AdjustTextBoxHeightForDpi(TextBox textBox, int baseHeight)
        {
            if (!textBox.Multiline) return;

            // get DPI from the parent control if possible
            float dpi = textBox.DeviceDpi;  // available on WinForms controls in .NET Framework 4.7+ and .NET Core/5+/6+/8+

            // scale height relative to 96 DPI (100% scale)
            int scaledHeight = (int)Math.Round(baseHeight * dpi / 96f);

            textBox.Height = scaledHeight;
        }
    }
}