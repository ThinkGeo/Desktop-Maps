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
    public class FindContainingFeatures : UserControl
    {
        public FindContainingFeatures()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (used in Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a feature layer to hold the Frisco zoning data
            ShapeFileFeatureLayer zoningLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Zoning.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco zoning polygons
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Set the map extent to Frisco, TX
            mapView.CurrentExtent = new RectangleShape(-10781137.28, 3917162.59, -10774579.34, 3911241.35);

            // Create a layer to hold features found by the spatial query
            InMemoryFeatureLayer highlightedFeaturesLayer = new InMemoryFeatureLayer();
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.MidnightBlue), GeoColors.MidnightBlue);
            highlightedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add each feature layer to it's own overlay
            // We do this so we can control and refresh/redraw each layer individually
            LayerOverlay zoningOverlay = new LayerOverlay();
            zoningOverlay.Layers.Add("Frisco Zoning", zoningLayer);
            mapView.Overlays.Add("Frisco Zoning Overlay", zoningOverlay);

            LayerOverlay highlightedFeaturesOverlay = new LayerOverlay();
            highlightedFeaturesOverlay.Layers.Add("Highlighted Features", highlightedFeaturesLayer);
            mapView.Overlays.Add("Highlighted Features Overlay", highlightedFeaturesOverlay);

            // Add a MarkerOverlay to the map to display the selected point for the query
            SimpleMarkerOverlay queryFeatureMarkerOverlay = new SimpleMarkerOverlay();
            mapView.Overlays.Add("Query Feature Marker Overlay", queryFeatureMarkerOverlay);

            // Add a sample point to the map for the initial query
            PointShape sampleShape = new PointShape(-10779425.2690712, 3914970.73561765);
            await GetFeaturesContainingAsync(sampleShape);

            // Set the map extent to the sample shape
            mapView.CurrentExtent = new RectangleShape(-10781407.8544813, 3916678.62545891, -10777442.6836611, 3913262.84577639);

            await mapView.RefreshAsync();
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
            LayerOverlay highlightedFeaturesOverlay = (LayerOverlay)mapView.Overlays["Highlighted Features Overlay"];
            InMemoryFeatureLayer highlightedFeaturesLayer = (InMemoryFeatureLayer)highlightedFeaturesOverlay.Layers["Highlighted Features"];

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
            txtNumberOfFeaturesFound.Text = string.Format("Number of features containing the drawn shape: {0}", features.Count());
        }

        /// <summary>
        /// Perform the spatial query and draw the shapes on the map
        /// </summary>
        private async Task GetFeaturesContainingAsync(PointShape point)
        {
            // Find the layers we will be modifying in the MapView
            SimpleMarkerOverlay queryFeatureMarkerOverlay = (SimpleMarkerOverlay)mapView.Overlays["Query Feature Marker Overlay"];
            ShapeFileFeatureLayer zoningLayer = (ShapeFileFeatureLayer)mapView.FindFeatureLayer("Frisco Zoning");

            // Clear the query point marker overlaylayer and add a marker on the newly drawn point
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
        private Panel panel1;
        private Label label2;
        private Label label1;
        private TextBox txtNumberOfFeaturesFound;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtNumberOfFeaturesFound = new System.Windows.Forms.TextBox();
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
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(852, 621);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtNumberOfFeaturesFound);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(858, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 621);
            this.panel1.TabIndex = 1;
            // 
            // txtNumberOfFeaturesFound
            // 
            this.txtNumberOfFeaturesFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNumberOfFeaturesFound.Location = new System.Drawing.Point(3, 137);
            this.txtNumberOfFeaturesFound.Multiline = true;
            this.txtNumberOfFeaturesFound.Name = "txtNumberOfFeaturesFound";
            this.txtNumberOfFeaturesFound.Size = new System.Drawing.Size(283, 64);
            this.txtNumberOfFeaturesFound.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click On The Map To Draw A New\r\nPoint";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Perfom a \'Contains\' Query";
            // 
            // FindContainingFeatures
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "FindContainingFeatures";
            this.Size = new System.Drawing.Size(1147, 621);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}