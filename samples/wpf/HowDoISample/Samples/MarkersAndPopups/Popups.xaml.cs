using System.Collections.Generic;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Add and remove popups on the map with the PopupOverlay, and open one where the map is clicked.
    /// </summary>
    public partial class Popups
    {
        private readonly PopupOverlay _popupOverlay = new PopupOverlay();
        private readonly List<Popup> _hotelPopups = new List<Popup>();
        private Popup _clickedPopup;
        private bool _initialized;

        public Popups()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            // Tilt with the right mouse button: a popup stays on its ground point under perspective.
            Map.IsTiltEnabled = true;
            Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9030;

            var hotels = new ShapeFileFeatureSource(SampleShared.Shapefile("Hotels.shp"))
            {
                ProjectionConverter = new ProjectionConverter(2276, 3857)
            };
            hotels.Open();
            foreach (var feature in hotels.GetAllFeatures(ReturningColumnsType.AllColumns))
            {
                _hotelPopups.Add(new Popup(feature.GetShape().GetCenterPoint())
                {
                    Content = feature.ColumnValues["NAME"]
                });
            }
            hotels.Close();

            foreach (var popup in _hotelPopups)
            {
                _popupOverlay.Popups.Add(popup);
            }

            Map.Overlays.Add(_popupOverlay);
            _ = Map.RefreshAsync();
        }

        private void ShowHotels_Changed(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            foreach (var popup in _hotelPopups)
            {
                if (ShowHotels.IsChecked == true)
                {
                    _popupOverlay.Popups.Add(popup);
                }
                else
                {
                    _popupOverlay.Popups.Remove(popup);
                }
            }

            _ = _popupOverlay.RefreshAsync();
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (_clickedPopup != null)
            {
                _popupOverlay.Popups.Remove(_clickedPopup);
            }

            var lonLat = ProjectionConverter.Convert(3857, 4326, e.WorldLocation);
            _clickedPopup = new Popup(e.WorldLocation)
            {
                Content = $"Lat {lonLat.Y:N4}, Lon {lonLat.X:N4}"
            };
            _popupOverlay.Popups.Add(_clickedPopup);
            _ = _popupOverlay.RefreshAsync();
        }
    }
}
