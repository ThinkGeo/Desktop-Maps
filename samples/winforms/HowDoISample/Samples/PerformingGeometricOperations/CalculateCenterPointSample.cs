using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Linq;
using System.Windows;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CalculateCenterPointSample: UserControl 
    {
        public CalculateCenterPointSample()
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

            ShapeFileFeatureLayer censusHousing = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
            InMemoryFeatureLayer centerPointLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project censusHousing layer to Spherical Mercator to match the map projection
            censusHousing.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style censusHousing layer
            censusHousing.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            censusHousing.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style centerPointLayer
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.White, 4);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.Black, 2);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add censusHousing layer to a LayerOverlay
            layerOverlay.Layers.Add("censusHousing", censusHousing);

            // Add centerPointLayer to the layerOverlay
            layerOverlay.Layers.Add("centerPointLayer", centerPointLayer);

            // Set the map extent to the censusHousing layer bounding box
            censusHousing.Open();
            mapView.CurrentExtent = censusHousing.GetBoundingBox();
            censusHousing.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);
        }

        /// <summary>
        /// Calculates the center point of a feature
        /// </summary>
        /// <param name="feature"> The target feature to calculate it's center point</param>
        private void CalculateCenterPoint(Feature feature)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer centerPointLayer = (InMemoryFeatureLayer)layerOverlay.Layers["centerPointLayer"];

            PointShape centerPoint;

            // Get the CenterPoint of the selected feature
            if ((bool)centroidCenter.Checked)
            {
                // Centroid, or geometric center, method. Accurate, but can be relatively slower on extremely complex shapes
                centerPoint = feature.GetShape().GetCenterPoint();
            }
            else
            {
                // BoundingBox method. Less accurate, but much faster
                centerPoint = feature.GetBoundingBox().GetCenterPoint();
            }

            // Show the centerPoint on the map
            centerPointLayer.InternalFeatures.Clear();
            centerPointLayer.InternalFeatures.Add("selectedFeature", feature);
            centerPointLayer.InternalFeatures.Add("centerPoint", new Feature(centerPoint));

            // Refresh the overlay to show the results
            layerOverlay.Refresh();

        }

        /// <summary>
        /// RadioButton checked event that will recalculate the center point so long as a feature was already selected
        /// </summary>
        private void centroidCenter_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer centerPointLayer = (InMemoryFeatureLayer)layerOverlay.Layers["centerPointLayer"];

            // Recalculate the center point if a feature has already been selected
            if (centerPointLayer.InternalFeatures.Contains("selectedFeature"))
            {
                CalculateCenterPoint(centerPointLayer.InternalFeatures["selectedFeature"]);
            }
        }

        /// <summary>
        /// Map event that fires whenever the user clicks on the map. Gets the closest feature from the click event and calculates the center point
        /// </summary>
        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            ShapeFileFeatureLayer censusHousing = (ShapeFileFeatureLayer)layerOverlay.Layers["censusHousing"];

            // Query the censusHousing layer to get the first feature closest to the map click event
            var feature = censusHousing.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            CalculateCenterPoint(feature);
        }

        private void bboxCenter_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];
            InMemoryFeatureLayer centerPointLayer = (InMemoryFeatureLayer)layerOverlay.Layers["centerPointLayer"];

            // Recalculate the center point if a feature has already been selected
            if (centerPointLayer.InternalFeatures.Contains("selectedFeature"))
            {
                CalculateCenterPoint(centerPointLayer.InternalFeatures["selectedFeature"]);
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private RadioButton bboxCenter;
        private RadioButton centroidCenter;
        private Label label2;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bboxCenter = new System.Windows.Forms.RadioButton();
            this.centroidCenter = new System.Windows.Forms.RadioButton();
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
            this.mapView.Size = new System.Drawing.Size(810, 577);
            this.mapView.TabIndex = 0;
            this.mapView.MapClick += new System.EventHandler<ThinkGeo.Core.MapClickMapViewEventArgs>(this.mapView_MapClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.bboxCenter);
            this.panel1.Controls.Add(this.centroidCenter);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(809, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 577);
            this.panel1.TabIndex = 1;
            // 
            // bboxCenter
            // 
            this.bboxCenter.AutoSize = true;
            this.bboxCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bboxCenter.ForeColor = System.Drawing.Color.White;
            this.bboxCenter.Location = new System.Drawing.Point(11, 132);
            this.bboxCenter.Name = "bboxCenter";
            this.bboxCenter.Size = new System.Drawing.Size(235, 24);
            this.bboxCenter.TabIndex = 3;
            this.bboxCenter.Text = "Show Bounding Box Center";
            this.bboxCenter.UseVisualStyleBackColor = true;
            this.bboxCenter.CheckedChanged += new System.EventHandler(this.bboxCenter_CheckedChanged);
            // 
            // centroidCenter
            // 
            this.centroidCenter.AutoSize = true;
            this.centroidCenter.Checked = true;
            this.centroidCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.centroidCenter.ForeColor = System.Drawing.Color.White;
            this.centroidCenter.Location = new System.Drawing.Point(11, 101);
            this.centroidCenter.Name = "centroidCenter";
            this.centroidCenter.Size = new System.Drawing.Size(194, 24);
            this.centroidCenter.TabIndex = 2;
            this.centroidCenter.TabStop = true;
            this.centroidCenter.Text = "Show Centroid Center";
            this.centroidCenter.UseVisualStyleBackColor = true;
            this.centroidCenter.CheckedChanged += new System.EventHandler(this.centroidCenter_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(223, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Click on a point to display its\r\ncenter point.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Center Point Controls:";
            // 
            // CalculateCenterPointSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CalculateCenterPointSample";
            this.Size = new System.Drawing.Size(1067, 577);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}