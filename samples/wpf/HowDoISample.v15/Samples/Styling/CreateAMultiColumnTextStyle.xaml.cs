using System;
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
    /// Build a label from several columns with a concat expression, from the style
    /// document beside the map - edit it and the map follows.
    /// </summary>
    public partial class CreateAMultiColumnTextStyle
    {
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public CreateAMultiColumnTextStyle()
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

            var tileSource = new FeatureSourceVectorTileSource();
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Countries02.shp"))
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857),
            });
            tileSource.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("WorldCapitals.shp"))
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857),
            });

            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text, tileSource));

            Map.CenterPoint = new PointShape(1675000, 5900000);
            Map.CurrentScale = 18496580;

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
                await Map.Basemap.SetStyleAsync(new MapStyle(Editor.Text));
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }
    }
}
