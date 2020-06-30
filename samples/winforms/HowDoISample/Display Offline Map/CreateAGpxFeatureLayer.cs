using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAGpxFeatureLayer : UserControl
    {
        private ProjectionConverter projectionConverter;

        public CreateAGpxFeatureLayer()
        {
            InitializeComponent();
            projectionConverter = new ProjectionConverter(4326, 3857);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.MapTools.Logo.IsEnabled = true;
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var gpsOverlay = new LayerOverlay();
            mapView.Overlays.Add("GPSOverlay", gpsOverlay);


            var dwgFiles = Directory.GetFiles(Path.Combine(SampleHelper.Root, "Gpx"));
            foreach (var dwgFile in dwgFiles)
            {
                sampleFileListBox.Items.Add(Path.GetFileName(dwgFile));
            }
            // Display the first file in the list
            sampleFileListBox.SelectedIndex = 0;
        }

        private void sampleFileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GpxFeatureLayer shapeLayer = new GpxFeatureLayer(SampleHelper.Get(sampleFileListBox.SelectedItem.ToString()));
            shapeLayer.FeatureSource.ProjectionConverter = projectionConverter;

            ValueStyle pointStyle = new ValueStyle();
            pointStyle.ColumnName = "IsWayPoint";
            pointStyle.ValueItems.Add(new ValueItem("0", PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Red, 4)));
            pointStyle.ValueItems.Add(new ValueItem("1", PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Green, 8)));
            LineStyle roadstyle = LineStyle.CreateSimpleLineStyle(GeoColors.Black, 1, true);

            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(pointStyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(roadstyle);
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            GpxFeatureLayer textLayer = new GpxFeatureLayer(SampleHelper.Get(sampleFileListBox.SelectedItem.ToString()));
            textLayer.FeatureSource.ProjectionConverter = projectionConverter;
            TextStyle labelStyle = TextStyle.CreateSimpleTextStyle("name", "Arial", 8, DrawingFontStyles.Bold, GeoColors.Black);
            labelStyle.TextPlacement = TextPlacement.Upper;
            labelStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            labelStyle.YOffsetInPixel = 8;

            textLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(labelStyle);
            textLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            shapeLayer.Open();
            mapView.CurrentExtent = (shapeLayer.GetBoundingBox());

            var gpsOverlay = (LayerOverlay)mapView.Overlays["GPSOverlay"];
            gpsOverlay.Layers.Clear();
            gpsOverlay.Layers.Add(shapeLayer);
            gpsOverlay.Layers.Add(textLayer);

            mapView.Refresh();
        }

        private Panel panel1;
        private ComboBox sampleFileListBox;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sampleFileListBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.UI.WinForms.MapResizeMode.PreserveScale;
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.DecimalDegree;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(662, 513);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.sampleFileListBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(518, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 73);
            this.panel1.TabIndex = 1;
            // 
            // sampleFileListBox
            // 
            this.sampleFileListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sampleFileListBox.FormattingEnabled = true;
            this.sampleFileListBox.Location = new System.Drawing.Point(6, 28);
            this.sampleFileListBox.Name = "sampleFileListBox";
            this.sampleFileListBox.Size = new System.Drawing.Size(121, 21);
            this.sampleFileListBox.TabIndex = 1;
            this.sampleFileListBox.SelectedIndexChanged += new System.EventHandler(this.sampleFileListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please select Gpx File:";
            // 
            // CreateAGpxFeatureLayer
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CreateAGpxFeatureLayer";
            this.Size = new System.Drawing.Size(662, 513);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code


    }
}