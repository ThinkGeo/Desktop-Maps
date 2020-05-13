using System;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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
    public class POIonRoute : UserControl
    {
        private static RoutingEngine routingEngine;
        private static RoutingSource RoutingSourceForShortest;
        private static string rootPath = Samples.rootDirectory;

        public POIonRoute()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));

            RoutingSourceForShortest = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.rtg"));
            routingEngine = new RoutingEngine(RoutingSourceForShortest, featureSource);

            RenderMap();
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

            WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
            winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

            ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
            featureSource.Open();
            RoutingLayer routingLayer = new RoutingLayer();
            routingLayer.StartPoint = featureSource.GetFeatureById(txtStartId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            routingLayer.EndPoint = featureSource.GetFeatureById(txtEndId.Text, ReturningColumnsType.NoColumns).GetShape().GetCenterPoint();
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            ShapeFileFeatureLayer poiLayer = new ShapeFileFeatureLayer(Path.Combine(rootPath, "poi.shp"));
            MemoryStream stream = new MemoryStream();
            Properties.Resources.Station.Save(stream, ImageFormat.Png);
            poiLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(stream));
            poiLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay poiOverlay = new LayerOverlay();
            poiOverlay.Layers.Add("POIlayer", poiLayer);
            winformsMap1.Overlays.Add("POIoverlay", poiOverlay);

            InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
            routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
            routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

            winformsMap1.Refresh();
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            RoutingLayer routingLayer = (RoutingLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            routingLayer.Routes.Clear();
            routingLayer.Routes.Add(routingEngine.GetRoute(txtStartId.Text, txtEndId.Text).Route);

            InMemoryFeatureLayer POIsOnRouteLayer = new InMemoryFeatureLayer();
            MemoryStream stream = new MemoryStream();
            Properties.Resources.Gas_Station.Save(stream, ImageFormat.Png);
            POIsOnRouteLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(stream));
            POIsOnRouteLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay POIsOnRouteOverlay = new LayerOverlay();
            POIsOnRouteOverlay.Layers.Add(POIsOnRouteLayer);
            winformsMap1.Overlays.Add(POIsOnRouteOverlay);

            ShapeFileFeatureLayer poiLayer = (ShapeFileFeatureLayer)((LayerOverlay)winformsMap1.Overlays["POIoverlay"]).Layers["POIlayer"];
            poiLayer.Open();
            Collection<Feature> features = poiLayer.QueryTools.GetFeaturesWithinDistanceOf(routingLayer.Routes[0], GeographyUnit.DecimalDegree, DistanceUnit.Meter,
                                            100, ReturningColumnsType.NoColumns);
            poiLayer.Close();
            POIsOnRouteLayer.Open();
            POIsOnRouteLayer.EditTools.BeginTransaction();
            foreach (Feature feature in features)
            {
                POIsOnRouteLayer.EditTools.Add(feature);
            }
            POIsOnRouteLayer.EditTools.CommitTransaction();
            POIsOnRouteLayer.Close();

            winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);
        }
        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtEndId;
        private TextBox txtStartId;
        private Label label2;
        private WinformsMap winformsMap1;
        private Label label3;
        private Button btnRoute;
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRoute = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndId = new System.Windows.Forms.TextBox();
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
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtStartId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEndId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 206);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(6, 165);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(209, 23);
            this.btnRoute.TabIndex = 19;
            this.btnRoute.Text = "Find gas stations along the route";
            this.btnRoute.UseVisualStyleBackColor = true;
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 65);
            this.label3.TabIndex = 14;
            this.label3.Text = "Click the button to find the closest gas station\r\n along the shortest path between start and \r\nend points.";
            // 
            // txtStartId
            // 
            this.txtStartId.Enabled = false;
            this.txtStartId.Location = new System.Drawing.Point(85, 96);
            this.txtStartId.Name = "txtStartId";
            this.txtStartId.Size = new System.Drawing.Size(130, 20);
            this.txtStartId.TabIndex = 1;
            this.txtStartId.Text = "178813";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "StartFeatureId:";
            // 
            // txtEndId
            // 
            this.txtEndId.Enabled = false;
            this.txtEndId.Location = new System.Drawing.Point(85, 130);
            this.txtEndId.Name = "txtEndId";
            this.txtEndId.Size = new System.Drawing.Size(130, 20);
            this.txtEndId.TabIndex = 2;
            this.txtEndId.Text = "51928";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "EndFeatureId:";
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
            // 
            // POIonRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "POIonRoute";
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
