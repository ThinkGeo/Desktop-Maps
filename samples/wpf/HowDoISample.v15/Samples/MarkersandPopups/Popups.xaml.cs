using System;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn to add, edit, or remove popups on the map using the PopupOverlay.
    /// </summary>
    public partial class Popups
    {

        private bool _initialized;
        public Popups()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            var thinkGeoCloudVectorMapsOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = thinkGeoCloudVectorMapsOverlay;

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9030;

            _ = AddHotelPopupsAsync();
        }

        /// <summary>
        /// Adds hotel popups to the map
        /// </summary>
        private async Task AddHotelPopupsAsync()
        {
            // Create a PopupOverlay
            var popupOverlay = new PopupOverlay();

            // Create a layer in order to query the data
            var hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp")
            {
                FeatureSource =
                {
                    // Project the data to match the map's projection
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Open the layer so that we can begin querying
            hotelsLayer.Open();

            // Query all the hotel features
            var hotelFeatures = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

            // Add each hotel feature to the popupOverlay
            foreach (var feature in hotelFeatures)
            {
                var popup = new Popup(feature.GetShape().GetCenterPoint())
                {
                    Content = feature.ColumnValues["NAME"]
                };
                popupOverlay.Popups.Add(popup);
            }

            // Close the hotel layer
            hotelsLayer.Close();

            // Add the popupOverlay to the map and refresh
            Map.Overlays.Add(popupOverlay);

            await Map.RefreshAsync();
        }
    }
}