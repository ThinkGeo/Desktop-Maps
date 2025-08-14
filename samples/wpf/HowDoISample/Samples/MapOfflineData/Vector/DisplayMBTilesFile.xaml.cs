using SkiaSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMbTilesFile : IDisposable
    {
        public DisplayMbTilesFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            var _selectedMvtServer = @"https://tiles.preludemaps.com/styles/TG_Savannah_Light/style.json";
            var mvtLayer = new MvtTilesAsyncLayer(_selectedMvtServer);
            mvtLayer.LoadingSkiaSKTypeface += MvtLayerLoadingSkiaSkTypeface;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(mvtLayer);

            MapView.Overlays.Clear();
            MapView.Overlays.Add(layerOverlay);

            await mvtLayer.OpenAsync();
            MapView.CurrentExtent = mvtLayer.GetBoundingBox();

            await MapView.RefreshAsync();
        }


        private void MvtLayerLoadingSkiaSkTypeface(object sender, LoadingSkiaSKTypefaceEventArgs e)
        {
            e.SkTypeFace = FontMatcher.GetSkTypeface(e.Text, e.Font);
        }


        private void SwitchTileSize_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            //var selectedType = ((RadioButton)sender).Content?.ToString();

            //if (selectedType == null)
            //    return;

            //var mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            //if (selectedType == "Apply Style")
            //    mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";

            //var layerOverlay = new LayerOverlay();
            //layerOverlay.Layers.Add(mvtLayer);

            //MapView.Overlays.Clear();
            //MapView.Overlays.Add(layerOverlay);

            //_ = MapView.RefreshAsync();
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    public static class FontMatcher
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
            var path = GetNotoFilePath(lang, font, @"D:\test\fonts");       // absolute file path

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

            foreach (var cp in EnumerateCodePoints(text))
            {
                if (cp <= 0x007F) continue; // ASCII → keep scanning

                // Order matters: detect distinctive blocks first
                if (In(cp, 0x4E00, 0x9FFF) || In(cp, 0x3400, 0x4DBF)) return "zh"; // CJK & Ext-A
                if (In(cp, 0x3040, 0x30FF) || In(cp, 0x31F0, 0x31FF)) return "ja"; // Hiragana/Katakana
                if (In(cp, 0xAC00, 0xD7AF) || In(cp, 0x1100, 0x11FF)) return "ko"; // Hangul syllables/jamo

                if (In(cp, 0x0600, 0x06FF) || In(cp, 0x0750, 0x077F) || In(cp, 0x08A0, 0x08FF) ||
                    In(cp, 0xFB50, 0xFDFF) || In(cp, 0xFE70, 0xFEFF)) return "ar"; // Arabic

                if (In(cp, 0x0590, 0x05FF)) return "he";  // Hebrew
                if (In(cp, 0x0E00, 0x0E7F)) return "th";  // Thai
                if (In(cp, 0x1000, 0x109F)) return "my";  // Myanmar
                if (In(cp, 0x0F00, 0x0FFF)) return "bo";  // Tibetan
                if (In(cp, 0x0530, 0x058F)) return "hy";  // Armenian
                if (In(cp, 0x10A0, 0x10FF)) return "ka";  // Georgian
                if (In(cp, 0x1200, 0x137F)) return "am";  // Ethiopic
                if (In(cp, 0x1780, 0x17FF)) return "km";  // Khmer
                if (In(cp, 0x0E80, 0x0EFF)) return "lo";  // Lao
                if (In(cp, 0x0900, 0x097F)) return "hi";  // Devanagari
                if (In(cp, 0x0370, 0x03FF)) return "el";  // Greek
                if (In(cp, 0x0400, 0x04FF)) return "ru";  // Cyrillic
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
                case "bo": family = "NotoSansTibetan"; break;
                case "hy": family = "NotoSansArmenian"; break;
                case "ka": family = "NotoSansGeorgian"; break;
                case "am": family = "NotoSansEthiopic"; break;
                case "km": family = "NotoSansKhmer"; break;
                case "lo": family = "NotoSansLao"; break;
                case "hi": family = "NotoSansDevanagari"; break;
                case "el": family = "NotoSans"; break;
                case "ru": family = "NotoSans"; break;
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
                if (wantBold && wantItalic) filename = $"{family}-BoldItalic.ttf";
                else if (wantBold) filename = $"{family}-Bold.ttf";
                else if (wantItalic) filename = $"{family}-Italic.ttf";
                else filename = $"{family}-Regular.ttf";
            }
            else
            {
                // No italic available → downgrade to Regular/Bold.
                filename = wantBold ? $"{family}-Bold.ttf" : $"{family}-Regular.ttf";
                // If your local CJK package is .otf, change the extension here.
                // filename = Path.ChangeExtension(filename, ".otf");
            }

            return Path.Combine(fontsFolder, filename);
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
}