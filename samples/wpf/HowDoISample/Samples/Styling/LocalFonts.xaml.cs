using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Labels drawn with no network: from glyph files the style points at, from font files in a folder, or from the installed fonts.
    /// Windows installs fonts for nearly every script, so there the three can look alike.
    /// </summary>
    public partial class LocalFonts
    {
        private static readonly string FontsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "Fonts");

        private bool _initialized;

        public LocalFonts()
        {
            InitializeComponent();
            FontsFolderText.Text = FontsFolder;

            // The folder is the whole process's: the other samples draw without it.
            Unloaded += (sender, e) => GpuBasemap.LocalFontsDirectory = null;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.CenterPoint = new PointShape(4000000, 5800000);
            Map.CurrentScale = 240000000;
            await ShowAsync();
        }

        private static Feature City(string name, double longitude, double latitude)
        {
            var city = new Feature(longitude, latitude);
            city.ColumnValues["name"] = name;
            return city;
        }

        private async void Source_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowAsync();
        }

        // A new basemap each time, on new sources: a basemap that is replaced takes its sources with it.
        private async Task ShowAsync()
        {
            Directory.CreateDirectory(FontsFolder);
            GpuBasemap.LocalFontsDirectory = FromFolder.IsChecked == true ? FontsFolder : null;

            var world = new MbTilesVectorTileSource(Path.Combine(AppContext.BaseDirectory, "Data", "Mbtiles", "maplibre.mbtiles"));
            await world.OpenAsync();

            var cities = new InMemoryFeatureSource(new[] { new FeatureSourceColumn("name") }, new[]
            {
                City("ᐃᖃᓗᐃᑦ", -68.517, 63.747),
                City("São Paulo", -46.633, -23.550),
                City("Москва", 37.618, 55.751),
                City("Αθήνα", 23.728, 37.984),
                City("香港", 114.169, 22.319),
                City("서울", 126.978, 37.567),
            })
            {
                ProjectionConverter = new ProjectionConverter(4326, 3857)
            };
            var cityTiles = new FeatureSourceVectorTileSource();
            cityTiles.FeatureSources.Add("cities", cities);

            var style = new MapStyle().SetBackground(GeoColor.FromHtml("#C9DCE8"));
            if (FromGlyphFiles.IsChecked == true)
            {
                style.AddStyle((string)Resources["GlyphsJson"]);
            }
            style.AddStyle((string)Resources["WorldStyle"], world)
                .AddStyle((string)Resources["CitiesStyle"], cityTiles);
            Map.Basemap = new GpuBasemap(style);

            var fontFiles = Directory.GetFiles(FontsFolder).Count(file => file.EndsWith(".ttf", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".otf", StringComparison.OrdinalIgnoreCase));
            Status.Text = FromFolder.IsChecked != true
                ? string.Empty
                : fontFiles == 0
                    ? "The folder has no .ttf or .otf files yet: the installed fonts draw everything."
                    : fontFiles + " font files in the folder.";
            await Map.RefreshAsync();
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
    }
}
