using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawEditDeleteShapesUsingInteractiveOverlaySample : UserControl
    {
        public DrawEditDeleteShapesUsingInteractiveOverlaySample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            LayerOverlay layerOverlay;
            InMemoryFeatureLayer featureLayer;

            // Create the layer that will store the drawn shapes
            featureLayer = new InMemoryFeatureLayer();

            // Add styles for the layer
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to a LayerOverlay
            layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add("featureLayer", featureLayer);

            // Add the LayerOverlay to the map
            mapView.Overlays.Add("layerOverlay", layerOverlay);

            // Update instructions
            instructions.Text = "Navigation Mode - The default map state. Allows you to pan and zoom the map with mouse controls.";

            await mapView.RefreshAsync();
        }

        private async Task UpdateLayerFeaturesAsync(InMemoryFeatureLayer featureLayer, LayerOverlay layerOverlay)
        {
            // If the user switched away from a Drawing Mode, add all the newly drawn shapes in the TrackOverlay into the the featureLayer
            foreach (Feature feature in mapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the TrackOverlay's features
            mapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // If the user switched away from Edit Mode, add all the shapes that were in the EditOverlay back into the the featureLayer
            foreach (Feature feature in mapView.EditOverlay.EditShapesLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the EditOverlay's features
            mapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            await mapView.RefreshAsync(new Overlay[] { mapView.TrackOverlay, mapView.EditOverlay, layerOverlay });

            // In case the user was in Delete Mode, remove the event handler to avoid deleting features unintentionally
            mapView.MapClick -= mapView_MapClick;
        }

        private async void navMode_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to naviage the map normally
            mapView.TrackOverlay.TrackMode = TrackMode.None;

            // Update instructions
            instructions.Text = "Navigation Mode - The default map state. Allows you to pan and zoom the map with mouse controls.";

        }

        private async void drawPoint_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Point, which draws a new point on the map on mouse click
            mapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Update instructions
            instructions.Text = "Draw Point Mode - Creates a Point Shape where at the location of each left mouse click event on the map.";

        }

        private async void drawLine_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            mapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Update instructions
            instructions.Text = "Draw Line Mode - Begin creating a Line Shape by left clicking on the map. Each subsequent left click adds another vertex to the line. Double left click to finish creating the Shape. Middle mouse click and drag allows the user to pan the map while drawing the Shape.";
        }

        private async void drawPolygon_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            mapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Update instructions
            instructions.Text = "Draw Polygon Mode - Begin creating a Polygon Shape by left clicking on the map. Each subsequent left click adds another vertex to the polygon. Double left click to finish creating the Shape. Middle mouse click and drag allows the user to pan the map while drawing the Shape.";

        }

        private async void editShape_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes
            mapView.TrackOverlay.TrackMode = TrackMode.None;

            // Put all features in the featureLayer into the EditOverlay
            foreach (Feature feature in featureLayer.InternalFeatures)
            {
                mapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear all the features in the featureLayer so that the editing features don't overlap with the original shapes
            // In UpdateLayerFeatures, we will add them all back to the featureLayer once the user switches modes
            featureLayer.InternalFeatures.Clear();

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            mapView.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            await mapView.RefreshAsync(new Overlay[] { mapView.EditOverlay, layerOverlay });

            // Update instructions
            instructions.Text = "Edit Shapes Mode - Allows the user to modify Shapes. Translate, rotate, or scale a shape using the anchor controls around the shape. Line and Polygon Shapes can also be modified: move a vertex by left mouse click and dragging on an existing vertex, add a vertex by left mouse clicking on a line segment, and remove a vertex by double left mouse clicking on an existing vertex.";

        }

        private async void deleteShape_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes
            mapView.TrackOverlay.TrackMode = TrackMode.None;

            // Add the event handler that will delete features on map click
            mapView.MapClick += mapView_MapClick;

            // Update instructions
            instructions.Text = "Delete Shape Mode - Deletes a shape by left mouse clicking on the shape.";

        }

        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Query the layer for the closest feature within 100 meters
            Collection<Feature> closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

            // If a feature was found, remove it from the layer
            if (closestFeatures.Count > 0)
            {
                featureLayer.InternalFeatures.Remove(closestFeatures[0]);

                // Refresh the layerOverlay to show the results
                await mapView.RefreshAsync(layerOverlay);
            }

        }

        #region Component Designer generated code
        private Panel panel1;
        private TextBox instructions;
        private Label label2;
        private RadioButton editShape;
        private RadioButton deleteShape;
        private RadioButton drawPolygon;
        private RadioButton drawLine;
        private RadioButton drawPoint;
        private RadioButton navMode;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.instructions = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editShape = new System.Windows.Forms.RadioButton();
            this.deleteShape = new System.Windows.Forms.RadioButton();
            this.drawPolygon = new System.Windows.Forms.RadioButton();
            this.drawLine = new System.Windows.Forms.RadioButton();
            this.drawPoint = new System.Windows.Forms.RadioButton();
            this.navMode = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(794, 626);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.instructions);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.editShape);
            this.panel1.Controls.Add(this.deleteShape);
            this.panel1.Controls.Add(this.drawPolygon);
            this.panel1.Controls.Add(this.drawLine);
            this.panel1.Controls.Add(this.drawPoint);
            this.panel1.Controls.Add(this.navMode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(797, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 626);
            this.panel1.TabIndex = 1;
            // 
            // instructions
            // 
            this.instructions.BackColor = System.Drawing.Color.Gray;
            this.instructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.instructions.ForeColor = System.Drawing.Color.White;
            this.instructions.Location = new System.Drawing.Point(25, 249);
            this.instructions.Multiline = true;
            this.instructions.Name = "instructions";
            this.instructions.Size = new System.Drawing.Size(299, 353);
            this.instructions.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Instructions:";
            // 
            // editShape
            // 
            this.editShape.AutoSize = true;
            this.editShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.editShape.ForeColor = System.Drawing.Color.White;
            this.editShape.Location = new System.Drawing.Point(25, 158);
            this.editShape.Name = "editShape";
            this.editShape.Size = new System.Drawing.Size(95, 21);
            this.editShape.TabIndex = 6;
            this.editShape.Text = "Edit Shape";
            this.editShape.UseVisualStyleBackColor = true;
            this.editShape.Click += new EventHandler(this.editShape_Click);
            // 
            // deleteShape
            // 
            this.deleteShape.AutoSize = true;
            this.deleteShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.deleteShape.ForeColor = System.Drawing.Color.White;
            this.deleteShape.Location = new System.Drawing.Point(25, 185);
            this.deleteShape.Name = "deleteShape";
            this.deleteShape.Size = new System.Drawing.Size(112, 21);
            this.deleteShape.TabIndex = 5;
            this.deleteShape.Text = "Delete Shape";
            this.deleteShape.UseVisualStyleBackColor = true;
            this.deleteShape.CheckedChanged += new System.EventHandler(this.deleteShape_Click);
            // 
            // drawPolygon
            // 
            this.drawPolygon.AutoSize = true;
            this.drawPolygon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.drawPolygon.ForeColor = System.Drawing.Color.White;
            this.drawPolygon.Location = new System.Drawing.Point(25, 131);
            this.drawPolygon.Name = "drawPolygon";
            this.drawPolygon.Size = new System.Drawing.Size(113, 21);
            this.drawPolygon.TabIndex = 4;
            this.drawPolygon.Text = "Draw Polygon";
            this.drawPolygon.UseVisualStyleBackColor = true;
            this.drawPolygon.CheckedChanged += new System.EventHandler(this.drawPolygon_Click);
            // 
            // drawLine
            // 
            this.drawLine.AutoSize = true;
            this.drawLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.drawLine.ForeColor = System.Drawing.Color.White;
            this.drawLine.Location = new System.Drawing.Point(25, 104);
            this.drawLine.Name = "drawLine";
            this.drawLine.Size = new System.Drawing.Size(89, 21);
            this.drawLine.TabIndex = 3;
            this.drawLine.Text = "Draw Line";
            this.drawLine.UseVisualStyleBackColor = true;
            this.drawLine.CheckedChanged += new System.EventHandler(this.drawLine_Click);
            // 
            // drawPoint
            // 
            this.drawPoint.AutoSize = true;
            this.drawPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.drawPoint.ForeColor = System.Drawing.Color.White;
            this.drawPoint.Location = new System.Drawing.Point(25, 77);
            this.drawPoint.Name = "drawPoint";
            this.drawPoint.Size = new System.Drawing.Size(94, 21);
            this.drawPoint.TabIndex = 2;
            this.drawPoint.Text = "Draw Point";
            this.drawPoint.UseVisualStyleBackColor = true;
            this.drawPoint.CheckedChanged += new System.EventHandler(this.drawPoint_Click);
            // 
            // navMode
            // 
            this.navMode.AutoSize = true;
            this.navMode.Checked = true;
            this.navMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.navMode.ForeColor = System.Drawing.Color.White;
            this.navMode.Location = new System.Drawing.Point(25, 50);
            this.navMode.Name = "navMode";
            this.navMode.Size = new System.Drawing.Size(121, 21);
            this.navMode.TabIndex = 1;
            this.navMode.TabStop = true;
            this.navMode.Text = "Navigate Mode";
            this.navMode.UseVisualStyleBackColor = true;
            this.navMode.CheckedChanged += new System.EventHandler(this.navMode_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Interactive";
            // 
            // DrawEditDeleteShapesUsingInteractiveOverlaySample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DrawEditDeleteShapesUsingInteractiveOverlaySample";
            this.Size = new System.Drawing.Size(1139, 626);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}