using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class PostgresLayerSample: UserControl
    {
        public PostgresLayerSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay countriesOverlay = new LayerOverlay();
            mapView.Overlays.Add(countriesOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            PostgreSqlFeatureLayer countriesLayer = new PostgreSqlFeatureLayer("User ID=ThinkGeoTest;Password=ThinkGeoTestPassword;Host=demodb.thinkgeo.com;Port=5432;Database=postgres;Pooling=true;", "countries", "gid", 4326);
            countriesLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            // Add the layer to the overlay we created earlier.
            countriesOverlay.Layers.Add("Countries", countriesLayer);

            // Set a point style to zoom level 1 and then apply it to all zoom levels up to 20.
            countriesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoBrushes.Black, 2), new GeoSolidBrush(GeoColors.Transparent));
            countriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the map view current extent to a bounding box that shows just a few sightings.  
            mapView.CurrentExtent = new RectangleShape(-14910723.898808584, 6701313.058217316, -7200979.520684462, 2773061.32240915);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        #region Create sample data
        // ========================================================
        // Code for creating the sample data in PostgreSql
        // ========================================================

        //Collection<FeatureSourceColumn> columns = new Collection<FeatureSourceColumn>();
        //columns.Add(new FeatureSourceColumn("comment", "varchar", 255));

        //PostgreSqlFeatureSource target = new PostgreSqlFeatureSource("User ID={username};Password={password};Host={server};Port=5432;Database={db};Pooling=true;", "frisco_coyote_sightings", "ID", 2276);
        //target.Open();

        //ShapeFileFeatureSource source = new ShapeFileFeatureSource(@"./Data/Frisco_Coyote_Sightings.shp");
        //source.Open();

        //var sourceFeatures = source.GetAllFeatures(ReturningColumnsType.AllColumns);

        //target.BeginTransaction();

        //foreach (var feature in sourceFeatures)
        //{
        //    var dict = new Dictionary<string, string>();
        //    dict.Add("comment", feature.ColumnValues["Comments"].ToString().Replace('"', ' ').Replace("'", ""));
        //    dict.Add("id", feature.ColumnValues["OBJECTID"]);
        //    var newFeature = new Feature(feature.GetWellKnownBinary(), feature.ColumnValues["OBJECTID"], dict);

        //    target.AddFeature(newFeature);
        //}

        //var results = target.CommitTransaction();
        //target.Close();

        //target.Open();
        //var features = target.GetAllFeatures(ReturningColumnsType.AllColumns);
        //target.Close();        
        #endregion

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
            this.mapView.Size = new System.Drawing.Size(981, 575);
            this.mapView.TabIndex = 0;
            // 
            // PostgresLayerSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "PostgresLayerSample";
            this.Size = new System.Drawing.Size(981, 575);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}