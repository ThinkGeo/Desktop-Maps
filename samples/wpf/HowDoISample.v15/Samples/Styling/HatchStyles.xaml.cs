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
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The classic GeoHatchStyle catalog as fill patterns: each hatch is stroked
    /// into a small image, registered with MapStyle.Images, and named by fill-pattern.
    /// </summary>
    public partial class HatchStyles
    {
        private static readonly GeoColor Foreground = GeoColors.Black;

        private readonly DispatcherTimer _applyTimer;
        private readonly Dictionary<GeoHatchStyle, BitmapImage> _previews = new Dictionary<GeoHatchStyle, BitmapImage>();
        private readonly Dictionary<string, GeoImage> _images = new Dictionary<string, GeoImage>();

        private double _pixelScale = 1d;
        private bool _initialized;

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

            // Rasterize at the display's pixel scale and declare that ratio, the way an
            // @2x sprite does - otherwise the pattern goes soft or moires.
            _pixelScale = Math.Max(1d, VisualTreeHelper.GetDpi(this).DpiScaleX);

            foreach (GeoHatchStyle style in Enum.GetValues(typeof(GeoHatchStyle)))
            {
                var png = RenderHatchTile(style, _pixelScale);
                _images[ImageId(style)] = new GeoImage(png);
                _previews[style] = ToBitmap(png);
            }

            var parks = new FeatureSourceVectorTileSource();
            parks.FeatureSources.Add(new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857),
            });

            var mapStyle = new MapStyle(Editor.Text, parks);
            AddPatternImages(mapStyle);
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
                var style = new MapStyle(Editor.Text);
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
            foreach (var image in _images)
            {
                style.Images.Add(image.Key, image.Value, (float)_pixelScale);
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
                Viewport = new Rect(0, 0, preview.PixelWidth / _pixelScale, preview.PixelHeight / _pixelScale),
                Stretch = Stretch.Fill,
            };
        }

        /// <summary>
        /// Strokes one hatch definition into a pattern tile. The path and cell size come
        /// from GeoHatchBrush.GetHatchPatternSvg - the same definition the classic
        /// renderer strokes - so the two renderers cannot drift apart.
        /// </summary>
        private static byte[] RenderHatchTile(GeoHatchStyle hatchStyle, double scale)
        {
            var brush = new GeoHatchBrush(hatchStyle, Foreground);
            var (svgPath, width, height) = brush.GetHatchPatternSvg(hatchStyle);

            // The definitions lean on an implicit start at the origin ("h8", "L 8 8"),
            // which Skia's parser allows and WPF's does not.
            var data = svgPath.TrimStart();
            if (data.Length == 0 || (data[0] != 'M' && data[0] != 'm'))
            {
                data = "M 0 0 " + data;
            }

            var pen = new Pen(new SolidColorBrush(Color.FromArgb(
                Foreground.A, Foreground.R, Foreground.G, Foreground.B)), 1d)
            {
                StartLineCap = PenLineCap.Flat,
                EndLineCap = PenLineCap.Flat,
                LineJoin = PenLineJoin.Miter,
            };

            var visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                context.PushTransform(new ScaleTransform(scale, scale));
                context.DrawGeometry(null, pen, Geometry.Parse(data));
                context.Pop();
            }

            var bitmap = new RenderTargetBitmap(
                Math.Max(1, (int)Math.Round(width * scale)),
                Math.Max(1, (int)Math.Round(height * scale)),
                96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using var stream = new System.IO.MemoryStream();
            encoder.Save(stream);
            return stream.ToArray();
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
