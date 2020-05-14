using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class PlotALatitudeAndLongitudePointOnTheMap : UserControl
    {
        public PlotALatitudeAndLongitudePointOnTheMap()
        {
            InitializeComponent();
        }

        private void PlotALatitudeAndLongitudePointOnTheMap_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            InMemoryFeatureLayer pointLayer = new InMemoryFeatureLayer();
            pointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.White, 8, GeoColors.Red, 1);
            pointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            pointLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            LayerOverlay pointOverlay = new LayerOverlay();
            pointOverlay.TileType = TileType.SingleTile;
            pointOverlay.Layers.Add("PointLayer", pointLayer);
            mapView.Overlays.Add("PointOverlay", pointOverlay);

            mapView.CurrentExtent = new RectangleShape(-12922411, 8734539, -8568181, 687275);
        }

        private void btnPlot_Click(object sender, EventArgs e)
        {
            double latitude = Convert.ToDouble(txtLatitude.Text, CultureInfo.InvariantCulture);
            double longitude = Convert.ToDouble(txtLongitude.Text, CultureInfo.InvariantCulture);

            Feature feature = new Feature(longitude, latitude, "Point");

            InMemoryFeatureLayer pointLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PointLayer");
            if (!pointLayer.InternalFeatures.Contains("Point"))
            {
                pointLayer.InternalFeatures.Add("Point", feature);
            }

            mapView.Refresh(mapView.Overlays["PointOverlay"]);
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private Button btnPloat;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtLatitude;
        private TextBox txtLongitude;
        private Label label2;
        private MapView mapView;

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
            this.btnPloat = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
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
            // btnPloat
            //
            this.btnPloat.Location = new System.Drawing.Point(64, 68);
            this.btnPloat.Name = "btnPloat";
            this.btnPloat.Size = new System.Drawing.Size(81, 23);
            this.btnPloat.TabIndex = 7;
            this.btnPloat.Text = "Plot";
            this.btnPloat.UseVisualStyleBackColor = true;
            this.btnPloat.Click += new System.EventHandler(this.btnPlot_Click);
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.txtLongitude);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtLatitude);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPloat);
            this.groupBox1.Location = new System.Drawing.Point(585, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // txtLongitude
            //
            this.txtLongitude.Enabled = false;
            this.txtLongitude.Location = new System.Drawing.Point(64, 13);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(81, 20);
            this.txtLongitude.TabIndex = 12;
            this.txtLongitude.Text = "-96.8236";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Longitude";
            //
            // txtLatitude
            //
            this.txtLatitude.Enabled = false;
            this.txtLatitude.Location = new System.Drawing.Point(63, 37);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(81, 20);
            this.txtLatitude.TabIndex = 8;
            this.txtLatitude.Text = "33.1507"; 
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Latitude";
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.Size = new System.Drawing.Size(740, 528);
            this.mapView.TabIndex = 0;
            //
            // PlotALatitudeAndLongitudePointOnTheMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.mapView);
            this.Name = "PlotALatitudeAndLongitudePointOnTheMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.PlotALatitudeAndLongitudePointOnTheMap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Component Designer generated code
    }
}