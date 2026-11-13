using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Open a table from a database - SQLite, a GeoPackage, an Esri file geodatabase,
    /// PostgreSQL / PostGIS or SQL Server - and draw it. The connection is one line;
    /// everything after it is the same for all of them, because a FeatureSource is a
    /// FeatureSource.
    /// </summary>
    public partial class Databases
    {
        // The basemap under the data. One source for the life of the sample: a
        // restyle keeps the sources it recognizes, so the map below holds still
        // while the layer above it changes table.
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };

        private FeatureSourceVectorTileSource _current;
        private int _request;
        private bool _ready;

        public Databases()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_ready || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _ready = true;
            await ShowChosenAsync();
        }

        private async void Database_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        /// <summary>
        /// The chosen table, read by whatever source understands it, cut into tiles
        /// and laid over the cloud basemap. The style never learns which database it
        /// was.
        /// </summary>
        /// <remarks>
        /// Asking the table where it is happens off the UI thread: for the two servers
        /// that is a network round trip. A click on another database while one is still
        /// answering wins, and the late answer is dropped.
        /// </remarks>
        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Dark, _cloud);
            FeatureSourceVectorTileSource data = null;
            RectangleShape bounds = null;

            try
            {
                var source = OpenChosen();
                bounds = await Task.Run(() =>
                {
                    source.Open();
                    var box = source.GetBoundingBox();
                    source.Close();
                    return box;
                });
                if (request != _request) return;

                data = new FeatureSourceVectorTileSource();
                data.FeatureSources.Add("data", source);
                // Whole-country polygons over a network round trip are worth capping:
                // past zoom 8 the covering level-8 tile is served instead of re-querying
                // and re-simplifying the same continents at every detent.
                if (DbPostgres.IsChecked == true)
                {
                    data.MaxDataZoom = 8;
                }

                style.AddStyle((string)Resources["StyleJson"], data);
                Status.Foreground = Brushes.LightGreen;
                Status.Text = source.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;

                // The files come from the HowDoI sample repo and the servers are
                // demodb.thinkgeo.com; without them there is nothing to draw, and
                // saying so beats an empty window. Drivers wrap the real reason, so
                // unwrap to it.
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                Status.Foreground = Brushes.Salmon;
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            // Restyling in place, not a new basemap: assigning a basemap starts from
            // an empty scene, and that blank moment is the flash a viewer sees when
            // switching tables.
            var retired = _current;
            _current = data;
            if (Map.Basemap == null)
            {
                Map.Basemap = new GpuBasemap(style);
            }
            else
            {
                await Map.Basemap.SetStyleAsync(style);
            }

            if (request == _request)
            {
                if (bounds != null && bounds.Width > 0 && bounds.Height > 0)
                {
                    bounds.ScaleUp(20);
                    Map.CurrentExtent = bounds;
                }

                await Map.RefreshAsync();
            }

            retired?.Dispose();
        }

        /// <summary>
        /// The line that differs. The first three are databases you carry as a file -
        /// SQLite opens it through a connection string, the other two by its path - and
        /// the last two are servers you connect to; each converts from the plane its own
        /// table stores.
        /// </summary>
        private FeatureSource OpenChosen()
        {
            if (DbSqlite.IsChecked == true)
                return new SqliteFeatureSource("Data Source=" + Data("SQLite", "frisco-restaurants.sqlite") + ";", "restaurants", "id", "geometry")
                { ProjectionConverter = new ProjectionConverter(2276, 3857) };

            if (DbGeoPackage.IsChecked == true)
                return new GdalFeatureSource(Data("GeoPackage", "mora_surficial_geology.gpkg"))
                { ProjectionConverter = new ProjectionConverter(26910, 3857) };

            if (DbFileGeoDatabase.IsChecked == true)
                return new FileGeoDatabaseFeatureSource(Data("FileGeoDatabase", "zoning.gdb"), "zoning")
                { ProjectionConverter = new ProjectionConverter(2276, 3857) };

            if (DbPostgres.IsChecked == true)
                return new PostgreSqlFeatureSource("User ID=ThinkGeoTest;Password=ThinkGeoTestPassword;Host=demodb.thinkgeo.com;Port=5432;Database=postgres;Pooling=true;", "countries", "gid", 4326)
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            return new SqlServerFeatureSource("Server=demodb.thinkgeo.com;Database=thinkgeo;User Id=ThinkGeoTest;Password=ThinkGeoTestPassword;TrustServerCertificate=True;", "frisco_coyote_sightings", "id")
            { ProjectionConverter = new ProjectionConverter(2276, 3857) };
        }

        private static string Data(string folder, string file) =>
            Path.Combine(AppContext.BaseDirectory, "Data", folder, file);
    }
}
