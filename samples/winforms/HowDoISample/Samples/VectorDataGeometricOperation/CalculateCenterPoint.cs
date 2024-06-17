using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CalculateCenterPoint : UserControl
    {
        public CalculateCenterPoint()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a feature layer to hold the Census Housing data
            var censusHousingLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp")
            {
                FeatureSource =
                    {
                        // Project censusHousing layer to Spherical Mercator to match the map projection
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add a style to use to draw the censusHousing layer
            censusHousingLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            censusHousingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var censusHousingOverlay = new LayerOverlay();
            censusHousingOverlay.Layers.Add("CensusHousingLayer", censusHousingLayer);
            mapView.Overlays.Add("CensusHousingOverlay", censusHousingOverlay);

            // Create a layer to hold the centerPointLayer and Style it
            var centerPointLayer = new InMemoryFeatureLayer();
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.White, 4);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.Black, 2);
            centerPointLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var centerPointOverlay = new LayerOverlay();
            centerPointOverlay.Layers.Add("CenterPointLayer", centerPointLayer);
            mapView.Overlays.Add("CenterPointOverlay", centerPointOverlay);

            // Set the map extent to the censusHousing layer bounding box
            censusHousingLayer.Open();
            mapView.CurrentExtent = censusHousingLayer.GetBoundingBox();
            censusHousingLayer.Close();

            // Add LayerOverlay to Map
            centroidCenter.Checked = true;

            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Calculates the center point of a feature
        /// </summary>
        /// <param name="feature"> The target feature to calculate its center point</param>
        private async Task CalculateCenterPointAsync(Feature feature)
        {
            var centerPointOverlay = (LayerOverlay)mapView.Overlays["CenterPointOverlay"];
            var centerPointLayer = (InMemoryFeatureLayer)centerPointOverlay.Layers["CenterPointLayer"];

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
            await centerPointOverlay.RefreshAsync();
        }

        /// <summary>
        /// Map event that fires whenever the user clicks on the map. Gets the closest feature from the click event and calculates the center point
        /// </summary>
        private async void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var censusHousingOverlay = (LayerOverlay)mapView.Overlays["CensusHousingOverlay"];
            var censusHousingLayer = (ShapeFileFeatureLayer)censusHousingOverlay.Layers["CensusHousingLayer"];

            // Query the censusHousing layer to get the first feature closest to the map click event
            var feature = censusHousingLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            await CalculateCenterPointAsync(feature);
        }

        /// <summary>
        /// RadioButton checked event that will recalculate the center point so long as a feature was already selected
        /// </summary>
        private async void centroidCenter_CheckedChanged(object sender, EventArgs e)
        {
            var centerPointOverlay = (LayerOverlay)mapView.Overlays["CenterPointOverlay"];
            var centerPointLayer = (InMemoryFeatureLayer)centerPointOverlay.Layers["CenterPointLayer"];

            // Recalculate the center point if a feature has already been selected
            if (centerPointLayer.InternalFeatures.Contains("selectedFeature"))
            {
                await CalculateCenterPointAsync(centerPointLayer.InternalFeatures["selectedFeature"]);
            }
        }

        private async void bboxCenter_CheckedChanged(object sender, EventArgs e)
        {
            var centerPointOverlay = (LayerOverlay)mapView.Overlays["CenterPointOverlay"];
            var centerPointLayer = (InMemoryFeatureLayer)centerPointOverlay.Layers["CenterPointLayer"];

            // Recalculate the center point if a feature has already been selected
            if (centerPointLayer.InternalFeatures.Contains("selectedFeature"))
            {
                await CalculateCenterPointAsync(centerPointLayer.InternalFeatures["selectedFeature"]);
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
            // CalculateCenterPoint
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "CalculateCenterPoint";
            this.Size = new System.Drawing.Size(1067, 577);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}