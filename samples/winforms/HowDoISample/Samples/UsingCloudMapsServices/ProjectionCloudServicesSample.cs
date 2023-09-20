using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Linq;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ProjectionCloudServicesSample: UserControl
    {
        private ProjectionCloudClient projectionCloudClient;

        public ProjectionCloudServicesSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
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
            mapView.CurrentExtent = new RectangleShape(-10798419.605087, 3934270.12359632, -10759021.6785336, 3896039.57306867);

            // Initialize the ProjectionCloudClient with our ThinkGeo Cloud credentials
            projectionCloudClient = new ProjectionCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");
            txtWKT.Text = "POINT(-96.834516 33.150083)\r\nLINESTRING(-96.83559 33.149, -96.835866046134 33.1508413556856, -96.835793626491 33.1508974965687, -96.8336008970734 33.1511063402186, -96.83356 33.15109, -96.83328 33.14922)\r\nPOLYGON((-96.83582 33.1508, -96.83578 33.15046, -96.83353 33.15068, -96.83358 33.15102, -96.83582 33.1508))";

            await mapView.RefreshAsync();
        }

        private async Task<Feature> ReprojectAFeature(Feature decimalDegreeFeature)
        {
            // Show a loading graphic to let users know the request is running
        //    loadingImage.Visibility = Visibility.Visible;

            Feature reprojectedFeature = await projectionCloudClient.ProjectAsync(decimalDegreeFeature, 4326, 3857);

            // Hide the loading graphic
        //    loadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeature;
        }

        /// <summary>
        /// Use the ProjectionCloudClient to reproject multiple features
        /// </summary>
        private async Task<Collection<Feature>> ReprojectMultipleFeatures(Collection<Feature> decimalDegreeFeatures)
        {
            // Show a loading graphic to let users know the request is running
          //  loadingImage.Visibility = Visibility.Visible;

            Collection<Feature> reprojectedFeatures = await projectionCloudClient.ProjectAsync(decimalDegreeFeatures, 4326, 3857);

            // Hide the loading graphic
           // loadingImage.Visibility = Visibility.Hidden;

            return reprojectedFeatures;
        }

        private async Task ClearMapAndAddFeaturesAsync(Collection<Feature> features)
        {
            // Get the layer we prepared from the MapView
            InMemoryFeatureLayer reprojectedFeatureLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("Reprojected Features Layer");

            // Clear old features from the feature layer and add the newly reprojected features
            reprojectedFeatureLayer.InternalFeatures.Clear();

            foreach (Feature sphericalMercatorFeature in features)
            {
                reprojectedFeatureLayer.InternalFeatures.Add(sphericalMercatorFeature);
            }

            // Set the map extent to zoom into the feature and refresh the map
            reprojectedFeatureLayer.Open();
            mapView.CurrentExtent = reprojectedFeatureLayer.GetBoundingBox();

            ZoomLevelSet standardZoomLevelSet = new ZoomLevelSet();
            await mapView.ZoomToScaleAsync(standardZoomLevelSet.ZoomLevel18.Scale);

            reprojectedFeatureLayer.Close();
            await mapView.RefreshAsync();
        }


        private async void btnReprojectFeature_Click(object sender, EventArgs e)
        {
            // Create a feature with coordinates in Decimal Degrees (4326)
            Feature decimalDegreeFeature = new Feature(-96.834516, 33.150083);

            // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            Feature sphericalMercatorFeature = await ReprojectAFeature(decimalDegreeFeature);

            // Add the reprojected features to the map
            await ClearMapAndAddFeaturesAsync(new Collection<Feature>() { sphericalMercatorFeature });
        }

        private async void btnReprojectFeatures_Click(object sender, EventArgs e)
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

            // Use the ProjectionCloudClient to convert between Decimal Degrees (4326) and Spherical Mercator (3857)
            Collection<Feature> sphericalMercatorFeatures = await ReprojectMultipleFeatures(decimalDegreeFeatures);

            // Add the reprojected features to the map
            await ClearMapAndAddFeaturesAsync(sphericalMercatorFeatures);
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button btnReprojectFeatures;
        private TextBox txtWKT;
        private Button btnReprojectFeature;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReprojectFeatures = new System.Windows.Forms.Button();
            this.txtWKT = new System.Windows.Forms.TextBox();
            this.btnReprojectFeature = new System.Windows.Forms.Button();
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
            this.mapView.Size = new System.Drawing.Size(793, 596);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.btnReprojectFeatures);
            this.panel1.Controls.Add(this.txtWKT);
            this.panel1.Controls.Add(this.btnReprojectFeature);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(799, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 596);
            this.panel1.TabIndex = 1;
            // 
            // btnReprojectFeatures
            // 
            this.btnReprojectFeatures.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnReprojectFeatures.Location = new System.Drawing.Point(4, 398);
            this.btnReprojectFeatures.Name = "btnReprojectFeatures";
            this.btnReprojectFeatures.Size = new System.Drawing.Size(331, 33);
            this.btnReprojectFeatures.TabIndex = 3;
            this.btnReprojectFeatures.Text = "Reproject and Display Multiple Features";
            this.btnReprojectFeatures.UseVisualStyleBackColor = true;
            this.btnReprojectFeatures.Click += new System.EventHandler(this.btnReprojectFeatures_Click);
            // 
            // txtWKT
            // 
            this.txtWKT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtWKT.Location = new System.Drawing.Point(4, 111);
            this.txtWKT.Multiline = true;
            this.txtWKT.Name = "txtWKT";
            this.txtWKT.Size = new System.Drawing.Size(328, 281);
            this.txtWKT.TabIndex = 2;
            // 
            // btnReprojectFeature
            // 
            this.btnReprojectFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnReprojectFeature.Location = new System.Drawing.Point(3, 74);
            this.btnReprojectFeature.Name = "btnReprojectFeature";
            this.btnReprojectFeature.Size = new System.Drawing.Size(332, 31);
            this.btnReprojectFeature.TabIndex = 1;
            this.btnReprojectFeature.Text = "Reproject and Display a Feature";
            this.btnReprojectFeature.UseVisualStyleBackColor = true;
            this.btnReprojectFeature.Click += new System.EventHandler(this.btnReprojectFeature_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Using Cloud Projection Services";
            // 
            // ProjectionCloudServicesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ProjectionCloudServicesSample";
            this.Size = new System.Drawing.Size(1134, 596);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}