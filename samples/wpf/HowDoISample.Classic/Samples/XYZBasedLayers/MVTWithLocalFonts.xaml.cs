using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MVT.xaml
    /// </summary>
    public partial class MVTWithLocalFonts : IDisposable
    {

        private bool _initialized;
        public MVTWithLocalFonts()
        {
            InitializeComponent();

            DataContext = this;
        }

        private string wvtServerUri = "https://tiles.preludemaps.com/styles/WorldStreets_Light/style.json";
        private bool _mapLoaded = false;
        private bool _useCustomFont = false;
        private const string FontsFolder = ".\\CustomFonts";
        private readonly ConcurrentDictionary<string, SKTypeface> _typefaceCache
            = new ConcurrentDictionary<string, SKTypeface>(StringComparer.OrdinalIgnoreCase);


        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            _mapLoaded = true;

            Map.CurrentExtent = MaxExtents.ThinkGeoMaps;
            _ = RefreshMvtAsync("Multi Tile");
        }

        private void CustomFonts_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _useCustomFont = true;

            _ = Map.RefreshAsync();
        }

        private void SystemFonts_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _useCustomFont = false;
            _typefaceCache.Clear();

            _ = Map.RefreshAsync();
        }

        private void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            var selectedType = ((RadioButton)sender).Content?.ToString();
            _ = RefreshMvtAsync(selectedType);
        }

        private async Task RefreshMvtAsync(string selectedType)
        {
            Map.Overlays.Clear();

            if (selectedType == "Multi Tile")
                LoadMultiTile();
            else if (selectedType == "Single Tile")
                LoadSingleTile();
            else
                LoadMultiBackgroundWithDynamicLabeling();

            await Map.RefreshAsync();
        }

        private void LoadSingleTile()
        {
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            var mvtLayer = new MvtTilesAsyncLayer(wvtServerUri);
            layerOverlay.CreatingSKTypefacesForCharacter += MvtLayerCreatingSkTypefacesForCharacter;
            layerOverlay.Layers.Add(mvtLayer);
            Map.Overlays.Add(layerOverlay);
        }

        private void LoadMultiTile()
        {
            var layerOverlay = new LayerOverlay();
            var mvtLayer = new MvtTilesAsyncLayer(wvtServerUri);
            layerOverlay.CreatingSKTypefacesForCharacter += MvtLayerCreatingSkTypefacesForCharacter;
            layerOverlay.Layers.Add(mvtLayer);
            Map.Overlays.Add(layerOverlay);
        }

        private void LoadMultiBackgroundWithDynamicLabeling()
        {
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultiTile;
            var mvtLayer1 = new MvtTilesAsyncLayer(wvtServerUri);
            mvtLayer1.LabelDisplayMode = LabelDisplayMode.ShapesOnly;
            layerOverlay.Layers.Add(mvtLayer1);
            Map.Overlays.Add(layerOverlay);

            var drawingOverlay = new FeatureLayerWpfDrawingOverlay();
            var mvtLayer2 = new MvtTilesAsyncLayer(wvtServerUri);
            mvtLayer2.LabelDisplayMode = LabelDisplayMode.LabelsOnly;
            drawingOverlay.FeatureLayers.Add(mvtLayer2);
            Map.Overlays.Add(drawingOverlay);
        }

        private void MvtLayerCreatingSkTypefacesForCharacter(object sender, CreatingSKTypefaceForCharacterEventArgs e)
        {
            if (!_useCustomFont)
            {
                return;
            }

            var typeface = GetSupportedTypefaceForCharacter(e.Font, e.Character);
            if (typeface != null)
            {
                e.Typeface = typeface;
            }
        }

        public void AppendLog(string message, Brush brush)
        {
            Dispatcher.Invoke(() =>
            {
                LabelWarning.Content = message;
                LabelWarning.Foreground = brush;
            });
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string fontsFolder = ".\\CustomFonts";
            if (!Directory.Exists(fontsFolder))
            {
                Directory.CreateDirectory(fontsFolder);
            }

            Process.Start("explorer.exe", fontsFolder);
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            Map.Dispose();
            foreach (var kv in _typefaceCache)
            {
                kv.Value?.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        private static bool TypefaceSupportsChar(SKTypeface typeface, char ch, GeoFont geoFont)
        {
            if (typeface == null)
                return false;

            using (var skFont = new SKFont(typeface, (geoFont?.Size ?? 10)))
            {
                skFont.LinearMetrics = true;
                skFont.Subpixel = true;
                skFont.Edging = SKFontEdging.SubpixelAntialias;

                return skFont.ContainsGlyphs(ch.ToString());
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }

        private SKTypeface GetSupportedTypefaceForCharacter(GeoFont geoFont, char character)
        {
            var isBold = geoFont?.IsBold == true;

            var candidates = isBold
                ? new[]
                {
                    "GoNotoKurrent-Bold.ttf",
                    "NotoSansCanadianAboriginal-Bold.ttf",
                    "NotoSansSC-Bold.ttf"
                }
                : new[]
                {
                    "GoNotoKurrent-Regular.ttf",
                    "NotoSansCanadianAboriginal-Regular.ttf",
                    "NotoSansSC-Regular.ttf"
                };

            foreach (var fontName in candidates)
            {
                var typeface = LoadTypeface(fontName);
                if (typeface != null && TypefaceSupportsChar(typeface, character, geoFont))
                {
                    return typeface;
                }
            }

            return null;
        }

        private SKTypeface LoadTypeface(string fontName)
        {
            var file = Path.Combine(FontsFolder, fontName);
            if (!_typefaceCache.TryGetValue(file, out var typeface))
            {
                typeface = SKTypeface.FromFile(file);
                if (typeface != null)
                {
                    _typefaceCache.TryAdd(file, typeface);
                    AppendLog($"loaded: {Path.GetFileName(file)}", Brushes.Green);
                }
                else
                {
                    AppendLog($"Cannot Load: {Path.GetFileName(file)}", Brushes.Red);
                }
            }

            return typeface;
        }
    }
}