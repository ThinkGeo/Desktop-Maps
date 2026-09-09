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
    /// Open a vector file - a shapefile, GeoJSON, KML, GPX, MapInfo TAB, TinyGeo, CAD,
    /// an S-57 nautical chart, a GeoPDF - or build features in memory, and draw them.
    /// Reading the format is one line; everything after it is the same for all of
    /// them, because a FeatureSource is a FeatureSource.
    /// </summary>
    public partial class VectorFileFormats
    {
        /// <summary>The one source-layer name the style document draws.</summary>
        private const string DataLayer = "data";

        // The basemap under the data. One source for the life of the sample: a
        // restyle keeps the sources it recognizes, so the map below holds still
        // while the layer above it changes file.
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);

        private readonly DispatcherTimer _applyTimer;
        private GpuBasemap _basemap;
        private FeatureSourceVectorTileSource _current;
        private bool _ready;

        public VectorFileFormats()
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

        private async void Format_Checked(object sender, RoutedEventArgs e)
        {
            if (!_ready) return;

            var retired = _current;
            var (style, extent) = await ComposeStyleAsync();

            // Restyling in place, not a new basemap: assigning a basemap starts from
            // an empty scene, and that blank moment is the flash a viewer sees when
            // switching files.
            await _basemap.SetStyleAsync(style);
            if (extent != null)
            {
                Map.CurrentExtent = extent;
            }

            await Map.RefreshAsync();
            retired?.Dispose();
        }

        /// <summary>
        /// The chosen file, read by whatever source understands it, cut into tiles and
        /// laid over the cloud basemap. The style never learns which format it was.
        /// </summary>
        /// <remarks>
        /// Opening the file to ask where it is happens off the UI thread. A GeoPDF or a
        /// DWG is tens of megabytes of parsing, and a sample that does it inline freezes
        /// the window before it draws anything.
        /// </remarks>
        private async Task<(MapStyle Style, RectangleShape Extent)> ComposeStyleAsync()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);

            try
            {
                var source = OpenChosen();

                // These are files on disk that nothing writes to while the sample runs.
                // Saying so lets the tile cutter read them through one open source and
                // keep the tiles it encodes, instead of cloning the source per worker -
                // which a GeoPDF or a DWG cannot do cheaply, or at all. One source means
                // one reader: GDAL allows a single feature iterator per dataset, so the
                // two settings only make sense together.
                _current = new FeatureSourceVectorTileSource
                {
                    SharedSources = true,
                    MaxConcurrentEncodes = 1,
                };
                _current.FeatureSources.Add(DataLayer, source);
                style.AddStyle(Editor.Text, _current);

                var bounds = await Task.Run(() =>
                {
                    source.Open();
                    var box = source.GetBoundingBox();
                    source.Close();
                    return box;
                });

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
                // The data comes from the HowDoI sample repo; without it there is
                // nothing to draw, and saying so beats an empty window. Formats read
                // through a plugin wrap the real reason, so unwrap to it.
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
        /// The line that differs. Each source converts from the plane its own format
        /// stores - a GPS track is degrees, a Texas cadastral file is state plane, a
        /// CAD drawing carries whatever the surveyor drew in.
        /// </summary>
        private FeatureSource OpenChosen()
        {
            if (FmtShapefile.IsChecked == true)
                return new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp"))
                { ProjectionConverter = new ProjectionConverter(2276, 3857) };

            if (FmtGeoJson.IsChecked == true)
                return new GeoJsonFeatureSource(Data("GeoJson", "pittsburghpacity-designated-historic-districts.geojson"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            if (FmtKml.IsChecked == true)
                return new KmlGdalFeatureSource(Data("Kml", "Frisco.kml"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            if (FmtGpx.IsChecked == true)
                return new GpxFeatureSource(Data("Gpx", "Hike_Bike.gpx"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            if (FmtTab.IsChecked == true)
                return new TabFeatureSource(Data("Tab", "City_ETJ.tab"))
                { ProjectionConverter = new ProjectionConverter(2276, 3857) };

            if (FmtTinyGeo.IsChecked == true)
                return new TinyGeoFeatureSource(Data("TinyGeo", "Zoning.tgeo"))
                { ProjectionConverter = new ProjectionConverter(2276, 3857) };

            if (FmtCad.IsChecked == true)
                return new CadFeatureSource(Data("CAD", "Zipcodes.DWG"))
                { ProjectionConverter = new ProjectionConverter(103376, 3857) };

            if (FmtS57.IsChecked == true)
                return new NauticalChartsFeatureSource(Data(Path.Combine("S57", "US1GC09M"), "US1GC09M.000"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            if (FmtGeoPdf.IsChecked == true)
                return new GeoPdfGdalFeatureSource(Data("GeoPdf", "bangalore.pdf"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            return BuildInMemory();
        }

        /// <summary>
        /// Features made in code rather than read from a file - the same shape of
        /// source, so it draws through the identical style.
        /// </summary>
        private static FeatureSource BuildInMemory()
        {
            var mosquitoes = new ShapeFileFeatureSource(SampleShared.Shapefile("Frisco_Mosquitos.shp"));
            mosquitoes.Open();
            var features = mosquitoes.GetAllFeatures(ReturningColumnsType.NoColumns);
            mosquitoes.Close();

            return new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(), features)
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            };
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
