using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class TranslateShapeSample: UserControl 
    {
        public TranslateShapeSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;
            ShapeFileFeatureLayer.BuildIndexFile(@"../../../Data/Shapefile/FriscoCityLimits.shp");

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/FriscoCityLimits.shp");
            InMemoryFeatureLayer translatedLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style translatedLayer
            translatedLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            translatedLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits layer to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add translatedLayer to the layerOverlay
            layerOverlay.Layers.Add("translatedLayer", translatedLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);
        }

        private void offsetTranslateShape_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            InMemoryFeatureLayer translatedLayer = (InMemoryFeatureLayer)layerOverlay.Layers["translatedLayer"];

            // Query the cityLimits layer to get all the features
            cityLimits.Open();
            var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            cityLimits.Close();

            // Translate the first feature's shape by the X and Y values on the UI in meters
            var translate = AreaBaseShape.TranslateByOffset(features[0].GetShape(), Convert.ToDouble(translateX.Text), Convert.ToDouble(translateY.Text), GeographyUnit.Meter, DistanceUnit.Meter);

            // Add the translated shape into translatedLayer to display the result.
            // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the
            // underlying data using BeginTransaction and CommitTransaction instead.
            translatedLayer.InternalFeatures.Clear();
            translatedLayer.InternalFeatures.Add(new Feature(translate));

            // Redraw the layerOverlay to see the translated feature on the map
            layerOverlay.Refresh();
        }

        private void degreeTranslateShape_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            InMemoryFeatureLayer translatedLayer = (InMemoryFeatureLayer)layerOverlay.Layers["translatedLayer"];

            // Query the cityLimits layer to get all the features
            cityLimits.Open();
            var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            cityLimits.Close();

            // Translate the first feature's shape by the X and Y values on the UI in meters
            var translate = AreaBaseShape.TranslateByDegree(features[0].GetShape(), Convert.ToDouble(translateDistance.Text), Convert.ToDouble(translateAngle.Text), GeographyUnit.Meter, DistanceUnit.Meter);

            // Add the translated shape into translatedLayer to display the result.
            // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the
            // underlying data using BeginTransaction and CommitTransaction instead.
            translatedLayer.InternalFeatures.Clear();
            translatedLayer.InternalFeatures.Add(new Feature(translate));

            // Redraw the layerOverlay to see the translated feature on the map
            layerOverlay.Refresh();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button degreeTranslateShape;
        private TextBox translateDistance;
        private TextBox translateAngle;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button offsetTranslateShape;
        private TextBox translateY;
        private TextBox translateX;
        private Label label3;
        private Label label2;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.degreeTranslateShape = new System.Windows.Forms.Button();
            this.translateDistance = new System.Windows.Forms.TextBox();
            this.translateAngle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.offsetTranslateShape = new System.Windows.Forms.Button();
            this.translateY = new System.Windows.Forms.TextBox();
            this.translateX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(811, 607);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.degreeTranslateShape);
            this.panel1.Controls.Add(this.translateDistance);
            this.panel1.Controls.Add(this.translateAngle);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.offsetTranslateShape);
            this.panel1.Controls.Add(this.translateY);
            this.panel1.Controls.Add(this.translateX);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(814, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 607);
            this.panel1.TabIndex = 1;
            // 
            // degreeTranslateShape
            // 
            this.degreeTranslateShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.degreeTranslateShape.Location = new System.Drawing.Point(3, 320);
            this.degreeTranslateShape.Name = "degreeTranslateShape";
            this.degreeTranslateShape.Size = new System.Drawing.Size(293, 35);
            this.degreeTranslateShape.TabIndex = 11;
            this.degreeTranslateShape.Text = "Translate";
            this.degreeTranslateShape.UseVisualStyleBackColor = true;
            this.degreeTranslateShape.Click += new System.EventHandler(this.degreeTranslateShape_Click);
            // 
            // translateDistance
            // 
            this.translateDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translateDistance.Location = new System.Drawing.Point(192, 269);
            this.translateDistance.Name = "translateDistance";
            this.translateDistance.Size = new System.Drawing.Size(100, 27);
            this.translateDistance.TabIndex = 10;
            this.translateDistance.Text = "1000";
            // 
            // translateAngle
            // 
            this.translateAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translateAngle.Location = new System.Drawing.Point(192, 233);
            this.translateAngle.Name = "translateAngle";
            this.translateAngle.Size = new System.Drawing.Size(100, 27);
            this.translateAngle.TabIndex = 9;
            this.translateAngle.Text = "120";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(19, 269);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Distance (m):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(19, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Angle:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(16, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Translate By Degree Angle:";
            // 
            // offsetTranslateShape
            // 
            this.offsetTranslateShape.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offsetTranslateShape.Location = new System.Drawing.Point(3, 143);
            this.offsetTranslateShape.Name = "offsetTranslateShape";
            this.offsetTranslateShape.Size = new System.Drawing.Size(293, 32);
            this.offsetTranslateShape.TabIndex = 5;
            this.offsetTranslateShape.Text = "Translate";
            this.offsetTranslateShape.UseVisualStyleBackColor = true;
            this.offsetTranslateShape.Click += new System.EventHandler(this.offsetTranslateShape_Click);
            // 
            // translateY
            // 
            this.translateY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translateY.Location = new System.Drawing.Point(192, 97);
            this.translateY.Name = "translateY";
            this.translateY.Size = new System.Drawing.Size(100, 27);
            this.translateY.TabIndex = 4;
            this.translateY.Text = "1000";
            // 
            // translateX
            // 
            this.translateX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translateX.Location = new System.Drawing.Point(192, 64);
            this.translateX.Name = "translateX";
            this.translateX.Size = new System.Drawing.Size(100, 27);
            this.translateX.TabIndex = 3;
            this.translateX.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Translate Y (m):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Translate X (m):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Translate By Offset:";
            // 
            // TranslateShapeSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "TranslateShapeSample";
            this.Size = new System.Drawing.Size(1113, 607);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}