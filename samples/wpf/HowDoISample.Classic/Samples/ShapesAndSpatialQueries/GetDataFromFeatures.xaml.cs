using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Read the column values of features: the park you click, or every hotel in a shapefile.
    /// </summary>
    public partial class GetDataFromFeatures : IDisposable
    {
        private bool _initialized;
        private string _mode = "one";
        private ShapeFileFeatureLayer _parksLayer;
        private ShapeFileFeatureLayer _hotelsLayer;
        private InMemoryFeatureLayer _highlightedHotelLayer;
        private LayerOverlay _parksOverlay;
        private LayerOverlay _hotelsOverlay;
        private PopupOverlay _popupOverlay;

        public GetDataFromFeatures()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            _parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);
            _parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _parksOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _parksOverlay.Layers.Add(_parksLayer);
            Map.Overlays.Add(_parksOverlay);

            _hotelsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"));
            _hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _hotelsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            _hotelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _highlightedHotelLayer = new InMemoryFeatureLayer();
            _highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 30, GeoBrushes.BrightYellow, GeoPens.Black);
            _highlightedHotelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _hotelsOverlay = new LayerOverlay { IsVisible = false };
            _hotelsOverlay.Layers.Add(_hotelsLayer);
            _hotelsOverlay.Layers.Add(_highlightedHotelLayer);
            Map.Overlays.Add(_hotelsOverlay);

            _popupOverlay = new PopupOverlay();
            Map.Overlays.Add(_popupOverlay);

            _ = RunModeAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _mode = (string)((RadioButton)sender).Tag;
            _ = RunModeAsync();
        }

        private async Task RunModeAsync()
        {
            try
            {
                if (_mode == "all")
                {
                    await ListAllHotelsAsync();
                }
                else
                {
                    await ShowParksAsync();
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async Task ShowParksAsync()
        {
            _parksOverlay.IsVisible = true;
            _hotelsOverlay.IsVisible = false;
            LsbHotels.Visibility = Visibility.Collapsed;
            Status.Text = "Click a park on the map.";

            _parksLayer.Open();
            var parksBBox = _parksLayer.GetBoundingBox();
            Map.CenterPoint = parksBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, parksBBox, Map.MapWidth, Map.MapHeight);
            await Map.ZoomInAsync();
            await Map.RefreshAsync();
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (_mode != "one") return;

            _parksLayer.Open();
            var park = _parksLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.AllColumns).FirstOrDefault();
            if (park == null) return;

            var parkInfo = new StringBuilder();
            foreach (var column in park.ColumnValues)
            {
                parkInfo.AppendLine($"{column.Key}: {column.Value}");
            }

            _popupOverlay.Popups.Clear();
            _popupOverlay.Popups.Add(new Popup(park.GetShape().GetCenterPoint())
            {
                Content = parkInfo.ToString(),
                FontSize = 10d,
                FontFamily = new FontFamily("Verdana")
            });
            _ = _popupOverlay.RefreshAsync();
        }

        private async Task ListAllHotelsAsync()
        {
            _popupOverlay.Popups.Clear();
            _parksOverlay.IsVisible = false;
            _hotelsOverlay.IsVisible = true;
            LsbHotels.Visibility = Visibility.Visible;
            Status.Text = "Pick a hotel from the list to zoom to it.";

            _hotelsLayer.Open();
            LsbHotels.ItemsSource = _hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            var hotelsBBox = _hotelsLayer.GetBoundingBox();

            Map.CenterPoint = hotelsBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, hotelsBBox, Map.MapWidth, Map.MapHeight);
            await Map.RefreshAsync();
        }

        private async void LsbHotels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(LsbHotels.SelectedItem is Feature hotel)) return;

            try
            {
                _highlightedHotelLayer.InternalFeatures.Clear();
                _highlightedHotelLayer.InternalFeatures.Add(hotel);
                await Map.ZoomToAsync(hotel.GetShape().GetCenterPoint(), new ZoomLevelSet().ZoomLevel18.Scale);
                await Map.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
