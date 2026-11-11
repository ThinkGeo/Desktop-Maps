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
    /// Open a vector file - a shapefile, GeoJSON, KML, GPX, MapInfo TAB, TinyGeo, CAD,
    /// a GeoPDF - or build features in memory, and draw them.
    /// Reading the format is one line; everything after it is the same for all of
    /// them, because a FeatureSource is a FeatureSource.
    /// </summary>
    public partial class VectorFileFormats
    {
        // The basemap under the data. One source for the life of the sample: a
        // restyle keeps the sources it recognizes, so the map below holds still
        // while the layer above it changes file.
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };

        private FeatureSourceVectorTileSource _current;
        private int _request;
        private bool _ready;

        public VectorFileFormats()
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

        private async void Format_Checked(object sender, RoutedEventArgs e)
        {
            if (_ready) await ShowChosenAsync();
        }

        /// <summary>
        /// The chosen file, read by whatever source understands it, cut into tiles and
        /// laid over the cloud basemap. The style never learns which format it was.
        /// </summary>
        /// <remarks>
        /// Opening the file to ask where it is happens off the UI thread: a GeoPDF or a
        /// DWG is tens of megabytes of parsing. A click on another format while one is
        /// still opening wins, and the late answer is dropped.
        /// </remarks>
        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
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
                style.AddStyle((string)Resources["StyleJson"], data);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = source.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;

                // The data comes from the HowDoI sample repo; without it there is
                // nothing to draw, and saying so beats an empty window. Formats read
                // through a plugin wrap the real reason, so unwrap to it.
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }

                Status.Foreground = Brushes.Firebrick;
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            // Restyling in place, not a new basemap: assigning a basemap starts from
            // an empty scene, and that blank moment is the flash a viewer sees when
            // switching files.
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

            if (FmtGeoPdf.IsChecked == true)
                return new GeoPdfGdalFeatureSource(Data("GeoPdf", "bangalore.pdf"))
                { ProjectionConverter = new ProjectionConverter(4326, 3857) };

            return BuildInMemory();
        }

        /// <summary>
        /// Features made in code rather than read from a file - a block, a street through
        /// it and three points in Frisco, TX - drawn through the identical style.
        /// </summary>
        private static FeatureSource BuildInMemory()
        {
            var features = new[]
            {
                new Feature("POLYGON((-96.830 33.145, -96.820 33.145, -96.820 33.153, -96.830 33.153, -96.830 33.145))"),
                new Feature("LINESTRING(-96.838 33.141, -96.825 33.149, -96.812 33.157)"),
                new Feature("POINT(-96.834 33.150)"),
                new Feature("POINT(-96.816 33.146)"),
                new Feature("POINT(-96.825 33.158)"),
            };

            return new InMemoryFeatureSource(Array.Empty<FeatureSourceColumn>(), features)
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857),
            };
        }

        private static string Data(string folder, string file) =>
            Path.Combine(AppContext.BaseDirectory, "Data", folder, file);
    }
}
