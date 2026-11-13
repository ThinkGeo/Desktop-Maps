using SkiaSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw the labels of a tile overlay with font files from a folder, for characters the system fonts lack.
    /// </summary>
    public partial class LocalFonts : IDisposable
    {
        private static readonly string FontsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "Fonts");

        private bool _initialized;
        private bool _useFolderFonts;
        private SKTypeface[] _typefaces = new SKTypeface[0];
        // _fonts[i] is _typefaces[i]; an SKFont answers which characters the file has.
        private SKFont[] _fonts = new SKFont[0];

        public LocalFonts()
        {
            InitializeComponent();
            FontsFolderText.Text = FontsFolder;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;

            var layerOverlay = new LayerOverlay();
            layerOverlay.CreatingSKTypefacesForCharacter += LayerOverlay_CreatingSKTypefacesForCharacter;
            layerOverlay.Layers.Add(new ThinkGeoVectorTileAsyncLayer(SampleKeys.ApiKey, ThinkGeoVectorStyles.Light));
            Map.Overlays.Add(layerOverlay);

            Map.CurrentExtent = MaxExtents.ThinkGeoMaps;

            _initialized = true;
            _ = Map.RefreshAsync();
        }

        // Raised on the drawing threads, once per character.
        private void LayerOverlay_CreatingSKTypefacesForCharacter(object sender, CreatingSKTypefaceForCharacterEventArgs e)
        {
            if (!_useFolderFonts) return;

            var typeface = _typefaces.Where((t, i) => t.IsBold == e.Font.IsBold && _fonts[i].ContainsGlyph(e.Character)).FirstOrDefault()
                ?? _typefaces.Where((t, i) => _fonts[i].ContainsGlyph(e.Character)).FirstOrDefault();
            if (typeface != null)
            {
                e.Typeface = typeface;
            }
        }

        private void SystemFonts_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _useFolderFonts = false;
            Status.Text = string.Empty;
            _ = Map.RefreshAsync();
        }

        private void FolderFonts_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            if (_typefaces.Length == 0)
            {
                Directory.CreateDirectory(FontsFolder);
                _typefaces = Directory.GetFiles(FontsFolder)
                    .Where(file => file.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".otf", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(file => file, StringComparer.OrdinalIgnoreCase)
                    .Select(file => SKTypeface.FromFile(file))
                    .Where(typeface => typeface != null)
                    .ToArray();
                _fonts = _typefaces.Select(typeface => typeface.ToFont()).ToArray();
            }

            _useFolderFonts = _typefaces.Length > 0;
            Status.Text = _useFolderFonts
                ? $"{_typefaces.Length} font files loaded."
                : "The folder has no .ttf or .otf files yet.";
            _ = Map.RefreshAsync();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(FontsFolder);
            Process.Start(new ProcessStartInfo(FontsFolder) { UseShellExecute = true });
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        public void Dispose()
        {
            Map.Dispose();
            foreach (var font in _fonts)
            {
                font.Dispose();
            }
            foreach (var typeface in _typefaces)
            {
                typeface.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
