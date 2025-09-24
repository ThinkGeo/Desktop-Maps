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

                    // CJK：Basic Han + Extensions A–G + Compatibility Ideographs & Compatibility Ideographs Supplement
                    else if (In(cp, 0x4E00, 0x9FFF)   // CJK Unified Ideographs
                          || In(cp, 0x3400, 0x4DBF)   // CJK Ext-A
                          || In(cp, 0x20000, 0x2A6DF) // CJK Ext-B
                          || In(cp, 0x2A700, 0x2B73F) // CJK Ext-C
                          || In(cp, 0x2B740, 0x2B81F) // CJK Ext-D
                          || In(cp, 0x2B820, 0x2CEAF) // CJK Ext-E
                          || In(cp, 0x2CEB0, 0x2EBEF) // CJK Ext-F
                          || In(cp, 0x30000, 0x3134F) // CJK Ext-G
                          || In(cp, 0xF900, 0xFAFF)   // CJK Compatibility Ideographs
                          || In(cp, 0x2F800, 0x2FA1F))// CJK Compatibility Ideographs Supplement
                    {
                        scripts.Add("zh");
                    }
                    // Japanese：Hiragana/Katakana + extensions + Kana Supplement + Kana Extended-A + Small Kana Extension + Halfwidth Katakana
                    else if (In(cp, 0x3040, 0x309F)   // Hiragana
                          || In(cp, 0x30A0, 0x30FF)   // Katakana
                          || In(cp, 0x31F0, 0x31FF)   // Katakana Phonetic Extensions
                          || In(cp, 0x1B000, 0x1B0FF) // Kana Supplement
                          || In(cp, 0x1B100, 0x1B12F) // Kana Extended-A
                          || In(cp, 0x1B130, 0x1B16F) // Small Kana Extension
                          || In(cp, 0xFF66, 0xFF9D))  // Halfwidth Katakana
                    {
                        scripts.Add("ja");
                    }
                    // Korean：Hangul syllables + Jamo (Basic/Extended-A/Extended-B) + Halfwidth Hangul
                    else if (In(cp, 0xAC00, 0xD7AF)   // Hangul Syllables
                          || In(cp, 0x1100, 0x11FF)   // Hangul Jamo
                          || In(cp, 0xA960, 0xA97F)   // Hangul Jamo Extended-A
                          || In(cp, 0xD7B0, 0xD7FF)   // Hangul Jamo Extended-B
                          || In(cp, 0xFFA0, 0xFFDC))  // Halfwidth Hangul
                    {
                        scripts.Add("ko");
                    }
                    // Arabic：Includes Extended-B & Extended-A, and Presentation Forms A/B
                    else if (In(cp, 0x0600, 0x06FF) || In(cp, 0x0750, 0x077F)
                          || In(cp, 0x0870, 0x089F) || In(cp, 0x08A0, 0x08FF) // Ext-B / Ext-A
                          || In(cp, 0xFB50, 0xFDFF) || In(cp, 0xFE70, 0xFEFF))
                    {
                        scripts.Add("ar");
                    }
                    // Hebrew
                    else if (In(cp, 0x0590, 0x05FF)) scripts.Add("he");

                    // Thai
                    else if (In(cp, 0x0E00, 0x0E7F)) scripts.Add("th");

                    // Myanmar：Basic + Extended-A/B
                    else if (In(cp, 0x1000, 0x109F) || In(cp, 0xAA60, 0xAA7F) || In(cp, 0xA9E0, 0xA9FF))
                    {
                        scripts.Add("my");
                    }

                    // Armenian
                    else if (In(cp, 0x0530, 0x058F)) scripts.Add("hy");

                    // Georgian：Includes Nuskhuri & Mtavruli
                    else if (In(cp, 0x10A0, 0x10FF) || In(cp, 0x2D00, 0x2D2F) || In(cp, 0x1C90, 0x1CBF))
                    {
                        scripts.Add("ka");
                    }

                    // Ethiopic: Basic + Supplement + Extended + Extended-A
                    else if (In(cp, 0x1200, 0x137F) || In(cp, 0x1380, 0x139F)
                          || In(cp, 0x2D80, 0x2DDF) || In(cp, 0xAB00, 0xAB2F))
                    {
                        scripts.Add("am");
                    }

                    // Khmer + Khmer Symbols
                    else if (In(cp, 0x1780, 0x17FF) || In(cp, 0x19E0, 0x19FF)) scripts.Add("km");

                    // Lao
                    else if (In(cp, 0x0E80, 0x0EFF)) scripts.Add("lo");

                    // Devanagari + Extended
                    else if (In(cp, 0x0900, 0x097F) || In(cp, 0xA8E0, 0xA8FF)) scripts.Add("hi");

                    // Bengali
                    else if (In(cp, 0x0980, 0x09FF)) scripts.Add("bn");

                    // Tamil
                    else if (In(cp, 0x0B80, 0x0BFF)) scripts.Add("ta");

                    // Sinhala
                    else if (In(cp, 0x0D80, 0x0DFF)) scripts.Add("si");

                    // Mongolian
                    else if (In(cp, 0x1800, 0x18AF)) scripts.Add("mn");

                    // Tifinagh
                    else if (In(cp, 0x2D30, 0x2D7F)) scripts.Add("ber");

                    // Tibetan
                    else if (In(cp, 0x0F00, 0x0FFF)) scripts.Add("bo");

                    // Yi
                    else if (In(cp, 0xA000, 0xA48F) || In(cp, 0xA490, 0xA4CF)) scripts.Add("yi");

                    // New Tai Lue
                    else if (In(cp, 0x1980, 0x19DF)) scripts.Add("talu");

                    // Thaana (Dhivehi/Maldivian)
                    else if (In(cp, 0x0780, 0x07BF)) scripts.Add("thaa");

                    // Greek（Includes  Greek Extended）
                    else if (In(cp, 0x0370, 0x03FF) || In(cp, 0x1F00, 0x1FFF)) scripts.Add("el");

                    // Cyrillic（Includes  Supplement / Extended-A/B）
                    else if (In(cp, 0x0400, 0x04FF) || In(cp, 0x0500, 0x052F)
                          || In(cp, 0x2DE0, 0x2DFF) || In(cp, 0xA640, 0xA69F))
                    {
                        scripts.Add("ru");
                    }
                }
                return scripts.Count > 1 ? "multi" : scripts.Count == 1 ? scripts.First() : "en";
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
                //Debug.WriteLine(lang);
                switch (lang)
                {
                    case "zh": family = "NotoSansSC"; break;
                    case "ja": family = "NotoSansJP"; break;
                    case "ko": family = "NotoSansKR"; break;
                    case "ar": family = "NotoSansArabic"; break;
                    case "he": family = "NotoSansHebrew"; break;
                    case "th": family = "NotoSansThai"; break;
                    case "my": family = "GoNotoKurrent"; break;
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
                    case "talu": family = "NotoSansNewTaiLue"; break;
                    case "thaa": family = "NotoSansThaana"; break;
                    case "el": family = "NotoSans"; break;
                    case "ru": family = "NotoSans"; break;
                    case "multi": family = "GoNotoKurrent"; break;
                    default: family = "NotoSans"; break;
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

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}