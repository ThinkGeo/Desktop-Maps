using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GroupingLayersUsingLayerOverlay : UserControl
    {
        public GroupingLayersUsingLayerOverlay()
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

            /**********************
             * Landuse LayerOverlay
             **********************/

            // Create cityLimits layer
            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.DimGray, 2);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var poiOverlay = new LayerOverlay();
            var landuseOverlay = new LayerOverlay();

            // Add cityLimits layer to the landuseGroup overlay
            landuseOverlay.Layers.Add(cityLimits);

            // Create Parks landuse layer
            var parks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Style Parks landuse layer
            parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.Green), GeoColors.Transparent);
            parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project Parks landuse layer to Spherical Mercator to match the map projection
            parks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Parks landuse layer to the landuseGroup overlay
            landuseOverlay.Layers.Add(parks);

            // Add Landuse overlay to the map
            mapView.Overlays.Add("landuseOverlay", landuseOverlay);

            /******************
             * POI LayerOverlay
             ******************/

            // Create Hotel POI layer
            var hotels = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

            // Style Hotel POI layer
            hotels.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.White, 2);
            hotels.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project Hotels POI layer to Spherical Mercator to match the map projection
            hotels.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add Hotel POI layer to the poiGroup overlay
            poiOverlay.Layers.Add(hotels);

            // Create School POI layer
            var schools = new ShapeFileFeatureLayer(@"./Data/Shapefile/Schools.shp");

            // Style School POI layer
            schools.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleSquareStyle(GeoColors.Red, 8, GeoColors.White, 2);
            schools.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project Schools POI layer to Spherical Mercator to match the map projection
            schools.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add School POI layer to the poiGroup overlay
            poiOverlay.Layers.Add(schools);

            // Add POI overlay to the map
            mapView.Overlays.Add("poiOverlay", poiOverlay);

            // Set the map extent
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            ShowPoi.Checked = true;
            ShowLandUse.Checked = true;

            await mapView.RefreshAsync();
        }

        private void ShowPoi_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkbox && checkbox.Checked)
            {
                var poiOverlay = (LayerOverlay)mapView.Overlays["poiOverlay"];
                poiOverlay.IsVisible = true;
            }
            else
            {
                var poiOverlay = (LayerOverlay)mapView.Overlays["poiOverlay"];
                poiOverlay.IsVisible = false;
            }
        }

        private void ShowLandUse_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkbox && checkbox.Checked)
            {
                var landuseOverlay = (LayerOverlay)mapView.Overlays["landuseOverlay"];
                landuseOverlay.IsVisible = true;
            }
            else
            {
                var landuseOverlay = (LayerOverlay)mapView.Overlays["landuseOverlay"];
                landuseOverlay.IsVisible = false;
            }
        }

        #region Component Designer generated code
        private Panel panel1;
        private CheckBox ShowLandUse;
        private CheckBox ShowPoi;
        private Label label1;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowPoi = new System.Windows.Forms.CheckBox();
            this.ShowLandUse = new System.Windows.Forms.CheckBox();
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
            this.mapView.Size = new System.Drawing.Size(842, 611);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.ShowLandUse);
            this.panel1.Controls.Add(this.ShowPoi);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(848, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 611);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "LayerOverlay Controls:";
            // 
            // ShowPoi
            // 
            this.ShowPoi.AutoSize = true;
            this.ShowPoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ShowPoi.ForeColor = System.Drawing.Color.White;
            this.ShowPoi.Location = new System.Drawing.Point(23, 51);
            this.ShowPoi.Name = "ShowPoi";
            this.ShowPoi.Size = new System.Drawing.Size(156, 24);
            this.ShowPoi.TabIndex = 1;
            this.ShowPoi.Text = "Show POI Group";
            this.ShowPoi.UseVisualStyleBackColor = true;
            this.ShowPoi.CheckedChanged += new System.EventHandler(this.ShowPoi_CheckedChanged);
            // 
            // ShowLandUse
            // 
            this.ShowLandUse.AutoSize = true;
            this.ShowLandUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ShowLandUse.ForeColor = System.Drawing.Color.White;
            this.ShowLandUse.Location = new System.Drawing.Point(23, 78);
            this.ShowLandUse.Name = "ShowLandUse";
            this.ShowLandUse.Size = new System.Drawing.Size(190, 24);
            this.ShowLandUse.TabIndex = 2;
            this.ShowLandUse.Text = "Show and Use Group";
            this.ShowLandUse.UseVisualStyleBackColor = true;
            this.ShowLandUse.CheckedChanged += new System.EventHandler(this.ShowLandUse_CheckedChanged);
            // 
            // GroupingLayersUsingLayerOverlay
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GroupingLayersUsingLayerOverlay";
            this.Size = new System.Drawing.Size(1135, 611);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}