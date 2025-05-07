using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Learn how to automatically reproject a raster layer using the ProjectionConverter class
    /// </summary>
    public class ProjectARaster : UserControl
    {
        public ProjectARaster()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map
        /// </summary>
        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the Map Unit to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we can add layers to, and add it to the MapView
            var layerOverlay = new LayerOverlay();
            mapView.Overlays.Add(layerOverlay);

            // Reproject a raster layer and set the extent
            await ReprojectRasterLayerAsync(layerOverlay);
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject a raster layer
        /// </summary>
        private async Task ReprojectRasterLayerAsync(LayerOverlay layerOverlay)
        {
            var worldRasterLayer = new GeoTiffRasterLayer(@"./Data/GeoTiff/World.tif");

            // Create a new ProjectionConverter to convert between World Geodetic System (4326) and US National Atlas Equal Area (2163)
            ProjectionConverter projectionConverter = new GdalProjectionConverter(4326, 2163);
            worldRasterLayer.ImageSource.ProjectionConverter = projectionConverter;

            layerOverlay.Layers.Clear();
            layerOverlay.Layers.Add("World", worldRasterLayer);

            // Set the map to the extent of the raster layer and refresh the map
            worldRasterLayer.Open();
            mapView.CurrentExtent = worldRasterLayer.GetBoundingBox();
            worldRasterLayer.Close();
            await mapView.RefreshAsync();
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1212, 647);
            this.mapView.TabIndex = 0;
            // 
            // ProjectARaster
            // 
            this.Controls.Add(this.mapView);
            this.Name = "ProjectARaster";
            this.Size = new System.Drawing.Size(1212, 647);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}