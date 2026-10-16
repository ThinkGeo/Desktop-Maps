using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Style points, lines and polygons, and label each of them, from the style
    /// document beside the map - edit it and the map follows.
    /// </summary>
    public partial class RenderPointsLinesAndPolygons
    {
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;
        // Every restyle passes this same source: a document on its own has no data to draw.
        private FeatureSourceVectorTileSource _tiles;

        public RenderPointsLinesAndPolygons()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The shapefiles are in Texas North Central feet (EPSG:2276); reproject to the map's EPSG:3857.
            _tiles = new FeatureSourceVectorTileSource();
            _tiles.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            _tiles.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Streets.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            _tiles.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Hotels.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });

            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text, _tiles));

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9000;

            _ = Map.RefreshAsync();
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(new MapStyle(Editor.Text, _tiles));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        // The same document over the same shapefiles, drawn into a PDF as vector paths and text.
        private async void ExportPdf_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog { Filter = "PDF files (*.pdf)|*.pdf", FileName = "Frisco.pdf" };
            if (dialog.ShowDialog() != true) return;

            // Sources of its own: the map is reading the ones it was given.
            var featureSources = new GeoCollection<FeatureSource>();
            foreach (var name in new[] { "Parks", "Streets", "Hotels" })
            {
                featureSources.Add(name, new ShapeFileFeatureSource(SampleShared.Shapefile(name + ".shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            }

            var layer = new MapStyleFeatureAsyncLayer(new MapStyle(Editor.Text), featureSources);
            try
            {
                await layer.OpenAsync();
                var pdf = new PdfGeoCanvas();
                pdf.SetPageSize(PrinterPageSize.AnsiA, PrinterOrientation.Landscape);
                using (var stream = File.Create(dialog.FileName))
                {
                    pdf.BeginDrawing(stream, Map.CurrentExtent, Map.MapUnit);
                    await layer.DrawAsync(pdf, new Collection<SimpleCandidate>());
                    pdf.EndDrawing();
                }

                Status.Foreground = Brushes.DarkGreen;
                Status.Text = "exported to " + dialog.FileName;
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not exported - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
            finally
            {
                await layer.CloseAsync();
            }
        }
    }
}
