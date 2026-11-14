using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Read the column values of the features in a shapefile - the one you click, or all of them.
    /// </summary>
    public partial class GetDataFromFeatures
    {
        private readonly PopupOverlay _popups = new PopupOverlay();
        private bool _initialized;

        // The tile cutter reads the source it is given on its own threads, so a source that
        // is not safe to read from two threads at once is queried through a copy.
        private FeatureSource _parks;
        private Collection<Feature> _allParks;

        public GetDataFromFeatures()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            try
            {
                Map.MapUnit = GeographyUnit.Meter;

                var parks = new ShapeFileFeatureSource(SampleShared.Shapefile("Parks.shp"))
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857),
                };
                var tiles = new FeatureSourceVectorTileSource();
                tiles.FeatureSources.Add("parks", parks);
                _parks = parks.ConcurrentAccess == ConcurrentAccess.Safe ? parks : parks.CloneDeep();

                var cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) };
                Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, cloud).AddStyle((string)Resources["StyleJson"], tiles));
                Map.Overlays.Add(_popups);

                _parks.Open();
                _allParks = _parks.GetAllFeatures(ReturningColumnsType.AllColumns);
                var parksBBox = _parks.GetBoundingBox();
                _parks.Close();

                ParkList.ItemsSource = _allParks.Select(ColumnText).ToList();

                Map.CenterPoint = parksBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, parksBBox, Map.MapWidth, Map.MapHeight);
                await Map.ZoomInAsync();
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (ParkList == null)
            {
                return;
            }

            ParkList.Visibility = ListAll.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// The park under the click: the features of the source that contain the clicked point.
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            _parks.Open();
            var park = _parks.SpatialQuery(e.WorldLocation, QueryType.Contains, ReturningColumnsType.AllColumns).FirstOrDefault();
            _parks.Close();

            if (park != null)
            {
                _ = ShowColumnsAsync(park);
            }
        }

        private async void ParkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ParkList.SelectedIndex < 0) return;

            try
            {
                var park = _allParks[ParkList.SelectedIndex];
                Map.CenterPoint = park.GetShape().GetCenterPoint();
                await ShowColumnsAsync(park);
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        private async Task ShowColumnsAsync(Feature park)
        {
            _popups.Popups.Clear();
            _popups.Popups.Add(new Popup(park.GetShape().GetCenterPoint())
            {
                Content = ColumnText(park),
                FontSize = 10d,
                FontFamily = new System.Windows.Media.FontFamily("Verdana"),
            });
            await _popups.RefreshAsync();
        }

        /// <summary>
        /// One line per column: each column of a feature is a data attribute.
        /// </summary>
        private static string ColumnText(Feature feature)
        {
            var text = new StringBuilder();
            foreach (var column in feature.ColumnValues)
            {
                text.AppendLine($"{column.Key}: {column.Value}");
            }

            return text.ToString().TrimEnd();
        }
    }
}
