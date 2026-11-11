using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a popup with the name of each hotel in a shapefile, using the PopupOverlay.
    /// </summary>
    public partial class Popups : IDisposable
    {
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

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            Map.CenterPoint = new PointShape(-10777290, 3908740);
            Map.CurrentScale = 9030;

            _ = AddHotelPopupsAsync();
        }

        private async Task AddHotelPopupsAsync()
        {
            var popupOverlay = new PopupOverlay();

            var hotelsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hotels.shp"))
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            hotelsLayer.Open();
            var hotelFeatures = hotelsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            foreach (var feature in hotelFeatures)
            {
                var popup = new Popup(feature.GetShape().GetCenterPoint())
                {
                    Content = feature.ColumnValues["NAME"]
                };
                popupOverlay.Popups.Add(popup);
            }
            hotelsLayer.Close();

            Map.Overlays.Add(popupOverlay);

            await Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
