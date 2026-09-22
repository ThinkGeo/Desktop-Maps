using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SkiaSharp;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A tile that cannot be loaded raises TileLoadFailed; the handler sees the exception,
    /// chooses what is drawn in the tile's place and whether the tile is asked for again.
    /// </summary>
    public partial class HandleExceptions
    {
        private bool _initialized;
        private int _failures;
        private readonly Dictionary<int, GeoImage> _ownImages = new Dictionary<int, GeoImage>();
        private ThinkGeoRasterTileSource _aerial;

        public HandleExceptions()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            _ = BuildMapAsync();
        }

        private async Task BuildMapAsync()
        {
            var style = new MapStyle();
            style.AddStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            await style.OpenAsync();

            // The key is wrong on purpose: the service answers 401 for every tile.
            _aerial = new ThinkGeoRasterTileSource("not-a-valid-key", ThinkGeoRasterMapType.Aerial);
            style.InsertRasterAt(StyleLayerSlot.AboveFills, _aerial, "aerial");

            var basemap = new GpuBasemap(style);
            basemap.TileLoadFailed += (_, e) =>
            {
                e.ShowOnMap = DrawDefault.IsChecked == true;
                e.Image = DrawCustom.IsChecked == true ? OwnImage(e.PixelSize, e.PixelRatio) : null;
                e.Retry = AskAgain.IsChecked == true;

                _failures++;
                Count.Text = $"{_failures} tile requests failed";
                Failure.Text = $"{e.Kind} tile {e.Zoom}/{e.X}/{e.Y} of \"{e.SourceName}\":\n{e.Exception.Message}";
            };
            Map.Basemap = basemap;

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 36000;
            await Map.RefreshAsync();
        }

        // The layer forgets its tiles and asks for them again, so they fail under the new choice.
        private void Choice_Changed(object sender, RoutedEventArgs e) => _aerial?.Refresh();

        // What DrawExceptionCore drew on the classic path: any picture, stretched to the tile.
        // Drawn at the size the tile has on screen, with 13 px letters scaled like the map's labels.
        private GeoImage OwnImage(int pixelSize, float pixelRatio)
        {
            if (_ownImages.TryGetValue(pixelSize, out var cached)) return cached;

            var bitmap = new SKBitmap(pixelSize, pixelSize);
            using (var canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(new SKColor(225, 240, 255, 190));
                using var font = new SKFont(SKTypeface.Default, 13 * pixelRatio);
                using var ink = new SKPaint { Color = new SKColor(30, 60, 110), IsAntialias = true };
                var centre = pixelSize / 2f;
                canvas.DrawText("Aerial imagery unavailable", centre, centre - font.Size * 0.4f, SKTextAlign.Center, font, ink);
                canvas.DrawText("Check the API key", centre, centre + font.Size * 1.1f, SKTextAlign.Center, font, ink);
            }

            var image = new GeoImage(bitmap);
            _ownImages[pixelSize] = image;
            return image;
        }
    }
}
