using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class SetMapExtentSample: UserControl
    {
        ShapeFileFeatureLayer friscoCityBoundary;

        public SetMapExtentSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load the Frisco data to a layer
            friscoCityBoundary = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/City_ETJ.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            friscoCityBoundary.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style the data so that we can see it on the map
            friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
            friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add Frisco data to a LayerOverlay and add it to the map
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCityBoundary);
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            // Populate Controls
            friscoCityBoundary.Open();
            featureIds.DataSource = friscoCityBoundary.FeatureSource.GetFeatureIds();
            friscoCityBoundary.Close();
            featureIds.SelectedIndex = 0;
        }


        private void ZoomToScale_Click(object sender, EventArgs e)
        {
            mapView.ZoomToScale(Convert.ToDouble(zoomScale.Text));
        }

        private void latlonZoom_Click(object sender, EventArgs e)
        {
            // Create a PointShape from the lat-lon
            var latlonPoint = new PointShape(Convert.ToDouble(latitude.Text), Convert.ToDouble(longitude.Text));

            // Convert the lat-lon projection to match the map
            var projectionConverter = new ProjectionConverter(4326, 3857);
            projectionConverter.Open();
            var convertedPoint = (PointShape)projectionConverter.ConvertToExternalProjection(latlonPoint);
            projectionConverter.Close();

            // Zoom to the converted lat-lon at the desired scale
            mapView.ZoomTo(convertedPoint, Convert.ToDouble(latlonScale.Text));
        }

        private void layerBoundingBox_Click(object sender, EventArgs e)
        {
            mapView.CurrentExtent = friscoCityBoundary.GetBoundingBox();
            mapView.Refresh();
        }

        private void featureBoundingBox_Click(object sender, EventArgs e)
        {
            var feature = friscoCityBoundary.FeatureSource.GetFeatureById(featureIds.SelectedItem.ToString(), ReturningColumnsType.NoColumns);
            mapView.CurrentExtent = feature.GetBoundingBox();
            mapView.Refresh();
        }


        private Panel panel1;
        private TextBox latlonScale;
        private TextBox longitude;
        private TextBox latitude;
        private Button latlonZoom;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Button featureBoundingBox;
        private ComboBox featureIds;
        private Label label5;
        private Label label4;
        private Button layerBoundingBox;
        private Label label3;
        private Button ZoomToScale;
        private Label label2;
        private TextBox zoomScale;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.latlonScale = new System.Windows.Forms.TextBox();
            this.longitude = new System.Windows.Forms.TextBox();
            this.latitude = new System.Windows.Forms.TextBox();
            this.latlonZoom = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.featureBoundingBox = new System.Windows.Forms.Button();
            this.featureIds = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.layerBoundingBox = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ZoomToScale = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.zoomScale = new System.Windows.Forms.TextBox();
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
            this.mapView.Location = new System.Drawing.Point(4, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.Margin = new System.Windows.Forms.Padding(0);
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(946, 634);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.latlonScale);
            this.panel1.Controls.Add(this.longitude);
            this.panel1.Controls.Add(this.latitude);
            this.panel1.Controls.Add(this.latlonZoom);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.featureBoundingBox);
            this.panel1.Controls.Add(this.featureIds);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.layerBoundingBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ZoomToScale);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.zoomScale);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(953, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 634);
            this.panel1.TabIndex = 1;
            // 
            // latlonScale
            // 
            this.latlonScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latlonScale.Location = new System.Drawing.Point(180, 482);
            this.latlonScale.Name = "latlonScale";
            this.latlonScale.Size = new System.Drawing.Size(100, 23);
            this.latlonScale.TabIndex = 18;
            this.latlonScale.Text = "200000";
            // 
            // longitude
            // 
            this.longitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.longitude.Location = new System.Drawing.Point(180, 448);
            this.longitude.Name = "longitude";
            this.longitude.Size = new System.Drawing.Size(100, 23);
            this.longitude.TabIndex = 17;
            this.longitude.Text = "33.15";
            // 
            // latitude
            // 
            this.latitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latitude.Location = new System.Drawing.Point(180, 417);
            this.latitude.Name = "latitude";
            this.latitude.Size = new System.Drawing.Size(100, 23);
            this.latitude.TabIndex = 16;
            this.latitude.Text = "-96.82";
            // 
            // latlonZoom
            // 
            this.latlonZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.latlonZoom.Location = new System.Drawing.Point(3, 525);
            this.latlonZoom.Name = "latlonZoom";
            this.latlonZoom.Size = new System.Drawing.Size(296, 34);
            this.latlonZoom.TabIndex = 15;
            this.latlonZoom.Text = "Zoom to Lat/Lon";
            this.latlonZoom.UseVisualStyleBackColor = true;
            this.latlonZoom.Click += new System.EventHandler(this.latlonZoom_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(17, 482);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "Scale:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(17, 448);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Longitude:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(17, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Latitude";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 378);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Lat/Long:";
            // 
            // featureBoundingBox
            // 
            this.featureBoundingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.featureBoundingBox.Location = new System.Drawing.Point(3, 320);
            this.featureBoundingBox.Name = "featureBoundingBox";
            this.featureBoundingBox.Size = new System.Drawing.Size(296, 33);
            this.featureBoundingBox.TabIndex = 9;
            this.featureBoundingBox.Text = "Set Extent to Feature BBox";
            this.featureBoundingBox.UseVisualStyleBackColor = true;
            this.featureBoundingBox.Click += new System.EventHandler(this.featureBoundingBox_Click);
            // 
            // featureIds
            // 
            this.featureIds.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.featureIds.FormattingEnabled = true;
            this.featureIds.Location = new System.Drawing.Point(130, 271);
            this.featureIds.Name = "featureIds";
            this.featureIds.Size = new System.Drawing.Size(150, 25);
            this.featureIds.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(20, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "FeatureID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 230);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Feature Bounding Box:";
            // 
            // layerBoundingBox
            // 
            this.layerBoundingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layerBoundingBox.Location = new System.Drawing.Point(3, 172);
            this.layerBoundingBox.Name = "layerBoundingBox";
            this.layerBoundingBox.Size = new System.Drawing.Size(296, 32);
            this.layerBoundingBox.TabIndex = 5;
            this.layerBoundingBox.Text = "Set Extent to Layer BBox";
            this.layerBoundingBox.UseVisualStyleBackColor = true;
            this.layerBoundingBox.Click += new System.EventHandler(this.layerBoundingBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(17, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Layer Bounding Box:";
            // 
            // ZoomToScale
            // 
            this.ZoomToScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZoomToScale.Location = new System.Drawing.Point(3, 94);
            this.ZoomToScale.Name = "ZoomToScale";
            this.ZoomToScale.Size = new System.Drawing.Size(296, 30);
            this.ZoomToScale.TabIndex = 3;
            this.ZoomToScale.Text = "Zoom To Scale";
            this.ZoomToScale.UseVisualStyleBackColor = true;
            this.ZoomToScale.Click += new System.EventHandler(this.ZoomToScale_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(14, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Scale:";
            // 
            // zoomScale
            // 
            this.zoomScale.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomScale.Location = new System.Drawing.Point(130, 61);
            this.zoomScale.Name = "zoomScale";
            this.zoomScale.Size = new System.Drawing.Size(150, 23);
            this.zoomScale.TabIndex = 1;
            this.zoomScale.Text = "1000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zoom To Scale:";
            // 
            // SetMapExtentSample
            // 
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "SetMapExtentSample";
            this.Size = new System.Drawing.Size(1255, 634);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #region Component Designer generated code
        #endregion Component Designer generated code

    }
}