using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw only the features that meet a FilterStyle's conditions; several conditions must all be met.
    /// </summary>
    public partial class RenderBasedOnFilters : IDisposable
    {
        private bool _initialized;
        private ShapeFileFeatureLayer _crimeLayer;
        private ShapeFileFeatureLayer _countriesLayer;

        public RenderBasedOnFilters()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _crimeLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco_Crime.shp"));
            // The crimes are in Texas North Central feet (EPSG:2276), the countries in EPSG:4326; the map is in EPSG:3857.
            _crimeLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _crimeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(CreateOffenseGroupFilter("Drugs", "drugs_icon.png", .6));
            _crimeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(CreateOffenseGroupFilter("Weapons", "weapon_icon.png", .25));
            _crimeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(CreateOffenseGroupFilter("Vandalism", "vandalism_icon.png", .25));
            _crimeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _countriesLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Countries02.shp"));
            _countriesLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            _countriesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(205, 233, 232, 214), GeoColor.FromArgb(205, 118, 138, 69)));
            _countriesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new FilterStyle
            {
                // Conditions are joined with AND by default (FilterCondition.Logical = true).
                Conditions =
                {
                    new FilterCondition("POP_CNTRY", ">10000000"),
                    new FilterCondition("LANDLOCKED", "^Y$")
                },
                Styles =
                {
                    new AreaStyle(new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1), new GeoSolidBrush(GeoColor.FromArgb(205, 255, 255, 0)))
                }
            });
            _countriesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(_countriesLayer);
            layerOverlay.Layers.Add(_crimeLayer);
            Map.Overlays.Add(layerOverlay);

            _initialized = true;
            OneCondition.IsChecked = true;
        }

        private static FilterStyle CreateOffenseGroupFilter(string offenseGroup, string iconFile, double iconScale)
        {
            return new FilterStyle
            {
                Conditions = { new FilterCondition("OffenseGro", offenseGroup) },
                Styles =
                {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White, GeoPens.Red),
                    new PointStyle(new GeoImage(Path.Combine(AppContext.BaseDirectory, "Resources", iconFile))) { ImageScale = iconScale }
                }
            };
        }

        private void OneCondition_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _countriesLayer.IsVisible = false;
            _crimeLayer.IsVisible = true;
            Map.CenterPoint = new PointShape(-10778210, 3914410);
            Map.CurrentScale = 18060;
            _ = Map.RefreshAsync();
        }

        private void TwoConditions_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _crimeLayer.IsVisible = false;
            _countriesLayer.IsVisible = true;
            Map.CenterPoint = MaxExtents.SphericalMercator.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, MaxExtents.SphericalMercator, Map.MapWidth, Map.MapHeight);
            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
