/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class EfficientlyMoveAPlaneImage : UserControl
    {
        private Timer timer;
        private int index;

        public EfficientlyMoveAPlaneImage()
        {
            InitializeComponent();
            timer = new Timer();
        }

        private void EfficientlyMoveAPlaneImage_Load(object sender, EventArgs e)
        {
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);

            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Setup the shapefile layer.
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Setup the mapshape layer.
            InMemoryFeatureLayer bitmapLayer = new InMemoryFeatureLayer();
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.PointType = PointType.Bitmap;
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.Image = new GeoImage(Samples.RootDirectory + @"Data\Prop Plane.png");
            bitmapLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.ContestedBorder2;
            bitmapLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Proj4Projection proj4Projection = new Proj4Projection(4326, 3857);
            proj4Projection.Open();

            PointShape planeShape = new PointShape(-95.2806, 38.9554);
            PointShape destinationPoint = new PointShape(36.04, 48.49);
            MultilineShape airLineShape = planeShape.GreatCircle(destinationPoint);
            bitmapLayer.InternalFeatures.Add("Plane", new Feature(proj4Projection.ConvertToExternalProjection(planeShape)) { Id = "Plane" });
            bitmapLayer.InternalFeatures.Add("AirLine", new Feature(proj4Projection.ConvertToExternalProjection(airLineShape)) { Id= "AirLine" });

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            LayerOverlay planeOverlay = new LayerOverlay();
            planeOverlay.Layers.Add("BitmapLayer", bitmapLayer);
            winformsMap1.Overlays.Add("PlaneOverlay", planeOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            InMemoryFeatureLayer bitmapLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("BitmapLayer");
            PointShape pointShape = bitmapLayer.InternalFeatures["Plane"].GetShape() as PointShape;
            LineShape airLine = ((MultilineShape)bitmapLayer.InternalFeatures["AirLine"].GetShape()).Lines[0];
            index += 30;
            if (index < airLine.Vertices.Count)
            {
                Vertex newPosition = airLine.Vertices[index];
                pointShape.X = newPosition.X;
                pointShape.Y = newPosition.Y;
                pointShape.Id = "Plane";

                bitmapLayer.Open();
                bitmapLayer.EditTools.BeginTransaction();
                bitmapLayer.EditTools.Update(pointShape);
                bitmapLayer.EditTools.CommitTransaction();
                bitmapLayer.Close();
            }
            else
            {
                index = 0;
            }

            winformsMap1.Refresh(winformsMap1.Overlays["PlaneOverlay"]);
        }

        private void EfficientlyMoveAPlaneImage_ParentChanged(object sender, EventArgs e)
        {
            timer.Stop();
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        private GroupBox groupBox1;
        private WinformsMap winformsMap1;
        private Label label1;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(511, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 44);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.TabIndex = 0;
            this.label1.Text = "This sample shows how to move a image \r\nrepresent a plane accroding to route.";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 39;
            this.winformsMap1.Text = "winformsMap1";
            //
            // EfficientlyMoveACarImage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "EfficientlyMoveACarImage";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.EfficientlyMoveAPlaneImage_Load);
            this.ParentChanged += new EventHandler(EfficientlyMoveAPlaneImage_ParentChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}