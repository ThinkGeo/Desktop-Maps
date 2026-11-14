using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the live NOAA weather feeds: the weather stations, or the weather warnings in effect.
    /// </summary>
    public partial class NOAAWeather : IDisposable
    {
        private bool _initialized;
        private int _request;
        private LayerOverlay _weatherOverlay;
        private PopupOverlay _popupOverlay;
        private NoaaWeatherWarningsFeatureLayer _warningsLayer;

        public NOAAWeather()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _weatherOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            Map.Overlays.Add(_weatherOverlay);
            _popupOverlay = new PopupOverlay();
            Map.Overlays.Add(_popupOverlay);

            Map.CenterPoint = new PointShape(-10807050, 5045070);
            Map.CurrentScale = 34016000;
            await ShowChosenAsync();
        }

        private async void Feed_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            _weatherOverlay.Layers.Clear();
            _popupOverlay.Popups.Clear();
            _warningsLayer = null;
            LoadingImage.Visibility = Visibility.Visible;
            Status.Text = "Downloading from NOAA...";

            try
            {
                if (Stations.IsChecked == true)
                    await ShowWeatherStationsAsync(request);
                else
                    await ShowWeatherWarningsAsync(request);
            }
            catch (Exception exception)
            {
                if (request == _request) Status.Text = "Unavailable: " + exception.Message;
            }

            if (request != _request) return;
            LoadingImage.Visibility = Visibility.Hidden;
            _ = Map.RefreshAsync();
        }

        private async Task ShowWeatherStationsAsync(int request)
        {
            var stationsLayer = new NoaaWeatherStationFeatureLayer
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            stationsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            stationsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Opening downloads the feed, so it runs off the UI thread.
            var count = await Task.Run(() =>
            {
                stationsLayer.Open();
                return stationsLayer.FeatureSource.GetCount();
            });
            if (request != _request) return;

            _weatherOverlay.Layers.Add(stationsLayer);
            Status.Text = count + " stations reporting";
        }

        private async Task ShowWeatherWarningsAsync(int request)
        {
            var warningsLayer = new NoaaWeatherWarningsFeatureLayer
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            warningsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherWarningsStyle());
            warningsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var count = await Task.Run(() =>
            {
                warningsLayer.Open();
                return warningsLayer.FeatureSource.GetCount();
            });
            if (request != _request) return;

            _weatherOverlay.Layers.Add(warningsLayer);
            _warningsLayer = warningsLayer;
            Status.Text = count == 0 ? "No warnings in effect right now." : count + " warning areas in effect";
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (_warningsLayer == null) return;

            var features = _warningsLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.AllColumns);
            _popupOverlay.Popups.Clear();
            if (features.Count > 0)
            {
                var titles = new StringBuilder();
                foreach (var feature in features)
                {
                    titles.AppendLine(feature.ColumnValues["TITLE"]);
                }

                _popupOverlay.Popups.Add(new Popup(features[0].GetShape().GetCenterPoint())
                {
                    Content = titles.ToString(),
                    FontSize = 10d,
                    FontFamily = new System.Windows.Media.FontFamily("Verdana")
                });
            }

            _ = _popupOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
