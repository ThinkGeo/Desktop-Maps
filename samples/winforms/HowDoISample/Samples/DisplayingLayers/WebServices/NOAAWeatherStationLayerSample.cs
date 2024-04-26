using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Diagnostics;
using System.Threading;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class NOAAWeatherStationLayerSample: UserControl
    {
        public delegate void InvokeDelegate();
        public NOAAWeatherStationLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the extent to a view of the US
            mapView.CurrentExtent = new RectangleShape(-14927495, 8262593, -6686622, 1827556);
            await mapView.RefreshAsync();

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay weatherOverlay = new LayerOverlay();
            weatherOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add("Weather", weatherOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 and our background is srid 3857 (spherical mercator).
            var noaaWeatherStationLayer = new NoaaWeatherStationFeatureLayer();
            noaaWeatherStationLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            // Create the weather stations style and add it on zoom level 1 and then apply it to all zoom levels up to 20.
            noaaWeatherStationLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            noaaWeatherStationLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the new layer to the overlay we created earlier
            weatherOverlay.Layers.Add(noaaWeatherStationLayer);
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
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1202, 611);
            this.mapView.TabIndex = 0;
            // 
            // NOAAWeatherStationLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "NOAAWeatherStationLayerSample";
            this.Size = new System.Drawing.Size(1202, 611);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}