using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MVT.xaml
    /// </summary>
    public partial class MVTWithLocalFonts : IDisposable
    {
        public MVTWithLocalFonts()
        {
            InitializeComponent();
        }

        private string wvtServerUri = string.Empty;
        private bool _mapLoaded = false;
        private bool _useCustomFont = false;

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            _mapLoaded = true;

            wvtServerUri = "https://tiles.preludemaps.com/styles/Savannah_Light_v4/style.json";
            ThinkGeoDebugger.DisplayTileId = true;

            MapView.CurrentExtent = MaxExtents.ThinkGeoMaps;
            _ = RefreshMvtAsync("Multi Tile");
        }

        private void CustomFont_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _useCustomFont = true;
            _ = MapView.RefreshAsync();
        }
        private void FallbackFont_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _useCustomFont = false;
            _ = MapView.RefreshAsync();
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
            MapView.Overlays.Clear();

            if (selectedType == "Multi Tile")
                LoadMultiTile();
            else if (selectedType == "Single Tile")
                LoadSingleTile();
            else
                LoadMultiBackgroundWithDynamicLabeling();

            await MapView.RefreshAsync();
        }

        private void LoadSingleTile()
        {
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            var mvtLayer = new MvtTilesAsyncLayer(wvtServerUri);
            layerOverlay.LoadingSkiaSKTypeface += MvtLayerLoadingSkiaSkTypeface;
            layerOverlay.Layers.Add(mvtLayer);
            MapView.Overlays.Add(layerOverlay);
        }

        private void LoadMultiTile()
        {
            var layerOverlay = new LayerOverlay();
            var mvtLayer = new MvtTilesAsyncLayer(wvtServerUri);
            layerOverlay.LoadingSkiaSKTypeface += MvtLayerLoadingSkiaSkTypeface;
            layerOverlay.Layers.Add(mvtLayer);
            MapView.Overlays.Add(layerOverlay);
        }

        private void LoadMultiBackgroundWithDynamicLabeling()
        {
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultiTile;
            var mvtLayer1 = new MvtTilesAsyncLayer(wvtServerUri);
            mvtLayer1.LabelDisplayMode = LabelDisplayMode.ShapesOnly;
            layerOverlay.Layers.Add(mvtLayer1);
            MapView.Overlays.Add(layerOverlay);

            var drawingOverlay = new FeatureLayerWpfDrawingOverlay();
            var mvtLayer2 = new MvtTilesAsyncLayer(wvtServerUri);
            mvtLayer2.LabelDisplayMode = LabelDisplayMode.LabelsOnly;
            drawingOverlay.FeatureLayers.Add(mvtLayer2);
            MapView.Overlays.Add(drawingOverlay);
        }

        private void MvtLayerLoadingSkiaSkTypeface(object sender, LoadingSkiaSKTypefaceEventArgs e)
        {
            if (_useCustomFont)
                e.SkTypeFace = NotoSansFontMatcher.GetSkTypeface(e.Text, e.Font);
        }

        public static class NotoSansFontMatcher
        {
            /// <summary>Typeface cache keyed by absolute font file path.</summary>
            private static readonly ConcurrentDictionary<string, SKTypeface> _typefaces
                = new ConcurrentDictionary<string, SKTypeface>(StringComparer.OrdinalIgnoreCase);

            /// <summary>Families that actually provide italic faces.</summary>
            private static readonly HashSet<string> _familiesWithItalic = new HashSet<string>(StringComparer.Ordinal)
        {
            "NotoSans",          // Latin (Cyrillic/Greek also covered here)
            "NotoSansHebrew"     // Add more only if you truly have italic files
        };

            /// <summary>
            /// Main entry: pick a language from text, map to a Noto family and style, then load/cached SKTypeface.
            /// </summary>
            public static SKTypeface GetSkTypeface(string text, GeoFont font)
            {
                var lang = DetectLanguageTag(text);           // e.g., "zh" / "ja" / "ko" / "en"
                var path = GetNotoFilePath(lang, font, @"D:\DisplayMbTilesFile_fonts\NotoSansFonts");       // absolute file path

                // Cache and lazy-load the typeface; fall back to defaults if file missing.
                return _typefaces.GetOrAdd(path, p =>
                {
                    if (File.Exists(p))
                    {
                        var tf = SKTypeface.FromFile(p);
                        if (tf != null) return tf;
                    }
                    // Fallbacks if file is not present or invalid.
                    return SKTypeface.FromFamilyName("Noto Sans") ?? SKTypeface.Default;
                });
            }

            // ----------------------- Language detection -----------------------

            /// <summary>
            /// Detect a coarse language tag from text. Default "en".
            /// Returns a BCP-47-like primary tag such as "en", "zh", "ja", "ko", "ar", ...
            /// </summary>
            private static string DetectLanguageTag(string text)
            {
                if (string.IsNullOrEmpty(text)) return "en";
                var scripts = new HashSet<string>();
                foreach (var cp in EnumerateCodePoints(text))
                {
                    if (cp <= 0x007F) continue; // ASCII → keep scanning

                    // Order matters: detect distinctive blocks first
                    if (In(cp, 0x4E00, 0x9FFF) || In(cp, 0x3400, 0x4DBF)) scripts.Add("zh"); // CJK & Ext-A
                    else if (In(cp, 0x3040, 0x30FF) || In(cp, 0x31F0, 0x31FF)) scripts.Add("ja"); // Hiragana/Katakana
                    else if (In(cp, 0xAC00, 0xD7AF) || In(cp, 0x1100, 0x11FF)) scripts.Add("ko"); // Hangul syllables/jamo
                    else if (In(cp, 0x0600, 0x06FF) || In(cp, 0x0750, 0x077F) || In(cp, 0x08A0, 0x08FF) ||
                        In(cp, 0xFB50, 0xFDFF) || In(cp, 0xFE70, 0xFEFF)) scripts.Add("ar"); // Arabic
                    else if (In(cp, 0x0590, 0x05FF)) scripts.Add("he");  // Hebrew
                    else if (In(cp, 0x0E00, 0x0E7F)) scripts.Add("th");  // Thai
                    else if (In(cp, 0x1000, 0x109F)) scripts.Add("my");  // Myanmar
                    else if (In(cp, 0x0530, 0x058F)) scripts.Add("hy");  // Armenian
                    else if (In(cp, 0x10A0, 0x10FF) || In(cp, 0x2D00, 0x2D2F)) scripts.Add("ka");  // Georgian
                    else if (In(cp, 0x1200, 0x137F)) scripts.Add("am");  // Ethiopic
                    else if (In(cp, 0x1780, 0x17FF)) scripts.Add("km");  // Khmer
                    else if (In(cp, 0x0E80, 0x0EFF)) scripts.Add("lo");  // Lao
                    else if (In(cp, 0x0900, 0x097F)) scripts.Add("hi");  // Devanagari
                    else if (In(cp, 0x0980, 0x09FF)) scripts.Add("bn");  // Bengali
                    else if (In(cp, 0x0B80, 0x0BFF)) scripts.Add("ta");  // Tamil
                    else if (In(cp, 0x0D80, 0x0DFF)) scripts.Add("si"); // Sinhala
                    else if (In(cp, 0x1800, 0x18AF)) scripts.Add("mn"); // Mongolian
                    else if (In(cp, 0x2D30, 0x2D7F) || In(cp, 0x10E60, 0x10E7F) || In(cp, 0x2D80, 0x2DDF) || In(cp, 0x10E80, 0x10EBF)) scripts.Add("ber"); // Tifinagh
                    else if (In(cp, 0x0F00, 0x0FFF) || In(cp, 0x11400, 0x1147F)) scripts.Add("bo");  // Tibetan
                    else if ((cp >= 0xA000 && cp <= 0xA48F) || (cp >= 0xA490 && cp <= 0xA4CF)) scripts.Add("yi"); // yi
                    else if (In(cp, 0x0370, 0x03FF)) scripts.Add("el");  // Greek
                    else if (In(cp, 0x0400, 0x04FF)) scripts.Add("ru");  // Cyrillic
                }
                return "en";

            }
            static bool In(int v, int a, int b) => v >= a && v <= b;
            // ----------------------- Language + style → file path -----------------------

            /// <summary>
            /// Build the best Noto Sans file path from language + GeoFont style.
            /// Italic is auto-downgraded if the family does not ship italic faces.
            /// </summary>
            private static string GetNotoFilePath(string languageTag, GeoFont font, string fontsFolder)
            {
                // Normalize language to primary subtag (e.g., "zh-CN" → "zh")
                var lang = (languageTag ?? "en").Trim().ToLowerInvariant();
                var dash = lang.IndexOf('-');
                if (dash > 0) lang = lang.Substring(0, dash);

                // Map language → Noto family suffix (tweak as needed)
                string family;
                switch (lang)
                {
                    case "zh": family = "NotoSansSC"; break;
                    case "ja": family = "NotoSansJP"; break;
                    case "ko": family = "NotoSansKR"; break;
                    case "ar": family = "NotoSansArabic"; break;
                    case "he": family = "NotoSansHebrew"; break;
                    case "th": family = "NotoSansThai"; break;
                    case "my": family = "NotoSansMyanmar"; break;
                    case "hy": family = "NotoSansArmenian"; break;
                    case "ka": family = "NotoSansGeorgian"; break;
                    case "am": family = "NotoSansEthiopic"; break;
                    case "km": family = "NotoSansKhmer"; break;
                    case "lo": family = "NotoSansLao"; break;
                    case "hi": family = "NotoSansDevanagari"; break;
                    case "bn": family = "NotoSansBengali"; break;
                    case "ta": family = "NotoSansTamil"; break;
                    case "si": family = "NotoSansSinhala"; break;
                    case "mn": family = "NotoSansMongolian"; break;
                    case "ber": family = "NotoSansTifinagh"; break;
                    case "bo": family = "NotoSerifTibetan"; break;
                    case "yi": family = "NotoSansYi"; break;
                    case "el": family = "NotoSans"; break;
                    case "ru": family = "NotoSans"; break;
                    default: family = "GoNotoCurrent"; break;
                }

                // Effective style from GeoFont (name hints included; case-insensitive)
                var name = font?.FontName ?? string.Empty;
                bool wantBold = (font?.IsBold ?? false) || name.IndexOf("bold", StringComparison.OrdinalIgnoreCase) >= 0;
                bool wantItalic = (font?.IsItalic ?? false) || name.IndexOf("italic", StringComparison.OrdinalIgnoreCase) >= 0;

                // Some script-specific families do not provide italic faces.
                bool hasItalic = _familiesWithItalic.Contains(family);

                // Compose file name; most families provide Regular/Bold; italic varies.
                string filename;
                if (hasItalic)
                {
                    if (wantBold && wantItalic)
                    {
                        // No BoldItalic available → downgrade to Regular.
                        var boldItalicFilename = $"{family}-BoldItalic.ttf";
                        filename = FontExists(fontsFolder, boldItalicFilename)
                            ? boldItalicFilename
                            : $"{family}-Regular.ttf";
                    }
                    else if (wantBold)
                    {
                        // No Bold available → downgrade to Regular.
                        var boldFilename = $"{family}-Bold.ttf";
                        filename = FontExists(fontsFolder, boldFilename)
                            ? boldFilename
                            : $"{family}-Regular.ttf";
                    }
                    else if (wantItalic)
                    {
                        // No Italic available → downgrade to Regular.
                        var italicFilename = $"{family}-Italic.ttf";
                        filename = FontExists(fontsFolder, italicFilename)
                            ? italicFilename
                            : $"{family}-Regular.ttf";
                    }
                    else
                    {
                        filename = $"{family}-Regular.ttf";
                    }
                }
                else
                {
                    // No Bold available → downgrade to Regular.
                    if (wantBold)
                    {
                        var boldFilename = $"{family}-Bold.ttf";
                        filename = FontExists(fontsFolder, boldFilename)
                            ? boldFilename
                            : $"{family}-Regular.ttf";
                    }
                    else
                    {
                        filename = $"{family}-Regular.ttf";
                    }
                    // If your local CJK package is .otf, change the extension here.
                    // filename = Path.ChangeExtension(filename, ".otf");
                }

                return Path.Combine(fontsFolder, filename);
            }

            private static bool FontExists(string fontsFolder, string filename)
            {
                return !string.IsNullOrEmpty(fontsFolder) &&
                       !string.IsNullOrEmpty(filename) &&
                       File.Exists(Path.Combine(fontsFolder, filename));
            }

            // ----------------------- Helpers -----------------------

            /// <summary>
            /// Enumerate Unicode code points (handles surrogate pairs).
            /// </summary>
            private static IEnumerable<int> EnumerateCodePoints(string s)
            {
                if (string.IsNullOrEmpty(s)) yield break;

                int i = 0;
                while (i < s.Length)
                {
                    char c = s[i];
                    if (char.IsHighSurrogate(c) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
                    {
                        yield return char.ConvertToUtf32(c, s[i + 1]);
                        i += 2;
                    }
                    else
                    {
                        yield return c;
                        i += 1;
                    }
                }
            }
        }

        public static class GoNotoFontMatcher
        {
            // Cache of loaded typefaces, keyed by absolute file path, to avoid reopening files repeatedly
            private static readonly ConcurrentDictionary<string, SKTypeface> _typefaces =
                new ConcurrentDictionary<string, SKTypeface>(StringComparer.OrdinalIgnoreCase);

            /// <summary>
            /// Returns the appropriate SKTypeface based on the text content and GeoFont style.
            /// Uses the Go Noto Universal font bundles, distinguishing only between bold and regular weights.
            /// </summary>
            public static SKTypeface GetSkTypeface(string text, GeoFont font)
            {
                var group = DetectGoNotoGroup(text);
                const string fontsFolder = @"D:\DisplayMbTilesFile_fonts\GoNotoFonts";
                bool wantBold =
                    (font?.IsBold ?? false) ||
                    ((font?.FontName?.IndexOf("bold", StringComparison.OrdinalIgnoreCase) ?? -1) >= 0);

                var typeface = TryLoadGoNotoTypeface(group, wantBold, fontsFolder);
                return typeface ?? (SKTypeface.FromFamilyName("Arial") ?? SKTypeface.Default);
            }

            /// <summary>
            /// Determines which Go Noto font bundle to use based on the text's character ranges.
            /// If the text spans multiple script regions, returns GoNotoCurrent.
            /// </summary>
            private static string DetectGoNotoGroup(string text)
            {
                if (string.IsNullOrEmpty(text))
                    return "GoNotoCurrent";

                var groups = new HashSet<string>();
                foreach (int cp in EnumerateCodePoints(text))
                {
                    if (cp <= 0x007F) continue; // Skip ASCII

                    if (IsEastAsia(cp)) groups.Add("GoNotoEastAsia");
                    else if (IsSouthEastAsia(cp)) groups.Add("GoNotoSouthEastAsia");
                    else if (IsAfricaMiddleEast(cp)) groups.Add("GoNotoAfricaMiddleEast");
                    else if (IsCjkCore(cp)) groups.Add("GoNotoCJKCore");
                    else if (IsEuropeAmericas(cp)) groups.Add("GoNotoEuropeAmericas");
                    // Additional script ranges can be added here as needed
                }

                return groups.Count == 1 ? groups.First() : "GoNotoCurrent";
            }

            /// <summary>
            /// Attempts to load the specified Go Noto font; tries the bold version first, then regular, and finally without a suffix.
            /// </summary>
            private static SKTypeface TryLoadGoNotoTypeface(string family, bool wantBold, string fontsFolder)
            {
                var candidates = new List<string>();
                if (wantBold) candidates.Add($"{family}-Bold.ttf");
                candidates.Add($"{family}-Regular.ttf");
                candidates.Add($"{family}.ttf");

                foreach (var filename in candidates)
                {
                    var path = Path.Combine(fontsFolder, filename);
                    if (File.Exists(path))
                    {
                        return _typefaces.GetOrAdd(path, p =>
                        {
                            var tf = SKTypeface.FromFile(p);
                            return tf ?? SKTypeface.Default;
                        });
                    }
                }

                return null;
            }

            // Helper functions for determining script regions:
            private static bool IsEastAsia(int cp)
            {
                // Mongolian, Tibetan, Yi, New Tai Lue, Yi extensions, Miao, Tangut, Bopomofo, Japanese Hiragana/Katakana, etc.
                return (In(cp, 0x0F00, 0x0FFF) ||     // Tibetan
                        In(cp, 0x1800, 0x18AF) ||     // Mongolian
                        In(cp, 0xA000, 0xA4CF) ||     // Yi Syllables & Radicals
                        In(cp, 0x1980, 0x19DF) ||     // New Tai Lue
                        In(cp, 0x16F00, 0x16F9F) ||   // Miao
                        In(cp, 0x17000, 0x187FF) ||   // Tangut
                        In(cp, 0x3100, 0x312F) ||     // Bopomofo
                        In(cp, 0x3040, 0x30FF) || In(cp, 0x31F0, 0x31FF)); // Hiragana/Katakana
            }

            private static bool IsSouthEastAsia(int cp)
            {
                // Lao, Myanmar
                return (
                        In(cp, 0x0E80, 0x0EFF) ||// Lao
                        In(cp, 0x1000, 0x109F));  // Myanmar
            }

            private static bool IsAfricaMiddleEast(int cp)
            {
                // Arabic, Hebrew, Ethiopic, Tifinagh, etc.
                return (In(cp, 0x0600, 0x06FF) || In(cp, 0x0750, 0x077F) ||
                        In(cp, 0x08A0, 0x08FF) || In(cp, 0xFB50, 0xFDFF) ||
                        In(cp, 0xFE70, 0xFEFF)) ||       // Arabic ranges
                       In(cp, 0x0590, 0x05FF) ||       // Hebrew
                       In(cp, 0x1200, 0x137F) ||       // Ethiopic
                       In(cp, 0x2D30, 0x2D7F) || In(cp, 0x10E60, 0x10E7F) ||
                       In(cp, 0x2D80, 0x2DDF) || In(cp, 0x10E80, 0x10EBF); // Tifinagh & extensions
            }

            private static bool IsCjkCore(int cp)
            {
                // Han characters (including Extension A) and Hangul
                return (In(cp, 0x3400, 0x4DBF) || In(cp, 0x4E00, 0x9FFF) ||
                        In(cp, 0x1100, 0x11FF) || In(cp, 0xAC00, 0xD7AF));
            }

            private static bool IsEuropeAmericas(int cp)
            {
                // Greek, Slavic (Cyrillic), Armenian, Georgian, etc.
                return (In(cp, 0x0370, 0x03FF) ||     // Greek
                        In(cp, 0x0400, 0x04FF) ||     // Cyrillic
                        In(cp, 0x0530, 0x058F) ||     // Armenian
                        In(cp, 0x10A0, 0x10FF) ||     // Georgian
                        In(cp, 0x2D00, 0x2D2F));      // Georgian Supplement
            }

            private static bool In(int v, int a, int b) => v >= a && v <= b;

            /// <summary>
            /// Enumerates the Unicode code points in a string, correctly handling surrogate pairs.
            /// </summary>
            private static IEnumerable<int> EnumerateCodePoints(string s)
            {
                if (string.IsNullOrEmpty(s)) yield break;

                int i = 0;
                while (i < s.Length)
                {
                    char c = s[i];
                    if (char.IsHighSurrogate(c) && i + 1 < s.Length && char.IsLowSurrogate(s[i + 1]))
                    {
                        yield return char.ConvertToUtf32(c, s[i + 1]);
                        i += 2;
                    }
                    else
                    {
                        yield return c;
                        i += 1;
                    }
                }
            }
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }


    }
}