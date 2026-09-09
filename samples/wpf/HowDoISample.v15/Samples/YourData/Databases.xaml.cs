using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
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
        /// <summary>The one source-layer name the style document draws.</summary>
        private const string DataLayer = "data";

        // The basemap under the data. One source for the life of the sample: a
        // restyle keeps the sources it recognizes, so the map below holds still
        // while the layer above it changes table.
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        private readonly DispatcherTimer _applyTimer;
        private GpuBasemap _basemap;
        private FeatureSourceVectorTileSource _current;
        private bool _ready;

        public Databases()
        {
            InitializeComponent();
            Map.MapUnit = GeographyUnit.Meter;

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private async void Map_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ready) return;

            _ready = true;
            var (style, extent) = await ComposeStyleAsync();
            _basemap = new GpuBasemap(style);
            Map.Basemap = _basemap;
            if (extent != null)
            {
                Map.CurrentExtent = extent;
            }

            await Map.RefreshAsync();
        }

        private async void Database_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;

            var retired = _current;
            var (style, extent) = await ComposeStyleAsync();

            // Restyling in place, not a new basemap: assigning a basemap starts from
            // an empty scene, and that blank moment is the flash a viewer sees when
            // switching tables.
            await _basemap.SetStyleAsync(style);
            if (extent != null)
            {
                Map.CurrentExtent = extent;
            }

            await Map.RefreshAsync();
            retired?.Dispose();
        }

        /// <summary>
        /// The chosen table, read by whatever source understands it, cut into tiles
        /// and laid over the cloud basemap. The style never learns which database it
        /// was.
        /// </summary>
        /// <remarks>
        /// Asking the table where it is happens off the UI thread: for the two servers
        /// that is a network round trip, and a sample that waits for it inline freezes
        /// the window before it draws anything.
        /// </remarks>
        private async Task<(MapStyle Style, RectangleShape Extent)> ComposeStyleAsync()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);

            try
            {
                var source = OpenChosen();
                var bounds = await Task.Run(() =>
                {
                    source.Open();
                    var box = source.GetBoundingBox();
                    source.Close();
                    return box;
                });

                _current = new FeatureSourceVectorTileSource();
                _current.FeatureSources.Add(DataLayer, source);
                // Whole-country polygons over a network round trip are worth capping:
                // past zoom 8 the covering level-8 tile is served instead of re-querying
                // and re-simplifying the same continents at every detent.
                if (DbPostgres.IsChecked == true)
                {
                    _current.MaxDataZoom = 8;
                }

                style.AddStyle(Editor.Text, _current);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = source.GetType().Name;
                if (bounds != null && bounds.Width > 0 && bounds.Height > 0)
                {
                    bounds.ScaleUp(20);
                    return (style, bounds);
                }
            }
            catch (Exception exception)
            {
                // The files come from the HowDoI sample repo and the servers are
                // demodb.thinkgeo.com; without them there is nothing to draw, and
                // saying so beats an empty window. Drivers wrap the real reason, so
                // unwrap to it.
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                _current = null;
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            return (style, null);
        }

        /// <summary>
        /// The line that differs. The first three are databases you carry as a file,
        /// the last two are servers you connect to; each converts from the plane its
        /// own table stores.
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

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_ready) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (_current == null) return;

            try
            {
                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
                style.AddStyle(Editor.Text, _current);
                await _basemap.SetStyleAsync(style);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        private static string Data(string folder, string file) =>
            Path.Combine(AppContext.BaseDirectory, "Data", folder, file);
    }
}
