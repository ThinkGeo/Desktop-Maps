﻿using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayTableFromSQLServer : UserControl
    {
        public DisplayTableFromSQLServer()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var coyoteSightingsOverlay = new LayerOverlay();
            mapView.Overlays.Add(coyoteSightingsOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 as our background is srid 3857 (spherical mercator).
            var coyoteSightingsLayer = new SqlServerFeatureLayer("Server=demodb.thinkgeo.com;Database=thinkgeo;User Id=ThinkGeoTest;Password=ThinkGeoTestPassword;", "frisco_coyote_sightings", "id")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };

            // Add the layer to the overlay we created earlier.
            coyoteSightingsOverlay.Layers.Add("Coyote Sightings", coyoteSightingsLayer);

            // Set a point style to zoom level 1 and then apply it to all zoom levels up to 20.
            coyoteSightingsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Black, new GeoPen(GeoColors.White, 1));
            coyoteSightingsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Set the map view current extent to a bounding box that shows just a few sightings.  
            mapView.CurrentExtent = new RectangleShape(-10784283.099060204, 3918532.598821122, -10781699.527518518, 3916820.409397046);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        #region Create sample data

        // ========================================================
        // Code for creating the sample data in SQL Server
        // ========================================================

        //Collection<FeatureSourceColumn> columns = new Collection<FeatureSourceColumn>();
        //columns.Add(new FeatureSourceColumn("comment", "varchar", 255));

        //SqlServerFeatureSource.CreateTable("Server={server};Database={db};User Id={username};Password={password};", "frisco_coyote_sightings", MsSqlSpatialDataType.Geometry, columns);

        //SqlServerFeatureSource target = new SqlServerFeatureSource("Server={server};Database={db};User Id={username};Password={password};", "frisco_coyote_sightings", "id");
        //target.Open();

        //ShapeFileFeatureSource source = new ShapeFileFeatureSource(@"./Data/Frisco_Coyote_Sightings.shp");
        //source.Open();

        //var sourceFeatures = source.GetAllFeatures(ReturningColumnsType.AllColumns);

        //target.BeginTransaction();

        //foreach (var feature in sourceFeatures)
        //{
        //    var dict = new Dictionary<string, string>();
        //    dict.Add("comment", feature.ColumnValues["Comments"].ToString().Replace('"', ' ').Replace("'", ""));

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
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1108, 644);
            this.mapView.TabIndex = 0;
            // 
            // DisplayTableFromSQLServer
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayTableFromSQLServer";
            this.Size = new System.Drawing.Size(1108, 644);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}