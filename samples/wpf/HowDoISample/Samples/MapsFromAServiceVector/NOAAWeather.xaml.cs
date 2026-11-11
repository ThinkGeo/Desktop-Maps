using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// NOAA's live feeds merged into the basemap's style: the weather stations' current
    /// temperatures, or the active weather warnings colored by severity.
    /// </summary>
    public partial class NOAAWeather
    {
        private readonly ThinkGeoVectorTileSource _cloud =
            new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };

        private int _request;
        private bool _initialized;

        public NOAAWeather()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, _cloud));
            Map.CenterPoint = new PointShape(-10807050, 5045070);
            Map.CurrentScale = 34016000;
            await ShowChosenAsync();
        }

        private async void Feed_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        /// <summary>
        /// The feed downloads when it opens, so a click can arrive while the other feed is
        /// still on its way: the last click wins and a late answer is dropped.
        /// </summary>
        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            Status.Text = "Downloading from NOAA...";
            try
            {
                var (styleJson, data, status) = Stations.IsChecked == true
                    ? await LoadStationsAsync()
                    : await LoadWarningsAsync();
                if (request != _request) return;

                var style = new MapStyle();
                style.AddStyle(ThinkGeoVectorStyles.Light, _cloud);
                style.AddStyle(styleJson, data);
                await Map.Basemap.SetStyleAsync(style);
                Status.Text = status;
            }
            catch (Exception exception)
            {
                if (request != _request) return;
                Status.Text = "Unavailable: " + exception.Message.Split('\n')[0];
            }

            await Map.RefreshAsync();
        }

        private async Task<(string StyleJson, FeatureSourceVectorTileSource Data, string Status)> LoadStationsAsync()
        {
            var stations = new NoaaWeatherStationFeatureSource { ProjectionConverter = new ProjectionConverter(4326, 3857) };
            var count = await Task.Run(() =>
            {
                stations.Open();
                return stations.GetCount();
            });

            var data = new FeatureSourceVectorTileSource();
            data.FeatureSources.Add("stations", stations);
            return ((string)Resources["StationsStyle"], data, FormattableString.Invariant($"{count} stations reporting"));
        }

        private async Task<(string StyleJson, FeatureSourceVectorTileSource Data, string Status)> LoadWarningsAsync()
        {
            var warnings = new NoaaWeatherWarningsFeatureSource { ProjectionConverter = new ProjectionConverter(4326, 3857) };
            var count = await Task.Run(() =>
            {
                warnings.Open();
                return warnings.GetCount();
            });

            var data = new FeatureSourceVectorTileSource();
            data.FeatureSources.Add("warnings", warnings);
            var status = count == 0
                ? "No warnings in effect right now. On a quiet day this map is nearly empty."
                : FormattableString.Invariant($"{count} warning areas in effect");
            return ((string)Resources["WarningsStyle"], data, status);
        }
    }
}
