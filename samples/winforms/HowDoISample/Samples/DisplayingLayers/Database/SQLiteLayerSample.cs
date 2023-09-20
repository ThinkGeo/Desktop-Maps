using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Diagnostics;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class SQLiteLayerSample: UserControl
    {
        public SQLiteLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay restuarantsOverlay = new LayerOverlay();
            mapView.Overlays.Add(restuarantsOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            SqliteFeatureLayer restaurantsLayer = new SqliteFeatureLayer(@"Data Source=../../../Data/SQLite/frisco-restaurants.sqlite;", "restaurants", "id", "geometry");
            restaurantsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to the overlay we created earlier.
            restuarantsOverlay.Layers.Add("Frisco Restaurants", restaurantsLayer);

            // Create a new text style and set various settings to make it look good.
            var textStyle = new TextStyle("Name", new GeoFont("Arial", 12), GeoBrushes.Black);
            textStyle.MaskType = MaskType.RoundedCorners;
            textStyle.OverlappingRule = LabelOverlappingRule.NoOverlapping;
            textStyle.Mask = new AreaStyle(GeoBrushes.WhiteSmoke);
            textStyle.SuppressPartialLabels = true;
            textStyle.YOffsetInPixel = -5;

            // Set a point style and the above text style to zoom level 1 and then apply it to all zoom levels up to 20.
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Green, new GeoPen(GeoColors.White, 2));
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the map view current extent to a bounding box that shows just a few restaurants.  
            mapView.CurrentExtent = new RectangleShape(-10776971.1234695, 3915454.06613793, -10775965.157585, 3914668.53918197);

            // Refresh the map.
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
            this.mapView.Size = new System.Drawing.Size(950, 575);
            this.mapView.TabIndex = 0;
            // 
            // SQLiteLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "SQLiteLayerSample";
            this.Size = new System.Drawing.Size(950, 575);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}