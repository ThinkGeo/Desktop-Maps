using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Collections.ObjectModel;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LineValidationSample: UserControl
    {
        public LineValidationSample()
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

            rdoCheckLineEndpointsMustTouchPoints.Checked = true;
        }

        private Panel panel1;
        private RadioButton rdoCheckLinesMustNotSelfOverlap;
        private RadioButton rdoCheckLinesMustNotSelfIntersect;
        private RadioButton rdoCheckLinesMustNotOverlapLines;
        private RadioButton rdoCheckLinesMustNotOverlap;
        private RadioButton rdoCheckLinesMustNotSelfIntersectOrTouch;
        private RadioButton rdoCheckLinesMustNotIntersect;
        private RadioButton rdoCheckLinesMustNotHavePseudonodes;
        private RadioButton rdoCheckLinesMustFormClosedPolygon;
        private RadioButton rdoCheckLinesMustBeSinglePart;
        private RadioButton rdoCheckLinesMustOverlapLines;
        private RadioButton rdoCheckLinesMustOverlapPolygonBoundaries;
        private RadioButton rdoCheckLineEndpointsMustTouchPoints;
        private Label label1;
        private TextBox txtValidationInfo;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoCheckLineEndpointsMustTouchPoints = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustOverlapPolygonBoundaries = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustOverlapLines = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustBeSinglePart = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustFormClosedPolygon = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotHavePseudonodes = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotIntersect = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotSelfIntersectOrTouch = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotOverlap = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotOverlapLines = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotSelfIntersect = new System.Windows.Forms.RadioButton();
            this.rdoCheckLinesMustNotSelfOverlap = new System.Windows.Forms.RadioButton();
            this.txtValidationInfo = new System.Windows.Forms.TextBox();
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
            this.mapView.Size = new System.Drawing.Size(1222, 703);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtValidationInfo);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotSelfOverlap);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotSelfIntersect);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotOverlapLines);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotOverlap);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotSelfIntersectOrTouch);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotIntersect);
            this.panel1.Controls.Add(this.rdoCheckLinesMustNotHavePseudonodes);
            this.panel1.Controls.Add(this.rdoCheckLinesMustFormClosedPolygon);
            this.panel1.Controls.Add(this.rdoCheckLinesMustBeSinglePart);
            this.panel1.Controls.Add(this.rdoCheckLinesMustOverlapLines);
            this.panel1.Controls.Add(this.rdoCheckLinesMustOverlapPolygonBoundaries);
            this.panel1.Controls.Add(this.rdoCheckLineEndpointsMustTouchPoints);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(832, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 703);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line Validation Tests";
            // 
            // rdoCheckLineEndpointsMustTouchPoints
            // 
            this.rdoCheckLineEndpointsMustTouchPoints.AutoSize = true;
            this.rdoCheckLineEndpointsMustTouchPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLineEndpointsMustTouchPoints.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLineEndpointsMustTouchPoints.Location = new System.Drawing.Point(22, 63);
            this.rdoCheckLineEndpointsMustTouchPoints.Name = "rdoCheckLineEndpointsMustTouchPoints";
            this.rdoCheckLineEndpointsMustTouchPoints.Size = new System.Drawing.Size(286, 24);
            this.rdoCheckLineEndpointsMustTouchPoints.TabIndex = 1;
            this.rdoCheckLineEndpointsMustTouchPoints.TabStop = true;
            this.rdoCheckLineEndpointsMustTouchPoints.Text = "Line Endpoints Must Touch Points";
            this.rdoCheckLineEndpointsMustTouchPoints.UseVisualStyleBackColor = true;
            this.rdoCheckLineEndpointsMustTouchPoints.CheckedChanged += new System.EventHandler(this.rdoCheckLineEndpointsMustTouchPoints_CheckedChanged);
            // 
            // rdoCheckLinesMustOverlapPolygonBoundaries
            // 
            this.rdoCheckLinesMustOverlapPolygonBoundaries.AutoSize = true;
            this.rdoCheckLinesMustOverlapPolygonBoundaries.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustOverlapPolygonBoundaries.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustOverlapPolygonBoundaries.Location = new System.Drawing.Point(22, 90);
            this.rdoCheckLinesMustOverlapPolygonBoundaries.Name = "rdoCheckLinesMustOverlapPolygonBoundaries";
            this.rdoCheckLinesMustOverlapPolygonBoundaries.Size = new System.Drawing.Size(330, 24);
            this.rdoCheckLinesMustOverlapPolygonBoundaries.TabIndex = 2;
            this.rdoCheckLinesMustOverlapPolygonBoundaries.TabStop = true;
            this.rdoCheckLinesMustOverlapPolygonBoundaries.Text = "Lines Must Overlap Polygon Boundaries";
            this.rdoCheckLinesMustOverlapPolygonBoundaries.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustOverlapPolygonBoundaries.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustOverlapPolygonBoundaries_CheckedChanged);
            // 
            // rdoCheckLinesMustOverlapLines
            // 
            this.rdoCheckLinesMustOverlapLines.AutoSize = true;
            this.rdoCheckLinesMustOverlapLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustOverlapLines.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustOverlapLines.Location = new System.Drawing.Point(22, 117);
            this.rdoCheckLinesMustOverlapLines.Name = "rdoCheckLinesMustOverlapLines";
            this.rdoCheckLinesMustOverlapLines.Size = new System.Drawing.Size(222, 24);
            this.rdoCheckLinesMustOverlapLines.TabIndex = 3;
            this.rdoCheckLinesMustOverlapLines.TabStop = true;
            this.rdoCheckLinesMustOverlapLines.Text = "Lines Must Overlap Lines";
            this.rdoCheckLinesMustOverlapLines.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustOverlapLines.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustOverlapLines_CheckedChanged);
            // 
            // rdoCheckLinesMustBeSinglePart
            // 
            this.rdoCheckLinesMustBeSinglePart.AutoSize = true;
            this.rdoCheckLinesMustBeSinglePart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustBeSinglePart.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustBeSinglePart.Location = new System.Drawing.Point(22, 144);
            this.rdoCheckLinesMustBeSinglePart.Name = "rdoCheckLinesMustBeSinglePart";
            this.rdoCheckLinesMustBeSinglePart.Size = new System.Drawing.Size(226, 24);
            this.rdoCheckLinesMustBeSinglePart.TabIndex = 4;
            this.rdoCheckLinesMustBeSinglePart.TabStop = true;
            this.rdoCheckLinesMustBeSinglePart.Text = "Lines Must Be Single Part";
            this.rdoCheckLinesMustBeSinglePart.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustBeSinglePart.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustBeSinglePart_CheckedChanged);
            // 
            // rdoCheckLinesMustFormClosedPolygon
            // 
            this.rdoCheckLinesMustFormClosedPolygon.AutoSize = true;
            this.rdoCheckLinesMustFormClosedPolygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustFormClosedPolygon.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustFormClosedPolygon.Location = new System.Drawing.Point(22, 171);
            this.rdoCheckLinesMustFormClosedPolygon.Name = "rdoCheckLinesMustFormClosedPolygon";
            this.rdoCheckLinesMustFormClosedPolygon.Size = new System.Drawing.Size(287, 24);
            this.rdoCheckLinesMustFormClosedPolygon.TabIndex = 5;
            this.rdoCheckLinesMustFormClosedPolygon.TabStop = true;
            this.rdoCheckLinesMustFormClosedPolygon.Text = "Lines Must Form Closed Polygons";
            this.rdoCheckLinesMustFormClosedPolygon.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustFormClosedPolygon.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustFormClosedPolygon_CheckedChanged);
            // 
            // rdoCheckLinesMustNotHavePseudonodes
            // 
            this.rdoCheckLinesMustNotHavePseudonodes.AutoSize = true;
            this.rdoCheckLinesMustNotHavePseudonodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotHavePseudonodes.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotHavePseudonodes.Location = new System.Drawing.Point(22, 198);
            this.rdoCheckLinesMustNotHavePseudonodes.Name = "rdoCheckLinesMustNotHavePseudonodes";
            this.rdoCheckLinesMustNotHavePseudonodes.Size = new System.Drawing.Size(294, 24);
            this.rdoCheckLinesMustNotHavePseudonodes.TabIndex = 6;
            this.rdoCheckLinesMustNotHavePseudonodes.TabStop = true;
            this.rdoCheckLinesMustNotHavePseudonodes.Text = "Lines Must Not Have Pseudonodes";
            this.rdoCheckLinesMustNotHavePseudonodes.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotHavePseudonodes.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotHavePseudonodes_CheckedChanged);
            // 
            // rdoCheckLinesMustNotIntersect
            // 
            this.rdoCheckLinesMustNotIntersect.AutoSize = true;
            this.rdoCheckLinesMustNotIntersect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotIntersect.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotIntersect.Location = new System.Drawing.Point(22, 225);
            this.rdoCheckLinesMustNotIntersect.Name = "rdoCheckLinesMustNotIntersect";
            this.rdoCheckLinesMustNotIntersect.Size = new System.Drawing.Size(214, 24);
            this.rdoCheckLinesMustNotIntersect.TabIndex = 7;
            this.rdoCheckLinesMustNotIntersect.TabStop = true;
            this.rdoCheckLinesMustNotIntersect.Text = "Lines Must Not Intersect";
            this.rdoCheckLinesMustNotIntersect.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotIntersect.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotIntersect_CheckedChanged);
            // 
            // rdoCheckLinesMustNotSelfIntersectOrTouch
            // 
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.AutoSize = true;
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.Location = new System.Drawing.Point(22, 252);
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.Name = "rdoCheckLinesMustNotSelfIntersectOrTouch";
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.Size = new System.Drawing.Size(324, 24);
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.TabIndex = 8;
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.TabStop = true;
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.Text = "Lines Must Not Self-Intersect Or Touch";
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotSelfIntersectOrTouch.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotSelfIntersectOrTouch_CheckedChanged);
            // 
            // rdoCheckLinesMustNotOverlap
            // 
            this.rdoCheckLinesMustNotOverlap.AutoSize = true;
            this.rdoCheckLinesMustNotOverlap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotOverlap.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotOverlap.Location = new System.Drawing.Point(22, 276);
            this.rdoCheckLinesMustNotOverlap.Name = "rdoCheckLinesMustNotOverlap";
            this.rdoCheckLinesMustNotOverlap.Size = new System.Drawing.Size(207, 24);
            this.rdoCheckLinesMustNotOverlap.TabIndex = 9;
            this.rdoCheckLinesMustNotOverlap.TabStop = true;
            this.rdoCheckLinesMustNotOverlap.Text = "Lines Must Not Overlap";
            this.rdoCheckLinesMustNotOverlap.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotOverlap.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotOverlap_CheckedChanged);
            // 
            // rdoCheckLinesMustNotOverlapLines
            // 
            this.rdoCheckLinesMustNotOverlapLines.AutoSize = true;
            this.rdoCheckLinesMustNotOverlapLines.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotOverlapLines.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotOverlapLines.Location = new System.Drawing.Point(22, 306);
            this.rdoCheckLinesMustNotOverlapLines.Name = "rdoCheckLinesMustNotOverlapLines";
            this.rdoCheckLinesMustNotOverlapLines.Size = new System.Drawing.Size(292, 24);
            this.rdoCheckLinesMustNotOverlapLines.TabIndex = 10;
            this.rdoCheckLinesMustNotOverlapLines.TabStop = true;
            this.rdoCheckLinesMustNotOverlapLines.Text = "Lines Must Not Overlap With Lines";
            this.rdoCheckLinesMustNotOverlapLines.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotOverlapLines.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotOverlapLines_CheckedChanged);
            // 
            // rdoCheckLinesMustNotSelfIntersect
            // 
            this.rdoCheckLinesMustNotSelfIntersect.AutoSize = true;
            this.rdoCheckLinesMustNotSelfIntersect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotSelfIntersect.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotSelfIntersect.Location = new System.Drawing.Point(22, 333);
            this.rdoCheckLinesMustNotSelfIntersect.Name = "rdoCheckLinesMustNotSelfIntersect";
            this.rdoCheckLinesMustNotSelfIntersect.Size = new System.Drawing.Size(249, 24);
            this.rdoCheckLinesMustNotSelfIntersect.TabIndex = 11;
            this.rdoCheckLinesMustNotSelfIntersect.TabStop = true;
            this.rdoCheckLinesMustNotSelfIntersect.Text = "Lines Must Not Self-Intersect";
            this.rdoCheckLinesMustNotSelfIntersect.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotSelfIntersect.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotSelfIntersect_CheckedChanged);
            // 
            // rdoCheckLinesMustNotSelfOverlap
            // 
            this.rdoCheckLinesMustNotSelfOverlap.AutoSize = true;
            this.rdoCheckLinesMustNotSelfOverlap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCheckLinesMustNotSelfOverlap.ForeColor = System.Drawing.Color.White;
            this.rdoCheckLinesMustNotSelfOverlap.Location = new System.Drawing.Point(22, 360);
            this.rdoCheckLinesMustNotSelfOverlap.Name = "rdoCheckLinesMustNotSelfOverlap";
            this.rdoCheckLinesMustNotSelfOverlap.Size = new System.Drawing.Size(242, 24);
            this.rdoCheckLinesMustNotSelfOverlap.TabIndex = 12;
            this.rdoCheckLinesMustNotSelfOverlap.TabStop = true;
            this.rdoCheckLinesMustNotSelfOverlap.Text = "Lines Must Not Self-Overlap";
            this.rdoCheckLinesMustNotSelfOverlap.UseVisualStyleBackColor = true;
            this.rdoCheckLinesMustNotSelfOverlap.CheckedChanged += new System.EventHandler(this.rdoCheckLinesMustNotSelfOverlap_CheckedChanged);
            // 
            // txtValidationInfo
            // 
            this.txtValidationInfo.Location = new System.Drawing.Point(24, 391);
            this.txtValidationInfo.Multiline = true;
            this.txtValidationInfo.Name = "txtValidationInfo";
            this.txtValidationInfo.Size = new System.Drawing.Size(328, 287);
            this.txtValidationInfo.TabIndex = 13;
            // 
            // LineValidationSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "LineValidationSample";
            this.Size = new System.Drawing.Size(1222, 703);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void rdoCheckLineEndpointsMustTouchPoints_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of point and line features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0,100 50)");
            Feature pointOnEndpointFeature = new Feature("POINT(0 0)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature };
            Collection<Feature> points = new Collection<Feature>() { pointOnEndpointFeature };
            TopologyValidationResult result = TopologyValidator.LineEndPointsMustTouchPoints(lines, points);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { pointOnEndpointFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine endpoints touching points are shown in green. \n\nInvalid endpoints are shown in red.";
        }

        private void rdoCheckLinesMustOverlapPolygonBoundaries_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line and polygon features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(-50 0,150 0)");
            Feature lineOnBoundaryFeature = new Feature("LINESTRING(-50 0,150 0)");
            Feature polygonFeature = new Feature("POLYGON((0 0,100 0,100 100,0 100,0 0))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature, lineOnBoundaryFeature };
            Collection<Feature> polygons = new Collection<Feature>() { polygonFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustOverlapPolygonBoundaries(lines, polygons);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature, lineOnBoundaryFeature }, invalidResultFeatures, new Collection<Feature>() { polygonFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping polygon boundaries are shown in green. \n\nInvalid line segments are shown in red.";

        }

        private void rdoCheckLinesMustOverlapLines_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            Feature coveringLineFeature = new Feature("LINESTRING(0 -50,50 0,100 0,100 150)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringLines = new Collection<Feature>() { coveringLineFeature };
            Collection<Feature> coveredLines = new Collection<Feature>() { lineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustBeCoveredByLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature }, invalidResultFeatures, new Collection<Feature>() { coveringLineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLine segments overlapping lines are shown in green. \n\nInvalid line segments are shown in red.";

        }

        private void rdoCheckLinesMustBeSinglePart_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature singleLineFeature = new Feature("MULTILINESTRING((0 -50,100 -50,100 -100,0 -100))");
            Feature multiLineFeature = new Feature("MULTILINESTRING((0 0,100 0),(100 100,0 100))");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { singleLineFeature, multiLineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustBeSinglePart(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { singleLineFeature, multiLineFeature }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines made of single segments are shown in green. \n\nLines with disjoint segments are shown in red.";

        }

        private void rdoCheckLinesMustFormClosedPolygon_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100,20 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 0,-50 0,-50 100,0 100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2 };
            TopologyValidationResult result = TopologyValidator.LinesMustFormClosedPolygon(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(lines, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nLine endpoints that do not form a closed polygon are shown in red.";

        }

        private void rdoCheckLinesMustNotHavePseudonodes_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineSegmentFeature1 = new Feature("LINESTRING(0 0,50 0,50 50,0 0)");
            Feature lineSegmentFeature2 = new Feature("LINESTRING(-50 0,-50 50)");
            Feature lineSegmentFeature3 = new Feature("LINESTRING(-100 0,-50 50)");
            Feature lineSegmentFeature4 = new Feature("LINESTRING(-50 -50,-50 -100)");
            Feature lineSegmentFeature5 = new Feature("LINESTRING(-100 -50,-50 -100)");
            Feature lineSegmentFeature6 = new Feature("LINESTRING(-50 -100,0 -100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineSegmentFeature1, lineSegmentFeature2, lineSegmentFeature3, lineSegmentFeature4, lineSegmentFeature5, lineSegmentFeature6 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotHavePseudonodes(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(lines, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nPseudonodes are shown in red.";

        }

        private void rdoCheckLinesMustNotIntersect_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotIntersect(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersections are shown in red.";

        }

        private void rdoCheckLinesMustNotSelfIntersectOrTouch_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfIntersectOrTouch(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nIntersecting points and overlapping segments are shown in red.";

        }

        private void rdoCheckLinesMustNotOverlap_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature lineFeature1 = new Feature("LINESTRING(0 0,100 0,100 100)");
            Feature lineFeature2 = new Feature("LINESTRING(0 -50,30 0,60 0,100 50)");
            Feature lineFeature3 = new Feature("LINESTRING(20 50,20 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 };
            TopologyValidationResult result = TopologyValidator.LinesMustNotOverlap(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { lineFeature1, lineFeature2, lineFeature3 }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";

        }

        private void rdoCheckLinesMustNotOverlapLines_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature overlappingLineFeature = new Feature("LINESTRING(0 0,100 0,100 100,0 100)");
            Feature overlappedLineFeature = new Feature("LINESTRING(150 0,100 30,100 60,150 100)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> coveringLines = new Collection<Feature>() { overlappingLineFeature };
            Collection<Feature> coveredLines = new Collection<Feature>() { overlappedLineFeature };
            TopologyValidationResult result = TopologyValidator.LinesMustNotOverlapLines(coveringLines, coveredLines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { overlappedLineFeature }, invalidResultFeatures, new Collection<Feature>() { overlappingLineFeature });

            // Update the help text
            txtValidationInfo.Text = "Features being validated against are shown in blue. \n\nLines being validated are shown in green. \n\nOverlapping line segments are shown in red.";

        }

        private void rdoCheckLinesMustNotSelfIntersect_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature selfIntersectingLine = new Feature("LINESTRING(0 0,100 0,100 100,50 100,50 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { selfIntersectingLine };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfIntersect(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { selfIntersectingLine }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nSelf-intersections are shown in red.";

        }

        private void rdoCheckLinesMustNotSelfOverlap_CheckedChanged(object sender, EventArgs e)
        {
            // Create a sample set of line features to use for the validation
            Feature selfOverlappingLine = new Feature("LINESTRING(0 0,100 0,100 100,0 100,20 0,40 0,40 -50)");

            // Use the TopologyValidator API to validate the sample data
            Collection<Feature> lines = new Collection<Feature>() { selfOverlappingLine };
            TopologyValidationResult result = TopologyValidator.LinesMustNotSelfOverlap(lines);

            // Get the invalid features returned from the API
            Collection<Feature> invalidResultFeatures = result.InvalidFeatures;

            // Clear the MapView and add the new valid/invalid features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { selfOverlappingLine }, invalidResultFeatures);

            // Update the help text
            txtValidationInfo.Text = "Lines being validated are shown in green. \n\nOverlapping segments are shown in red.";

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