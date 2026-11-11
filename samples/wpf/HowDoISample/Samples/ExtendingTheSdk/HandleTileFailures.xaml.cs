using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A tile that cannot be loaded raises TileLoadFailed; the handler sees the exception,
    /// chooses what is drawn in the tile's place and whether the tile is asked for again.
    /// </summary>
    public partial class HandleTileFailures
    {
        private bool _initialized;
        private int _failures;
        private readonly Dictionary<int, GeoImage> _ownImages = new Dictionary<int, GeoImage>();
        private ThinkGeoRasterTileSource _aerial;

        public HandleTileFailures()
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
            style.AddStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });

            // The key is wrong on purpose: the service answers 401 for every tile.
            _aerial = new ThinkGeoRasterTileSource("not-a-valid-key", ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1);
            style.AddRaster(_aerial, sourceId: "aerial", slot: StyleLayerSlot.AboveFills);

            var basemap = new GpuBasemap(style);
            basemap.TileLoadFailed += (_, e) =>
            {
                e.ShowOnMap = DrawDefault.IsChecked == true;
                e.Image = DrawCustom.IsChecked == true ? OwnImage(e.TileSize, e.DevicePixelRatio) : null;
                e.Retry = AskAgain.IsChecked == true;

                _failures++;
                Count.Text = _failures == 1 ? "1 tile request failed" : $"{_failures} tile requests failed";
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

            var image = new GeoImage(pixelSize, pixelSize);
            var canvas = GeoCanvas.CreateDefaultGeoCanvas();
            canvas.BeginDrawing(image, new RectangleShape(0, pixelSize, pixelSize, 0), GeographyUnit.Meter);
            canvas.Clear(new GeoSolidBrush(GeoColor.FromArgb(190, 225, 240, 255)));
            var font = new GeoFont("Arial", 13 * pixelRatio);
            var ink = new GeoSolidBrush(GeoColor.FromArgb(255, 30, 60, 110));
            var centre = pixelSize / 2f;
            canvas.DrawText("Aerial imagery unavailable", font, ink, new[] { new ScreenPointF(centre, centre - font.Size) }, DrawingLevel.LevelOne);
            canvas.DrawText("Check the API key", font, ink, new[] { new ScreenPointF(centre, centre + font.Size) }, DrawingLevel.LevelOne);
            canvas.EndDrawing();

            _ownImages[pixelSize] = image;
            return image;
        }
    }
}
