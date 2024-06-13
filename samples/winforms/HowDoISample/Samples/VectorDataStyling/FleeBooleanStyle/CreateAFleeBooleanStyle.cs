using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateAFleeBooleanStyle : UserControl
    {

        public CreateAFleeBooleanStyle()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a layer with polygon data
            ShapeFileFeatureLayer countries02Layer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");

            // Project the layer's data to match the projection of the map
            countries02Layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Add the layer to a layer overlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(countries02Layer);

            // Add the overlay to the map
            mapView.Overlays.Add(layerOverlay);

            // Add the fleeBooleanStyle to the countries02 layer
            AddFleeBooleanStyle(countries02Layer);

            // Set the map extent
            mapView.CurrentExtent = MaxExtents.SphericalMercator;
            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Create a fleeBooleanStyle and add it to the countries02 layer
        /// </summary>
        private void AddFleeBooleanStyle(ShapeFileFeatureLayer layer)
        {
            // Highlight the countries that are land locked and have a population greater than 10 million  
            string expression = "(POP_CNTRY>10000000 && LANDLOCKED=='Y')";
            FleeBooleanStyle landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this  
            // to access the Convert.Toxxx methods to convert variable types  
            landLockedCountryStyle.StaticTypes.Add(typeof(System.Convert));

            // The math class might be handy to include but in this sample we do not use it  
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));  

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1), new GeoSolidBrush(GeoColor.FromArgb(205,255,255,0))));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(205, 233, 232, 214), GeoColor.FromArgb(205, 118, 138, 69)));

            // Add the landLockedCountryStyle to the collection of custom styles for ZoomLevel 1. 
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);

            // Apply the styles for ZoomLevel 1 down to ZoomLevel 20. This effectively applies the area style on every zoom level on the map. 
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
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1281, 643);
            this.mapView.TabIndex = 0;
            // 
            // CreateAFleeBooleanStyle
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateAFleeBooleanStyle";
            this.Size = new System.Drawing.Size(1281, 643);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}