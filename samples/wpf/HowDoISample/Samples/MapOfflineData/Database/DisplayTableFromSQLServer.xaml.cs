using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a SQLServer Layer on the map
    /// </summary>
    public partial class DisplayTableFromSQLServer : IDisposable
    {
        public DisplayTableFromSQLServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the SQLServer layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            MapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var coyoteSightingsOverlay = new LayerOverlay();
            MapView.Overlays.Add(coyoteSightingsOverlay);

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
            MapView.CurrentExtent = new RectangleShape(-10784283.099060204, 3918532.598821122, -10781699.527518518, 3916820.409397046);

            // Refresh the map.
            await MapView.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        #region Code for creating the sample data in SQL Server

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
    }
}