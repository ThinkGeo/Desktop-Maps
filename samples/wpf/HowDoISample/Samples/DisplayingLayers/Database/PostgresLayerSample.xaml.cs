using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a Postgres Layer on the map
    /// </summary>
    public partial class PostgresLayerSample : UserControl, IDisposable
    {
        public PostgresLayerSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the PostgreSql layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay countriesOverlay = new LayerOverlay();
            mapView.Overlays.Add(countriesOverlay);

            // Create the new layer and set the projection as the data is in srid 4326 as our background is srid 3857 (spherical mercator).
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
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
