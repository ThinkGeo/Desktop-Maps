using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class PointValidationSample: UserControl
    {
        public PointValidationSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create an InMemoryFeatureLayer to hold the shapes to be validated
            // Add styles to display points, lines, and polygons on this layer in green
            InMemoryFeatureLayer validatedFeaturesLayer = new InMemoryFeatureLayer();
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Green), GeoColors.Green);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 3, false);
            validatedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the shapes to perform the validation against
            // Add styles to display points, lines, and polygons on this layer in blue
            InMemoryFeatureLayer filterFeaturesLayer = new InMemoryFeatureLayer();
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 12, GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Blue), GeoColors.Blue);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 3, false);
            filterFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create an InMemoryFeatureLayer to hold the resultf features from the validation API
            // Add styles to display points, lines, and polygons on this layer in red
            InMemoryFeatureLayer resultFeaturesLayer = new InMemoryFeatureLayer();
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Red, 12, GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.Red), GeoColors.Red);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 3, false);
            resultFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layers to an overlay, and add the overlay to the map
            LayerOverlay featuresOverlay = new LayerOverlay();
            featuresOverlay.Layers.Add("Filter Features", filterFeaturesLayer);
            featuresOverlay.Layers.Add("Validated Features", validatedFeaturesLayer);
            featuresOverlay.Layers.Add("Result Features", resultFeaturesLayer);
            mapView.Overlays.Add("Features Overlay", featuresOverlay);

            // Set a default extent for the map
            mapView.CurrentExtent = new RectangleShape(0, 200, 200, 0);

            rdoCheckIfPointsAreTouchingLines.Checked = true;

        }

        private Panel panel1;
        private RadioButton rdoCheckIfPointsAreWithinPolygons;
        private RadioButton rdoCheckIfPointsAreTouchingPolygonBoundaries;
        private RadioButton rdoCheckIfPointsAreTouchingLineEndpoints;
        private RadioButton rdoCheckIfPointsAreTouchingLines;
        private Label label1;
        private TextBox txtValidationInfo;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtValidationInfo = new System.Windows.Forms.TextBox();
            this.rdoCheckIfPointsAreWithinPolygons = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPointsAreTouchingLineEndpoints = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPointsAreTouchingLines = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.Meter;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1185, 664);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtValidationInfo);
            this.panel1.Controls.Add(this.rdoCheckIfPointsAreWithinPolygons);
            this.panel1.Controls.Add(this.rdoCheckIfPointsAreTouchingPolygonBoundaries);
            this.panel1.Controls.Add(this.rdoCheckIfPointsAreTouchingLineEndpoints);
            this.panel1.Controls.Add(this.rdoCheckIfPointsAreTouchingLines);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(781, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 664);
            this.panel1.TabIndex = 1;
            // 
            // txtValidationInfo
            // 
            this.txtValidationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidationInfo.Location = new System.Drawing.Point(3, 208);
            this.txtValidationInfo.Multiline = true;
            this.txtValidationInfo.Name = "txtValidationInfo";
            this.txtValidationInfo.Size = new System.Drawing.Size(398, 190);
            this.txtValidationInfo.TabIndex = 5;
            // 
            // rdoCheckIfPointsAreWithinPolygons
            // 
            this.rdoCheckIfPointsAreWithinPolygons.AutoSize = true;
            this.rdoCheckIfPointsAreWithinPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPointsAreWithinPolygons.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPointsAreWithinPolygons.Location = new System.Drawing.Point(22, 177);
            this.rdoCheckIfPointsAreWithinPolygons.Name = "rdoCheckIfPointsAreWithinPolygons";
            this.rdoCheckIfPointsAreWithinPolygons.Size = new System.Drawing.Size(270, 24);
            this.rdoCheckIfPointsAreWithinPolygons.TabIndex = 4;
            this.rdoCheckIfPointsAreWithinPolygons.TabStop = true;
            this.rdoCheckIfPointsAreWithinPolygons.Text = "Points Must Be Within Polygons";
            this.rdoCheckIfPointsAreWithinPolygons.UseVisualStyleBackColor = true;
            this.rdoCheckIfPointsAreWithinPolygons.CheckedChanged += new System.EventHandler(this.rdoCheckIfPointsAreWithinPolygons_CheckedChanged);
            // 
            // rdoCheckIfPointsAreTouchingPolygonBoundaries
            // 
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.AutoSize = true;
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.Location = new System.Drawing.Point(22, 150);
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.Name = "rdoCheckIfPointsAreTouchingPolygonBoundaries";
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.Size = new System.Drawing.Size(381, 24);
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.TabIndex = 3;
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.TabStop = true;
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.Text = "Points Must Be Touching Polygons Boundaries";
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.UseVisualStyleBackColor = true;
            this.rdoCheckIfPointsAreTouchingPolygonBoundaries.CheckedChanged += new System.EventHandler(this.rdoCheckIfPointsAreTouchingPolygonBoundaries_CheckedChanged);
            // 
            // rdoCheckIfPointsAreTouchingLineEndpoints
            // 
            this.rdoCheckIfPointsAreTouchingLineEndpoints.AutoSize = true;
            this.rdoCheckIfPointsAreTouchingLineEndpoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPointsAreTouchingLineEndpoints.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPointsAreTouchingLineEndpoints.Location = new System.Drawing.Point(22, 123);
            this.rdoCheckIfPointsAreTouchingLineEndpoints.Name = "rdoCheckIfPointsAreTouchingLineEndpoints";
            this.rdoCheckIfPointsAreTouchingLineEndpoints.Size = new System.Drawing.Size(336, 24);
            this.rdoCheckIfPointsAreTouchingLineEndpoints.TabIndex = 2;
            this.rdoCheckIfPointsAreTouchingLineEndpoints.TabStop = true;
            this.rdoCheckIfPointsAreTouchingLineEndpoints.Text = "Points Must Be Touching Line EndPoints";
            this.rdoCheckIfPointsAreTouchingLineEndpoints.UseVisualStyleBackColor = true;
            this.rdoCheckIfPointsAreTouchingLineEndpoints.CheckedChanged += new System.EventHandler(this.rdoCheckIfPointsAreTouchingLineEndpoints_CheckedChanged);
            // 
            // rdoCheckIfPointsAreTouchingLines
            // 
            this.rdoCheckIfPointsAreTouchingLines.AutoSize = true;
            this.rdoCheckIfPointsAreTouchingLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPointsAreTouchingLines.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPointsAreTouchingLines.Location = new System.Drawing.Point(22, 96);
            this.rdoCheckIfPointsAreTouchingLines.Name = "rdoCheckIfPointsAreTouchingLines";
            this.rdoCheckIfPointsAreTouchingLines.Size = new System.Drawing.Size(264, 24);
            this.rdoCheckIfPointsAreTouchingLines.TabIndex = 1;
            this.rdoCheckIfPointsAreTouchingLines.TabStop = true;
            this.rdoCheckIfPointsAreTouchingLines.Text = "Points Must Be Touching Lines";
            this.rdoCheckIfPointsAreTouchingLines.UseVisualStyleBackColor = true;
            this.rdoCheckIfPointsAreTouchingLines.CheckedChanged += new System.EventHandler(this.rdoCheckIfPointsAreTouchingLines_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Point Validation Tests";
            // 
            // PointValidationSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "PointValidationSample";
            this.Size = new System.Drawing.Size(1185, 664);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void rdoCheckIfPointsAreTouchingLines_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of point and line features to use for the validation
            Feature uncoveredPointFeature1 = new Feature("POINT(0 0)");
            Feature uncoveredPointFeature2 = new Feature("POINT(50 0)");
            Feature coveredPointFeature = new Feature("POINT(150 0)");
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> points = new Collection<Feature>() { uncoveredPointFeature1, uncoveredPointFeature2, coveredPointFeature };
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.PointsMustTouchLines(points, lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { uncoveredPointFeature1, uncoveredPointFeature2, coveredPointFeature }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching lines are shown in green. \n\nPoints not touching lines are shown in red.";

        }

        private void rdoCheckIfPointsAreTouchingLineEndpoints_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of point and line features to use for the validation
            Feature pointFeature1 = new Feature("POINT(0 0)");
            Feature pointFeature2 = new Feature("POINT(50 0)");
            Feature pointFeatureOnEndpoint = new Feature("POINT(100 0)");
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnEndpoint };
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.PointsMustTouchLineEndpoints(points, lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnEndpoint }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching line endpoints are shown in green. \n\nPoints not touching line endpoints are shown in red.";

        }

        private void rdoCheckIfPointsAreTouchingPolygonBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of point and polygon features to use for the validation
            Feature pointFeature1 = new Feature("POINT(150 0)");
            Feature pointFeature2 = new Feature("POINT(50 50)");
            Feature pointFeatureOnBoundary = new Feature("POINT(0 0)");
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnBoundary };
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            TopologyValidationResult result = TopologyValidator.PointsMustTouchPolygonBoundaries(points, polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureOnBoundary }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints touching polygon boundaries are shown in green. \n\nPoints not touching polygon boundaries are shown in red.";

        }

        private void rdoCheckIfPointsAreWithinPolygons_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of point and polygon features to use for the validation
            Feature pointFeature1 = new Feature("POINT(150 0)");
            Feature pointFeature2 = new Feature("POINT(0 0)");
            Feature pointFeatureInsidePolygon = new Feature("POINT(50 50)");
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> points = new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureInsidePolygon };
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            TopologyValidationResult result = TopologyValidator.PointsMustBeWithinPolygons(points, polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { pointFeature1, pointFeature2, pointFeatureInsidePolygon }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPoints within polygons are shown in green. \n\nPoints not within polygons are shown in red.";

        }

        private void ClearMapAndAddFeatures(Collection<Feature> validatedFeatures, Collection<Feature> resultFeatures, Collection<Feature> filterFeatures = null)
        {
            // Get the InMemoryFeatureLayers from the MapView
            InMemoryFeatureLayer validatedFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Validated Features");
            InMemoryFeatureLayer filterFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Filter Features");
            InMemoryFeatureLayer resultFeaturesLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Result Features");

            validatedFeaturesLayer.Open();
            filterFeaturesLayer.Open();
            resultFeaturesLayer.Open();

            // Clear the existing features from each layer
            validatedFeaturesLayer.Clear();
            filterFeaturesLayer.Clear();
            resultFeaturesLayer.Clear();

            // Add (blue) filter features to the map, if there are any
            if (filterFeatures != null)
            {
                foreach (Feature filterFeature in filterFeatures)
                {
                    filterFeaturesLayer.InternalFeatures.Add(filterFeature);
                }
            }

            // Add (green) validated features to the map
            foreach (Feature validatedFeature in validatedFeatures)
            {
                validatedFeaturesLayer.InternalFeatures.Add(validatedFeature);
            }

            // Add (red) invalid features to the map
            foreach (Feature resultFeature in resultFeatures)
            {
                resultFeaturesLayer.InternalFeatures.Add(resultFeature);
            }

            // Refresh/redraw the layers and reset the map extent
            LayerOverlay featureOverlay = (LayerOverlay)mapView.Overlays["Features Overlay"];
            mapView.CurrentExtent = featureOverlay.GetBoundingBox();
            mapView.ZoomOut();
            mapView.Refresh();

            validatedFeaturesLayer.Close();
            filterFeaturesLayer.Close();
            resultFeaturesLayer.Close();
        }

    }
}