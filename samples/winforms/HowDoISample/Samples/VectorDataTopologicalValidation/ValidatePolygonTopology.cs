using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ValidatePolygonTopology : UserControl
    {
        public ValidatePolygonTopology()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
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

            rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Checked = true;

            await mapView.RefreshAsync();
        }


        private async void rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature coveredPolygonFeature = new Feature("POLYGON((0 0,50 0,50 50,0 50,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { coveredPolygonFeature };
            TopologyValidationResult result = TopologyValidator.PolygonBoundariesMustOverlapPolygonBoundaries(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { coveredPolygonFeature }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.";

        }

        private async void rdoCheckIfPolygonBoundariesOverlapLines_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon and line features to use for the validation
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature lineFeature = new Feature("LINESTRING(-50 0,100 0,100 150)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.PolygonBoundariesMustOverlapLines(polygons, lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature }, invalidResultFeatures, new Collection<Feature>() { lineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons being validated are shown in green. \n\nNon-overlapping polygon boundaries are shown in red.";

        }

        private async void rdoCheckIfPolygonsOverlapPolygons_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustOverlapPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nOverlapping regions are shown in green. \n\nNon-overlapping regions are shown in red.";

        }

        private async void rdoCheckIfPolygonsAreWithinPolygons_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustBeWithinPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons fully within polygons are shown in green. \n\nPolygons not within polygons are shown in red.";

        }

        private async void rdoCheckIfPolygonsContainPoints_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of points and polygon features to use for the validation
            Feature pointFeature = new Feature("POINT(50 50)");
            Feature polygonWithPointFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature polygonFeature = new Feature("POLYGON((150 0,250 0,250 100,150 100,150 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature, polygonWithPointFeature };
            Collection<Feature> points = new Collection<Feature>() { pointFeature };
            TopologyValidationResult result = TopologyValidator.PolygonsMustContainPoint(polygons, points);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature, polygonWithPointFeature }, invalidResultFeatures, new Collection<Feature>() { pointFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nPolygons containing points are shown in green. \n\nPolygons not containing points are shown in red.";

        }

        private async void rdoCheckIfPolygonsCoverEachOther_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");
            Feature polygonFeature2 = new Feature("POLYGON((-50 -50,50 -50,50 50,-50 50,-50 -50))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> firstPolygonsSet = new Collection<Feature>() { polygonFeature1 };
            Collection<Feature> secondPolygonsSet = new Collection<Feature>() { polygonFeature2 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustOverlapEachOther(firstPolygonsSet, secondPolygonsSet);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "All non-overlapping regions from two different sets of polygons are shown in red. \n\nOverlapping regions are shown in green";

        }

        private async void rdoCheckIfPolygonsHaveGaps_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((0 0,40 0,40 40,0 40,0 0))");
            Feature polygonFeature2 = new Feature("POLYGON((30 30,70 30,70 70,30 70,30 30))");
            Feature polygonFeature3 = new Feature("POLYGON((60 0,100 0,100 40,60 40,60 0))");
            Feature polygonFeature4 = new Feature("POLYGON((30 10,70 10,70 -30,30 -30,30 10))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotHaveGaps(polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Features being validated are shown in green. \n\nGaps (Inner rings) within the union of the polygons are shown in red.";

        }

        private async void rdoCheckPolygonsMustNotOverlap_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature polygonFeature4 = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotOverlap(polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3, polygonFeature4 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Features being validated are shown in green. \n\nOverlapping polygon regions are shown in red.";

        }

        private async void rdoCheckPolygonsMustNotOverlapPolygons_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of polygon features to use for the validation
            Feature polygonFeature1 = new Feature("POLYGON((25 25,50 25,50 50,25 50,25 25))");
            Feature polygonFeature2 = new Feature("POLYGON((75 25,125 25,125 75,75 75,75 25))");
            Feature polygonFeature3 = new Feature("POLYGON((150 25,200 25,200 75,150 75,150 25))");
            Feature coveringPolygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringPolygons = new Collection<Feature>() { coveringPolygonFeature };
            Collection<Feature> coveredPolygons = new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 };
            TopologyValidationResult result = TopologyValidator.PolygonsMustNotOverlapPolygons(coveringPolygons, coveredPolygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { polygonFeature1, polygonFeature2, polygonFeature3 }, invalidResultFeatures, new Collection<Feature>() { coveringPolygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nNon-overlapping polygon regions are shown in green. \n\nOverlapping polygon regions are shown in red.";

        }

        private async Task ClearMapAndAddFeaturesAsync(Collection<Feature> validatedFeatures, Collection<Feature> resultFeatures, Collection<Feature> filterFeatures = null)
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
            await mapView.RefreshAsync();

            validatedFeaturesLayer.Close();
            filterFeaturesLayer.Close();
            resultFeaturesLayer.Close();
            await mapView.ZoomOutAsync();
        }

        #region Component Designer generated code

        private Panel panel1;
        private RadioButton rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries;
        private Label label1;
        private RadioButton rdoCheckPolygonsMustNotOverlapPolygons;
        private RadioButton rdoCheckPolygonsMustNotOverlap;
        private RadioButton rdoCheckIfPolygonsHaveGaps;
        private RadioButton rdoCheckIfPolygonsCoverEachOther;
        private RadioButton rdoCheckIfPolygonsContainPoints;
        private RadioButton rdoCheckIfPolygonsAreWithinPolygons;
        private RadioButton rdoCheckIfPolygonsOverlapPolygons;
        private RadioButton rdoCheckIfPolygonBoundariesOverlapLines;
        private TextBox txtValidationInfo;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtValidationInfo = new System.Windows.Forms.TextBox();
            this.rdoCheckPolygonsMustNotOverlapPolygons = new System.Windows.Forms.RadioButton();
            this.rdoCheckPolygonsMustNotOverlap = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonsHaveGaps = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonsCoverEachOther = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonsContainPoints = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonsAreWithinPolygons = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonsOverlapPolygons = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonBoundariesOverlapLines = new System.Windows.Forms.RadioButton();
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(848, 718);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtValidationInfo);
            this.panel1.Controls.Add(this.rdoCheckPolygonsMustNotOverlapPolygons);
            this.panel1.Controls.Add(this.rdoCheckPolygonsMustNotOverlap);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonsHaveGaps);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonsCoverEachOther);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonsContainPoints);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonsAreWithinPolygons);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonsOverlapPolygons);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonBoundariesOverlapLines);
            this.panel1.Controls.Add(this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(851, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 718);
            this.panel1.TabIndex = 1;
            // 
            // txtValidationInfo
            // 
            this.txtValidationInfo.BackColor = System.Drawing.Color.Gray;
            this.txtValidationInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtValidationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValidationInfo.ForeColor = System.Drawing.Color.White;
            this.txtValidationInfo.Location = new System.Drawing.Point(22, 308);
            this.txtValidationInfo.Multiline = true;
            this.txtValidationInfo.Name = "txtValidationInfo";
            this.txtValidationInfo.Size = new System.Drawing.Size(438, 170);
            this.txtValidationInfo.TabIndex = 10;
            // 
            // rdoCheckPolygonsMustNotOverlapPolygons
            // 
            this.rdoCheckPolygonsMustNotOverlapPolygons.AutoSize = true;
            this.rdoCheckPolygonsMustNotOverlapPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckPolygonsMustNotOverlapPolygons.ForeColor = System.Drawing.Color.White;
            this.rdoCheckPolygonsMustNotOverlapPolygons.Location = new System.Drawing.Point(22, 277);
            this.rdoCheckPolygonsMustNotOverlapPolygons.Name = "rdoCheckPolygonsMustNotOverlapPolygons";
            this.rdoCheckPolygonsMustNotOverlapPolygons.Size = new System.Drawing.Size(354, 24);
            this.rdoCheckPolygonsMustNotOverlapPolygons.TabIndex = 9;
            this.rdoCheckPolygonsMustNotOverlapPolygons.TabStop = true;
            this.rdoCheckPolygonsMustNotOverlapPolygons.Text = "Polygons Must Not Overlap Other Polygons";
            this.rdoCheckPolygonsMustNotOverlapPolygons.UseVisualStyleBackColor = true;
            this.rdoCheckPolygonsMustNotOverlapPolygons.CheckedChanged += new System.EventHandler(this.rdoCheckPolygonsMustNotOverlapPolygons_CheckedChanged);
            // 
            // rdoCheckPolygonsMustNotOverlap
            // 
            this.rdoCheckPolygonsMustNotOverlap.AutoSize = true;
            this.rdoCheckPolygonsMustNotOverlap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckPolygonsMustNotOverlap.ForeColor = System.Drawing.Color.White;
            this.rdoCheckPolygonsMustNotOverlap.Location = new System.Drawing.Point(22, 247);
            this.rdoCheckPolygonsMustNotOverlap.Name = "rdoCheckPolygonsMustNotOverlap";
            this.rdoCheckPolygonsMustNotOverlap.Size = new System.Drawing.Size(234, 24);
            this.rdoCheckPolygonsMustNotOverlap.TabIndex = 8;
            this.rdoCheckPolygonsMustNotOverlap.TabStop = true;
            this.rdoCheckPolygonsMustNotOverlap.Text = "Polygons Must Not Overlap";
            this.rdoCheckPolygonsMustNotOverlap.UseVisualStyleBackColor = true;
            this.rdoCheckPolygonsMustNotOverlap.CheckedChanged += new System.EventHandler(this.rdoCheckPolygonsMustNotOverlap_CheckedChanged);
            // 
            // rdoCheckIfPolygonsHaveGaps
            // 
            this.rdoCheckIfPolygonsHaveGaps.AutoSize = true;
            this.rdoCheckIfPolygonsHaveGaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonsHaveGaps.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonsHaveGaps.Location = new System.Drawing.Point(22, 223);
            this.rdoCheckIfPolygonsHaveGaps.Name = "rdoCheckIfPolygonsHaveGaps";
            this.rdoCheckIfPolygonsHaveGaps.Size = new System.Drawing.Size(327, 24);
            this.rdoCheckIfPolygonsHaveGaps.TabIndex = 7;
            this.rdoCheckIfPolygonsHaveGaps.TabStop = true;
            this.rdoCheckIfPolygonsHaveGaps.Text = "Union of Polygons Must Not Have Gaps";
            this.rdoCheckIfPolygonsHaveGaps.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonsHaveGaps.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonsHaveGaps_CheckedChanged);
            // 
            // rdoCheckIfPolygonsCoverEachOther
            // 
            this.rdoCheckIfPolygonsCoverEachOther.AutoSize = true;
            this.rdoCheckIfPolygonsCoverEachOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonsCoverEachOther.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonsCoverEachOther.Location = new System.Drawing.Point(22, 196);
            this.rdoCheckIfPolygonsCoverEachOther.Name = "rdoCheckIfPolygonsCoverEachOther";
            this.rdoCheckIfPolygonsCoverEachOther.Size = new System.Drawing.Size(293, 24);
            this.rdoCheckIfPolygonsCoverEachOther.TabIndex = 6;
            this.rdoCheckIfPolygonsCoverEachOther.TabStop = true;
            this.rdoCheckIfPolygonsCoverEachOther.Text = "Polygons Must Overlap Each Other";
            this.rdoCheckIfPolygonsCoverEachOther.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonsCoverEachOther.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonsCoverEachOther_CheckedChanged);
            // 
            // rdoCheckIfPolygonsContainPoints
            // 
            this.rdoCheckIfPolygonsContainPoints.AutoSize = true;
            this.rdoCheckIfPolygonsContainPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonsContainPoints.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonsContainPoints.Location = new System.Drawing.Point(22, 169);
            this.rdoCheckIfPolygonsContainPoints.Name = "rdoCheckIfPolygonsContainPoints";
            this.rdoCheckIfPolygonsContainPoints.Size = new System.Drawing.Size(254, 24);
            this.rdoCheckIfPolygonsContainPoints.TabIndex = 5;
            this.rdoCheckIfPolygonsContainPoints.TabStop = true;
            this.rdoCheckIfPolygonsContainPoints.Text = "Polygons Must Contain Points";
            this.rdoCheckIfPolygonsContainPoints.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonsContainPoints.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonsContainPoints_CheckedChanged);
            // 
            // rdoCheckIfPolygonsAreWithinPolygons
            // 
            this.rdoCheckIfPolygonsAreWithinPolygons.AutoSize = true;
            this.rdoCheckIfPolygonsAreWithinPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonsAreWithinPolygons.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonsAreWithinPolygons.Location = new System.Drawing.Point(22, 142);
            this.rdoCheckIfPolygonsAreWithinPolygons.Name = "rdoCheckIfPolygonsAreWithinPolygons";
            this.rdoCheckIfPolygonsAreWithinPolygons.Size = new System.Drawing.Size(291, 24);
            this.rdoCheckIfPolygonsAreWithinPolygons.TabIndex = 4;
            this.rdoCheckIfPolygonsAreWithinPolygons.TabStop = true;
            this.rdoCheckIfPolygonsAreWithinPolygons.Text = "Polygons Must Be Within Polygons";
            this.rdoCheckIfPolygonsAreWithinPolygons.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonsAreWithinPolygons.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonsAreWithinPolygons_CheckedChanged);
            // 
            // rdoCheckIfPolygonsOverlapPolygons
            // 
            this.rdoCheckIfPolygonsOverlapPolygons.AutoSize = true;
            this.rdoCheckIfPolygonsOverlapPolygons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonsOverlapPolygons.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonsOverlapPolygons.Location = new System.Drawing.Point(22, 115);
            this.rdoCheckIfPolygonsOverlapPolygons.Name = "rdoCheckIfPolygonsOverlapPolygons";
            this.rdoCheckIfPolygonsOverlapPolygons.Size = new System.Drawing.Size(276, 24);
            this.rdoCheckIfPolygonsOverlapPolygons.TabIndex = 3;
            this.rdoCheckIfPolygonsOverlapPolygons.TabStop = true;
            this.rdoCheckIfPolygonsOverlapPolygons.Text = "Polygons Must Overlap Polygons";
            this.rdoCheckIfPolygonsOverlapPolygons.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonsOverlapPolygons.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonsOverlapPolygons_CheckedChanged);
            // 
            // rdoCheckIfPolygonBoundariesOverlapLines
            // 
            this.rdoCheckIfPolygonBoundariesOverlapLines.AutoSize = true;
            this.rdoCheckIfPolygonBoundariesOverlapLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonBoundariesOverlapLines.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonBoundariesOverlapLines.Location = new System.Drawing.Point(22, 88);
            this.rdoCheckIfPolygonBoundariesOverlapLines.Name = "rdoCheckIfPolygonBoundariesOverlapLines";
            this.rdoCheckIfPolygonBoundariesOverlapLines.Size = new System.Drawing.Size(330, 24);
            this.rdoCheckIfPolygonBoundariesOverlapLines.TabIndex = 2;
            this.rdoCheckIfPolygonBoundariesOverlapLines.TabStop = true;
            this.rdoCheckIfPolygonBoundariesOverlapLines.Text = "Polygon Boundaries Must Overlap Lines";
            this.rdoCheckIfPolygonBoundariesOverlapLines.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonBoundariesOverlapLines.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonBoundariesOverlapLines_CheckedChanged);
            // 
            // rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries
            // 
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.AutoSize = true;
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.ForeColor = System.Drawing.Color.White;
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Location = new System.Drawing.Point(22, 61);
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Name = "rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries";
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Size = new System.Drawing.Size(438, 24);
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.TabIndex = 1;
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.TabStop = true;
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.Text = "Polygon Boundaries Must Overlap Polygon Boundaries";
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.UseVisualStyleBackColor = true;
            this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries.CheckedChanged += new System.EventHandler(this.rdoCheckIfPolygonBoundariesOverlapPolygonBoundaries_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Polygon Validation Tests";
            // 
            // ValidatePolygonTopology
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ValidatePolygonTopology";
            this.Size = new System.Drawing.Size(1329, 718);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}