using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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
    public class RoutingAroundRoadblocks : UserControl
    {
        private RoutingEngine routingEngine;
        private RoutingSource routingSource;
        private ShapeFileFeatureSource featureSource;
        private InMemoryFeatureLayer roadblocksLayer;

        private bool isAddingRoadblocks;
        private Collection<string> avoidableFeatureIds;
        private Feature startFeature;
        private Feature endFeature;
        private static string rootPath = Samples.rootDirectory;


        public RoutingAroundRoadblocks()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
            routingEngine = new RoutingEngine(routingSource, featureSource);
            routingEngine.RoutingAlgorithm.FindingRoute += new EventHandler<FindingRouteRoutingAlgorithmEventArgs>(RoutingAlgorithm_FindingRoute);

            isAddingRoadblocks = false;
            avoidableFeatureIds = new Collection<string>();

            RenderMap();
            Route();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            Route();
        }

        private void Route()
        {
            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];

            avoidableFeatureIds.Clear();
            foreach (Feature feature in roadblocksLayer.InternalFeatures)
            {
                avoidableFeatureIds.Add(feature.Id);
            }

            routingLayer.Routes.Clear();
            RoutingResult routingResult = routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.EndPoint);
            if (routingResult.Features.Count == 0)
            {
                MessageBox.Show("No route exists!");
            }
            else
            {
                routingLayer.Routes.Add(routingResult.Route);
            }

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }

        void RoutingAlgorithm_FindingRoute(object sender, FindingRouteRoutingAlgorithmEventArgs e)
        {
            Collection<string> startPointAdjacentIds = e.RouteSegment.StartPointAdjacentIds;
            Collection<string> endPointAdjacentIds = e.RouteSegment.EndPointAdjacentIds;

            Collection<string> beContainedFeatureIds = new Collection<string>();
            foreach (string featureId in startPointAdjacentIds)
            {
                if (avoidableFeatureIds.Contains(featureId))
                {
                    beContainedFeatureIds.Add(featureId);
                }
            }
            foreach (string featureId in endPointAdjacentIds)
            {
                if (avoidableFeatureIds.Contains(featureId))
                {
                    beContainedFeatureIds.Add(featureId);
                }
            }

            // Remove the ones that be contained in the avoidable area
            foreach (string id in beContainedFeatureIds)
            {
                if (e.RouteSegment.StartPointAdjacentIds.Contains(id))
                {
                    e.RouteSegment.StartPointAdjacentIds.Remove(id);
                }
                if (e.RouteSegment.EndPointAdjacentIds.Contains(id))
                {
                    e.RouteSegment.EndPointAdjacentIds.Remove(id);
                }
            }
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            if (isAddingRoadblocks)
            {
                featureSource.Open();
                Collection<Feature> closestFeatures = featureSource.GetFeaturesNearestTo(e.WorldLocation, winformsMap1.MapUnit, 1, ReturningColumnsType.NoColumns);
                if (closestFeatures.Count > 0)
                {
                    PointShape position = ((LineBaseShape)closestFeatures[0].GetShape()).GetCenterPoint();
                    Feature feature = new Feature(position.GetWellKnownBinary(), closestFeatures[0].Id);
                    if (feature.Id != startFeature.Id && feature.Id != endFeature.Id)
                    {
                        roadblocksLayer.InternalFeatures.Add(feature);
                    }

                    LayerOverlay overlay = (LayerOverlay)winformsMap1.Overlays["RoutingOverlay"];
                    winformsMap1.Refresh(overlay);
                }
                Route();
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            isAddingRoadblocks = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            isAddingRoadblocks = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            roadblocksLayer.InternalFeatures.Clear();
            Route();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int featureCount = roadblocksLayer.InternalFeatures.Count;
            if (featureCount > 0)
            {
                roadblocksLayer.InternalFeatures.RemoveAt(roadblocksLayer.InternalFeatures.Count - 1);
            }
            Route();
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            RoutingLayer routingLayer = new RoutingLayer();
            string[] startCoordinates = txtStartCoordinate.Text.Split(',');
            routingLayer.StartPoint = new PointShape(double.Parse(startCoordinates[0], CultureInfo.InvariantCulture), double.Parse(startCoordinates[1], CultureInfo.InvariantCulture));
            string[] endCoordinates = txtEndCoordinate.Text.Split(',');
            routingLayer.EndPoint = new PointShape(double.Parse(endCoordinates[0], CultureInfo.InvariantCulture), double.Parse(endCoordinates[1], CultureInfo.InvariantCulture));
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            featureSource.Open();
            startFeature = featureSource.GetFeaturesNearestTo(routingLayer.StartPoint, winformsMap1.MapUnit, 1, ReturningColumnsType.NoColumns)[0];
            endFeature = featureSource.GetFeaturesNearestTo(routingLayer.EndPoint, winformsMap1.MapUnit, 1, ReturningColumnsType.NoColumns)[0];

            roadblocksLayer = new InMemoryFeatureLayer();
            MemoryStream stream = new MemoryStream();
            Properties.Resources.roadblock.Save(stream, ImageFormat.Png);
            roadblocksLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(stream));
            //stream.Close();
            roadblocksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingOverlay.Layers.Add("roadblocksLayer", roadblocksLayer);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndCoordinate;
        private TextBox txtStartCoordinate;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private Label label4;
        private Button btnClear;
        private Button btnBegin;
        private Button btnStop;
        private Button btnRoute;
        private Button btnRemove;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoutingAroundRoadblocks));
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBegin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartCoordinate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndCoordinate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnBegin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartCoordinate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndCoordinate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 336);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(22, 302);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(183, 23);
            this.btnRoute.TabIndex = 18;
            this.btnRoute.Text = "Route Around Roadblocks";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(116, 129);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(102, 23);
            this.btnStop.TabIndex = 17;
            this.btnStop.Text = "Stop Adding";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 26);
            this.label4.TabIndex = 16;
            this.label4.Text = "Generate a route around any roadblocks by\r\nclicking \"Route Around Roadblocks\"";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(7, 158);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(102, 23);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(116, 158);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(102, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(7, 129);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(103, 23);
            this.btnBegin.TabIndex = 15;
            this.btnBegin.Text = "Add Roadblock";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 91);
            this.label3.TabIndex = 14;
            this.label3.Text = "Click the \"Add Roadblock\" Button, and then \r\nplace roadblocks by clicking on the map. You \r\ncan return to normal mode by clicking \r\n\"Stop Adding\" Button. Of course, the \r\n\"Remove\" Button is used to delete the \r\nprevious roadblock you have added and \r\n\"Clear\" button is used to clear all roadblocks;\r\n";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartCoordinate
            // 
            this.txtStartCoordinate.Enabled = false;
            this.txtStartCoordinate.Location = new System.Drawing.Point(93, 238);
            this.txtStartCoordinate.Name = "txtStartCoordinate";
            this.txtStartCoordinate.Size = new System.Drawing.Size(130, 20);
            this.txtStartCoordinate.TabIndex = 1;
            this.txtStartCoordinate.Text = "-96.736022,32.860551";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start Coordinate:";
            // 
            // txtEndCoordinate
            // 
            this.txtEndCoordinate.Enabled = false;
            this.txtEndCoordinate.Location = new System.Drawing.Point(93, 267);
            this.txtEndCoordinate.Name = "txtEndCoordinate";
            this.txtEndCoordinate.Size = new System.Drawing.Size(130, 20);
            this.txtEndCoordinate.TabIndex = 2;
            this.txtEndCoordinate.Text = "-96.842102,32.755379";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 270);
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
            // RoutingAroundRoadblocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "RoutingAroundRoadblocks";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
