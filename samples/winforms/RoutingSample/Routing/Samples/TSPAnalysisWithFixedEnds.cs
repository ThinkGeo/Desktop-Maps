using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Routing;
using System.Globalization;
using System.Diagnostics;
using System.IO;
using ThinkGeo.MapSuite.RoutingSamples;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

public class TSPAnalysisWithFixedEnds : UserControl
{
    private static RoutingEngine routingEngine;
    private Label label7;
    private static RoutingSource routingSource;
    private static string rootPath = Samples.rootDirectory;

    public TSPAnalysisWithFixedEnds()
    {
        InitializeComponent();
    }

    private void UserControl_Load(object sender, EventArgs e)
    {
        ShapeFileFeatureSource featureSource = new ShapeFileFeatureSource(Path.Combine(rootPath, "DallasCounty-4326.shp"));
        routingSource = new RtgRoutingSource(Path.Combine(rootPath, "DallasCounty-4326.shortest.rtg"));
        routingEngine = new RoutingEngine(routingSource, new AStarRoutingAlgorithm(), featureSource);

        RenderMap();
    }

    private void btnFindBestRoute_Click(object sender, EventArgs e)
    {
        LayerOverlay overlay = (LayerOverlay)winformsMap1.Overlays["RoutingOverlay"];
        RoutingLayer routingLayer = (RoutingLayer)overlay.Layers["RoutingLayer"];

        Stopwatch watch = new Stopwatch();
        watch.Start();
        //New API for GetRouteViaVisitstops wher you can have a start and an end point.
        RoutingResult routingResult = routingEngine.GetRoute(routingLayer.StartPoint, routingLayer.EndPoint, routingLayer.StopPoints, int.Parse(txtIterations.Text));
        watch.Stop();
        // Render the route
        routingLayer.Routes.Clear();
        routingLayer.Routes.Add(routingResult.Route);
        winformsMap1.Refresh(winformsMap1.Overlays["RoutingOverlay"]);

        routingLayer.StopPoints.Clear();
        foreach (PointShape stop in routingResult.OrderedStops)
        {
            routingLayer.StopPoints.Add(stop);
        }
        routingLayer.ShowStopOrder = true;
        txtTimeUsed.Text = watch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + " ms";
        txtDistance.Text = routingResult.Distance.ToString(CultureInfo.InvariantCulture);
        winformsMap1.Refresh(overlay);
    }

    private void RenderMap()
    {
        winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
        winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
        winformsMap1.CurrentExtent = new RectangleShape(-96.905564, 32.926216, -96.651506, 32.744942);

        WorldStreetsAndImageryOverlay worldStreetsAndImageryOverlay = new WorldStreetsAndImageryOverlay();
        winformsMap1.Overlays.Add(worldStreetsAndImageryOverlay);

        RoutingLayer routingLayer = new RoutingLayer();
        LayerOverlay routingOverlay = new LayerOverlay();
        winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);
        routingOverlay.Layers.Add("RoutingLayer", routingLayer);

        string[] startCoordinates = txtStart.Text.Split(',');
        routingLayer.StartPoint = new PointShape(double.Parse(startCoordinates[0], CultureInfo.InvariantCulture), double.Parse(startCoordinates[1], CultureInfo.InvariantCulture));
        string[] endCoordinates = txtEnd.Text.Split(',');
        routingLayer.EndPoint = new PointShape(double.Parse(endCoordinates[0], CultureInfo.InvariantCulture), double.Parse(endCoordinates[1], CultureInfo.InvariantCulture));
        foreach (object item in lsbPoints.Items)
        {
            string[] coordinate = item.ToString().Split(',');
            PointShape pointNeedVisit = new PointShape(double.Parse(coordinate[0], CultureInfo.InvariantCulture), double.Parse(coordinate[1], CultureInfo.InvariantCulture));
            routingLayer.StopPoints.Add(pointNeedVisit);
        }
        routingLayer.ShowStopOrder = false;

