using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// The same classic layers drawn twice: added to a MapStyle with AddFeatureLayer on
    /// the left, drawn by a LayerOverlay on the right, cameras wired together.
    /// </summary>
    public partial class MigrateAClassicLayer
    {
        private static readonly PointShape FriscoCenter = new PointShape(-10777290, 3908740);
        private const double FriscoScale = 9000;

        private bool _syncing;

        public MigrateAClassicLayer()
        {
            InitializeComponent();

            NewMap.MapUnit = GeographyUnit.Meter;
            ClassicMap.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle().SetBackground(GeoColor.FromHtml("#EAE8E2"));
            var warnings = new List<string>();
            foreach (var layer in ClassicLayers())
            {
                var translation = FeatureLayerTranslator.Translate(layer, NewMap.ZoomScales);
                style.AddFeatureLayer(translation);
                warnings.AddRange(translation.Warnings);
            }

            NewMap.Basemap = new GpuBasemap(style);
            Status.Text = warnings.Count == 0
                ? "Every style on these layers has an equivalent; nothing was left out."
                : "Not translated:\n" + string.Join("\n", warnings);

            var overlay = new LayerOverlay { TileType = TileType.SingleTile };
            foreach (var layer in ClassicLayers())
            {
                overlay.Layers.Add(layer);
            }

            ClassicMap.Overlays.Add(overlay);

            NewMap.CenterPoint = FriscoCenter;
            NewMap.CurrentScale = FriscoScale;
            ClassicMap.CenterPoint = FriscoCenter;
            ClassicMap.CurrentScale = FriscoScale;

            NewMap.CurrentExtentChanged += (_, e) => MirrorExtent(ClassicMap, e.NewExtent);
            ClassicMap.CurrentExtentChanged += (_, e) => MirrorExtent(NewMap, e.NewExtent);
        }

        private async void NewMap_Loaded(object sender, RoutedEventArgs e) => await NewMap.RefreshAsync();

        private async void ClassicMap_Loaded(object sender, RoutedEventArgs e) => await ClassicMap.RefreshAsync();

        private void MirrorExtent(MapView target, RectangleShape extent)
        {
            if (_syncing)
            {
                return;
            }

            _syncing = true;
            target.CurrentExtent = extent;
            _ = target.RefreshAsync();
            _syncing = false;
        }

        /// <summary>
        /// The layers as a classic application builds them. Built once per map: a
        /// shapefile is read by one renderer at a time.
        /// </summary>
        private static IEnumerable<FeatureLayer> ClassicLayers()
        {
            ShapeFileFeatureLayer Layer(string file) =>
                new ShapeFileFeatureLayer(SampleShared.Shapefile(file)) { FeatureSource = { ProjectionConverter = new ProjectionConverter(2276, 3857) } };

            var zoning = Layer("Zoning.shp");
            zoning.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new ValueStyle("ZONING", new Collection<ValueItem>
            {
                new ValueItem("C-1", new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#F2C9B4")))),
                new ValueItem("I", new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#CBC3D8")))),
                new ValueItem("AG", new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#D7E3BE")))),
                new ValueItem("O-1", new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#F4E2A6")))),
                new ValueItem("O-2", new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#F4E2A6")))),
            })
            {
                DefaultStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromHtml("#E9E4DA"))),
            });
            zoning.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var parks = Layer("Parks.shp");
            parks.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateHatchStyle(
                GeoHatchStyle.DiagonalCross, GeoColor.FromHtml("#5E9E62"), GeoColor.FromArgb(110, 180, 224, 182), GeoColor.FromHtml("#3F7F45")));
            parks.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TextStyle("NAME", new GeoFont("Segoe UI", 12, DrawingFontStyles.Bold | DrawingFontStyles.Underline), new GeoSolidBrush(GeoColors.DarkGreen))
            {
                HaloPen = new GeoPen(GeoColors.White, 2),
            });
            parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var trails = Layer("Hike_Bike.shp");
            trails.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new LineStyle(new GeoPen(GeoColor.FromHtml("#8C5A2B"), 2) { DashStyle = LineDashStyle.Dash }));
            trails.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var streets = Layer("Streets.shp");
            streets.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new LineStyle(new GeoPen(GeoColors.DimGray, 6), new GeoPen(GeoColors.WhiteSmoke, 4)));
            streets.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            streets.ZoomLevelSet.ZoomLevel17.CustomStyles.Add(new TextStyle("FULL_NAME", new GeoFont("Segoe UI", 11, DrawingFontStyles.Bold | DrawingFontStyles.Strikeout), new GeoSolidBrush(GeoColors.MidnightBlue))
            {
                HaloPen = new GeoPen(GeoColors.White, 2),
            });
            streets.ZoomLevelSet.ZoomLevel17.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var schools = Layer("Schools.shp");
            schools.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(new PointStyle(PointSymbolType.Star, 14, new GeoSolidBrush(GeoColor.FromHtml("#F5B301")), new GeoPen(GeoColor.FromHtml("#8A5A00"), 1)));
            schools.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(new TextStyle("NAME", new GeoFont("Segoe UI", 11, DrawingFontStyles.Bold | DrawingFontStyles.Underline), new GeoSolidBrush(GeoColor.FromHtml("#8A5A00")))
            {
                TextPlacement = TextPlacement.Lower,
                YOffsetInPixel = 2,
                HaloPen = new GeoPen(GeoColors.White, 2),
            });
            schools.ZoomLevelSet.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            yield return zoning;
            yield return parks;
            yield return trails;
            yield return streets;
            yield return schools;
        }
    }
}
