using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// The classic GeoHatchStyle catalog as fill patterns: each hatch brush is registered
    /// with MapStyle.Images as a pattern image, and named by fill-pattern.
    /// </summary>
    public partial class HatchStyles
    {
        private static readonly GeoColor Foreground = GeoColors.Black;

        private readonly DispatcherTimer _applyTimer;
        private readonly Dictionary<GeoHatchStyle, BitmapImage> _previews = new Dictionary<GeoHatchStyle, BitmapImage>();
        private readonly Dictionary<string, GeoHatchBrush> _brushes = new Dictionary<string, GeoHatchBrush>();

        private bool _initialized;
        // Every restyle passes this same source: a document on its own has no data to draw.
        private FeatureSourceVectorTileSource _parks;

        public HatchStyles()
        {
            InitializeComponent();

            _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyEditorStyleAsync(); };

            foreach (var style in Enum.GetValues(typeof(GeoHatchStyle)).Cast<GeoHatchStyle>()
                         .OrderBy(style => style.ToString(), StringComparer.OrdinalIgnoreCase))
            {
                HatchPicker.Items.Add(style);
            }

            HatchPicker.SelectedItem = GeoHatchStyle.Cross;
            HatchPicker.SelectionChanged += (_, _) => Pick(SelectedHatch);

            // Hovering a row previews it on the map, through the same document edit.
            var rowStyle = new System.Windows.Style(typeof(ComboBoxItem));
            rowStyle.Setters.Add(new EventSetter(UIElement.PreviewMouseMoveEvent,
                new System.Windows.Input.MouseEventHandler((sender, _) =>
                {
                    if (sender is ComboBoxItem row && row.Content is GeoHatchStyle hovered)
                    {
                        Pick(hovered);
                    }
                })));
            HatchPicker.ItemContainerStyle = rowStyle;
            HatchPicker.DropDownClosed += (_, _) => Pick(SelectedHatch);
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(-10777610, 3909120);
            Map.CurrentScale = 2260;

            foreach (GeoHatchStyle style in Enum.GetValues(typeof(GeoHatchStyle)))
            {
                _brushes[ImageId(style)] = new GeoHatchBrush(style, Foreground);
            }

            _parks = new FeatureSourceVectorTileSource();
            _parks.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });

            var mapStyle = new MapStyle(Editor.Text, _parks);
            AddPatternImages(mapStyle);
            foreach (GeoHatchStyle style in Enum.GetValues(typeof(GeoHatchStyle)))
            {
                if (mapStyle.Images.TryGetImage(ImageId(style), out var image, out _))
                {
                    _previews[style] = ToBitmap(image.GetImageBytes());
                }
            }

            Map.Basemap = new GpuBasemap(mapStyle);

            ShowSwatch(SelectedHatch);
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
                var style = new MapStyle(Editor.Text, _parks);
                AddPatternImages(style);
                await Map.Basemap.SetStyleAsync(style);
                Status.Foreground = Brushes.DarkGreen;
                Status.Text = FormattableString.Invariant($"applied at {DateTime.Now:HH:mm:ss}");
            }
            catch (Exception exception)
            {
                Status.Foreground = Brushes.Firebrick;
                Status.Text = "not applied - " + exception.Message.Split('\n')[0].TrimEnd('\r');
            }
        }

        /// <summary>Rewrites fill-pattern in the document; the editor pipeline applies it.</summary>
        private void Pick(GeoHatchStyle hatch)
        {
            ShowSwatch(hatch);

            var updated = Regex.Replace(Editor.Text,
                "\"fill-pattern\":\\s*\"[^\"]*\"",
                "\"fill-pattern\": \"" + ImageId(hatch) + "\"");
            if (updated != Editor.Text)
            {
                Editor.Text = updated;
            }
        }

        private void AddPatternImages(MapStyle style)
        {
            foreach (var brush in _brushes)
            {
                style.Images.Add(brush.Key, brush.Value);
            }
        }

        private GeoHatchStyle SelectedHatch =>
            HatchPicker.SelectedItem is GeoHatchStyle style ? style : GeoHatchStyle.Cross;

        private static string ImageId(GeoHatchStyle style) =>
            "hatch-" + style.ToString().ToLowerInvariant();

        /// <summary>The registered image itself, tiled at the size the map draws it at.</summary>
        private void ShowSwatch(GeoHatchStyle hatch)
        {
            if (!_previews.TryGetValue(hatch, out var preview))
            {
                return;
            }

            Swatch.Background = new ImageBrush(preview)
            {
                TileMode = TileMode.Tile,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewport = new Rect(0, 0, preview.PixelWidth, preview.PixelHeight),
                Stretch = Stretch.Fill,
            };
        }

        private static BitmapImage ToBitmap(byte[] png)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new System.IO.MemoryStream(png);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }
    }
}
