using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a table from a database: SQLite, GeoPackage, an Esri File Geodatabase, PostgreSQL or SQL Server.
    /// </summary>
    public partial class Databases : IDisposable
    {
        private bool _initialized;
        private int _request;
        private LayerOverlay _dataOverlay;

        public Databases()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));
            _dataOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            Map.Overlays.Add(_dataOverlay);
            await ShowChosenAsync();
        }

        private async void Database_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            _dataOverlay.Layers.Clear();
            Status.Text = "Connecting...";

            try
            {
                var layer = CreateChosenLayer();
                // A database on a server can be slow or down, so the connection is made off the UI thread.
                var extent = await Task.Run(() =>
                {
                    layer.Open();
                    return layer.GetBoundingBox();
                });
                if (request != _request) return;

                _dataOverlay.Layers.Add(layer);
                extent.ScaleUp(20);
                Map.CurrentExtent = extent;
                Status.Text = layer.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;
                Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private FeatureLayer CreateChosenLayer()
        {
            if (GeoPackage.IsChecked == true) return CreateGeoPackageLayer();
            if (FileGeoDatabase.IsChecked == true) return CreateFileGeoDatabaseLayer();
            if (Postgres.IsChecked == true) return CreatePostgresLayer();
            if (SqlServer.IsChecked == true) return CreateSqlServerLayer();
            return CreateSqliteLayer();
        }

        private static FeatureLayer CreateSqliteLayer()
        {
            var connectionString = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "Data", "SQLite", "frisco-restaurants.sqlite") + ";";
            var restaurantsLayer = new SqliteFeatureLayer(connectionString, "restaurants", "id", "geometry");
            restaurantsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Green, new GeoPen(GeoColors.White, 2));
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Name", new GeoFont("Noto Sans", 12), GeoBrushes.Black)
            {
                MaskType = MaskType.RoundedCorners,
                OverlappingRule = LabelOverlappingRule.NoOverlapping,
                Mask = new AreaStyle(GeoBrushes.WhiteSmoke),
                SuppressPartialLabels = true,
                YOffsetInPixel = -5
            };
            restaurantsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return restaurantsLayer;
        }

        private static FeatureLayer CreateGeoPackageLayer()
        {
            // A GeoPackage is a SQLite database that GDAL reads; this one is in UTM zone 10N (EPSG 26910).
            var geologyLayer = new GdalFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoPackage", "mora_surficial_geology.gpkg"));
            geologyLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(26910, 3857);
            geologyLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.LightRed, 4);
            geologyLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(128, GeoColors.LightSteelBlue), 2F, GeoColors.Black, 2F, false);
            geologyLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(64, GeoColors.LightGreen), GeoColors.Black, 1);
            geologyLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return geologyLayer;
        }

        private static FeatureLayer CreateFileGeoDatabaseLayer()
        {
            var zoningLayer = new FileGeoDatabaseFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "FileGeoDatabase", "zoning.gdb"))
            {
                ActiveLayer = "zoning"
            };
            zoningLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Black, new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)));
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return zoningLayer;
        }

        private static FeatureLayer CreatePostgresLayer()
        {
            var countriesLayer = new PostgreSqlFeatureLayer("User ID=ThinkGeoTest;Password=ThinkGeoTestPassword;Host=demodb.thinkgeo.com;Port=5432;Database=postgres;Pooling=true;", "countries", "gid", 4326);
            countriesLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            countriesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(GeoBrushes.Black, 2), new GeoSolidBrush(GeoColors.Transparent));
            countriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return countriesLayer;
        }

        private static FeatureLayer CreateSqlServerLayer()
        {
            var sightingsLayer = new SqlServerFeatureLayer("Server=demodb.thinkgeo.com;Database=thinkgeo;User Id=ThinkGeoTest;Password=ThinkGeoTestPassword;TrustServerCertificate=True;", "frisco_coyote_sightings", "id");
            sightingsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            sightingsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Black, new GeoPen(GeoColors.White, 1));
            sightingsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return sightingsLayer;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
