using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.RoutingSamples
{
    public class EditRoutingIndexFile : UserControl
    {
        private RoutingEngine routingEngine;
        private RoutingSource routingSource;
        private ShapeFileFeatureSource featureSource;
        private RoutingLayer routingLayer;
        private RoutingResult routingResult;
        private static string rootPath = Samples.rootDirectory;

        private bool isInEditMode;
        private RouteSegment editRouteSegment;
        private Feature editFeature;

        public EditRoutingIndexFile()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            routingEngine = new RoutingEngine(routingSource, featureSource);

            featureSource.Open();
            routingSource.Open();

            isInEditMode = false;

            RenderMap();
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            routingEngine.RoutingSource = routingSource;

            routingLayer.Routes.Clear();
            routingEngine.RoutingSource.Open();
            routingResult = routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.EndPoint);
            routingLayer.Routes.Add(routingResult.Route);

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        private void btnbeginEdit_Click(object sender, EventArgs e)
        {
            isInEditMode = true;
        }

        private void btnStopEdit_Click(object sender, EventArgs e)
        {
            isInEditMode = false;
            if (routingSource.IsInTransaction)
            {
                routingSource.RollbackTransaction();
            }
            ClearRoadOverlay();
        }

        private void ClearRoadOverlay()
        {
            LayerOverlay roadOverlay = (LayerOverlay)winformsMap1.Overlays["RoadsOverlay"];
            InMemoryFeatureLayer currentRoadLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentRoadLayer"];
            currentRoadLayer.InternalFeatures.Clear();
            InMemoryFeatureLayer adjacentRoadsLayer = (InMemoryFeatureLayer)roadOverlay.Layers["adjacentRoadsLayer"];
            adjacentRoadsLayer.InternalFeatures.Clear();
            InMemoryFeatureLayer currentEditLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentEditLayer"];
            currentEditLayer.InternalFeatures.Clear();
            winformsMap1.Refresh(roadOverlay);
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            // Select the nearest feature from route features
            LayerOverlay roadOverlay = (LayerOverlay)winformsMap1.Overlays["RoadsOverlay"];
            if (isInEditMode)
            {
                featureSource.Open();
                Collection<Feature> closestFeatures = featureSource.GetFeaturesNearestTo(e.WorldLocation, winformsMap1.MapUnit, 1, ReturningColumnsType.NoColumns);
                if (closestFeatures.Count > 0)
                {
                    editFeature = closestFeatures[0];
                    routingSource.Open();
                    editRouteSegment = routingSource.GetRouteSegmentByFeatureId(editFeature.Id);

                    RenderRouteSegmentInformation(roadOverlay);
                }
            }
            else if (routingSource.IsInTransaction)
            {
                EditRoutingData(e.WorldLocation, roadOverlay);
            }
        }

        private void RenderRouteSegmentInformation(LayerOverlay roadOverlay)
        {
            // Render Road information
            InMemoryFeatureLayer currentRoadLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentRoadLayer"];
            currentRoadLayer.InternalFeatures.Clear();
            currentRoadLayer.InternalFeatures.Add(editFeature.Id, editFeature);
            // render adjacent road
            InMemoryFeatureLayer adjacentRoadsLayer = roadOverlay.Layers["adjacentRoadsLayer"] as InMemoryFeatureLayer;
            // Get the feature id stored in FeatureSource
            Collection<string> adjacentIds = new Collection<string>();
            foreach (string id in editRouteSegment.StartPointAdjacentIds)
            {
                if (!adjacentIds.Contains(id))
                {
                    adjacentIds.Add(id);
                }
            }
            foreach (string id in editRouteSegment.EndPointAdjacentIds)
            {
                if (!adjacentIds.Contains(id))
                {
                    adjacentIds.Add(id);
                }
            }
            // Render features
            Collection<Feature> features = featureSource.GetFeaturesByIds(adjacentIds, ReturningColumnsType.AllColumns);
            adjacentRoadsLayer.InternalFeatures.Clear();
            foreach (Feature feature in features)
            {
                adjacentRoadsLayer.InternalFeatures.Add(feature.Id, feature);
            }

            winformsMap1.Refresh(roadOverlay);
        }

        private void EditRoutingData(PointShape clickedPosition, LayerOverlay roadOverlay)
        {
            InMemoryFeatureLayer currentRoadLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentRoadLayer"];
            InMemoryFeatureLayer adjacentRoadsLayer = (InMemoryFeatureLayer)roadOverlay.Layers["adjacentRoadsLayer"];
            if (currentRoadLayer.InternalFeatures.Count <= 0)
            {
                return;
            }

            // Select the editing feature
            routingSource.Open();
            Collection<RouteSegment> routeSegments = routingSource.GetRouteSegmentsNearestTo(clickedPosition, featureSource, winformsMap1.MapUnit, 1);

            if (routeSegments.Count > 0)
            {
                string featureId = routeSegments[0].FeatureId;
                featureSource.Open();
                Feature feature = featureSource.GetFeatureById(featureId, ReturningColumnsType.NoColumns);
                // Highlight the selected feature
                InMemoryFeatureLayer currentEditLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentEditLayer"];
                currentEditLayer.InternalFeatures.Clear();
                currentEditLayer.InternalFeatures.Add(feature);
                winformsMap1.Refresh(roadOverlay);

                UpdateRouteSegmentInformation(currentRoadLayer, adjacentRoadsLayer, routeSegments, feature);

                currentEditLayer.InternalFeatures.Remove(feature);
                winformsMap1.Refresh(roadOverlay);
            }
        }

        private void UpdateRouteSegmentInformation(InMemoryFeatureLayer currentRoadLayer, InMemoryFeatureLayer adjacentRoadsLayer, Collection<RouteSegment> routeSegments, Feature feature)
        {
            bool isUpdated = false;
            if (feature.Id == currentRoadLayer.InternalFeatures[0].Id)
            {
                // update road segment's roadtype, weight information
                if (MessageBox.Show("Do you want to update the information of selected segment marked with blue?", "Edit Selected Segment", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    plRoadInformation.Visible = true;
                    winformsMap1.Enabled = false;

                    txtSegmentType.Text = editRouteSegment.RouteSegmentType.ToString();
                    txtWeight.Text = editRouteSegment.Weight.ToString();
                }
            }
            else if (adjacentRoadsLayer.InternalFeatures.Contains(feature.Id))
            {
                // remove the adjacent segments
                if (MessageBox.Show("Do you want to remove the selected segment marked with blue from current adjacent collection?", "Remove Selected Segment", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (editRouteSegment.StartPointAdjacentIds.Contains(routeSegments[0].FeatureId))
                    {
                        editRouteSegment.StartPointAdjacentIds.Remove(routeSegments[0].FeatureId);
                        isUpdated = true;
                    }
                    if (editRouteSegment.EndPointAdjacentIds.Contains(routeSegments[0].FeatureId))
                    {
                        editRouteSegment.EndPointAdjacentIds.Remove(routeSegments[0].FeatureId);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        adjacentRoadsLayer.InternalFeatures.Remove(feature.Id);
                    }
                }
            }
            else
            {
                // Add the new adjacent segments
                if (MessageBox.Show("Do you want to add the selected segment marked with blue  into current adjacent collection?", "Add Segment Into Adjacent Collection", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    DialogResult result = MessageBox.Show("Which collection do you want to add it into? Click 'Yes' to add it into segment's start adjacent collection or 'No' to add it into segment's end adjacent collection.", "Choose Segment's adjacent collection", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        editRouteSegment.StartPointAdjacentIds.Add(routeSegments[0].FeatureId);
                        isUpdated = true;
                    }
                    else if (result == DialogResult.No)
                    {
                        editRouteSegment.EndPointAdjacentIds.Add(routeSegments[0].FeatureId);
                        isUpdated = true;
                    }
                    if (isUpdated)
                    {
                        adjacentRoadsLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }
            }

            if (isUpdated)
            {
                routingSource.UpdateRouteSegment(editRouteSegment);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!routingSource.IsInTransaction)
            {
                routingSource.BeginTransaction();
            }
            isInEditMode = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (routingSource.IsInTransaction)
            {
                isInEditMode = true;
                routingSource.Open();
                routingSource.CommitTransaction();
                MessageBox.Show("The route segment has updated successfully!", "Save Successfully");
            }

            LayerOverlay roadOverlay = (LayerOverlay)winformsMap1.Overlays["RoadsOverlay"];
            InMemoryFeatureLayer currentEditLayer = (InMemoryFeatureLayer)roadOverlay.Layers["currentEditLayer"];
            currentEditLayer.InternalFeatures.Clear();
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            ClearRoadOverlay();

            // Recover the route segment information
            if (routingSource.IsInTransaction)
            {
                routingSource.Open();
                routingSource.RollbackTransaction();
                editRouteSegment = routingSource.GetRouteSegmentByFeatureId(editFeature.Id);
                isInEditMode = true;
            }
        }

        private void btnConfirmRoadInfor_Click(object sender, EventArgs e)
        {
            editRouteSegment.RouteSegmentType = int.Parse(txtSegmentType.Text.Trim());
            editRouteSegment.Weight = float.Parse(txtWeight.Text.Trim());
            routingSource.UpdateRouteSegment(editRouteSegment);

            plRoadInformation.Visible = false;
            winformsMap1.Enabled = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            plRoadInformation.Visible = false;
            winformsMap1.Enabled = true;
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            routingLayer = new RoutingLayer();
            string[] startCoordinates = txtStartPoint.Text.Split(',');
            routingLayer.StartPoint = new PointShape(double.Parse(startCoordinates[0], CultureInfo.InvariantCulture), double.Parse(startCoordinates[1], CultureInfo.InvariantCulture));
            string[] endCoordinates = txtEndPoint.Text.Split(',');
            routingLayer.EndPoint = new PointShape(double.Parse(endCoordinates[0], CultureInfo.InvariantCulture), double.Parse(endCoordinates[1], CultureInfo.InvariantCulture));
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer adjacentRoadsLayer = new InMemoryFeatureLayer();
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.FromArgb(160, GeoColor.SimpleColors.Yellow), 6));
            adjacentRoadsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay roadsOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("RoadsOverlay", roadsOverlay);
            roadsOverlay.Layers.Add("adjacentRoadsLayer", adjacentRoadsLayer);

            InMemoryFeatureLayer currentRoadLayer = new InMemoryFeatureLayer();
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.FromArgb(160, GeoColor.SimpleColors.Red), 6));
            currentRoadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            roadsOverlay.Layers.Add("currentRoadLayer", currentRoadLayer);

            InMemoryFeatureLayer currentEditLayer = new InMemoryFeatureLayer();
            currentEditLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.SimpleColors.Blue, 6));
            currentEditLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            roadsOverlay.Layers.Add("currentEditLayer", currentEditLayer);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndPoint;
        private TextBox txtStartPoint;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private Label label4;
        private Button btnbeginEdit;
        private Label label5;
        private Button btnFindPath;
        private Button btnStopEdit;
        private Button btnRollback;
        private Button btnSave;
        private Button btnEdit;
        private Panel plRoadInformation;
        private GroupBox groupBox2;
        private Button BtnCancel;
        private Button btnConfirmRoadInfor;
        private Label label7;
        private TextBox txtWeight;
        private TextBox txtSegmentType;
        private Label label6;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRoutingIndexFile));
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnStopEdit = new System.Windows.Forms.Button();
            this.btnRollback = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.btnbeginEdit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.plRoadInformation = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.btnConfirmRoadInfor = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.txtSegmentType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.plRoadInformation.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(204, 234);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(0, 13);
            this.lblCoordinate.TabIndex = 6;
            this.lblCoordinate.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnStopEdit);
            this.groupBox1.Controls.Add(this.btnRollback);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnFindPath);
            this.groupBox1.Controls.Add(this.btnbeginEdit);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartPoint);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndPoint);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 367);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // btnStopEdit
            // 
            this.btnStopEdit.Location = new System.Drawing.Point(110, 195);
            this.btnStopEdit.Name = "btnStopEdit";
            this.btnStopEdit.Size = new System.Drawing.Size(104, 23);
            this.btnStopEdit.TabIndex = 21;
            this.btnStopEdit.Text = "Stop Editing";
            this.btnStopEdit.UseVisualStyleBackColor = true;
            this.btnStopEdit.Click += new System.EventHandler(this.btnStopEdit_Click);
            // 
            // btnRollback
            // 
            this.btnRollback.Location = new System.Drawing.Point(152, 336);
            this.btnRollback.Name = "btnRollback";
            this.btnRollback.Size = new System.Drawing.Size(62, 23);
            this.btnRollback.TabIndex = 21;
            this.btnRollback.Text = "Rollback";
            this.btnRollback.UseVisualStyleBackColor = true;
            this.btnRollback.Click += new System.EventHandler(this.btnRollback_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(8, 336);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(66, 23);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(80, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFindPath
            // 
            this.btnFindPath.Location = new System.Drawing.Point(89, 109);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(126, 23);
            this.btnFindPath.TabIndex = 20;
            this.btnFindPath.Text = "Find Shortest Path";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);
            // 
            // btnbeginEdit
            // 
            this.btnbeginEdit.Location = new System.Drawing.Point(8, 195);
            this.btnbeginEdit.Name = "btnbeginEdit";
            this.btnbeginEdit.Size = new System.Drawing.Size(96, 23);
            this.btnbeginEdit.TabIndex = 19;
            this.btnbeginEdit.Text = "Begin Editing";
            this.btnbeginEdit.UseVisualStyleBackColor = true;
            this.btnbeginEdit.Click += new System.EventHandler(this.btnbeginEdit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(209, 91);
            this.label5.TabIndex = 18;
            this.label5.Text = "Please click the \"Edit\" button, and then\r\nselect a route segment on the map by \r\nMouse Click to add, remove or update its\r\nadjacent route segment information. After\r\nthat, you can click \"Save\" button to save\r\nthe result or \"Rollback\" button to cacel the\r\nediting.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 39);
            this.label4.TabIndex = 18;
            this.label4.Text = "Try to click \"Begin Editing\" button below to \r\nbegin editing route Index or \"Stop" +
                " Editing\"\r\nbutton to exit edit mode.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "Please click Button to get the shortest path \r\nbetween the points that are set as" +
                " below.";
            // 
            // txtStartPoint
            // 
            this.txtStartPoint.Enabled = false;
            this.txtStartPoint.Location = new System.Drawing.Point(89, 54);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.Size = new System.Drawing.Size(125, 20);
            this.txtStartPoint.TabIndex = 1;
            this.txtStartPoint.Text = "-96.736022,32.860551";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Coordinate:";
            // 
            // txtEndPoint
            // 
            this.txtEndPoint.Enabled = false;
            this.txtEndPoint.Location = new System.Drawing.Point(90, 83);
            this.txtEndPoint.Name = "txtEndPoint";
            this.txtEndPoint.Size = new System.Drawing.Size(124, 20);
            this.txtEndPoint.TabIndex = 2;
            this.txtEndPoint.Text = "-96.842102,32.755379";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "End Coordinate:";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            this.winformsMap1.MapClick += new System.EventHandler<MapClickWinformsMapEventArgs>(this.winformsMap1_MapClick);
            // 
            // plRoadInformation
            // 
            this.plRoadInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plRoadInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plRoadInformation.Controls.Add(this.groupBox2);
            this.plRoadInformation.Location = new System.Drawing.Point(196, 172);
            this.plRoadInformation.Name = "plRoadInformation";
            this.plRoadInformation.Size = new System.Drawing.Size(232, 112);
            this.plRoadInformation.TabIndex = 10;
            this.plRoadInformation.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnCancel);
            this.groupBox2.Controls.Add(this.btnConfirmRoadInfor);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtWeight);
            this.groupBox2.Controls.Add(this.txtSegmentType);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(0, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 110);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Route Segment Information";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Location = new System.Drawing.Point(137, 77);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(66, 23);
            this.BtnCancel.TabIndex = 8;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnConfirmRoadInfor
            // 
            this.btnConfirmRoadInfor.Location = new System.Drawing.Point(66, 77);
            this.btnConfirmRoadInfor.Name = "btnConfirmRoadInfor";
            this.btnConfirmRoadInfor.Size = new System.Drawing.Size(65, 23);
            this.btnConfirmRoadInfor.TabIndex = 9;
            this.btnConfirmRoadInfor.Text = "Ok";
            this.btnConfirmRoadInfor.UseVisualStyleBackColor = true;
            this.btnConfirmRoadInfor.Click += new System.EventHandler(this.btnConfirmRoadInfor_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Weight:";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(94, 47);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(109, 20);
            this.txtWeight.TabIndex = 5;
            // 
            // txtSegmentType
            // 
            this.txtSegmentType.Location = new System.Drawing.Point(94, 21);
            this.txtSegmentType.Name = "txtSegmentType";
            this.txtSegmentType.Size = new System.Drawing.Size(109, 20);
            this.txtSegmentType.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Segment Type:";
            // 
            // EditRoutingIndexFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plRoadInformation);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "EditRoutingIndexFile";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.plRoadInformation.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
