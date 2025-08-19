using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// Demonstrates how to cluster points using a ClusterPointStyle.
    /// </summary>
    public class DisplayClusterPoints : UserControl
    {
        private readonly ShapeFileFeatureLayer coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp");

        public DisplayClusterPoints()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes the map and overlays when the Form is loaded.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add ThinkGeo Cloud Vector Maps as the background overlay.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load coyote sightings shapefile (projected to match the map projection).
            var coyoteSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Coyote_Sightings.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(coyoteSightings);
            mapView.Overlays.Add(layerOverlay);

            // Apply Cluster Point Style
            AddClusterPointStyle(coyoteSightings);

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10780320, 3915120);
            mapView.CurrentScale = 288900;

            _ = mapView.RefreshAsync();
        }

        /// <summary>
        /// Creates and applies a cluster style to the given layer.
        /// </summary>
        private void AddClusterPointStyle(ShapeFileFeatureLayer layer)
        {
            // Setup the un-clustered point style 
            var unclusteredPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.Orange, 10);

            // Setup the clustered point style (paw icon).
            var clusteredPointStyle = new PointStyle(new GeoImage(@"../../../Resources/coyote_paw.png"))
            {
                ImageScale = .25,
                Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                MaskType = MaskType.RoundedCorners
            };

            // Create a text style that will display the number of features within a clustered point
            var textStyle = new TextStyle("FeatureCount", new GeoFont("Segoe UI", 14, DrawingFontStyles.Bold), GeoBrushes.DimGray)
            {
                HaloPen = new GeoPen(GeoBrushes.White, 1),
                YOffsetInPixel = 20
            };

            // Cluster style definition.
            var clusterPointStyle = new ClusterPointStyle(unclusteredPointStyle, textStyle)
            {
                MinimumFeaturesPerCellToCluster = 2,
                ClusteredPointStyle = clusteredPointStyle
            };

            // Apply cluster style across all zoom levels.
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(clusterPointStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1182, 553);
            this.mapView.TabIndex = 0;
            // 
            // DisplayClusterPoints
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayClusterPoints";
            this.Size = new System.Drawing.Size(1182, 553);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}