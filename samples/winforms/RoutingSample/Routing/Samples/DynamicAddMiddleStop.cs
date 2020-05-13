using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;

namespace HowDoISamples
{
    public class DynamicAddMiddleStop : UserControl
    {
        private bool isPointSelected;
        private string currentStopId;
        private Timer timer;

        public DynamicAddMiddleStop()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }


        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            InMemoryFeatureLayer mouseMoveLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["MouseMoveOverlay"]).Layers["MouseMoveLayer"];
            //winformsMap1.Overlays["MouseMoveOverlay"].Lock.EnterWriteLock();
            //try
            //{
            mouseMoveLayer.InternalFeatures.Clear();
            //}
            //finally
            //{
            //    winformsMap1.Overlays["MouseMoveOverlay"].Lock.ExitWriteLock();
            //}
            winformsMap1.Overlays["MouseMoveOverlay"].Lock.IsDirty = true;

            if (isPointSelected)
            {
                timer.Stop();

                InMemoryFeatureLayer stopLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["StopOverlay"]).Layers["StopLayer"];
                PointShape currentShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y, winformsMap1.Width, winformsMap1.Height);
                // Find the routeSegment which is nearest to the current mouse position
                FeatureLayer austinstreetsLayer = winformsMap1.FindFeatureLayer("AustinstreetsLayer");
                austinstreetsLayer.Open();
                Collection<Feature> tempFeatures = austinstreetsLayer.QueryTools.GetFeaturesNearestTo(currentShape, GeographyUnit.DecimalDegree, 1, new string[] { });
                Feature stopRoad = tempFeatures[0];
                austinstreetsLayer.Close();

                Feature stopPoint = new Feature(stopRoad.GetShape().GetCenterPoint().GetWellKnownBinary(), stopRoad.Id);
                //winformsMap1.Overlays["StopOverlay"].Lock.EnterWriteLock();
                //try
                //{
                stopLayer.InternalFeatures[currentStopId] = stopPoint;
                //}
                //finally
                //{
                //    winformsMap1.Overlays["StopOverlay"].Lock.ExitWriteLock();
                //}
                winformsMap1.Overlays["StopOverlay"].Lock.IsDirty = true;
            }
            else
            {
                if (string.IsNullOrEmpty(currentStopId))
                {
                    PointShape targetPointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, e.X, e.Y, winformsMap1.Width, winformsMap1.Height);
                    double searchingTolerance = GetControlPointOffsetRadius(winformsMap1.Width, winformsMap1.CurrentExtent.Width, 10);
                    RectangleShape searchingArea = new RectangleShape(targetPointShape.X - searchingTolerance, targetPointShape.Y + searchingTolerance, targetPointShape.X + searchingTolerance, targetPointShape.Y - searchingTolerance);
                    inMemoryLayer.Open();
                    Collection<Feature> features = inMemoryLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);
                    inMemoryLayer.Close();
                    foreach (Feature item in features)
                    {
                        Collection<LineShape> lines = ConvertLineBaseShapeToLines((LineBaseShape)item.GetShape());
                        Feature feature = GetPointFromLines(lines, targetPointShape, searchingArea);
                        if (!string.IsNullOrEmpty(feature.Id))
                        {
                            //winformsMap1.Overlays["MouseMoveOverlay"].Lock.EnterWriteLock();
                            //try
                            //{
                            mouseMoveLayer.InternalFeatures.Add(feature);
                            //}
                            //finally
                            //{
                            //    winformsMap1.Overlays["MouseMoveOverlay"].Lock.ExitWriteLock();
                            //}
                            winformsMap1.Overlays["MouseMoveOverlay"].Lock.IsDirty = true;
                            winformsMap1.Refresh();
                            break;
                        }
                    }
                }
            }
            winformsMap1.Refresh();
            timer.Start();
        }

        private void winformsMap1_MouseDown(object sender, MouseEventArgs e)
        {
            InMemoryFeatureLayer mouseMoveLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["MouseMoveOverlay"]).Layers["MouseMoveLayer"];
            if (mouseMoveLayer.InternalFeatures.Count > 0 && string.IsNullOrEmpty(currentStopId))
            {
                isPointSelected = true;
                InMemoryFeatureLayer stopLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["StopOverlay"]).Layers["StopLayer"];
                PointShape currentShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, ((MouseEventArgs)e).X, ((MouseEventArgs)e).Y, winformsMap1.Width, winformsMap1.Height);

                FeatureLayer austinstreetsLayer = winformsMap1.FindFeatureLayer("AustinstreetsLayer");
                austinstreetsLayer.Open();
                Collection<Feature> tempFeatures = austinstreetsLayer.QueryTools.GetFeaturesNearestTo(currentShape, GeographyUnit.DecimalDegree, 1, new string[] { });
                Feature stopRoad = tempFeatures[0];
                austinstreetsLayer.Close();
                currentStopId = stopRoad.Id;
                Feature stopPoint = new Feature(stopRoad.GetShape().GetCenterPoint().GetWellKnownBinary(), stopRoad.Id);
                Feature lastFeature = stopLayer.InternalFeatures[stopLayer.InternalFeatures.Count - 1];
                //winformsMap1.Overlays["StopOverlay"].Lock.EnterWriteLock();
                //try
                //{
                stopLayer.InternalFeatures.RemoveAt(stopLayer.InternalFeatures.Count - 1);
                stopLayer.InternalFeatures.Add(currentStopId, stopPoint);
                stopLayer.InternalFeatures.Add(lastFeature.Id, lastFeature);
                //}
                //finally
                //{
                //    winformsMap1.Overlays["StopOverlay"].Lock.ExitWriteLock();
                //}
                winformsMap1.Overlays["StopOverlay"].Lock.IsDirty = true;
            }
        }

        private void winformsMap1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isPointSelected)
            {
                isPointSelected = false;
                RenderPathBetweenPoints();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RenderPathBetweenPoints();
        }

        private static double GetControlPointOffsetRadius(double screenWidth, double currentExtentWidth, double distanceInPixel)
        {
            double radius = 0;

            if (screenWidth != 0)
            {
                radius = distanceInPixel / screenWidth * currentExtentWidth;
            }

            return radius;
        }

        private Feature GetPointFromLines(IEnumerable<LineShape> lines, PointShape targetPointShape, RectangleShape searchingArea)
        {
            Feature returnFeature = new Feature();

            foreach (LineShape targetLineShape in lines)
            {
                if (searchingArea.Intersects(targetLineShape))
                {
                    Vertex vertexToBeAdded = new Vertex(targetLineShape.GetClosestPointTo(targetPointShape, GeographyUnit.DecimalDegree));
                    return new Feature(vertexToBeAdded);
                }
            }

            return returnFeature;
        }

        private static Collection<LineShape> ConvertLineBaseShapeToLines(LineBaseShape sourceShape)
        {
            Collection<LineShape> lines = null;
            LineShape lineShape = sourceShape as LineShape;
            if (lineShape != null)
            {
                lines = new Collection<LineShape>();
                lines.Add(lineShape);
            }
            else
            {
                lines = ((MultilineShape)sourceShape).Lines;
            }
            return lines;
        }

        private void RenderMap()
        {
            winformsMap1.ExtentOverlay.PanMode = MapPanMode.Disabled;
            // Render the map
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-97.763384, 30.299707, -97.712382, 30.259309);

            ShapeFileFeatureLayer austinstreetsLayer = new ShapeFileFeatureLayer(@"..\..\SampleData\Austinstreets.shp");
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(GetRoadStyle());
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay austinstreetsOverlay = new LayerOverlay();
            austinstreetsOverlay.Layers.Add("AustinstreetsLayer", austinstreetsLayer);
            winformsMap1.Overlays.Add("AustinstreetsOverlay", austinstreetsOverlay);

            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(100, GeoColor.StandardColors.Purple), 5);
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            InMemoryFeatureLayer stopLayer = new InMemoryFeatureLayer();
            stopLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City1;
            stopLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay stopOverlay = new LayerOverlay();
            stopOverlay.Layers.Add("StopLayer", stopLayer);
            winformsMap1.Overlays.Add("StopOverlay", stopOverlay);

            austinstreetsLayer.Open();
            Feature startRoad = austinstreetsLayer.FeatureSource.GetFeatureById("4716", ReturningColumnsType.NoColumns);
            Feature endRoad = austinstreetsLayer.FeatureSource.GetFeatureById("9638", ReturningColumnsType.NoColumns);
            austinstreetsLayer.Close();
            Feature startPoint = new Feature(startRoad.GetShape().GetCenterPoint().GetWellKnownBinary(), "4716");
            Feature endPoint = new Feature(endRoad.GetShape().GetCenterPoint().GetWellKnownBinary(), "9638");
            stopLayer.InternalFeatures.Add("4716", startPoint);
            stopLayer.InternalFeatures.Add("9638", endPoint);

            //Handle Mouse move event
            InMemoryFeatureLayer mouseMoveLayer = new InMemoryFeatureLayer();
            mouseMoveLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City4;
            mouseMoveLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay mouseMoveOverlay = new LayerOverlay();
            mouseMoveOverlay.Layers.Add("MouseMoveLayer", mouseMoveLayer);
            winformsMap1.Overlays.Add("MouseMoveOverlay", mouseMoveOverlay);

            RenderPathBetweenPoints();

            // make sure the whole map render finished, then can drag to add middle stop.
            winformsMap1.ThreadingMode = MapThreadingMode.Multithreaded;
        }

        private void RenderPathBetweenPoints()
        {
            FeatureSource featureSource = new ShapeFileFeatureSource(@"..\..\SampleData\Austinstreets.shp");
            RoutingSource routingSource = new RtgRoutingSource(@"..\..\SampleData\Austinstreets.rtg");
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);

            RoutingResult routingResult = null;
            InMemoryFeatureLayer stopLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["StopOverlay"]).Layers["StopLayer"];
            if (stopLayer.InternalFeatures.Count > 2)
            {
                string startId = stopLayer.InternalFeatures[0].Id;
                string stopId = stopLayer.InternalFeatures[1].Id;
                string endId = stopLayer.InternalFeatures[2].Id;
                routingResult = routingEngine.GetRoute(startId, stopId);
                RoutingResult secondRoutingResult = routingEngine.GetRoute(stopId, endId);
                foreach (Feature item in secondRoutingResult.Features)
                {
                    routingResult.Features.Add(item);
                }
            }
            else
            {
                routingResult = routingEngine.GetRoute("4716", "9638");
            }
            //winformsMap1.Overlays["RoutingOverlay"].Lock.EnterWriteLock();
            //try
            //{
            InMemoryFeatureLayer inmemoryLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
            inmemoryLayer.InternalFeatures.Clear();

            foreach (Feature feature in routingResult.Features)
            {
                inmemoryLayer.InternalFeatures.Add(feature);
            }
            //}
            //finally
            //{
            //    winformsMap1.Overlays["RoutingOverlay"].Lock.ExitWriteLock();
            //}
            winformsMap1.Overlays["RoutingOverlay"].Lock.IsDirty = true;
            winformsMap1.Refresh();
        }

        private Style GetRoadStyle()
        {
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "CFCC";
            LineStyle highwayStyle = new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 7), new GeoPen(GeoColor.FromHtml("#f9de4d"), 5));
            ValueItem highwayItem1 = new ValueItem("A11", highwayStyle);
            ValueItem highwayItem2 = new ValueItem("A21", highwayStyle);
            ValueItem highwayItem3 = new ValueItem("A25", highwayStyle);
            ValueItem highwayItem4 = new ValueItem("A31", highwayStyle);
            ValueItem highwayItem5 = new ValueItem("A35", highwayStyle);

            LineStyle localroadStyle = new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 5), new GeoPen(GeoColor.StandardColors.White, 3));
            ValueItem localroadItem1 = new ValueItem("A41", localroadStyle);
            ValueItem localroadItem2 = new ValueItem("A51", localroadStyle);
            ValueItem localroadItem3 = new ValueItem("A63", localroadStyle);
            ValueItem localroadItem4 = new ValueItem("A64", localroadStyle);
            ValueItem localroadItem5 = new ValueItem("A74", localroadStyle);
            ValueItem unknownItem = new ValueItem("P41", localroadStyle);

            valueStyle.ValueItems.Add(highwayItem1);
            valueStyle.ValueItems.Add(highwayItem2);
            valueStyle.ValueItems.Add(highwayItem3);
            valueStyle.ValueItems.Add(highwayItem4);
            valueStyle.ValueItems.Add(highwayItem5);
            valueStyle.ValueItems.Add(localroadItem1);
            valueStyle.ValueItems.Add(localroadItem2);
            valueStyle.ValueItems.Add(localroadItem3);
            valueStyle.ValueItems.Add(localroadItem4);
            valueStyle.ValueItems.Add(localroadItem5);
            valueStyle.ValueItems.Add(unknownItem);

            return valueStyle;
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private WinformsMap winformsMap1;
        private Label label1;
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
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
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
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 74);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 39);
            this.label1.TabIndex = 8;
            this.label1.Text = "Move the mouse to the path and drag to\r\nadd a middle stop where the mouse is \r\nhe" +
                "ld  on for 200ms.";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.SingleThreaded;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            this.winformsMap1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseUp);
            this.winformsMap1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseMove);
            this.winformsMap1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.winformsMap1_MouseDown);
            // 
            // DynamicAddMiddleStop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DynamicAddMiddleStop";
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
