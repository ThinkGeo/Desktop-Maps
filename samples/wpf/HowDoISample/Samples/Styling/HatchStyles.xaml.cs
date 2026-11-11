using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The classic GeoHatchStyle catalog as fill patterns: the picked hatch brush is registered
    /// with MapStyle.Images under the name the document's fill-pattern uses.
    /// </summary>
    public partial class HatchStyles
    {
        private readonly DispatcherTimer _applyTimer;
        private GeoHatchStyle _hatch = GeoHatchStyle.Cross;
        private bool _initialized;
        // Every restyle passes this same source: a document on its own has no data to draw.
        private FeatureSourceVectorTileSource _parks;

        public HatchStyles()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };

            HatchPicker.ItemsSource = Enum.GetValues(typeof(GeoHatchStyle)).Cast<GeoHatchStyle>()
                .OrderBy(style => style.ToString(), StringComparer.OrdinalIgnoreCase)
                .ToList();
            HatchPicker.SelectedItem = _hatch;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(-10777610, 3909120);
            Map.CurrentScale = 2260;

            _parks = new FeatureSourceVectorTileSource();
            _parks.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });

            Map.Basemap = new GpuBasemap(ComposeStyle());
            _ = Map.RefreshAsync();
        }

        private void HatchPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HatchPicker.SelectedItem is GeoHatchStyle hatch)
            {
                Pick(hatch);
            }
        }

        // Hovering a row previews it on the map; closing the list goes back to the selection.
        private void HatchRow_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is ComboBoxItem { Content: GeoHatchStyle hovered })
            {
                Pick(hovered);
            }
        }

        private void HatchPicker_DropDownClosed(object sender, EventArgs e)
        {
            if (HatchPicker.SelectedItem is GeoHatchStyle hatch)
            {
                Pick(hatch);
            }
        }

        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;
            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // A pick applies at once; only typing in the editor waits for a pause.
        private void Pick(GeoHatchStyle hatch)
        {
            if (hatch == _hatch) return;
            _hatch = hatch;
            if (!_initialized) return;
            _applyTimer.Stop();
            _ = ApplyEditorStyleAsync();
        }

        // A document that does not compile changes nothing; the map keeps the last one that worked.
        private async Task ApplyEditorStyleAsync()
        {
            if (!IsLoaded) return;

            try
            {
                await Map.Basemap.SetStyleAsync(ComposeStyle());
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        /// <summary>
        /// The document plus the one image it names: the picked hatch, stroked into a pattern
        /// image and registered as "hatch". The swatch shows that image, tiled at the size the
        /// map draws it.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle(Editor.Text, _parks);
            style.Images.Add("hatch", new GeoHatchBrush(_hatch, GeoColors.Black));

            if (style.Images.TryGetImage("hatch", out var image, out _))
            {
                var preview = new BitmapImage();
                preview.BeginInit();
                preview.StreamSource = new System.IO.MemoryStream(image.GetImageBytes());
                preview.CacheOption = BitmapCacheOption.OnLoad;
                preview.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                preview.EndInit();
                Swatch.Background = new ImageBrush(preview)
                {
                    TileMode = TileMode.Tile,
                    ViewportUnits = BrushMappingMode.Absolute,
                    Viewport = new Rect(0, 0, preview.PixelWidth, preview.PixelHeight),
                    Stretch = Stretch.Fill,
                };
            }

            return style;
        }
    }
}
