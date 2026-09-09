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
    /// One dataset, four expressions - match, step, filter and case: the classic
    /// ValueStyle, ClassBreakStyle, FilterStyle and boolean-expression styles are one
    /// mechanism, an expression in the layer's paint. Every rule ends in a default,
    /// so a feature no condition matches still has a look. Pick a rule or edit it;
    /// text that does not compile changes nothing.
    /// </summary>
    public partial class RenderBasedOnRules
    {
        private readonly DispatcherTimer _applyTimer;
        private bool _initialized;

        public RenderBasedOnRules()
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

            Map.Basemap = new GpuBasemap(new MapStyle(Editor.Text, tileSource));

            Map.CenterPoint = MaxExtents.SphericalMercator.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, MaxExtents.SphericalMercator, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        private void Rule_Checked(object sender, RoutedEventArgs e)
        {
            if (Editor != null && ((RadioButton)sender).Tag is string key)
            {
                Editor.Text = (string)FindResource(key);
            }
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
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r') +
                              "   (the map is still drawing the last document that worked)";
            }
        }
    }
}
