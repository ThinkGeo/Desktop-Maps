/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public partial class ConvertWorldCoordinatesToScreenCoordinates : UserControl
    {
        public ConvertWorldCoordinatesToScreenCoordinates()
        {
            InitializeComponent();
        }

        private void ConvertScreenCoordinateToWorldCoordinate_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            Proj4Projection proj4Projection = new Proj4Projection(3857, 4326);
            proj4Projection.Open();
            RectangleShape extent = proj4Projection.ConvertToExternalProjection(winformsMap1.CurrentExtent);

            ScreenPointF screenPoint = ExtentHelper.ToScreenCoordinate(extent, new PointShape(Double.Parse(longitudeTextBox.Text, CultureInfo.InvariantCulture), Double.Parse(latitudeTextBox.Text, CultureInfo.InvariantCulture)), 740, 528);
            screenPositionTextBox.Text = string.Format(CultureInfo.InvariantCulture, "({0}, {1})", screenPoint.X.ToString("N1", CultureInfo.InvariantCulture), screenPoint.Y.ToString("N1", CultureInfo.InvariantCulture));
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private TextBox screenPositionTextBox;
        private Label label3;
        private Button convertButton;
        private TextBox latitudeTextBox;
        private TextBox longitudeTextBox;
        private Label label2;
        private Label label1;
        private WinformsMap winformsMap1;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.screenPositionTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.convertButton = new System.Windows.Forms.Button();
            this.latitudeTextBox = new System.Windows.Forms.TextBox();
            this.longitudeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.screenPositionTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.convertButton);
            this.groupBox1.Controls.Add(this.latitudeTextBox);
            this.groupBox1.Controls.Add(this.longitudeTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(465, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 91);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "World Coordinates To Screen Coordiates";
            //
            // screenPositionTextBox
            //
            this.screenPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.screenPositionTextBox.Location = new System.Drawing.Point(103, 59);
            this.screenPositionTextBox.Name = "screenPositionTextBox";
            this.screenPositionTextBox.ReadOnly = true;
            this.screenPositionTextBox.Size = new System.Drawing.Size(90, 20);
            this.screenPositionTextBox.TabIndex = 17;
            this.screenPositionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // label3
            //
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Screen Position:";
            //
            // convertButton
            //
            this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.convertButton.Location = new System.Drawing.Point(199, 57);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(60, 23);
            this.convertButton.TabIndex = 15;
            this.convertButton.Text = "Convert WorldCoordinateTo ScreenCoordiate";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            //
            // latitudeTextBox
            //
            this.latitudeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.latitudeTextBox.Location = new System.Drawing.Point(199, 28);
            this.latitudeTextBox.Name = "latitudeTextBox";
            this.latitudeTextBox.ReadOnly = true;
            this.latitudeTextBox.Size = new System.Drawing.Size(60, 20);
            this.latitudeTextBox.TabIndex = 12;
            this.latitudeTextBox.Text = "38.9554";
            //
            // longitudeTextBox
            //
            this.longitudeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.longitudeTextBox.Location = new System.Drawing.Point(76, 28);
            this.longitudeTextBox.Name = "longitudeTextBox";
            this.longitudeTextBox.ReadOnly = true;
            this.longitudeTextBox.Size = new System.Drawing.Size(60, 20);
            this.longitudeTextBox.TabIndex = 11;
            this.longitudeTextBox.Text = "-95.2806";
            //
            // label2
            //
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Latitude:";
            //
            // label1
            //
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Longitude:";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 12;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ConvertWorldCoordinatesToScreenCoordinates
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ConvertWorldCoordinatesToScreenCoordinates";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ConvertScreenCoordinateToWorldCoordinate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}