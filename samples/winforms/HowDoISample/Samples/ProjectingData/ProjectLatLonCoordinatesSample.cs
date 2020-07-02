using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ProjectLatLonCoordinatesSample: UserControl
    {
        public ProjectLatLonCoordinatesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map's unit of measurement to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create a new feature layer to display the shapes we will be reprojecting
            InMemoryFeatureLayer reprojectedFeaturesLayer = new InMemoryFeatureLayer();

            // Add a point, line, and polygon style to the layer. These styles control how the shapes will be drawn
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 6, false);
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(80, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
            reprojectedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to an overlay
            LayerOverlay reprojectedFeaturesOverlay = new LayerOverlay();
            reprojectedFeaturesOverlay.Layers.Add("Reprojected Features Layer", reprojectedFeaturesLayer);

            // Add the overlay to the map
            mapView.Overlays.Add("Reprojected Features Overlay", reprojectedFeaturesOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10779751.80, 3915369.33, -10779407.60, 3915141.57);
        }

        private Feature ReprojectFeature(Feature decimalDegreeFeature)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            Feature sphericalMercatorFeature = projectionConverter.ConvertToExternalProjection(decimalDegreeFeature);
            projectionConverter.Close();

            // Return the reprojected feature
            return sphericalMercatorFeature;
        }

        /// <summary>
        /// Use the ProjectionConverter to reproject multiple features
        /// </summary>
        private Collection<Feature> ReprojectMultipleFeatures(Collection<Feature> decimalDegreeFeatures)
        {
            // Create a new ProjectionConverter to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(4326, 3857);

            // Convert the feature to Spherical Mercator
            projectionConverter.Open();
            Collection<Feature> sphericalMercatorFeatures = projectionConverter.ConvertToExternalProjection(decimalDegreeFeatures);
            projectionConverter.Close();

            // Return the reprojected features
            return sphericalMercatorFeatures;
        }

        /// <summary>
        /// Draw reprojected features on the map
        /// </summary>
        private void ClearMapAndAddFeatures(Collection<Feature> reprojectedFeatures)
        {
            // Get the layer we prepared from the MapView
            InMemoryFeatureLayer reprojectedFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Reprojected Features Layer");

            // Clear old features from the feature layer and add the newly reprojected features
            reprojectedFeatureLayer.InternalFeatures.Clear();
            foreach (Feature sphericalMercatorFeature in reprojectedFeatures)
            {
                reprojectedFeatureLayer.InternalFeatures.Add(sphericalMercatorFeature);
            }

            // Set the map extent to zoom into the feature and refresh the map
            reprojectedFeatureLayer.Open();
            mapView.CurrentExtent = reprojectedFeatureLayer.GetBoundingBox();

            ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
            mapView.ZoomToScale(standardZoomLevelSet.ZoomLevel18.Scale);

            reprojectedFeatureLayer.Close();
            mapView.Refresh();
        }

        private Panel panel1;
        private Button reprojectAndDisplayMultipleFeatures;
        private TextBox txtWKT;
        private Button reprojectAndDisplayFeature;
        private Label label1;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectLatLonCoordinatesSample));
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.reprojectAndDisplayFeature = new System.Windows.Forms.Button();
            this.txtWKT = new System.Windows.Forms.TextBox();
            this.reprojectAndDisplayMultipleFeatures = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.Meter;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1244, 587);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.reprojectAndDisplayMultipleFeatures);
            this.panel1.Controls.Add(this.txtWKT);
            this.panel1.Controls.Add(this.reprojectAndDisplayFeature);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(943, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 587);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reproject Features and \r\nCoordinates";
            // 
            // reprojectAndDisplayFeature
            // 
            this.reprojectAndDisplayFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reprojectAndDisplayFeature.ForeColor = System.Drawing.Color.Black;
            this.reprojectAndDisplayFeature.Location = new System.Drawing.Point(3, 72);
            this.reprojectAndDisplayFeature.Name = "reprojectAndDisplayFeature";
            this.reprojectAndDisplayFeature.Size = new System.Drawing.Size(295, 32);
            this.reprojectAndDisplayFeature.TabIndex = 1;
            this.reprojectAndDisplayFeature.Text = "Reproject and Display a Feature";
            this.reprojectAndDisplayFeature.UseVisualStyleBackColor = true;
            this.reprojectAndDisplayFeature.Click += new System.EventHandler(this.reprojectAndDisplayFeature_Click);
            // 
            // txtWKT
            // 
            this.txtWKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWKT.Location = new System.Drawing.Point(3, 121);
            this.txtWKT.Multiline = true;
            this.txtWKT.Name = "txtWKT";
            this.txtWKT.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWKT.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtWKT.Size = new System.Drawing.Size(295, 264);
            this.txtWKT.TabIndex = 2;
            this.txtWKT.Text = resources.GetString("txtWKT.Text");
            // 
            // reprojectAndDisplayMultipleFeatures
            // 
            this.reprojectAndDisplayMultipleFeatures.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reprojectAndDisplayMultipleFeatures.ForeColor = System.Drawing.Color.Black;
            this.reprojectAndDisplayMultipleFeatures.Location = new System.Drawing.Point(3, 402);
            this.reprojectAndDisplayMultipleFeatures.Name = "reprojectAndDisplayMultipleFeatures";
            this.reprojectAndDisplayMultipleFeatures.Size = new System.Drawing.Size(295, 34);
            this.reprojectAndDisplayMultipleFeatures.TabIndex = 3;
            this.reprojectAndDisplayMultipleFeatures.Text = "Reproject And Display Multiple Features";
            this.reprojectAndDisplayMultipleFeatures.UseVisualStyleBackColor = true;
            this.reprojectAndDisplayMultipleFeatures.Click += new System.EventHandler(this.reprojectAndDisplayMultipleFeatures_Click);
            // 
            // ProjectLatLonCoordinatesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ProjectLatLonCoordinatesSample";
            this.Size = new System.Drawing.Size(1244, 587);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private void reprojectAndDisplayFeature_Click(object sender, EventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            Feature decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Convert the feature to Spherical Mercator
            Feature sphericalMercatorFeature = ReprojectFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            ClearMapAndAddFeatures(new Collection<Feature>() { sphericalMercatorFeature });
        }

        private void reprojectAndDisplayMultipleFeatures_Click(object sender, EventArgs e)
        {
            // Create features based on the WKT in the textbox in the UI
            Collection<Feature> decimalDegreeFeatures = new Collection<Feature>();
            string[] wktStrings = txtWKT.Text.Split('\n');
            foreach (string wktString in wktStrings)
            {
                try
                {
                    Feature wktFeature = new Feature(wktString);
                    decimalDegreeFeatures.Add(wktFeature);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // Convert the features to Spherical Mercator
            Collection<Feature> sphericalMercatorFeatures = ReprojectMultipleFeatures(decimalDegreeFeatures);

            // Add the reprojected features to the map
            ClearMapAndAddFeatures(sphericalMercatorFeatures);
        }
    }
}