        InMemoryFeatureLayer routingExtentLayer = new InMemoryFeatureLayer();
        routingExtentLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green));
        routingExtentLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        routingExtentLayer.InternalFeatures.Add(new Feature(new RectangleShape(-97.080185, 33.013491, -96.465213, 32.490127)));
        routingOverlay.Layers.Add("RoutingExtentLayer", routingExtentLayer);

        winformsMap1.Refresh();
    }

    #region "Component Designer generated code"

    private Label lblCoordinate;
    private GroupBox groupBox1;
    private Label Label1;
    private Label label2;
    private Label Label3;
    private WinformsMap winformsMap1;
    private TextBox txtStart;
    private ListBox lsbPoints;
    private Button btnFindBestRoute;
    private Label label4;
    private TextBox txtIterations;
    private TextBox txtTimeUsed;
    private Label label6;
    private TextBox txtDistance;
    private TextBox txtEnd;
    private Label label5;
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TSPAnalysisWithFixedEnds));
        this.lblCoordinate = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.label7 = new System.Windows.Forms.Label();
        this.Label3 = new System.Windows.Forms.Label();
        this.lsbPoints = new System.Windows.Forms.ListBox();
        this.txtEnd = new System.Windows.Forms.TextBox();
        this.Label1 = new System.Windows.Forms.Label();
        this.txtTimeUsed = new System.Windows.Forms.TextBox();
        this.label6 = new System.Windows.Forms.Label();
        this.txtDistance = new System.Windows.Forms.TextBox();
        this.label5 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.btnFindBestRoute = new System.Windows.Forms.Button();
        this.txtIterations = new System.Windows.Forms.TextBox();
        this.txtStart = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
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
        this.groupBox1.Controls.Add(this.label7);
        this.groupBox1.Controls.Add(this.Label3);
        this.groupBox1.Controls.Add(this.lsbPoints);
        this.groupBox1.Controls.Add(this.txtEnd);
        this.groupBox1.Controls.Add(this.Label1);
        this.groupBox1.Controls.Add(this.txtTimeUsed);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.txtDistance);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.btnFindBestRoute);
        this.groupBox1.Controls.Add(this.txtIterations);
        this.groupBox1.Controls.Add(this.txtStart);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Location = new System.Drawing.Point(514, 0);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(226, 413);
        this.groupBox1.TabIndex = 8;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Instructions";
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(10, 20);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(221, 91);
        this.label7.TabIndex = 30;
        this.label7.Text = "This sample demonstrates how to generate\r\n the optimal sequence of visiting locations, \r\nbetween two fixed ends. The iterations value\r\n is a number that you can set to improve the\r\n accuracy of the RoutingResult. The larger\r\n the iterations value the more accurate and\r\n time consuming the result.";
        // 
        // Label3
        // 
        this.Label3.AutoSize = true;
        this.Label3.Location = new System.Drawing.Point(9, 206);
        this.Label3.Name = "Label3";
        this.Label3.Size = new System.Drawing.Size(37, 13);
        this.Label3.TabIndex = 29;
        this.Label3.Text = "Stops:";
        // 
        // lsbPoints
        // 
        this.lsbPoints.FormattingEnabled = true;
        this.lsbPoints.Items.AddRange(new object[] {
            "-96.735022,32.850551",
            "-96.826500,32.830000",
            "-96.783500,32.855100",
            "-96.780600,32.855300"});
        this.lsbPoints.Location = new System.Drawing.Point(13, 226);
        this.lsbPoints.Name = "lsbPoints";
        this.lsbPoints.Size = new System.Drawing.Size(207, 95);
        this.lsbPoints.TabIndex = 19;
        // 
        // txtEnd
        // 
        this.txtEnd.Enabled = false;
        this.txtEnd.Location = new System.Drawing.Point(78, 150);
        this.txtEnd.Name = "txtEnd";
        this.txtEnd.Size = new System.Drawing.Size(142, 20);
        this.txtEnd.TabIndex = 28;
        this.txtEnd.Text = "-96.842102,32.755379";
        // 
        // Label1
        // 
        this.Label1.AutoSize = true;
        this.Label1.Location = new System.Drawing.Point(10, 153);
        this.Label1.Name = "Label1";
        this.Label1.Size = new System.Drawing.Size(53, 13);
        this.Label1.TabIndex = 27;
        this.Label1.Text = "EndPoint:";
        // 
        // txtTimeUsed
        // 
        this.txtTimeUsed.Location = new System.Drawing.Point(92, 358);
        this.txtTimeUsed.Name = "txtTimeUsed";
        this.txtTimeUsed.ReadOnly = true;
        this.txtTimeUsed.Size = new System.Drawing.Size(128, 20);
        this.txtTimeUsed.TabIndex = 26;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(24, 363);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(61, 13);
        this.label6.TabIndex = 25;
        this.label6.Text = "Time Used:";
        // 
        // txtDistance
        // 
        this.txtDistance.Location = new System.Drawing.Point(92, 332);
        this.txtDistance.Name = "txtDistance";
        this.txtDistance.ReadOnly = true;
        this.txtDistance.Size = new System.Drawing.Size(128, 20);
        this.txtDistance.TabIndex = 26;
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(6, 336);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(79, 13);
        this.label5.TabIndex = 25;
        this.label5.Text = "Total Distance:";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(10, 179);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(53, 13);
        this.label4.TabIndex = 24;
        this.label4.Text = "Iterations:";
        // 
        // btnFindBestRoute
        // 
        this.btnFindBestRoute.Location = new System.Drawing.Point(38, 384);
        this.btnFindBestRoute.Name = "btnFindBestRoute";
        this.btnFindBestRoute.Size = new System.Drawing.Size(153, 23);
        this.btnFindBestRoute.TabIndex = 20;
        this.btnFindBestRoute.Text = "Find The Best Route";
        this.btnFindBestRoute.UseVisualStyleBackColor = true;
        this.btnFindBestRoute.Click += new System.EventHandler(this.btnFindBestRoute_Click);
        // 
        // txtIterations
        // 
        this.txtIterations.Location = new System.Drawing.Point(78, 176);
        this.txtIterations.Name = "txtIterations";
        this.txtIterations.Size = new System.Drawing.Size(142, 20);
        this.txtIterations.TabIndex = 1;
        this.txtIterations.Text = "100";
        // 
        // txtStart
        // 
        this.txtStart.Enabled = false;
        this.txtStart.Location = new System.Drawing.Point(78, 124);
        this.txtStart.Name = "txtStart";
        this.txtStart.Size = new System.Drawing.Size(142, 20);
        this.txtStart.TabIndex = 1;
        this.txtStart.Text = "-96.736022,32.860551";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(10, 127);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(56, 13);
        this.label2.TabIndex = 13;
        this.label2.Text = "StartPoint:";
        // 
        // winformsMap1
        // 
        this.winformsMap1.BackColor = System.Drawing.Color.White;
        this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
        this.winformsMap1.CurrentScale = 590591790D;
        this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.winformsMap1.DrawingQuality = DrawingQuality.Default;
        this.winformsMap1.Location = new System.Drawing.Point(0, 0);
        this.winformsMap1.MapFocusMode = MapFocusMode.Default;
        this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
        this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
        this.winformsMap1.MaximumScale = 80000000000000D;
        this.winformsMap1.MinimumScale = 200D;
        this.winformsMap1.Name = "winformsMap1";
        this.winformsMap1.Size = new System.Drawing.Size(740, 528);
        this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        this.winformsMap1.TabIndex = 9;
        this.winformsMap1.Text = "winformsMap1";
        this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        this.winformsMap1.ThreadingMode = MapThreadingMode.SingleThreaded;
        this.winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
        // 
        // TSPAnalysisWithFixedEnds
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.lblCoordinate);
        this.Controls.Add(this.winformsMap1);
        this.Name = "TSPAnalysisWithFixedEnds";
        this.Size = new System.Drawing.Size(740, 528);
        this.Load += new System.EventHandler(this.UserControl_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
}
